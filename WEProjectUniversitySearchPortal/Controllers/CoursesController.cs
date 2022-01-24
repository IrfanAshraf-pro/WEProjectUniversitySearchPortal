using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEProjectUniversitySearchPortal.Models;
using System.Data.SqlClient;

namespace WEProjectUniversitySearchPortal.Controllers
{
    public class CoursesController : Controller
    {
        static public String constr = @"Data Source=DESKTOP-E188SG7\SQLSERVER2019;Initial Catalog=universityPortal;Integrated Security=true";
        static public SqlConnection con = new SqlConnection(constr);
        // GET: Courses
        public ActionResult Courses()
        {
            ViewBag.msg = "";
            return View();
        }
        [HttpPost]
        public ActionResult Courses(Courses c)
        {
            try
            {
                con.Open();
                String querry = @"insert into courses values('" + c.u_id + "','" + c.p_id + "','" + c.name + "','" +c.crhs+ "','" +c.c_code+ "')";
                SqlCommand cmd = new SqlCommand(querry, con);
                cmd.ExecuteNonQuery();
                con.Close();
                ViewBag.msg = "Success";
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View();
        }
        public ActionResult AllCourses(String uniId,String uname,String programId, String programname)
        {
            var list = readingAllCourses(uniId, programId);
            return View(list);
        }
        public List<Courses> readingAllCourses(String uniId,String pId)
        {
            String querry = @"select * from courses where u_id='"+uniId+"' and p_id='"+pId+"'";
            con.Open();
            SqlCommand smd = new SqlCommand(querry, con);
            SqlDataReader sdr = smd.ExecuteReader();
            List<Courses> plist = new List<Courses>();
            while (sdr.Read())
            {
                Courses c = new Courses();
                c.c_id = sdr["c_id"].ToString();
                c.u_id = sdr["u_id"].ToString();
                c.p_id = sdr["p_id"].ToString();
                c.name = sdr["cname"].ToString();
                c.crhs = sdr["chrs"].ToString();
                c.c_code = sdr["c_code"].ToString();
                plist.Add(c);
            }
            con.Close();
            return plist;
        }
    }
}