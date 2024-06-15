using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudMVC.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ProfilePhotoPath { get; set; }
    }
}