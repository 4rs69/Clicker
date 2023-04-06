using System;

namespace Models
{
    [Serializable]
    public class BusinessImprovementModel
    {
        public string Name { get; }
        public float Price { get; }
        public float BoostIncome { get; }
        
        public bool IsPurchased { get; set; }
        
        
        public BusinessImprovementModel(string name, float price, float boostIncome, bool isPurchased)
        {
            Name = name;
            Price = price;
            BoostIncome = boostIncome;
            IsPurchased = isPurchased;
        }
    }
}