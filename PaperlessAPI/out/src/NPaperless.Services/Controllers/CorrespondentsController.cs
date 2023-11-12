using Microsoft.AspNetCore.Mvc;
using FizzWare.NBuilder;
using AutoMapper;
using NPaperless.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using NPaperless.Services.Services.CorrespondentsRepo;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace NPaperless.Services.Controllers;

[ApiController]
[Route("/api/correspondents/")]
// [Route("[controller]")]
public class CorrespondentsController : ControllerBase
{
    private ILogger<CorrespondentsController> _logger;
    
    private readonly ICorrespondentRepo _correspondentRepo;

    public CorrespondentsController(ILogger<CorrespondentsController> logger, ICorrespondentRepo correspondentRepo)
    {
        _logger = logger;
        _correspondentRepo = correspondentRepo;
    }

    [HttpOptions]

    public IActionResult Options()
    {
        return Ok();
    }

    [HttpGet(Name = "GetCorrespondents")]
    public async Task<IActionResult> GetCorrespondents()
    {
        var corrs =  await _correspondentRepo.GetAll();
        return Ok(new ListResponse<Correspondent>()
        {
            Count = corrs.Count,
            Next = null,
            Previous = null,
            Results = corrs
        });
        //return Ok(corrs);
    }

    [HttpPost(Name = "CreateCorrespondent")]
    public async Task<IActionResult> CreateCorrespondent(CorrespondentDto correspondent)
    {

        var corr = await _correspondentRepo.CreateOne(correspondent);
        
        return Ok(corr);
    }

    [HttpOptions("{id:int}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult OptionsId()
    {
        return Ok();
    }

    [HttpPut("{id:int}", Name = "UpdateCorrespondent")]
    public async Task<IActionResult> UpdateCorrespondent([FromRoute] int id, [FromBody] CorrespondentDto correspondent)
    {
        var corr = await _correspondentRepo.UpdateOne(id, correspondent);

        return Ok(corr);
    }

    [HttpDelete("{id:int}", Name = "DeleteCorrespondent")]
    public async Task<IActionResult> DeleteCorrespondent([FromRoute] int id)
    {
        var corr = await _correspondentRepo.DeleteOne(id);

        return Ok(corr);
    }
}
