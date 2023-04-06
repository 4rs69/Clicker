using System;

namespace Models
{
    [Serializable] 
    public class BusinessModel
    {
        public string Name { get; }
        public float IncomeDelay { get; }
        public float BaseIncome { get; }
        public float BasePrice { get; }
        
        public float CurrentIncome { get; set; }
        public int Level { get; set; }
        public float CurrentPrice { get; set; }
        public readonly BusinessImprovementModel[] BusinessImprovementModels;
        
        public BusinessModel(string name, float incomeDelay, int level, float baseIncome, float currentIncome, float currentPrice, float basePrice, BusinessImprovementModel[] businessImprovementModels)
        {
            Name = name;
            IncomeDelay = incomeDelay;
            Level = level;
            BaseIncome = baseIncome;
            CurrentIncome = currentIncome;
            BasePrice = basePrice;
            CurrentPrice = currentPrice;
            BusinessImprovementModels = businessImprovementModels;
        }
    }
}