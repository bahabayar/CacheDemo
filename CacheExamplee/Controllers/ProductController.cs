using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CacheExamplee.EntityContext;
using System.Runtime.Caching;


namespace CacheExamplee.Controllers
{
    public class ProductController : Controller
    {
      

        // GET: Product
        
        public ActionResult List()
        {

            var cache = MemoryCache.Default;
            var policy =  new CacheItemPolicy().AbsoluteExpiration=DateTime.Now.AddMinutes(1);


            List<Product> productList = new List<Product>();
            productList = (List<Product>) cache.Get("productList");
            if (productList == null)
            {
                using (var context = new NorthwindEntities())
                {
                    productList = context.Products.ToList();
                    cache.Set("productList", productList,policy);
                }
            }
            else
            {

            }

            return View(productList);
        }

        
    }
}