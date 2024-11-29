using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace techcareer_web_bootcamp.Pages.Books
{
    public class Create : PageModel
    {
        [BindProperty, Required(ErrorMessage = "The Name of the book is required")]
        public string Title { get; set; } = "";
        [BindProperty]
        public string? Author { get; set; }
        [BindProperty]
        public string? Category { get; set; }
        [BindProperty]
        public int? PublishedYear { get; set; }
        [BindProperty]
        public bool IsAvailable { get; set; }
        [BindProperty]
        public string? Description { get; set; }

        public void OnGet()
        {
        }

        public string ErrorMessage { get; set; } = "";

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            // Save the book to the database
            // Redirect to the list of books
            if (Author == null) Author = "Unknown";
            if (Category == null) Category = "Unknown";
            if (PublishedYear == null) PublishedYear = 0;
            if (Description == null) Description = "No description available";

            try
            {
                string connectionString = "Server=.;Database=projects;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Books (Title, Author, Category, PublishedYear, IsAvailable, Description) VALUES (@Title, @Author, @Category, @PublishedYear, @IsAvailable, @Description)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", Title);
                        command.Parameters.AddWithValue("@Author", Author);
                        command.Parameters.AddWithValue("@Category", Category);
                        command.Parameters.AddWithValue("@PublishedYear", PublishedYear);
                        command.Parameters.AddWithValue("@IsAvailable", IsAvailable);
                        command.Parameters.AddWithValue("@Description", Description);

                        command.ExecuteNonQuery();

                    }

                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Books/Index");

        }
    }
}