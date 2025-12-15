namespace SimplePOS
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.Button btnManageProducts;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblGrandTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.cmbProduct.Location = new System.Drawing.Point(80, 12);
            this.cmbProduct.Size = new System.Drawing.Size(150, 21);
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown; 
            this.cmbProduct.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbProduct.SelectedIndexChanged += new System.EventHandler(this.cmbProduct_SelectedIndexChanged);

            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            // this.btnReceipt = new System.Windows.Forms.Button();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblGrandTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            // txtProduct
          


            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.SuspendLayout();

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Text = "Product:";

            
            // cmbProduct
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.cmbProduct.Location = new System.Drawing.Point(80, 12);
            this.cmbProduct.Size = new System.Drawing.Size(150, 21);
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProduct.SelectedIndexChanged += new System.EventHandler(this.cmbProduct_SelectedIndexChanged);
            this.cmbProduct.TextChanged += new System.EventHandler(this.cmbProduct_TextChanged);


            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 15);
            this.label2.Text = "Price:";

            // txtPrice
            this.txtPrice.Location = new System.Drawing.Point(290, 12);
            this.txtPrice.Size = new System.Drawing.Size(80, 20);
            this.txtPrice.Enabled = false;
            this.txtPrice.BackColor = System.Drawing.Color.White;  
            this.txtPrice.ForeColor = System.Drawing.Color.Black;  


            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(390, 15);
            this.label3.Text = "Qty:";

            // txtQuantity
            this.txtQuantity.Location = new System.Drawing.Point(420, 12);
            this.txtQuantity.Size = new System.Drawing.Size(60, 20);

            // btnAdd
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(15, 45);
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnUpdate
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(115, 45);
            this.btnUpdate.Size = new System.Drawing.Size(100, 30);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // btnDelete
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(215, 45);
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnClear
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(315, 45);
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // btnPurchase
            this.btnPurchase.BackColor = System.Drawing.Color.FromArgb(0, 150, 136);
            this.btnPurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPurchase.ForeColor = System.Drawing.Color.White;
            this.btnPurchase.Location = new System.Drawing.Point(415, 45);
            this.btnPurchase.Size = new System.Drawing.Size(100, 30);
            this.btnPurchase.Text = "Purchase";
            this.btnPurchase.Click += new System.EventHandler(this.btnPurchase_Click);

            // btnManageProducts
            this.btnManageProducts = new System.Windows.Forms.Button();
            this.btnManageProducts.BackColor = System.Drawing.Color.FromArgb(156, 39, 176);
            this.btnManageProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageProducts.ForeColor = System.Drawing.Color.White;
            this.btnManageProducts.Location = new System.Drawing.Point(15, 420);
            this.btnManageProducts.Size = new System.Drawing.Size(120, 30);
            this.btnManageProducts.Text = "Manage Products";
            this.btnManageProducts.Click += new System.EventHandler(this.btnManageProducts_Click);

            // dgvCart
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.Location = new System.Drawing.Point(15, 85);
            this.dgvCart.Size = new System.Drawing.Size(505, 330);
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellClick);

            // lblTotal - Subtotal
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.lblTotal.Location = new System.Drawing.Point(15, 420); // slightly above bottom
            this.lblTotal.Size = new System.Drawing.Size(505, 25);
            this.lblTotal.Text = "Total: ₱0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // lblGrandTotal - Total including tax
            this.lblGrandTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblGrandTotal.ForeColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.lblGrandTotal.Location = new System.Drawing.Point(15, 450); // below lblTotal
            this.lblGrandTotal.Size = new System.Drawing.Size(505, 25);
            this.lblGrandTotal.Text = "Grand Total (incl. tax): ₱0.00";
            this.lblGrandTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;


            // Form1
            this.ClientSize = new System.Drawing.Size(540, 480);
            this.Controls.Add(this.btnManageProducts);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblGrandTotal);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.btnPurchase);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbProduct);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS System - Point of Sale";

            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
