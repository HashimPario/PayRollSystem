using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project1
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        //Method to open the 'Add Employee' window after clicking the 'Add/Update Employee' button.
        private void button1_Click(object sender, EventArgs e)
        {
            AddEmployees formEmployee = new AddEmployees();

            formEmployee.Show();
        }

        //Method to open the 'EmpView' window after clicking the 'vView Employee' button.
        private void button2_Click(object sender, EventArgs e)
        {
            EmpView Empview = new EmpView();
            Empview.Show();
        }

        //Method to open the 'Settings' window after clicking the 'settings' button.
        private void button3_Click(object sender, EventArgs e)
        {
            Settings SettingForm = new Settings();
            SettingForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Method for log out
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
