using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        DataConnect data = new DataConnect();
        
       
        public DataTable GetProductsByPriceRange(decimal lowPrice, decimal highPrice)
    {
        SqlParameter[] parameters = {
            new SqlParameter("@lowPrice", lowPrice),
            new SqlParameter("@highPrice", highPrice)
        };

        return data.GetData("Product_GetByPriceRange", parameters); 
    }
        public DataTable GetData()
        {
            return data.GetData("Product_Select_All", null);
        }
        public DataTable GetDataProduct()
        {
            return data.GetData("Product_Select_data", null);
        }

        public DataTable SearchProductinOrder(string name)
        {
            SqlParameter[] para ={
                new SqlParameter("Product_Name", name)
            };
            return data.GetData("Product_Select_data_ByName", para);
        }

        public DataTable GetDataProductAvailable()
        {
            return data.GetData("Product_Select_Available", null);
        }

        public DataTable GetDataProductWarranty(string Name)
        {
            SqlParameter[] para ={
                new SqlParameter("Product_Name", Name)
            };
            return data.GetData("Product_Select_Warranty_Price", para);
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

        public int Insert(Product product)
        {
            SqlParameter[] para = {
                new SqlParameter("@Product_Name", product.ProductName),
                new SqlParameter("@Product_Image", product.ProductImage),
                new SqlParameter("@Product_Price", product.ProductPrice),
                new SqlParameter("@Product_Quantity", product.ProductQuantity),
                new SqlParameter("@Brand_Id", product.BrandId),
                new SqlParameter("@Category_Id", product.CategoryId),
                new SqlParameter("@Product_Warranty", product.ProductWarranty),
                new SqlParameter("@Product_Status", product.ProductStatus),
                new SqlParameter("@Product_Details", product.ProductDetails)
            };
            return data.ExecuteSQL("Product_Insert", para);
        }

        public int Update(Product obj)
        {
            SqlParameter[] para = {
                new SqlParameter("Product_Id", obj.ProductId),
                        new SqlParameter("Product_Name", obj.ProductName),
                       new SqlParameter("Product_Image", obj.ProductImage),
                       new SqlParameter("Product_Price", obj.ProductPrice),
                       new SqlParameter("Product_Quantity", obj.ProductQuantity),
                       new SqlParameter("Brand_Id", obj.BrandId),
                       new SqlParameter("Category_Id", obj.CategoryId),
                       new SqlParameter("Product_Warranty", obj.ProductWarranty),
                       new SqlParameter("Product_Status", obj.ProductStatus),
                       new SqlParameter("Product_Details", obj.ProductDetails)
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
