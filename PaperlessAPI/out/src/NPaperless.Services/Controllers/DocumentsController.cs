using Microsoft.AspNetCore.Mvc;
using FizzWare.NBuilder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.Json;
using NPaperless.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using NPaperless.Services.Services.CorrespondentsRepo;
using NPaperless.Services.Repositories.DocumentsRepos;
using System.Threading.Tasks;

namespace NPaperless.Services.Controllers;

[ApiController]
[Route("/api/documents/")]
public partial class DocumentsController : ControllerBase
{
    private ILogger<DocumentsController> _logger;
    private readonly IDocumentRepo _documentRepo;


    public DocumentsController(ILogger<DocumentsController> logger, IDocumentRepo documentRepo)
    {
        _logger = logger;
        _documentRepo = documentRepo;
    }

    [HttpGet(Name = "GetDocuments")]
    public async Task<IActionResult> GetDocuments([FromQuery] DocumentsFilterModel filter)
    {
        var docs = await _documentRepo.GetAllDocs();
        return Ok(new ListResponse<Document>()
        {
            Count = docs.Count,
            Next = null,
            Previous = null,
            Results = docs
        });

        /*
        Random r = new Random();

        int count = r.Next(1, 20);

        return this.Ok(new ListResponse<Document>()
        {
            Count = count,
            Next = null,
            Previous = null,
            Results = Builder<Document>.CreateListOfSize(count)
                .All()
                    .With(p => p.ArchiveSerialNumber = null)
                    .With(p => p.ArchivedFileName = p.Id + ".pdf")
                    .With(p => p.OriginalFileName = p.Id + ".pdf")
                    .With(p => p.DocumentType = filter.DocTypeId)
                    .With(p => p.Correspondent = filter.CorrespondentId)
                    .With(p => p.Added = DateTime.Now)
                    .With(p => p.CreatedDate = DateTime.Now)
                    .With(p => p.Created = DateTime.Now)
                    .With(p => p.Modified = DateTime.Now)
                    .With(p => p.Tags = Enumerable.Range(1, r.Next(1, 4)).Select(u => (uint)u).ToArray())
                .Build()
        });
        */
    }

    [HttpGet("{id}", Name = "GetDocument")]
    public async Task<IActionResult> GetDocument([FromRoute] uint id)
    {
        var doc = await _documentRepo.GetOneDoc(id);

        return Ok(doc);
    }

    [HttpGet("{id}/suggestions", Name = "GetDocumentSuggestions")]
    public IActionResult GetDocumentSuggestions([FromRoute] uint id)
    {
        string result = @"{
            ""correspondents"": [
                6
            ],
            ""tags"": [
                3
            ],
            ""document_types"": [
                3
            ],
            ""storage_paths"": [],
            ""dates"": [
                ""2022-06-08"",
                ""2022-12-01"",
                ""2022-12-05""
            ]
        }";

        return this.Ok(JsonSerializer.Deserialize<object>(result));
    }


    [HttpPut("{id:int}", Name = "UpdateDocument")]
    public async Task<IActionResult> UpdateDocument([FromRoute] uint id, [FromBody] DocumentDto document)
    {
        var updatedDoc = await _documentRepo.UpdateOneDoc(id, document);

        return Ok(updatedDoc);
    }

    [HttpDelete("{id:int}", Name = "DeleteDocument")]
    public async Task<IActionResult> DeleteDocument([FromRoute] uint id)
    {
        var deletedDoc = await _documentRepo.DeleteOneDoc(id);

        return Ok(deletedDoc);
    }



    [HttpPost("post_document", Name = "UploadDocument")]
    public async Task<IActionResult> UploadDocument([FromForm] IEnumerable<IFormFile> document)
    {


        DocumentDto doc = new DocumentDto();

        var createdDoc = await _documentRepo.CreateOneDoc(doc);
        return Ok(createdDoc);
    }


    [HttpGet("{id:int}/thumb/", Name = "GetDocumentThumb")]
    public IActionResult GetDocumentThumb([FromRoute] int id)
    {
        var bitmap = new Bitmap(128, 128, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        Graphics graphics = Graphics.FromImage(bitmap);

        var pen = new Pen(Color.Black, 5);

        graphics.FillRectangle(new SolidBrush(RandomColorGenerator.GetRandomColor()), new Rectangle(0, 0, 128, 128));

        using (var memoryStream = new MemoryStream())
        {
            bitmap.Save(memoryStream, ImageFormat.Jpeg);
            return new FileContentResult(memoryStream.ToArray(), "image/jpeg");
        }
    }

    [HttpGet("{id:int}/download/", Name = "DownloadDocument")]
    public IActionResult DownloadDocument([FromRoute] int id, [FromQuery] bool? original)
    {
        return Ok();
    }

    [HttpGet("{id:int}/preview/", Name = "GetDocumentPreview")]
    public IActionResult GetDocumentPreview([FromRoute] int id)
    {
        Bitmap bitmap = new Bitmap(1000, 5000, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        Graphics graphics = Graphics.FromImage(bitmap);

        var pen = new Pen(Color.Black, 5);

        graphics.FillRectangle(new SolidBrush(RandomColorGenerator.GetRandomColor()), new Rectangle(500, 500, 500, 3000));

        using (var memoryStream = new MemoryStream())
        {
            bitmap.Save(memoryStream, ImageFormat.Png);
            return new FileContentResult(memoryStream.ToArray(), "image/webp");
        }
    }

    [HttpGet("{id:int}/metadata/", Name = "GetDocumentMetadata")]
    public IActionResult GetDocumentMetadata([FromRoute] int id)
    {
        return Ok(Builder<DocumentMetadata>.CreateNew().Build());
    }
}
