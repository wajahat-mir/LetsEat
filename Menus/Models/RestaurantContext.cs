using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Menus.Models
{
    public class RestaurantContext : IRestaurantContext
    {
        private readonly IMongoDatabase _db;

        public RestaurantContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);

        }
        public IMongoDatabase Restaurants => _db;
    }

    public interface IRestaurantContext
    {
        IMongoDatabase Restaurants { get; }
    }
}
