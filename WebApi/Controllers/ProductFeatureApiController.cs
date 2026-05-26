using Amazon.AWSSupport.Model;
using Amazon.SimpleDB.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopEdmx;
using OnlineShopEdmx.Model;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFeatureApiController : ControllerBase
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        private Populator populator = new Populator();
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductFeatureDetail>>> Get()
        {

            var result = await Task.FromResult(new ProductFeatureViewModelLst
            {
                dbModelLst = _unitOfWork.GetRepositoryInstance<ProductFeatureDetail>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList()
            }) ;
            return result.dbModelLst.ToList();

        }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ProductFeatureDetail> Create(ProductFeatureDetail item)
        {
            var exists = _unitOfWork
        .GetRepositoryInstance<ProductFeatureDetail>()
        .GetAllRecords()
        .Any(x => x.ProductFeatureName == item.ProductFeatureName);
            if (exists)
            {
                return BadRequest("ProductFeature name already exists");
            }

            if (item.ProductFeatureName.Length < 3)
            {
                return BadRequest();
            }


            item.IsActive = true;
            item.IsDelete = false;
            _unitOfWork.GetRepositoryInstance<ProductFeatureDetail>().Add(item);
            return Ok(item);
        }


        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, ProductFeatureDetail todoItem)
        {
            eCommerceEntities _context = new eCommerceEntities();
            var exists = _unitOfWork
        .GetRepositoryInstance<ProductFeatureDetail>()
        .GetAllRecords()
        .Any(x =>
        x.ProductFeatureName == todoItem.ProductFeatureName &&
        x.ProductFeatureId != todoItem.ProductFeatureId);
            if (exists)
            {
                return BadRequest("ProductFeature name already exists");
            }
            if (id != todoItem.ProductFeatureId)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

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
            var todo = _context.ProductFeature.Find(id);

            if (todo == null)
            {
                return NotFound();
            }
            _context.ProductFeature.Remove(todo);
            _context.SaveChanges();
            //return NoContent();
            return Ok(todo);
        }

        // GET: api/ProductItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductFeatureDetail>> GetProduct(int id)
        {
            var productItems = _unitOfWork.GetRepositoryInstance<ProductFeatureDetail>().GetFirstorDefault(id);
            if (productItems == null)
            {
                return NotFound();
            }
            return productItems;
        }

        private bool TodoItemExists(int id)
        {
            eCommerceEntities _context = new eCommerceEntities();

            return _context.ProductFeature.Any(e => e.ProductFeatureId == id);
        }
    }
}