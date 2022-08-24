using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using RVA_Projekat.Dto;
using RVA_Projekat.Model;
using RVA_Projekat.Enums;
using RVA_Projekat.Interface.InterfaceUser;

namespace RVA_Projekat.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
      
        private readonly IConfigurationSection _secretKey;

        public UserService(IConfiguration config,IUserRepository userRepository)
        {
            _secretKey = config.GetSection("SecretKey");
            _userRepository = userRepository;
        }

        public string Login(UserDto dto)
        { 
            User user = _userRepository.FindByUsername(dto.Username);
            if (user == null)
                return null;

            if (BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))//Uporedjujemo hes pasvorda iz baze i unetog pasvorda
            {
                List<Claim> claims = new List<Claim>();

                User u = Get(dto);
                if (u.Role == Enums.Uloga.ADMIN)
                    claims.Add(new Claim(ClaimTypes.Role, "admin")); //Add user type to claim
                else if(u.Role==Enums.Uloga.KORISNIK)
                    claims.Add(new Claim(ClaimTypes.Role, "korisnik"));


                claims.Add(new Claim(ClaimTypes.Role, "user"));

                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:44386", //url servera koji je izdao token
                    claims: claims, //claimovi
                    expires: DateTime.Now.AddYears(1), //vazenje tokena u minutama
                    signingCredentials: signinCredentials //kredencijali za potpis
                );
                string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                
                return tokenString;
            }
            else
            {
                return null;
            }
        }

        public User Edit(UserEditdto dto)
        {
            User user = _userRepository.FindByUsername(dto.Username);
            if (user == null)
                return null;
          
            user.Name = dto.Name;
            user.LastName = dto.LastName;
            _userRepository.Edit(user);
            return user;
           
        }

        public User DodajEntitet(UserRegisterDto dto)
        {
            User user = _userRepository.FindByUsername(dto.Username);
            if (user != null)
                return null;
            Uloga u;
            if (dto.Uloga == "ADMIN")
                u = Uloga.ADMIN;
            else
            {
                u = Uloga.KORISNIK;
            }
            User newUser = new User() { Username = dto.Username, Password = BCrypt.Net.BCrypt.HashPassword(dto.Password), Name = dto.Name, LastName = dto.Lastname, Role = u };
            try
            {
               return _userRepository.Add(newUser);
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }

        public User Get(UserDto dto)
        {
            User user = _userRepository.FindByUsername(dto.Username);
            
            return user;
        }
    }
}
