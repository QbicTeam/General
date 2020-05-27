using Microsoft.EntityFrameworkCore;
using SIQbic.API.Model;

namespace SIQbic.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        {}

        public DbSet<User> Users { get; set; }

    }
}