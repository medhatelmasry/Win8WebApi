using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.WebApi.Website.Models.Database;

namespace Northwind.WebApi.Website.Controllers
{
    public class SupplierController : ApiController
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET api/<controller>
        public IEnumerable<Supplier> Get()
        {
            return ctx.Suppliers.ToList();
        }

        public Supplier GetById(int id)
        {
            var supplier = ctx.Suppliers.FirstOrDefault((c) => c.SupplierID == id);

            if (supplier == null)
            {
                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                throw new HttpResponseException(resp);
            }
            return supplier;
        }

        public IEnumerable<Supplier> GetByCompanyName(string name)
        {
            return ctx.Suppliers
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