using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.WebApi.Website.Models.Database;

namespace Northwind.WebApi.Website.Controllers
{
    public class CustomerController : ApiController
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET api/<controller>
        public IEnumerable<Customer> Get()
        {
            return ctx.Customers.ToList();
        }

        public Customer GetById(string id)
        {
            var customer = ctx.Customers.FirstOrDefault((c) => c.CustomerID == id);

            if (customer == null)
            {
                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                throw new HttpResponseException(resp);
            }
            return customer;
        }

        public IEnumerable<Customer> GetByCompanyName(string name)
        {
            return ctx.Customers
                .Where(c => c.CompanyName.Contains(name))
                .Select(c => c);
        }

        // POST api/<controller>
        public void Post(string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}