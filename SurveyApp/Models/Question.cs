using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models
{
    public class Question
    {
        public Question()
        {
            Options = new List<Option>();
            userAnswers = new List<UserAnswer>();
        }
        public int Id { get; set; }

        [Required]
        [Remote(action: "IsQuestionRepeeted", controller: "Admin")]
        public string Text { get; set; }
        public List<Option> Options { get; set; }
        public List<UserAnswer> userAnswers { get; set; }
    }
}
