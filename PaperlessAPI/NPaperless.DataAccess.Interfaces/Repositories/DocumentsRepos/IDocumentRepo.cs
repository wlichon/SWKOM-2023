using NPaperless.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NPaperless.Services.Repositories.DocumentsRepos
{
    public interface IDocumentRepo
    {
        public Task<List<Document>> GetAllDocs();

        public Task<Document> GetOneDoc(uint id);

        public Task<Document> UpdateOneDoc(uint id, Document document);

        public Task<Document> DeleteOneDoc(uint id);

        public Task<Document> CreateOneDoc(Document document);


    }
}
