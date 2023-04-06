using Models;
using SimpleEventBus.Events;

namespace Events
{
    public class LevelUpWithoutBalanceEvent : EventBase
    {
        public BusinessModel BusinessModel { get; }

        public LevelUpWithoutBalanceEvent(BusinessModel businessModel)
        {
            BusinessModel = businessModel;
        }
    }
}