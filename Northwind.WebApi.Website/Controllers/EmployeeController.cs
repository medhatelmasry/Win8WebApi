using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.WebApi.Website.Models.Database;

namespace Northwind.WebApi.Website.Controllers
{
    public class EmployeeController : ApiController
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET api/<controller>
        public IEnumerable<Employee> Get()
        {
            return ctx.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            var emp = ctx.Employees.FirstOrDefault((c) => c.EmployeeID == id);

            if (emp == null)
            {
                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                throw new HttpResponseException(resp);
            }
            return emp;
        }

        public IEnumerable<Employee> GetByLastName(string name)
        {
            return ctx.Employees
                .Where(c => c.LastName.Contains(name))
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