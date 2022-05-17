using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public User user { get; set; }
        public int UserId { get; set; }
        public Question question { get; set; }
        public int QuestionId { get; set; }
        public Option option { get; set; }
        public int OptionId { get; set; }
    }
}
