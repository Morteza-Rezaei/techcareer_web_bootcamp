using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace techcareer_web_bootcamp.Pages.Books
{
    public class Delete : PageModel
    {
        public string Title { get; set; } = "";

        public void OnGet(int Id)
        {
            try
            {
                string connectionString = "Server=.;Database=projects;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Title FROM Books WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Title = reader.GetString(0);
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
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void OnPost(int Id)
        {
            deleteCustomer(Id);
            Response.Redirect("/Books/Index");
        }

        public void deleteCustomer(int Id)
        {
            try
            {
                string connectionString = "Server=.;Database=projects;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM Books WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not delete book: " + e.Message);
            }
        }
    }
}