﻿using RVA_Projekat.Infrastructure;
using RVA_Projekat.Interface;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Initilaizer
{
    public class ZaposleniInitializer : IZaposleniInitializer
    {
        IZaposleniRepository repository;
        IBrutoHonorarRepository honorarRepository;
        HonorarDbContext _dbContext;

        public ZaposleniInitializer(IZaposleniRepository repository, HonorarDbContext dbContext,IBrutoHonorarRepository brutoHonorarRepository)
        {
            this.repository = repository;
            _dbContext = dbContext;
            honorarRepository = brutoHonorarRepository;
        }

        public void InitializeUseres()
        {
            List<Zaposleni> zaposleni = repository.GetAll();
            Zaposleni zaposleni1 = new Zaposleni() { Ime = "Nela", GodineIskustva = 5 };
            Zaposleni zaposleni2 = new Zaposleni() { Ime = "Ana", GodineIskustva = 5 };
            Zaposleni zaposleni3 = new Zaposleni() { Ime = "Maja", GodineIskustva = 5 };
            if (zaposleni.Count > 3)
            {
                    return;
                
            }
            foreach (BrutoHonorar bh in honorarRepository.GetAll())
            {
                zaposleni1.BrutoHonorarId = bh.Id;
                zaposleni2.BrutoHonorarId = bh.Id;
                zaposleni3.BrutoHonorarId = bh.Id;
                break;
            }
            repository.Add(zaposleni1);
            repository.Add(zaposleni2);
            repository.Add(zaposleni3);
            _dbContext.SaveChanges();
        }
    }
}