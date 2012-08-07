using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.WebApi.Website.Models.Database;

namespace Northwind.WebApi.Website.Controllers
{
    public class RegionController : ApiController
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET api/<controller>
        public IEnumerable<Region> Get()
        {
            return ctx.Regions.ToList();
        }

        public Region GetById(int id)
        {
            var region = ctx.Regions.FirstOrDefault((c) => c.RegionID == id);

            if (region == null)
            {
                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                throw new HttpResponseException(resp);
            }
            return region;
        }

        public IEnumerable<Region> GetByDescription(string name)
        {
            return ctx.Regions
                .Where(c => c.RegionDescription.Contains(name))
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