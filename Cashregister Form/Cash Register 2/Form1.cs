using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Cash_Register_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "admin";
            textBox2.Text = "admin";
        }

        private bool CheckCredentials(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                return true;
            }

            string filePath = "credentials.txt";

            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        string[] credentials = line.Split(',');
                        if (credentials.Length == 2 && credentials[0] == username && credentials[1] == password)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while checking credentials: " + ex.Message);
            }

            return false;
        }



        private void btnConnect_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (CheckCredentials(username, password))
            {
                LobbyForm lobbyForm = new LobbyForm();
                lobbyForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (CheckCredentials(username, password))
            {
                LobbyForm lobbyForm = new LobbyForm();
                lobbyForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}