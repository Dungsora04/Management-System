using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Order
    {
        public int Order_Id { get; set; }
        public DateTime Orders_Date { get; set;}
        public int Customer_Id { get; set;}
        public int Users_Id { get; set; }
        public int Total_Amount { get; set; }
        public int Paid_Amount { get; set;}
        public int Due_Amount { get; set; }
        public int Discount { get; set; }
        public int Grand_Total { get; set; }

    }
}
