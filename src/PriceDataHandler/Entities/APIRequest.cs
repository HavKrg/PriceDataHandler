namespace PriceDataHandler.Entities;
class APIRequest
{
    public DailyPrices DailyPrices { get; set; }
    public string BaseURI { get; set; }
    public string  AreaId { get; set; }
    public string  Date { get; set; }

    public APIRequest(DailyPrices dailyPrices, string baseURI, string areaId, string date)
    {
        DailyPrices = dailyPrices;
        BaseURI = baseURI;
        AreaId =areaId;
        Date = date;
    }
}