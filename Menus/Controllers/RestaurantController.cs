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
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public RestaurantController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        // GET: api/Restaurant
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new ObjectResult(await _restaurantRepository.GetAllRestaurants());
        }

        // POST: api/Restaurant
        [HttpPost]
        public async Task<IActionResult> Post( [FromBody]Restaurant restaurant)
        {
            await _restaurantRepository.Create(restaurant);
            return new OkObjectResult(restaurant);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            await _restaurantRepository.Delete(name);
            return new OkResult();
        }
    }
}
