using Microsoft.AspNetCore.Http;
using NPaperless.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace NPaperless.Services.BusinessLogic
{
    public interface IDocumentLogic
    {
        public Task<List<Document>> GetAllDocs();

        public Task<Document> GetOneDoc(uint id);

        public Task<List<Document>> SearchDocs(string searchTerm);

        public Task<Document> UpdateOneDoc(uint id, Document document);

        public Task<Document> DeleteOneDoc(uint id);

        public Task<Document> CreateOneDoc(IEnumerable<IFormFile> file);
    }
}
