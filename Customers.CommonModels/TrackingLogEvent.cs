namespace Customers.CommonModels;

public class TrackingLogEvent
{
    public int Id { get; set; }
    public DateTime EventDate { get; set; }
    public TrackingLogEventType EventTypeId { get; set; }
    public string UserIPAddress { get; set; }
    public int? CustomerId { get; set; }
    public string? UpdatePageUrl { get; set; }
}
