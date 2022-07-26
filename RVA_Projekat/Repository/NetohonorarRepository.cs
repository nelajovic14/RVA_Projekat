﻿using RVA_Projekat.Infrastructure;
using RVA_Projekat.Interface.Neto;
using RVA_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RVA_Projekat.Repository
{
    public class NetohonorarRepository : INetohonorarRepository
    {
        private HonorarDbContext _dbContext;
        public NetohonorarRepository(HonorarDbContext honorarDbContext)
        {
            _dbContext = honorarDbContext;
        }
        public NetoHonorar Add(NetoHonorar entity)
        {
            _dbContext.NetoHonorars.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public NetoHonorar Edit(NetoHonorar nh)
        {
            _dbContext.Update(nh);
            _dbContext.SaveChanges();
            return nh;
        }

        public NetoHonorar Find(int id)
        {
            NetoHonorar nh= _dbContext.NetoHonorars.Find(id);
            return nh;
        }

        public List<NetoHonorar> GetAll()
        {
            return _dbContext.NetoHonorars.ToList<NetoHonorar>();
            //return new List<NetoHonorar>();
        }

        public void Remove(NetoHonorar entity)
        {
            _dbContext.NetoHonorars.Remove(entity);
            _dbContext.SaveChanges();
            
        }
        public DateTime GetLastChange(int id)
        {
            NetoHonorar nh = Find(id);
            return nh.new_date;
        }
    }
}
