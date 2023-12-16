using CovidChart.API.Hubs;
using CovidChart.API.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CovidChart.API.Services
{
    public class CovidService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<CovidHub> _hubContext;

        public CovidService(AppDbContext context, IHubContext<CovidHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IQueryable<Covid> GetList()
        {
            return _context.Covids.AsQueryable();
        }
        public async Task SaveCovid(Covid covid)
        {
            await _context.Covids.AddAsync(covid);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveCovidList",
                GetCovidChartList());
        }

        public List<CovidsChart> GetCovidChartList()
        {
            List<CovidsChart> covidCharts = new List<CovidsChart>();
            using (var command=_context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select tarih,[1],[2],[3],[4],[5],[6] from (select [City],[Count],Cast([CovidDate] as date) as tarih from Covids) as covidT pivot (Sum(count) for City in([1],[2],[3],[4],[5],[6])) as PTable order by tarih asc";
                command.CommandType=System.Data.CommandType.Text;
                _context.Database.OpenConnection();
                using (var reader=command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CovidsChart cc=new CovidsChart();
                        cc.CovidDate=reader.GetDateTime(0).ToShortDateString();
                        Enumerable.Range(1, 6).ToList().ForEach(x =>
                        {
                            if (System.DBNull.Value.Equals(reader[x]))
                            {
                                cc.Counts.Add(0);
                            }
                            else
                            {
                                cc.Counts.Add(reader.GetInt32(x));
                            }
                        });
                        covidCharts.Add(cc);
                    }
                }

                _context.Database.CloseConnection();
                return covidCharts;
            }

        }

    }
}
