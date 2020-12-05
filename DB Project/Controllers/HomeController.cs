using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DB_Project.Models;
using System.Data.SqlClient;
using System.Text.Encodings.Web;

namespace DB_Project.Controllers
{
    public class HomeController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<ClassesTaught> classes = new List<ClassesTaught>();
        List<Professors> Professors = new List<Professors>();
        List<Tutors> Tutors = new List<Tutors>();
        List<Textbooks> Textbooks = new List<Textbooks>();
        List<Events> EventList = new List<Events>();
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

        private void FetchEventData()
        {
            if (classes.Count > 0)
            {
                classes.Clear();
            }


            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM Events";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    EventList.Add(new Events()
                    {
                        Event_Title = dr["Event_Title"].ToString()
                    ,
                        Event_Date = (DateTime)dr["Event_Date"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        private void FetchClassData()
        {
            if (classes.Count > 0)
            {
                classes.Clear();
            }

            if (true)
            {
                try
                {
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "SELECT * FROM Classes";
                    dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        classes.Add(new ClassesTaught()
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
            else
            {
                try
                {
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "SELECT Class_Title, Class_Code, Class_Description, Professor_Name FROM Professor_Instructs, Classes, Professors WHERE Professor_Instructs.Professor_Email = Professors.Professor_Email AND Professor_Instructs.Class_Code = Classes.Class_Code";
                    dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        classes.Add(new ClassesTaught()
                        {
                            Class_Title = dr["Class_Title"].ToString()
                        ,
                            Class_Code = dr["Class_Code"].ToString()
                        ,
                            Class_Description = dr["Class_Description"].ToString()
                        ,
                            Professor_Name = dr["Professor_Name"].ToString()

                        });
                    }
                    con.Close();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        private void FetchProfData(int rating = 0)
        {
            if (Professors.Count > 0)
            {
                Professors.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM Professors WHERE Average_Rating >" + rating;
                dr = com.ExecuteReader();
                //while (dr.Read())
                //{
                //    Professors.Add(new Professors()
                //    {
                //        Professor_Email = dr["Professor_Email"].ToString()
                //    ,
                //        Professor_Name = dr["Professor_Name"].ToString()
                //    ,
                //        Office_Location = dr["Office_Location"].ToString()
                //    ,
                //        Professor_Website = dr["Professor_Website"].ToString()
                //    ,
                //        Average_Rating = (double)dr["Average_Rating"]
                //    });
                //}
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

        public IActionResult ProfessorsPage(int rating)
        {
            FetchProfData(rating);
            return View(Professors);
        }

        //public IActionResult ProfessorsPage(string name, int id = 1)
        //{

        //    ViewData["Message"] = "Hello " + name;
        //    ViewData["NumTimes"] = id;
        //    return View();
        //}

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
            FetchEventData();
            return View(EventList);
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
