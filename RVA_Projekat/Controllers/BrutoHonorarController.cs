using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVA_Projekat.Dogadjaji;
using RVA_Projekat.Dto;
using RVA_Projekat.Enums;
using RVA_Projekat.Interface.Bruto;
using RVA_Projekat.Interface.Neto;
using RVA_Projekat.Model;
using System;
using System.Collections.Generic;

namespace RVA_Projekat.Controllers
{
    [Route("api/brutohonorar")]
    [ApiController]
    public class BrutoHonorarController : ControllerBase
    {
        private IBrutoHonorarService service;
        private INetohonorarService netohonorarService;
        private ILoggerManager loggerManager; 
        public BrutoHonorarController(IBrutoHonorarService service, INetohonorarService netohonorarService,ILoggerManager loggerManager)
        {
            this.service = service;
            this.netohonorarService = netohonorarService;
            this.loggerManager = loggerManager;
        }
        [HttpGet]
        [Authorize(Roles ="user")]
        public IActionResult Get()
        {
            List<BrutoHonorar> brutoHonorars = service.GetAll();
            List<BrutoHonorarDto> brutoHonorarDto = new List<BrutoHonorarDto>();
            foreach(var bh in brutoHonorars)
            {
                brutoHonorarDto.Add(new BrutoHonorarDto { Id = bh.Id, TrenutnaPlata = bh.TrenutnaPlata, valuta = bh.valuta.ToString() });
            }
            return Ok(brutoHonorarDto);
        }
        [HttpPost("getbruto")]
        [Authorize(Roles ="user")]
        public IActionResult GetBruto([FromBody]NetoHonorarDto dto)
        {
            List<BrutoHonorar> brutoHonorars = service.GetAll();
            //NetoHonorar neto = netohonorarService.Get(dto.Id);
            foreach (var bh in brutoHonorars)
            {
                if (bh.Id == dto.BrutoHonorarId)
                {
                    return Ok(new BrutoHonorarDto { Id = bh.Id, TrenutnaPlata = bh.TrenutnaPlata, valuta = bh.valuta.ToString() });
                }
            }
            return Ok();
        }
        [HttpPost("getbrutoZaposleni")]
        [Authorize(Roles ="user")]
        public IActionResult GetBrutoZaposleni([FromBody] ZaposleniDto dto)
        {
            List<BrutoHonorar> brutoHonorars = service.GetAll();
            foreach (var bh in brutoHonorars)
            {
                if (bh.Id == dto.BrutoHonorarId)
                {
                    return Ok(new BrutoHonorarDto { Id = bh.Id, TrenutnaPlata = bh.TrenutnaPlata, valuta = bh.valuta.ToString() });
                }
            }
            return Ok();
        }
        [HttpDelete]
        [Authorize(Roles ="user")]
        public IActionResult Delete([FromBody] BrutoHonorarDto dto)
        {
            BrutoHonorar brutoHonorar = service.Get(dto);
            List<NetoHonorar> netoHonorars = netohonorarService.GetAll();
            foreach(NetoHonorar nh in netoHonorars)
            {
                if (nh.BrutoHonorarId == dto.Id)
                {
                    netohonorarService.Obrisi(nh,DateTime.Now);
                    break;
                }
            }
            try
            {
                service.Delete(brutoHonorar);
                loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Delete bruto", korisnik = dto.Korisnik, poruka = "SUCCES" });
                return Ok();
            }
            catch
            {
                loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Delete bruto", korisnik = dto.Korisnik, poruka = "ERROR" });
                return BadRequest();
            }
            
        }
        [HttpPost]
        [Authorize(Roles = "user")]
        public IActionResult Post([FromBody] BrutoHonorarDto dto)
        {
            
            BrutoHonorar bh = service.DodajEntitet(dto);
            loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Add bruto", korisnik = dto.Korisnik, poruka = "SUCCES" });
            return Ok(bh);
        }
        [HttpPut]
        [Authorize(Roles = "user")]
        public IActionResult Put([FromBody] BrutoHonorarDto dto)
        {
            BrutoHonorar bh=service.Edit(dto);
            loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Edit bruto", korisnik = dto.Korisnik, poruka = "SUCCES" });
            return Ok(bh);
        }
        

    }
}
