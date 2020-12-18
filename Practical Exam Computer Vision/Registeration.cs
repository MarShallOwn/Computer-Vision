using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practical_Exam_Computer_Vision
{
    public partial class Registeration : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=localhost;Initial Catalog=ComputerVision;Integrated Security=True");
        public Registeration()
        {
            InitializeComponent();
        }

        private void Registeration_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != string.Empty || textBox2.Text != string.Empty || textBox3.Text != string.Empty)
            if (textBox2.Text == textBox3.Text) {
                string username = textBox1.Text;
                string password = BCrypt.Net.BCrypt.HashPassword(textBox2.Text, 13);
                    SqlCommand cmd = new SqlCommand($"INSERT INTO [dbo].[User] (username, password) VALUES('{username}','{password}')", connection);
                    connection.Open();

                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();

                        connection.Close();
                        MessageBox.Show("Registered!");
                    }
                    catch (SqlException ex)
                    {
                        // the exception alone won't tell you why it failed...
                        if (ex.Number == 2627) // <-- but this will
                        {
                            connection.Close();
                            MessageBox.Show("Username Already exist please try another ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
            }
            else
            {
                MessageBox.Show("Please enter both password same ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();

            login.Show();
        }
    }
}