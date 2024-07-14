using Customers.Infrastructure;
using Customers.MainApp.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Customers.MainApp.Services
{
    public interface ICustomerService
    {
        void EnsureInitialized(string applicationUrl, string userIpAddress);

        ServiceResponse<Customer> GetCustomer(int id);
        ServiceResponse<Customer> AddCustomer(Customer customer);
        ServiceResponse<Customer> UpdateCustomer(Customer customer);
        ServiceResponse DeleteCustomer(int id);
        ServiceResponse<List<Customer>> GetCustomerList();
    }
}
