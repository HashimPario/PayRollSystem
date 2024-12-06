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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private bool AuthenticateUser(string username, string password)
        {

            string query = "SELECT COUNT(*) FROM Access WHERE user_name = @username AND password = @password";

            using (SqlCommand command = new SqlCommand(query, Connection.getConnection()))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                int count = Convert.ToInt32(command.ExecuteScalar());
                Connection.closeConnection();

                return count > 0;
            }
        }



    
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (AuthenticateUser(username, password))
            {

                this.Hide();
               
                Dashboard dashboardForm = new Dashboard();
                dashboardForm.Show();

               

            }
            else
            {
              
                MessageBox.Show("Invalid username or password. Please try again.");
            }

        }
    }
}
