using RVA_Projekat.Infrastructure;
using RVA_Projekat.Interface;
using RVA_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace RVA_Projekat.Repository
{
    public class UserRepository : IUserRepository
    {
        private HonorarDbContext _dbContext;
        public UserRepository(HonorarDbContext honorarDbContext)
        {
            _dbContext = honorarDbContext;
        }
        public User Add(User entity)
        {
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public User Find(int id)
        {
            User u = _dbContext.Users.Find(id);
            return u;
        }

        public User FindByUsername(string username)
        {
            return  _dbContext.Users.SingleOrDefault<User>(u => String.Equals(u.Username, username));
        }

        public List<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public void Remove(User entity)
        {
            _dbContext.Users.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
