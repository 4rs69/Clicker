using Models;
using SimpleEventBus.Events;

namespace Events
{
    public class IncomeUpdateEvent : EventBase
    {
        public BusinessModel BusinessModel { get; }
        
        public IncomeUpdateEvent(BusinessModel businessModel)
        {
            BusinessModel = businessModel;
        }
    }
}