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

using BusinessLogicLayer;
using DTO;
namespace Management_System.PAL
{
    public partial class UserControlCategory : UserControl
    {
        Category category = new Category();
        CategoryBUS categorybus = new CategoryBUS();
        //private string connectionString = "Data Source=localhost;Initial Catalog=CSMS;Integrated Security=True;";
        private string id = "";
        public UserControlCategory()
        {
            InitializeComponent();
            txtCategoryName.Clear();
            dgvCategory.Columns[0].Visible = false;

            try
            {
                dgvCategory.DataSource = categorybus.GetData();
                DataTable a;
                a = categorybus.GetData();
                //MessageBox.Show(a.Rows[0][1].ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblTotal.Text = dgvCategory.Rows.Count.ToString();

            }
            catch
            {
                MessageBox.Show("View Brand is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void EmptyBox()
        {
            txtCategoryName.Clear();
            cmbStatus.SelectedIndex = 0;
        }

        private void EmptyBox1()
        {
            txtCategoryName1.Clear();
            cmbStatus1.SelectedIndex = 0;
            id = "";
        }

        private void picSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch, "Search");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (txtCategoryName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter category name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus.SelectedIndex == 0)
            {
                MessageBox.Show("Please select status", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                category.CategoryName = txtCategoryName.Text;
                category.CategoryStatus = cmbStatus.SelectedItem.ToString();

                try
                {
                    categorybus.Insert(category);
                    MessageBox.Show("Adding Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSearchCategoryName.Clear();
                    dgvCategory.Columns[0].Visible = false;

                    try
                    {
                        dgvCategory.DataSource = categorybus.GetData();
                        DataTable a;
                        a = categorybus.GetData();
                        //MessageBox.Show(a.Rows[0][1].ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblTotal.Text = dgvCategory.Rows.Count.ToString();

                    }
                    catch
                    {
                        MessageBox.Show("View Brand is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    EmptyBox();

                }
                catch
                {
                    MessageBox.Show("Adding Fail!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            
                /*
                 using (SqlConnection connection = new SqlConnection(connectionString))
                {
                   connection.Open();
                
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Category;", connection))
                    using (SqlCommand command1 = new SqlCommand("INSERT INTO Category  (Category_Name,Category_Status)" +
                        "OUTPUT inserted.Category_Id VALUES (@Category_Name,@Category_Status);", connection))
                    {
                        command1.Parameters.AddWithValue("@Category_Name", txtCategoryName.Text.Trim());
                        command1.Parameters.AddWithValue("@Category_Status", cmbStatus.SelectedItem.ToString());

                       command1.ExecuteNonQuery();
                        EmptyBox();
                    }
                }
                */
            }
        }

        private void tpAddCategory_Enter(object sender, EventArgs e)
        {
            EmptyBox();
        }

        private void tpManageCategory_Enter(object sender, EventArgs e)
        {
            txtSearchCategoryName.Clear();
            dgvCategory.Columns[0].Visible = false;

            try
            {
                dgvCategory.DataSource = categorybus.GetData();
                lblTotal.Text = dgvCategory.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("View Category is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            /*
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Category", connection))
                {
                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvCategory.DataSource = dataTable;
                    }
                    lblTotal.Text = dgvCategory.Rows.Count.ToString();
                }
            }
            */
        }

        private void txtSearchCategoryName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvCategory.DataSource = categorybus.GetDataByName(txtSearchCategoryName.Text);
                lblTotal.Text = dgvCategory.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("Search Bar is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            /*
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Category WHERE Category_Name LIKE '%" + txtSearchCategoryName.Text + "%';", connection))
                {

                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvCategory.DataSource = dataTable;
                    }
                    lblTotal.Text = dgvCategory.Rows.Count.ToString();
                }
            }
        */
        }

        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvCategory.Rows[e.RowIndex];
                id = row.Cells[0].Value.ToString();
                txtCategoryName1.Text = row.Cells[1].Value.ToString();
                cmbStatus1.SelectedItem = row.Cells[2].Value.ToString();
                tcCategory.SelectedTab = tpOptions;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Please select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtCategoryName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter category name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus1.SelectedIndex == 0)
            {
                MessageBox.Show("Please select status", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                category.CategoryId = Convert.ToInt32(id);
                category.CategoryName = txtCategoryName1.Text;
                category.CategoryStatus = cmbStatus1.SelectedItem.ToString();
                //MessageBox.Show(category.CategoryName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    categorybus.Update(category);
                    MessageBox.Show("Update Successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EmptyBox1();
                    tcCategory.SelectedTab = tpManageCategory;
                }
                catch
                {
                    MessageBox.Show("Update Fail!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                /*
                                using (SqlConnection connection = new SqlConnection(connectionString))
                                {
                                    connection.Open();

                                    using (SqlCommand command1 = new SqlCommand("UPDATE Category SET Category_Name = @Category_Name where Category_Id = @Category_Id and not exists (select * from Category where Category_Name = @Category_Name)", connection))
                                    using (SqlCommand command2 = new SqlCommand("UPDATE Category SET Category_Status = @Category_Status where Category_Id = @Category_Id", connection))
                                    {

                                        command1.Parameters.AddWithValue("@Category_Name", txtCategoryName1.Text.Trim());
                                        command1.Parameters.AddWithValue("@Category_Id", id);
                                        command2.Parameters.AddWithValue("@Category_Status", cmbStatus1.SelectedItem.ToString());
                                        command2.Parameters.AddWithValue("@Category_Id", id);

                                        command1.ExecuteNonQuery();
                                        command2.ExecuteNonQuery();
                                        EmptyBox1();
                                        tcCategory.SelectedTab = tpManageCategory;
                                    }
                                }
                */
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Please select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtCategoryName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter category name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus1.SelectedIndex == 0)
            {
                MessageBox.Show("Please select status", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are You want to delete this category ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        categorybus.Delete(id);
                        Console.WriteLine($"Row with ID {id} deleted successfully.");
                    }
                    catch
                    {
                        Console.WriteLine($"No rows found with ID {id}.");
                    }
                    EmptyBox1();
                    /*
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command1 = new SqlCommand("DELETE FROM Category WHERE Category_Id = @categoryid", connection))
                        {

                            command1.Parameters.AddWithValue("@categoryid", id);
                            int rowsAffected = command1.ExecuteNonQuery();

                            if (rowsAffected > 0)
                                Console.WriteLine($"Row with ID {id} deleted successfully.");
                            else
                                Console.WriteLine($"No rows found with ID {id}.");
                            EmptyBox1();
                            tcCategory.SelectedTab = tpManageCategory;
                        }
                    }
                */
                }
                tcCategory.SelectedTab = tpManageCategory;
            }
        }

        private void tpOptions_Enter(object sender, EventArgs e)
        {
            if (id == "")
            {
                tcCategory.SelectedTab = tpManageCategory;
            }
        }

        private void tpOptions_Leave(object sender, EventArgs e)
        {
            EmptyBox1();
        }        
    }
}
