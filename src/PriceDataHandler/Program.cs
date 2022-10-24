namespace PriceDataHandler;
class Program
{
    static async Task Main(string[] args)
    {

        var configLocation = args[0];
        System.Console.WriteLine("Got arguments");
        // var configLocation = "c:/Pricedata/config.json";
        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        try
        {
            var myConf = JsonSerializer.Deserialize<JsonConfiguration>(new StreamReader(configLocation).ReadToEnd(), jsonOptions);

            var jsonPrices = HelperFunctions.FormatPriceData(myConf.PriceFile);

            var priceData = JsonSerializer.Deserialize<List<PriceItemBuffer>>(jsonPrices, jsonOptions);

            var dailyPrices = HelperFunctions.CreateDailyPrices(priceData);

            var apiRequest = new APIRequest(dailyPrices, myConf.APIbaseURI, myConf.APIareaId);

            await File.WriteAllTextAsync($"{myConf.PriceHistoryLocation}{apiRequest.Date}.json", jsonPrices);

            await HelperFunctions.PostAsync(new HttpClient(), apiRequest);

        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        System.Console.WriteLine();
    }


}





