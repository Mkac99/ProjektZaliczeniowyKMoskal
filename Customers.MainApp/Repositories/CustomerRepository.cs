using Customers.MainApp.Data;
using Customers.MainApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Customers.MainApp.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomersContext _context;

        public CustomerRepository(CustomersContext context)
        {
            _context = context;
        }

        public List<Customer> GetCustomers()
        {
            return _context.Customer.ToList();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _context.Customer.Find(customerId);
        }

        public Customer InsertCustomer(Customer customer)
        {
            var entityEntry = _context.Customer.Add(customer);
            _context.SaveChanges();
            return entityEntry.Entity;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            var entityEntry = _context.Customer.Update(customer);
            _context.SaveChanges();
            return entityEntry.Entity;
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customer.Remove(customer);
            _context.SaveChanges();
        }
    }
}
