using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEProjectUniversitySearchPortal.Models;
using System.Data.SqlClient;


namespace WEProjectUniversitySearchPortal.Controllers
{
    public class ProgramController : Controller
    {
        static public String constr = @"Data Source=DESKTOP-E188SG7\SQLSERVER2019;Initial Catalog=universityPortal;Integrated Security=true";
        static public SqlConnection con = new SqlConnection(constr);
        // GET: Program
        public ActionResult Program()
        {
            ViewBag.msg = "";
            return View();
        }
        [HttpPost]
        public ActionResult Program(Program p)
        {
            try
            {
                con.Open();
                String querry = @"insert into programs values('"+p.u_id+ "','"+p.pName+ "','"+p.requirements+"')";
                SqlCommand cmd = new SqlCommand(querry,con);
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
        [HttpGet]
        public ActionResult AllPrograms(String uniId, String uname)
        {
            var plist = readingAllPrograms(uniId);
            return View(plist);
        }

        #region private functions
        public List<Program> readingAllPrograms(String uniId)
        {
            String querry = @"select * from programs where u_id='"+uniId+"'";
            con.Open();
            SqlCommand smd = new SqlCommand(querry, con);
            SqlDataReader sdr = smd.ExecuteReader();
            List<Program> plist = new List<Program>();
            while (sdr.Read())
            {
                Program p = new Program();
                p.u_id = sdr["u_id"].ToString();
                p.p_id = sdr["p_id"].ToString();
                p.pName = sdr["pname"].ToString();
                p.requirements = sdr["prequirements"].ToString();
                plist.Add(p);
            }
            con.Close();
            return plist;
        }

        #endregion
    }
}