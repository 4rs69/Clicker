using System;
using Controllers;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

namespace Services
{
    public class IncomeService : MonoBehaviour, IDisposable
    {
        [SerializeField] private PlayerBalanceController _playerBalanceController;
        
        private CompositeDisposable _subscriptions;

        //todo передовать вместо флота startBlanacePlayer
        public void Initialize(float startBalanceConfig)
        {
            _playerBalanceController.Initialize(startBalanceConfig);
        }
        
        public float GetPlayerBalance()
        {
            return _playerBalanceController.GetPlayerModel().Balance;
        }
        
        public void Dispose()
        {
            _subscriptions?.Dispose();
        }

        private void Awake()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.Game.Subscribe<LevelUpWithoutBalanceEvent>(AddBalanceToEventContext),
                EventStreams.Game.Subscribe<BusinessImproveWithoutBalanceEvent>(AddBalanceToEventContext),
                EventStreams.Game.Subscribe<BalanceUpEvent>(PlayerBalanceUp)
            };
        }

        private void PlayerBalanceUp(BalanceUpEvent eventData)
        {
            _playerBalanceController.GetPlayerModel().Balance += eventData.BalanceUp;
        }
        
        private void AddBalanceToEventContext(BusinessImproveWithoutBalanceEvent eventData)
        {
            EventStreams.Game.Publish(new BusinessImproveWithBalanceEvent(eventData.BusinessImprovementModel, _playerBalanceController.GetPlayerModel()));
        }
        
        private void AddBalanceToEventContext(LevelUpWithoutBalanceEvent eventData)
        {
            EventStreams.Game.Publish(new LevelUpWithBalanceEvent(eventData.BusinessModel, _playerBalanceController.GetPlayerModel()));
        }
    }
}
