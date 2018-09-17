using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Menus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Menus.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
       private readonly IDishRepository _dishRepository;
       public DishController(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        // GET: api/Dish
        [HttpGet("{collection}")]
        public async Task<IActionResult> Get(string collection)
        {
            return new ObjectResult(await _dishRepository.GetAllDishes(collection));
        }

        // GET: api/Dish/name
        [HttpGet("{collection}/{name}", Name = "Get")]
        public async Task<IActionResult> Get(string collection, string name)
        {
            var dish = await _dishRepository.GetDish(collection, name);
            if (dish == null)
                return new NotFoundResult();
            return new ObjectResult(dish);
        }

        // GET: api/Dish/category
        [Route("{collection}/[action]/{category}")]
        [HttpGet]
        public async Task<IActionResult> GetByCategory(string collection, string category)
        {
            var dish = await _dishRepository.GetDishByCategory(collection, category);
            if (dish == null)
                return new NotFoundResult();
            return new ObjectResult(dish);
        }

        [Route("{collection}/[action]")]
        [HttpGet]
        public IActionResult GetListOfCategories(string collection)
        {
            return new ObjectResult(_dishRepository.GetListOfCategories(collection));
        }

        // POST: api/Dish
        [HttpPost("{collection}")]
        public async Task<IActionResult> Post(string collection, [FromBody]Dish dish)
        {
            await _dishRepository.Create(collection, dish);
            return new ObjectResult(dish);
        }
        // PUT: api/Dish/5
        [HttpPut("{collection}/{name}")]
        public async Task<IActionResult> Put(string collection, string name, [FromBody]Dish dish)
        {
            var gameFromDb = await _dishRepository.GetDish(collection, name);
            if (gameFromDb == null)
                return new NotFoundResult();
            dish.Id = gameFromDb.Id;
            await _dishRepository.Update(collection, dish);
            return new OkObjectResult(dish);
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{collection}/{name}")]
        public async Task<IActionResult> Delete(string collection, string name)
        {
            var dishFromDb = await _dishRepository.GetDish(collection, name);
            if (dishFromDb == null)
                return new NotFoundResult();
            await _dishRepository.Delete(collection, name);
            return new OkResult();
        }
    }
}
