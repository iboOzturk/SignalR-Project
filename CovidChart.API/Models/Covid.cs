namespace CovidChart.API.Models
{
    public enum ECity
    {
        Istanbul=1,
        Ankara=2,
        Bursa=3,
        Izmir=4,
        Konya=5,
        Antalya=6
    }
    public class Covid
    {
        public int Id { get; set; }
        public ECity City { get; set; }
        public int Count { get; set; }
        public DateTime CovidDate { get; set; } 
    }
}
