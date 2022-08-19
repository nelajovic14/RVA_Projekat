using Microsoft.AspNetCore.Mvc;
using RVA_Projekat.Dto;
using RVA_Projekat.Interface;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Controllers
{
    [Route("api/netohonorar")]
    [ApiController]
    public class NetohonorarController : ControllerBase
    {
        private INetohonorarService netohonorarService;
        public NetohonorarController(INetohonorarService netohonorarService)
        {
            this.netohonorarService = netohonorarService;
        }
        [HttpPost("get")]
        public List<NetoHonorarDto> Get(){
            List<NetoHonorar> netoHonorars= netohonorarService.GetAll();
            List<NetoHonorarDto> netoHonorarsdto = new List<NetoHonorarDto>();
            
            foreach(NetoHonorar nh in netoHonorars)
            {
                List<string> porezs = new List<string>();
                if (nh.Porezi != null)
                {
                    foreach (var p in nh.Porezi)
                    {
                        porezs.Add(p.Tip.ToString());
                    }
                }
                netoHonorarsdto.Add(new NetoHonorarDto { Id=nh.Id,umanjenje=nh.umanjenje.ToString(),uvecanje=nh.uvecanje.ToString(),Porezi=porezs });
            }
            return netoHonorarsdto;
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] NetoHonorarDto dto)
        {
            NetoHonorar netoHonorar=netohonorarService.Get(dto.Id);
            netohonorarService.Obrisi(netoHonorar);
            return Ok();
        }
        [HttpPost("dodaj")]
        public IActionResult Dodaj([FromBody] NetoHonorarDto dto)
        {
            NetoHonorar nh=netohonorarService.DodajEntitet(dto);
            return Ok(nh);
        }
    }
}
