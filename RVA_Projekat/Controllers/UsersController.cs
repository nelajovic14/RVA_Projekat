using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RVA_Projekat.Dto;
using RVA_Projekat.Infrastructure;
using RVA_Projekat.Interface;
using RVA_Projekat.Model;

namespace RVA_Projekat.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userServis;
        public UsersController(IUserService userServis)
        {
            this.userServis = userServis;
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDto dto)
        {
            return Ok(userServis.Login(dto));
        }
       


    }
}
