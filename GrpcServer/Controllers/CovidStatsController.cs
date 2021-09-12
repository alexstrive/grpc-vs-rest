using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GrpcServer.Controllers
{
    [ApiController]
    [Route("stats")]
    public class CovidStatsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly CovidStatsDatabaseService _db;

        public CovidStatsController(ILogger<CovidStatsController> logger, CovidStatsDatabaseService db)
        {
            _logger = logger;
            _db = db;
        }
        
        [HttpGet]
        [Route("")]
        public IEnumerable<CovidStatsEntry> GetAll()
        {
            return _db.Entries;
        }
    }
}