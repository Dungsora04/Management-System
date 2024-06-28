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
                        EmptyBox1();
                        tcCategory.SelectedTab = tpManageCategory;
                    }
                    catch
                    {
                        MessageBox.Show($"Having Products of this Category!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.WriteLine($"No rows found with ID {id}.");
                    }
                    
                }
                
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
