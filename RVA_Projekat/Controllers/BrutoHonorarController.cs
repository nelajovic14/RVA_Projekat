using Microsoft.AspNetCore.Mvc;
using RVA_Projekat.Dto;
using RVA_Projekat.Enums;
using RVA_Projekat.Interface;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Controllers
{
    [Route("api/brutohonorar")]
    [ApiController]
    public class BrutoHonorarController : ControllerBase
    {
        private IBrutoHonorarService service;
        private INetohonorarService netohonorarService;
        public BrutoHonorarController(IBrutoHonorarService service, INetohonorarService netohonorarService)
        {
            this.service = service;
            this.netohonorarService = netohonorarService;
        }
        [HttpGet]   
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
        public IActionResult getbruto([FromBody]NetoHonorarDto dto)
        {
            List<BrutoHonorar> brutoHonorars = service.GetAll();
            NetoHonorar neto = netohonorarService.Get(dto.Id);
            foreach (var bh in brutoHonorars)
            {
                if (bh.Id == neto.BrutoHonorarId)
                {
                    return Ok(new BrutoHonorarDto { Id = bh.Id, TrenutnaPlata = bh.TrenutnaPlata, valuta = bh.valuta.ToString() });
                }
            }
            return Ok();
        }
        [HttpPost("getbrutoZaposleni")]
        public IActionResult getbrutoZaposleni([FromBody] Zaposleni dto)
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
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] BrutoHonorarDto dto)
        {
            BrutoHonorar brutoHonorar = service.GetById(dto.Id);
            List<NetoHonorar> netoHonorars = netohonorarService.GetAll();
            foreach(NetoHonorar nh in netoHonorars)
            {
                if (nh.BrutoHonorarId == dto.Id)
                {
                    netohonorarService.Obrisi(nh);
                    break;
                }
            }
            service.Delete(brutoHonorar);
            return Ok();
        }
        [HttpPost("dodaj")]
        public IActionResult Dodaj([FromBody] BrutoHonorarDto dto)
        {
            BrutoHonorar bh=service.DodajEntitet(dto);
            return Ok(bh);
        }
    }
}
