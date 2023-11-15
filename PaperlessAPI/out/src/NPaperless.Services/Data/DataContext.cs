using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NPaperless.Services.Models;

namespace NPaperless.Services.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { 
        
        }

        public DbSet<Correspondent> Correspondents { get; set; }

        public DbSet<Document> Documents { get; set; }
        //public DbSet<DocumentMetadata> DocumentMetadata { get; set; }

        //public DbSet<DocumentType> DocumentTypes { get; set; }
        //public DbSet<Correspondent> Correspondents { get; set; }

    }

   
}
