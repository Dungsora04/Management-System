using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class OrderDAO
    {
        DataConnect data = new DataConnect();

        public DataTable GetData()
        {
            return data.GetData("Order_Select_All", null);
        }

        public DataTable GetData_MaxOrderId()
        {
            return data.GetData("GetData_MaxOrderId", null);
        }

        public DataTable GetDataCustomerbyOrder(int ID)
        {
            SqlParameter[] para ={
                new SqlParameter("Customer_Id", ID)
            };
            return data.GetData("Order_Select_Customer", para);
        }


        public DataTable GetDataDGV()
        {
            return data.GetData("Order_Select_DGV", null);
        }

        public DataTable GetDataByCustomerProduct(int ID)
        {
            SqlParameter[] para ={
                new SqlParameter("Orders_Id", ID)
            };
            return data.GetData("Order_Select_CustomerProduct", para);
        }

        public DataTable GetDataByNumber(string Number)
        {
            SqlParameter[] para ={
                new SqlParameter("Customer_Number", Number)
            };
            return data.GetData("Order_Select_ByNumber", para);
        }

        public DataTable GetDataByName(string Name)
        {
            SqlParameter[] para ={
                new SqlParameter("Order_Name", Name)
            };
            return data.GetData("Order_Select_ByName", para);
        }

        public int Insert(Order obj)
        {
            SqlParameter[] para = {
                        new SqlParameter("Orders_Date", obj.Orders_Date),
                        new SqlParameter("Customer_Id", obj.Customer_Id),
                        new SqlParameter("Users_Id", obj.Users_Id),
                        new SqlParameter("Total_Amount", obj.Total_Amount),
                        new SqlParameter("Paid_Amount", obj.Paid_Amount),
                        new SqlParameter("Due_Amount", obj.Due_Amount),
                        new SqlParameter("Discount", obj.Discount),
                        new SqlParameter("Grand_Total", obj.Grand_Total)
                    };
            return data.ExecuteSQL("Order_Insert", para);
        }

        public int Update(Order obj)
        {
            SqlParameter[] para = {
                        new SqlParameter("Orders_Id", obj.Order_Id),
                        new SqlParameter("Orders_Date", obj.Orders_Date),
                        new SqlParameter("Customer_Id", obj.Customer_Id),
                        new SqlParameter("Users_Id", obj.Users_Id),
                        new SqlParameter("Total_Amount", obj.Total_Amount),
                        new SqlParameter("Paid_Amount", obj.Paid_Amount),
                        new SqlParameter("Due_Amount", obj.Due_Amount),
                        new SqlParameter("Discount", obj.Discount),
                        new SqlParameter("Grand_Total", obj.Grand_Total)
            };
            return data.ExecuteSQL("Order_Update", para);
        }

        public int Delete(string ID)
        {
            SqlParameter[] para ={
                new SqlParameter("Orders_Id", Convert.ToInt32(ID))
            };
            return data.ExecuteSQL("Order_Delete", para);
        }
    }
}
