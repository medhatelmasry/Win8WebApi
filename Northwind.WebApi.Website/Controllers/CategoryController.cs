using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.WebApi.Website.Models.Database;

namespace Northwind.WebApi.Website.Controllers
{
    public class CategoryController : ApiController
    {
        public class CustomCategory
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
        }

        NorthwindEntities ctx = new NorthwindEntities();
        // GET api/<controller>
        public IEnumerable<CustomCategory> Get()
        {
            List<CustomCategory> simpleList = new List<CustomCategory>();

            foreach (var i in ctx.Categories)
            {
                simpleList.Add(new CustomCategory
                {
                    CategoryId = i.CategoryID,
                    CategoryName = i.CategoryName,
                    Description = i.Description
                });
            }

            return simpleList.ToList();
        }

        public Category GetById(int id)
        {
            var category = ctx.Categories.FirstOrDefault((c) => c.CategoryID == id);

            if (category == null)
            {
                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                throw new HttpResponseException(resp);
            }
            return category;
        }

        public IEnumerable<Category> GetByName(string name)
        {
            return ctx.Categories
                .Where(c => c.CategoryName.Contains(name))
                .Select(c => c);
        }

        // POST /api/people
        public void Post(Category value) //INSERT
        {
            Category newCat = new Category();
            newCat.CategoryName = value.CategoryName;
            newCat.Description = value.Description;

            ctx.AddToCategories(newCat);
            ctx.SaveChanges();
        }

        // PUT /api/people/5
        /* for this to work, put this XML in the <system.webServer> section of the web.config file
        <modules runAllManagedModulesForAllRequests="true" />
         */

        public void Put(int id, Category value) //UPDATE
        {
            var item = ctx.Categories.SingleOrDefault(p => p.CategoryID == id);
            if (item != null)
            {
                item.CategoryName = value.CategoryName;
                item.Description = value.Description;

                ctx.SaveChanges();
            }
        }

        // DELETE /api/people/5
        /* for this to work, put this XML in the <system.webServer> section of the web.config file
        <modules runAllManagedModulesForAllRequests="true" />
         */
        public void Delete(int id)
        {
            var item = ctx.Categories.SingleOrDefault(p => p.CategoryID == id);
            if (item != null)
            {
                ctx.DeleteObject(item);
                ctx.SaveChanges();
            }
        }
    }
}