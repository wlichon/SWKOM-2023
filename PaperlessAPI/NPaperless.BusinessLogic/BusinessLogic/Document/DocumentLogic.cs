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

namespace NPaperless.Services.BusinessLogic
{
    public class DocumentLogic : IDocumentLogic
    {
        private readonly IDocumentRepo _documentRepository;
        private IValidator<Document> _validator;
        private readonly MinIOUploader _minioClient;
        private readonly RabbitMQClient _rabbitMQClient;
        private readonly ILogger _logger;


        public DocumentLogic(IDocumentRepo documentRepository, IValidator<Document> validator, IHostingEnvironment env, ILogger logger)
        {
            _rabbitMQClient = new RabbitMQClient(env, logger);
            _minioClient = new MinIOUploader(env, logger);
            _documentRepository = documentRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Document> CreateOneDoc(IEnumerable<IFormFile> file)
        {
            Document document = new Document();

            var f = file.FirstOrDefault();

            if (f == null)
                return document;

            _rabbitMQClient.PublishMessage(f.FileName);

            await _minioClient.UploadFileAsync(f);



            return await _documentRepository.CreateOneDoc(document);
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
