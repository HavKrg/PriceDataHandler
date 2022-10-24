namespace PriceDataHandler;
class Program
{
    static async Task Main(string[] args)
    {

        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var priceFile = $"{StaticValues.baseLocation}\\{args[0]}";
        var jsonFile = $"{StaticValues.baseLocation}\\prices\\{StaticValues.storedJsonPricesFileName}";

        try
        {
            var jsonPrices = HelperFunctions.FormatPriceData(priceFile);

            await File.WriteAllTextAsync(jsonFile, jsonPrices);

            var priceData = JsonSerializer.Deserialize<List<PriceItemBuffer>>(jsonPrices, jsonOptions);

            var dailyPrices = HelperFunctions.CreateDailyPrices(priceData);

            await HelperFunctions.PostAsync(new HttpClient(), new APIRequest(dailyPrices, StaticValues.baseURI,
            StaticValues.areaId, StaticValues.date));

        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        System.Console.WriteLine();
    }


}





