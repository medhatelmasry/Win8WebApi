using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.WebApi.Website.Models.Database;

namespace Northwind.WebApi.Website.Controllers
{
    public class OrderController : ApiController
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET api/<controller>
        public IEnumerable<Order> Get()
        {
            return ctx.Orders.ToList();
        }

        public Order GetById(int id)
        {
            var ord = ctx.Orders.FirstOrDefault((c) => c.OrderID == id);

            if (ord == null)
            {
                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                throw new HttpResponseException(resp);
            }
            return ord;
        }

        public IEnumerable<Order> GetByCompanyName(string name)
        {
            return ctx.Orders
                .Where(c => c.Customer.CompanyName.Contains(name))
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