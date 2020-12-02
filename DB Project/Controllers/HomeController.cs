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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.ConnectionString = DB_Project.Properties.Resources.ConnectionString;
        }

        public IActionResult Index()
        {
            FetchData();
            return View(classes);
        }

        private void FetchData()
        {
            if(classes.Count > 0)
            {
                classes.Clear();
            }

            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM Classes";
                dr = com.ExecuteReader();
                while(dr.Read())
                {
                    classes.Add(new Classes() {Class_Title = dr["Class_Title"].ToString()
                    ,Class_Code = dr["Class_Code"].ToString()
                    ,Class_Description = dr["Class_Description"].ToString()

                        });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IActionResult Privacy()
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
