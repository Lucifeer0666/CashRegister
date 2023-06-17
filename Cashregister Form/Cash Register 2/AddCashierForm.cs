using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cash_Register_2
{
    public partial class AddCashierForm : Form
    {
        private const string CashierFilePath = "cashiers.txt";

        public AddCashierForm()
        {
            InitializeComponent();
        }

        private void btnAddCashier_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            string filePath = "credentials.txt";
            string cashierEntry = $"{textBox1.Text.Trim()},{textBox2.Text.Trim()}";

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(cashierEntry);
                }

                MessageBox.Show("Cashier added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the cashier: " + ex.Message);
            }


            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a username and password.");
                return;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(CashierFilePath, true))
                {
                    writer.WriteLine($"{username},{password}");
                }

                MessageBox.Show("Cashier added successfully.");
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            // Validate the input
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            string credentialsFilePath = "credentials.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(credentialsFilePath, true))
                {
                    writer.WriteLine($"{username},{password}");
                }

                MessageBox.Show("Cashier added successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred while adding cashier: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void AddCashierForm_Load(object sender, EventArgs e)
        {

        }
    }

}