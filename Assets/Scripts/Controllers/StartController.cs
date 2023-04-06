using Events;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class StartController : MonoBehaviour
    {
        [SerializeField] private Button _continue;
        [SerializeField] private Button _newGame;
        [SerializeField] private Button _achievements;
        [SerializeField] private Button _exitGame;

        private void Awake()
        {
            _continue.onClick.AddListener(OnContinueGame);
            _newGame.onClick.AddListener(OnStartNewGame);
            _achievements.onClick.AddListener(OnAchievement);
            _exitGame.onClick.AddListener(OnExitGame);
            
        }

        private void OnStartNewGame()
        {
            EventStreams.Game.Publish(new NewGameEvent());
            EventStreams.Game.Publish(new ChangeGameStateEvent());
        }

        private void OnContinueGame()
        {
            EventStreams.Game.Publish(new ChangeGameStateEvent());
           
        }

        private void OnExitGame()
        {
            EventStreams.Game.Publish(new ExitGameEvent());
        }

        private void OnAchievement()
        {
            EventStreams.Game.Publish(new ChangeAchievementStateEvent());
        }
    }
}