namespace PriceDataHandler.Entities;
class DailyPrices
{
    public List<PriceItem> Prices { get; set; }

    public DailyPrices()
    {
        Prices = new List<PriceItem>();
    }
}