namespace PriceDataHandler;
class Program
{
    static async Task Main(string[] args)
    {
        var configLocation = args[0];
        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        System.Console.WriteLine(new StreamReader(configLocation).ReadToEnd());
        var myConf = JsonSerializer.Deserialize<JsonConfiguration>(new StreamReader(configLocation).ReadToEnd(), jsonOptions);
        
        var priceFile = $"{myConf.PriceFile}";
        var jsonFile = $"{myConf.JsonPriceFileNameFormat}";
        

        try
        {
            var jsonPrices = HelperFunctions.FormatPriceData(priceFile);

            await File.WriteAllTextAsync(jsonFile, jsonPrices);

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





