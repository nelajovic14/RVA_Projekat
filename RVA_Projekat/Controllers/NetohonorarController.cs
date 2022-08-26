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
        [Authorize(Roles ="user")]
        public IActionResult Get(){
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
                netoHonorarsdto.Add(new NetoHonorarDto { Id=nh.Id,umanjenje=nh.umanjenje.ToString(),uvecanje=nh.uvecanje.ToString(),Porezi=porezs,BrutoHonorarId=nh.BrutoHonorarId });
            }
            return Ok(netoHonorarsdto);
        }
        [HttpDelete]
        [Authorize(Roles ="user")]
        public IActionResult Delete([FromBody] NetoHonorarDto dto)
        {
            NetoHonorar netoHonorar=netohonorarService.Get(dto);
            DateTime izmenaBrisanja = DateTime.Parse(dto.VremeZaIzmenu);
             bool uspesno=netohonorarService.Obrisi(netoHonorar,izmenaBrisanja);
            if (uspesno)
            {
                loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Delete neto", korisnik = dto.Korisnik, poruka = "SUCCES" });
            }
            else
            {
                loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Delete neto", korisnik = dto.Korisnik, poruka = "ERROR" });
            }
            return Ok();
        }
        [HttpPost]
        [Authorize(Roles ="user")]
        public IActionResult Post([FromBody] NetoHonorarDto dto)
        {
            NetoHonorar nh=netohonorarService.DodajEntitet(dto);
            loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Add neto", korisnik = dto.Korisnik, poruka = "SUCCES" });
            return Ok(nh);
        }
        [HttpPost("dupliraj")]
        [Authorize(Roles ="user")]
        public IActionResult Dupliraj([FromBody] NetoHonorarDto netoHonorar)
        {
            NetoHonorar nh= netohonorarService.Dupliraj(netoHonorar);
            loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Duplicate Neto", korisnik = netoHonorar.Korisnik, poruka = "SUCCES" });
            return Ok(nh);
        }

        [HttpPut]
        [Authorize(Roles ="user")]
        public IActionResult Put([FromBody] NetoHonorarDto dto)
        {           
            NetoHonorar nh= netohonorarService.Edit(dto);
            if (nh != null)
            {
                loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Edit neto", korisnik = dto.Korisnik, poruka = "SUCCES" });
                return Ok(nh);
            }
            else
            {

                loggerManager.LogInformation(new Dogadjaj { dogadjaj = "Edit neto", korisnik = dto.Korisnik, poruka = "ERROR" });
                return Ok(new NetoHonorar { Id=-1});
            }
        }

        [HttpPost("pretraga")]
        [Authorize(Roles ="user")]
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
