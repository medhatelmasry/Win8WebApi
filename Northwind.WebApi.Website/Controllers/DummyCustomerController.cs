using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Northwind.WebApi.Website.Models;

namespace Northwind.WebApi.Website.Controllers
{
    public class DummyCustomerController : ApiController
    {
        private static List<DummyCustomer> customers =
                    new List<DummyCustomer>
            {
                new DummyCustomer() { Id = 1, Name="George Washington", Email = "george.washington@whitehouse.gov", Phone="2025550001" },
                new DummyCustomer() { Id = 2, Name="John Adams", Email = "john.adams@whitehouse.gov", Phone="2025550002" },
                new DummyCustomer() { Id = 3, Name="Thomas Jefferson", Email = "thomas.jefferson@whitehouse.gov", Phone="2025550003" },
                new DummyCustomer() { Id = 4, Name="James Madison", Email = "james.madison@whitehouse.gov", Phone="2025550004" },
                new DummyCustomer() { Id = 5, Name="James Monroe", Email = "james.monroe@whitehouse.gov", Phone="2025550005" }
            };

        public IEnumerable<DummyCustomer> Get()
        {
            return customers;
        }

        public DummyCustomer Get(int id)
        {
            return customers.Where(c => c.Id == id).FirstOrDefault();
        }

        public HttpResponseMessage PostReservation(DummyCustomer customer)
        {
            if (ModelState.IsValid)
            {
                var newId = customers.Max(c => c.Id);
                customer.Id = newId + 1;
                customers.Add(customer);
                //var responseMessage = new HttpResponseMessage<DummyCustomer>(customer, System.Net.HttpStatusCode.Created);
                HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.Created, customer);

                responseMessage.Headers.Location = new Uri(VirtualPathUtility.AppendTrailingSlash(Request.RequestUri.ToString()) + newId.ToString());
                return responseMessage;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }

        public void Put(DummyCustomer customer)
        {
            var cust = customers.Where(c => c.Id == customer.Id).FirstOrDefault();
            if (cust != null)
            {
                cust.Name = customer.Name;
                cust.Email = customer.Email;
                cust.Phone = customer.Phone;
            }
        }

        public void Delete(int id)
        {
            var customer = customers.Where(c => c.Id == id).FirstOrDefault();
            if (customer != null)
                customers.Remove(customer);
        }
    }
}