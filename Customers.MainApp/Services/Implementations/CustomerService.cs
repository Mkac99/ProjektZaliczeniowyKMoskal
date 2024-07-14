using Customers.CommonModels;
using Customers.Infrastructure;
using Customers.MainApp.Data;
using Customers.MainApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Customers.MainApp.Repositories;

namespace Customers.MainApp.Services.Implementations
{
    public class CustomerService : BaseService, ICustomerService
    {
        const string Error404_CouldNotFindCustomer = "Could not find customer(ID {0})";
        const string Error500_ExceptionThrownWhileSendingEvent = "Exception thrown while sending event to the Tracking app. Details:\n";
        const string Error500_FailedToSendEvent = "Failed to send event to the Tracking app. Details:\n";

        private readonly ICustomerRepository _customerRepository;
        private readonly ITrackingLogsService _trackingLogsService;

        private bool _initialized;
        private string _applicationUrl;
        private string _userIpAddress;

        public CustomerService(ICustomerRepository customerRepository, ITrackingLogsService trackingLogsService)
        {
            _customerRepository = customerRepository;
            _trackingLogsService = trackingLogsService;

        }

        public void EnsureInitialized(string applicationUrl, string userIpAddress)
        {
            if (_initialized) return;

            _applicationUrl = applicationUrl;
            _userIpAddress = userIpAddress;
            _initialized = true;
        }

        public ServiceResponse<List<Customer>> GetCustomerList()
        {
            var response = TryExecute(() =>
            {
                var customersList = _customerRepository.GetCustomers();
                return ServiceResponse<List<Customer>>.Success(customersList);
            });
            if (!response.IsSuccess)
                return response;

            var sendEventResponse = SendEvent(TrackingLogEventType.CustomerListVisited);
            if (!sendEventResponse.IsSuccess)
                return sendEventResponse.AsGenericResponse<List<Customer>>();

            return response;
        }

        public ServiceResponse<Customer> GetCustomer(int id)
        {
            return TryExecute(() =>
            {
                var customer = _customerRepository.GetCustomerById(id);
                if (customer == null)
                {
                    return ServiceResponse<Customer>.Error(new ErrorDetails(404, string.Format(Error404_CouldNotFindCustomer, id)));
                }

                return ServiceResponse<Customer>.Success(customer);
            });
        }

        public ServiceResponse<Customer> AddCustomer(Customer customer)
        {
            var response = TryExecute(() =>
            {
                var newCustomer = _customerRepository.InsertCustomer(customer);
                
                return ServiceResponse<Customer>.Success(newCustomer);
            });
            if (!response.IsSuccess)
                return response;

            var sendEventResponse = SendEvent(TrackingLogEventType.CustomerAdded, response.ResponseDTO.Id, true);
            if (!sendEventResponse.IsSuccess)
                return sendEventResponse.AsGenericResponse<Customer>();

            return response;
        }

        public ServiceResponse<Customer> UpdateCustomer(Customer customer)
        {
            var response = TryExecute(() =>
            {
                var updatedCustomer = _customerRepository.UpdateCustomer(customer);
                return ServiceResponse<Customer>.Success(updatedCustomer);

            });
            if (!response.IsSuccess)
                return response;

            var sendEventResponse = SendEvent(TrackingLogEventType.CustomerUpdated, response.ResponseDTO.Id, true);
            if (!sendEventResponse.IsSuccess)
                return sendEventResponse.AsGenericResponse<Customer>();

            return response;
        }

        public ServiceResponse DeleteCustomer(int id)
        {
            var response = TryExecute(() =>
            {
                var customer = _customerRepository.GetCustomerById(id);
                if (customer == null)
                {
                    return ServiceResponse.Error(new ErrorDetails(404, string.Format(Error404_CouldNotFindCustomer, id)));
                }

                _customerRepository.DeleteCustomer(customer);
                return ServiceResponse.Success();
            });
            if (!response.IsSuccess)
                return response;

            var sendEventResponse = SendEvent(TrackingLogEventType.CustomerDeleted, id);
            if (!sendEventResponse.IsSuccess)
                return sendEventResponse;

            return response;
        }

        private ServiceResponse SendEvent(TrackingLogEventType type, int? customerId = null, bool includeUrl = false)
        {
            try
            {
                var trackingLogEvent = new TrackingLogEvent
                {
                    EventDate = DateTime.UtcNow,
                    UserIPAddress = _userIpAddress,
                    EventTypeId = type,
                    UpdatePageUrl = includeUrl ? $"{_applicationUrl}/Customer/Update/{customerId}" : null,
                    CustomerId = customerId,
                };
                var response = _trackingLogsService.SendEvent(trackingLogEvent);
                if (!response.IsSuccess)
                    return ServiceResponse.Error(new ErrorDetails(500, $"{Error500_FailedToSendEvent}{response.ErrorDetails}"));
            }
            catch (Exception ex)
            {
                return ServiceResponse.Error(new ErrorDetails(500, $"{Error500_ExceptionThrownWhileSendingEvent}{ex}"));
            }
            return ServiceResponse.Success();
        }
    }
}
