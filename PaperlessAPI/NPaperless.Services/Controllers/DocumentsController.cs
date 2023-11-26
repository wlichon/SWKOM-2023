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
using NPaperless.Services.BusinessLogic;
using AutoMapper;

namespace NPaperless.Services.Controllers;

[ApiController]
[Route("/api/documents/")]
public partial class DocumentsController : ControllerBase
{
    private ILogger<DocumentsController> _logger;
    private readonly IDocumentLogic _documentLogic;
    private IMapper _mapper;


    public DocumentsController(ILogger<DocumentsController> logger, IDocumentLogic documentLogic, IMapper mapper)
    {
        _logger = logger;
        _documentLogic = documentLogic;
        _mapper = mapper;
    }

    [HttpGet(Name = "GetDocuments")]
    public async Task<IActionResult> GetDocuments()
    {
        var docs = await _documentLogic.GetAllDocs();

        ListResponse<DocumentDto> listDocs = null;
        try
        {
            listDocs = new ListResponse<DocumentDto>()
            {
                Count = docs.Count,
                Next = null,
                Previous = null,
                Results = _mapper.Map<List<Document>, List<DocumentDto>>(docs)
            };

        }catch(Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return Ok(listDocs);
    }

    [HttpGet("{id}", Name = "GetDocument")]
    public async Task<IActionResult> GetDocument([FromRoute] uint id)
    {
        var doc = await _documentLogic.GetOneDoc(id);

        return Ok(_mapper.Map<DocumentDto>(doc));
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
        var updatedDoc = await _documentLogic.UpdateOneDoc(id, _mapper.Map<Document>(document));

        return Ok(_mapper.Map<DocumentDto>(updatedDoc));
    }

    [HttpDelete("{id:int}", Name = "DeleteDocument")]
    public async Task<IActionResult> DeleteDocument([FromRoute] uint id)
    {
        var deletedDoc = await _documentLogic.DeleteOneDoc(id);

        return Ok(_mapper.Map<DocumentDto>(deletedDoc));
    }



    [HttpPost("post_document", Name = "UploadDocument")]
    public async Task<IActionResult> UploadDocument([FromForm] IEnumerable<IFormFile> document)
    {

        var createdDoc = await _documentLogic.CreateOneDoc(document);
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
