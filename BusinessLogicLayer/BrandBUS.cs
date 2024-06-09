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
    public class BrandBUS
    {
        BrandDAO dao = new BrandDAO();

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

        public int Insert(Brand obj)
        {
            return dao.Insert(obj);
        }


        public int Update(Brand obj)
        {
            return dao.Update(obj);
        }

        public int Delete(string ID)
        {
            return dao.Delete(ID);
        }
    }
}
