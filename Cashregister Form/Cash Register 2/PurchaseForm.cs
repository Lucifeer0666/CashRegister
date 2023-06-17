using CashApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cash_Register_2
{
    public partial class PurchaseForm : Form
    {
        private double total = 0;
        private IEnumerable<string> purchaseItems;

        public PurchaseForm()
        {
            InitializeComponent();
        }

        private void LoadItems()
        {
            try
            {
                string[] items = File.ReadAllLines("items.txt");

                foreach (string item in items)
                {
                    string[] parts = item.Split(',');
                    if (parts.Length >= 3)
                    {
                        string itemNumber = parts[0];
                        string itemName = parts[1];
                        string itemPrice = parts[2];

                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Items file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PurchaseForm_Load(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double couponPercentage;
            int itemNumber;
            int quantity;

            // Try to parse the input values
            if (!double.TryParse(textBox1.Text, out couponPercentage) ||
                !int.TryParse(textBox2.Text, out itemNumber) ||
                !int.TryParse(textBox3.Text, out quantity))
            {
                MessageBox.Show("Invalid input. Please enter valid numbers.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Read the items from the file
            string[] items = File.ReadAllLines("items.txt");

            if (itemNumber >= 1 && itemNumber <= items.Length)
            {
                // Get the selected item details
                string selectedItem = items[itemNumber - 1];
                string[] itemDetails = selectedItem.Split(',');

                // Extract the item name, description, and price
                string itemName = itemDetails[0];
                string itemDescription = itemDetails[1];

                double itemPrice;
                if (!double.TryParse(itemDetails[2], out itemPrice))
                {
                    MessageBox.Show("Invalid item price in the file. Please check the file contents.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Calculate the total for the current item
                double itemTotal = itemPrice * quantity;

                // Apply the coupon percentage
                double couponAmount = itemTotal * couponPercentage / 100;
                itemTotal -= couponAmount;

                // Add the item total to the overall total
                total += itemTotal;

                // Display the current item details and total in textBox4
                string itemTotalText = $"{itemName}, {itemDescription}, {itemPrice.ToString("0.00")} € ";

                textBox4.Text = itemTotalText;

                // Update the total textbox with the updated overall total
                textBoxTotal.Text = total.ToString("0.00") + " €";

                // Clear the input fields for the next item
                textBox2.Clear();
                textBox3.Clear();
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Invalid item number. Please enter a valid item number.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPurchase_Click(object sender, EventArgs e)
        {
            // Perform the purchase or any other necessary actions here
            // You can access the total and the list of items in the textBox4 for further processing

            // Clear the textBox4 and reset the total
            textBox4.Clear();
            total = 0;

            // Clear other input fields if needed
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Clear();
            textBoxTotal.Clear();
        }
    

    private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Do you want to proceed with the purchase?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string itemName = textBox4.Text;
                string totalAmount = textBoxTotal.Text;

                string purchaseDetails = $"{itemName} - Total: {totalAmount}";

                try
                {
                    using (StreamWriter writer = new StreamWriter("purchase_history.txt", true))
                    {
                        writer.WriteLine(purchaseDetails);
                    }

                    MessageBox.Show("Purchase succeeded!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving the purchase details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (result == DialogResult.No)
            {
                // Perform any necessary actions for canceling the purchase
            }
        }




        private void SavePurchase(string purchaseItem, string total)
        {
            throw new NotImplementedException();
        }

      


        private void ClearForm()
        {
            // Clear the textboxes and reset the form to its initial state
            // Implement your clearing logic here, for example:
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
          
            this.Hide();

        
            LobbyForm lobbyForm = new LobbyForm();
            lobbyForm.Show();
        }

        private void textBoxTotal_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

    
