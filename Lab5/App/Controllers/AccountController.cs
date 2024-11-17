using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AccountController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public IActionResult Authorize()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string fullname, string email, string password, string phonenumber)
        {
            var requestBody = new
            {
                client_id = _configuration["Auth0:ClientId"],
                email,
                password,
                connection = "Username-Password-Authentication",
                username,
                user_metadata = new
                {
                    fullname,
                    phonenumber
                }
            };

            var response = await _httpClient.PostAsJsonAsync($"https://{_configuration["Auth0:Domain"]}/dbconnections/signup", requestBody);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return BadRequest($"Помилка під час реєстрації: {errorContent}");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Юзернейм та пароль обов'язкові.");
            }

            var requestBody = new
            {
                client_id = _configuration["Auth0:ClientId"],
                client_secret = _configuration["Auth0:ClientSecret"],
                audience = _configuration["Auth0:Audience"],
                grant_type = "password",
                username = username,
                password = password,
                scope = "openid profile email"
            };

            var response = await _httpClient.PostAsJsonAsync($"https://{_configuration["Auth0:Domain"]}/oauth/token", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                

                HttpContext.Session.SetString("AccessToken", tokenResponse.access_token);

                return RedirectToAction("Index", "Home");
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return BadRequest($"Не вдалося ввійти: {errorContent}");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized("Ви не увійшли в систему. Будь ласка, увійдіть.");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"https://{_configuration["Auth0:Domain"]}/userinfo");

            if (response.IsSuccessStatusCode) 
            {
                var userProfile = await response.Content.ReadFromJsonAsync<UserProfile>();
                return View(userProfile);
            }

            return BadRequest("Не вдалося отримати інформацію про користувача.");
        }
    }

    public class UserProfile
    {
        public string Sub { get; set; } 
        public string Name { get; set; } 
        public string Email { get; set; } 
        public string Picture { get; set; } 
        public UserMetadata UserMetadata { get; set; } 
    }

    public class UserMetadata
    {
        public string Fullname { get; set; } 
        public string Phonenumber { get; set; } 
    }

    public class TokenResponse
    {
        public string access_token { get; set; }
        public string id_token { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }
}
