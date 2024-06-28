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

using BusinessLogicLayer;
using DTO;
using System.Drawing.Imaging;
using ComboBox = System.Windows.Forms.ComboBox;
using System.Diagnostics;
using DataAccessLayer;
using System.Reflection;

namespace Management_System.PAL
{
    public partial class UserControlProduct : UserControl
    {

        Product product = new Product();
        ProductBUS productbus = new ProductBUS();
        private List<ProductDto> products;
        private List<BrandDto> brands;
        private List<CategoryDto> categories;
        int a = 0;

        private byte[] GetImageBytes(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        public void LoadData()
        {
            ProductDAO productDAO = new ProductDAO();
            CategoryDAO categoryDAO = new CategoryDAO();
            BrandDAO brandDAO = new BrandDAO();
            try
            {
                products = ConvertDataTableToList<ProductDto>(productDAO.GetData());
                categories = ConvertDataTableToList<CategoryDto>(categoryDAO.GetData());
                brands = ConvertDataTableToList<BrandDto>(brandDAO.GetData());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<T> ConvertDataTableToList<T>(DataTable dt) where T : new()
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = new T();
                foreach (DataColumn column in dt.Columns)
                {
                    PropertyInfo property = item.GetType().GetProperty(column.ColumnName);
                    if (property != null && row[column] != DBNull.Value)
                    {
                        property.SetValue(item, row[column], null);
                    }
                }
                data.Add(item);
            }
            return data;
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

        BrandBUS brandBUS = new BrandBUS();
        CategoryBUS categoryBUS = new CategoryBUS();

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
            cmbStatus.SelectedIndex = 0;

            cmbBrand.DataSource = null;
            cmbBrand.Items.Clear();
            cmbBrand.Items.Add("--SELECT--");
            try
            {
                DataTable brandItem = brandBUS.Auto_Fill_Combobox();
                foreach (DataRow row in brandItem.Rows)
                {
                    Item item = new Item();
                    item.Text = row["Brand_Name"].ToString();
                    item.Id = Convert.ToInt32(row["Brand_Id"]);
                    cmbBrand.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (cmbBrand.Items.Count > 0)
            {
                cmbBrand.SelectedIndex = 0;
            }


            cmbCategory.DataSource = null;
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("--SELECT--");
            try
            {
                DataTable categoryItem = categoryBUS.Auto_Fill_Combobox();
                foreach (DataRow row in categoryItem.Rows)
                {
                    Item item = new Item();
                    item.Text = row["Category_Name"].ToString();
                    item.Id = Convert.ToInt32(row["Category_Id"]);
                    cmbCategory.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            try
            {
                DataTable brandItem = brandBUS.Auto_Fill_Combobox();
                foreach (DataRow row in brandItem.Rows)
                {
                    Item item = new Item();
                    item.Text = row["Brand_Name"].ToString();
                    item.Id = Convert.ToInt32(row["Brand_Id"]);
                    cmbBrand1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            cmbBrand1.SelectedIndex = 0;


            cmbCategory1.Items.Clear();
            cmbCategory1.Items.Add("--SELECT--");
            try
            {
                DataTable categoryItem = categoryBUS.Auto_Fill_Combobox();
                foreach (DataRow row in categoryItem.Rows)
                {
                    Item item = new Item();
                    item.Text = row["Category_Name"].ToString();
                    item.Id = Convert.ToInt32(row["Category_Id"]);
                    cmbCategory1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            cmbCategory1.SelectedIndex = 0;
        }

        private void ComboBoxAutoFill1()
        {
            cmbBrand2.Items.Clear();
            cmbBrand2.Items.Add("All Brands");
            try
            {
                DataTable brandItem = brandBUS.Auto_Fill_Combobox();
                foreach (DataRow row in brandItem.Rows)
                {
                    Item item = new Item();
                    item.Text = row["Brand_Name"].ToString();
                    item.Id = Convert.ToInt32(row["Brand_Id"]);
                    cmbBrand2.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            cmbBrand2.SelectedIndex = 0;



            cmbCategory2.Items.Clear();
            cmbCategory2.Items.Add("All Categories");
            try
            {
                DataTable categoryItem = categoryBUS.Auto_Fill_Combobox();
                foreach (DataRow row in categoryItem.Rows)
                {
                    Item item = new Item();
                    item.Text = row["Category_Name"].ToString();
                    item.Id = Convert.ToInt32(row["Category_Id"]);
                    cmbCategory2.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            cmbCategory2.SelectedIndex = 0;


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
                product.ProductName = txtProductName.Text.Trim();
                product.ProductImage = GetImageBytes(picPhoto.Image);
                product.ProductPrice = Convert.ToInt32(nudRate.Value);
                product.ProductQuantity = Convert.ToInt32(nudQuantity.Value);
                product.BrandId = Convert.ToInt32((cmbBrand.SelectedItem as Item).Id.ToString());
                product.CategoryId = Convert.ToInt32((cmbCategory.SelectedItem as Item).Id.ToString());
                product.ProductWarranty = Convert.ToInt32(nudWarranty.Value);
                product.ProductStatus = cmbStatus.SelectedItem.ToString();
                product.ProductDetails = txtDetails.Text.Trim();

                try
                {
                    productbus.Insert(product);
                    MessageBox.Show("Adding Successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EmptyBox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Adding Fail! {ex.Message}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                dgvProduct.DataSource = productbus.GetData();
                lblTotal.Text = dgvProduct.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("View Product is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            dgvProduct.Columns[10].Visible = false;
            dgvProduct.Columns[11].Visible = false;
        }

        private void txtSearchProductName_TextChanged(object sender, EventArgs e)
        {
            ComboBoxAutoFill1();

            try
            {
                dgvProduct.DataSource = productbus.GetDataByName(txtSearchProductName.Text);
                lblTotal.Text = dgvProduct.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("Search Bar is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
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

                product.ProductId = Convert.ToInt32(Id);
                product.ProductName = txtProductName1.Text.Trim();
                product.ProductImage = GetImageBytes(picPhoto1.Image);
                product.ProductPrice = Convert.ToInt32(nudRate1.Value);
                product.ProductQuantity = Convert.ToInt32(nudQuantity1.Value);
                product.BrandId = Convert.ToInt32((cmbBrand1.SelectedItem as Item).Id.ToString());
                product.CategoryId = Convert.ToInt32((cmbCategory1.SelectedItem as Item).Id.ToString());
                product.ProductWarranty = Convert.ToInt32(nudWarranty1.Value);
                product.ProductStatus = cmbStatus1.SelectedItem.ToString();
                product.ProductDetails = txtDetails1.Text.Trim();

                try
                {
                    productbus.Update(product);
                    MessageBox.Show("Update Successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tcProduct.SelectedTab = tpManageProduct;
                    EmptyBox1();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Update Fail! {ex.Message}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    try
                    {
                        productbus.Delete(Id);
                        Console.WriteLine($"Row with ID {Id} deleted successfully.");
                        EmptyBox1();
                        tcProduct.SelectedTab = tpManageProduct;
                    }
                    catch
                    {
                        MessageBox.Show("Having an Order that contain this Product", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.WriteLine($"No rows found with ID {Id}.");
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
            decimal lowPrice = nudLowPrice.Value * 1000000;
            decimal highPrice = nudHighPrice.Value * 1000000;
            if (highPrice < lowPrice)
            {
                MessageBox.Show("Error Value !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DataTable dt = productbus.GetProductsByPriceRange(lowPrice, highPrice);

                dgvProduct.DataSource = dt;
                lblTotal.Text = dgvProduct.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void cmbBrand2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            List<ProductDto> filteredProducts;
            
            if (cmbBrand2.SelectedIndex != 0 && cmbCategory2.SelectedIndex != 0)
            {
                var brandName = (cmbBrand2.SelectedItem as Item).Text;
                var categoryName = (cmbCategory2.SelectedItem as Item).Text;
                filteredProducts = products.Where(p => p.Brand_Name == brandName && p.Category_Name == categoryName).ToList();
                a++;
            }
            else if (cmbBrand2.SelectedIndex != 0)
            {
                var brandName = (cmbBrand2.SelectedItem as Item).Text;
                filteredProducts = products.Where(p => p.Brand_Name == brandName).ToList();
                a++;
            }
            
            else if (a != 0)
            {
                var categoryName = (cmbCategory2.SelectedItem as Item).Text; // chỉ cần không sử dụng cái này lần đầu tiên là được
                filteredProducts = products.Where(p => p.Category_Name == categoryName).ToList();
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
            try
            {
                dgvProduct.DataSource = productbus.GetData();
                lblTotal.Text = dgvProduct.Rows.Count.ToString();
            }
            catch
            {
                MessageBox.Show("View Product is error now!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            dgvProduct.Columns[10].Visible = false;
            dgvProduct.Columns[11].Visible = false;
        }
    }
}