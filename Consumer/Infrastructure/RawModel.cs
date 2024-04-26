using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Consumer.Infrastruture;

public class RawModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string? car_id { get; set; }
    public string? display_name { get; set; }
    public string? latitude { get; set; }
    public string? longitude { get; set; }
    public int power { get; set; }
    public int speed { get; set; }
    public int heading { get; set; }
    public int elevation { get; set; }
    public bool windows_open { get; set; }
    public bool is_user_present { get; set; }
    public bool is_climate_on { get; set; }
    public double inside_temp { get; set; }
    public double outside_temp { get; set; }
    public int odometer { get; set; }
    public int battery_level { get; set; }
    public long timestamp { get; set; }
}