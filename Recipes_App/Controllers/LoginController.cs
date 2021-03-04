using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Recipes_App.Models;
using Microsoft.Extensions.Configuration;

namespace Recipes_App.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("LoggedIn");

            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Login([Bind("Username,User_Password")] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var dt = new DataTable();

                // Check database to see if username already exists.
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("LocalhostConnection")))
                {
                    sqlConnection.Open();
                    SqlDataAdapter da = new SqlDataAdapter("FetchRecipesUserByUsername", sqlConnection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("Username", loginViewModel.Username);
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    string passwordHash = dt.Rows[0]["User_Password"].ToString();

                    if (PasswordEncryptionUsingRFC2898.CheckPassword(loginViewModel.User_Password, passwordHash))
                    {
                        // Credentials matched.
                        // Add key to Session to flag user as logged in.
                        HttpContext.Session.Set("LoggedIn", new byte[] { 0x1 });

                        // Redirect to All Recipes List
                        return RedirectToAction("Index", "Recipe");
                    }
                    else
                    {
                        ViewData["Message"] = "Password Incorrect";
                    }
                }
                else
                {
                    ViewData["Message"] = "Error occured when checking credentials.";
                }
            }

            return View(loginViewModel);
        }
    }
}
