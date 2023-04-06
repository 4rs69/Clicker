using SimpleEventBus.Events;

namespace Events
{
    public class BalanceUpEvent : EventBase
    {
        public float BalanceUp { get; }
        
        public BalanceUpEvent(float balanceUp)
        {
            BalanceUp = balanceUp;
        }
    }
}