using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NPaperless.Services.DTOs;

namespace NPaperless.Services.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { 
        
        }

        public DbSet<CreateStoragePathRequest> StoragePaths { get; set; }
    }

   
}
