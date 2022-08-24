using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVA_Projekat.Dogadjaji;
using RVA_Projekat.Dto;
using RVA_Projekat.Interface.InterfaceZaposlenih;
using RVA_Projekat.Model;

namespace RVA_Projekat.Controllers
{
    [Route("api/zaposleni")]
    [ApiController]
    public class ZaposleniController : ControllerBase
    {
        IZaposleniService zaposleniService;
        ILoggerManager loggerManager;

        public ZaposleniController(IZaposleniService zaposleniService,ILoggerManager loggerManager)
        {
            this.zaposleniService = zaposleniService;
            this.loggerManager = loggerManager;
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Get()
        {

            return Ok(zaposleniService.GetAll());
        }
        [HttpDelete]
        [Authorize(Roles ="user")]
        public IActionResult Delete([FromBody] ZaposleniDto dto)
        {
            Zaposleni zaposleni=zaposleniService.Get(dto);
            try
            {
                zaposleniService.Obrisi(zaposleni);
                loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Delete zaposleni", korisnik = dto.Korisnik, poruka = "SUCCES" });
            }
            catch
            {
                loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Delete zaposleni", korisnik = dto.Korisnik, poruka = "ERROR" });
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost]
        [Authorize(Roles = "user")]
        public IActionResult Post([FromBody] ZaposleniDto dto)
        {            
            Zaposleni zaposleni=new Zaposleni { GodineIskustva = dto.GodineIskustva, Ime=dto.Ime,BrutoHonorarId=dto.BrutoHonorarId };
            zaposleniService.Dodaj(zaposleni);
            loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Add zaposleni", korisnik = dto.Korisnik, poruka = "SUCCES" });
            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "user")]
        public IActionResult Put([FromBody] ZaposleniDto dto)
        {
            Zaposleni z=zaposleniService.Edit(dto);
            loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Edit zaposleni", korisnik = dto.Korisnik, poruka = "SUCCES" });
            return Ok(z);
        }
    }
}
