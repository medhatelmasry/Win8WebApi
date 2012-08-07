using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.WebApi.Website.Models.Database;

namespace Northwind.WebApi.Website.Controllers
{
    public class ProductController : ApiController
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET api/<controller>
        public IEnumerable<Product> Get()
        {
            return ctx.Products.ToList();
        }

        public Product GetById(int id)
        {
            var product = ctx.Products.FirstOrDefault((c) => c.ProductID == id);

            if (product == null)
            {
                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                throw new HttpResponseException(resp);
            }
            return product;
        }

        public IEnumerable<Product> GetByName(string name)
        {
            return ctx.Products
                .Where(c => c.ProductName.Contains(name))
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