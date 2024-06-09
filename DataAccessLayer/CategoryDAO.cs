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
    public class CategoryDAO
    {
        DataConnect data = new DataConnect();

        public DataTable GetData()
        {
            return data.GetData("Category_Select_All", null);
        }

        public DataTable GetDataByID(string ID)
        {
            SqlParameter[] para ={
                new SqlParameter("Category_Id", ID)
            };
            return data.GetData("Category_Select_ByID", para);
        }

        public DataTable GetDataByName(string Name)
        {
            SqlParameter[] para ={
                new SqlParameter("Category_Name", Name)
            };
            return data.GetData("Category_Select_ByName", para);
        }

        public int Insert(Category obj)
        {
            SqlParameter[] para = {
                        new SqlParameter("Category_Name", obj.CategoryName),
                        new SqlParameter("Category_Status", obj.CategoryStatus)
                    };
            return data.ExecuteSQL("Category_Insert", para);
        }

        public int Update(Category obj)
        {
            SqlParameter[] para = {
                new SqlParameter("Category_Id", obj.CategoryId),
                        new SqlParameter("Category_Name", obj.CategoryName),
                        new SqlParameter("Category_Status", obj.CategoryStatus)
            };
            return data.ExecuteSQL("Category_Update", para);
        }

        public int Delete(string ID)
        {
            SqlParameter[] para ={
                new SqlParameter("Category_Id", Convert.ToInt32(ID))
            };
            return data.ExecuteSQL("Category_Delete", para);
        }
    }
}
