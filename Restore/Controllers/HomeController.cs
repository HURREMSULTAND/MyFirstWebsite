using Dapper;
using Restore.Entities;
using  Restore .Extensions;
using Restore.Models;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restore.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

      
        

    [HttpGet]
    public ActionResult Index()
    {
        SqlConnection connection = new SqlConnection("server=.\\SQLExpress; database=CvDb; integrated security =true");

        var about = connection.QueryFirst<About>(sql: "select * from Abouts");

        var skills = connection.Query<Skill>(sql: "ap_ListSkill", commandType: System.Data.CommandType.StoredProcedure);

        var services = connection.Query<Service>(sql: "select * from Services");

        var serviceSlogan = connection.QuerySingle<Slogan>(commandType: System.Data.CommandType.StoredProcedure, sql: "ap_ListSlogan", param: new
        {
            @sectionName = "Services"
        });

        var skillSlogan = connection.QuerySingle<Slogan>(sql: "ap_ListSlogan", param: new
        {
            @sectionName = "Skills"
        }, commandType: System.Data.CommandType.StoredProcedure);

        var reviews = connection.Query<CustomerReview>(sql: "ap_ListCustomerReviews", commandType: System.Data.CommandType.StoredProcedure);

        var viewModel = new IndexViewModel();

        viewModel.About = about;
        viewModel.Skills = skills;
        viewModel.Services = services;
        viewModel.ServiceSlogan = serviceSlogan;
        viewModel.SkillSlogan = skillSlogan;
        viewModel.CustomerReviews = reviews;

        return View(viewModel);
    }


    [HttpGet]
        public ActionResult Portfolio()
        {

            SqlConnection connection = new SqlConnection("server=.\\SQLExpress; database=CvDb; integrated security =true");
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "ap_ListSlogan";

            command.Parameters.Add("@sectionName", "Skills");

            connection.Open();
            var reader = command.ExecuteReader();
            while(reader.Read())
            {
                var title = reader.GetString(2);
                var description = reader.GetString(3);
            }
            connection.Close();
            reader.Close();
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            SqlConnection connection = new SqlConnection("server=.\\SQLExpress; database=CvDb; integrated security =true");
            var affectedRows = connection.Execute(sql: "ap_CreateContact", commandType: System.Data.CommandType.StoredProcedure, param: contact);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }



    }
}