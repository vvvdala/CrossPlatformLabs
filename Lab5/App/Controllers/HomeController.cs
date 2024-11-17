using App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.Controllers
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

        public IActionResult DisplayLab(string lab)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            ViewBag.AccessToken = accessToken;

            if (string.IsNullOrEmpty(accessToken)) 
            {
                return Unauthorized("Ви не увійшли в систему. Будь ласка, увійдіть.");
            }

            switch (lab)
            {
                case ("lab1"):
                    return View("Lab1");

                case ("lab2"):
                    return View("Lab2");

                case ("lab3"):
                    return View("Lab3");

                default:
                    return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> RunLabs(IFormFile inputFile, string pathProject)
        {
            if (inputFile == null || inputFile.Length == 0)
            {
                return BadRequest("Файл не був завантажен.");
            }

            var tempInputFilePath = Path.Combine(Path.GetTempPath(), "INPUT.TXT");
            using (var stream = new FileStream(tempInputFilePath, FileMode.Create))
            {
                await inputFile.CopyToAsync(stream);
            }

            var tempOutputFilePath = Path.Combine(Path.GetTempPath(), "OUTPUT.TXT");

            Environment.SetEnvironmentVariable("INPUT_FILE", tempInputFilePath);
            Environment.SetEnvironmentVariable("OUTPUT_FILE", tempOutputFilePath);

            string result = MyLibrary.LabsLibrary.ExecuteLabs(pathProject);

            if (System.IO.File.Exists(tempOutputFilePath))
            {
                result += "\nРезультати з OUTPUT.TXT:\n";
                result += await System.IO.File.ReadAllTextAsync(tempOutputFilePath);
            }

            ViewData["Result"] = result;

            return View("LabsResult");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
