using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Recipes_App.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipes_App.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IConfiguration _configuration;
        public RecipeController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Recipe
        public IActionResult Index()
        {
            var dt = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("LocalhostConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter("RecipeViewAll", sqlConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }
            return View(dt);
        }


        // GET: Recipe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Ingredients,Instructions")] RecipeViewModel recipeViewModel)
        {
            //RecipeViewModel recipeViewModel = new RecipeViewModel();
            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("LocalhostConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("RecipeCreate", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.Parameters.AddWithValue("pkid", recipeViewModel.pkid);
                    sqlCommand.Parameters.AddWithValue("Title", recipeViewModel.Title);
                    sqlCommand.Parameters.AddWithValue("Ingredients", recipeViewModel.Ingredients.Trim());
                    sqlCommand.Parameters.AddWithValue("Instructions", recipeViewModel.Instructions.Trim());
                    sqlCommand.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipeViewModel);
        }

        // GET: Recipe/Edit/5
        public IActionResult Edit(int? id)
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel();
            recipeViewModel = FetchRecipeByPkid(id);
            return View(recipeViewModel);
        }

        // POST: Recipe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("pkid,Title,Ingredients,Instructions")] RecipeViewModel recipeViewModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("LocalhostConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("RecipeEdit", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("pkid", recipeViewModel.pkid);
                    sqlCommand.Parameters.AddWithValue("Title", recipeViewModel.Title);
                    sqlCommand.Parameters.AddWithValue("Ingredients", recipeViewModel.Ingredients.Trim());
                    sqlCommand.Parameters.AddWithValue("Instructions", recipeViewModel.Instructions.Trim());
                    sqlCommand.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Recipe/Details/5
        public IActionResult Details(int? id)
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel();
            recipeViewModel = FetchRecipeByPkid(id);
            return View(recipeViewModel);
        }

        // GET: Recipe/Delete/5
        public IActionResult Delete(int? id)
        {
            RecipeViewModel recipeViewModel = FetchRecipeByPkid(id);
            return View(recipeViewModel);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("LocalhostConnection")))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("RecipeDeleteByPkid", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("pkid", id);
                sqlCommand.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public RecipeViewModel FetchRecipeByPkid(int? id)
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("LocalhostConnection")))
            {
                var dt = new DataTable();
                sqlConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter("RecipeViewByPkid", sqlConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("pkid", id);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    recipeViewModel.pkid = int.Parse(dt.Rows[0]["pkid"].ToString());
                    recipeViewModel.Title = dt.Rows[0].Field<string>("Title");
                    recipeViewModel.Ingredients = dt.Rows[0].Field<string>("Ingredients");
                    recipeViewModel.Instructions = dt.Rows[0].Field<string>("Instructions");
                }
                return recipeViewModel;
            }
        }
    }
}
