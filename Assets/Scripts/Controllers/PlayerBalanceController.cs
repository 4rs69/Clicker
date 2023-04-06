using Configs;
using Models;
using UnityEngine;
using Views;

namespace Controllers
{
    public class PlayerBalanceController : MonoCache
    {
        [SerializeField] private PlayerBalanceView _playerBalanceView;

        private PlayerBalanceModel _model;

        public void Initialize(float startBalance)
        {
            _model = new PlayerBalanceModel
            {
                Balance = startBalance
            };
            DisplayView();
        }
        
        public override void OnTick()
        {
            DisplayView();
        }

        public PlayerBalanceModel GetPlayerModel()
        {
            return _model;
        }

        private void DisplayView()
        {
            _playerBalanceView.Display(_model.Balance.ToString());
        }
    }
}