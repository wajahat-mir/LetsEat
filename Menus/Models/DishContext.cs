using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Menus.Models
{
    public class DishContext : IDishContext
    {
        private readonly IMongoDatabase _db;
        
        public DishContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
            
        }
        public IMongoDatabase Dishes => _db;
    }

    public interface IDishContext
    {
        IMongoDatabase Dishes { get; }
    }
}
