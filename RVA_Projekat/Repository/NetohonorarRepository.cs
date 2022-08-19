using RVA_Projekat.Infrastructure;
using RVA_Projekat.Interface;
using RVA_Projekat.Model;
using System.Collections.Generic;
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
    }
}
