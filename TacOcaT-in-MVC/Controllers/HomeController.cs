using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TacOcaT_in_MVC.Models;

namespace TacOcaT_in_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ReverseInput()
        {
            TacOcaT model = new();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReverseInput(TacOcaT tacOcaT)
        {
            string inputWord = tacOcaT.InputWord;
            inputWord = Regex.Replace(inputWord.ToLower(), "[^a-z0-9]+","");
            string revWord = "";

            for(int i = inputWord.Length - 1; i >= 0; i--)
            {
                revWord += inputWord[i];
            }
            tacOcaT.RevWord = revWord;
            if (inputWord == revWord)
            {
                tacOcaT.IsTacO = true;
                tacOcaT.Message = $"Well done! {inputWord} is a palindrom...";
            }
            else
            {
                tacOcaT.IsTacO = false;
                tacOcaT.Message = $"Oops! {inputWord} is not a palindrom";
            }
            return View(tacOcaT);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
