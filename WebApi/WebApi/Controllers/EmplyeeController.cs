using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmplyeeController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _evn;
        public EmplyeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _evn = env;
        }

        [HttpGet]

        public JsonResult Get()
        {

            string query = @"select Id,EmployeeName,Department,CONVERT(varchar(10),DateofJoning,120) as DateofJoning,Photo from Employee";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Employee");
            SqlDataReader sqlDataReader;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    sqlDataReader = command.ExecuteReader();
                    table.Load(sqlDataReader);
                    sqlDataReader.Close();
                    con.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employee model)
        {

            string query = @"  insert into Employee(EmployeeName,Department,DateofJoning,Photo) values
                 ('" + model.EmployeeName + @"', '" + model.Department + @"', ' " + model.DateofJoning + @"','" + model.Department + @" ' )";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Employee");
            SqlDataReader sqlDataReader;

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    sqlDataReader = command.ExecuteReader();
                    table.Load(sqlDataReader);
                    sqlDataReader.Close();
                    con.Close();

                }
            }

            return new JsonResult("Add Succesfully");

        }

        [HttpPut]
        public JsonResult Put(Employee model)
        {

            string query = @"update Employee set EmployeeName='" + model.EmployeeName + @"', Department='" + model.Department + @"',DateofJoning='" + model.DateofJoning + @"',Photo='" + model.Photo + @"'where Id=" + model.Id + @"";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Employee");
            SqlDataReader sqlDataReader;

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    sqlDataReader = command.ExecuteReader();
                    table.Load(sqlDataReader);
                    sqlDataReader.Close();
                    con.Close();

                }
            }

            return new JsonResult("Update Succesfully");

        }


        [HttpDelete("{Id}")]
        public JsonResult Delete(int Id)
        {

            string query = @"Delete Employee 
                where Id=" + Id + @"";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Employee");
            SqlDataReader sqlDataReader;

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    sqlDataReader = command.ExecuteReader();
                    table.Load(sqlDataReader);
                    sqlDataReader.Close();
                    con.Close();

                }
            }

            return new JsonResult("Delete Succesfully");

        }

        [HttpPost]
        [Route("SaveFile")]
        public JsonResult SaveFile()
        {
            try
            {
                var httprequest = Request.Form;
                var postfile = httprequest.Files[0];
                string filename = postfile.FileName;
                var physicalpath = _evn.ContentRootPath + "/Photos/" + filename;
                using (var stream = new FileStream(physicalpath, FileMode.Create))
                {
                    postfile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("not");

            }

        }

        [HttpGet]
        [Route("GetAllDepartment")]
        public JsonResult GetDepartment()
        {
            string query = @"select DepartmentName from Department";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Employee");
            SqlDataReader sqlDataReader;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    sqlDataReader = command.ExecuteReader();
                    table.Load(sqlDataReader);
                    sqlDataReader.Close();
                    con.Close();
                }
            }

            return new JsonResult(table);

        }
    }
}
