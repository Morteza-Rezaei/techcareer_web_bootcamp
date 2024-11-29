using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;

namespace techcareer_web_bootcamp.Pages.Books
{
    public class Index : PageModel
    {
        public List<BookInfo> BooksList { get; set; } = [];
        public void OnGet()
        {
            try
            {
                string connectionString = "Server=.;Database=projects;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Books ORDER BY Title ASC";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BookInfo book = new BookInfo();
                                book.Id = Convert.ToInt32(reader["Id"]);
                                book.Title = reader["Title"].ToString();
                                book.Author = reader["Author"].ToString();
                                book.Category = reader["Category"].ToString();
                                book.PublishedYear = Convert.ToInt32(reader["PublishedYear"]);
                                book.IsAvailable = Convert.ToBoolean(reader["IsAvailable"]);
                                book.Description = reader["Description"].ToString();
                                BooksList.Add(book);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
    public class BookInfo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; }
        public string? Description { get; set; }
    }
}