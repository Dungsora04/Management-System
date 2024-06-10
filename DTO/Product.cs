using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Product
    {
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public byte[] Product_Image { get; set; }
        public int Product_Price { get; set; }
        public int Product_Quantity { get; set; }
        public int Brand_Id { get; set; }
        public int Category_Id { get; set;}
        public int Product_Warranty { get; set; }
        public string Product_Status { get; set; }
        public string Product_Details { get; set; }
    }
}
