using TMPro;
using UnityEngine;

namespace Views
{
    public class PlayerBalanceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _balance;

        public void Display(string balance)
        {
            _balance.text = "Balance: " + balance + "$";
        }
    }
}