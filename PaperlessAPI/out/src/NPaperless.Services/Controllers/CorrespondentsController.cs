using Microsoft.AspNetCore.Mvc;
using FizzWare.NBuilder;
using AutoMapper;
using NPaperless.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using NPaperless.Services.Services.CorrespondentsRepo;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace NPaperless.Services.Controllers;

[ApiController]
[Route("/api/correspondents/")]
// [Route("[controller]")]
public class CorrespondentsController : ControllerBase
{
    private ILogger<CorrespondentsController> _logger;
    
    private readonly ICorrespondentRepo _correspondentRepo;

    private IMapper _mapper;

    public CorrespondentsController(ILogger<CorrespondentsController> logger, ICorrespondentRepo correspondentRepo, IMapper mapper)
    {
        _logger = logger;
        _correspondentRepo = correspondentRepo;
        _mapper = mapper;

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
            Results = _mapper.Map<List<Correspondent>>(corrs)
        });
        //return Ok(corrs);
    }

    [HttpPost(Name = "CreateCorrespondent")]
    public async Task<IActionResult> CreateCorrespondent(CorrespondentDto correspondent)
    {
        
        var corr = await _correspondentRepo.CreateOne(_mapper.Map<Correspondent>(correspondent));
        
        return Ok(_mapper.Map<DocumentDto>(corr));
    }

    [HttpPut("{id:int}", Name = "UpdateCorrespondent")]
    public async Task<IActionResult> UpdateCorrespondent([FromRoute] int id, [FromBody] CorrespondentDto correspondent)
    {
        var corr = await _correspondentRepo.UpdateOne(id, _mapper.Map<Correspondent>(correspondent));

        return Ok(_mapper.Map<DocumentDto>(corr));
    }

    [HttpDelete("{id:int}", Name = "DeleteCorrespondent")]
    public async Task<IActionResult> DeleteCorrespondent([FromRoute] int id)
    {
        var corr = await _correspondentRepo.DeleteOne(id);

        return Ok(_mapper.Map<DocumentDto>(corr));
    }
}
