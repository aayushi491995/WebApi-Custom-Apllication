using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssignmentWebApi.Models;

namespace AssignmentWebApi.Controllers
{
    public class ProductController : ApiController
    {
        ProductEntities db = new ProductEntities();
        //GET
        public IHttpActionResult GetAllProducts()
        {
            
            IList<tblProducts> prod = null;
            prod = db.tblProducts.ToList<tblProducts>();
            if (prod.Count == 0)
            {
                return NotFound();
            }
            return Ok(prod);
        }

        //GET/ID
        public IHttpActionResult GetAllProductById(int id)
        {
            tblProducts prod = null;
            prod = (from pd in db.tblProducts
                    where pd.id == id
                    select pd).FirstOrDefault<tblProducts>();

            if (prod == null)
            {
                return NotFound();
            }
            return Ok(prod);
        }

        //POST
        [HttpPost]
        public IHttpActionResult PostProduct(tblProducts prd)
        {
            if (!ModelState.IsValid)
            { return BadRequest("Invalid Data"); }

            using (var data = new ProductEntities())
            {
                data.tblProducts.Add(new tblProducts()
                {
                    pname = prd.pname,
                    price = prd.price,
                    pdesc = prd.pdesc
                });

                data.SaveChanges();
            }

            return Ok();
        }

        //PUT
        [HttpPut]
        public IHttpActionResult UpdateProduct(tblProducts prd)
        {
            if (!ModelState.IsValid)
            { return BadRequest("Invalid Data"); }

            var existingProduct = (from pd in db.tblProducts
                                   where pd.id == prd.id
                                   select pd).FirstOrDefault<tblProducts>();

            if (existingProduct != null)
            {
                existingProduct.pname = prd.pname;
                existingProduct.price = prd.price;
                existingProduct.pdesc = prd.pdesc;                
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }


        //DELETE
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            

            var existingProduct = (from pd in db.tblProducts
                                   where pd.id == id
                                   select pd).FirstOrDefault();            

            if (existingProduct != null)
            {
                db.Entry(existingProduct).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
