namespace CovidChart.API.Models
{
    public class CovidsChart
    {
        public CovidsChart()
        {
            Counts=new List<int>();
        }
        public string CovidDate { get; set; }
        public List<int> Counts { get; set; }
    }
}
