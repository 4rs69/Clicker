using Events;
using Models;
using UnityEngine;
using Views;

namespace Controllers
{
    public class ProcessIncomeController : MonoCache
    {
        [SerializeField] private ProcessIncomeView _processIncomeView;
        
        private float _currentTime, _timeDelay;
        private BusinessModel _businessModel;

        public void Initialize(BusinessModel businessModel)
        {
            _timeDelay = businessModel.IncomeDelay;
            _businessModel = businessModel;
            
            _processIncomeView.Display(_currentTime);
        }

        public override void OnTick()
        {
            if (_businessModel.Level >= 1)
            {
                UpdateStatusProcessBar();   
            }
        }

        private void UpdateStatusProcessBar()
        {
            _currentTime += Timer % _timeDelay;

            var ratio = _currentTime / _timeDelay;
            if (ratio >= 1)
            {
                ratio = 1;
                _currentTime = 0;

                EventStreams.Game.Publish(new TimeIncomeEvent(_businessModel));
            }

            _processIncomeView.Display(ratio);
        }
    }
}