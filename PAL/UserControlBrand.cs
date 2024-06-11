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
        //private string connectionString = "Data Source=Thinkpad-E14\\SQLEXPRESS02;Initial Catalog=CSMS;Integrated Security=True;";
        private string Id = "";
        public UserControlBrand()
        {
            InitializeComponent();
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
                    EmptyBox();
                }
                catch
                {
                    MessageBox.Show("Adding Fail!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    connection.Open();
                //    using (SqlCommand command1 = new SqlCommand("INSERT INTO Brand  (Brand_Name,Brand_Status)" +
                //        "OUTPUT inserted.Brand_Id VALUES (@Brand_Name,@Brand_Status);", connection))
                //    {
                //        //command1.Parameters.AddWithValue("@Brand_Id", (int)command.ExecuteScalar()+1);
                //        command1.Parameters.AddWithValue("@Brand_Name", txtBrandName.Text.Trim());
                //        command1.Parameters.AddWithValue("@Brand_Status", cmbStatus.SelectedItem.ToString());

                //        command1.ExecuteNonQuery();
                //        EmptyBox();
                //    }
                //}
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
                lblTotal.Text = dgvBrand.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("View Brand is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command1 = new SqlCommand("SELECT * FROM Brand", connection))
            //    {
            //        using (var reader = command1.ExecuteReader())
            //        {
            //            var dataTable = new DataTable();
            //            dataTable.Load(reader);
            //            dgvBrand.DataSource = dataTable;
            //        }
            //        lblTotal.Text = dgvBrand.Rows.Count.ToString();
            //    }
            //}
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
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command1 = new SqlCommand("SELECT * FROM Brand WHERE Brand_Name LIKE '%" + txtSearchBrandName.Text + "%';", connection))
            //    {

            //        using (var reader = command1.ExecuteReader())
            //        {
            //            var dataTable = new DataTable();
            //            dataTable.Load(reader);
            //            dgvBrand.DataSource = dataTable;
            //        }
            //        lblTotal.Text = dgvBrand.Rows.Count.ToString();
            //    }
            //}
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
                //MessageBox.Show(brand.BrandName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    connection.Open();
                //    using (SqlCommand command1 = new SqlCommand("UPDATE Brand SET Brand_Name = @Brand_Name where Brand_Id = @Brand_Id and not exists(select * from Brand where Brand_Name = @Brand_Name)", connection))
                //    using (SqlCommand command2 = new SqlCommand("UPDATE Brand SET Brand_Status = @Brand_Status where Brand_Id = @Brand_Id", connection))
                //    {

                //        command1.Parameters.AddWithValue("@Brand_Name", txtBrandName1.Text.Trim());
                //        command1.Parameters.AddWithValue("@Brand_Id", Id);
                //        command2.Parameters.AddWithValue("@Brand_Status", cmbStatus1.SelectedItem.ToString());
                //        command2.Parameters.AddWithValue("@Brand_Id", Id);

                //        command1.ExecuteNonQuery();
                //        command2.ExecuteNonQuery();
                //        EmptyBox1();
                //        tcBrand.SelectedTab = tpManageBrand;
                //    }
                //}
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
                    //using (SqlConnection connection = new SqlConnection(connectionString))
                    //{
                    //    connection.Open();
                    //    using (SqlCommand command1 = new SqlCommand("DELETE FROM Brand WHERE Brand_Id = @brandid", connection))
                    //    {

                    //        command1.Parameters.AddWithValue("@brandid", Id);
                    //        int rowsAffected = command1.ExecuteNonQuery();

                    //        if (rowsAffected > 0)
                    //            Console.WriteLine($"Row with ID {Id} deleted successfully.");
                    //        else
                    //            Console.WriteLine($"No rows found with ID {Id}.");
                    //        EmptyBox1();

                    //    }
                    //}

                    try
                    { 
                        brandbus.Delete(Id);
                        Console.WriteLine($"Row with ID {Id} deleted successfully.");
                    }
                    catch { 
                        Console.WriteLine($"No rows found with ID {Id}."); 
                    }
                    EmptyBox1();
                }
                
                tcBrand.SelectedTab = tpManageBrand;
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
