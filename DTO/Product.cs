using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public byte[] ProductImage { get; set; }
        public int ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set;}
        public int ProductWarranty { get; set; }
        public string ProductStatus { get; set; }
        public string ProductDetails { get; set; }
    }
}
