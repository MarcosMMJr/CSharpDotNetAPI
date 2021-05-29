using Microsoft.EntityFrameworkCore;
using vaivoa.Models;

namespace vaivoa.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options)
      : base(options)
    {
    }

    public DbSet<CardInfo> CardInfos { get; set; }
    public DbSet<AccountInfo> AccountInfos { get; set; }
  }
}