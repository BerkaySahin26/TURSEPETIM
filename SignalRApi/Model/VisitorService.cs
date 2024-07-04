using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRApi.DAL;
using SignalRApi.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApi.Model
{
    public class VisitorService
    {
        private readonly Context _context;
        private readonly IHubContext<VisitorHub> _hubContext;
        public VisitorService(Context context, IHubContext<VisitorHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        // Ziyaretçi listesini sorgula
        public IQueryable<Visitor> GetList()
        {
            return _context.Visitors.AsQueryable();
        }
        // Yeni bir ziyaretçi kaydeden ve güncel ziyaretçi listesini tüm istemcilere ileten asenkron metot
        public async Task SaveVisitor(Visitor visitor)
        {
            await _context.Visitors.AddAsync(visitor);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("CallVisitorList",GetVisitorChartList());
        }
        public List<VisitorChart> GetVisitorChartList() // Ziyaretçi istatistikleri
        {
            List<VisitorChart> visitorCharts = new List<VisitorChart>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "Select * From crosstab ( 'Select VisitDate,City,CityVisitCount From Visitors Order By 1, 2') As ct(VisitDate date,City1 int, City2 int, City3 int, City4 int, City5 int);";
                command.CommandType = System.Data.CommandType.Text;
                _context.Database.OpenConnection();
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VisitorChart visitorChart = new VisitorChart(); // Yeni bir ziyaretçi istatistik nesnesi oluşturur
                        visitorChart.VisitDate = reader.GetDateTime(0).ToShortDateString(); // Ziyaret tarihini ayarlar
                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            visitorChart.Counts.Add(reader.GetInt32(x));
                        });
                        visitorCharts.Add(visitorChart); // Ziyaretçi istatistik nesnesini listeye ekledik
                    }
                }
                _context.Database.CloseConnection(); // Bağlantıyı kapattık
                return visitorCharts;
            }
        }
    }
}
