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
    public class UserDAO
    {
        DataConnect data = new DataConnect();
        public DataTable GetData()
        {
            return data.GetData("User_Select_All", null);
        }

        public DataTable GetDataByID(string ID)
        {
            SqlParameter[] para ={
                new SqlParameter("@Users_Id", ID)
            };
            return data.GetData("User_Select_ByID", para);
        }

        public DataTable GetDataByName(string Name)
        {
            SqlParameter[] para ={
                new SqlParameter("@Users_Name", Name)
            };
            return data.GetData("User_Select_ByName", para);
        }

        public int Insert(User obj)
        {
            SqlParameter[] para = {
                        new SqlParameter("@Users_Name", obj.UserName),
                        new SqlParameter("@Users_Email", obj.Email),
                        new SqlParameter("@Users_Password", obj.Password) ,
                        new SqlParameter("@Roles_Id", obj.RoleId),
                    };
            return data.ExecuteSQL("User_Insert", para);
        }

        public int Update(User obj)
        {
            SqlParameter[] para = {
                         new SqlParameter("@Users_Name", obj.UserName),
                         new SqlParameter("@Users_Id", obj.UserId),
                        new SqlParameter("@Users_Email", obj.Email),
                        new SqlParameter("@Users_Password", obj.Password) ,
                        new SqlParameter("@Roles_Id", obj.RoleId),
            };
            return data.ExecuteSQL("User_Update", para);
        }

        public int Delete(string ID)
        {
            SqlParameter[] para ={
                new SqlParameter("@Users_Id", Convert.ToInt32(ID))
            };
            return data.ExecuteSQL("User_Delete", para);
        }





        //----------------------------phan nay danh cho log in va forgot pass-------------------------------------------------------------------------------
        public DataTable check_Password(string username, string password)
        {
            SqlParameter[] para ={
                new SqlParameter("username", username),
                new SqlParameter("password", password)
            };
            return data.GetData("User_Check_Password", para);
        }

        public DataTable check_Email(string username, string email)
        {
            SqlParameter[] para ={
                new SqlParameter("username", username),
                new SqlParameter("email", email)
            };
            return data.GetData("User_Check_Email", para);
        }

    }
}
