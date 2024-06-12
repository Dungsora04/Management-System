using BusinessLogicLayer;
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
using DTO;
using System.Web.Security;

namespace Management_System.PAL
{
    public partial class FormLogIn : Form
    {
        //private string connectionString = "Data Source=localhost;Initial Catalog=CSMS;Integrated Security=True;MultipleActiveResultSets=True;";
        UserBUS userBUS = new UserBUS();

        public static class SharedData
        {
            public static string ValueToPass { get; set; }
        }
        public static class SharedData1
        {
            public static string ValueToPass1 { get; set; }
        }
        // Set the value somewhere in Form1


        public FormLogIn()
        {
            InitializeComponent();
            txtPassword1.UseSystemPasswordChar = true;
            
        }

        private void EmptyBox()
        {
            txtUsername1.Clear();
            txtPassword1.Clear();
            
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void picHide_Click(object sender, EventArgs e)
        {
            if (picHide.Visible == true)
            {
                txtPassword1.UseSystemPasswordChar = true;
                picShow.Visible = true;
                picHide.Visible = false;
            }
        }
        private void picShow_Click(object sender, EventArgs e)
        {
            if (picShow.Visible == true)
            {
                txtPassword1.UseSystemPasswordChar = false;
                picShow.Visible = false;
                picHide.Visible = true;
            }
        }

        

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (txtUsername1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter username.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show("Không được để trống username.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtPassword1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show("Hãy nhập password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    //MessageBox.Show("Please enter password1.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable check = userBUS.check_Password(txtUsername1.Text.Trim(), txtPassword1.Text.Trim());
                    
                    if (check.Rows.Count > 0)
                    {
                        SharedData.ValueToPass = txtUsername1.Text;
                        SharedData1.ValueToPass1 = check.Rows[0][0].ToString();
                        FormMain formMain = new FormMain();
                        formMain.name = txtUsername1.Text;
                        formMain.ShowDialog();
                        EmptyBox();
                    }
                    else
                    {
                        MessageBox.Show("Username or password is incorrect.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("Fail", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //using (SqlConnection connection = new SqlConnection(connectionString))  
                //{
                //    connection.Open(); 
                //    using (SqlCommand command = new SqlCommand("SELECT Count(*) FROM Users WHERE Users_Name = @username AND Users_Password = @password", connection))
                //    using (SqlCommand command1 = new SqlCommand("SELECT * FROM Users WHERE Users_Name = @username AND Users_Password = @password", connection))
                //    {
                //        command.Parameters.AddWithValue("@username", txtUsername1.Text.Trim());
                //        command.Parameters.AddWithValue("@password", txtPassword1.Text.Trim());
                //        command1.Parameters.AddWithValue("@username", txtUsername1.Text.Trim());
                //        command1.Parameters.AddWithValue("@password", txtPassword1.Text.Trim());

                //        int userCount = (int)command.ExecuteScalar();

                //        if (userCount > 0)
                //        {
                //            SharedData.ValueToPass = txtUsername1.Text;
                            
                //            using (SqlDataReader reader = command1.ExecuteReader())
                //            {
                //                while (reader.Read())
                //                {
                //                    Int32 roleId = reader.GetInt32(4);
                //                    SharedData1.ValueToPass1 = roleId.ToString();
                //                    //MessageBox.Show(roleId.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                }
                //            }
                //            command1.ExecuteNonQuery();
                //            FormMain formMain = new FormMain();
                //            formMain.name = txtUsername1.Text;
                //            formMain.ShowDialog();
                //            EmptyBox();
                //        }
                //        else
                //        {
                //            MessageBox.Show("Username or password is incorrect.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            //MessageBox.Show("Sai username hoặc mật khẩu, vui lòng thử lại.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        }
                //    }

                //}
            }
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            FormForgotPassword formForgotPassword = new FormForgotPassword();
            formForgotPassword.ShowDialog();
        }
    }
}
