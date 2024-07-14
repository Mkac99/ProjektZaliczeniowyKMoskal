using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers.CommonModels;
using Customers.Tracking.Data;

namespace Customers.Tracking.Repositories
{
    public class TrackingLogEventRepository : ITrackingLogEventRepository
    {

        private TrackingContext _trackingContext;

        public TrackingLogEventRepository(TrackingContext trackingContext)
        {
            _trackingContext = trackingContext;
        }

        public List<TrackingLogEvent> GetEvents()
        {
            return _trackingContext.TrackingLogEvent.ToList();
        }

        public TrackingLogEvent InsertEvent(TrackingLogEvent trackingLogEvent)
        {
            var entityEntry = _trackingContext.TrackingLogEvent.Add(trackingLogEvent);
            _trackingContext.SaveChanges();
            return entityEntry.Entity;
        }
    }
}
