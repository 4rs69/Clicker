using Events;
using Models;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Controllers
{
    public class BusinessController : MonoCache
    {
        [SerializeField] private BusinessView _businessView;
        [SerializeField] private ProcessIncomeController _processIncomeController;
        [SerializeField] private BusinessImprovementController[] _businessImprovementControllers;
        [SerializeField] private Button _levelUpButton;

        private BusinessModel _model;

        public void Initialize(BusinessModel model)
        {
            _model = model;
            _processIncomeController.Initialize(model);
            
            for (var i = 0; i < _businessImprovementControllers.Length; i++)
            {
                _businessImprovementControllers[i].Initialize(_model.BusinessImprovementModels[i], OnIncomeUpdate);
            }
            
            _levelUpButton.onClick.AddListener(OnLevelUp);
            DisplayView();
        }
        
        public override void OnTick()
        {
            DisplayView();
        }

        private void OnIncomeUpdate()
        {
            EventStreams.Game.Publish(new IncomeUpdateEvent(_model));
        }

        private void OnLevelUp()
        {
            EventStreams.Game.Publish(new LevelUpWithoutBalanceEvent(_model));
        }

        private void DisplayView()
        {
            _businessView.Display(_model.Name, _model.Level.ToString(), _model.CurrentIncome.ToString(), _model.CurrentPrice.ToString());
        }
    }
}