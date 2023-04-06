using System.Collections.Generic;
using Controllers;
using Models;
using UnityEngine;

namespace Services
{
    public class BusinessService : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private BusinessController _businessControllerPrefab;

        private readonly List<BusinessController> _controllers = new ();

        public void Initialize(BusinessModel[] businessModels)
        {
            foreach (var model in businessModels)
            {
                var businessController = Instantiate(_businessControllerPrefab, _parent);
                businessController.Initialize(model);
                _controllers.Add(businessController);
            }
        }

        public void ResetControllers()
        {
            foreach (var controller in _controllers)
            {
                Destroy(controller.gameObject);
            }
            _controllers.Clear();
        }
    }
}