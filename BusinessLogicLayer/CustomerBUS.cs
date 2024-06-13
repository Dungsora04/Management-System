using DataAccessLayer;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CustomerBUS
    {
        CustomerDAO customerDAO = new CustomerDAO();

        public DataTable GetData()
        {
            return customerDAO.GetData();
        }

        public int Insert(Customer obj)
        {
            return customerDAO.Insert(obj);
        }

        public DataTable GetDataByName(string name)
        {
            return customerDAO.GetDataByName(name);
        }

        public DataTable GetDetails(string id)
        {
            return customerDAO.GetDetails(id);
        }

        public int Update(Customer obj)
        {
            return customerDAO.Update(obj);
        }


        public int Delete(string ID)
        {
            return customerDAO.Delete(ID);  
        }
    }
}
