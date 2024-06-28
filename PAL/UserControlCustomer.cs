using BusinessLogicLayer;
using DTO;
using Guna.UI2.WinForms.Suite;
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
    public partial class UserControlCustomer : UserControl
    {
        private string id = "";
        CustomerBUS customerBUS = new CustomerBUS();
        Customer customerDTO = new Customer();

        public UserControlCustomer()
        {
            InitializeComponent();
        }

        public void EmptyBox()
        {
            txtCustomerName.Clear();
            mtbCustomerNumber.Clear();
            txtSearchCustomerName.Clear();
            dgvCustomer.Columns[0].Visible = false;

            try
            {
                dgvCustomer.DataSource = customerBUS.GetData();
                DataTable a;
                a = customerBUS.GetData();
                lblTotal.Text = dgvCustomer.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("View Customer error!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void EmptyBox1()
        {
            txtCustomerName1.Clear();
            txtNumber1.Clear();
            id = "";
        }

        private void picSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch, "Search");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter user name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (mtbCustomerNumber.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                customerDTO.CustomerName = txtCustomerName.Text;
                customerDTO.CustomerNumber = mtbCustomerNumber.Text.Trim();
                try
                {
                    customerBUS.Insert(customerDTO);
                    MessageBox.Show("Adding Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EmptyBox();
                }
                catch
                {
                    MessageBox.Show("Adding Fail!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                try
                {
                    dgvCustomer.DataSource = customerBUS.GetData();
                    DataTable a;
                    a = customerBUS.GetData();
                    lblTotal.Text = dgvCustomer.Rows.Count.ToString();
                }
                catch
                {
                    MessageBox.Show("View Customer error!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void txtSearchCustomerName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvCustomer.DataSource = customerBUS.GetDataByName(txtSearchCustomerName.Text);
                lblTotal.Text = dgvCustomer.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("Search Bar is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];
                id = row.Cells[0].Value.ToString();
                txtCustomerName1.Text = row.Cells[1].Value.ToString();
                txtNumber1.Text = row.Cells[2].Value.ToString();


                try
                {
                    dgvDetails.DataSource = customerBUS.GetDetails(id);
                }
                catch
                {
                    MessageBox.Show("View Details is error!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                tcCustomer.SelectedTab = tpOptions;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("First select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtCustomerName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter user name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtNumber1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                customerDTO.CustomerId = Convert.ToInt32(id);
                customerDTO.CustomerName = txtCustomerName1.Text;
                customerDTO.CustomerNumber = txtNumber1.Text;
                try
                {
                    customerBUS.Update(customerDTO);
                    MessageBox.Show("Update Successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EmptyBox1();
                    tcCustomer.SelectedTab = tpManageCustomer;
                }
                catch
                {
                    MessageBox.Show("Update Fail!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("First select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtCustomerName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter user name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtNumber1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are You want to delete this customer ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {

                    try
                    {
                        customerBUS.Delete(id);
                        MessageBox.Show("Customer deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        EmptyBox1();
                        tcCustomer.SelectedTab = tpManageCustomer;
                    }
                    catch
                    {
                        Console.WriteLine($"No customer found.");
                    }
                    
                }
            }
        }

        private void btnRemove_Enter(object sender, EventArgs e)
        {
            if (id == "")
            {
                tcCustomer.SelectedTab = tpManageCustomer;
            }
        }

        private void btnRemove_Leave(object sender, EventArgs e)
        {
            EmptyBox1();
        }
        private void tpOptions_Enter(object sender, EventArgs e)
        {
            if (id == "")
            {
                tcCustomer.SelectedTab = tpManageCustomer;
            }
        }

        private void tpManageCustomer_Enter(object sender, EventArgs e)
        {
            EmptyBox();
        }

        private void txtSearchCustomerNumber_TextChanged(object sender, EventArgs e)
        {

            try
            {
                dgvCustomer.DataSource = customerBUS.GetDataByNumber(txtSearchCustomerNumber.Text);
                lblTotal.Text = dgvCustomer.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("Search Bar is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}