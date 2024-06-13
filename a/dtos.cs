using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_System.DTO
{
    public class ProductDto
    {
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public byte[] Product_Image { get; set; }
        public int Product_Price { get; set; }
        public string Brand_Name { get; set; }
        public string Category_Name { get; set; }
        public int Product_Quantity { get; set; }
        public int Product_Warranty { get; set; }
        public string Product_Status { get; set; }
        public string Product_Details { get; set; }
    }

    public class BrandDto
    {
        public int Brand_Id { get; set; }
        public string Brand_Name { get; set; }
        public string Brand_Status { get; set; }
    }

    public class CategoryDto
    {
        public int Category_Id { get; set; }
        public string Category_Name { get; set; }
        public string Category_Status { get; set; }
    }

}
