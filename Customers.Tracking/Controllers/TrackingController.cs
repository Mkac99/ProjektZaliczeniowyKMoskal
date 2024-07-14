﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Customers.CommonModels;
using Customers.Infrastructure;
using Customers.Tracking.Services;

namespace Customers.Tracking.Controllers
{
    public class TrackingController : Controller
    {
        private ITrackingService _trackingService;

        public TrackingController(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        [HttpPost]
        public IActionResult Event([FromBody] TrackingLogEvent trackingLogEvent)
        {
           
            var serviceResponse = _trackingService.SaveEvent(trackingLogEvent);
            if (!serviceResponse.IsSuccess)
            {
                return Problem(
                    detail: serviceResponse.ErrorDetails.Message,
                    statusCode: serviceResponse.ErrorDetails.Code);
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult Logs()
        {
            var serviceResponse = _trackingService.GetLogs();
            if (!serviceResponse.IsSuccess)
            {
                return Problem(
                    detail: serviceResponse.ErrorDetails.Message,
                    statusCode: serviceResponse.ErrorDetails.Code);
            }
            return Ok(serviceResponse.ResponseDTO);
        }

    }
}
