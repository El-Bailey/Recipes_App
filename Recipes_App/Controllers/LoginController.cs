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

        [HttpPost]
        public IActionResult Login([Bind("Username,User_Password")] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var dt = new DataTable();

                // Check database to see if credentials match.
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("LocalhostConnection")))
                {
                    sqlConnection.Open();
                    SqlDataAdapter da = new SqlDataAdapter("CheckRecipesUserCredentials", sqlConnection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("Username", loginViewModel.Username);
                    da.SelectCommand.Parameters.AddWithValue("User_Password", loginViewModel.User_Password);
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    int.TryParse(dt.Rows[0][0].ToString(), out int pkid);
                    // Check whether credentials matched
                    if (pkid > 0)
                    {
                        // Credentials matched
                        ViewData["Message"] = "Login Successful!";
                        return RedirectToAction("Index", "Recipe");
                        // redirect to recipe list here
                    }
                    else //(dt.Rows[0].Field<int>("pkid") == -1)
                    {
                        ViewData["Message"] = "Invalid Credentials";
                    }

                }
                else
                {
                    ViewData["Message"] = "Error occured when checking credentials.";
                }
                //return RedirectToAction(nameof(Index));
            }

            //return View();
            return View(loginViewModel);
            //return RedirectToAction("Index","Recipe");
            //return View(new RecipeViewModel );
        }
    }
}
