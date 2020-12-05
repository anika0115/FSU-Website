using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DB_Project.Models;
using System.Data.SqlClient;
using System.Text.Encodings.Web;

namespace DB_Project.Controllers
{
    public class ProfessorsController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<Classes> classes = new List<Classes>();
        List<Professors> ProfessorList = new List<Professors>();
        List<Tutors> Tutors = new List<Tutors>();
        List<Textbooks> Textbooks = new List<Textbooks>();
        private readonly ILogger<HomeController> _logger;

        public ProfessorsController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.ConnectionString = DB_Project.Properties.Resources.ConnectionString;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProfessorsPage(int rating, string name, string email, string location)
        {
            FetchProfData(rating, name, email, location);
            return View(ProfessorList);
        }

        private void FetchProfData(double rating = 0, string name = "*", string email = "*", string location = "*")
        {
            if (ProfessorList.Count > 0)
            {
                ProfessorList.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM Professors WHERE Average_Rating >" + rating + " AND Professor_Name LIKE '" + name + "%' AND Professor_Email LIKE '" + email + "%' AND Office_Location LIKE '" + location + "%'";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ProfessorList.Add(new Professors()
                    {
                        Professor_Email = dr["Professor_Email"].ToString()
                    ,
                        Professor_Name = dr["Professor_Name"].ToString()
                    ,
                        Office_Location = dr["Office_Location"].ToString()
                    ,
                        Professor_Website = dr["Professor_Website"].ToString()
                    ,
                        Average_Rating = (double)dr["Average_Rating"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // GET: Professsors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            return View(ProfessorList);

        }

    }
}
