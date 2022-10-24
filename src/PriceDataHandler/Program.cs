namespace PriceDataHandler;
class Program
{
    static async Task Main(string[] args)
    {

        // var configLocation = args[0];
        var configLocation = "c:/Pricedata/config.json";
        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        try
        {
            var myConf = JsonSerializer.Deserialize<JsonConfiguration>(new StreamReader(configLocation).ReadToEnd(), jsonOptions);

            var jsonPrices = HelperFunctions.FormatPriceData(myConf.PriceFile);

            await File.WriteAllTextAsync(myConf.JsonPriceFileNameFormat, jsonPrices);

            var priceData = JsonSerializer.Deserialize<List<PriceItemBuffer>>(jsonPrices, jsonOptions);

            var dailyPrices = HelperFunctions.CreateDailyPrices(priceData);

            await HelperFunctions.PostAsync(new HttpClient(), new APIRequest(dailyPrices, myConf.APIbaseURI,
            myConf.APIareaId, myConf.APIdate));

        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        System.Console.WriteLine();
    }


}





