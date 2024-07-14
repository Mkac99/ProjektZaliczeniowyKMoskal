using System.Collections.Generic;
using Customers.CommonModels;
using Customers.Infrastructure;

namespace Customers.Tracking.Services
{
    public interface ITrackingService
    {
        ServiceResponse<TrackingLogEvent> SaveEvent(TrackingLogEvent trackingLogEvent);
        ServiceResponse<List<TrackingLogEvent>> GetLogs();
    }
}
