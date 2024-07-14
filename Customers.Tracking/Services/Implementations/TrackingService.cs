using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers.CommonModels;
using Customers.Infrastructure;
using Customers.Tracking.Data;
using Customers.Tracking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Tracking.Services.Implementations
{
    public class TrackingService : BaseService, ITrackingService
    {

        private ITrackingLogEventRepository _repository;

        public TrackingService(ITrackingLogEventRepository repository)
        {
            _repository = repository;
        }

        public ServiceResponse<TrackingLogEvent> SaveEvent(TrackingLogEvent trackingLogEvent)
        {
            return TryExecute(()=>
            {
                var insertedTrackingLogEvent = _repository.InsertEvent(trackingLogEvent);
                return ServiceResponse<TrackingLogEvent>.Success(insertedTrackingLogEvent);
            });
           
        }

        public ServiceResponse<List<TrackingLogEvent>> GetLogs()
        {
            return TryExecute(() =>
            {
                var logList = _repository.GetEvents()
                    .OrderByDescending(logEvent => logEvent.EventDate)
                    .Take(20)
                    .ToList();
                return ServiceResponse<List<TrackingLogEvent>>.Success(logList);
            });
        }

    }
}
