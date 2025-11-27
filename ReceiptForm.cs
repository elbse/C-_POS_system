using System;
using System.Windows.Forms;

namespace C__POS_System
{
    public partial class ReceiptForm : Form
    {
        public ReceiptForm(string receiptText)
        {
            InitializeComponent();
            txtReceipt.Text = receiptText;
        }
    }
}
