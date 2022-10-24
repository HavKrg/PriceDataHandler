namespace PriceDataHandler.Entities;

class PriceItem
{
    public int Hour { get; set; }
    public double Value { get; set; }
    public PriceItem(string hour, double price)
    {
        Hour = int.Parse(hour);
        Value = price;
    }
}