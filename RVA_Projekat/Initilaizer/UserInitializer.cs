using RVA_Projekat.Infrastructure;
using RVA_Projekat.Interface;
using RVA_Projekat.Model;
using System;
using System.Collections.Generic;

namespace RVA_Projekat.Initilaizer
{
    public class UserInitializer:IUserInitializer
    {
        private IUserRepository userRepository;
        private HonorarDbContext _dbContext;

        public UserInitializer(IUserRepository userRepository, HonorarDbContext dbContext)
        {
            this.userRepository = userRepository;
            _dbContext = dbContext;
        }

        public void InitializeUseres()
        {
            List<User> users=userRepository.GetAll();

            if(users.Count > 0)
            {
                if((userRepository.FindByUsername("admin") != null))
                {
                    return;
                }
            }
            userRepository.Add(new User() { Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin") });
            _dbContext.SaveChanges();
        }

    }
}
