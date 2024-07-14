using System.Security.Cryptography.X509Certificates;
using Customers.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Customers.MainApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Message()
        {
           
            if (TempData.ContainsKey(nameof(ErrorDetails)))
            {
                string json = TempData[nameof(ErrorDetails)].ToString();
                ViewData.Model = JsonConvert.DeserializeObject<ErrorDetails>(json);
            }
            else
            {
                ViewData.Model=new ErrorDetails(-1,"Unknown Error");
            }

            return View();
        }
    }
}
