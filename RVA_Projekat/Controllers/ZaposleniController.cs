using Microsoft.AspNetCore.Mvc;
using RVA_Projekat.Dogadjaji;
using RVA_Projekat.Dto;
using RVA_Projekat.Interface;
using RVA_Projekat.Model;

namespace RVA_Projekat.Controllers
{
    [Route("api/zaposleni")]
    [ApiController]
    public class ZaposleniController : ControllerBase
    {
        IZaposleniService zaposleniService;
        IBrutoHonorarService service;
        ILoggerManager loggerManager;

        public ZaposleniController(IZaposleniService zaposleniService, IBrutoHonorarService service,ILoggerManager loggerManager)
        {
            this.zaposleniService = zaposleniService;
            this.service = service;
            this.loggerManager = loggerManager;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return Ok(zaposleniService.GetAll());
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] ZaposleniDto dto)
        {
            Zaposleni zaposleni=zaposleniService.Get(dto.Id);
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
        [HttpPost("dodaj")]
        public IActionResult Dodaj([FromBody] ZaposleniDto dto)
        {
            
            Zaposleni zaposleni=new Zaposleni { GodineIskustva = dto.GodineIskustva, Ime=dto.Ime,BrutoHonorarId=dto.BrutoHonorarId };
            zaposleniService.Dodaj(zaposleni);
            loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Add zaposleni", korisnik = dto.Korisnik, poruka = "SUCCES" });
            return Ok();
        }
        [HttpPost("izmeni")]
        public IActionResult Edit([FromBody] ZaposleniDto dto)
        {
            Zaposleni z=zaposleniService.Edit(dto);
            loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Edit zaposleni", korisnik = dto.Korisnik, poruka = "SUCCES" });
            return Ok(z);
        }
    }
}
