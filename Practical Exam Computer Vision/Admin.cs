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
    public partial class Admin : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=localhost;Initial Catalog=ComputerVision;Integrated Security=True");
        public Admin()
        {
            InitializeComponent();

            this.Size = new System.Drawing.Size(818, 497);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddProduct addProduct = new AddProduct();
            addProduct.Show();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand($"SELECT * FROM [dbo].[Product]", connection);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            int x = 12;
            int y = 89;

            bool start = true;

            // loop inside the list of products that is comming from the database {List of Objects}
            while (dr.Read())
            {
                // Make product Object
                Product product = new Product();
                GroupBox groupBox = new GroupBox();
                // insert data of the product from database and insert it in the created object
                product.Id = int.Parse(dr["Id"].ToString());
                product.productName = dr["name"].ToString();
                product.productColor = dr["color"].ToString();
                product.productPrice = int.Parse(dr["price"].ToString());
                product.productQuantity = int.Parse(dr["quantity"].ToString());

                groupBox.Size = new System.Drawing.Size(776, 55);

                if (start != true)
                {
                    y += 61;
                }

                groupBox.Location = new System.Drawing.Point(x, y);

                Label[] labels = new Label[4];

                labels[0] = new Label { Text = product.productName, Location = new System.Drawing.Point(26, 22), Size = new System.Drawing.Size(168, 17), TextAlign = ContentAlignment.MiddleCenter, AutoSize = false };
                labels[1] = new Label { Text = product.productColor, Location = new System.Drawing.Point(236, 22), Size = new System.Drawing.Size(97, 17), TextAlign = ContentAlignment.MiddleCenter, AutoSize = false };
                labels[2] = new Label { Text = product.productPrice.ToString(), Location = new System.Drawing.Point(384, 22), Size = new System.Drawing.Size(58, 17), TextAlign = ContentAlignment.MiddleCenter, AutoSize = false };
                labels[3] = new Label { Text = product.productQuantity.ToString(), Location = new System.Drawing.Point(481, 22), Size = new System.Drawing.Size(54, 17), TextAlign = ContentAlignment.MiddleCenter, AutoSize = false };
                Button editButton = new Button { Text = "Edit", AccessibleName = product.Id.ToString(), Location = Location = new System.Drawing.Point(583, 19), Size = new System.Drawing.Size(75, 23) };
                Button deleteButton = new Button { Text = "Delete", AccessibleName = product.Id.ToString(), Location = Location = new System.Drawing.Point(683, 19), Size = new System.Drawing.Size(75, 23) };
                editButton.Click += new EventHandler(editButton_Click);
                deleteButton.Click += new EventHandler(deleteButton_Click);

                for (int i = 0; i < labels.Length; i++)
                {
                    groupBox.Controls.Add(labels[i]);
                }
                groupBox.Controls.Add(editButton);
                groupBox.Controls.Add(deleteButton);

                this.Controls.Add(groupBox);

                start = false;
            }

            connection.Close();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string productID = button.AccessibleName;

            this.Hide();
            EditProduct editProduct = new EditProduct(productID);
            editProduct.Show();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string productID = button.AccessibleName;

            this.Hide();

            DeleteProduct deleteProduct = new DeleteProduct(productID);
            deleteProduct.Show();
        }

        // Return to home
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        private void button2_Click_1(object sender, EventArgs e)
        {

        }

    }
}
