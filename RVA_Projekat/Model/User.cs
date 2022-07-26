﻿using RVA_Projekat.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVA_Projekat.Model
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string Username { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Password { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public Uloga Role { get; set; }
    }
}
