using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        DataConnect data = new DataConnect();

        public DataTable GetData()
        {
            return data.GetData("Product_Select_All", null);
        }

        public DataTable GetDataByID(string ID)
        {
            SqlParameter[] para ={
                new SqlParameter("Product_Id", ID)
            };
            return data.GetData("Product_Select_ByID", para);
        }

        public DataTable GetDataByName(string Name)
        {
            SqlParameter[] para ={
                new SqlParameter("Product_Name", Name)
            };
            return data.GetData("Product_Select_ByName", para);
        }

        public int Insert(Product obj)
        {
            SqlParameter[] para = {
                       new SqlParameter("Product_Name", obj.Product_Name),
                       new SqlParameter("Product_Image", obj.Product_Image),
                       new SqlParameter("Product_Price", obj.Product_Price),
                       new SqlParameter("Product_Quantity", obj.Product_Quantity),
                       new SqlParameter("Brand_Id", obj.Brand_Id),
                       new SqlParameter("Category_Id", obj.Category_Id),
                       new SqlParameter("Product_Warranty", obj.Product_Warranty),
                       new SqlParameter("Product_Status", obj.Product_Status),
                       new SqlParameter("Product_Details", obj.Product_Details)
                    };
            return data.ExecuteSQL("Product_Insert", para);
        }

        public int Update(Product obj)
        {
            SqlParameter[] para = {
                new SqlParameter("Product_Id", obj.Product_Id),
                        new SqlParameter("Product_Name", obj.Product_Name),
                       new SqlParameter("Product_Image", obj.Product_Image),
                       new SqlParameter("Product_Price", obj.Product_Price),
                       new SqlParameter("Product_Quantity", obj.Product_Quantity),
                       new SqlParameter("Brand_Id", obj.Brand_Id),
                       new SqlParameter("Category_Id", obj.Category_Id),
                       new SqlParameter("Product_Warranty", obj.Product_Warranty),
                       new SqlParameter("Product_Status", obj.Product_Status),
                       new SqlParameter("Product_Details", obj.Product_Details)
            };
            return data.ExecuteSQL("Product_Update", para);
        }

        public int Delete(string ID)
        {
            SqlParameter[] para ={
                new SqlParameter("Product_Id", Convert.ToInt32(ID))
            };
            return data.ExecuteSQL("Product_Delete", para);
        }
    }
}
