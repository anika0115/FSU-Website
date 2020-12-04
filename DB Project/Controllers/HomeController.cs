using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DB_Project.Models;
using System.Data.SqlClient;

namespace DB_Project.Controllers
{
    public class HomeController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<Classes> classes = new List<Classes>();
        List<Professors> Professors = new List<Professors>();
        List<Tutors> Tutors = new List<Tutors>();
        List<Textbooks> Textbooks = new List<Textbooks>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.ConnectionString = DB_Project.Properties.Resources.ConnectionString;
        }

        public IActionResult Index()
        {
            FetchData();
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }


        private void FetchData()
        {
        }


        private void FetchClassData()
        {
            if (classes.Count > 0)
            {
                classes.Clear();
            }


            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM Classes";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    classes.Add(new Classes()
                    {
                        Class_Title = dr["Class_Title"].ToString()
                    ,
                        Class_Code = dr["Class_Code"].ToString()
                    ,
                        Class_Description = dr["Class_Description"].ToString()

                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void FetchProfData()
        {
            if (Professors.Count > 0)
            {
                Professors.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM Professors";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    Professors.Add(new Professors()
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

        private void FetchTutorData()
        {
            if (Tutors.Count > 0)
            {
                Tutors.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM Tutors";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    Tutors.Add(new Tutors()
                    {
                        Tutor_Name = dr["Tutor_Name"].ToString()
                    ,
                        Tutor_Email = dr["Tutor_Email"].ToString()
                    ,
                        Tutor_Price = (double)dr["Tutor_Price"]
                    ,
                        Tutor_Description = dr["Tutor_Description"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void FetchTextBookData()
        {
            if (Textbooks.Count > 0)
            {
                Tutors.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM Textbooks";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    Textbooks.Add(new Textbooks()
                    {
                        ISBN = dr["ISBN"].ToString()
                    ,
                        Textbook_Price = (double)dr["Textbook_Price"]
                    ,
                        Textbook_Title = dr["Textbook_Title"].ToString()
                    ,
                        Textbook_Author = dr["Textbook_Author"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public IActionResult ProfessorsPage()
        {
            FetchProfData();
            return View(Professors);
        }


        public IActionResult ClassesPage()
        {
            FetchClassData();
            return View(classes);
        }

        public IActionResult TutorsPage()
        {
            FetchTutorData();
            return View(Tutors);
        }

        public IActionResult TextbookExchangePage()
        {
            FetchTextBookData();
            return View(Textbooks);
        }

        public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult testPage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
