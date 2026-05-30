using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopEdmx;
using OnlineShopEdmx.Model;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace WebApi.Controllers
{
    [Authorize]

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        private Populator populator = new Populator();
        private readonly IWebHostEnvironment hostingEnvo;


        public ProductApiController(IWebHostEnvironment hostingEnvo)
        {

            this.hostingEnvo = hostingEnvo;
        }

        [HttpGet]
            public async Task<ActionResult> Get()

        {
            var products = from p in _unitOfWork
                   .GetRepositoryInstance<ProductDetail>()
                   .GetAllRecordsIQueryable()

                           join c in _unitOfWork
                               .GetRepositoryInstance<CategoryDetail>()
                               .GetAllRecordsIQueryable()

                           on p.CategoryId equals c.CategoryId

                           join pf in _unitOfWork
                               .GetRepositoryInstance<ProductFeatureDetail>()
                               .GetAllRecordsIQueryable()

                           on p.ProductFeatureId equals pf.ProductFeatureId

                           where p.IsDelete == false

                           select new
                           {
                               p.ProductId,
                               p.ProductName,
                               p.CategoryId,
                               CategoryName = c.CategoryName,
                               ProductFeature= pf.ProductFeatureName,
                               p.Quantity,
                               p.Price,
                               p.IsActive, 
                               p.ProductImage
                           };

            /*var result = await Task.FromResult(new ProductViewModelLst
            {
                dbModelLst = _unitOfWork.GetRepositoryInstance<products>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList()
            }) ;*/
             return Ok(products.ToList());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpPost]
        public ActionResult<ProductDetail> Create( [FromForm] ProductViewModel model)
            {
            ProductDetail item = new ProductDetail();
            item.ProductName = model.ProductName;
            item.CategoryId = model.CategoryId;
            item.Quantity = model.Quantity;
            item.Price = model.Price;
            item.Description = model.Description;
            item.IsActive = model.IsActive;
            item.ProductFeatureId = model.ProductFeatureId;
            item.CreatedDate = item.ModifiedDate = DateTime.Now;
            item.IsDelete = false;

            // image handling later
            string uniqueFileName = null;

            if (model.productImage != null)
            {

                string uploadsFolder =
                    Path.Combine(
                        hostingEnvo.WebRootPath,
                        "productImages"
                    );
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName =
                    Guid.NewGuid().ToString()
                    + "_"
                    + model.productImage.FileName;

                string filePath =
                    Path.Combine(
                        uploadsFolder,
                        uniqueFileName
                    );

                using (var stream = new FileStream(
                    filePath,
                    FileMode.Create))
                {
                    model.productImage.CopyTo(stream);
                }
            }
            item.ProductImage = uniqueFileName;
            _unitOfWork.GetRepositoryInstance<ProductDetail>().Add(item);

            return Ok(item);
        }
        
        
        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, [FromForm] ProductViewModel model)
        {
            eCommerceEntities _context = new eCommerceEntities();

            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.ProductName = model.ProductName;
            product.CategoryId = model.CategoryId;
            product.Quantity = model.Quantity;
            product.Price = model.Price;
            product.Description = model.Description;
            product.IsActive = model.IsActive;
            product.ProductFeatureId = model.ProductFeatureId;
            product.CreatedDate = product.ModifiedDate = DateTime.Now;
            product.IsDelete = false;

            // image handling later
            string uniqueFileName = null;

            if (model.productImage != null)
            {

                string uploadsFolder =
                    Path.Combine(
                        hostingEnvo.WebRootPath,
                        "productImages"
                    );
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName =
                    Guid.NewGuid().ToString()
                    + "_"
                    + model.productImage.FileName;

                string filePath =
                    Path.Combine(
                        uploadsFolder,
                        uniqueFileName
                    );

                using (var stream = new FileStream(
                    filePath,
                    FileMode.Create))
                {
                    model.productImage.CopyTo(stream);
                }
            }
            product.ProductImage = uniqueFileName;


            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            eCommerceEntities _context = new eCommerceEntities();
            var todo = _context.Product.Find(id);

            if (todo == null)
            {
                return NotFound();
            }

            _context.Product.Remove(todo);
            _context.SaveChanges();

            return NoContent();
        }


        // GET: api/ProductItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetail>> GetProduct(int id)
        {
            var productItems = _unitOfWork.GetRepositoryInstance<ProductDetail>().GetFirstorDefault(id);
            if (productItems == null)
            {
                return NotFound();
            }
            return productItems;
        }

        private bool TodoItemExists(int id)
        {
            eCommerceEntities _context = new eCommerceEntities();

            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}