using Models;
using SimpleEventBus.Events;

namespace Events
{
    public class TimeIncomeEvent : EventBase
    {
        public BusinessModel BusinessModel { get; }

        public TimeIncomeEvent(BusinessModel businessModel)
        {
            BusinessModel = businessModel;
        }
    }
}