using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Management_System.PAL
{
    public partial class FormStart : Form
    {
        public FormStart()
        {
            InitializeComponent();
            richTextBox1.Text =
                "\r\nĐề tài: Xây dựng ứng dụng quản lý một cửa hàng máy tính\r\n\r\n" +
                "Đây là một ứng dụng chạy trên máy đơn, được tạo ra sử dụng Winform và .NET Framework. Ngoài ra, nó còn trữ dữ liệu trên SQL Server, và truy vấn SQL để thao tác với dữ liệu.\r\n\r\n" +
                "Ứng dụng có các chức năng cơ bản sau:\r\n" +
                "- Đăng nhập, đăng xuất, phân quyền admin với các user khác\r\n" +
                "- Lấy lại mật khẩu đã quên\r\n" +
                "- Thêm, sửa, xóa dữ liệu (Nhãn hàng, sản phẩm, đơn hàng, user... )";
            richTextBox1.Select(0, 56);
            richTextBox1.SelectionFont = new Font("Segoe UI Semibold", 12, FontStyle.Bold | FontStyle.Underline);
            richTextBox1.ReadOnly = true;

            //phan nay de bo cai auto-selection cua textbox
            Control phake = new Control();
            this.Controls.Add(phake);
            this.ActiveControl = phake;

            Color color1 = ColorTranslator.FromHtml("#131313");
            this.BackColor = color1;
            richTextBox1.BackColor = color1;

            Color color2 = ColorTranslator.FromHtml("#FFEA00");
            label5.ForeColor = color2;
        }

        //xoa comment cai doan nay de khoi dong app trong cai form start 
        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            FormLogIn formLogIn = new FormLogIn();
            formLogIn.ShowDialog();
            Close();
        }

    }
}
