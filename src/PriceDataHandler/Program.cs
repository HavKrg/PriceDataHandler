namespace PriceDataHandler;
class Program
{
    static async Task Main(string[] args)
    {

        // var configLocation = args[0];
        System.Console.WriteLine("Got arguments");
        var configLocation = "c:/Pricedata/config.json";
        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        try
        {
            var myConf = JsonSerializer.Deserialize<JsonConfiguration>(new StreamReader(configLocation).ReadToEnd(), jsonOptions);
            System.Console.WriteLine("parsed config");

            var jsonPrices = HelperFunctions.FormatPriceData(myConf.PriceFile);
            System.Console.WriteLine("parsed prices");

            

            var priceData = JsonSerializer.Deserialize<List<PriceItemBuffer>>(jsonPrices, jsonOptions);

            var dailyPrices = HelperFunctions.CreateDailyPrices(priceData);

            var apiRequest = new APIRequest(dailyPrices, myConf.APIbaseURI, myConf.APIareaId);

            await File.WriteAllTextAsync(apiRequest.Date, jsonPrices);
            System.Console.WriteLine("Wrote prices");

            await HelperFunctions.PostAsync(new HttpClient(), apiRequest);

        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        System.Console.WriteLine();
    }


}





