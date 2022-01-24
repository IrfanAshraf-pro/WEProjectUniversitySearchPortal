using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using WEProjectUniversitySearchPortal.Models;
using System.Data.SqlClient;

namespace WEProjectUniversitySearchPortal.Controllers
{
    public class UniversityController : Controller
    {
        static public String constr = @"Data Source=DESKTOP-E188SG7\SQLSERVER2019;Initial Catalog=universityPortal;Integrated Security=true";
        static public SqlConnection con = new SqlConnection(constr);
        // GET: University
        [HttpGet]
        public ActionResult AllUniversities()
        {
            var uList = gettingAllUniversities();
            return View(uList);
        }
        [HttpPost]
        public ActionResult AllUniversities(String uniSearch)
        {
            var uList = gettingAllUniversities(uniSearch);
            return View(uList);
        }
        #region getting All universities
        private List<University> gettingAllUniversities()
        {
            con.Open();
            String querry = "select * from universities";
            SqlCommand cmd = new SqlCommand(querry, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            List<University> uList = new List<University>();
            while (sdr.Read())
            {
                University u = new University();
                u.u_id = sdr["u_id"].ToString();
                u.name = sdr["name"].ToString();
                u.location = sdr["location"].ToString();
                u.registered = sdr["registered"].ToString();
                u.path = sdr["image"].ToString();
                uList.Add(u);
            }
            con.Close();
            return uList;
        }
        private List<University> gettingAllUniversities(String search)
        {
            con.Open();
            String querry = "select * from universities where location='" + search + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            List<University> uList = new List<University>();
            while (sdr.Read())
            {
                University u = new University();
                u.u_id = sdr["u_id"].ToString();
                u.name = sdr["name"].ToString();
                u.location = sdr["location"].ToString();
                u.registered = sdr["registered"].ToString();
                u.path = sdr["image"].ToString();
                uList.Add(u);
            }
            con.Close();
            return uList;
        }
        #endregion
        [HttpGet]
        public ActionResult University()
        {
            ViewBag.path = null;
            return View();
        }
        [HttpPost]
        public ActionResult University(University u)
        {

            try
            {
                var aext = new[] { ".png", ".jpeg", ".jpg", ".bnp" };
                var ext = Path.GetExtension(u.image.FileName);
                var imageName = u.name + "_" + u.location + ext;
                if (aext.Contains(ext))
                {
                    var path = Path.Combine(Server.MapPath("~/images/"), imageName);
                    u.image.SaveAs(path);
                    var pathToDatabae = "/images/" + imageName;
                    con.Open();
                    String querry = "insert into universities values('" + u.name + "','" + u.location + "','" + u.registered + "','" + pathToDatabae + "')";
                    SqlCommand cmd = new SqlCommand(querry, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ViewBag.path = "Saved Successfully" + path;
                }
            } catch (Exception ex)
            {
                ViewBag.path = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult SelectedUniversity(String uniId, String uname)
        {
            return View();   
        }
       

        private void readingFacultyOfUni(String uniId)
        {
            String querry = "select * from faculties where u_id='" + uniId + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            SqlDataReader sdr = cmd.ExecuteReader();

        }
        private void readingProgramOfUni()
        {

        }
    }
}