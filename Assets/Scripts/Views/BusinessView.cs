using TMPro;
using UnityEngine;

namespace Views
{
    public class BusinessView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _businessName;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _income;
        [SerializeField] private TMP_Text _levelUpPrice;

        public void Display(string businessName, string level, string income, string levelPrice)
        {
            _businessName.text = businessName;
            _level.text = "Level " + level;
            _income.text = "Income: " + income;
            _levelUpPrice.text = levelPrice + "$";
        }
    }
}