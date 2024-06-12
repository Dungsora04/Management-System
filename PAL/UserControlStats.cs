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
    public partial class UserControlStats : UserControl
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=CSMS;Integrated Security=True;MultipleActiveResultSets=True;";

        public UserControlStats()
        {
            InitializeComponent();
            int currentYear = DateTime.Now.Year;
            cmbYearSelect.Items.Clear();
            for (int year = currentYear; year > currentYear - 3; year--)
            {
                cmbYearSelect.Items.Add(year);
            }
            cmbYearSelect.SelectedIndex = 0;
            lblYearSelect.Text = cmbYearSelect.SelectedItem.ToString();
            lblYearSelect1.Text = cmbYearSelect.SelectedItem.ToString();
            chartMonthlyProfit.ChartAreas[0].AxisX.Interval = 1;
            FillChartProfit();
            Fill_Table();
        }

        void FillChartProfit()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "DECLARE @year INT;\r\n" +
                               "SET @year = @yearselect;\r\n;" +
                               "WITH Months(Month, MonthName) AS \r\n" +
                               "(\r\n" +
                               "SELECT 1, CAST('Jan' as varchar(10))\r\n" +
                               "UNION ALL\r\n" +
                               "SELECT Month + 1,  CASE Month + 1  WHEN 2 THEN CAST('Fed' as varchar(10)) WHEN 3 THEN CAST('Mar' as varchar(10))  WHEN 4 THEN CAST('Arp' as varchar(10))\r\n            WHEN 5 THEN CAST('May' as varchar(10))\r\n            WHEN 6 THEN CAST('Jun' as varchar(10))\r\n            WHEN 7 THEN CAST('Jul' as varchar(10))\r\n            WHEN 8 THEN CAST('Aug' as varchar(10))\r\n            WHEN 9 THEN CAST('Sep' as varchar(10))\r\n            WHEN 10 THEN CAST('Oct' as varchar(10))\r\n            WHEN 11 THEN CAST('Nov' as varchar(10))\r\n            WHEN 12 THEN CAST('Dec' as varchar(10))\r\n        " +
                               "END\r\n" +
                               "FROM Months\r\n" +
                               "WHERE Month < 12\r\n" +
                               ")\r\n" +
                               "SELECT \r\n" +
                               "m.MonthName,\r\n" +
                               "ISNULL(SUM(o.Grand_Total), 0) AS Revenue\r\n" +
                               "FROM Months m LEFT JOIN Orders o ON MONTH(o.Orders_Date) = m.Month AND YEAR(o.Orders_Date) = @year\r\n" +
                               "GROUP BY m.MonthName, m.Month\r\n" +
                               "ORDER BY m.Month;";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@yearselect", (int)cmbYearSelect.SelectedItem);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        chartMonthlyProfit.DataSource = dataTable;
                    }
                }
            }
            chartMonthlyProfit.Series["Revenue"].XValueMember = "MonthName";
            chartMonthlyProfit.Series["Revenue"].YValueMembers = "Revenue";
            chartMonthlyProfit.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartMonthlyProfit.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chartMonthlyProfit.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartMonthlyProfit.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            chartMonthlyProfit.Invalidate();
        }

        private void Fill_Table() 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DECLARE @Year INT;\r\n" +
                               "SET @Year = @yearselect;\r\n" +
                               "SELECT TOP 10 p.Product_Name, SUM(oi.Orders_Quantity) AS Total_Sold\r\n" +
                               "FROM OrdersInfo oi JOIN Product p ON oi.Product_Id = p.Product_Id JOIN Orders o ON oi.Orders_Id = o.Orders_Id\r\n" +
                               "WHERE YEAR(o.Orders_Date) = @Year\r\n" +
                               "GROUP BY p.Product_Name\r\n" +
                               "ORDER BY Total_Sold DESC;";
                using (SqlCommand command1 = new SqlCommand(query, connection))
                {
                    command1.Parameters.AddWithValue("@yearselect", (int)cmbYearSelect.SelectedItem);
                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvTopSell.DataSource = dataTable;
                    }
                }
            }
        }

        private void cmbYearSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillChartProfit();
            Fill_Table();
            lblYearSelect.Text = cmbYearSelect.SelectedItem.ToString();
            lblYearSelect1.Text = cmbYearSelect.SelectedItem.ToString();
        }

        private void dgvTopSell_MouseEnter(object sender, EventArgs e)
        {
            Fill_Table();
        }

        private void chartMonthlyProfit_MouseEnter(object sender, EventArgs e)
        {
            FillChartProfit();
        }
    }

}
