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
    public partial class Home : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=localhost;Initial Catalog=ComputerVision;Integrated Security=True");

        public Home()
        {
            InitializeComponent();

            this.Size = new System.Drawing.Size(818, 497);
        }

        private void Home_Load(object sender, EventArgs e)
        {
            if(LoggedInUser.admin == true)
            {
                Button button = new Button();

                button.Location = new System.Drawing.Point(6, 30);
                button.Text = "Admin";
                button.Size = new System.Drawing.Size(75, 23);

                button.Click += new EventHandler(adminButton_Click);

                this.Controls.Add(button);
            }


            SqlCommand cmd = new SqlCommand($"SELECT * FROM [dbo].[Product]", connection);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            int x = 89;
            int y = 45;

            bool newRow = true;

            bool start = true;
            
            // loop inside the list of products that is comming from the database {List of Objects}
            while (dr.Read())
            {
                // Make product Object to store data temporarty
                Product product = new Product();
                GroupBox groupBox = new GroupBox();
                // insert data of the product from database and insert it in the created object
                product.Id = int.Parse(dr["Id"].ToString());
                product.productName = dr["name"].ToString();
                product.productColor = dr["color"].ToString();
                product.productDescription = dr["description"].ToString();
                product.productPrice = int.Parse(dr["price"].ToString());
                product.productQuantity = int.Parse(dr["quantity"].ToString());

                groupBox.Size = new System.Drawing.Size(262, 219);
                if (newRow == true && start != true)
                {
                    x -= 340;
                    y += 257;
                }
                else if (newRow == false)
                {
                    x += 340;
                }

                groupBox.Location = new System.Drawing.Point(x, y);

                Label[] labels = new Label[5];

                labels[0] = new Label { Text = product.productName, Location = new System.Drawing.Point(6, 30) };
                labels[1] = new Label { Text = product.productColor, Location = new System.Drawing.Point(6, 59) };
                labels[2] = new Label { Text = product.productDescription, Location = new System.Drawing.Point(6, 88) };
                labels[3] = new Label { Text = product.productPrice.ToString(), Location = new System.Drawing.Point(6, 117) };
                labels[4] = new Label { Text = product.productQuantity.ToString(), Location = new System.Drawing.Point(6, 146) };
                Button buyButton = new Button { Text = "Buy", AccessibleName = product.Id.ToString(), Location = Location = new System.Drawing.Point(97, 174) };
                buyButton.Click += new EventHandler(buyButton_Click);

                for(int i=0; i< labels.Length; i++)
                {
                    groupBox.Controls.Add(labels[i]);
                }
                groupBox.Controls.Add(buyButton);

                this.Controls.Add(groupBox);

                newRow = !newRow;
                start = false;
            }

            connection.Close();
        }

        private void buyButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string productID = button.AccessibleName;

            MessageBox.Show($"Product Bought with ID of {productID}");
        }

        private void adminButton_Click (object sender, EventArgs e)
        {
            this.Hide();
            Admin admin = new Admin();
            admin.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoggedInUser.Id = 0;
            LoggedInUser.admin = false;
            LoggedInUser.username = null;

            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}


public class Product
{
    public int Id;
    public string productName;
    public string productColor;
    public string productDescription;
    public int productPrice;
    public int productQuantity;
}