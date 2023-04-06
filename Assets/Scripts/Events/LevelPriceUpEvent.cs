using Models;
using SimpleEventBus.Events;

namespace Events
{
    public class LevelPriceUpEvent : EventBase
    {
        public BusinessModel BusinessModel { get; }

        public LevelPriceUpEvent(BusinessModel businessModel)
        {
            BusinessModel = businessModel;
        }
    }
}