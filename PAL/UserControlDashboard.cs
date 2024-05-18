using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Management_System.PAL
{
    public partial class UserControlDashboard : UserControl
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=CSMS;Integrated Security=True;";
        public UserControlDashboard()
        {
            InitializeComponent();
        }

        public void Count()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT COUNT(*) FROM Product", connection))
                {
                    label2.Text = ((int)command1.ExecuteScalar() + 1).ToString();
                }

                using (SqlCommand command1 = new SqlCommand("SELECT COUNT(*) FROM Orders WHERE Orders_Id is not null", connection))
                {
                    label3.Text = ((int)command1.ExecuteScalar() + 1).ToString();
                }

                using (SqlCommand command1 = new SqlCommand("SELECT COUNT(*) FROM Product where Product_Status = 'Not Available';", connection))
                {
                    label5.Text = ((int)command1.ExecuteScalar() + 1).ToString();
                }
                
                
                using (SqlCommand command1 = new SqlCommand("SELECT SUM(Grand_Total) FROM Orders", connection))
                {
                    label7.Text = ((int)command1.ExecuteScalar() + 1).ToString("N0",
                    System.Globalization.CultureInfo.GetCultureInfo("de"));
                }
                
                
                
            }
            
        }

        private void UserControlDashboard_Load(object sender, EventArgs e)
        {
            Count();
        }
    }
}
