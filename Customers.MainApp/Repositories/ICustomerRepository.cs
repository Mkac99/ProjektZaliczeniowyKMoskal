using Customers.MainApp.Models;
using System.Collections.Generic;

namespace Customers.MainApp.Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        Customer GetCustomerById(int customerId);
        Customer InsertCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
