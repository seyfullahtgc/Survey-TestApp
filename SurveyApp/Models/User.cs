using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models
{
    public class User
    {
        public User()
        {
            userAnswers = new List<UserAnswer>();
        }
        public int Id { get; set; }

        [Required]
        [Remote(action: "IsUserNameExist", controller: "Admin")]
        [Display(Name = "Enter Your Name")]
        public string Name { get; set; }
        public List<UserAnswer> userAnswers { get; set; }
    }
}
