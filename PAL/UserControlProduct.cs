using Management_System.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
// Sap xep dua ra danh sach hang phu hop(gia, pho bien... -> Dashboard)
// 
namespace Management_System.PAL
{
    /*
     * 
     * gợi ý sđt khi đã mua
     * khóa -> sđt
     * discount -> có mức cố định cho nhiều th
     * 
     * */

    public partial class UserControlProduct : UserControl
    {
        private List<ProductDto> products;
        private List<BrandDto> brands;
        private List<CategoryDto> categories;

        public void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Load Products
                using (SqlCommand command = new SqlCommand("SELECT p.Product_Id, p.Product_Name, p.Product_Image, p.Product_Price, p.Product_Quantity, b.Brand_Name, c.Category_Name, p.Product_Warranty, p.Product_Status " +
                                                           "FROM Product p INNER JOIN Brand b ON b.Brand_Id = p.Brand_Id INNER JOIN Category c ON c.Category_Id = p.Category_Id", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        products = new List<ProductDto>();
                        while (reader.Read())
                        {
                            products.Add(new ProductDto
                            {
                                Product_Id = reader.GetInt32(0),
                                Product_Name = reader.GetString(1),
                                Product_Image = (byte[])reader[2],
                                Product_Price = reader.GetInt32(3),
                                Product_Quantity = reader.GetInt32(4),
                                Brand_Name = reader.GetString(5),
                                Category_Name = reader.GetString(6),
                                Product_Warranty = reader.GetInt32(7),
                                Product_Status = reader.GetString(8)
                    });
                        }
                    }
                }

                // Load Brands
                using (SqlCommand command = new SqlCommand("SELECT * FROM Brand", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        brands = new List<BrandDto>();
                        while (reader.Read())
                        {
                            brands.Add(new BrandDto
                            {
                                Brand_Id = reader.GetInt32(0),
                                Brand_Name = reader.GetString(1),
                                Brand_Status = reader.GetString(2)
                            });
                        }
                    }
                }

                // Load Categories
                using (SqlCommand command = new SqlCommand("SELECT * FROM Category", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        categories = new List<CategoryDto>();
                        while (reader.Read())
                        {
                            categories.Add(new CategoryDto
                            {
                                Category_Id = reader.GetInt32(0),
                                Category_Name = reader.GetString(1),
                                Category_Status = reader.GetString(2)
                            });
                        }
                    }
                }
            }
        }

        private class Item
        {
            public string Text { get; set; }
            public int Id { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        private string connectionString = "Data Source=Thinkpad-E14\\SQLEXPRESS02;Initial Catalog=CSMS;Integrated Security=True;";
        private string Id = "";
        byte[] image;
        MemoryStream memoryStream;
        public UserControlProduct()
        {
            InitializeComponent();
        }

        private void ImageUpload(PictureBox picture)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    picture.Image = Image.FromFile(openFileDialog.FileName);
            }
            catch (Exception)
            {
                MessageBox.Show("Image upload error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EmptyBox()
        {
            txtProductName.Clear();
            picPhoto.Image = null;
            nudRate.Value = 0;
            nudQuantity.Value = 0;
            nudWarranty.Value = 0;
            txtDetails.Clear();
            cmbBrand.DataSource = null;
            cmbBrand.Items.Clear();
            cmbBrand.Items.Add("--SELECT--");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command1 = new SqlCommand("SELECT Brand_Name,Brand_Id FROM Brand WHERE Brand_Status = 'Available' ORDER BY Brand_Name;", connection))
                {
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item item = new Item();
                            item.Text = reader.GetString(0);
                            item.Id = reader.GetInt32(1);
                            cmbBrand.Items.Add(item);
                        }
                    }

                }

            }
            if (cmbBrand.Items.Count > 0)
            {
                cmbBrand.SelectedIndex = 0;
            }
            cmbCategory.DataSource = null;
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("--SELECT--");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command1 = new SqlCommand("SELECT Category_Name,Category_Id FROM Category WHERE Category_Status = 'Available' ORDER BY Category_Name;", connection))
                {
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item item = new Item();
                            item.Text = reader.GetString(0);
                            item.Id = reader.GetInt32(1);
                            cmbCategory.Items.Add(item);
                        }
                    }

                }

            }
            if (cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 0;
            }
            cmbStatus.SelectedIndex = 0;
        }

        private void EmptyBox1()
        {
            txtProductName1.Clear();
            picPhoto1.Image = null;
            nudRate1.Value = 0;
            nudQuantity1.Value = 0;
            nudWarranty1.Value = 0;
            txtDetails.Clear();
            ComboBoxAutoFill();
            cmbStatus1.SelectedIndex = 0;
            Id = "";
        }

        private void ComboBoxAutoFill()
        {
            cmbBrand1.Items.Clear();
            cmbBrand1.Items.Add("--SELECT--");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command1 = new SqlCommand("SELECT Brand_Name,Brand_Id FROM Brand WHERE Brand_Status = 'Available' ORDER BY Brand_Name;", connection))
                {
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //string categoryName = reader.GetString(0); // Assuming Brand_Name is the first column
                            //cmbBrand1.Items.Add(categoryName);
                            Item item = new Item();
                            item.Text = reader.GetString(0);
                            item.Id = reader.GetInt32(1);
                            cmbBrand1.Items.Add(item);
                        }
                    }
                    cmbBrand1.SelectedIndex = 0;
                }

            }

            cmbCategory1.Items.Clear();
            cmbCategory1.Items.Add("--SELECT--");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command1 = new SqlCommand("SELECT Category_Name,Category_Id FROM Category WHERE Category_Status = 'Available' ORDER BY Category_Name;", connection))
                {
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //string categoryName = reader.GetString(0); // Assuming Brand_Name is the first column
                            //cmbCategory1.Items.Add(categoryName);
                            Item item = new Item();
                            item.Text = reader.GetString(0);
                            item.Id = reader.GetInt32(1);
                            cmbCategory1.Items.Add(item);
                        }
                    }
                    cmbCategory1.SelectedIndex = 0;
                }

            }
        }
        private void ComboBoxAutoFill1()
        {
            cmbBrand2.Items.Clear();
            cmbBrand2.Items.Add("All Brands");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command1 = new SqlCommand("SELECT Brand_Name,Brand_Id FROM Brand WHERE Brand_Status = 'Available' ORDER BY Brand_Name;", connection))
                {
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //string categoryName = reader.GetString(0); // Assuming Brand_Name is the first column
                            //cmbBrand2.Items.Add(categoryName);
                            Item item = new Item();
                            item.Text = reader.GetString(0);
                            item.Id = reader.GetInt32(1);
                            cmbBrand2.Items.Add(item);

                        }
                    }
                    cmbBrand2.SelectedIndex = 0;
                }

            }

            cmbCategory2.Items.Clear();
            cmbCategory2.Items.Add("All Categories");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command1 = new SqlCommand("SELECT Category_Name,Category_Id FROM Category WHERE Category_Status = 'Available' ORDER BY Category_Name;", connection))
                {
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //string categoryName = reader.GetString(0); // Assuming Brand_Name is the first column
                            //cmbCategory2.Items.Add(categoryName);
                            Item item = new Item();
                            item.Text = reader.GetString(0);
                            item.Id = reader.GetInt32(1);
                            cmbCategory2.Items.Add(item);
                        }
                    }
                    cmbCategory2.SelectedIndex = 0;
                }

            }
        }
        private void picSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch, "Search");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            ImageUpload(picPhoto);
        }

        private void btnBrowse1_Click(object sender, EventArgs e)
        {
            ImageUpload(picPhoto1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter product name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (picPhoto.Image == null)
            {
                MessageBox.Show("Please select image.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudRate.Value == 0)
            {
                MessageBox.Show("Please enter rate.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudQuantity.Value == 0)
            {
                MessageBox.Show("Please enter quantity.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudWarranty.Value == 0)
            {
                MessageBox.Show("Please enter warranty.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbBrand.SelectedIndex == 0)
            {
                MessageBox.Show("Please select brand.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbCategory.SelectedIndex == 0)
            {
                MessageBox.Show("Please select category.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus.SelectedIndex == 0)
            {
                MessageBox.Show("Please select status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtDetails.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please select Details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                //MessageBox.Show(cmbBrand.SelectedValue.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (MemoryStream picStream = new MemoryStream())
                    {
                        picPhoto.Image.Save(picStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] bytePic = picStream.ToArray();
                        using (SqlCommand command1 = new SqlCommand("INSERT INTO Product  (Product_Name,Product_Image,Product_Price,Product_Quantity,Brand_Id,Category_Id ,Product_Warranty,Product_Status,Product_Details) " +
                            " OUTPUT inserted.Product_Id VALUES (@Product_Name,@Product_Image,@Product_Price,@Product_Quantity,@Brand_Id,@Category_Id,@Product_Warranty,@Product_Status,@Product_Details);", connection))
                        {
                            //command1.Parameters.AddWithValue("@Product_Id", (int)command.ExecuteScalar() + i++);
                            command1.Parameters.AddWithValue("@Product_Name", txtProductName.Text.Trim());
                            command1.Parameters.AddWithValue("@Product_Image", bytePic);
                            command1.Parameters.AddWithValue("@Product_Price", Convert.ToInt32(nudRate.Value));
                            command1.Parameters.AddWithValue("@Product_Quantity", Convert.ToInt32(nudQuantity.Value));

                            command1.Parameters.AddWithValue("@Brand_Id", Convert.ToInt32((cmbBrand.SelectedItem as Item).Id.ToString()));
                            command1.Parameters.AddWithValue("@Category_Id", Convert.ToInt32((cmbCategory.SelectedItem as Item).Id.ToString()));
                            command1.Parameters.AddWithValue("@Product_Warranty", Convert.ToInt32(nudWarranty.Value));
                            command1.Parameters.AddWithValue("@Product_Status", cmbStatus.SelectedItem.ToString());
                            command1.Parameters.AddWithValue("@Product_Details", txtDetails.Text.Trim());

                            command1.ExecuteNonQuery();
                            EmptyBox();
                        }
                    }
                }
            }
        }

        private void tpAddProduct_Enter(object sender, EventArgs e)
        {
            EmptyBox();
        }

        private void tpManageProduct_Enter(object sender, EventArgs e)
        {
            ComboBoxAutoFill1();
            LoadData();
            txtSearchProductName.Clear();
            dgvProduct.Columns[0].Visible = false;
            //dgvProduct.Columns[1].Visible = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT p.Product_Id, p.Product_Name, p.Product_Image,p.Product_Price,b.Brand_Name, c.Category_Name, p.Product_Quantity, p.Product_Warranty, p.Product_Status, p.Product_Details, p.Brand_Id,p.Category_Id\r\nFROM Product p\r\nINNER JOIN Brand b\r\nON b.Brand_Id = p.Brand_Id\r\nINNER JOIN Category c\r\nON c.Category_Id = p.Category_Id", connection))
                {
                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvProduct.DataSource = dataTable;
                    }
                    lblTotal.Text = dgvProduct.Rows.Count.ToString();
                }
            }
            dgvProduct.Columns[10].Visible = false;
            dgvProduct.Columns[11].Visible = false;

        }

        private void txtSearchProductName_TextChanged(object sender, EventArgs e)
        {
            ComboBoxAutoFill1();
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command1 = new SqlCommand("SELECT p.Product_Id, p.Product_Name, p.Product_Image,p.Product_Price,b.Brand_Name, c.Category_Name,p.Product_Quantity,p.Product_Warranty,p.Product_Status,p.Product_Details FROM Product p\r\nINNER JOIN Brand b\r\nON b.Brand_Id = p.Brand_Id\r\nINNER JOIN Category c\r\nON c.Category_Id = p.Category_Id WHERE Product_Name LIKE '%" + txtSearchProductName.Text + "%';", connection))
            //    {

            //        using (var reader = command1.ExecuteReader())
            //        {
            //            var dataTable = new DataTable();
            //            dataTable.Load(reader);
            //            dgvProduct.DataSource = dataTable;
            //        }
            //        lblTotal.Text = dgvProduct.Rows.Count.ToString();
            //    }
            //}
            var searchText = txtSearchProductName.Text.ToLower();
            var filteredProducts = products.Where(p => p.Product_Name.ToLower().Contains(searchText)).ToList();

            dgvProduct.DataSource = filteredProducts;

            lblTotal.Text = dgvProduct.Rows.Count.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ComboBoxAutoFill();
                DataGridViewRow row = dgvProduct.Rows[e.RowIndex];
                Id = row.Cells[0].Value.ToString();
                txtProductName1.Text = row.Cells[1].Value.ToString();
                image = (byte[])row.Cells[2].Value;
                memoryStream = new MemoryStream(image);
                picPhoto1.Image = Image.FromStream(memoryStream);
                nudRate1.Value = Convert.ToInt32(row.Cells[3].Value.ToString());


                //Convert.ToInt32((cmbBrand.SelectedItem as Item).Id.ToString());
                //cmbBrand1.SelectedItem = cmbBrand1.FindStringExact(row.Cells[4].Value.ToString());
                foreach (var item in cmbBrand1.Items)
                {
                    if (item.ToString() == row.Cells[4].Value.ToString())
                    {
                        cmbBrand1.SelectedItem = item;
                    }
                }
                foreach (var item in cmbCategory1.Items)
                {
                    if (item.ToString() == row.Cells[5].Value.ToString())
                    {
                        cmbCategory1.SelectedItem = item;
                    }
                }
                nudQuantity1.Value = Convert.ToInt32(row.Cells[6].Value.ToString());
                nudWarranty1.Value = Convert.ToInt32(row.Cells[7].Value.ToString());
                cmbStatus1.SelectedItem = row.Cells[8].Value.ToString();
                txtDetails1.Text = row.Cells[9].Value.ToString();
                tcProduct.SelectedTab = tpOptions;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {

            if (Id == "")
            {
                MessageBox.Show("Please select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (txtProductName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter product name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (picPhoto1.Image == null)
            {
                MessageBox.Show("Please select image.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (nudRate1.Value == 0)
            {
                MessageBox.Show("Please enter rate.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudQuantity1.Value == 0)
            {
                MessageBox.Show("Please enter quantity.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudWarranty1.Value == 0)
            {
                MessageBox.Show("Please enter warranty.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbBrand1.SelectedIndex == 0)
            {
                MessageBox.Show("Please select brand.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbCategory1.SelectedIndex == 0)
            {
                MessageBox.Show("Please select category.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus1.SelectedIndex == 0)
            {
                MessageBox.Show("Please select status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtDetails1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please select Details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (MemoryStream picStream = new MemoryStream())
                    {
                        picPhoto1.Image.Save(picStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] bytePic = picStream.ToArray();
                        using (SqlCommand command1 = new SqlCommand("UPDATE Product SET Product_Name = @Product_Name where Product_Id = @Product_Id and not exists(select * from Product where Product_Name = @Product_Name)", connection))
                        using (SqlCommand command2 = new SqlCommand("UPDATE Product SET Product_Image = @Product_Image where Product_Id = @Product_Id", connection))
                        using (SqlCommand command3 = new SqlCommand("UPDATE Product SET Product_Price = @Product_Price where Product_Id = @Product_Id", connection))
                        using (SqlCommand command4 = new SqlCommand("UPDATE Product SET Product_Quantity = @Product_Quantity where Product_Id = @Product_Id", connection))
                        using (SqlCommand command6 = new SqlCommand("UPDATE Product SET Brand_Id = @Brand_Id where Product_Id = @Product_Id", connection))
                        using (SqlCommand command7 = new SqlCommand("UPDATE Product SET Category_Id = @Category_Id where Product_Id = @Product_Id", connection))
                        using (SqlCommand command5 = new SqlCommand("UPDATE Product SET Product_Warranty = @Product_Warranty where Product_Id = @Product_Id", connection))
                        using (SqlCommand command8 = new SqlCommand("UPDATE Product SET Product_Status = @Product_Status where Product_Id = @Product_Id", connection))
                        using (SqlCommand command9 = new SqlCommand("UPDATE Product SET Product_Details = @Product_Details where Product_Id = @Product_Id", connection))
                        //using (SqlCommand command1 = new SqlCommand("UPDATE Product SET ( Product_Name, Product_Image, Product_Rate, Product_Quantity, Product_Brand, Product_Category, Product_Status) VALUES ( @Product_Name, @Product_Image, @Product_Rate, @Product_Quantity, @Product_Brand, @Product_Category, @Product_Status);", connection))
                        {
                            command1.Parameters.AddWithValue("@Product_Name", txtProductName1.Text.Trim());
                            command1.Parameters.AddWithValue("@Product_Id", Id);
                            command2.Parameters.AddWithValue("@Product_Image", bytePic);
                            command2.Parameters.AddWithValue("@Product_Id", Id);
                            command3.Parameters.AddWithValue("@Product_Price", Convert.ToInt32(nudRate1.Value));
                            command3.Parameters.AddWithValue("@Product_Id", Id);
                            command4.Parameters.AddWithValue("@Product_Quantity", Convert.ToInt32(nudQuantity1.Value));
                            command4.Parameters.AddWithValue("@Product_Id", Id);
                            command6.Parameters.AddWithValue("@Brand_Id", Convert.ToInt32((cmbBrand1.SelectedItem as Item).Id.ToString()));
                            command6.Parameters.AddWithValue("@Product_Id", Id);
                            command7.Parameters.AddWithValue("@Category_Id", Convert.ToInt32((cmbCategory1.SelectedItem as Item).Id.ToString()));
                            command7.Parameters.AddWithValue("@Product_Id", Id);
                            command5.Parameters.AddWithValue("@Product_Warranty", Convert.ToInt32(nudWarranty1.Value));
                            command5.Parameters.AddWithValue("@Product_Id", Id);
                            command8.Parameters.AddWithValue("@Product_Status", cmbStatus1.SelectedItem.ToString());
                            command8.Parameters.AddWithValue("@Product_Id", Id);
                            command9.Parameters.AddWithValue("@Product_Details", txtDetails1.Text.Trim());
                            command9.Parameters.AddWithValue("@Product_Id", Id);


                            command1.ExecuteNonQuery();
                            command2.ExecuteNonQuery();
                            command3.ExecuteNonQuery();
                            command4.ExecuteNonQuery();
                            command5.ExecuteNonQuery();
                            command6.ExecuteNonQuery();
                            command7.ExecuteNonQuery();
                            command8.ExecuteNonQuery();
                            command9.ExecuteNonQuery();
                            tcProduct.SelectedTab = tpManageProduct;
                            EmptyBox1();

                        }
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (Id == "")
            {
                MessageBox.Show("Please select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtProductName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter product name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (picPhoto1.Image == null)
            {
                MessageBox.Show("Please select image.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudRate1.Value == 0)
            {
                MessageBox.Show("Please enter rate.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudQuantity1.Value == 0)
            {
                MessageBox.Show("Please enter quantity.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudWarranty1.Value == 0)
            {
                MessageBox.Show("Please enter warranty.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbBrand1.SelectedIndex == 0)
            {
                MessageBox.Show("Please select brand.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbCategory1.SelectedIndex == 0)
            {
                MessageBox.Show("Please select category.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus1.SelectedIndex == 0)
            {
                MessageBox.Show("Please select status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtDetails1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please select Details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you want to delete this product", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command1 = new SqlCommand("DELETE FROM Product WHERE Product_Id = @productid", connection))
                        {

                            command1.Parameters.AddWithValue("@productid", Id);
                            int rowsAffected = command1.ExecuteNonQuery();

                            if (rowsAffected > 0)
                                Console.WriteLine($"Row with ID {Id} deleted successfully.");
                            else
                                Console.WriteLine($"No rows found with ID {Id}.");
                            EmptyBox1();
                            tcProduct.SelectedTab = tpManageProduct;
                        }
                    }
                }

            }
        }

        private void tpOptions_Enter(object sender, EventArgs e)
        {
            if (Id == "")
            {
                tcProduct.SelectedTab = tpManageProduct;
            }
        }

        private void tpOptions_Leave(object sender, EventArgs e)
        {
            EmptyBox1();
        }

        private void picSearchPrice_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ NumericUpDown controls
            decimal lowPrice = nudLowPrice.Value * 1000000;
            decimal highPrice = nudHighPrice.Value * 1000000;
            if (highPrice < lowPrice)
            {
                MessageBox.Show("Error Value !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Product WHERE Product_Price BETWEEN @lowPrice AND @highPrice";

                using (SqlCommand command1 = new SqlCommand(sql, connection))
                {
                    // Thêm tham số để ngăn chặn SQL injection
                    command1.Parameters.AddWithValue("@lowPrice", lowPrice);
                    command1.Parameters.AddWithValue("@highPrice", highPrice);

                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvProduct.DataSource = dataTable;
                    }
                    lblTotal.Text = dgvProduct.Rows.Count.ToString();
                }
            }
        }

        private void cmbBrand2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbBrand2.SelectedIndex != 0 && cmbCategory2.SelectedIndex != 0)
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        using (SqlCommand command1 = new SqlCommand("SELECT p.Product_Id, p.Product_Name, p.Product_Image,p.Product_Price,b.Brand_Name, c.Category_Name,p.Product_Quantity,p.Product_Warranty,p.Product_Status, p.Product_Details\r\nFROM Product p\r\nINNER JOIN Brand b\r\nON b.Brand_Id = p.Brand_Id\r\nINNER JOIN Category c\r\nON c.Category_Id = p.Category_Id WHERE p.Brand_Id = @BrandId AND p.Category_Id = @Category_Id;", connection))
            //        {
            //            command1.Parameters.AddWithValue("@BrandId", Convert.ToInt32((cmbBrand2.SelectedItem as Item).Id.ToString()));
            //            command1.Parameters.AddWithValue("@Category_Id", Convert.ToInt32((cmbCategory2.SelectedItem as Item).Id.ToString()));
            //            using (var reader = command1.ExecuteReader())
            //            {
            //                var dataTable = new DataTable();
            //                dataTable.Load(reader);
            //                dgvProduct.DataSource = dataTable;
            //            }
            //            lblTotal.Text = dgvProduct.Rows.Count.ToString();
            //        }
            //    }
            //}
            //else if (cmbBrand2.SelectedIndex != 0)
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        using (SqlCommand command1 = new SqlCommand("SELECT p.Product_Id, p.Product_Name, p.Product_Image,p.Product_Price,b.Brand_Name, c.Category_Name,p.Product_Quantity,p.Product_Warranty,p.Product_Status, p.Product_Details\r\nFROM Product p\r\nINNER JOIN Brand b\r\nON b.Brand_Id = p.Brand_Id\r\nINNER JOIN Category c\r\nON c.Category_Id = p.Category_Id WHERE p.Brand_Id = @BrandId;", connection))
            //        {
            //            command1.Parameters.AddWithValue("@BrandId", Convert.ToInt32((cmbBrand2.SelectedItem as Item).Id.ToString()));
            //            using (var reader = command1.ExecuteReader())
            //            {
            //                var dataTable = new DataTable();
            //                dataTable.Load(reader);
            //                dgvProduct.DataSource = dataTable;
            //            }
            //            lblTotal.Text = dgvProduct.Rows.Count.ToString();
            //        }
            //    }
            //}
            //else if (cmbCategory2.SelectedIndex != 0)
            //{
            //    cmbCategory2.SelectedIndex = 0;
            //}
            //else
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        using (SqlCommand command1 = new SqlCommand("SELECT p.Product_Id, p.Product_Name, p.Product_Image,p.Product_Price,b.Brand_Name, c.Category_Name,p.Product_Quantity,p.Product_Warranty,p.Product_Status, p.Product_Details\r\nFROM Product p\r\nINNER JOIN Brand b\r\nON b.Brand_Id = p.Brand_Id\r\nINNER JOIN Category c\r\nON c.Category_Id = p.Category_Id;", connection))
            //        {

            //            using (var reader = command1.ExecuteReader())
            //            {
            //                var dataTable = new DataTable();
            //                dataTable.Load(reader);
            //                dgvProduct.DataSource = dataTable;
            //            }
            //            lblTotal.Text = dgvProduct.Rows.Count.ToString();
            //        }
            //    }
            //}

            List<ProductDto> filteredProducts;

            if (cmbBrand2.SelectedIndex != 0 && cmbCategory2.SelectedIndex != 0)
            {
                var brandName = (cmbBrand2.SelectedItem as Item).Text;
                var categoryName = (cmbCategory2.SelectedItem as Item).Text;
                filteredProducts = products.Where(p => p.Brand_Name == brandName && p.Category_Name == categoryName).ToList();
            }
            else if (cmbBrand2.SelectedIndex != 0)
            {
                var brandName = (cmbBrand2.SelectedItem as Item).Text;
                filteredProducts = products.Where(p => p.Brand_Name == brandName).ToList();
            }
            else
            {
                filteredProducts = products;
            }

            dgvProduct.DataSource = filteredProducts;
            lblTotal.Text = dgvProduct.Rows.Count.ToString();

        }

        private void cmbCategory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbBrand2.SelectedIndex != 0 && cmbCategory2.SelectedIndex != 0)
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        using (SqlCommand command1 = new SqlCommand("SELECT p.Product_Id, p.Product_Name, p.Product_Image,p.Product_Price,b.Brand_Name, c.Category_Name,p.Product_Quantity,p.Product_Warranty,p.Product_Status, p.Product_Details\r\nFROM Product p\r\nINNER JOIN Brand b\r\nON b.Brand_Id = p.Brand_Id\r\nINNER JOIN Category c\r\nON c.Category_Id = p.Category_Id WHERE p.Brand_Id = @BrandId AND p.Category_Id = @Category_Id;", connection))
            //        {
            //            command1.Parameters.AddWithValue("@BrandId", Convert.ToInt32((cmbBrand2.SelectedItem as Item).Id.ToString()));
            //            command1.Parameters.AddWithValue("@Category_Id", Convert.ToInt32((cmbCategory2.SelectedItem as Item).Id.ToString()));
            //            using (var reader = command1.ExecuteReader())
            //            {
            //                var dataTable = new DataTable();
            //                dataTable.Load(reader);
            //                dgvProduct.DataSource = dataTable;
            //            }
            //            lblTotal.Text = dgvProduct.Rows.Count.ToString();
            //        }
            //    }
            //}
            //else if (cmbCategory2.SelectedIndex != 0)
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        using (SqlCommand command1 = new SqlCommand("SELECT p.Product_Id, p.Product_Name, p.Product_Image,p.Product_Price,b.Brand_Name, c.Category_Name,p.Product_Quantity,p.Product_Warranty,p.Product_Status, p.Product_Details\r\nFROM Product p\r\nINNER JOIN Brand b\r\nON b.Brand_Id = p.Brand_Id\r\nINNER JOIN Category c\r\nON c.Category_Id = p.Category_Id WHERE p.Category_Id = @Category_Id;", connection))
            //        {
            //            command1.Parameters.AddWithValue("@Category_Id", Convert.ToInt32((cmbCategory2.SelectedItem as Item).Id.ToString()));
            //            using (var reader = command1.ExecuteReader())
            //            {
            //                var dataTable = new DataTable();
            //                dataTable.Load(reader);
            //                dgvProduct.DataSource = dataTable;
            //            }
            //            lblTotal.Text = dgvProduct.Rows.Count.ToString();
            //        }
            //    }
            //}
            //else if (cmbBrand2.SelectedIndex != 0)
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        using (SqlCommand command1 = new SqlCommand("SELECT p.Product_Id, p.Product_Name, p.Product_Image,p.Product_Price,b.Brand_Name, c.Category_Name,p.Product_Quantity,p.Product_Warranty,p.Product_Status, p.Product_Details\r\nFROM Product p\r\nINNER JOIN Brand b\r\nON b.Brand_Id = p.Brand_Id\r\nINNER JOIN Category c\r\nON c.Category_Id = p.Category_Id WHERE p.Brand_Id = @BrandId;", connection))
            //        {
            //            command1.Parameters.AddWithValue("@BrandId", Convert.ToInt32((cmbBrand2.SelectedItem as Item).Id.ToString()));
            //            using (var reader = command1.ExecuteReader())
            //            {
            //                var dataTable = new DataTable();
            //                dataTable.Load(reader);
            //                dgvProduct.DataSource = dataTable;
            //            }
            //            lblTotal.Text = dgvProduct.Rows.Count.ToString();
            //        }
            //    }
            //}
            //else
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        using (SqlCommand command1 = new SqlCommand("SELECT p.Product_Id, p.Product_Name, p.Product_Image,p.Product_Price,b.Brand_Name, c.Category_Name,p.Product_Quantity,p.Product_Warranty,p.Product_Status, p.Product_Details\r\nFROM Product p\r\nINNER JOIN Brand b\r\nON b.Brand_Id = p.Brand_Id\r\nINNER JOIN Category c\r\nON c.Category_Id = p.Category_Id;", connection))
            //        {

            //            using (var reader = command1.ExecuteReader())
            //            {
            //                var dataTable = new DataTable();
            //                dataTable.Load(reader);
            //                dgvProduct.DataSource = dataTable;
            //            }
            //            lblTotal.Text = dgvProduct.Rows.Count.ToString();
            //        }
            //    }
            //}
            List<ProductDto> filteredProducts;

            if (cmbBrand2.SelectedIndex != 0 && cmbCategory2.SelectedIndex != 0)
            {
                var brandName = (cmbBrand2.SelectedItem as Item).Text;
                var categoryName = (cmbCategory2.SelectedItem as Item).Text;
                filteredProducts = products.Where(p => p.Brand_Name == brandName && p.Category_Name == categoryName).ToList();
            }
            else if (cmbCategory2.SelectedIndex != 0)
            {
                var categoryName = (cmbCategory2.SelectedItem as Item).Text;
                filteredProducts = products.Where(p => p.Category_Name == categoryName).ToList();
            }
            else
            {
                filteredProducts = products;
            }

            dgvProduct.DataSource = filteredProducts;
            lblTotal.Text = dgvProduct.Rows.Count.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ComboBoxAutoFill1();
            txtSearchProductName.Clear();
            nudLowPrice.Value = 0;
            nudHighPrice.Value = 0;
            dgvProduct.Columns[0].Visible = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT p.Product_Id, p.Product_Name, p.Product_Image,p.Product_Price,b.Brand_Name, c.Category_Name,p.Product_Quantity,p.Product_Warranty,p.Product_Status, p.Product_Details\r\nFROM Product p\r\nINNER JOIN Brand b\r\nON b.Brand_Id = p.Brand_Id\r\nINNER JOIN Category c\r\nON c.Category_Id = p.Category_Id", connection))
                {
                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvProduct.DataSource = dataTable;
                    }
                    lblTotal.Text = dgvProduct.Rows.Count.ToString();
                }
            }
        }
    }
}