namespace PriceDataHandler;

public class StaticValues
{
    static CultureInfo culture = new CultureInfo("nn-NO");
    public static string baseURI = "https://localhost:7020/api/energyprices/area/dailyprices";
    public static string areaId = "6213f4d6-9fd1-481a-aa9d-08dab5375b94";
    public static string date = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day + 1}";
    public static string storedJsonPricesFileName = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.json";
    public static string baseLocation = "/data";
}


