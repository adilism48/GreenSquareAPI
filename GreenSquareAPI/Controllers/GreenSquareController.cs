using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace GreenSquareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreenSquareController : ControllerBase
    {
        private IConfiguration _configuration;
        public GreenSquareController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetProducts")]
        public JsonResult GetProducts()
        {
            string query = "select * from dbo.products";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("greenSquareDBCon");
            SqlDataReader myReader;
            using(SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myConn)) 
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet]
        [Route("GetProductsByCategory")]
        public JsonResult GetProductsByCategory(int category)
        {
            string query = "select * from dbo.products where category=@category";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("greenSquareDBCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@category", category);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        [Route("AddProduct")]
        public JsonResult AddProduct([FromForm] int category, [FromForm] string product_name, [FromForm] string price)
        {
            string query = "insert into dbo.products(category, product_name, price) values(@0, @1, @2)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("greenSquareDBCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@0", category);
                    myCommand.Parameters.AddWithValue("@1", product_name);
                    myCommand.Parameters.AddWithValue("@2", price);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Added Successfuly");
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public JsonResult DeleteProduct([FromForm] int id)
        {
            string query = "delete from dbo.products where id=@id";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("greenSquareDBCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Deleted Successfuly");
        }

        [HttpPatch]
        [Route("UpdateProduct")]
        public JsonResult UpdateProduct([FromForm] int category, [FromForm] string product_name, [FromForm] string price, int id)
        {
            string query = "update dbo.products set category=@1, product_name=@2, price=@3 where id=@0";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("greenSquareDBCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@1", category);
                    myCommand.Parameters.AddWithValue("@2", product_name);
                    myCommand.Parameters.AddWithValue("@3", price);
                    myCommand.Parameters.AddWithValue("@0", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Updated Successfuly");
        }
    }
}
