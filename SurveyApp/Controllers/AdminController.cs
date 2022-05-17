using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Data;
using SurveyApp.Models;

namespace SurveyApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISurveyRepository _surveyRepository;
        public AdminController(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public IActionResult Index()
        {
            var allQuestions = _surveyRepository.AllQuestions();
            return View(allQuestions);
        }

        public IActionResult CreateQuestion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                Question newQuestion = new Question
                {
                    Text = question.Text
                };
                _surveyRepository.AddQuestion(newQuestion);
                return RedirectToAction("CreateOption", new { id = newQuestion.Id });
            }
            return View(question);
        }

        public IActionResult CreateOption(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOption(int id, Option option)
        {
            if (ModelState.IsValid)
            {
                Option newOption = new Option
                {
                    Text = option.Text,
                    QuestionId = id,
                    question = option.question
                };
                _surveyRepository.AddOption(newOption);
                return RedirectToAction(nameof(Index));
            }
            return View(option);
        }

        public IActionResult EditQuestion(int id)
        {
            var question = _surveyRepository.GetQuestion(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditQuestion(int id, Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Question newQuestion = _surveyRepository.GetQuestion(id);
                    newQuestion.Text = question.Text;
                    _surveyRepository.UpdateQuestion(newQuestion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        public IActionResult EditOption(int id)
        {
            var option = _surveyRepository.GetOption(id);
            if (option == null)
            {
                return NotFound();
            }
            return View(option);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOption(int id, Option option)
        {
            if (id != option.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Option newOption = _surveyRepository.GetOption(id);
                    newOption.Text = option.Text;
                    _surveyRepository.UpdateOption(newOption);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionExists(option.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(option);
        }

        public IActionResult DeleteQuestion(int id)
        {
            var question =_surveyRepository.GetQuestion(id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        [HttpPost, ActionName("DeleteQuestion")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteQuestionConfirmed(int id)
        {
            _surveyRepository.DeleteQuestion(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteOption(int id)
        {
            var option = _surveyRepository.GetOption(id);
            if (option == null)
            {
                return NotFound();
            }

            return View(option);
        }

        [HttpPost, ActionName("DeleteOption")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteOptionConfirmed(int id)
        {
            _surveyRepository.DeleteOption(id);
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _surveyRepository.AllQuestions().Any(e => e.Id == id);
        }

        private bool OptionExists(int id)
        {
            return _surveyRepository.AllOptions().Any(e => e.Id == id);
        }

        public IActionResult AllUsers()
        {
            var allUsers = _surveyRepository.AllUsers(); 
            return View(allUsers);
        }

        public IActionResult statistics()
        {
            var questions = _surveyRepository.AllQuestions(); 
            return View(questions);
        }

        public IActionResult IsQuestionRepeeted(string text)
        {
            var question = _surveyRepository.AllQuestions().Where(a => a.Text == text).FirstOrDefault();
            if (question == null)
            {
                return Json(true);
            }
            else
            {
                return Json("This Question is already exist in the survey you can edit it.");
            }
        }

        public IActionResult IsUserNameExist(string name)
        {
            var question = _surveyRepository.AllUsers().Where(a => a.Name == name).FirstOrDefault();
            if (question == null)
            {
                return Json(true);
            }
            else
            {
                return Json("This Name is already exist, Please choose another Name.");
            }
        }
    }
}