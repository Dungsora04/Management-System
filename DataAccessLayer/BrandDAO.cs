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
    public class BrandDAO
    {
        DataConnect data = new DataConnect();

        public DataTable GetData()
        {
            return data.GetData("Brand_Select_All", null);
        }
        
        public DataTable GetDataByID(string ID)
        {
            SqlParameter[] para ={
                new SqlParameter("Brand_Id", ID)
            };
            return data.GetData("Brand_Select_ByID", para);
        }

        public DataTable GetDataByName(string Name)
        {
            SqlParameter[] para ={
                new SqlParameter("Brand_Name", Name)
            };
            return data.GetData("Brand_Select_ByName", para);
        }

        public int Insert(Brand obj)
        {
            SqlParameter[] para = {
                        new SqlParameter("Brand_Name", obj.BrandName),
                        new SqlParameter("Brand_Status", obj.BrandStatus)
                    };
            return data.ExecuteSQL("Brand_Insert", para);
        }

        public int Update(Brand obj) {
            SqlParameter[] para = {
                new SqlParameter("Brand_Id", obj.BrandId),
                        new SqlParameter("Brand_Name", obj.BrandName),
                        new SqlParameter("Brand_Status", obj.BrandStatus)
            };
            return data.ExecuteSQL("Brand_Update", para);
        }

        public int Delete(string ID)
        {
            SqlParameter[] para ={
                new SqlParameter("Brand_Id", Convert.ToInt32(ID))
            };
            return data.ExecuteSQL("Brand_Delete", para);
         }

    }
}
