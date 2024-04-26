using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Consumer.Infrastruture;

public class OverSpeedingModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string? car_id { get; set; }
    public int speed { get; set; }
    public long timestamp { get; set; }
}