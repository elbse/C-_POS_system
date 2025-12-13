using System;
using System.Data;
using System.Windows.Forms;

namespace SimplePOS
{
    public partial class ReceiptForm : Form
    {
        public ReceiptForm(DataTable cart)
        {
            InitializeComponent();
            GenerateReceipt(cart);
        }





        private void GenerateReceipt(DataTable cart)
        {
            decimal subtotal = 0;

            txtReceipt.Clear();
            txtReceipt.AppendText("========================================\r\n");
            txtReceipt.AppendText("           SIMPLE POS SYSTEM\r\n");
            txtReceipt.AppendText("========================================\r\n");
            txtReceipt.AppendText($"Date: {DateTime.Now}\r\n\r\n");

            txtReceipt.AppendText("Product          Qty   Price   Total\r\n");
            txtReceipt.AppendText("----------------------------------------\r\n");

            foreach (DataRow row in cart.Rows)
            {
                decimal total = Convert.ToDecimal(row["Total"]);
                subtotal += total;

                txtReceipt.AppendText(
                    $"{row["Product"],-16} {row["Quantity"],3}   ${row["Price"],6:F2}   ${total:F2}\r\n");
            }

            txtReceipt.AppendText("----------------------------------------\r\n");
            txtReceipt.AppendText($"TOTAL:    ${subtotal:F2}\r\n");
            txtReceipt.AppendText("========================================\r\n");
            txtReceipt.AppendText("       Thank you for your purchase!\r\n");
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}