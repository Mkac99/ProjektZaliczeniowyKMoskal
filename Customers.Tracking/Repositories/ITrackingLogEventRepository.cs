using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers.CommonModels;

namespace Customers.Tracking.Repositories
{
    public interface ITrackingLogEventRepository
    {
        public List<TrackingLogEvent> GetEvents();
        public TrackingLogEvent InsertEvent(TrackingLogEvent trackingLogEvent);
    }
}
