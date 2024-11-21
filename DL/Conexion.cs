using System;
using System.Data.SqlClient;
using System.Configuration;

namespace DL
{
    public class Conexion
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["JGuevaraProgramacionNCapas"].ConnectionString.ToString();
        }
    }
}
