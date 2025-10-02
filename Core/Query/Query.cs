using System.Text.Json;

namespace Core.Query;

// class for dealing with reading in and parsing json
public class Query
{
    // method for reading in json and returning a list of OrderDetails objects
    public static List<OrderDetails> ReadAndParseJsonIntoList(string jsonFilePath)
    {
        // define json 
        string json = File.ReadAllText(jsonFilePath);    
        
        // deserialize json into List
        List<OrderDetails>? orders = JsonSerializer.Deserialize<List<OrderDetails>>(json);

        return orders!;
    }
}