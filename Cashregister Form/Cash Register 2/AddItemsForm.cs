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

    public partial class AddItemsForm : Form
    {
        public AddItemsForm()
        {
            InitializeComponent();
        }

        private void AddItemsForm_Load(object sender, EventArgs e)
        {

        }
        private int itemNumber = 1;
        private void button1_Click(object sender, EventArgs e)
        {



            string name = textBox1.Text;
            string description = textBox2.Text;
            string price = textBox3.Text;

            // Validate input
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(price))
            {
                MessageBox.Show("Please enter all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newItem = $"{name},{description},{price}";

            try
            {
                using (StreamWriter writer = new StreamWriter("items.txt", true))
                {
                    writer.WriteLine(newItem);
                }

                MessageBox.Show("Item added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

             
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
