using NPaperless.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NPaperless.Services.Repositories.DocumentsRepos
{
    public interface IDocumentRepo
    {
        public Task<List<Document>> GetAll();

        public Task<Document> GetOne(long id);

        public Task<Document> UpdateOne(long id);

        public Task<Document> DeleteOne(long id);

        public Task<Document> CreateOne(long id)
    }
}
