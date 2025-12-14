using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SimplePOS
{
    // Define a Product class with inventory tracking
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } // Stock quantity

        public Product(string name, decimal price, int quantity = 0)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Name} (Stock: {Quantity})";
        }
    }

    public partial class Form1 : Form
    {
        private DataTable cartTable;
        private Dictionary<string, Product> products; // Store products by name for easy lookup

        public Form1()
        {
            InitializeComponent();
            InitializeProducts();
            InitializeCart();
        }

        private void InitializeProducts()
        {
            // Initialize product dictionary
            products = new Dictionary<string, Product>(StringComparer.OrdinalIgnoreCase);
            
            // Add some default products with stock
            AddProduct("Apple", 0.99m, 50);
            AddProduct("Banana", 0.59m, 30);
            AddProduct("Milk", 2.49m, 20);
            AddProduct("Bread", 1.99m, 25);
            AddProduct("Eggs", 3.49m, 40);

            RefreshProductComboBox();
        }

        private void RefreshProductComboBox()
        {
            cmbProduct.Items.Clear();
            foreach (var product in products.Values.OrderBy(p => p.Name))
            {
                cmbProduct.Items.Add(product.Name);
            }
        }

        private void InitializeCart()
        {
            // Create a DataTable to store cart items
            cartTable = new DataTable();
            cartTable.Columns.Add("Product");
            cartTable.Columns.Add("Price", typeof(decimal));
            cartTable.Columns.Add("Quantity", typeof(int));
            cartTable.Columns.Add("Total", typeof(decimal), "Price * Quantity");

            dgvCart.DataSource = cartTable;

            dgvCart.Columns["Price"].DefaultCellStyle.Format = "C2";
            dgvCart.Columns["Total"].DefaultCellStyle.Format = "C2";
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePriceFromProduct();
        }

        private void cmbProduct_TextChanged(object sender, EventArgs e)
        {
            // When user types, try to find matching product
            UpdatePriceFromProduct();
        }

        private void UpdatePriceFromProduct()
        {
            string productName = cmbProduct.Text.Trim();
            if (string.IsNullOrEmpty(productName))
            {
                txtPrice.Clear();
                return;
            }

            // Check if product exists in inventory
            if (products.TryGetValue(productName, out Product product))
            {
                txtPrice.Text = product.Price.ToString("F2");
            }
            else
            {
                // Product doesn't exist, allow manual price entry
                // Don't clear if user is typing
            }
        }





        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(cmbProduct.Text))
            {
                MessageBox.Show("Please enter or select a product.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid price.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productName = cmbProduct.Text.Trim();

            // Check if product exists in inventory
            if (products.TryGetValue(productName, out Product existingProduct))
            {
                // Product exists - check stock availability
                if (existingProduct.Quantity < quantity)
                {
                    MessageBox.Show($"Insufficient stock! Available: {existingProduct.Quantity}", 
                        "Stock Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Use the product's price from inventory
                price = existingProduct.Price;
            }
            else
            {
                // New product - add to inventory with the entered price
                // Ask user if they want to add this as a new product
                DialogResult result = MessageBox.Show(
                    $"Product '{productName}' not found in inventory.\n\nWould you like to add it as a new product?",
                    "New Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Add new product to inventory (default stock: 0, user can update later)
                    AddProduct(productName, price, 0);
                    MessageBox.Show($"Product '{productName}' added to inventory.\nNote: Stock is 0. Please update stock if needed.",
                        "Product Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // Add to cart
            cartTable.Rows.Add(productName, price, quantity);

            // Clear inputs
            cmbProduct.Text = "";
            txtPrice.Clear();
            txtQuantity.Clear();
            cmbProduct.Focus();

            UpdateTotal();
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row in the cart to update.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate inputs
            if (string.IsNullOrWhiteSpace(cmbProduct.Text))
            {
                MessageBox.Show("Please enter or select a product.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid price.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productName = cmbProduct.Text.Trim();

            // Check stock if product exists
            if (products.TryGetValue(productName, out Product product))
            {
                // Get old quantity from cart
                DataGridViewRow row = dgvCart.SelectedRows[0];
                int oldQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                int quantityDifference = quantity - oldQuantity;

                // Check if we have enough stock for the increase
                if (quantityDifference > 0 && product.Quantity < quantityDifference)
                {
                    MessageBox.Show($"Insufficient stock! Available: {product.Quantity}", 
                        "Stock Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                price = product.Price; // Use inventory price
            }

            // Update selected row
            DataGridViewRow selectedRow = dgvCart.SelectedRows[0];
            selectedRow.Cells["Product"].Value = productName;
            selectedRow.Cells["Price"].Value = price;
            selectedRow.Cells["Quantity"].Value = quantity;

            ClearInputs();
            UpdateTotal();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                dgvCart.Rows.RemoveAt(dgvCart.SelectedRows[0].Index);
                ClearInputs();
                UpdateTotal();
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            if (cartTable.Rows.Count == 0)
            {
                MessageBox.Show("Cart is empty. Add items to purchase.", "Purchase", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Validate stock availability before purchase
            List<string> stockIssues = new List<string>();
            foreach (DataRow row in cartTable.Rows)
            {
                string productName = row["Product"].ToString();
                int cartQuantity = Convert.ToInt32(row["Quantity"]);

                if (products.TryGetValue(productName, out Product product))
                {
                    if (product.Quantity < cartQuantity)
                    {
                        stockIssues.Add($"{productName}: Need {cartQuantity}, Available {product.Quantity}");
                    }
                }
            }

            if (stockIssues.Count > 0)
            {
                MessageBox.Show("Insufficient stock for the following items:\n\n" + string.Join("\n", stockIssues),
                    "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Process purchase - deduct quantities from inventory
            foreach (DataRow row in cartTable.Rows)
            {
                string productName = row["Product"].ToString();
                int cartQuantity = Convert.ToInt32(row["Quantity"]);

                if (products.TryGetValue(productName, out Product product))
                {
                    product.Quantity -= cartQuantity;
                }
            }

            decimal total = CalculateTotal();
            MessageBox.Show($"Purchase successful!\n\nTotal amount: {total:C2}\n\nInventory quantities have been updated.",
                "Purchase Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear cart
            cartTable.Rows.Clear();
            UpdateTotal();
            RefreshProductComboBox(); // Refresh to show updated stock

            
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            if (cartTable.Rows.Count == 0)
            {
                MessageBox.Show("Cart is empty. Add items to generate a receipt.", "Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string receipt = "********** POS Receipt **********\n";
            foreach (DataRow row in cartTable.Rows)
            {
                receipt += $"{row["Product"]} x {row["Quantity"]} = {((decimal)row["Total"]):C2}\n";
            }
            receipt += $"---------------------------------------\n";
            receipt += $"TOTAL: {CalculateTotal():C2}\n";
            receipt += "***************************************";

            MessageBox.Show(receipt, "Receipt");
        }

        private void dgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCart.Rows[e.RowIndex];
                cmbProduct.Text = row.Cells["Product"].Value.ToString();
                txtPrice.Text = ((decimal)row.Cells["Price"].Value).ToString("F2");
                txtQuantity.Text = row.Cells["Quantity"].Value.ToString();
            }
        }



        // Product Management Methods
        private void AddProduct(string name, decimal price, int quantity)
        {
            if (products.ContainsKey(name))
            {
                // Update existing product
                products[name].Price = price;
                products[name].Quantity = quantity;
            }
            else
            {
                // Add new product
                products[name] = new Product(name, price, quantity);
            }
            RefreshProductComboBox();
        }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            using (ProductManagementForm pmForm = new ProductManagementForm(products))
            {
                if (pmForm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh product list after management
                    products = pmForm.GetProducts();
                    RefreshProductComboBox();
                }
            }
        }

        private void ClearInputs()
        {
            cmbProduct.Text = "";
            txtPrice.Clear();
            txtQuantity.Clear();
            cmbProduct.Focus();
        }


        private void UpdateTotal()
        {
            lblTotal.Text = $"Total: {CalculateTotal():C2}";
        }

        private decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (DataRow row in cartTable.Rows)
            {
                total += (decimal)row["Total"];
            }
            return total;
        }
    }
}
