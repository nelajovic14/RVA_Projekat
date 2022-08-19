using Microsoft.AspNetCore.Mvc;
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

        public ZaposleniController(IZaposleniService zaposleniService, IBrutoHonorarService service)
        {
            this.zaposleniService = zaposleniService;
            this.service = service;
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
            zaposleniService.Obrisi(zaposleni);
            return Ok();
        }
        [HttpPost("dodaj")]
        public IActionResult Dodaj([FromBody] ZaposleniDto dto)
        {
            
            Zaposleni zaposleni=new Zaposleni { GodineIskustva = dto.GodineIskustva, Ime=dto.Ime,BrutoHonorarId=dto.BrutoHonorarId };
            zaposleniService.Dodaj(zaposleni);
            return Ok();
        }
    }
}
