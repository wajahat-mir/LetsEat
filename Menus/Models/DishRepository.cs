using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Menus.Models
{
    public class DishRepository : IDishRepository
    {
        private readonly IDishContext _context;
        public DishRepository(IDishContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Dish>> GetAllDishes(string Collection)
        {
            return await _context
            .Dishes.GetCollection<Dish>(Collection)
            .Find(_ => true)
            .ToListAsync();
        }
        public Task<Dish> GetDish(string Collection, string name)
        {
            FilterDefinition<Dish> filter = Builders<Dish>.Filter.Eq(m => m.Name, name);
            return _context
            .Dishes.GetCollection<Dish>(Collection)
            .Find(filter)
            .FirstOrDefaultAsync();
        }

        public async Task<List<Dish>> GetDishByCategory(string Collection, string category)
        {
            FilterDefinition<Dish> filter = Builders<Dish>.Filter.Eq(m => m.Category, category);
            return await _context
            .Dishes.GetCollection<Dish>(Collection)
            .Find(filter)
            .ToListAsync();
        }
        
        public List<string> GetListOfCategories(string Collection)
        {
            return _context
            .Dishes.GetCollection<Dish>(Collection)
            .AsQueryable()
            .Select(e => e.Category)
            .Distinct()
            .ToList();
        }

        public async Task Create(string Collection, Dish dish)
        {
            await _context.Dishes.GetCollection<Dish>(Collection).InsertOneAsync(dish);
        }

        public async Task<bool> Update(string Collection, Dish dish)
        {
            ReplaceOneResult updateResult =
            await _context
            .Dishes.GetCollection<Dish>(Collection)
            .ReplaceOneAsync(
            filter: g => g.Id == dish.Id,
            replacement: dish);
            return updateResult.IsAcknowledged
            && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(string Collection, string name)
        {
            FilterDefinition<Dish> filter = Builders<Dish>.Filter.Eq(m => m.Name, name);
            DeleteResult deleteResult = await _context
            .Dishes.GetCollection<Dish>(Collection)
            .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;
        }
    }

    public interface IDishRepository
    {
        Task<IEnumerable<Dish>> GetAllDishes(string Collection);
        Task<Dish> GetDish(string Collection,  string name);
        Task<List<Dish>> GetDishByCategory(string Collection,  string category);
        List<string> GetListOfCategories(string Collection);
        Task Create(string Collection,  Dish dish);
        Task<bool> Update(string Collection, Dish dish);
        Task<bool> Delete(string Collection, string name);
    }
}
