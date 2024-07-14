using Customers.Infrastructure;
using Customers.MainApp.Models;
using Customers.MainApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Customers.MainApp.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
            
        }

        public IActionResult Index()
        {
            EnsureCustomerServiceInitialized();
            var response = _customerService.GetCustomerList();
            if (IsErrorResponse(response, out var actionResult))
                return actionResult;
            ViewData.Model = response.ResponseDTO;
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            EnsureCustomerServiceInitialized();
            var response = _customerService.AddCustomer(customer);
            if (IsErrorResponse(response, out var actionResult))
                return actionResult;
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            EnsureCustomerServiceInitialized();
            var response = _customerService.GetCustomer(id);
            if (IsErrorResponse(response, out var actionResult))
                return actionResult;

            ViewData.Model = response.ResponseDTO;
            return View();
        }

        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            EnsureCustomerServiceInitialized();
            var response = _customerService.UpdateCustomer(customer);
            if (IsErrorResponse(response, out var actionResult))
                return actionResult;
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            EnsureCustomerServiceInitialized();
            var response = _customerService.GetCustomer(id);
            if (IsErrorResponse(response, out var actionResult))
                return actionResult;

            ViewData.Model = response.ResponseDTO;
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            EnsureCustomerServiceInitialized();
            var response = _customerService.DeleteCustomer(customer.Id);
            if (IsErrorResponse(response, out var actionResult))
                return actionResult;
        
            return RedirectToAction(nameof(Index));
        }

        private void EnsureCustomerServiceInitialized()
        {
            _customerService.EnsureInitialized($"{Request.Scheme}://{Request.Host}{Request.PathBase}",
                Request.HttpContext.Connection.RemoteIpAddress.ToString());
        }
    }
}
