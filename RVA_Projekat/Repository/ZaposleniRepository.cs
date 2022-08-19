using RVA_Projekat.Infrastructure;
using RVA_Projekat.Interface;
using RVA_Projekat.Model;
using System.Collections.Generic;
using System.Linq;

namespace RVA_Projekat.Repository
{
    public class ZaposleniRepository : IZaposleniRepository
    {
        HonorarDbContext _dbContext;

        public ZaposleniRepository(HonorarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Zaposleni Add(Zaposleni entity)
        {
            _dbContext.Zaposlenis.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Zaposleni Find(int id)
        {
            Zaposleni z= _dbContext.Zaposlenis.Find(id);
            return z;
        }

        public List<Zaposleni> GetAll()
        {
            return _dbContext.Zaposlenis.ToList<Zaposleni>();
        }

        public void Remove(Zaposleni entity)
        {
            _dbContext.Zaposlenis.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
