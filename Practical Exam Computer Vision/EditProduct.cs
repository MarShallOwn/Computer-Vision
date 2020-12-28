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
    public partial class EditProduct : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=localhost;Initial Catalog=ComputerVision;Integrated Security=True");
        private string productId;
        public EditProduct(string Id)
        {
            productId = Id;
            InitializeComponent();
        }

        private void EditProduct_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand($"SELECT name, color, description, price, quantity FROM [dbo].[Product] WHERE Id='{productId}'", connection);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            // Make product Object to store data temporary
            Product product = new Product();

            // loop inside the list of products that is comming from the database {List of Objects}
            while (dr.Read())
            {
                // insert data of the product from database to the edit fields
                product.productName = dr["name"].ToString();
                product.productColor = dr["color"].ToString();
                product.productDescription = dr["description"].ToString();
                product.productPrice = int.Parse(dr["price"].ToString());
                product.productQuantity = int.Parse(dr["quantity"].ToString());
            }

            textBox1.Text = product.productName;
            textBox2.Text = product.productColor;
            textBox3.Text = product.productDescription;
            textBox4.Text = product.productPrice.ToString();
            textBox5.Text = product.productQuantity.ToString();

            connection.Close();
        }

        // Edit Product and send updated data to the backend
        private void button1_Click(object sender, EventArgs e)
        {
            string productName = textBox1.Text;
            string productColor = textBox2.Text;
            string productDescription = textBox3.Text;
            int productPrice = int.Parse(textBox4.Text);
            int productQuantity = int.Parse(textBox5.Text);

            if (productName != string.Empty || productColor != string.Empty || productDescription != string.Empty || productPrice.ToString() != string.Empty || productQuantity.ToString() != string.Empty)
            {
                SqlCommand cmd = new SqlCommand($"UPDATE [dbo].[Product] SET name='{productName}', color='{productColor}', description='{productDescription}', price='{productPrice}', quantity='{productQuantity}' WHERE Id='{productId}'", connection);
                connection.Open();

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();

                    connection.Close();
                    MessageBox.Show("Product Updated!");
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

        // Cancel Edit And return to admin form
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin admin = new Admin();
            admin.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
