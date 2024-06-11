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

namespace Management_System.PAL
{
    public partial class FormForgotPassword : Form
    {
        private string connectionString = "Data Source=Thinkpad-E14\\SQLEXPRESS02;Initial Catalog=CSMS;Integrated Security=True;";
        UserBUS userBUS = new UserBUS();


        public FormForgotPassword()
        {
            InitializeComponent();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter username.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show("Không được để trống username.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            else if (txtEmail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show("Không được để trống email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            else
            {
                try
                {
                    DataTable check = userBUS.check_Email(txtUsername.Text.Trim(), txtEmail.Text.Trim());
                    if (check.Rows.Count > 0)
                    {
                        string password = check.Rows[0][0].ToString();
                        MessageBox.Show($"Your password was sent to your email. Please check your inbox and try again.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TextBox textBox = new TextBox
                        {
                            Multiline = true,
                            ReadOnly = true,
                            Text = $"{password}",
                            Dock = DockStyle.Fill
                        };

                        Form form = new Form
                        {
                            Text = txtEmail.Text.Trim(),
                            Controls = { textBox },
                            MaximumSize = new Size(300, 200),
                            MinimumSize = new Size(300, 200),
                            FormBorderStyle = FormBorderStyle.FixedDialog
                        };
                        form.ShowDialog();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("No user found with the provided username and email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //MessageBox.Show($"Không tìm thấy user.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch
                {
                    MessageBox.Show("Failure!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    connection.Open();
                //    using (SqlCommand command = new SqlCommand("SELECT Users_Password FROM Users WHERE Users_Name = @username AND Users_Email = @email", connection))
                //    {
                //        command.Parameters.AddWithValue("@username", txtUsername.Text.Trim());  
                //        command.Parameters.AddWithValue("@email", txtEmail.Text.Trim());

                //        object result = command.ExecuteScalar();

                //        if (result != null) 
                //        {
                //            string password = result.ToString();
                //            MessageBox.Show($"Your password was sent to your email. Please check your inbox and try again.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            //MessageBox.Show($"Password đã được gửi đến {txtEmail.Text.Trim()}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //            TextBox textBox = new TextBox
                //            {
                //                Multiline = true,
                //                ReadOnly = true,
                //                Text = $"{password}",
                //                Dock = DockStyle.Fill
                //            };
                            
                //            Form form = new Form
                //            {
                //                Text = txtEmail.Text.Trim(),
                //                Controls = { textBox },
                //                MaximumSize = new Size(300, 200),
                //                MinimumSize = new Size(300, 200),
                //                FormBorderStyle = FormBorderStyle.FixedDialog 
                //            };
                //            form.ShowDialog();
                //            Close();
                //        }
                //        else
                //        {
                //            MessageBox.Show("No user found with the provided username and email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            //MessageBox.Show($"Không tìm thấy user.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        }
                //    }
                //}
            }
        }
    }
}
