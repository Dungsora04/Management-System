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
        private string connectionString = "Data Source=localhost;Initial Catalog=CSMS;Integrated Security=True;";
        private string id = "";

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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Customer", connection))
                {
                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvCustomer.DataSource = dataTable;
                    }
                    lblTotal.Text = dgvCustomer.Rows.Count.ToString();
                }
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

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command1 = new SqlCommand("INSERT INTO Customer (Customer_Name,Customer_Number) " +
                        "OUTPUT inserted.Customer_Id VALUES(@Customer_Name,@Customer_Number); ", connection))
                    {
                        command1.Parameters.AddWithValue("@Users_Name", txtCustomerName.Text.Trim());
                        command1.Parameters.AddWithValue("@Users_Email", mtbCustomerNumber.Text.Trim());

                        command1.ExecuteNonQuery();
                        EmptyBox();
                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command1 = new SqlCommand("SELECT Customer_Id, Customer_Name, Customer_Number\r\nFROM Customer", connection))
                    {
                        using (var reader = command1.ExecuteReader())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            dgvCustomer.DataSource = dataTable;
                        }
                        lblTotal.Text = dgvCustomer.Rows.Count.ToString();
                    }
                }
            }
        }


        private void txtSearchCustomerName_TextChanged(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Customer WHERE Customer_Name LIKE '%" + txtSearchCustomerName.Text + "%';", connection))
                {

                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvCustomer.DataSource = dataTable;
                    }
                    lblTotal.Text = dgvCustomer.Rows.Count.ToString();
                }
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command1 = new SqlCommand("UPDATE Customer SET Customer_Name = @Customer_Name, Customer_Number = @Customer_Number " +
                        "where Customer_Id = @Customer_Id", connection))
                    {

                        command1.Parameters.AddWithValue("@Customer_Name", txtCustomerName1.Text.Trim());
                        command1.Parameters.AddWithValue("@Customer_Number", txtNumber1.Text.Trim());
                        command1.Parameters.AddWithValue("@Customer_Id", id);


                        command1.ExecuteNonQuery();
                        EmptyBox1();
                        tcCustomer.SelectedTab = tpManageCustomer;
                    }
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
                DialogResult dialogResult = MessageBox.Show("Are You want to delete this user ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command1 = new SqlCommand("DELETE FROM Customer WHERE Customer_Id = @Customer_Id", connection))
                        {

                            command1.Parameters.AddWithValue("@Customer_Id", id);
                            int rowsAffected = command1.ExecuteNonQuery();

                            if (rowsAffected > 0)
                                Console.WriteLine($"Row with ID {id} deleted successfully.");
                            else
                                Console.WriteLine($"No rows found with ID {id}.");
                            EmptyBox1();
                            tcCustomer.SelectedTab = tpManageCustomer;
                        }
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
            txtSearchCustomerName.Clear();
            dgvCustomer.Columns[0].Visible = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Customer", connection))
                {
                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvCustomer.DataSource = dataTable;
                    }
                    lblTotal.Text = dgvCustomer.Rows.Count.ToString();
                }
            }
        }
    }
}
