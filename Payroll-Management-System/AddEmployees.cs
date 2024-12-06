using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Project1
{
    public partial class AddEmployees : Form
    {
        public AddEmployees()
        {
            InitializeComponent();
        }

        // Load event when the form is loaded
        private void Form2_Load(object sender, EventArgs e)
        {
            // Call the method to populate the data grid view with employee data
            BindData();
        }

        // Method to bind data to the DataGridView
        void BindData()
        {
            // SQL command to select all records from Employees table
            SqlCommand cnn = new SqlCommand("Select " +
                "Employees.employee_id as Employee_Id, " +
                "Employees.first_name as First_Name, " +
                "Employees.last_name as Last_Name, " +
                "Employees.gender As Gender, "+
                "Employees.date_of_birth As Date_Of_Birth, "+
                "Employees.address AS Address, "+
                "Employees.contact_number AS Contact_Number, "+
                "Employees.hired_date As Hired_Date, "+
                "Employees.designation AS Designation, "+
                "Designations.basic_salary AS Basic_Salary, "+
                "Designations.allowances AS Allowances, "+
                "Designations.overtime_rate AS Overtime_Rate "+
                "from Employees join Designations ON Employees.designation = Designations.designation", Connection.getConnection());

            SqlDataAdapter da = new SqlDataAdapter(cnn);

            DataTable table = new DataTable();

            // Fill the DataTable with the retrieved data
            da.Fill(table);

            // Set the DataGridView's data source to the DataTable
            dataGridView1.DataSource = table;
        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        // Click event handler for the calculate button
        private void button2_Click(object sender, EventArgs e)
        {

            string selectedText = comboBox1.SelectedItem?.ToString(); 

            decimal basic_salary;
            decimal allowances;
            decimal overtime_rate;

            // Fetch  data from the Designations table
            string selectQuery = "SELECT designation,basic_salary,allowances,overtime_rate FROM Designations WHERE Designations.designation = @selectedText";

            using (SqlCommand cmd = new SqlCommand(selectQuery, Connection.getConnection()))
            {
                cmd.Parameters.AddWithValue("@selectedText", selectedText);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Update labels with values
                        basic_salary = (decimal)reader["basic_salary"];
                        allowances = (decimal)reader["allowances"];
                        overtime_rate = (decimal)reader["overtime_rate"];

                        label10.Text = $"Basic Salary  : {basic_salary} ₤";
                        label11.Text = $"Allowances    : {allowances} ₤";
                        label12.Text = $"Overtime Rate : {overtime_rate} ₤";
                    }
                }
            }
            Connection.closeConnection();
        }

        // Click event handler for the 'Add/Update Employee' button
        private void button1_Click(object sender, EventArgs e)
        {

            // SQL command to insert new employee data into the Employees table
            SqlCommand cnn = new SqlCommand("Insert into Employees(first_name, last_name, gender, date_of_birth, address, contact_number , hired_date, designation) values (@first_name , @last_name , @gender, @date_of_birth, @address, @contact_number, @hired_date, @designation)", Connection.getConnection());

            // Set parameters for the SQL command from the input fields in the form
            cnn.Parameters.AddWithValue("@first_name", textBox1.Text);
            cnn.Parameters.AddWithValue("@last_name", textBox2.Text);

            // Determine gender based on radio button selection
            if (radioButton1.Checked)
            {
                cnn.Parameters.AddWithValue("@gender", "M");
            }
            else if (radioButton2.Checked)
            {
                cnn.Parameters.AddWithValue("@gender", "F");
            }

            // Convert date values to DateTime format
            DateTime selectedDate = dateTimePicker1.Value.Date;
            cnn.Parameters.AddWithValue("@date_of_birth", selectedDate);

            // Set other input fields as parameters
            cnn.Parameters.AddWithValue("@address", textBox3.Text);
            cnn.Parameters.AddWithValue("@contact_number", textBox4.Text);

            // Convert date values to DateTime format
            DateTime selectedDate2 = dateTimePicker2.Value.Date;
            cnn.Parameters.AddWithValue("@hired_date", selectedDate2);

            // Convert string inputs to respective data types
            string selectedText = comboBox1.Text;
            cnn.Parameters.AddWithValue("@designation", selectedText);

            // Execute the SQL command
            cnn.ExecuteNonQuery();

            // Close the database connection
            Connection.closeConnection();

            // Refresh the DataGridView with updated data
            BindData();

            // Clear input fields after adding an employee
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.ResetText();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        // Click event handler for the 'Clear' button
        private void button3_Click(object sender, EventArgs e)
        {
            // Clear all input fields
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.ResetText();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
    }
}
