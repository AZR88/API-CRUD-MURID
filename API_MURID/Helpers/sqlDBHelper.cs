using API_MURID.Models;
using Npgsql;
using System.Data;

namespace API_MURID.Helpers
{
    public class SqlDBHelper
    {
        private NpgsqlConnection connection;
        private string __constr;

        public SqlDBHelper(string pCOnstr)
        {
            __constr = pCOnstr;
            connection = new NpgsqlConnection();
            connection.ConnectionString = __constr;
        }

        public NpgsqlCommand GetNpgsqlCommand(string query)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public void closeConnection()
        {
            connection.Close();
        }
       
        }
    }


