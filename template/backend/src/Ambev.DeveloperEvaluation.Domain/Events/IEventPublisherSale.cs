namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// interface for event sale
/// </summary>
public interface IEventPublisherSale
{
    /// <summary>
    ///
    /// </summary>
    /// <returns>True if the Sale was deleted, false if not found</returns>
    public void PublishEvent(EventPublisherEnum eventEnum);
}
