using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPaperless.Services.Models;
using System;

namespace NPaperless.Services.Controllers;

[ApiController]
[Route("/api")]
public class TasksController : ControllerBase
{
    private readonly ILogger<TasksController> _logger;

    public TasksController(ILogger<TasksController> logger)
    {
        _logger = logger;
    }

    [HttpGet("tasks")]
    public IActionResult GetTasks()
    {
        Random rand = new Random();
        int count = 10;

        return Ok(Builder<Models.Task>.CreateListOfSize(count)
                        .All()
                        .With(p => p.Type = Models.TaskType.file)
                        .With(p => p.DateCreated = DateTimeOffset.Now.AddDays(rand.Next(-10, 0)))
                        .With(p => p.DateDone = DateTimeOffset.Now.AddDays(rand.Next(-10, 0)))
                        .Build());
    }

    [HttpOptions("tasks")]
    public IActionResult Options()
    {
        // Return a 200 OK response for CORS preflight requests
        return Ok();
    }

    [HttpPost("acknowledge_tasks")]
    public IActionResult AckTasks([FromBody] AckTasksRequest tasks)
    {
        _logger.LogInformation($"Acknowledge tasks: {string.Join(", ", tasks.Tasks)}");
        return Ok();
    }
}

