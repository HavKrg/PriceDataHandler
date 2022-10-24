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
            var jsonPrices = FormatPriceData(priceFile);

            await File.WriteAllTextAsync(jsonFile, jsonPrices);

            var priceData = JsonSerializer.Deserialize<List<PriceItemBuffer>>(jsonPrices, jsonOptions);

            var dailyPrices = CreateDailyPrices(priceData);

            await PostAsync(new HttpClient(), new APIRequest(dailyPrices, StaticValues.baseURI,
            StaticValues.areaId, StaticValues.date));

        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        System.Console.WriteLine();
    }

    public static string FormatPriceData(string fileName)
    {
        string pricedata = new StreamReader(fileName).ReadToEnd();

        pricedata = pricedata.Replace("}", "},");
        pricedata = pricedata.Remove(pricedata.LastIndexOf(","));
        pricedata = pricedata.Insert(0, "[\n");
        pricedata = pricedata.Insert(pricedata.Length, "\n]");

        return pricedata;
    }

    public static DailyPrices CreateDailyPrices(List<PriceItemBuffer> priceData)
    {
        var dailyPrices = new DailyPrices();

        foreach (var item in priceData)
        {
            var priceItem = new PriceItem(item.Period, item.Price);
            dailyPrices.Prices.Add(priceItem);
        }

        return dailyPrices;
    }

    static async Task PostAsync(HttpClient httpClient, APIRequest apiRequest)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(apiRequest.DailyPrices),
            Encoding.UTF8,
            "application/json");

        try
        {
            var response = await httpClient.PostAsync($"{apiRequest.BaseURI}?areaId={apiRequest.AreaId}&date={apiRequest.Date}",
            jsonContent);

            response.EnsureSuccessStatusCode();

            if (response.StatusCode.ToString() == "OK")
                System.Console.WriteLine($"{DateTime.Now} : Prices for {apiRequest.Date} were successfully sent to API");
        }
        catch (System.Exception ex)
        {

            System.Console.WriteLine(ex.Message);
        }
    }
}





