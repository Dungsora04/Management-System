using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DTO;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        DataConnect data = new DataConnect();

        public DataTable GetData()
        {
            return data.GetData("Customer_Select_All", null);
        }

        public int Insert(Customer obj)
        {
            SqlParameter[] para = {
                        new SqlParameter("Customer_Name", obj.CustomerName),
                        new SqlParameter("Customer_Number", obj.CustomerNumber)
                    };
            return data.ExecuteSQL("Customer_Insert", para);
        }

        public DataTable GetDataByName(string Name)
        {
            SqlParameter[] para ={
                new SqlParameter("Customer_Name", Name)
            };
            return data.GetData("Customer_Select_ByName", para);
        }

        public DataTable GetDetails(string id)
        {
            SqlParameter[] para ={
                new SqlParameter("Customer_Id", Convert.ToInt32(id))
            };
            return data.GetData("Customer_Get_Detail", para);
        }

        public int Update(Customer obj)
        {
            SqlParameter[] para = {
                new SqlParameter("Customer_Id", obj.CustomerId),
                new SqlParameter("Customer_Name", obj.CustomerName),
                new SqlParameter("Customer_Number", obj.CustomerNumber)
            };
            return data.ExecuteSQL("Customer_Update", para);
        }

        public int Delete(string ID)
        {
            SqlParameter[] para ={
                new SqlParameter("Customer_Id", Convert.ToInt32(ID))
            };
            return data.ExecuteSQL("Customer_Delete", para);
        }




    }
}
