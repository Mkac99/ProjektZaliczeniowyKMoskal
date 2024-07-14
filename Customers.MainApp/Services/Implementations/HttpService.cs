using Customers.CommonModels;
using Customers.Infrastructure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Customers.MainApp.Services.Implementations
{
    public class HttpService : BaseService, IHttpService
    {
        public ServiceResponse<HttpResponseMessage> Get(string url)
        {
            return TryExecute(() =>
            {
                using (var httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync(url).Result;
                    return ServiceResponse<HttpResponseMessage>.Success(response);
                }
            });
        }

        public ServiceResponse<HttpResponseMessage> Post(string url, HttpContent content)
        {
            return TryExecute(() =>
            {
                using (var httpClient = new HttpClient())
                {
                    var response = httpClient.PostAsync(url, content).Result;
                    return ServiceResponse<HttpResponseMessage>.Success(response);
                }
            });
        }
    }
}
