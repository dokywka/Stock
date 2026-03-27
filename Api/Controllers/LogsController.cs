using Microsoft.AspNetCore.Mvc;
using StockApp.Core.Interfaces;
using StockApp.Core.Models;
using System.Collections.Generic;

namespace StockApp.Api.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        // GET: api/logs?level=Error
        [HttpGet]
        public ActionResult<List<LogEntry>> GetLogs([FromQuery] string level)
        {
            var logs = _logService.GetLogs(level);
            return Ok(logs);
        }
    }
}