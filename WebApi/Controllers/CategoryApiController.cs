using Amazon.AWSSupport.Model;
using Microsoft.AspNetCore.Authorization;
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
using Microsoft.OpenApi.Models;

namespace WebApi.Controllers
{

    [Authorize]

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        private Populator populator = new Populator();
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDetail>>> Get()
        {

            var result = await Task.FromResult(new CategoryViewModelLst
            {
                dbModelLst = _unitOfWork.GetRepositoryInstance<CategoryDetail>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList()
            }) ;
            return result.dbModelLst.ToList();

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CategoryDetail> Create(
    CategoryDetail item
)
        {
            var exists = _unitOfWork
                .GetRepositoryInstance<CategoryDetail>()
                .GetAllRecords()
                .Any(x =>
                    x.CategoryName.ToLower()
                    == item.CategoryName.ToLower()
                    && x.IsDelete == false
                );

            if (exists)
            {
                return BadRequest(
                    "Category name already exists"
                );
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            item.IsActive = true;
            item.IsDelete = false;

            _unitOfWork
                .GetRepositoryInstance<CategoryDetail>()
                .Add(item);

            return Ok(item);
        }


        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, CategoryDetail todoItem)
        {
            eCommerceEntities _context = new eCommerceEntities();
            var exists = _unitOfWork
        .GetRepositoryInstance<CategoryDetail>()
        .GetAllRecords()
        .Any(x =>
        x.CategoryName == todoItem.CategoryName &&
        x.CategoryId != todoItem.CategoryId);
            if (exists)
            {
                return BadRequest("Category name already exists");
            }
            if (id != todoItem.CategoryId)
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
            var todo = _context.Category.Find(id);

            if (todo == null)
            {
                return NotFound();
            }
            _context.Category.Remove(todo);
            _context.SaveChanges();
            //return NoContent();
            return Ok(todo);
        }

        // GET: api/ProductItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDetail>> GetProduct(int id)
        {
            var productItems = _unitOfWork.GetRepositoryInstance<CategoryDetail>().GetFirstorDefault(id);
            if (productItems == null)
            {
                return NotFound();
            }
            return productItems;
        }

        private bool TodoItemExists(int id)
        {
            eCommerceEntities _context = new eCommerceEntities();

            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}