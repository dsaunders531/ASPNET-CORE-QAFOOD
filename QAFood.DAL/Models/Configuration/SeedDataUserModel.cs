﻿using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.DAL.Models
{
    /// <summary>
    /// App configuration seed data user model used by Identity.
    /// </summary>
    [NotMapped]
    public class SeedDataUserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
