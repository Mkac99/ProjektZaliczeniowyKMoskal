using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers.CommonModels;
using Customers.Infrastructure;

namespace Customers.MainApp.Services
{
    public interface ITrackingLogsService
    {
        ServiceResponse SendEvent(TrackingLogEvent trackingLogEvent);
        ServiceResponse<List<TrackingLogEvent>> GetLatestLogs();
    }
}
