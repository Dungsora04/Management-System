using System;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms;

using BusinessLogicLayer;
using DTO;

//using Microsoft.ReportingServices.Diagnostics.Internal;
//using Microsoft.Reporting.Map.WebForms.BingMaps;
//using System.Management;
//using static System.ComponentModel.Design.ObjectSelectorEditor;
//using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;
//using System.Security.Cryptography;
//using System.Diagnostics.Eventing.Reader;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Drawing.Printing;
//using System.Collections.Generic;
//using System.ComponentModel;

namespace Management_System.PAL
{
    public partial class UserControlOrder : UserControl
    {
        private class Item
        {
            public string Text { get; set; }
            public int Id { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        Order order = new Order();
        OrderBUS orderbus = new OrderBUS();
        ProductBUS productbus = new ProductBUS();
        string warranty = "";
        //string warranty_days = "";
        string product_detail = "";
        private string connectionString = "Data Source=localhost;Initial Catalog=CSMS;Integrated Security=True;";
        private string Id = "";
        public string name;
        private Int32 userId = 0;
        private string RoleId = "";
        int a;
        int oTotal = 0;

        public UserControlOrder()
        {
            InitializeComponent();
            name = FormLogIn.SharedData.ValueToPass;
            lblUserName.Text = FormLogIn.SharedData.ValueToPass;
            RoleId = FormLogIn.SharedData1.ValueToPass1;
            if (RoleId != "1")
            {
                btnChange.Enabled = false;
                btnRemove.Enabled = false;
            }
        }
        public void EmptyBox()
        {
            dtpDate.Value = DateTime.Now;
            txtCustomerName.Clear();
            mtbCustomerNumber.Clear();
            AddClear();
            dgvProductList.Rows.Clear();
            txtTotalAmount.Text = "0";
            nudPaidAmount.Value = 0;
            txtDueAmount.Text = "0";
            nudDiscount.Value = 0;
            txtGrandTotal.Text = "0";
            a = 0;
            oTotal = 0;
            cmbDiscount.SelectedIndex = 1;

        }
        public void Display()
        {
            try
            {
                dgvOrder_Product.DataSource = productbus.GetDataProduct();
            }
            catch
            {
                MessageBox.Show("View Product is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void AddClear()
        {
            cmbProduct.Items.Clear();
            cmbProduct.Items.Add("-- SELECT --");
            cmbProduct.SelectedIndex = 0;
            txtRate.Clear();
            nudQuantity.Value = 0;
            txtTotal.Clear();
            nudDiscount.Value = 0;
            cmbDiscount.SelectedIndex = 1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT Product_Name,Product_Id From Product Where Product_Status = 'Available' order By Product_Name", connection))
                {

                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item item = new Item();
                            item.Text = reader.GetString(0);
                            item.Id = reader.GetInt32(1);
                            cmbProduct.Items.Add(item);
                        }
                    }
                    command1.ExecuteNonQuery();
                }

            }
            Display();
        }

        private void EmptyBox1()
        {
            dtpDate.Value = DateTime.Now;
            txtCustomerName1.Clear();
            mtbCustomerNumber1.Clear();
            dgvProductList.Rows.Clear();
            txtTotalAmount1.Text = "0";
            nudPaidAmount1.Value = 0;
            txtDueAmount1.Text = "0";
            nudDiscount1.Value = 0;
            txtGrandTotal1.Text = "0";


            Id = "";

        }



        RichTextBox richTextBox = new RichTextBox();




        private void btnAdd_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAdd, "Add");
        }

        private void picSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch, "Search");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //int check = 0;
            lblUserName.Text = FormLogIn.SharedData.ValueToPass;
            //MessageBox.Show(lblUserName.Text, userId.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (cmbProduct.SelectedIndex == 0)
            {
                MessageBox.Show("Choose Your Product.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtCustomerName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter your name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudQuantity.Value == 0)
            {
                MessageBox.Show("Please enter quantity.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (!mtbCustomerNumber.MaskCompleted)
            {
                MessageBox.Show("Please enter your phone number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {

                if (nudQuantity.Value > 0)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        //var rate_hold = -1;
                        using (SqlCommand command2 = new SqlCommand("SELECT Product_Warranty FROM Product WHERE Product_Name = @Product_Name;", connection))
                        {
                            command2.Parameters.AddWithValue("@Product_Name", (cmbProduct.SelectedItem as Item).Text.ToString());
                            SqlDataReader reader1 = command2.ExecuteReader();

                            while (reader1.Read())
                            {
                                string Product_War = reader1["Product_Warranty"].ToString();
                                warranty = Product_War;

                            }
                            reader1.Close();
                        }
                    }
                    int rate, total;
                    Int32.TryParse(txtRate.Text, out rate);
                    Int32.TryParse(txtTotal.Text, out total);
                    if (dgvProductList.Rows.Count != 0)
                    {

                        int flag = 0;
                        foreach (DataGridViewRow rows in dgvProductList.Rows)
                        {
                            //MessageBox.Show((cmbProduct.SelectedItem as Item).Id.ToString(), rows.Cells[0].Value.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (rows.Cells[0].Value.ToString() == (cmbProduct.SelectedItem as Item).Id.ToString())
                            {
                                DateTime BuyDay = dtpDate.Value;
                                int day = BuyDay.Day;
                                int month = BuyDay.Month;
                                int year = BuyDay.Year;
                                DateTime date1 = new DateTime(year, month, day);
                                DateTime date2 = DateTime.Now;

                                TimeSpan difference = date2 - date1;
                                int days = (int)difference.TotalDays;
                                DateTime resultDate = BuyDay.AddDays(Convert.ToUInt32(warranty) - days);
                                warranty = resultDate.ToString();
                                int quantity = Convert.ToInt32(rows.Cells[3].Value.ToString());
                                int total1 = Convert.ToInt32(rows.Cells[5].Value.ToString());
                                quantity += Convert.ToInt32(nudQuantity.Value);
                                total1 += total;
                                rows.Cells[3].Value = quantity;
                                rows.Cells[4].Value = warranty;
                                rows.Cells[5].Value = total1;
                                AddClear();
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 0)
                        {
                            DateTime BuyDay1 = dtpDate.Value;
                            int day1 = BuyDay1.Day;
                            int month1 = BuyDay1.Month;
                            int year1 = BuyDay1.Year;
                            DateTime date11 = new DateTime(year1, month1, day1);
                            DateTime date21 = DateTime.Now;

                            TimeSpan difference1 = date21 - date11;
                            int days1 = (int)difference1.TotalDays;
                            DateTime resultDate = BuyDay1.AddDays(Convert.ToUInt32(warranty) - days1);
                            warranty = resultDate.ToString();

                            txtTotal.Text = (rate * Convert.ToInt32(nudQuantity.Value)).ToString();
                            string[] row =
                            {
                                        (cmbProduct.SelectedItem as Item).Id.ToString(), (cmbProduct.SelectedItem as Item).Text.ToString(), txtRate.Text, nudQuantity.Value.ToString(),  warranty, txtTotal.Text
                                    };
                            dgvProductList.Rows.Add(row);
                            AddClear();
                        }
                    }
                    else
                    {
                        DateTime BuyDay = dtpDate.Value;
                        int day = BuyDay.Day;
                        int month = BuyDay.Month;
                        int year = BuyDay.Year;
                        DateTime date1 = new DateTime(year, month, day);
                        DateTime date2 = DateTime.Now;

                        TimeSpan difference = date2 - date1;
                        int days = (int)difference.TotalDays;
                        DateTime resultDate = BuyDay.AddDays(Convert.ToUInt32(warranty) - days);
                        warranty = resultDate.ToString();

                        txtTotal.Text = (rate * Convert.ToInt32(nudQuantity.Value)).ToString();
                        string[] row =
                        {
                                        (cmbProduct.SelectedItem as Item).Id.ToString(), (cmbProduct.SelectedItem as Item).Text.ToString(), txtRate.Text, nudQuantity.Value.ToString(), warranty, txtTotal.Text
                                    };
                        dgvProductList.Rows.Add(row);
                        AddClear();
                    }

                }

                txtTotalAmount.Text = oTotal.ToString();

            }

            foreach (DataGridViewRow row in dgvProductList.Rows)
            {
                oTotal += Convert.ToInt32(row.Cells[5].Value.ToString());

                txtTotalAmount.Text = oTotal.ToString();
            }

            a = oTotal;

            txtDueAmount.Text = "-" + a.ToString();
            oTotal = 0;
            lblTotal.Text = dgvOrders.Rows.Count.ToString();

        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var rate_hold = -1;
                using (SqlCommand command2 = new SqlCommand("SELECT Product_Warranty FROM Product WHERE Product_Name = @Product_Name;", connection))
                {
                    command2.Parameters.AddWithValue("@Product_Name", cmbProduct.SelectedItem.ToString());
                    SqlDataReader reader1 = command2.ExecuteReader();

                    while (reader1.Read())
                    {
                        string Product_War = reader1["Product_Warranty"].ToString();
                        warranty = Product_War;

                    }
                    reader1.Close();
                }
                using (SqlCommand command1 = new SqlCommand("SELECT Product_Price FROM Product WHERE Product_Name = @ProductName;", connection))
                {
                    command1.Parameters.AddWithValue("@ProductName", cmbProduct.SelectedItem.ToString());
                    SqlDataReader reader = command1.ExecuteReader();
                    while (reader.Read())
                    {
                        rate_hold = reader.GetInt32(0);
                    }
                    if (rate_hold != -1)
                    {
                        txtRate.Text = rate_hold.ToString();
                    }

                    //command1.ExecuteNonQuery();
                    reader.Close();
                }
            }
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            int rate;
            Int32.TryParse(txtRate.Text, out rate);
            txtTotal.Text = (rate * Convert.ToInt32(nudQuantity.Value)).ToString();
        }

        private void dgvProduct(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                int rowIndex = dgvProductList.CurrentCell.RowIndex;
                dgvProductList.Rows.RemoveAt(rowIndex);
                cmbDiscount.SelectedIndex = 1;
                nudDiscount.Value = 0;
                if (dgvProductList.Rows.Count != 0)
                {


                    foreach (DataGridViewRow rows in dgvProductList.Rows)
                    {
                        oTotal += Convert.ToInt32(rows.Cells[5].Value.ToString());
                        a = oTotal;
                        //MessageBox.Show(oTotal.ToString(), "a", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTotalAmount.Text = oTotal.ToString();
                        txtDueAmount.Text = (Convert.ToInt32(nudPaidAmount.Value) - Convert.ToInt32(txtTotalAmount.Text) + Convert.ToInt32(nudDiscount.Value)).ToString();
                    }
                }
                else
                {
                    txtTotalAmount.Text = "0";
                    txtDueAmount.Text = "0";
                    nudPaidAmount.Value = 0;
                    cmbDiscount.Text = "";
                    nudDiscount.Value = 0;
                    a = 0;
                }
                oTotal = 0;

            }
        }

        private void nudPaidAmount_ValueChanged(object sender, EventArgs e)
        {
            txtDueAmount.Text = (Convert.ToInt32(nudPaidAmount.Value) - Convert.ToInt32(txtGrandTotal.Text)).ToString();

        }

        private void nudDiscount_ValueChanged(object sender, EventArgs e)
        {
            txtGrandTotal.Text = (Convert.ToInt32(txtTotalAmount.Text) - Convert.ToInt32(nudDiscount.Value)).ToString();

        }

        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            nudPaidAmount_ValueChanged(sender, e);
            nudDiscount_ValueChanged(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvProductList.Rows.Count == 0)
            {
                MessageBox.Show("Choose Your Product.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtCustomerName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter your name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (!mtbCustomerNumber.MaskCompleted)
            {
                MessageBox.Show("Please enter your phone number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbDiscount.SelectedItem == "-- SELECT -- " || cmbDiscount.SelectedItem == "")
            {
                MessageBox.Show("Please choose your discount offer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudPaidAmount.Value == 0)
            {
                MessageBox.Show("Please enter your money", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command1 = new SqlCommand("INSERT INTO Customer (Customer_Name, Customer_Number) " +
                                                "OUTPUT inserted.Customer_Id Values ( @Customer_Name, @Customer_Number);", connection))
                    {
                        command1.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text.Trim());
                        command1.Parameters.AddWithValue("@Customer_Number", mtbCustomerNumber.Text.Trim());

                        command1.ExecuteNonQuery();
                    }

                    using (SqlCommand command2 = new SqlCommand("SELECT Customer_Name,Customer_Id FROM Customer", connection))
                    {
                        SqlDataReader reader = command2.ExecuteReader();
                        while (reader.Read())
                        {
                            if (txtCustomerName.Text.Trim() == reader.GetString(0)) txtCustomerName.Text = reader.GetInt32(1).ToString();
                        }
                        reader.Close();
                    }

                    using (SqlCommand command2 = new SqlCommand("SELECT Users_Name,Users_Id FROM Users", connection))
                    {
                        SqlDataReader reader = command2.ExecuteReader();
                        while (reader.Read())
                        {
                            if (name == reader.GetString(0))
                            {
                                userId = reader.GetInt32(1);
                            }
                        }
                        reader.Close();
                    }

                    // proc done
                    //MessageBox.Show(txtCustomerName.Text, dgvProductList.Rows.Count.ToString() + "before", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    order.Orders_Date = dtpDate.Value.Date;
                    order.Customer_Id = Convert.ToInt32(txtCustomerName.Text);
                    order.Users_Id = userId;
                    order.Total_Amount = Convert.ToInt32(txtTotalAmount.Text);
                    order.Paid_Amount = Convert.ToInt32(nudPaidAmount.Value);
                    order.Due_Amount = Convert.ToInt32(txtDueAmount.Text);
                    order.Discount = Convert.ToInt32(nudDiscount.Value);
                    order.Grand_Total = Convert.ToInt32(txtGrandTotal.Text);
                    try
                    {
                        //MessageBox.Show(order.Users_Id.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        orderbus.Insert(order);
                        MessageBox.Show("Adding Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Adding Fail!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //MessageBox.Show(txtCustomerName.Text, dgvProductList.Rows.Count.ToString() + "after", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //using (SqlCommand command1 = new SqlCommand("INSERT INTO Orders (Orders_Date, Customer_Id, Users_Id, Total_Amount, Paid_Amount, Due_Amount, Discount, Grand_Total) " +
                    //                        "OUTPUT inserted.Orders_Id Values (@OrdersDate, @Customer_Id, @Users_Id, @TotalAmount, @PaidAmount, @DueAmount, @Discount, @GrandTotal);", connection))
                    //{
                    //    //MessageBox.Show(name, userId.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    command1.Parameters.AddWithValue("@Customer_Id", Convert.ToInt32(txtCustomerName.Text.Trim()));
                    //    command1.Parameters.AddWithValue("@OrdersDate", dtpDate.Value.Date);
                    //    command1.Parameters.AddWithValue("@Users_Id", userId);
                    //    command1.Parameters.AddWithValue("@TotalAmount", Convert.ToInt32(txtTotalAmount.Text));
                    //    command1.Parameters.AddWithValue("@PaidAmount", Convert.ToInt32(nudPaidAmount.Value));
                    //    command1.Parameters.AddWithValue("@DueAmount", Convert.ToInt32(txtDueAmount.Text));
                    //    command1.Parameters.AddWithValue("@Discount", nudDiscount.Value);
                    //    command1.Parameters.AddWithValue("@GrandTotal", txtGrandTotal.Text);
                    //    command1.ExecuteNonQuery();
                    //    //command1.Parameters.AddWithValue("@Customer_Id", txtCustomerName.Text.Trim());
                    //}
                    // done proc


                    DataTable a;
                    a = orderbus.GetData_MaxOrderId();
                    txtCustomerName.Text = a.Rows[0][0].ToString();
                    //MessageBox.Show(a.Rows[0][0].ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //using (SqlCommand command2 = new SqlCommand("SELECT Orders_Id\r\nFROM Orders\r\nWHERE Orders_Id = (SELECT MAX(Orders_Id) FROM Orders);", connection))
                    //{
                    //    SqlDataReader reader = command2.ExecuteReader();
                    //    while (reader.Read())
                    //    {
                    //            int intValue = reader.GetInt32(0);
                    //            txtCustomerName.Text = intValue.ToString();
                    //    }
                    //    reader.Close();
                    //}

                    for (int i = 0; i < dgvProductList.Rows.Count; i++)
                    {
                        using (SqlCommand command1 = new SqlCommand("INSERT INTO OrdersInfo (Orders_Id, Product_Id, Orders_Quantity, Warranty)" +
                                                "OUTPUT inserted.OrdersInfo_Id Values (@Orders_Id, @Product_Id, @Orders_Quantity, @Warranty);", connection))
                        {
                            command1.Parameters.AddWithValue("@Orders_Id", Convert.ToInt32(txtCustomerName.Text.Trim()));
                            command1.Parameters.AddWithValue("@Product_Id", Convert.ToInt32(dgvProductList.Rows[i].Cells[0].Value.ToString()));
                            command1.Parameters.AddWithValue("@Orders_Quantity", Convert.ToInt32(dgvProductList.Rows[i].Cells[3].Value.ToString()));
                            command1.Parameters.AddWithValue("@Warranty", dgvProductList.Rows[i].Cells[4].Value.ToString());
                            command1.ExecuteNonQuery();
                            tpManageOrders_Enter(sender, e);

                        }

                    }
                    EmptyBox();
                }
                product_detail = "";

            }
        }

        private void Receipt()
        {
            richTextBox.Clear();
            richTextBox.Text += "\t        COMPUTER SHOP MANAGEMENT SYSTEM\n";
            richTextBox.Text += "\t *********************************************************\n\n";
            richTextBox.Text += "  Date: " + dtpDate.Text.Trim() + "\n";
            richTextBox.Text += "  Name: " + txtCustomerName.Text.ToString().Trim() + "\n";
            richTextBox.Text += "  Customer Number: " + mtbCustomerNumber.Text.ToString().Trim() + "\n\n";
            richTextBox.Text += "\t *********************************************************\n\n";
            richTextBox.Text += "  Name\t\tRate\t\tQuantity\t\tTotal\n";


            for (int i = 0; i < dgvProductList.Rows.Count; i++)
            {
                richTextBox.Text += "  ";
                for (int j = 0; j < dgvProductList.Columns.Count - 1; j++)
                {
                    string str = dgvProductList.Rows[i].Cells[j].Value.ToString();
                    if (str.Length <= 8)
                    {
                        richTextBox.Text += dgvProductList.Rows[i].Cells[j].Value.ToString() + "\t\t";
                    }
                    else
                    {

                        string desc = "";
                        desc = dgvProductList.Rows[i].Cells[j].Value.ToString();
                        for (int k = 0; k <= 8; k++)
                        {
                            richTextBox.Text += desc[k];
                        }
                        richTextBox.Text += "..  ";
                    }
                }
                richTextBox.Text += "\t\t\t";
                richTextBox.Text += "\n";
            }


            richTextBox.Text += "\t *********************************************************\n\n";
            richTextBox.Text += "\t\t\t\t\tTotal: $" + txtTotalAmount.Text + "\n";
            richTextBox.Text += "\t\t\t\t\tPaid Amount: $" + nudPaidAmount.Text + "\n";
            richTextBox.Text += "\t\t\t\t\tDue Amount: $" + txtDueAmount.Text + "\n";
            richTextBox.Text += "\t\t\t\t\tDiscount: $" + nudDiscount.Text + "\n";
            richTextBox.Text += "\t\t\t\t\tGrand Total: $" + txtGrandTotal.Text + "\n";
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            Receipt();
            printPreviewDialog.Document = printDocument;
            printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            printPreviewDialog.ShowDialog();

        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox.Text, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new System.Drawing.Point(10, 10));

        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            nudPaidAmount_ValueChanged(sender, e);

        }

        private void tpManageOrders_Enter(object sender, EventArgs e)
        {
            txtSearchCustomerName.Clear();
            dgvOrders.Columns[0].Visible = false;

            // proc done

            try
            {
                dgvOrders.DataSource = orderbus.GetDataDGV();
                lblTotal.Text = dgvOrders.Rows.Count.ToString();

            }
            catch
            {
                MessageBox.Show("View Order is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command1 = new SqlCommand("SELECT o.Orders_Id, o.Orders_Date, c.Customer_Name, c.Customer_Number, o.Total_Amount, o.Paid_Amount, o.Due_Amount, o.Discount, o.Grand_Total, u.Users_Name\r\nFROM Orders o\r\nINNER JOIN Customer c\r\nON o.Customer_Id = c.Customer_Id\r\nINNER JOIN Users u\r\nON o.Users_Id = u.Users_Id", connection))
            //    {

            //        using (var reader = command1.ExecuteReader())
            //        {
            //            var dataTable = new DataTable();
            //            dataTable.Load(reader);
            //            dgvOrders.DataSource = dataTable;


            //        }

            //        //   txtSearchCustomerName.Clear();
            //        //dgvOrders.Columns[0].Visible = false;
            //        lblTotal.Text = dgvOrders.Rows.Count.ToString();

            //    }
            //}




        }

        private void txtSearchCustomerName_TextChanged(object sender, EventArgs e)
        {
            // done proc

            try
            {
                dgvOrders.DataSource = orderbus.GetDataByName(txtSearchCustomerName.Text);
                lblTotal.Text = dgvOrders.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("Search Bar is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command1 = new SqlCommand("SELECT o.Orders_Id, o.Orders_Date, c.Customer_Name, c.Customer_Number, o.Total_Amount, o.Paid_Amount, o.Due_Amount, o.Discount, o.Grand_Total, u.Users_Name\r\nFROM Orders o\r\nINNER JOIN Customer c\r\nON o.Customer_Id = c.Customer_Id\r\nINNER JOIN Users u\r\nON o.Users_Id = u.Users_Id WHERE Customer_Name LIKE '%" + txtSearchCustomerName.Text + "%';", connection))
            //    {

            //        using (var reader = command1.ExecuteReader())
            //        {
            //            var dataTable = new DataTable();
            //            dataTable.Load(reader);
            //            dgvOrders.DataSource = dataTable;
            //            //command1.ExecuteNonQuery();
            //        }
            //        lblTotal.Text = dgvOrders.Rows.Count.ToString();
            //    }
            //}
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //dgvDetails.Rows.Clear();
                DataGridViewRow row = dgvOrders.Rows[e.RowIndex];

                Id = row.Cells[0].Value.ToString();
                dtpDate1.Text = row.Cells[1].Value.ToString();
                txtCustomerName1.Text = row.Cells[2].Value.ToString();
                mtbCustomerNumber1.Text = row.Cells[3].Value.ToString();
                txtTotalAmount1.Text = row.Cells[4].Value.ToString();
                nudPaidAmount1.Value = Convert.ToInt32(row.Cells[5].Value.ToString());
                txtDueAmount1.Text = row.Cells[6].Value.ToString();
                nudDiscount1.Value = Convert.ToInt32(row.Cells[7].Value.ToString());
                txtGrandTotal1.Text = row.Cells[8].Value.ToString();
                txtUserName1.Text = row.Cells[9].Value.ToString();

                // proc done
                //dgvDetails.Columns[0].Visible = false;

                try
                {
                    dgvDetails.DataSource = orderbus.GetDataByCustomerProduct(Convert.ToInt32(Id));
                    lblTotal.Text = dgvDetails.Rows.Count.ToString();
                }
                catch
                {
                    MessageBox.Show("Search Bar is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    connection.Open();
                //    using (SqlCommand command1 = new SqlCommand("SELECT p.Product_Name, p.Product_Warranty, i.Orders_Quantity, p.Product_Price*i.Orders_Quantity AS Total\r\nFROM OrdersInfo i\r\nINNER JOIN Product p\r\nON i.Product_Id = p.Product_Id\r\nwhere i.Orders_Id = @orderId ", connection)) //"+p_detals+"
                //    {
                //        command1.Parameters.AddWithValue("@orderId", Convert.ToInt32(row.Cells[0].Value.ToString()));

                //        using (var reader = command1.ExecuteReader())
                //        {
                //            var dataTable = new DataTable();
                //            dataTable.Load(reader);
                //            dgvDetails.DataSource = dataTable;
                //        }
                //        lblTotal.Text = dgvDetails.Rows.Count.ToString();
                //    }
                //}
                //foreach (char c in p_detals)
                //{
                //    if (c == '/')
                //    {
                //        count++;
                //    }
                //}
                //if (count >= 1)
                //{
                //    more_details = p_detals.Split('/');

                //    string[] a = new string[100];
                //    foreach (string word in more_details)
                //    {
                //        m_details = word.Split(',');
                //        int cnt = 0;
                //        foreach (string word1 in m_details)
                //        {
                //            a[cnt] = word1;
                //            cnt++;

                //        }
                //        DateTime BuyDay = DateTime.Parse(row.Cells[1].Value.ToString());
                //        int day = BuyDay.Day;
                //        int month = BuyDay.Month;
                //        int year = BuyDay.Year;
                //        DateTime date1 = new DateTime(year, month, day);
                //        DateTime date2 = DateTime.Now;
                //        TimeSpan differece = date2 - date1;
                //        int days = (int)differece.TotalDays;
                //        if (Convert.ToInt32(a[3]) > 0)
                //        {
                //            a[3] = (Convert.ToInt32(a[3]) - days).ToString();
                //            string[] row1 =
                //            {
                //           a[0], a[1], a[2], a[3]
                //        };
                //            dgvDetails.Rows.Add(row1);
                //            row1 = null;
                //        }
                //        else
                //        {
                //            string[] row1 =
                //            {
                //           a[0], a[1], a[2], "0"
                //        };
                //            dgvDetails.Rows.Add(row1);
                //            row1 = null;
                //        }
                //    }
                //}
                //else
                //{
                //    DateTime BuyDay = DateTime.Parse(row.Cells[1].Value.ToString());
                //    int day = BuyDay.Day;
                //    int month = BuyDay.Month;
                //    int year = BuyDay.Year;
                //    DateTime date1 = new DateTime(year, month, day);
                //    DateTime date2 = DateTime.Now;
                //    TimeSpan differece = date2 - date1;
                //    int days = (int)differece.TotalDays;
                //    int cnt = 0;
                //    more_details = p_detals.Split(',');
                //    string[] a = new string[100];
                //    foreach (string word in more_details)
                //    {
                //        m_details = word.Split(',');
                //        a[cnt] = word;
                //        cnt++;

                //    }
                //    if (Convert.ToInt32(a[3]) > 0)
                //    {
                //        a[3] = (Convert.ToInt32(a[3]) - days).ToString();
                //        string[] row1 =
                //          {
                //           a[0], a[1], a[2], a[3]
                //        };
                //        dgvDetails.Rows.Add(row1);
                //        row1 = null;
                //    }
                //    else
                //    {
                //        string[] row1 =
                //     {
                //           a[0], a[1], a[2], "0"
                //        };
                //        dgvDetails.Rows.Add(row1);
                //        row1 = null;
                //    }
                //}



                tcOrders.SelectedTab = tpOptions;

            }
        }

        private void tpOptions_Enter(object sender, EventArgs e)
        {
            if (Id == "")
            {
                tcOrders.SelectedTab = tpManageOrders;
            }
        }

        private void tpOptions_Leave(object sender, EventArgs e)
        {
            EmptyBox1();
        }



        private void tpAddOrder_Enter(object sender, EventArgs e)
        {
            EmptyBox();

        }

        private void nudPaidAmount1_ValueChanged(object sender, EventArgs e)
        {
            txtDueAmount1.Text = (Convert.ToInt32(nudPaidAmount1.Value) - Convert.ToInt32(txtTotalAmount1.Text)).ToString();
        }

        private void nudDiscount1_ValueChanged(object sender, EventArgs e)
        {
            txtGrandTotal1.Text = (Convert.ToInt32(txtTotalAmount1.Text) - Convert.ToInt32(nudDiscount1.Value)).ToString();
        }

        private void txtTotalAmount1_TextChanged(object sender, EventArgs e)
        {
            nudPaidAmount1_ValueChanged(sender, e);
            nudDiscount1_ValueChanged(sender, e);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (Id == "")
            {
                MessageBox.Show("Id.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtCustomerName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Nhập vào tên khách hàng.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (!mtbCustomerNumber1.MaskCompleted)
            {
                MessageBox.Show("Nhập vào số điện thoại khách hàng.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudPaidAmount1.Value == 0)
            {
                MessageBox.Show("Nhập vào số tiền chi trả.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you want to delete this product", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    // done order proc
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command1 = new SqlCommand("DELETE FROM Orders WHERE Orders_Id = @productid", connection))
                        using (SqlCommand command = new SqlCommand("DELETE FROM OrdersInfo WHERE Orders_Id = @productid", connection))
                        {

                            command.Parameters.AddWithValue("@productid", Id);
                            command1.Parameters.AddWithValue("@productid", Id);
                            int rowsAffected = command.ExecuteNonQuery();
                            command1.ExecuteNonQuery();

                            if (rowsAffected > 0)
                                Console.WriteLine($"Row with ID {Id} deleted successfully.");
                            else
                                Console.WriteLine($"No rows found with ID {Id}.");
                            tcOrders.SelectedTab = tpManageOrders;
                            EmptyBox1();
                        }
                        /*
                               using (SqlCommand command1 = new SqlCommand("DELETE FROM Orders WHERE Orders_Id = @productid", connection))
                               {

                                   command1.Parameters.AddWithValue("@productid", Id);
                                   int rowsAffected = command1.ExecuteNonQuery();

                                   if (rowsAffected > 0)
                                       Console.WriteLine($"Row with ID {Id} deleted successfully.");
                                   else
                                       Console.WriteLine($"No rows found with ID {Id}.");
                                   EmptyBox1();
                                   tcOrders.SelectedTab = tpManageOrders;
                                   tpManageOrders_Enter(sender, e);
                               }
                        */


                    }
                }
            }
        }

        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // proc DGV

            try
            {
                dgvOrders.DataSource = orderbus.GetDataDGV();
                lblTotal.Text = dgvOrders.Rows.Count.ToString();

            }
            catch
            {
                MessageBox.Show("View Order is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command1 = new SqlCommand("SELECT o.Orders_Id, o.Orders_Date, c.Customer_Name, c.Customer_Number, o.Total_Amount, o.Paid_Amount, o.Due_Amount, o.Discount, o.Grand_Total, u.Users_Name\r\nFROM Orders o\r\nINNER JOIN Customer c\r\nON o.Customer_Id = c.Customer_Id\r\nINNER JOIN Users u\r\nON o.Users_Id = u.Users_Id;", connection))
            //    {

            //        using (var reader = command1.ExecuteReader())
            //        {
            //            var dataTable = new DataTable();
            //            dataTable.Load(reader);
            //            dgvOrders.DataSource = dataTable;
            //            command1.ExecuteNonQuery();
            //        }


            //    }
            //}

        }


        private void btnChange_Click(object sender, EventArgs e)
        {
            if (Id == "")
            {
                MessageBox.Show("Hàng đầu tiên.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtCustomerName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Nhập vào tên khách hàng.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (!mtbCustomerNumber1.MaskCompleted)
            {
                MessageBox.Show("Nhập vào số điện thoại khách hàng.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudPaidAmount1.Value == 0)
            {
                MessageBox.Show("Nhập vào số tiền chi trả.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();

                    //   using (SqlCommand cmd = new SqlCommand("ALTER TABLE Orders DROP CONSTRAINT Customer_Number", connection))

                    using (SqlCommand command2 = new SqlCommand("SELECT Customer_Name,Customer_Id FROM Customer", connection))
                    {
                        SqlDataReader reader = command2.ExecuteReader();
                        while (reader.Read())
                        {
                            if (txtCustomerName1.Text.Trim() == reader.GetString(0))
                            {
                                txtCustomerName1.Text = reader.GetInt32(1).ToString();

                            }
                        }
                        //foreach (DataGridViewRow row in dgvBrand.Rows)
                        //{
                        //    txtCustomerName1.Text  = row.Cells["qty"].Value.ToString();
                        //    //More code here
                        //}
                        reader.Close();
                    }

                    // done proc 
                    //      using (SqlCommand cmd = new SqlCommand("update Orders set Orders_Date = @OrdersDate, Customer_Name = @CustomerName, Customer_Number= @CustomerNumber, Total_Amount = @TotalAmount, Paid_Amount = @PaidAmount, Due_Amount = @DueAmount, Discount = @Discount, Grand_Total = @GrandTotal, Payment_Status = @PaymentStatus\r\nwhere Orders_Id = " + Id+ ";", connection))
                    using (SqlCommand command1 = new SqlCommand("UPDATE Orders SET Orders_Date = @OrdersDate where Orders_Id= @Orders_Id", connection))
                    using (SqlCommand command2 = new SqlCommand("UPDATE Orders SET Customer_Id = @Customer_Id where Orders_Id= @Orders_Id", connection))
                    using (SqlCommand command4 = new SqlCommand("UPDATE Orders SET Total_Amount = @TotalAmount where Orders_Id= @Orders_Id", connection))
                    using (SqlCommand command5 = new SqlCommand("UPDATE Orders SET Paid_Amount = @PaidAmount where Orders_Id= @Orders_Id", connection))
                    using (SqlCommand command6 = new SqlCommand("UPDATE Orders SET Due_Amount = @DueAmount where Orders_Id= @Orders_Id", connection))
                    using (SqlCommand command7 = new SqlCommand("UPDATE Orders SET Discount = @Discount where Orders_Id= @Orders_Id", connection))
                    using (SqlCommand command8 = new SqlCommand("UPDATE Orders SET Grand_Total = @GrandTotal where Orders_Id= @Orders_Id", connection))
                    {
                        command1.Parameters.AddWithValue("@OrdersDate", dtpDate1.Value.Date);
                        command1.Parameters.AddWithValue("@Orders_Id", Id);
                        command2.Parameters.AddWithValue("@Customer_Id", Convert.ToInt32(txtCustomerName1.Text.Trim()));
                        command2.Parameters.AddWithValue("@Orders_Id", Id);
                        command4.Parameters.AddWithValue("@TotalAmount", Convert.ToInt32(txtTotalAmount1.Text.Trim()));
                        command4.Parameters.AddWithValue("@Orders_Id", Id);
                        command5.Parameters.AddWithValue("@PaidAmount", Convert.ToInt32(nudPaidAmount1.Value));
                        command5.Parameters.AddWithValue("@Orders_Id", Id);
                        command6.Parameters.AddWithValue("@DueAmount", Convert.ToInt32(txtDueAmount1.Text.Trim()));
                        command6.Parameters.AddWithValue("@Orders_Id", Id);
                        command7.Parameters.AddWithValue("@Discount", nudDiscount1.Value);
                        command7.Parameters.AddWithValue("@Orders_Id", Id);
                        command8.Parameters.AddWithValue("@GrandTotal", txtGrandTotal1.Text);
                        command8.Parameters.AddWithValue("@Orders_Id", Id);



                        command1.ExecuteNonQuery();
                        command2.ExecuteNonQuery();
                        //  command3.ExecuteNonQuery();
                        command4.ExecuteNonQuery();
                        command5.ExecuteNonQuery();
                        command6.ExecuteNonQuery();
                        command7.ExecuteNonQuery();
                        command8.ExecuteNonQuery();
                        tcOrders.SelectedTab = tpManageOrders;
                        tpManageOrders_Enter(sender, e);
                        EmptyBox1();
                    }



                }
            }
        }

        private void tpAddOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show(name, userId.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtSearchCustomerNumber_TextChanged(object sender, EventArgs e)
        {
            // done proc

            try
            {
                dgvOrders.DataSource = orderbus.GetDataByNumber(txtSearchCustomerNumber.Text);
                lblTotal.Text = dgvOrders.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("Search Bar is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command1 = new SqlCommand("SELECT o.Orders_Id, o.Orders_Date, c.Customer_Name, c.Customer_Number, o.Total_Amount, o.Paid_Amount, o.Due_Amount, o.Discount, o.Grand_Total, u.Users_Name\r\nFROM Orders o\r\nINNER JOIN Customer c\r\nON o.Customer_Id = c.Customer_Id\r\nINNER JOIN Users u\r\nON o.Users_Id = u.Users_Id WHERE Customer_Number LIKE '%" + txtSearchCustomerNumber.Text + "%';", connection))
            //    {

            //        using (var reader = command1.ExecuteReader())
            //        {
            //            var dataTable = new DataTable();
            //            dataTable.Load(reader);
            //            dgvOrders.DataSource = dataTable;
            //            //command1.ExecuteNonQuery();
            //        }
            //        lblTotal.Text = dgvOrders.Rows.Count.ToString();

            //    }

            //}
        }

        private void UserControlOrder_Load(object sender, EventArgs e)
        {
            lblUserName.Text = name;
        }



        private void cmbDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {

            oTotal = a;
            if (cmbDiscount.SelectedItem == "5%")
            {

                txtTotalAmount.Text = a.ToString();
                nudDiscount.Value = Convert.ToInt32(oTotal * 0.05);
                txtDueAmount.Text = (Convert.ToInt32(nudPaidAmount.Value) - Convert.ToInt32(txtTotalAmount.Text) + Convert.ToInt32(nudDiscount.Value)).ToString();

                oTotal = 0;

            }
            else if (cmbDiscount.SelectedItem == "10%")
            {

                txtTotalAmount.Text = a.ToString();
                nudDiscount.Value = Convert.ToInt32(oTotal * 0.1);
                txtDueAmount.Text = (Convert.ToInt32(nudPaidAmount.Value) - Convert.ToInt32(txtTotalAmount.Text) + Convert.ToInt32(nudDiscount.Value)).ToString();

                oTotal = 0;

            }
            else if (cmbDiscount.SelectedItem == "15%")
            {

                txtTotalAmount.Text = a.ToString();
                nudDiscount.Value = Convert.ToInt32(oTotal * 0.15);
                txtDueAmount.Text = (Convert.ToInt32(nudPaidAmount.Value) - Convert.ToInt32(txtTotalAmount.Text) + Convert.ToInt32(nudDiscount.Value)).ToString();

                oTotal = 0;

            }
            else if (cmbDiscount.SelectedItem == "20%")
            {

                txtTotalAmount.Text = a.ToString();
                nudDiscount.Value = Convert.ToInt32(oTotal * 0.2);
                txtDueAmount.Text = (Convert.ToInt32(nudPaidAmount.Value) - Convert.ToInt32(txtTotalAmount.Text) + Convert.ToInt32(nudDiscount.Value)).ToString();

                oTotal = 0;

            }
            else if (cmbDiscount.SelectedItem == "25%")
            {

                txtTotalAmount.Text = a.ToString();
                nudDiscount.Value = Convert.ToInt32(oTotal * 0.25);
                txtDueAmount.Text = (Convert.ToInt32(nudPaidAmount.Value) - Convert.ToInt32(txtTotalAmount.Text) + Convert.ToInt32(nudDiscount.Value)).ToString();

                oTotal = 0;

            }
            else
            {
                txtTotalAmount.Text = a.ToString();
                txtGrandTotal.Text = a.ToString();
                nudDiscount.Value = 0;
                txtDueAmount.Text = (Convert.ToInt32(nudPaidAmount.Value) - Convert.ToInt32(txtTotalAmount.Text)).ToString();
                oTotal = 0;


            }


        }

        private void Tab_Add_Order_Enter(object sender, EventArgs e)
        {
            cmbDiscount.SelectedIndex = 1;
        }

        private void mtbCustomerNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable a;
                a = orderbus.GetDataByNumber(mtbCustomerNumber.Text);
                if(a.Rows.Count == 1) {
                    txtCustomerName.Text = a.Rows[0][2].ToString();
                }
                else
                {
                    txtCustomerName.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("CustomerNumber is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvOrder_Product_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvOrder_Product.Rows[e.RowIndex];
                cmbProduct.Text = row.Cells[1].Value.ToString();
                txtRate.Text = row.Cells[3].Value.ToString();
            }
        }

    }
}
