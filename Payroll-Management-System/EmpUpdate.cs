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
namespace Project1
{
    public partial class EmpUpdate : Form
    {
        // Store the selected employee ID
        private int employeeId;

        // Constructor that initializes the form and loads employee data based on the selected ID
        public EmpUpdate(int selectedEmployeeId)
        {
            InitializeComponent();
            this.employeeId = selectedEmployeeId;

            // Load data of the selected employee
            LoadEmployeeData();
        }

        // Load event when the form is loaded
        private void EmpUpdate_Load(object sender, EventArgs e)
        {
        
        }

        // Method to load data of the selected employee into the form fields
        private void LoadEmployeeData()
        {
            
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employees WHERE employee_id = @employeeId", Connection.getConnection()))
            {
                cmd.Parameters.AddWithValue("@employeeId", employeeId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Connection.closeConnection();
                    if (reader.Read())
                    {
                        // Populate form fields with employee data
                        textBox1.Text = reader["first_name"].ToString();
                        textBox2.Text = reader["last_name"].ToString();

                        string gender = reader["gender"].ToString();
                        if (gender == "M")
                        {
                            radioButton1.Checked = true;
                        }
                        else if (gender == "F")
                        {
                            radioButton2.Checked = true;
                        }

                        dateTimePicker1.Value = (DateTime)reader["date_of_birth"];
                        textBox3.Text = reader["address"].ToString();
                        textBox4.Text = reader["contact_number"].ToString();
                        dateTimePicker2.Value = (DateTime)reader["hired_date"];
                        comboBox1.Text = reader["designation"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Employee not found.");
                        this.Close();
                    }

                }
            }
        }

        // Method to update employee data based on changes made in the form
        private void UpdateEmployee()
        {
            // Update employee data in the database
            using (SqlCommand cmd = new SqlCommand("UPDATE Employees SET " +
                "first_name = @first_name, " +
                "last_name = @last_name, " +
                "gender = @gender, " +
                "date_of_birth = @date_of_birth, " +
                "address = @address, " +
                "contact_number = @contact_number, " +
                "hired_date = @hired_date, " +
                "designation = @designation " +
                "WHERE employee_id = @employee_id", Connection.getConnection()))
            {
                // Set parameters for the update command
                cmd.Parameters.AddWithValue("@employee_id", employeeId);
                cmd.Parameters.AddWithValue("@first_name", textBox1.Text);
                cmd.Parameters.AddWithValue("@last_name", textBox2.Text);

                
                cmd.Parameters.AddWithValue("@gender", radioButton1.Checked ? "M" : "F");

                cmd.Parameters.AddWithValue("@date_of_birth", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@address", textBox3.Text);
                cmd.Parameters.AddWithValue("@contact_number", textBox4.Text);
                cmd.Parameters.AddWithValue("@hired_date", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@designation", comboBox1.Text);

                int rowsAffected = cmd.ExecuteNonQuery();
                Connection.closeConnection();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Employee updated successfully.");
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Failed to update employee.");
                }
                
            }
            
        }
        // Click event handler for the update button
        private void button2_Click(object sender, EventArgs e)
        {
            // Call the method to update employee data
            UpdateEmployee();
        }

        // Click event handler for the clear button to reset form fields
        private void button3_Click(object sender, EventArgs e)
        {
            // Clear all form fields
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
