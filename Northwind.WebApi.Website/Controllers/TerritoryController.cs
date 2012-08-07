using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.WebApi.Website.Models.Database;

namespace Northwind.WebApi.Website.Controllers
{
    public class TerritoryController : ApiController
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET api/<controller>
        public IEnumerable<Territory> Get()
        {
            return ctx.Territories.ToList();
        }

        public Territory GetByTerritoryId(string id)
        {
            var territory = ctx.Territories.FirstOrDefault((c) => c.TerritoryID == id);

            if (territory == null)
            {
                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                throw new HttpResponseException(resp);
            }
            return territory;
        }

        public IEnumerable<Territory> GetByDescription(string name)
        {
            return ctx.Territories
                .Where(c => c.TerritoryDescription.Contains(name))
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