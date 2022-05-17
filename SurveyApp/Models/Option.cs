using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models
{
    public class Option
    {
        public Option()
        {
            userAnswers = new List<UserAnswer>();
        }
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public Question question { get; set; }
        public List<UserAnswer> userAnswers { get; set; }
    }
}
