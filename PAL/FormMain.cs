using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Management_System.PAL
{
    public partial class FormMain : Form
    {
        public string name = "{?}";
        private string roleId = "";
        public FormMain()
        {
            InitializeComponent();
            roleId = FormLogIn.SharedData1.ValueToPass1;
        }

        private void Move_Panel(Control btn)
        {
            pnlMove.Top = btn.Top;
            pnlMove.Height = btn.Height;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            lblUsername.Text = name;
            timerDateAndTime.Start();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to log out?", "Log Out Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn ĐĂNG XUẤT không " + name + " ?", "Log Out Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes) 
            {
                Move_Panel(btnLogOut);
                timerDateAndTime.Stop();
                Close();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Move_Panel(btnDashboard);
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlDashboard1.Count();
            userControlDashboard1.Visible = true;
            userControlOrder1.Visible = false;
            userControlReport1.Visible = false;
            userControlUser1.Visible = false;
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            Move_Panel(btnBrand);
            
            userControlDashboard1.Visible = false;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = false;
            userControlReport1.Visible = false;
            userControlUser1.Visible = false;
            userControlBrand1.EmptyBox();
            userControlBrand1.Visible = true;
            
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            Move_Panel(btnCategory);
            userControlDashboard1.Visible = false;
            userControlBrand1.Visible = false;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = false;
            userControlReport1.Visible = false;
            userControlUser1.Visible = false;
            userControlCategory1.EmptyBox();
            userControlCategory1.Visible = true;
            
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            Move_Panel(btnProduct);
            userControlDashboard1.Visible = false;
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = false;
            userControlOrder1.Visible = false;
            userControlReport1.Visible = false;
            userControlUser1.Visible = false;
            userControlProduct1.EmptyBox();
            userControlProduct1.Visible = true;
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            Move_Panel(btnOrders);
            userControlDashboard1.Visible = false;
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlReport1.Visible = false;
            userControlUser1.Visible = false;
            userControlOrder1.EmptyBox();
            userControlOrder1.Visible = true;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Move_Panel(btnReport);
            userControlDashboard1.Visible = false;
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = false;
            userControlUser1.Visible = false;
            userControlReport1.Visible = true;
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {

            if (roleId == "1")
            {
                Move_Panel(btnUsers);
                userControlDashboard1.Visible = false;
                userControlBrand1.Visible = false;
                userControlCategory1.Visible = false;
                userControlProduct1.Visible = false;
                userControlOrder1.Visible = false;
                userControlReport1.Visible = false;
                userControlUser1.EmptyBox();
                userControlUser1.Visible = true;
            }
            else
            {
                MessageBox.Show("You are not allow to go here.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void timerDateAndTime_Tick(object sender, EventArgs e)
        {
            lblTimeAndDate.Text = DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss  tt");
        }
    }
}
