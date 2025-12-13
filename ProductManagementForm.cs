using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SimplePOS
{
    public partial class ProductManagementForm : Form
    {
        private Dictionary<string, Product> products;
        private DataGridView dgvProducts;
        private TextBox txtProductName;
        private TextBox txtProductPrice;
        private TextBox txtProductQuantity;
        private Button btnAddProduct;
        private Button btnUpdateProduct;
        private Button btnDeleteProduct;
        private Button btnClose;
        private Label label1;
        private Label label2;
        private Label label3;

        public ProductManagementForm(Dictionary<string, Product> existingProducts)
        {
            // Create a copy of the products dictionary
            products = new Dictionary<string, Product>(existingProducts, StringComparer.OrdinalIgnoreCase);
            foreach (var kvp in existingProducts)
            {
                products[kvp.Key] = new Product(kvp.Value.Name, kvp.Value.Price, kvp.Value.Quantity);
            }

            InitializeComponent();
            LoadProducts();
        }

        public Dictionary<string, Product> GetProducts()
        {
            return products;
        }

        private void InitializeComponent()
        {
            this.dgvProducts = new DataGridView();
            this.txtProductName = new TextBox();
            this.txtProductPrice = new TextBox();
            this.txtProductQuantity = new TextBox();
            this.btnAddProduct = new Button();
            this.btnUpdateProduct = new Button();
            this.btnDeleteProduct = new Button();
            this.btnClose = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();

            this.SuspendLayout();

            // dgvProducts
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.Location = new System.Drawing.Point(12, 12);
            this.dgvProducts.Size = new System.Drawing.Size(560, 300);
            this.dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.CellClick += new DataGridViewCellEventHandler(this.dgvProducts_CellClick);

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 330);
            this.label1.Text = "Product Name:";

            // txtProductName
            this.txtProductName.Location = new System.Drawing.Point(100, 327);
            this.txtProductName.Size = new System.Drawing.Size(150, 20);

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 330);
            this.label2.Text = "Price:";

            // txtProductPrice
            this.txtProductPrice.Location = new System.Drawing.Point(300, 327);
            this.txtProductPrice.Size = new System.Drawing.Size(100, 20);

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(410, 330);
            this.label3.Text = "Stock Qty:";

            // txtProductQuantity
            this.txtProductQuantity.Location = new System.Drawing.Point(480, 327);
            this.txtProductQuantity.Size = new System.Drawing.Size(92, 20);

            // btnAddProduct
            this.btnAddProduct.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnAddProduct.FlatStyle = FlatStyle.Flat;
            this.btnAddProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddProduct.Location = new System.Drawing.Point(12, 360);
            this.btnAddProduct.Size = new System.Drawing.Size(100, 30);
            this.btnAddProduct.Text = "Add Product";
            this.btnAddProduct.Click += new EventHandler(this.btnAddProduct_Click);

            // btnUpdateProduct
            this.btnUpdateProduct.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.btnUpdateProduct.FlatStyle = FlatStyle.Flat;
            this.btnUpdateProduct.ForeColor = System.Drawing.Color.White;
            this.btnUpdateProduct.Location = new System.Drawing.Point(118, 360);
            this.btnUpdateProduct.Size = new System.Drawing.Size(100, 30);
            this.btnUpdateProduct.Text = "Update Product";
            this.btnUpdateProduct.Click += new EventHandler(this.btnUpdateProduct_Click);

            // btnDeleteProduct
            this.btnDeleteProduct.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            this.btnDeleteProduct.FlatStyle = FlatStyle.Flat;
            this.btnDeleteProduct.ForeColor = System.Drawing.Color.White;
            this.btnDeleteProduct.Location = new System.Drawing.Point(224, 360);
            this.btnDeleteProduct.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteProduct.Text = "Delete Product";
            this.btnDeleteProduct.Click += new EventHandler(this.btnDeleteProduct_Click);

            // btnClose
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(472, 360);
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // ProductManagementForm
            this.ClientSize = new System.Drawing.Size(584, 405);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDeleteProduct);
            this.Controls.Add(this.btnUpdateProduct);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.txtProductQuantity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtProductPrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvProducts);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Product Management";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadProducts()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product Name");
            table.Columns.Add("Price", typeof(decimal));
            table.Columns.Add("Stock Quantity", typeof(int));

            foreach (var product in products.Values.OrderBy(p => p.Name))
            {
                table.Rows.Add(product.Name, product.Price, product.Quantity);
            }

            dgvProducts.DataSource = table;
            dgvProducts.Columns["Price"].DefaultCellStyle.Format = "C2";
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
                txtProductName.Text = row.Cells["Product Name"].Value.ToString();
                txtProductPrice.Text = ((decimal)row.Cells["Price"].Value).ToString("F2");
                txtProductQuantity.Text = row.Cells["Stock Quantity"].Value.ToString();
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Please enter a product name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtProductPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid price.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtProductQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Please enter a valid quantity (0 or greater).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productName = txtProductName.Text.Trim();

            if (products.ContainsKey(productName))
            {
                MessageBox.Show("Product already exists. Use Update to modify it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            products[productName] = new Product(productName, price, quantity);
            LoadProducts();
            ClearInputs();
            MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Please select or enter a product name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtProductPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid price.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtProductQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Please enter a valid quantity (0 or greater).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productName = txtProductName.Text.Trim();

            if (!products.ContainsKey(productName))
            {
                MessageBox.Show("Product not found. Use Add to create a new product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            products[productName].Price = price;
            products[productName].Quantity = quantity;
            LoadProducts();
            ClearInputs();
            MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Please select a product to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productName = txtProductName.Text.Trim();

            if (!products.ContainsKey(productName))
            {
                MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete '{productName}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                products.Remove(productName);
                LoadProducts();
                ClearInputs();
                MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ClearInputs()
        {
            txtProductName.Clear();
            txtProductPrice.Clear();
            txtProductQuantity.Clear();
            txtProductName.Focus();
        }
    }
}

