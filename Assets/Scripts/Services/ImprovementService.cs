using System;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

namespace Services
{
    public class ImprovementService : MonoBehaviour, IDisposable
    {
        [SerializeField] private int _countOfLevels = 5;
        
        private CompositeDisposable _subscriptions;

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
        
        private void Awake()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.Game.Subscribe<LevelUpWithBalanceEvent>(LevelUp),
                EventStreams.Game.Subscribe<BusinessImproveWithBalanceEvent>(ImproveBusiness)
            };
        }

        private void ImproveBusiness(BusinessImproveWithBalanceEvent eventData)
        {
            var playerModel = eventData.PlayerBalanceModel;
            var improvementBusiness = eventData.BusinessImprovementModel;

            if (playerModel.Balance >= improvementBusiness.Price)
            {
                improvementBusiness.IsPurchased = true;
                playerModel.Balance -= improvementBusiness.Price;
            }
        }
        
        private void LevelUp(LevelUpWithBalanceEvent eventData)
        {
            var playerModel = eventData.PlayerBalanceModel;
            var businessModel = eventData.BusinessModel;

            if (playerModel.Balance >= businessModel.CurrentPrice && businessModel.Level < _countOfLevels)
            {
                businessModel.Level += 1;
                playerModel.Balance -= businessModel.CurrentPrice;
                EventStreams.Game.Publish(new LevelPriceUpEvent(businessModel));
            }
           
        }
    }
}