using System;
using DG.Tweening;
using Events;
using FSM;
using SimpleEventBus.Disposables;
using Unity.VisualScripting;
using UnityEngine;
using StateMachine = FSM.StateMachine;

namespace Services
{
    public class StateService : MonoBehaviour, IDisposable
    {
        [SerializeField] private CanvasGroup _startScreen;
        [SerializeField] private CanvasGroup _gameScreen;
        [SerializeField] private CanvasGroup _pauseScreen;
        [SerializeField] private CanvasGroup _achievementScreen;
        [SerializeField] private GlobalUpdate _globalUpdate;
    
        private StateMachine _stateMachine;
        private CompositeDisposable _subscriptions;

        private bool _isPauseScreen;
        private bool _isGameScreen;
        private bool _isAchievementScreen;

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
    
        private void Awake()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.Game.Subscribe<ChangeGameStateEvent>(SetGameState),
                EventStreams.Game.Subscribe<ChangeGameStateEvent>(SetStartState),
                EventStreams.Game.Subscribe<ChangeAchievementStateEvent>(SetAchievementState)
            };
        
            _stateMachine = new StateMachine();
            
            _stateMachine.AddState("StartState",
                _ =>
                {
                    FadeOut(_startScreen);
                },
                onExit:_ =>
                {
                    FadeIn(_startScreen);
                });
            
            
            _stateMachine.AddState("GameState",
                _ =>
                {
                    FadeOut(_gameScreen);
                },
                state =>
                {
                    if (Input.GetKeyDown(KeyCode.Escape) && state.timer.Elapsed >= 0.5f)
                    {
                        _isPauseScreen = true;
                        _stateMachine.RequestStateChange("PauseState");
                    }
                },
                _ =>
                {
                    FadeIn(_gameScreen);
                });
            
            _stateMachine.AddState("PauseState",
                _ => 
                {
                    FadeOut(_pauseScreen);
                    _globalUpdate.gameObject.SetActive(false);
                },
                _ =>
                {
                    if (!_isPauseScreen)
                    {
                        _stateMachine.RequestStateChange("GameState");
                    }
                },
                _ =>
                {
                    FadeIn(_pauseScreen);
                    _globalUpdate.gameObject.SetActive(true);
                });
            
            _stateMachine.AddState("AchievementState",
                _ => 
                {
                    FadeOut(_achievementScreen);
                },
                onExit: _ =>
                {
                    FadeIn(_achievementScreen);
                });
            
            _stateMachine.SetStartState("StartState");
        
            _stateMachine.AddTransition("StartState", "AchievementState", _ => _isAchievementScreen);
            _stateMachine.AddTransition("AchievementState", "StartState", _ => !_isAchievementScreen);
            _stateMachine.AddTransition("GameState", "PauseState", _ => _isPauseScreen);
            _stateMachine.AddTransition("PauseState", "GameState", _ => !_isPauseScreen);
            _stateMachine.AddTransition("StartState", "GameState", _ => _isGameScreen);

            _stateMachine.Init();
        }

        private void Update()
        {
            _stateMachine.OnLogic();
        }

        private void SetGameState(ChangeGameStateEvent eventData)
        {
            _isPauseScreen = false;
        }

        private void SetStartState(ChangeGameStateEvent eventData)
        {
            _isGameScreen = true;
        }

        private void SetAchievementState(ChangeAchievementStateEvent eventData)
        {
            _isAchievementScreen = true;
        }
    
        private void FadeIn(CanvasGroup disappearingCanvas)
        {
            disappearingCanvas.DOFade(0f,0.2f).OnComplete(() =>
            {
                disappearingCanvas.gameObject.SetActive(false);
            });
        }

        private void FadeOut(CanvasGroup appearingCanvas)
        {
            appearingCanvas.gameObject.SetActive(true);
            appearingCanvas.DOFade(1f,0.2f);
        }
    }
}