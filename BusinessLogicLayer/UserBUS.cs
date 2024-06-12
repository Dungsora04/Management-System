using DataAccessLayer;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BusinessLogicLayer
{
    public class UserBUS
    {
        UserDAO dao = new UserDAO();

        public DataTable GetData()
        {
            return dao.GetData();
        }

        public DataTable GetDataByID(string id)
        {
            return dao.GetDataByID(id);
        }

        public DataTable GetDataByName(string name)
        {
            return dao.GetDataByName(name);
        }

        public int Insert(User obj)
        {
            return dao.Insert(obj);
        }


        public int Update(User obj)
        {
            return dao.Update(obj);
        }

        public int Delete(string ID)
        {
            return dao.Delete(ID);
        }





        //----------------------------phan nay danh cho log in va forgot pass-------------------------------------------------------------------------------

        public DataTable check_Password(string username, string password)
        {
            return dao.check_Password(username, password);
        }

        public DataTable check_Email(string username, string email)
        {
            return dao.check_Email(username, email);
        }

    }
}
