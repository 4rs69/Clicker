using Models;
using SimpleEventBus.Events;

namespace Events
{
    public class BusinessImproveWithoutBalanceEvent : EventBase
    {
        public BusinessImprovementModel BusinessImprovementModel { get; }
        
        public BusinessImproveWithoutBalanceEvent(BusinessImprovementModel businessImprovementModel)
        {
            BusinessImprovementModel = businessImprovementModel;
        }
    }
}