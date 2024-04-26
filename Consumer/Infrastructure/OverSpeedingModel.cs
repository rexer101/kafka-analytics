using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Consumer.Infrastruture;

public class OverSpeedingModel
{
    public string? car_id { get; set; }
    public int speed { get; set; }
    public string? timestamp { get; set; }
}