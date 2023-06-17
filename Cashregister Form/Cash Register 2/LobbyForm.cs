using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cash_Register_2
{
    public partial class LobbyForm : Form
    {
        public LobbyForm()
        {
            InitializeComponent();
        }

        private void btnAddCashier_Click(object sender, EventArgs e)
        {
           
            AddCashierForm addCashierForm = new AddCashierForm();

         
            addCashierForm.ShowDialog();
        }
        private void btnAddItems_Click(object sender, EventArgs e)
        {
            // Open the Add Items form
            var addItemsForm = new AddItemsForm();
            addItemsForm.ShowDialog();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            // Open the Purchase form
            var purchaseForm = new PurchaseForm();
            purchaseForm.ShowDialog();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            // Close the application
            Application.Exit();
        }

        private void LobbyForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCashierForm addCashierForm = new AddCashierForm();


            addCashierForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var addItemForm = new AddItemsForm();
            addItemForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PurchaseForm purchaseForm = new PurchaseForm();
            purchaseForm.Show();
            this.Hide();
        }
    }
}