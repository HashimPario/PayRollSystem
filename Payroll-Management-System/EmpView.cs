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
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace Project1
{
    public partial class EmpView : Form
    {
        // Constructor for the form
        public EmpView()
        {
            InitializeComponent();
        }

        // Load event when the form is loaded
        private void EmpView_Load(object sender, EventArgs e)
        {
            // Retrieve all employee data and bind to DataGridView on form load
            SqlCommand cnn = new SqlCommand("Select * from Employees", Connection.getConnection());
            SqlDataAdapter da = new SqlDataAdapter(cnn);

            DataTable table = new DataTable();

            da.Fill(table);
            Connection.closeConnection();
            dataGridView1.DataSource = table;
        }


        // Click event handler for the search button
        private void button1_Click(object sender, EventArgs e)
        {
            // Search for employees based on the entered text in textBox1
            string searchKeyword = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                // Search for employees whose first name contains the entered keyword
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employees WHERE first_name LIKE @searchTerm", Connection.getConnection());
                cmd.Parameters.AddWithValue("@searchTerm", "%" + searchKeyword + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();

                da.Fill(table);
                Connection.closeConnection();
                dataGridView1.DataSource = table;
            }
            else
            {
                // If search keyword is empty, display all employees
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", Connection.getConnection());
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                da.Fill(table);
                Connection.closeConnection();
                dataGridView1.DataSource = table;
            }
        }

        // Method to load all employees into the DataGridView
        private void LoadEmployees()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", Connection.getConnection());
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable table = new DataTable();
            da.Fill(table);
            Connection.closeConnection();
            dataGridView1.DataSource = table;
        }

        // Click event handler for the remove button
        private void button2_Click(object sender, EventArgs e)
        {
            // Remove selected employee from the database and refresh the DataGridView
            RemoveEmployee();
            LoadEmployees();
        }


        // Method to remove an employee from the database
        private void RemoveEmployee()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected employee's ID and delete the record from the database
                int selectedEmployeeId = (int)dataGridView1.SelectedRows[0].Cells["employee_id"].Value;

                // Delete data from salary table and Employees table
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Salary WHERE employee_id = @selectedEmployeeId ; DELETE FROM Employees WHERE employee_id = @selectedEmployeeId", Connection.getConnection()))
                {
                    cmd.Parameters.AddWithValue("@selectedEmployeeId", selectedEmployeeId);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    Connection.closeConnection();

                    // Show success or failure message and refresh DataGridView
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Employee removed successfully.");
                        LoadEmployees();
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove the employee.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to remove.");
            }
        }


        // Click event handler for the update button
        private void button3_Click(object sender, EventArgs e)
        {
            // Open the employee update form with the selected employee's ID and refresh the DataGridView
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int selectedEmployeeId = (int)dataGridView1.SelectedRows[0].Cells["employee_id"].Value;

                EmpUpdate updateForm = new EmpUpdate(selectedEmployeeId);
                updateForm.Show();

                LoadEmployees();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // Click event handler for the salary details button
        private void button4_Click(object sender, EventArgs e)
        {
            // Open the salary details form with details of the selected employee
            if (dataGridView1.SelectedRows.Count == 1)
            {

                int selectedEmployeeId = (int)dataGridView1.SelectedRows[0].Cells["employee_id"].Value;
                string selectedDesignation = (string)dataGridView1.SelectedRows[0].Cells["designation"].Value;


                string selectedText = selectedDesignation;

                decimal selectedBasicSalary = 0;
                decimal selectedAllowances = 0;
                decimal selectedEmployeerate = 0;

                // Fetch  data from the Designations table
                string selectQuery = "SELECT designation,basic_salary,allowances,overtime_rate FROM Designations WHERE Designations.designation = @selectedText";

                using (SqlCommand cmd = new SqlCommand(selectQuery, Connection.getConnection()))
                {
                    cmd.Parameters.AddWithValue("@selectedText", selectedText);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Update variables with values
                            selectedBasicSalary = (decimal)reader["basic_salary"];
                            selectedAllowances = (decimal)reader["allowances"];
                            selectedEmployeerate = (decimal)reader["overtime_rate"];
                        }
                    }
                }
                Connection.closeConnection();

                Salary salary = new Salary(selectedEmployeeId, selectedBasicSalary, selectedAllowances, selectedEmployeerate);
                salary.Show();
            }
        }
    }
}