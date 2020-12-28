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

    public partial class Login : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=localhost;Initial Catalog=ComputerVision;Integrated Security=True");
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;


            if (username != string.Empty || password != string.Empty)
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM [dbo].[User] WHERE username='{username}'", connection);
                connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    User user = new User();

                    while (dr.Read())
                    {
                        user.Id = int.Parse(dr["Id"].ToString());
                        user.username = dr["username"].ToString();
                        user.password = dr["password"].ToString();
                        user.admin = Convert.ToBoolean(dr["admin"]);
                    }
                    if(user.username == null || user.password == null)
                    {
                        MessageBox.Show("User Not found");
                    }
                    else
                    {
                        if (BCrypt.Net.BCrypt.Verify(password, user.password) == true)
                        {
                        LoggedInUser.Id = user.Id;
                        LoggedInUser.username = user.username;
                        LoggedInUser.admin = user.admin;
                            connection.Close();
                            MessageBox.Show("Logged In!");
                        this.Hide();
                        Home home = new Home();
                        home.Show();
                        }
                    }
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registeration registeration = new Registeration();
            registeration.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    public class User
    {
        public int Id;
        public string username;
        public bool admin;
        public string password;
    }
}
