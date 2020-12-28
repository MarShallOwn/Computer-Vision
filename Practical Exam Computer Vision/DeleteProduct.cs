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
    public partial class DeleteProduct : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=localhost;Initial Catalog=ComputerVision;Integrated Security=True");
        private string productId;

        public DeleteProduct(string Id)
        {
            productId = Id;
            InitializeComponent();
        }


        // Cancel Delete And return to admin form
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin admin = new Admin();
            admin.Show();
        }

        // Delete Product from database
        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand($"DELETE FROM [dbo].[Product] WHERE Id='{productId}'", connection);
            connection.Open();

            try
            {
                SqlDataReader dr = cmd.ExecuteReader();

                connection.Close();
                MessageBox.Show("Product Deleted!");
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
    }
}
