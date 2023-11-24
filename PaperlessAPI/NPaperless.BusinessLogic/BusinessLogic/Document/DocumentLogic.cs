using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Configuration;
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


        public DocumentLogic(IDocumentRepo documentRepository, IValidator<Document> validator, IHostingEnvironment env)
        {
            _rabbitMQClient = new RabbitMQClient(env);
            _minioClient = new MinIOUploader(env);
            _documentRepository = documentRepository;
            _validator = validator;
        }

        public async Task<Document> CreateOneDoc(IEnumerable<IFormFile> file)
        {
            Document document = new Document();

            ValidationResult valResult = await _validator.ValidateAsync(document);

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
            return await _documentRepository.UpdateOneDoc(id, document);
        }
    }
}
