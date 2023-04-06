using Models;
using SimpleEventBus.Events;

namespace Events
{
    public class LevelUpWithBalanceEvent : EventBase
    {
        public BusinessModel BusinessModel { get; }
        public PlayerBalanceModel PlayerBalanceModel { get; }
        
        public LevelUpWithBalanceEvent(BusinessModel businessModel, PlayerBalanceModel playerBalanceModel)
        {
            BusinessModel = businessModel;
            PlayerBalanceModel = playerBalanceModel;
        }
    }
}