using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Menus.Models
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IRestaurantContext _context;

        public RestaurantRepository(IRestaurantContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetAllRestaurants()
        {
            return await _context.Restaurants.ListCollectionNames().ToListAsync();
        }

        public async Task Create(Restaurant restaurant)
        {
            await _context.Restaurants.CreateCollectionAsync(restaurant.name);
        }

        public async Task Delete(string name)
        {
            await _context.Restaurants.DropCollectionAsync(name);
        }
    }

    public interface IRestaurantRepository
    {
        Task<IEnumerable<string>> GetAllRestaurants();
        Task Create(Restaurant restaurant);
        Task Delete(string name);
    }
}
