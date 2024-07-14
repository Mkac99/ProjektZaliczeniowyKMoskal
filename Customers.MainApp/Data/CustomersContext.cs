using Customers.MainApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Customers.MainApp.Data
{
    public class CustomersContext : DbContext
    {
        public CustomersContext(DbContextOptions<CustomersContext> options)
            : base(options)
        {

        }

        public DbSet<Customer> Customer { get; set; }

    }
}