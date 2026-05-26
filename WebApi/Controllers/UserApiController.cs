using Amazon.AWSSupport.Model;
using Amazon.SimpleDB.Model;
using Amazon.SimpleEmail.Model;
using BCrypt.Net;
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
    public class UserApiController : ControllerBase
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        private Populator populator = new Populator();
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetail>>> Get()
        {
            eCommerceEntities _context = new eCommerceEntities();

            var result = await Task.FromResult(new UserViewModelLst
            {
                dbModelLst = _unitOfWork.GetRepositoryInstance<UserDetail>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList()
            });

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
        public ActionResult<UserDetail> Create(UserDetail item)
        {
            item.Password = BCrypt.Net.BCrypt.HashPassword(item.Password);
            var exists = _unitOfWork
        .GetRepositoryInstance<UserDetail>()
        .GetAllRecords()
        .Any(x => x.FullName == item.FullName);
            if (exists)
            {
                return BadRequest("User name already exists");
            }

            if (item.FullName.Length < 3)
            {
                return BadRequest();
            }


            item.IsActive = true;
            item.IsDelete = false;
            _unitOfWork.GetRepositoryInstance<UserDetail>().Add(item);
            return Ok(item);
        }


        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, UserDetail todoItem)
        {
            eCommerceEntities _context = new eCommerceEntities();
            var exists = _unitOfWork
        .GetRepositoryInstance<UserDetail>()
        .GetAllRecords()
        .Any(x =>
        x.FullName == todoItem.FullName &&
        x.UserId != todoItem.UserId);
            if (exists)
            {
                return BadRequest("User name already exists");
            }
            if (id != todoItem.UserId)
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
            var todo = _context.User.Find(id);

            if (todo == null)
            {
                return NotFound();
            }
            _context.User.Remove(todo);
            _context.SaveChanges();
            //return NoContent();
            return Ok(todo);
        }

        // GET: api/ProductItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetail>> GetProduct(int id)
        {
            var productItems = _unitOfWork.GetRepositoryInstance<UserDetail>().GetFirstorDefault(id);
            if (productItems == null)
            {
                return NotFound();
            }
            return productItems;
        }

        private bool TodoItemExists(int id)
        {
            eCommerceEntities _context = new eCommerceEntities();

            return _context.User.Any(e => e.UserId == id);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel model)
        {
            eCommerceEntities _context = new eCommerceEntities();

            var user = _context.User
                .FirstOrDefault(x => x.Email == model.Email);

            if (user == null)
            {
                return BadRequest("Invalid email");
            }

            bool isValid =
                BCrypt.Net.BCrypt.Verify(
                    model.Password,
                    user.Password
                );

            if (!isValid)
            {
                return BadRequest("Invalid password");
            }

            return Ok(new
            {
                user.UserId,
                user.FullName,
                user.Email
            });
        }

    }
}