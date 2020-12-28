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
    public partial class AddProduct : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=localhost;Initial Catalog=ComputerVision;Integrated Security=True");
        public AddProduct()
        {
            InitializeComponent();
        }

        // Add Product to Database
        private void button1_Click(object sender, EventArgs e)
        {
            string productName = textBox1.Text;
            string productColor = textBox2.Text;
            string productDescription = textBox3.Text;
            int productPrice = int.Parse(textBox4.Text);
            int productQuantity = int.Parse(textBox5.Text);

            if (productName != string.Empty || productColor != string.Empty || productDescription != string.Empty || productPrice.ToString() != string.Empty || productQuantity.ToString() != string.Empty)
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO [dbo].[Product] (name, color, description, price, quantity) VALUES('{productName}','{productColor}', '{productDescription}', '{productPrice}', '{productQuantity}')", connection);
                connection.Open();

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();

                    connection.Close();
                    MessageBox.Show("Product Added!");
                    this.Hide();
                    Admin admin = new Admin();
                    admin.Show();
                }
                catch (SqlException ex)
                {
                    connection.Close();
                    throw ex;
                }
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // allow only numbers in the textbox for the price
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // allow only numbers in the textbox for the Quantity
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
