using Customers.MainApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customers.MainApp.Controllers
{
    public class TrackingLogsController : BaseController
    {
        private readonly ITrackingLogsService _trackingLogsService;

        public TrackingLogsController(ITrackingLogsService trackingLogsService)
        {
            _trackingLogsService = trackingLogsService;
        }

        public IActionResult Index()
        {
            var response = _trackingLogsService.GetLatestLogs();
            if (IsErrorResponse(response, out var actionResult))
                return actionResult;

            ViewData.Model = response.ResponseDTO;
            return View();
        }

    }
}
