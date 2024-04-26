using Consumer.Infrastruture;
using MongoDB.Driver;
using System.Linq;

namespace Consumer.Services;

public class OverSpeedingService
{
    private readonly IMongoCollection<OverSpeedingModel> _booksCollection;

    public OverSpeedingService(IConfiguration configuration)
    {
        var mongoClient = new MongoClient(configuration["VehiclesDatabase:ConnectionString"]);

        var mongoDatabase = mongoClient.GetDatabase(configuration["VehiclesDatabase:DatabaseName"]);

        _booksCollection = mongoDatabase.GetCollection<OverSpeedingModel>
        (configuration["VehiclesDatabase:CollectionNameOver"]);
    }

    public async Task<List<OverSpeedingModel>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task CreateAsync(OverSpeedingModel model) =>
        await _booksCollection.InsertOneAsync(model);
}