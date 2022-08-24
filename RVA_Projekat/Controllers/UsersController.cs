using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RVA_Projekat.Dogadjaji;
using RVA_Projekat.Dto;
using RVA_Projekat.Infrastructure;
using RVA_Projekat.Interface.InterfaceUser;
using RVA_Projekat.Model;

namespace RVA_Projekat.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userServis;
        private readonly ILoggerManager logger;
        public UsersController(IUserService userServis, ILoggerManager logger)
        {
            this.userServis = userServis;
            this.logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto dto)
        {
            if (userServis.Login(dto) == null)
            {
                logger.LogInformation(new Dogadjaj { korisnik = "unknown", poruka = "ERROR", dogadjaj = "Log in" });
            }
            else
            {
                logger.LogInformation(new Dogadjaj { korisnik = dto.Username, poruka = "SUCCES", dogadjaj = "Log in" });
            }
            return Ok(userServis.Login(dto));
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] UserDto dto)
        {
            
                logger.LogInformation(new Dogadjaj { korisnik = dto.Username, poruka = "SUCCES", dogadjaj = "Log out" });
            
            return Ok();
        }

        [HttpPost("register")]
        [Authorize(Roles = "admin")]
        public IActionResult Register([FromBody] UserRegisterDto dto)
        {
            if (userServis.DodajEntitet(dto)!=null)
            {
                logger.LogInformation(new Dogadjaj { korisnik = dto.Username, poruka = "SUCCES", dogadjaj = "Register" });
                return Ok();
            }
            else
            {
                logger.LogInformation(new Dogadjaj { korisnik = dto.Korisnik, poruka = "ERROR", dogadjaj = "Register" });
                return BadRequest();
            }        
        }

        [HttpPut]
        [Authorize(Roles = "user")]
        public IActionResult Put([FromBody] UserEditdto dto)
        {
            User u =userServis.Edit(dto);
            if(u == null)
            {
                logger.LogInformation(new Dogadjaj { korisnik = dto.Username, poruka = "ERROR", dogadjaj = "Edit" });
            }
            else
            {
                logger.LogInformation(new Dogadjaj { korisnik = dto.Username, poruka = "SUCCES", dogadjaj = "Edit" });
            }
            return Ok();
        }
        [HttpPost("getUser")]
        [Authorize(Roles = "user")]
        public IActionResult getUser([FromBody] UserDto dto)
        {
            User u = userServis.Get(dto);
            UserRegisterDto pom;
            if (u.Role == Enums.Uloga.ADMIN)
                pom = new UserRegisterDto { Lastname = u.LastName, Name = u.Name, Username = u.Username, Password = u.Password, Uloga = "ADMIN" };
            else
            {
                pom = new UserRegisterDto { Lastname = u.LastName, Name = u.Name, Username = u.Username, Password = u.Password, Uloga = "KORISNIK" };
            }
            return Ok(pom);
        }
    }
}
