using Models;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BusinessConfig", menuName = "BusinessConfig")]
    public class BusinessConfig : ScriptableObject
    {
        public ConfigBusinessModel[] BusinessModels => _businessModel;

        [SerializeField] private ConfigBusinessModel[] _businessModel;
    }
}
