using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

public class EventPublisherSale : IEventPublisherSale
{
    private readonly ILogger<EventPublisherSale> _logger;

    public EventPublisherSale(ILogger<EventPublisherSale> logger)
    {
        _logger = logger;
    }

    public void PublishEvent(EventPublisherEnum eventEnum)
    {
        {
            switch (eventEnum)
            {
                case EventPublisherEnum.SaleCreated:
                    _logger.LogInformation("Event: Sale Created.");
                    break;

                case EventPublisherEnum.SaleModified:
                    _logger.LogInformation("Event: Sale Modified.");
                    break;

                case EventPublisherEnum.SaleCancelled:
                    _logger.LogInformation("Event: Sale Cancelled.");
                    break;

                case EventPublisherEnum.ItemCancelled:
                    _logger.LogInformation("Event: Item Cancelled.");
                    break;

                default:
                    _logger.LogInformation("Unknown Event.");
                    break;
            }
        }
    }
}
