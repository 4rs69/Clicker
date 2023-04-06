using Models;
using SimpleEventBus.Events;

namespace Events
{
    public class BusinessImproveWithBalanceEvent : EventBase
    {
        public BusinessImprovementModel BusinessImprovementModel { get; }
        public PlayerBalanceModel PlayerBalanceModel { get; }
        
        public BusinessImproveWithBalanceEvent(BusinessImprovementModel businessImprovementModel, PlayerBalanceModel playerBalanceModel)
        {
            BusinessImprovementModel = businessImprovementModel;
            PlayerBalanceModel = playerBalanceModel;
        }
    }
}