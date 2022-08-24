using RVA_Projekat.Infrastructure;
using RVA_Projekat.Interface.InterfaceUser;
using RVA_Projekat.Model;
using System;
using System.Collections.Generic;

namespace RVA_Projekat.Initilaizer
{
    public class UserInitializer:IUserInitializer
    {
        private IUserRepository userRepository;

        public UserInitializer(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void InitializeUseres()
        {
            List<User> users=userRepository.GetAll();

            if(users.Count > 0)
            {
                return;
            }
            userRepository.Add(new User() { Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin"),Name="Admin",LastName="Admin",Role=Enums.Uloga.ADMIN});

        }

    }
}
