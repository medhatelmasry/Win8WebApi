using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.WebApi.Website.Models.Database;

namespace Northwind.WebApi.Website.Controllers
{
    public class ShipperController : ApiController
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET api/<controller>
        public IEnumerable<Shipper> Get()
        {
            return ctx.Shippers.ToList();
        }

        public Shipper GetById(int id)
        {
            var cat = ctx.Shippers.FirstOrDefault((c) => c.ShipperID == id);

            if (cat == null)
            {
                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                throw new HttpResponseException(resp);
            }
            return cat;
        }

        public IEnumerable<Shipper> GetByCompanyName(string name)
        {
            return ctx.Shippers
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