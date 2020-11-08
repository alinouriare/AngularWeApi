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
    public class DepartmentController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _evn;
        public DepartmentController(IConfiguration configuration,IWebHostEnvironment env)
        {
            _configuration = configuration;
            _evn = env;
        }

        [HttpGet]

        public JsonResult Get()
        {

            string query = @"select Id,DepartmentName from Department";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Employee");
            SqlDataReader sqlDataReader;
            using (SqlConnection con=new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand command=new SqlCommand(query,con))
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
        public JsonResult Post(Department model)
        {

            string query = @"insert into Department values('" + model.DepartmentName + @"')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Employee");
            SqlDataReader sqlDataReader;

            using (SqlConnection con=new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand command=new SqlCommand(query,con))
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
        public JsonResult Put(Department model)
        {

            string query = @"update Department 
set DepartmentName='"+model.DepartmentName+@"'
where Id="+model.Id+@"";
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

            string query = @"Delete Department 
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

      
      
    }
  
}
