using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Data.Common;
using ado.net.Models;

namespace ado.net.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();  
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string user,string password)
        {
            string strCon = @"Data Source=WINDOWS-11\SQLEXPRESS;Initial Catalog=qlBh;Integrated Security=True";
            var connection = new SqlConnection(strCon);
            connection.Open();
               SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            //cmd.CommandText = "SELECT*FROM [TkAdmin] WHERE tenDN='" + user+"' AND Pass='"+password+"'";
            cmd.CommandText = "select*from [TkAdmin] where tenDN=@user and Pass=@password";
            SqlParameter parameter = new SqlParameter();
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Session["ad"] = user;
                return RedirectToAction("Index","QLSP");
            }
            
            else
            {
                return View();
            }
            connection.Close();

        }
        public ActionResult test()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Home(string tim) {
            string strCon = @"Data Source=WINDOWS-11\SQLEXPRESS;Initial Catalog=qlBh;Integrated Security=True";
            var connection = new SqlConnection(strCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select*from sanPham where tenSP=@tim";
            SqlParameter parameter1 = new SqlParameter();
            cmd.Parameters.AddWithValue("tim", tim);
            SqlDataReader red= cmd.ExecuteReader();
            if (red.Read())
            {
                ViewBag.ten = tim;
                return View();
            }

            return View();
                }
    }
}