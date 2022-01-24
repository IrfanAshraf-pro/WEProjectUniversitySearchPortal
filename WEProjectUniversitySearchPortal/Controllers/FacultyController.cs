using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEProjectUniversitySearchPortal.Models;
using System.Data.SqlClient;
using System.IO;


namespace WEProjectUniversitySearchPortal.Controllers
{
    public class FacultyController : Controller
    {
        static public String constr = @"Data Source=DESKTOP-E188SG7\SQLSERVER2019;Initial Catalog=universityPortal;Integrated Security=true";
        static public SqlConnection con = new SqlConnection(constr);
        // GET: Faculty
        public ActionResult Faculty()
        {
            ViewBag.path = null;
            return View();
        }
        [HttpPost]
        public ActionResult Faculty(Faculty f)
        {
            try
            {
                var aext = new[] { ".png", ".jpeg", ".jpg", ".bnp" };
                var ext = Path.GetExtension(f.fimage.FileName);
                var imageName =f.name + "_" + f.u_id + ext;
                if (aext.Contains(ext))
                {
                    var path = Path.Combine(Server.MapPath("~/images/"), imageName);
                    f.fimage.SaveAs(path);
                    var pathToDatabae = "/images/" + imageName;
                    con.Open();
                    String querry = "insert into faculties values('" + f.u_id + "','" + f.name + "','" + f.experience + "','" + pathToDatabae + "')";
                    SqlCommand cmd = new SqlCommand(querry, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ViewBag.path = "Saved Successfully" + path;
                }
            }
            catch (Exception ex)
            {
                ViewBag.path = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult AllFaculty(String uniId, String uname)
        {
            var list = readingFaculty(uniId);
            return View(list);

        }


        #region private functions
        private List<Faculty> readingFaculty(String uId)
        {
            con.Open();
            String querry = @"select * from faculties where u_id='"+uId+"'";
            SqlCommand cmd = new SqlCommand(querry, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            List<Faculty> lfaculty = new List<Faculty>();
            while (sdr.Read())
            {
                Faculty f = new Faculty();
                f.u_id = sdr["u_id"].ToString();
                f.f_id = sdr["f_id"].ToString();
                f.name = sdr["fname"].ToString();
                f.experience = sdr["experience"].ToString();
                f.fpath = sdr["fimage"].ToString();
                lfaculty.Add(f);
            }
            con.Close();
            return lfaculty;
        }


        #endregion

    }
}