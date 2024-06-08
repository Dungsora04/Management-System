using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class DataConnect
    {
        SqlConnection connection;

        public DataConnect()
        {
            connection = new SqlConnection("Data Source=localhost;Initial Catalog=CSMS;Integrated Security=True;MultipleActiveResultSets=True;");
        }
        public DataTable GetData(string strSQL) //select
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, connection);
            connection.Open();
            da.Fill(dt);
            connection.Close();
            return dt;
        }
        public DataTable GetData(string procName, SqlParameter[] para)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            if (para != null)
            {
                cmd.Parameters.AddRange(para);
            }
            cmd.Connection = connection;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            connection.Open();
            da.Fill(dt);
            connection.Close();
            return dt;
        }


        public int ExecuteSQL(string strSQL)
        { 
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            connection.Open();
            int row = cmd.ExecuteNonQuery();
            connection.Close();
            return row;
        }
        public int ExecuteSQL(string procName, SqlParameter[] para ) {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;
            if (para != null) cmd.Parameters.AddRange(para);
       
            connection.Open();
            int row = cmd.ExecuteNonQuery();
            connection.Close();
            return row;
        }
    }

}
