using Customers.Infrastructure;
using Customers.MainApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Customers.MainApp.Services
{
    public interface IHttpService
    { 
        ServiceResponse<HttpResponseMessage> Get(string url); 
        ServiceResponse<HttpResponseMessage> Post(string url, HttpContent content);
    }
}
