using abc.Domain;
using Prism.Events;

namespace abc.infrastructure.Events
{
    public class StockUpdatedEvent : PubSubEvent<Stock>
    {
    }
}
