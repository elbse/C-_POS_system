using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;

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
            AddProduct("Apple", 15.00m, 50);
            AddProduct("Banana", 10.00m, 30);
            AddProduct("Milk", 25.49m, 20);
            AddProduct("Bread", 45.99m, 25);
            AddProduct("Eggs", 11.00m, 40);

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

            //Check if product exists in inventory
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

                // Update stock
                product.Quantity -= quantityDifference;

                // Update cart row
                row.Cells["Product"].Value = productName;
                row.Cells["Price"].Value = product.Price;
                row.Cells["Quantity"].Value = quantity;
                
            }

            
            MessageBox.Show("Cart item updated successfully!",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


       private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                dgvCart.Rows.RemoveAt(dgvCart.SelectedRows[0].Index);
                ClearInputs();
                UpdateTotal();

                MessageBox.Show("Cart item deleted successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a row to delete.",
                    "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            // Check if there is anything to clear
            if (string.IsNullOrWhiteSpace(cmbProduct.Text) &&
                string.IsNullOrWhiteSpace(txtPrice.Text) &&
                string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show(
                    "There are no input fields to clear.",
                    "Nothing to Clear",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Clear inputs
            ClearInputs();

            MessageBox.Show(
                "Input fields cleared successfully!",
                "Cleared",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }



        private const decimal TAX_RATE = 0.12m; // 12% tax


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

            decimal subTotal = CalculateTotal();
            decimal tax = subTotal * TAX_RATE;
            decimal grandTotal = subTotal + tax;

            // Show confirmation
            DialogResult confirmResult = MessageBox.Show(
                $"Total amount: {grandTotal:C2}\n\nComplete purchase?",
                "Confirm Purchase",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
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

                // Show receipt window
                ShowReceipt(subTotal, tax, grandTotal);

                // Clear cart
                cartTable.Rows.Clear();
                UpdateTotal();
                RefreshProductComboBox(); // Refresh to show updated stock
            }
        }

        private void ShowReceipt(decimal subTotal, decimal tax, decimal grandTotal)
        {
            // Create receipt window
            Form receiptWindow = new Form();
            receiptWindow.Text = "Receipt";
            receiptWindow.Size = new System.Drawing.Size(450, 550);
            receiptWindow.FormBorderStyle = FormBorderStyle.FixedDialog;
            receiptWindow.MaximizeBox = false;
            receiptWindow.MinimizeBox = false;
            receiptWindow.StartPosition = FormStartPosition.CenterParent;

            // Receipt text box
            TextBox receiptText = new TextBox();
            receiptText.Multiline = true;
            receiptText.ScrollBars = ScrollBars.Vertical;
            receiptText.Font = new System.Drawing.Font("Courier New", 10);
            receiptText.ReadOnly = true;
            receiptText.Location = new System.Drawing.Point(10, 10);
            receiptText.Size = new System.Drawing.Size(415, 450);

            // Generate receipt content
            string receipt = new string('=', 45) + "\r\n";
            receipt += "           POS SYSTEM RECEIPT\r\n";
            receipt += new string('=', 45) + "\r\n";
            receipt += $"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\r\n";
            receipt += new string('=', 45) + "\r\n\r\n";

            receipt += $"{"Item",-20} {"Qty",-5} {"Price",-10} {"Total",-10}\r\n";
            receipt += new string('-', 45) + "\r\n";

            // Loop through cart items
            foreach (DataRow row in cartTable.Rows)
            {
                string product = row["Product"].ToString();
                int quantity = Convert.ToInt32(row["Quantity"]);
                decimal price = Convert.ToDecimal(row["Price"]);
                decimal total = Convert.ToDecimal(row["Total"]);

                receipt += $"{product,-20} {quantity,-5} ₱{price,-9:F2} ₱{total,-9:F2}\r\n";
            }

            receipt += new string('-', 45) + "\r\n";

            receipt += $"\r\n{"Subtotal:",-35} ₱{subTotal,8:F2}\r\n";
            receipt += $"{"Tax (12%):",-35} ₱{tax,8:F2}\r\n";
            receipt += new string('=', 45) + "\r\n";
            receipt += $"{"GRAND TOTAL:",-35} ₱{grandTotal,8:F2}\r\n";
            receipt += new string('=', 45) + "\r\n\r\n";
            receipt += "      Thank you for your purchase!\r\n";
            receipt += "           Please come again!\r\n";
            receipt += new string('=', 45) + "\r\n";

            receiptText.Text = receipt;

            // Print button
            Button printBtn = new Button();
            printBtn.Text = "Print";
            printBtn.BackColor = System.Drawing.Color.FromArgb(92, 184, 92);
            printBtn.ForeColor = System.Drawing.Color.White;
            printBtn.FlatStyle = FlatStyle.Flat;
            printBtn.Size = new System.Drawing.Size(140, 35);
            printBtn.Location = new System.Drawing.Point(80, 470);
            printBtn.Click += (s, e) => PrintReceipt(receipt);

            // Close button
            Button closeBtn = new Button();
            closeBtn.Text = "Close";
            closeBtn.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
            closeBtn.ForeColor = System.Drawing.Color.White;
            closeBtn.FlatStyle = FlatStyle.Flat;
            closeBtn.Size = new System.Drawing.Size(140, 35);
            closeBtn.Location = new System.Drawing.Point(230, 470);
            closeBtn.Click += (s, e) => receiptWindow.Close();

            // Add controls to form
            receiptWindow.Controls.Add(receiptText);
            receiptWindow.Controls.Add(printBtn);
            receiptWindow.Controls.Add(closeBtn);

            receiptWindow.ShowDialog();
        }

        private void PrintReceipt(string receiptText)
        {
            // Create receipts folder if it doesn't exist
            string folder = "receipts";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            // Generate filename with timestamp
            string filename = $"{folder}/receipt_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

            try
            {
                File.WriteAllText(filename, receiptText);
                MessageBox.Show($"Receipt saved to {filename}", "Success",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save receipt: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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



    
        private void AddProduct(string name, decimal price, int quantity)
        {
            if (products.ContainsKey(name))
            {
                
                products[name].Price = price;
                products[name].Quantity = quantity;
            }
            else
            {
                
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
        private decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (DataRow row in cartTable.Rows)
            {
                total += (decimal)row["Total"];
            }
            return total;
        }

        private void UpdateTotal()
        {
            decimal subTotal = CalculateTotal();
            lblTotal.Text = $"Total: {subTotal:C2}";

            // Update grand total with tax
            decimal tax = subTotal * TAX_RATE;
            decimal grandTotal = subTotal + tax;
            lblGrandTotal.Text = $"Grand Total (incl. tax): {grandTotal:C2}";
        }


        
    }
}