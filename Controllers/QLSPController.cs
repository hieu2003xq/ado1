using ado.net.Models;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace ado.net.Controllers
{
    public class QLSPController : Controller
    {
        // GET: QLSP
        public ActionResult Index()
        {
            string strCon = @"Data Source=WINDOWS-11\SQLEXPRESS;Initial Catalog=qlBh;Integrated Security=True";
            var connection = new SqlConnection(strCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select*from sanPham";
            var reader=cmd.ExecuteReader();
            List<SP> lst=new List<SP>();
            while (reader.Read())
            {
                int masp = Convert.ToInt32(reader["maSP"]);
                string tensp = reader["tenSP"].ToString();
                string giaban = reader["giaBan"].ToString();
                string hinhAnh = reader["hinhAnh"].ToString();
                SP a=new SP()
                {
                    maSP = masp,
                    tenSP = tensp,
                    giaBan = giaban,
                    hinhAnh=hinhAnh,
                };
                lst.Add(a);
            }
            return View(lst);
        }
       
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult themSP(SP data)
        {
            var a = Sanitizer.GetSafeHtmlFragment(data.tenSP);
            if (!string.IsNullOrEmpty(a))
            {
                string strCon = @"Data Source=WINDOWS-11\SQLEXPRESS;Initial Catalog=qlBh;Integrated Security=True";
                var connection = new SqlConnection(strCon);
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                string sql = "insert into sanPham (maSP,tenSP,giaBan,hinhAnh) values(@ma,@ten,@gia,@hinhAnh)";
                cmd.CommandText = sql;
                try
                {
                    cmd.Parameters.Add("@ma", System.Data.SqlDbType.Int).Value = data.maSP;
                    cmd.Parameters.Add("@ten", System.Data.SqlDbType.NVarChar).Value = data.tenSP;
                    cmd.Parameters.Add("@gia", System.Data.SqlDbType.Money).Value = data.giaBan;
                    cmd.Parameters.Add("@hinhANh", System.Data.SqlDbType.NVarChar).Value = "0";

                    int ret = cmd.ExecuteNonQuery();
                    

                    return Json(new { success = true });
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return Json(new { success = false });
                }
                
            }return RedirectToAction("Index");
           
        }
        public ActionResult xoa(int id)
        {
            string strCon = @"Data Source=WINDOWS-11\SQLEXPRESS;Initial Catalog=qlBh;Integrated Security=True";
            var connection = new SqlConnection(strCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.Text;
            string sql = "delete from sanPham where maSP=@ma" ;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@ma", System.Data.SqlDbType.Int).Value = id;
            int ret = cmd.ExecuteNonQuery();
            if(ret>0)
            {
                MessageBox.Show("xoa thang cong");
            }
            else
            {
                MessageBox.Show("xoa khong thang cong");
            }
            return RedirectToAction("thu", "QLSP");
        }
        [HttpGet]
        public ActionResult sua(int id)
        {
            string strCon = @"Data Source=WINDOWS-11\SQLEXPRESS;Initial Catalog=qlBh;Integrated Security=True";
            var connection = new SqlConnection(strCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select*from sanPham where maSP=@ma";
            cmd.Parameters.AddWithValue("@ma", id);
            var reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                int masp = Convert.ToInt32(reader["maSP"]);
                string tensp = reader["tenSP"].ToString();
                string giaban = reader["giaBan"].ToString();
                string hinhAnh = reader["hinhAnh"].ToString();
                SP a = new SP()
                {
                    maSP = masp,
                    tenSP = tensp,
                    giaBan = giaban,
                    hinhAnh = hinhAnh,
                };
                return View(a);
            }
            return RedirectToAction("Index", "QLSP");
           
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult sua(int maSP, string tenSP, string giaBan, string hinhAnh)
        {
            string strCon = @"Data Source=WINDOWS-11\SQLEXPRESS;Initial Catalog=qlBh;Integrated Security=True";
            var connection = new SqlConnection(strCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
                string sql = "update sanPham set tenSP=@ten ,giaBan=@gia,hinhAnh=@hinhAnh where maSP=@ma";
            cmd.CommandText= sql;
            cmd.Parameters.Add("@ma", System.Data.SqlDbType.Int).Value = maSP;
            cmd.Parameters.Add("@ten", System.Data.SqlDbType.NVarChar).Value = tenSP;
            cmd.Parameters.Add("@gia", System.Data.SqlDbType.Money).Value = giaBan;
            cmd.Parameters.Add("@hinhAnh", System.Data.SqlDbType.NVarChar).Value = hinhAnh;
            int ret = cmd.ExecuteNonQuery();
            if (ret > 0)
            {
                MessageBox.Show("sua thang cong");
            }
            else
            {
                MessageBox.Show("sua khong thang cong");
            }
            return RedirectToAction("Index", "QLSP");
        }
        [HttpPost]
        public ActionResult getList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string strCon = @"Data Source=WINDOWS-11\SQLEXPRESS;Initial Catalog=qlBh;Integrated Security=True";
            var connection = new SqlConnection(strCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select*from sanPham";
            var reader = cmd.ExecuteReader();
            List<SP> lst = new List<SP>();
            while (reader.Read())
            {
                int masp = Convert.ToInt32(reader["maSP"]);
                string tensp = reader["tenSP"].ToString();
                string giaban = reader["giaBan"].ToString();
                string hinhAnh = reader["hinhAnh"].ToString();
                SP a = new SP()
                {
                    maSP = masp,
                    tenSP = tensp,
                    giaBan = giaban,
                    hinhAnh = hinhAnh,
                };
                lst.Add(a);
            }
            reader.Close();
           
            int recordsTotals = lst.Count;
           
            if (!string.IsNullOrEmpty(searchValue))
            {
                lst=lst.Where(m => m.tenSP.ToLower().Contains(searchValue)).ToList();
            }
            int totalrowsafterfiltering =lst.Count;
            var sql = @"SELECT TOP (@length) [maSP],[tenSP],[giaBan],[hinhAnh]FROM [qlBh].[dbo].[sanPham] where maSP>@start";
            cmd.CommandText= sql;
            cmd.Parameters.Add("@length", System.Data.SqlDbType.Int).Value = length;
            cmd.Parameters.Add("@start", System.Data.SqlDbType.Int).Value = start;
            var red=cmd.ExecuteReader();
            List<SP> dsSP = new List<SP>();
            while (red.Read())
            {
                int masp = Convert.ToInt32(red["maSP"]);
                string tensp = red["tenSP"].ToString();
                string giaban = red["giaBan"].ToString();
                string hinhAnh = red["hinhAnh"].ToString();
                SP a = new SP()
                {
                    maSP = masp,
                    tenSP = tensp,
                    giaBan = giaban,
                    hinhAnh = hinhAnh,
                };
                dsSP.Add(a);
            }
            connection.Close(); 
             lst = lst.Skip(start).Take(length).ToList(); 
            return Json(new { data =lst , draw = Request["draw"], recordsTotals = recordsTotals, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult test() {
            
            return View();

        }
        public ActionResult thu()
        {
            
            return View();
        }
        public ActionResult check()
        {
            return View();
        }
    }
}