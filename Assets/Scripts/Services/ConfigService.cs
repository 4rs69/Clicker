using System;
using Events;
using Models;
using ScriptableObjects;
using SimpleEventBus.Disposables;
using TEst;
using UnityEngine;

namespace Services
{
    public class ConfigService : MonoBehaviour,IDisposable
    {
        [SerializeField] private BusinessConfig _businessConfig;

        private BusinessModel[] _businessModels;
        private CompositeDisposable _subscriptions;
        
        public void Initialize(BusinessModel[] businessModels = null)
        {
            if (businessModels == null)
            {
                CreateBusinessModels();
                return;
            }
            _businessModels = businessModels;
        }

        public BusinessModel[] GetBusinessModels()
        {
            return _businessModels;
        }
        
        public void Dispose()
        {
            _subscriptions?.Dispose();
        }

        private void Awake()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.Game.Subscribe<LevelPriceUpEvent>(CountPriceLevelUp),
                EventStreams.Game.Subscribe<TimeIncomeEvent>(DistributeIncome),
                EventStreams.Game.Subscribe<IncomeUpdateEvent>(InitializeUpdateIncome)
            };
        }

        private void CreateBusinessModels()
        {
            _businessModels = new BusinessModel[_businessConfig.BusinessModels.Length];

            for (var i = 0; i < _businessConfig.BusinessModels.Length; i++)
            {
                var businessName = _businessConfig.BusinessModels[i].Name;
                var incomeDelay = _businessConfig.BusinessModels[i].IncomeDelay;
                var level = _businessConfig.BusinessModels[i].Level;
                var income = _businessConfig.BusinessModels[i].Income;
                var price = _businessConfig.BusinessModels[i].Price;
                
                var businessImproves = CreateBusinessImproveModels(i);

                _businessModels[i] = new BusinessModel(businessName, incomeDelay, level, income, income, price, price, businessImproves);
            }
        }

        private BusinessImprovementModel[] CreateBusinessImproveModels(int index)
        {
            var businessImproves = new BusinessImprovementModel[_businessConfig.BusinessModels[index].TypesImprovement.Length];
            for (var j = 0; j < businessImproves.Length; j++)
            {
                var businessImproveName = _businessConfig.BusinessModels[index].TypesImprovement[j].Name;
                var businessImprovePrice = _businessConfig.BusinessModels[index].TypesImprovement[j].Price;
                var businessImproveBoostIncome = _businessConfig.BusinessModels[index].TypesImprovement[j].BoostIncome;
                var businessImproveIsPurchased = _businessConfig.BusinessModels[index].TypesImprovement[j].IsPurchased;

                businessImproves[j] = new BusinessImprovementModel(businessImproveName, businessImprovePrice,
                    businessImproveBoostIncome, businessImproveIsPurchased);
            }

            return businessImproves;
        }

        private void InitializeUpdateIncome(IncomeUpdateEvent eventData)
        {
            CountIncome(eventData.BusinessModel);
        }
        
        private void DistributeIncome(TimeIncomeEvent eventData)
        {
            var businessModel = eventData.BusinessModel;
            CountIncome(businessModel);
            EventStreams.Game.Publish(new BalanceUpEvent(businessModel.CurrentIncome));
        }
        
        private void CountIncome(BusinessModel businessModel)
        {
            var firstImproveBoost = businessModel.BusinessImprovementModels[0].IsPurchased ?
                businessModel.BusinessImprovementModels[0].BoostIncome : 0;
            var secondImproveBoost = businessModel.BusinessImprovementModels[1].IsPurchased ? 
                businessModel.BusinessImprovementModels[1].BoostIncome : 0;
            
            var income = businessModel.Level * businessModel.BaseIncome + 
                         (firstImproveBoost + secondImproveBoost);

            if (income > 0)
                businessModel.CurrentIncome = income;
        }

        private void CountPriceLevelUp(LevelPriceUpEvent eventData)
        {
            var businessModel = eventData.BusinessModel;
            businessModel.CurrentPrice = (businessModel.Level + 1) * businessModel.BasePrice;
            CountIncome(businessModel);
        }
    }
}
