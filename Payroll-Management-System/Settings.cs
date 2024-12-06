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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project1
{
    public partial class Settings : Form
    {
        // Constructor initializing the Settings form
        public Settings()
        {
            InitializeComponent();
        }


        // Load event when the Settings form is loaded
        private void Settings_Load(object sender, EventArgs e)
        {
            // Call method to populate the DataGridView with Settings data
            BindData();
        }

        // Method to fetch and bind Settings data to DataGridView
        void BindData()
        {
            SqlCommand cnn = new SqlCommand("Select * from Settings", Connection.getConnection());
            SqlDataAdapter da = new SqlDataAdapter(cnn);

            DataTable table = new DataTable();

            da.Fill(table);
            Connection.closeConnection();
            dataGridView1.DataSource = table;
        }

        // Click event handler for the button to insert Settings data into the database
        private void button1_Click(object sender, EventArgs e)
        {

            // SQL command to insert data into Settings table
            SqlCommand cnn = new SqlCommand("Insert into Settings(date_range_for_cycle, cycle_begin_date, cycle_end_date, no_of_leaves_for_employee) values (@date_range_for_cycle ,@cycle_begin_date ,@cycle_end_date , @no_of_leaves_for_employeee)", Connection.getConnection());

            // Set parameter values from form inputs
            cnn.Parameters.AddWithValue("@date_range_for_cycle", int.Parse(textBox1.Text));
            cnn.Parameters.AddWithValue("@no_of_leaves_for_employeee", int.Parse(textBox2.Text));
            
            DateTime selectedDate = dateTimePicker1.Value.Date;
            cnn.Parameters.AddWithValue("@cycle_begin_date", selectedDate);

            DateTime selectedDate2 = dateTimePicker2.Value.Date;
            cnn.Parameters.AddWithValue("@cycle_end_date", selectedDate2);

            // Execute the SQL command
            cnn.ExecuteNonQuery();

            Connection.closeConnection();

            // Refresh the DataGridView with updated data
            BindData();

            // Clear data from fields
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
