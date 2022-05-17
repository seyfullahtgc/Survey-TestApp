using Microsoft.AspNetCore.Mvc;
using SurveyApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.ViewModels
{
    public class StartSurveyViewModel
    {
        public StartSurveyViewModel()
        {
            Options = new List<Option>();
        }

        public int QuestionId { get; set; }
        public Question question { get; set; }
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Please Answer this question")]
        public int OptionId { get; set; }
        public Option option { get; set; }
        public List<Option> Options { get; set; }
    }
}
