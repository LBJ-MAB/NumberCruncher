using System.Text.Json;

namespace Core.Query;

// class for dealing with reading in and parsing json
public class Query
{
    // json serializer object properties
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    // method for reading in json and returning a list of OrderDetails objects
    public List<OrderDetails> ReadAndParseJsonIntoList(string jsonFilePath)
    {
        // define json 
        string json = File.ReadAllText(jsonFilePath);
        
        // deserialize json into List of OrderDetails() objects
        List<OrderDetails>? orders = JsonSerializer.Deserialize<List<OrderDetails>>(json, _options);

        return orders;
    }
    
    // method for filter
    
    // method for map
    
    // method for group
    
    // method for join
    
    // projections to DTOs
    
    // sorting
    
    // pagination
}