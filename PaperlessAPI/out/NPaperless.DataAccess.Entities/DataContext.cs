using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NPaperless.Services.Models;

namespace NPaperless.Services.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { 
        
        }

        public DataContext()
        {

        }
        virtual public DbSet<Correspondent> Correspondents { get; set; }
        virtual public DbSet<Document> Documents { get; set; }
     

    }

   
}
