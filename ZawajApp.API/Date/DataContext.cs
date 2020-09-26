using Microsoft.EntityFrameworkCore;
using ZawajApp.API.Models;

namespace ZawajApp.API.Date
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Value> Values { get; set; }
    }
}