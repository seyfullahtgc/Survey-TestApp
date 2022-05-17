using SurveyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Data
{
    public interface ISurveyRepository
    {
        Question GetQuestion(int Id);
        IEnumerable<Question> AllQuestions();
        Question AddQuestion(Question question);
        Question UpdateQuestion(Question questionChanges);
        Question DeleteQuestion(int Id);
        Option GetOption(int Id);
        IEnumerable<Option> AllOptions();
        Option AddOption(Option option);
        Option UpdateOption(Option optionChanges);
        Option DeleteOption(int Id);
        User GetUser(int Id);
        IEnumerable<User> AllUsers();
        User AddUser(User user);
        UserAnswer AddUserAnswer(UserAnswer userAnswer);
    }
}
