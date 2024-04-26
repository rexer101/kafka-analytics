using Consumer.Infrastruture;
using Microsoft.VisualBasic;
using MongoDB.Driver;

namespace Consumer.Services;

public class RawDataService
{
    private readonly IMongoCollection<RawModel> _Collection;

    public RawDataService(IConfiguration configuration)
    {
        var mongoClient = new MongoClient(configuration["VehiclesDatabase:ConnectionString"]);

        var mongoDatabase = mongoClient.GetDatabase(configuration["VehiclesDatabase:DatabaseName"]);

        _Collection = mongoDatabase.GetCollection<RawModel>
        (configuration["VehiclesDatabase:CollectionNameRaw"]);
    }

    public async Task<int> GetAsync()
    {
        var result = await _Collection.Find(_ => true).ToListAsync();
        return result.Count;
    }
    
    public async Task CreateAsync(RawModel model) =>
        await _Collection.InsertOneAsync(model);
}