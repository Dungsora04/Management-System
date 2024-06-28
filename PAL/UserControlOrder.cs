using System;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms;
using BusinessLogicLayer;
using DTO;
using static Management_System.PAL.FormLogIn;
using Microsoft.ReportingServices.Diagnostics.Internal;


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
        OrdersInfo ordersinfo = new OrdersInfo();
        OrderBUS orderbus = new OrderBUS();
        ProductBUS productbus = new ProductBUS();
        Customer customer = new Customer();
        CustomerBUS customerbus = new CustomerBUS();
        User user = new User();
        UserBUS userbus = new UserBUS();
        string warranty = "";
        string product_detail = "";
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

            DataTable a;
            a = productbus.GetDataProductAvailable();
            for (int i = 0; i < a.Rows.Count; i++)
            {
                Item item = new Item();
                item.Text = a.Rows[i][0].ToString();
                item.Id = Convert.ToInt32(a.Rows[i][1]);
                cmbProduct.Items.Add(item);
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
            lblUserName.Text = FormLogIn.SharedData.ValueToPass;
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
                    
                    DataTable a;
                    a = productbus.GetDataProductWarranty((cmbProduct.SelectedItem as Item).Text.ToString());
                    for (int i = 0; i < a.Rows.Count; i++)
                    {
                        Item item = new Item();
                        item.Text = a.Rows[i][0].ToString();
                        item.Id = Convert.ToInt32(a.Rows[i][1]);
                        cmbProduct.Items.Add(item);
                    }

                    int rate, total;
                    Int32.TryParse(txtRate.Text, out rate);
                    Int32.TryParse(txtTotal.Text, out total);
                    if (dgvProductList.Rows.Count != 0)
                    {
                        
                        int flag = 0;
                        foreach (DataGridViewRow rows in dgvProductList.Rows)
                        {
                         
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
                        //dgvProductList.Rows.Add(row);
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

            DataTable a;
            a = productbus.GetDataProductWarranty(cmbProduct.SelectedItem.ToString());
            if (a.Rows.Count > 0)
            {
                warranty = a.Rows[0][0].ToString();
                txtRate.Text = a.Rows[0][1].ToString();
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

                DataTable check = customerbus.CheckCustomerExist(txtCustomerName.Text.Trim(), mtbCustomerNumber.Text.Trim());

                if (check.Rows.Count > 0)
                {
                    // do nothing
                }
                else
                {
                    customer.CustomerName = txtCustomerName.Text.Trim();
                    customer.CustomerNumber = mtbCustomerNumber.Text.Trim();
                    customerbus.Insert(customer);
                }

                DataTable check1 = customerbus.CheckCustomerExist(txtCustomerName.Text.Trim(), mtbCustomerNumber.Text.Trim());
                txtCustomerName.Text = check1.Rows[0][0].ToString();
                DataTable check2 = userbus.GetDataByName(name);
                userId = Convert.ToInt32(check2.Rows[0][0]);

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
                    orderbus.Insert(order);
                    MessageBox.Show("Adding Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Adding Fail!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DataTable a;
                a = orderbus.GetData_MaxOrderId();
                txtCustomerName.Text = a.Rows[0][0].ToString();
                for (int i = 0; i < dgvProductList.Rows.Count; i++)
                {
                    ordersinfo.Orders_Id = Convert.ToInt32(txtCustomerName.Text.Trim());
                    ordersinfo.Product_Id = Convert.ToInt32(dgvProductList.Rows[i].Cells[0].Value.ToString());
                    ordersinfo.Orders_Quantity = Convert.ToInt32(dgvProductList.Rows[i].Cells[3].Value.ToString());
                    ordersinfo.Warranty = dgvProductList.Rows[i].Cells[4].Value.ToString();

                    orderbus.InsertOrderInfo(ordersinfo);

                }
                EmptyBox();
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
                for (int j = 1; j < dgvProductList.Columns.Count - 1; j++)
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
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
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

                try
                {
                    dgvDetails.DataSource = orderbus.GetDataByCustomerProduct(Convert.ToInt32(Id));
                    lblTotal.Text = dgvDetails.Rows.Count.ToString();
                }
                catch
                {
                    MessageBox.Show("Search Bar is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                DialogResult dialogResult = MessageBox.Show("Are you want to delete this order!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        orderbus.DeleteOrdersInfo(Id);
                        orderbus.Delete(Id);

                        MessageBox.Show($"Row with ID {Id} deleted successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tcOrders.SelectedTab = tpManageOrders;
                        EmptyBox1();

                    }
                    catch
                    {
                        MessageBox.Show($"No rows found with ID {Id}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
            }
        }

        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvOrders.DataSource = orderbus.GetDataDGV();
                lblTotal.Text = dgvOrders.Rows.Count.ToString();

            }
            catch
            {
                MessageBox.Show("View Order is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

                DataTable check1 = customerbus.CheckCustomerExist(txtCustomerName1.Text.Trim(), mtbCustomerNumber1.Text.Trim());
                txtCustomerName1.Text = check1.Rows[0][0].ToString();

                // update
                order.Order_Id = Convert.ToInt32(Id);
                order.Orders_Date = dtpDate1.Value.Date;
                order.Customer_Id = Convert.ToInt32(txtCustomerName1.Text);
                order.Users_Id = userId;
                order.Total_Amount = Convert.ToInt32(txtTotalAmount1.Text);
                order.Paid_Amount = Convert.ToInt32(nudPaidAmount1.Value);
                order.Due_Amount = Convert.ToInt32(txtDueAmount1.Text);
                order.Discount = Convert.ToInt32(nudDiscount1.Value);
                order.Grand_Total = Convert.ToInt32(txtGrandTotal1.Text);

                try
                {
                    orderbus.Update(order);
                    MessageBox.Show("Update Successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch
                {
                    MessageBox.Show("Update Fail!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                tcOrders.SelectedTab = tpManageOrders;
                tpManageOrders_Enter(sender, e);
                EmptyBox1();
            }
        }

        private void tpAddOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show(name, userId.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtSearchCustomerNumber_TextChanged(object sender, EventArgs e)
        {

            try
            {
                dgvOrders.DataSource = orderbus.GetDataByNumber(txtSearchCustomerNumber.Text);
                lblTotal.Text = dgvOrders.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("Search Bar is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
                if (a.Rows.Count > 0 && a.Rows.Count < 5)
                {
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

        private void txtSearchProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvOrder_Product.DataSource = productbus.SearchProductinOrder(txtSearchProductName.Text);
            }
            catch
            {
                MessageBox.Show("Search Bar is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
