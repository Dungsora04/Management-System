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

    public partial class UserControlBrand : UserControl
    {

        Brand brand = new Brand();
        BrandBUS brandbus = new BrandBUS(); 
        private string Id = "";
        public UserControlBrand()
        {
            InitializeComponent();
            txtSearchBrandName.Clear();
            dgvBrand.Columns[0].Visible = false;

            try
            {
                dgvBrand.DataSource = brandbus.GetData();
                DataTable a;
                a = brandbus.GetData();
                lblTotal.Text = dgvBrand.Rows.Count.ToString();

            }
            catch
            {
                MessageBox.Show("View Brand is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void EmptyBox()
        {
            txtBrandName.Clear();
            cmbStatus.SelectedIndex = 0;    
        }

        private void EmptyBox1()
        {
            txtBrandName1.Clear();
            cmbStatus1.SelectedIndex = 0;
            Id = "";
        }
        private void picSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch, "Search");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtBrandName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter brand name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if(cmbStatus.SelectedIndex == 0)
            {
                MessageBox.Show("PLease select status." , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                brand.BrandName = txtBrandName.Text;
                brand.BrandStatus = cmbStatus.SelectedItem.ToString();
                
                try
                {
                    brandbus.Insert(brand);
                    MessageBox.Show("Adding Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSearchBrandName.Clear();
                    dgvBrand.Columns[0].Visible = false;

                    try
                    {
                        dgvBrand.DataSource = brandbus.GetData();
                        DataTable a;
                        a = brandbus.GetData();
                        lblTotal.Text = dgvBrand.Rows.Count.ToString();

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

        private void tpAddBrand_Enter(object sender, EventArgs e)
        {
            EmptyBox();
        }

        private void tpManageBrand_Enter(object sender, EventArgs e)
        {
            txtSearchBrandName.Clear();
            dgvBrand.Columns[0].Visible = false;
            
            try
            {
                dgvBrand.DataSource = brandbus.GetData();
                DataTable a;
                a = brandbus.GetData();
                lblTotal.Text = dgvBrand.Rows.Count.ToString();
                
            }
            catch
            {
                MessageBox.Show("View Brand is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSearchBrandName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvBrand.DataSource = brandbus.GetDataByName(txtSearchBrandName.Text);
                lblTotal.Text = dgvBrand.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("Search Bar is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvBrand_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvBrand.Rows[e.RowIndex];
                Id = row.Cells[0].Value.ToString();
                txtBrandName1.Text = row.Cells[1].Value.ToString();
                cmbStatus1.SelectedItem = row.Cells[2].Value.ToString();
                tcBrand.SelectedTab = tpOptions; 
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if(Id == "")
            {
                MessageBox.Show("First select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtBrandName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter brand name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus1.SelectedIndex == 0)
            {
                MessageBox.Show("PLease select status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                brand.BrandId = Convert.ToInt32(Id);
                brand.BrandName = txtBrandName1.Text;
                brand.BrandStatus = cmbStatus1.SelectedItem.ToString();
                try
                {
                    brandbus.Update(brand);
                    MessageBox.Show("Update Successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EmptyBox1();
                    tcBrand.SelectedTab = tpManageBrand;
                }
                catch
                {
                    MessageBox.Show("Update Fail!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (Id == "")
            {
                MessageBox.Show("First select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtBrandName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter brand name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus1.SelectedIndex == 0)
            {
                MessageBox.Show("PLease select status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you want to delete this brand ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    { 
                        brandbus.Delete(Id);
                        Console.WriteLine($"Row with ID {Id} deleted successfully.");
                        EmptyBox1();
                        tcBrand.SelectedTab = tpManageBrand;
                    }
                    catch (Exception ex) {
                        MessageBox.Show($"Having Products of this Brand!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.WriteLine($"No rows found with ID {Id}.");
                        Console.WriteLine(ex.Message);
                        
                    }
                    
                }
                
                
            }
        }

        private void tpOptions_Enter(object sender, EventArgs e)
        {
            if(Id == "")
            {
                tcBrand.SelectedTab = tpManageBrand;
            }
        }

        private void tpOptions_Leave(object sender, EventArgs e)
        {
            EmptyBox1();
        }

        private void txtBrandName1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
