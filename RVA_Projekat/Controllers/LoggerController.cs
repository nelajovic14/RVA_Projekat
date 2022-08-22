using Microsoft.AspNetCore.Mvc;
using RVA_Projekat.Dogadjaji;
using RVA_Projekat.Dto;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Controllers
{
    [Route("api/logger")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly ILoggerManager logger;

        public LoggerController(ILoggerManager logger)
        {
            this.logger = logger;


        }
        [HttpPost]
        public IActionResult Post([FromBody] UserDto dto)
        {
            List<Dogadjaj> info = logger.GetInfo(dto.Username);
            return Ok(info);
        }
    }
}
