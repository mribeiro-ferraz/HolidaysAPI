using Microsoft.EntityFrameworkCore;

namespace HolidaysApi.Models
{
    public class HolidaysContext : DbContext
    {
        public HolidaysContext(DbContextOptions<HolidaysContext> options)
            : base(options)
        {
        }
        public DbSet<Holidays> Holidays { get; set; }
    }
}
