using TMPro;
using UnityEngine;

namespace Views
{
    public class BusinessImprovementView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _improvementName;
        [SerializeField] private TMP_Text _boostIncome;
        [SerializeField] private TMP_Text _state;


        public void Display(string improvementName, string boostIncome, string state)
        {
            _boostIncome.text = "Income: " + boostIncome + "$";
            _improvementName.text = improvementName;
            _state.text = state;
        }
    }
}