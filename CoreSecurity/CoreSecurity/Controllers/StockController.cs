using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using CoreSecurity.EntityStore;
using CoreSecurity.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreSecurity.Controllers
{
    [Produces("application/json")]
    [Route("api/Stock")]
    public class StockController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        public StockController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // POST: api/Stock
        [HttpPost]
        public IActionResult Post([FromBody]StockTbInput stockTbInput)
        {
            if (stockTbInput != null)
            {
                List<StockTb> stocklList = ConvertDataTable<StockTb>(GetStock(stockTbInput));
                return Ok(stocklList);
            }
            else
            {
                return BadRequest();
            }
        }

        public DataTable GetStock(StockTbInput stockTbInput)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection("Data Source=SAI-PC\\SQLEXPRESS; UID=sa; Password=Pass$123;Database=AllSampleCode;"))
            {

                //con.Open();
                //var query = "select * from StockTb where Name='" + stockTbInput.Name +"'";
                //SqlCommand cmd = new SqlCommand(query, con) { CommandType = CommandType.Text };
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(dt);


                con.Open();
                var @query = "select * from StockTb where Name=@Name";
                SqlCommand cmd = new SqlCommand(query, con) { CommandType = CommandType.Text };
                cmd.Parameters.AddWithValue("@Name", stockTbInput.Name.Trim());
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }

            return dt;
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

    }
}
