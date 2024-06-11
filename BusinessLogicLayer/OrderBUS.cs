using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer;
using DTO;
using System.Data.SqlClient;
using System.Data;

namespace BusinessLogicLayer
{
    public class OrderBUS
    {
        OrderDAO dao = new OrderDAO();

        public DataTable GetData()
        {
            return dao.GetData();
        }

        public DataTable GetData_MaxOrderId()
        {
            return dao.GetData_MaxOrderId();
        }

        public DataTable GetDataCustomerbyOrder(int ID)
        {
            return dao.GetDataCustomerbyOrder(ID);
        }

        public DataTable GetDataDGV()
        {
            return dao.GetDataDGV();
        }

        public DataTable GetDataByCustomerProduct(int ID)
        {
            return dao.GetDataByCustomerProduct(ID);
        }

        public DataTable GetDataByNumber(string number)
        {
            return dao.GetDataByNumber(number);
        }

        public DataTable GetDataByName(string name)
        {
            return dao.GetDataByName(name);
        }

        public int Insert(Order obj)
        {
            return dao.Insert(obj);
        }


        public int Update(Order obj)
        {
            return dao.Update(obj);
        }

        public int Delete(string ID)
        {
            return dao.Delete(ID);
        }
    }
}
