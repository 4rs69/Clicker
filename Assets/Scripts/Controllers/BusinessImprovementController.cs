using System;
using Events;
using Models;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Controllers
{
    public class BusinessImprovementController : MonoCache
    {
        [SerializeField] private BusinessImprovementView _businessImprovementView;
        [SerializeField] private Button _businessImproveButton;
        
        private BusinessImprovementModel _model;
        private Action _incomeUpdate;
        
        public void Initialize(BusinessImprovementModel model, Action incomeUpdate)
        {
            _incomeUpdate = incomeUpdate;
            _model = model;
            _businessImproveButton.onClick.AddListener(OnImproveBusiness);
            DisplayView();
        }
        
        public override void OnTick()
        {
            DisplayView();
        }

        private void OnImproveBusiness()
        {
            if (!_model.IsPurchased)
            {
                EventStreams.Game.Publish(new BusinessImproveWithoutBalanceEvent(_model));
                _incomeUpdate?.Invoke();
            }
        }

        private void DisplayView()
        {
            var state = _model.IsPurchased ? "Purchased" : "Price: " + _model.Price;
            _businessImprovementView.Display(_model.Name, _model.BoostIncome.ToString(), state);
        }
    }
}