using RVA_Projekat.Infrastructure;
using RVA_Projekat.Interface;
using RVA_Projekat.Model;
using System.Collections.Generic;
using System.Linq;

namespace RVA_Projekat.Repository
{
    public class BrutoHonorarRepository : IBrutoHonorarRepository
    {
        private HonorarDbContext _dbContext;
        public BrutoHonorarRepository(HonorarDbContext honorarDbContext)
        {
            _dbContext = honorarDbContext;
        }
        public BrutoHonorar Add(BrutoHonorar entity)
        {
            _dbContext.BrutoHonorars.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public BrutoHonorar Find(int id)
        {
            return _dbContext.BrutoHonorars.Find(id);
        }

        public List<BrutoHonorar> GetAll()
        {
            return _dbContext.BrutoHonorars.ToList<BrutoHonorar>();
        }

        public void Remove(BrutoHonorar entity)
        {
            _dbContext.BrutoHonorars.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
