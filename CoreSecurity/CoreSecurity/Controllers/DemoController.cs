using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoreSecurity.EntityStore;
using CoreSecurity.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreSecurity.Controllers
{
    public class DemoController : Controller
    {

        private readonly DatabaseContext _databaseContext;
        public DemoController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult Index(string firstName)
        {

            try
            {
                List<Registration> registrations = GetUserDetailsbyId_Entityframework(firstName);
                return View(registrations);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetUserDetailsbyId(string registrationId)
        {
            DataTable dt = new DataTable();

            #region connection String
            var connection = "Data Source=SAI-PC\\SQLEXPRESS; UID=sa; Password=Pass$123;Database=AllSampleCode;";
            #endregion

            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                //Inline Query
                var query = "select * from Registration where RegistrationId=" + registrationId;
                SqlCommand cmd = new SqlCommand(query, con) { CommandType = CommandType.Text };
                SqlDataAdapter da = new SqlDataAdapter { SelectCommand = cmd };
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetUserDetailsbyId_StoreProcedure(string registrationId)
        {
            DataTable dt = new DataTable();

            #region connection String
            var connection = "Data Source=SAI-PC\\SQLEXPRESS; UID=sa; Password=Pass$123;Database=AllSampleCode;";
            #endregion

            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Usp_GetUserDetailsByRegistrationId", con);
                cmd.Parameters.AddWithValue("@RegistrationId", registrationId);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter { SelectCommand = cmd };
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetUserDetailsbyId_ParameterizedQuery(string registrationId)
        {
            DataTable dt = new DataTable();

            #region connection String
            var connection = "Data Source=SAI-PC\\SQLEXPRESS; UID=sa; Password=Pass$123;Database=AllSampleCode;";
            #endregion

            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                var @query = "select * from Registration where RegistrationId=@RegistrationId";
                SqlCommand cmd = new SqlCommand(query, con) { CommandType = CommandType.Text };
                cmd.Parameters.AddWithValue("@RegistrationId", registrationId);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            return dt;
        }

        public List<Registration> GetUserDetailsbyId_Entityframework(string firstName)
        {
            try
            {
                var result = (from registration in _databaseContext.Registration
                              where registration.FirstName == firstName
                              select registration).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
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