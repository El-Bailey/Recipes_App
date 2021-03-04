using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Recipes_App.Models;
using Microsoft.Extensions.Configuration;

namespace Recipes_App.Controllers
{
    public class UserRegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserRegistrationController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Username,User_Email,User_Password,Confirm_Password")] UserRegistrationModel userRegistrationModel)
        {
            if (ModelState.IsValid)
            {
                var dt = new DataTable();

                // Check database to see if username already exists.
                using(SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("LocalhostConnection")))
                {
                    sqlConnection.Open();
                    SqlDataAdapter da = new SqlDataAdapter("FetchRecipesUserByUsername", sqlConnection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("Username", userRegistrationModel.Username);
                    da.Fill(dt);
                }

                if(dt.Rows.Count > 0)
                {
                    // Username already exists. 
                    ViewData["Message"] = "Username already exists. Choose a different username.";
                }
                else
                {
                    // Get password hash for User_Password
                    string passwordHash = PasswordEncryptionUsingRFC2898.GetPasswordHash(userRegistrationModel.User_Password);
                    
                    // Add user to database.
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("LocalhostConnection")))
                    {
                        sqlConnection.Open();
                        SqlCommand sqlCommand = new SqlCommand("RecipesUserCreate", sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("Username", userRegistrationModel.Username);
                        sqlCommand.Parameters.AddWithValue("User_Email", userRegistrationModel.User_Email);
                        //sqlCommand.Parameters.AddWithValue("User_Password", userRegistrationModel.User_Password.Trim());
                        sqlCommand.Parameters.AddWithValue("User_Password", passwordHash);
                        int rowsAffected = sqlCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            ViewData["Message"] = userRegistrationModel.Username + "'s Account Created Successfully!";
                        }
                        else
                        {
                            ViewData["Message"] = "Account could not be created.";
                        }
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            
            return View(userRegistrationModel);
        }
        
    }
}
