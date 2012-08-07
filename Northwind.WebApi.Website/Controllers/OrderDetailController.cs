using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.WebApi.Website.Models.Database;

namespace Northwind.WebApi.Website.Controllers
{
    public class OrderDetailController : ApiController
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET api/<controller>
        public IEnumerable<Order_Detail> Get()
        {
            return ctx.Order_Details.ToList();
        }

        public Order_Detail GetByOrderId(int id)
        {
            var orderId = ctx.Order_Details.FirstOrDefault((c) => c.OrderID == id);

            if (orderId == null)
            {
                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                throw new HttpResponseException(resp);
            }
            return orderId;
        }

        public IEnumerable<Order_Detail> GetByProductName(string name)
        {
            return ctx.Order_Details
                .Where(c => c.Product.ProductName.Contains(name))
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