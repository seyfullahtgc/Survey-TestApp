using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Models;

namespace SurveyApp.Data
{
    public class SQLSurveyRepository : ISurveyRepository
    {
        private readonly AppDbContext _context;
        public SQLSurveyRepository(AppDbContext context)
        {
            _context = context;
        }
        public Option AddOption(Option option)
        {
            _context.Options.Add(option);
            _context.SaveChanges();
            return option;
        }

        public Question AddQuestion(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
            return question;
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public UserAnswer AddUserAnswer(UserAnswer userAnswer)
        {
            _context.UserAnswers.Add(userAnswer);
            _context.SaveChanges();
            return userAnswer;
        }

        public IEnumerable<Option> AllOptions()
        {
            return _context.Options.Include(p => p.question).Include(o => o.userAnswers);
        }

        public IEnumerable<Question> AllQuestions()
        {
            return _context.Questions.Include(q => q.Options).Include(e => e.userAnswers);
        }

        public IEnumerable<User> AllUsers()
        {
            return _context.Users.Include(u => u.userAnswers);
        }

        public Option DeleteOption(int Id)
        {
            Option option = _context.Options.Include(o => o.question).FirstOrDefault(m => m.Id == Id);
            if (option != null)
            {
                _context.Options.Remove(option);
                _context.SaveChanges();
            }
            return option;
        }

        public Question DeleteQuestion(int Id)
        {
            Question question = _context.Questions.Find(Id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                _context.SaveChanges();
            }
            return question;
        }

        public Option GetOption(int Id)
        {
            return _context.Options.Include(o => o.question).FirstOrDefault(m => m.Id == Id);
        }

        public Question GetQuestion(int Id)
        {
            return _context.Questions.Include(q => q.Options).Include(e => e.userAnswers).FirstOrDefault(m => m.Id == Id);
        }

        public User GetUser(int Id)
        {
            return _context.Users.Include(u => u.userAnswers).FirstOrDefault(m => m.Id == Id);
        }

        public Option UpdateOption(Option optionChanges)
        {
            var option = _context.Options.Attach(optionChanges);
            option.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return optionChanges;
        }

        public Question UpdateQuestion(Question questionChanges)
        {
            var question = _context.Questions.Attach(questionChanges);
            question.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return questionChanges;
        }
    }
}
