using System.Text.Json;
using System.Text;
using PriceScript;



// var originalJson = args[0];
var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

var priceFile = "c:\\pricedata\\pricedata.txt";
var jsonFIle = $"c:\\pricedata\\prices\\{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.json";

if(File.Exists(priceFile))
{
    string requestBody = await new StreamReader(priceFile).ReadToEndAsync();
    
        requestBody = requestBody.Replace("}", "},");
        requestBody = requestBody.Remove(requestBody.LastIndexOf(","));
        requestBody = requestBody.Insert(0, "[\n");
        requestBody = requestBody.Insert(requestBody.Length, "\n]");
    
    var data = JsonSerializer.Deserialize<List<PriceScript.PriceItemBuffer>>(requestBody, options);

    System.Console.WriteLine(requestBody);
}



// var dailyPrices = new DailyPrices();

// foreach (var item in data)
// { 
//     var priceItem = new PriceItem(item.Period, item.Price);
//     dailyPrices.Prices.Add(priceItem);
// }

//  await PostAsync(new HttpClient(), dailyPrices);


// static async Task PostAsync(HttpClient httpClient, DailyPrices dailyPrices)
// {
//     using StringContent jsonContent = new(
//         JsonSerializer.Serialize(dailyPrices),
//         Encoding.UTF8,
//         "application/json");
    
//     var baseURI = "https://localhost:7020/api/energyprices/area/dailyprices";
//     var areaId = "?areaId=6213f4d6-9fd1-481a-aa9d-08dab5375b94";
//     var date = $"&date={DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day+1}";
//     using HttpResponseMessage response = await httpClient.PostAsync(
//         // "https://localhost:7020/api/energyprices/area/dailyprices?areaId=6213f4d6-9fd1-481a-aa9d-08dab5375b94&date=2022-07-31",
//         $"{baseURI}{areaId}{date}",
//         jsonContent);

//     response.EnsureSuccessStatusCode();
    
//     var jsonResponse = await response.Content.ReadAsStringAsync();
//     System.Console.WriteLine(jsonResponse);
// }

// System.Console.WriteLine(dailyPrices);

