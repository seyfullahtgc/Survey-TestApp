using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Data;
using SurveyApp.Models;
using SurveyApp.ViewModels;

namespace SurveyApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyRepository _surveyRepository;
        public SurveyController(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    Name = user.Name
                };
                _surveyRepository.AddUser(newUser);
                return RedirectToAction("StartSurvey", new { id = newUser.Id });
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult StartSurvey(int id)
        {
            ViewBag.UserId = id;
            ViewBag.UserName = _surveyRepository.GetUser(id).Name;
            var model = new List<StartSurveyViewModel>();
            foreach (Question question in _surveyRepository.AllQuestions())
            {
                var startSurveyViewModel = new StartSurveyViewModel
                {
                    QuestionId = question.Id,
                    QuestionText = question.Text,
                    Options = question.Options
                };
                model.Add(startSurveyViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult StartSurvey(List<StartSurveyViewModel> model, int id)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    var userAnswer = new UserAnswer
                    {
                        UserId = id,
                        QuestionId = model[i].QuestionId,
                        OptionId = model[i].OptionId
                    };
                    _surveyRepository.AddUserAnswer(userAnswer);
                }
                return View("Finish");
            }
            return View(model);
        }
    }
}