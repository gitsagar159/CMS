using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace CMS.Util
{
    public class BaseRepository
    {
        private bool connection_open;
        protected SqlConnection connection;
       
        protected SqlConnection Get_Connection()
        {
            connection_open = false;
            connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["CMS_ConnectionString"].ConnectionString;


            if (Open_Connection())
            {
                connection_open = true;
                return connection;
            }
            else
            {
                throw new Exception("Error");
            }
        }


        private bool Open_Connection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}