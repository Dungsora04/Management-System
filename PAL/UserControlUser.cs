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
    public partial class UserControlUser : UserControl
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
        private string connectionString = "Data Source=localhost;Initial Catalog=CSMS;Integrated Security=True;";
        private string id = "";
        private string id_role = "";
        User user = new User();
        UserBUS userBUS = new UserBUS();
        public UserControlUser()
        {
            InitializeComponent();
            dgvUser.DataSource = userBUS.GetData();
            lblTotal.Text = dgvUser.Rows.Count.ToString();
        }

        public void EmptyBox()
        {
            txtUserName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            cmbRole.DataSource = null;
            cmbRole.Items.Clear();
            cmbRole.Items.Add("--SELECT--");
            cmbRole.SelectedIndex = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command1 = new SqlCommand("SELECT Roles_Name,Roles_Id FROM Roles", connection))
                {
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item item = new Item();
                            item.Text = reader.GetString(0);
                            item.Id = reader.GetInt32(1);
                            cmbRole.Items.Add(item);
                        }
                    }
                    command1.ExecuteNonQuery();
                }

            }
        }

        public void EmptyBox1()
        {
            txtUserName1.Clear();
            txtEmail1.Clear();
            txtPassword1.Clear();
            id = "";
            cmbRole1.DataSource = null;
            cmbRole1.Items.Clear();
            cmbRole1.Items.Add("--SELECT--");
            cmbRole1.SelectedIndex = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command1 = new SqlCommand("SELECT Roles_Name,Roles_Id FROM Roles", connection))
                {
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item item = new Item();
                            item.Text = reader.GetString(0);
                            item.Id = reader.GetInt32(1);
                            cmbRole1.Items.Add(item);
                        }
                    }
                    command1.ExecuteNonQuery();
                }

            }
        }
        private void picSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch, "Search");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtUserName.Text.Trim() == string.Empty) {
                MessageBox.Show("Please enter user name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtEmail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("PLease enter password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbRole.SelectedIndex == 0)
            {
                MessageBox.Show("PLease enter role.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                user.UserName = txtUserName.Text;
                user.Email = txtEmail.Text;
                user.Password = txtPassword.Text;
                user.RoleId = cmbRole.SelectedIndex;
                try
                {
                    userBUS.Insert(user);
                    MessageBox.Show("Adding Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EmptyBox();
                }
                catch
                {
                    MessageBox.Show("Adding Fail!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void tpAddUser_Enter(object sender, EventArgs e)
        {
            EmptyBox();
        }

        private void tpManagerUser_Enter(object sender, EventArgs e)
        {
            EmptyBox();
            EmptyBox1();
            txtSearchUserName.Clear();
            dgvUser.Columns[0].Visible = false;
            try
            {
                dgvUser.DataSource = userBUS.GetData();
                lblTotal.Text = dgvUser.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("View User is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSearchUserName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvUser.DataSource = userBUS.GetDataByName(txtSearchUserName.Text);
                lblTotal.Text = dgvUser.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("Search Bar is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvUser.Rows[e.RowIndex];
                id = row.Cells[0].Value.ToString();
                txtUserName1.Text = row.Cells[1].Value.ToString();
                txtEmail1.Text = row.Cells[2].Value.ToString();
                txtPassword1.Text = row.Cells[3].Value.ToString();
                foreach (var item in cmbRole1.Items)
                {
                    if (item.ToString() == row.Cells[4].Value.ToString())
                    {
                        cmbRole1.SelectedItem = item;
                    }
                }
                tcUser.SelectedTab = tpOptions;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("First select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtUserName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter user name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtEmail1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtPassword1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("PLease enter password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbRole1.SelectedIndex == 0)
            {
                MessageBox.Show("PLease enter role.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                user.UserName = txtUserName1.Text;
                user.Email = txtEmail1.Text;
                user.Password = txtPassword1.Text;     
                user.UserId = Convert.ToInt32(id);
                user.RoleId = Convert.ToInt32((cmbRole1.SelectedItem as Item).Id.ToString());
                try
                {
                    userBUS.Update(user);
                    MessageBox.Show("Update Successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EmptyBox1();
                    tcUser.SelectedTab = tpManagerUser;
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
            else if (txtUserName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter user name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtEmail1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtPassword1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("PLease enter password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are You want to delete this user ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        userBUS.Delete(id);
                        Console.WriteLine($"Row with ID {id} deleted successfully.");
                        EmptyBox1();
                        tcUser.SelectedTab = tpManagerUser;
                    }
                    catch
                    {
                        MessageBox.Show("This User sold products in the past", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.WriteLine($"No rows found with ID {id}.");
                    }
                    
                }
                
            }
        }

        private void btnRemove_Enter(object sender, EventArgs e)
        {
            if(id == "")
            {
                tcUser.SelectedTab = tpManagerUser;
            }
        }

        private void btnRemove_Leave(object sender, EventArgs e)
        {
            EmptyBox1 ();
        }


        ///////////////////////////////////////////////////////////////////////////////////

        public void EmptyBoxRole()
        {
            txtRoleName.Clear();
            Descriptions.Clear();
        }

        public void EmptyBox1Role()
        {
            txtRoleName1.Clear();
            Descriptions1.Clear();
            id_role = "";
        }
        private void picSearch_MouseHoverRole(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch, "Search");
        }

        private void btnAdd_ClickRole(object sender, EventArgs e)
        {
            if (txtRoleName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter role name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (Descriptions.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Descriptions.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command1 = new SqlCommand("INSERT INTO Roles  (Roles_Name,Descriptions) " +
                        " OUTPUT inserted.Roles_Id VALUES (@Roles_Name,@Descriptions);", connection))
                    {
                        command1.Parameters.AddWithValue("@Roles_Name", txtRoleName.Text.Trim());
                        command1.Parameters.AddWithValue("@Descriptions", Descriptions.Text.Trim());

                        command1.ExecuteNonQuery();
                        EmptyBoxRole();
                    }
                }
            }
        }

        private void tpAddRole_Enter(object sender, EventArgs e)
        {
            EmptyBox();
        }

        private void tpManagerRole_Enter(object sender, EventArgs e)
        {
            EmptyBox();
            txtSearchRoleName.Clear();
            dgvRole.Columns[0].Visible = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Roles", connection))
                {
                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvRole.DataSource = dataTable;
                    }
                    lblTotalRole.Text = dgvRole.Rows.Count.ToString();
                }
            }
        }

        private void txtSearchUserName_TextChangedRole(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Roles WHERE Roles_Name LIKE '%" + txtSearchRoleName.Text + "%';", connection))
                {

                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvRole.DataSource = dataTable;
                    }
                    lblTotalRole.Text = dgvRole.Rows.Count.ToString();
                }
            }
        }

        private void dgvRole_CellClickRole(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvRole.Rows[e.RowIndex];
                id_role = row.Cells[0].Value.ToString();
                txtRoleName1.Text = row.Cells[1].Value.ToString();
                Descriptions1.Text = row.Cells[2].Value.ToString();
                tcUser.SelectedTab = tpOptionsRole;
            }
        }

        private void btnChange_ClickRole(object sender, EventArgs e)
        {
            if (id_role == "")
            {
                MessageBox.Show("First select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtRoleName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter user name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (Descriptions1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Descriptions.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command1 = new SqlCommand("UPDATE Roles SET Roles_Name = @Roles_Name where Roles_Id = @Roles_Id and not exists (select * from Roles where Roles_Name = @Roles_Name)", connection))
                    using (SqlCommand command2 = new SqlCommand("UPDATE Roles SET Descriptions = @Descriptions where Roles_Id = @Roles_Id", connection))
                    {

                        command1.Parameters.AddWithValue("@Roles_Name", txtRoleName1.Text.Trim());
                        command1.Parameters.AddWithValue("@Roles_Id", id_role);
                        command2.Parameters.AddWithValue("@Descriptions", Descriptions1.Text.Trim());
                        command2.Parameters.AddWithValue("@Roles_Id", id_role);


                        command1.ExecuteNonQuery();
                        command2.ExecuteNonQuery();
                        EmptyBox1();
                        tcUser.SelectedTab = tpManagerRole;
                    }
                }
            }
        }

        private void btnRemove_ClickRole(object sender, EventArgs e)
        {
            if (id_role == "")
            {
                MessageBox.Show("First select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtRoleName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter role name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (Descriptions1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Descriptions.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are You want to delete this role ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command1 = new SqlCommand("DELETE FROM Roles WHERE Roles_Id = @Roles_Id", connection))
                        {

                            command1.Parameters.AddWithValue("@Roles_Id", id_role);
                            int rowsAffected = command1.ExecuteNonQuery();

                            if (rowsAffected > 0)
                                Console.WriteLine($"Row with ID {id_role} deleted successfully.");
                            else
                                Console.WriteLine($"No rows found with ID {id_role}.");
                            EmptyBox1();
                            tcUser.SelectedTab = tpManagerRole;
                        }
                    }
                }
            }
        }

        private void btnRemove_EnterRole(object sender, EventArgs e)
        {
            if (id_role == "")
            {
                tcUser.SelectedTab = tpManagerRole;
            }
        }

        private void btnRemove_LeaveRole(object sender, EventArgs e)
        {
            EmptyBox1Role();
        }
    }
}
