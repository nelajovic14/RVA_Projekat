using RVA_Projekat.Infrastructure;
using RVA_Projekat.Interface;
using RVA_Projekat.Model;
using System.Collections.Generic;
using System.Linq;

namespace RVA_Projekat.Repository
{
    public class PorezRepository : IPorezRepository
    {
        HonorarDbContext _dbContext;

        public PorezRepository(HonorarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Porez Add(Porez entity)
        {
            _dbContext.Porez.Add(entity);
            
            return entity;
        }
        public void SaveAll()
        {
            _dbContext.SaveChanges();
        }

        public Porez Find(int id)
        {
            return _dbContext.Porez.Find(id);

        }

        public List<Porez> GetAll()
        {
            return _dbContext.Porez.ToList<Porez>();
        }

        public void Remove(Porez entity)
        {
            _dbContext.Remove(entity);
        }
    }
}
