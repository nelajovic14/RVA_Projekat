using Microsoft.AspNetCore.Mvc;
using RVA_Projekat.Dogadjaji;
using RVA_Projekat.Dto;
using RVA_Projekat.Enums;
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
        private ILoggerManager loggerManager;
        public NetohonorarController(INetohonorarService netohonorarService,ILoggerManager loggerManager)
        {
            this.netohonorarService = netohonorarService;
            this.loggerManager = loggerManager;
        }
        [HttpGet]
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
            try
            {
                netohonorarService.Obrisi(netoHonorar);
                loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Delete neto", korisnik = dto.Korisnik, poruka = "SUCCES" });
            }
            catch
            {
                loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Delete neto", korisnik = dto.Korisnik, poruka = "ERROR" });
            }
            return Ok();
        }
        [HttpPost("dodaj")]
        public IActionResult Dodaj([FromBody] NetoHonorarDto dto)
        {
            NetoHonorar nh=netohonorarService.DodajEntitet(dto);
            loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Add neto", korisnik = dto.Korisnik, poruka = "SUCCES" });
            return Ok(nh);
        }
        [HttpPost("dupliraj")]
        public IActionResult Dupliraj([FromBody] NetoHonorarDto netoHonorar)
        {
            NetoHonorar netoHon = netohonorarService.Get(netoHonorar.Id);
            NetoHonorarDto dto = new NetoHonorarDto{ BrutoHonorarId=netoHon.BrutoHonorarId, Porezi=netoHonorar.Porezi, umanjenje=netoHonorar.umanjenje, uvecanje=netoHonorar.uvecanje};            
            NetoHonorar neto = netohonorarService.DodajEntitet(dto);
            loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Duplicate neto", korisnik = netoHonorar.Korisnik, poruka = "SUCCES" });
            return Ok(neto);
        }

        [HttpPost("izmeni")]
        public IActionResult Izmeni([FromBody] NetoHonorarDto dto)
        {
            NetoHonorar nh= netohonorarService.Edit(dto);
            loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Edit neto", korisnik = dto.Korisnik, poruka = "SUCCES" });
            return Ok(nh);
        }

        [HttpPost("pretraga")]
        public IActionResult Pretrazi([FromBody] PodaciZaPretragu podaci)
        {
            List<NetoHonorar> netoHonorars = netohonorarService.GetAll();
            List<NetoHonorar> pretrazeni = new List<NetoHonorar>();
            List<PorezType> poreziNH = new List<PorezType>();
            if (podaci.Porezi != null)
            {
                foreach (var p in podaci.Porezi)
                {
                    if (p == "POTROSNJA")
                        poreziNH.Add(PorezType.POTROSNJA);
                    else if (p == "DOBIT")
                        poreziNH.Add(Enums.PorezType.DOBIT);
                    else if (p == "DOHODAK")
                        poreziNH.Add(Enums.PorezType.DOHODAK);
                    else if (p == "IMOVINA")
                        poreziNH.Add(Enums.PorezType.IMOVINA);

                }
            }
            if ((podaci.Porezi != null) && (podaci.Uvecanje!=null) && (podaci.Umanjenje != null))
            {

                foreach (var n in netoHonorars)
                {
                    bool pripadaPorezu = true;
                    List<PorezType> porezs = new List<PorezType>();
                    
                    if (n.Porezi != null)
                    {
                        foreach (var p in n.Porezi)
                        {
                            if (p.Tip == PorezType.POTROSNJA)
                                porezs.Add(PorezType.POTROSNJA);
                            else if (p.Tip == PorezType.DOBIT)
                                porezs.Add(Enums.PorezType.DOBIT);
                            else if (p.Tip == PorezType.DOHODAK)
                                porezs.Add(Enums.PorezType.DOHODAK);
                            else if (p.Tip == PorezType.IMOVINA)
                                porezs.Add(Enums.PorezType.IMOVINA);

                        }
                    }
                    foreach (var p in poreziNH)
                    {
                        if (!porezs.Contains(p))
                            pripadaPorezu = false;
                    }
                    if ((n.umanjenje.ToString() == podaci.Umanjenje) && (n.uvecanje.ToString() == podaci.Uvecanje) && pripadaPorezu)
                    {
                        pretrazeni.Add(n);
                    }
                }
            }
            else if (podaci.Uvecanje != null && podaci.Umanjenje != null)
            {
                foreach (var n in netoHonorars)
                {

                    if ((n.umanjenje.ToString() == podaci.Umanjenje) && (n.uvecanje.ToString() == podaci.Uvecanje))
                    {
                        pretrazeni.Add(n);
                    }
                }
            }
            else if(podaci.Porezi != null && podaci.Uvecanje != null )
            {
                foreach (var n in netoHonorars)
                {
                    bool pripadaPorezu = true;
                    List<PorezType> porezs = new List<PorezType>();
                    if (n.Porezi != null)
                    {
                        foreach (var p in n.Porezi)
                        {
                            if (p.Tip == PorezType.POTROSNJA)
                                porezs.Add(PorezType.POTROSNJA);
                            else if (p.Tip == PorezType.DOBIT)
                                porezs.Add(Enums.PorezType.DOBIT);
                            else if (p.Tip == PorezType.DOHODAK)
                                porezs.Add(Enums.PorezType.DOHODAK);
                            else if (p.Tip == PorezType.IMOVINA)
                                porezs.Add(Enums.PorezType.IMOVINA);

                        }
                    }
                    foreach (var p in poreziNH)
                    {
                        if (!porezs.Contains(p))
                            pripadaPorezu = false;
                    }
                    if ( (n.uvecanje.ToString() == podaci.Uvecanje) && pripadaPorezu)
                    {
                        pretrazeni.Add(n);
                    }
                }
            }
            else if(podaci.Porezi != null && podaci.Umanjenje != null)
            {
                foreach (var n in netoHonorars)
                {
                    bool pripadaPorezu = true;
                    List<PorezType> porezs = new List<PorezType>();
                    if (n.Porezi != null)
                    {
                        foreach (var p in n.Porezi)
                        {
                            if (p.Tip == PorezType.POTROSNJA)
                                porezs.Add(PorezType.POTROSNJA);
                            else if (p.Tip == PorezType.DOBIT)
                                porezs.Add(Enums.PorezType.DOBIT);
                            else if (p.Tip == PorezType.DOHODAK)
                                porezs.Add(Enums.PorezType.DOHODAK);
                            else if (p.Tip == PorezType.IMOVINA)
                                porezs.Add(Enums.PorezType.IMOVINA);

                        }
                    }
                    foreach (var p in poreziNH)
                    {
                        if (!porezs.Contains(p))
                            pripadaPorezu = false;
                    }
                    if ((n.umanjenje.ToString() == podaci.Umanjenje) && pripadaPorezu)
                    {
                        pretrazeni.Add(n);
                    }
                }
            }
            else if(podaci.Porezi != null )
            {
                foreach (var n in netoHonorars)
                {
                    bool pripadaPorezu = true;
                    List<PorezType> porezs = new List<PorezType>();
                    if (n.Porezi != null)
                    {
                        foreach (var p in n.Porezi)
                        {
                            if (p.Tip == PorezType.POTROSNJA)
                                porezs.Add(PorezType.POTROSNJA);
                            else if (p.Tip == PorezType.DOBIT)
                                porezs.Add(Enums.PorezType.DOBIT);
                            else if (p.Tip == PorezType.DOHODAK)
                                porezs.Add(Enums.PorezType.DOHODAK);
                            else if (p.Tip == PorezType.IMOVINA)
                                porezs.Add(Enums.PorezType.IMOVINA);

                        }
                    }
                    foreach (var p in podaci.Porezi)
                    {
                        if (p == "POTROSNJA")
                            poreziNH.Add(PorezType.POTROSNJA);
                        else if (p == "DOBIT")
                            poreziNH.Add(Enums.PorezType.DOBIT);
                        else if (p == "DOHODAK")
                            poreziNH.Add(Enums.PorezType.DOHODAK);
                        else if (p == "IMOVINA")
                            poreziNH.Add(Enums.PorezType.IMOVINA);

                    }
                    foreach (var p in poreziNH)
                    {
                        if (!porezs.Contains(p))
                            pripadaPorezu = false;
                    }
                    if (pripadaPorezu)
                    {
                        pretrazeni.Add(n);
                    }
                }
            }
            else if (podaci.Umanjenje != null)
            {
                foreach (var n in netoHonorars)
                {
                    if (n.umanjenje.ToString() == podaci.Umanjenje)
                    {
                        pretrazeni.Add(n);
                    }
                }
            }
            else if(podaci.Uvecanje !=null)
            {
                foreach (var n in netoHonorars)
                {
                    if (n.uvecanje.ToString() == podaci.Uvecanje)
                    {
                        pretrazeni.Add(n);
                    }
                }
            }
            List<NetoHonorarDto> netoHonorarsdto = new List<NetoHonorarDto>();

            foreach (NetoHonorar nh in pretrazeni)
            {
                List<string> porezs = new List<string>();
                if (nh.Porezi != null)
                {
                    foreach (var p in nh.Porezi)
                    {
                        porezs.Add(p.Tip.ToString());
                    }
                }
                netoHonorarsdto.Add(new NetoHonorarDto { Id = nh.Id, umanjenje = nh.umanjenje.ToString(), uvecanje = nh.uvecanje.ToString(), Porezi = porezs });
            }
            loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Search neto", korisnik = podaci.Korisnik, poruka = "SUCCES" });
            return Ok(netoHonorarsdto);
        }
    }
}
