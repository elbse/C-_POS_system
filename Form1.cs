using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C__POS_System
{
    public partial class Form1 : Form
    {
        Dictionary<string, double> products = new Dictionary<string, double>()
        {
            {"Burger", 50.00},
            {"Fries", 30.00},
            {"Spaghetti", 50.00}
        };

        double subtotal, tax, total;
        string productName;
        int qty;

        public Form1()
        {
            InitializeComponent();

            // Fill dropdown with products
            foreach (var item in products)
            {
                comboProducts.Items.Add($"{item.Key} - ₱{item.Value:F2}");
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (comboProducts.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a product");
                return;
            }

            if (!int.TryParse(txtQty.Text, out qty) || qty <= 0)
            {
                MessageBox.Show("Invalid quantity");
                return;
            }

            string selected = comboProducts.SelectedItem.ToString();
            productName = selected.Split('-')[0].Trim();

            double price = products[productName];
            subtotal = price * qty;
            tax = subtotal * 0.12;   // 12% VAT
            total = subtotal + tax;

            MessageBox.Show($"Total Amount: ₱{total:F2}", "Calculation Successful");
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(productName))
            {
                MessageBox.Show("Please calculate total first.");
                return;
            }

            string receipt =
                "===== RECEIPT =====\r\n" +
                $"Product : {productName}\r\n" +
                $"Price   : ₱{products[productName]:F2}\r\n" +
                $"Quantity: {qty}\r\n" +
                $"Subtotal: ₱{subtotal:F2}\r\n" +
                $"Tax 12% : ₱{tax:F2}\r\n" +
                $"TOTAL   : ₱{total:F2}\r\n" +
                "===================\r\n" +
                "Thank you for shopping!";

            ReceiptForm receiptForm = new ReceiptForm(receipt);
            receiptForm.ShowDialog();
        }
    }
}
