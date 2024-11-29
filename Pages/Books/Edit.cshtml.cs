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
    public class Edit : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

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

        public string ErrorMessage { get; set; } = "";
        public void OnGet(int Id)
        {
            try
            {
                string connectionString = "Server=.;Database=projects;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Books WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Id = reader.GetInt32(0);
                                Title = reader.GetString(1);
                                Author = reader.GetString(2);
                                Category = reader.GetString(3);
                                PublishedYear = reader.GetInt32(4);
                                IsAvailable = reader.GetBoolean(5);
                                Description = reader.GetString(6);
                            }
                            else
                            {
                                Response.Redirect("/Books/Index");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

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
                    string sql = "UPDATE Books SET Title = @Title, Author = @Author, Category = @Category, PublishedYear = @PublishedYear, IsAvailable = @IsAvailable, Description = @Description WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
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