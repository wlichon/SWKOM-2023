using System.IO;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Minio;
using NPaperless.Models.Models;
using NPaperless.Services.Models;
using NPaperless.Services.Repositories.DocumentsRepos;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml.Linq;
using Document = NPaperless.Services.Models.Document;
//new


namespace NPaperless.Services.BusinessLogic
{
    public class DocumentLogic : IDocumentLogic
    {
        private readonly IDocumentRepo _documentRepository;
        private IValidator<Document> _validator;
        private readonly MinIOUploader _minioClient;
        private readonly RabbitMQClient _rabbitMQClient;
        private readonly ElasticSearchClient _elasticSearchClient;
        private readonly ILogger _logger;


        public DocumentLogic(IDocumentRepo documentRepository, IValidator<Document> validator, IHostingEnvironment env, ILogger logger)
        {
            _rabbitMQClient = new RabbitMQClient(env, logger);
            _minioClient = new MinIOUploader(env, logger);
            _documentRepository = documentRepository;
            _validator = validator;
            _elasticSearchClient = new ElasticSearchClient();
            _logger = logger;
        }

        public async Task<Document> CreateOneDoc(IEnumerable<IFormFile> file)
        {
            Document document = new Document();

            var f = file.FirstOrDefault();

            if (f == null)
                return document;

            var fileName = f.FileName;
            document.title = fileName;      //show file title in frontend

            await _documentRepository.CreateOneDoc(document);

            await _minioClient.UploadFileAsync(f);

            _rabbitMQClient.PublishMessage(f.FileName, document.id);



            return document;
        }

        public async Task<Document> DeleteOneDoc(uint id)
        {
            return await _documentRepository.DeleteOneDoc(id);
        }

        public async Task<List<Document>> GetAllDocs()
        {
            
            // You can add business logic here if needed
            var docs = await _documentRepository.GetAllDocs();

            return docs;
          
        }

        /*public async Task<List<Document>> SearchDocs(string searchTerm)
        {
            var docs = await _elasticSearchClient.SearchAsync<Document>(
                q => q.MatchPhrasePrefix(m => m.Field(f => f.title).Query(searchTerm)),
                "swkom2023-documents"
                );
            return docs;
        }*/

        /*public async Task<List<Document>> SearchDocs(string searchTerm)
        {
            // Call the ElasticSearchClient method to perform the search
            var searchResults = await _elasticSearchClient.SearchTitleAndContentAsync<Document>(searchTerm);

            Console.WriteLine($"Search for term '{searchTerm}' returned {searchResults.Count} documents.");

            // Process or filter the search results as needed
            // For example, you might want to filter out certain documents or sort the results

            return searchResults;
        }*/

        public async Task<List<Document>> SearchDocs(string searchTerm)
        {
            // Call the ElasticSearchClient method to perform the search
            var searchResults = await _elasticSearchClient.SearchTitleAndContentAsync(searchTerm);

            Console.WriteLine($"Search for term '{searchTerm}' returned {searchResults.Count} documents.");

            return searchResults;
        }





        public async Task<Document> GetOneDoc(uint id)
        {
            return await _documentRepository.GetOneDoc(id);
        }

        public async Task<Document> UpdateOneDoc(uint id, Document document)
        {
            ValidationResult valResult = await _validator.ValidateAsync(document);
            if (!valResult.IsValid)
            {
                _logger.Log(LogLevel.Information, valResult.ToString());
                return document;
            }
            
            return await _documentRepository.UpdateOneDoc(id, document);
        }
    }
}
