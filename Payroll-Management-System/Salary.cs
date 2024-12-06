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
using System.Globalization;

namespace Project1
{
    public partial class Salary : Form
    {
        // Variables to store data related to salary calculations
        private int employeeId;
        private decimal overtimeRate;
        private decimal basicSalary;
        private decimal allowances;
        private decimal noPayValue;
        private decimal basePay;
        private decimal grossPay;

        // Constructor to initialize Salary form with relevant employee and salary details
        public Salary(int selectedEmployeeId, decimal selectedBasicSalary, decimal selectedAllowances, decimal selectedEmployeerate)
        {
            InitializeComponent();
            this.employeeId = selectedEmployeeId;
            this.basicSalary = selectedBasicSalary;
            this.allowances = selectedAllowances;
            this.overtimeRate = selectedEmployeerate;

            LoadSalarydata();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Salary_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadSalarydata()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT first_name, last_name FROM Employees WHERE employee_id = @employeeId", Connection.getConnection()))
            {
                cmd.Parameters.AddWithValue("@employeeId", employeeId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string first_name = reader["first_name"].ToString();
                        string last_name = reader["last_name"].ToString();
                        label10.Text = $"Name                          :   {first_name} {last_name}";
                    }
                }
            }
            Connection.closeConnection();
        }


        // Click event handler for the calculate button
        private void button1_Click(object sender, EventArgs e)
            
        {
            // Calculations based on selected date range, leaves, and hours
            DateTime BeginDate;
            DateTime EndDate;

            // Fetch the latest cycle begin and end dates from the Settings table
            string selectQuery = "SELECT TOP 1 cycle_begin_date, cycle_end_date FROM Settings ORDER BY setting_id DESC";

            using (SqlCommand cmd = new SqlCommand(selectQuery, Connection.getConnection()))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        BeginDate = (DateTime)reader["cycle_begin_date"];
                        EndDate = (DateTime)reader["cycle_end_date"];

                        // Check if selected dates match the system's cycle dates
                        if (BeginDate.Date == dateTimePicker1.Value.Date && EndDate.Date == dateTimePicker2.Value.Date)
                        {
                            // Perform salary calculations based on entered values
                            TimeSpan dateRange = dateTimePicker2.Value - dateTimePicker1.Value;
                            int salaryCycleDateRange = dateRange.Days;

                            int no_of_leaves = string.IsNullOrWhiteSpace(textBox1.Text) ? 0 : int.Parse(textBox1.Text);
                            int no_of_hours = string.IsNullOrWhiteSpace(textBox2.Text) ? 0 : int.Parse(textBox2.Text);

                            decimal no_pay_value = Math.Round(((basicSalary / salaryCycleDateRange) * no_of_leaves), 2);
                            decimal base_pay_value = Math.Round((basicSalary + allowances + (overtimeRate * no_of_hours)), 2);
                            decimal gross_pay = Math.Round((base_pay_value - (no_pay_value + base_pay_value * (decimal)0.01)), 2);

                            // Update labels with calculated values
                            label7.Text = $"No Pay Value:  PKR {no_pay_value}";
                            label8.Text = $"Base Pay Value:  PKR {base_pay_value}";
                            label9.Text = $"Gross Pay:  PKR {gross_pay}";
                            noPayValue = no_pay_value;
                            basePay = base_pay_value;
                            grossPay = gross_pay;

                        }
                        else
                        {
                            MessageBox.Show("Begin and End Dates you picked do not match the system data.Try again");
                        }


                    }
                    else
                    {
                        MessageBox.Show("No rows found in the Settings table.");
                    }
                }
            }

            Connection.closeConnection();



        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        // Click event handler for the save button to insert salary data into the database
        private void button2_Click(object sender, EventArgs e)
        {
            // Insert salary data into the database
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Salary (employee_id, month, no_pay_value, base_pay_value, gross_pay) " + "VALUES (@EmployeeId, @Month, @NoPayValue, @BasePayValue, @GrossPay)", Connection.getConnection()))
            {

                cmd.Parameters.AddWithValue("@EmployeeId", employeeId); 
                cmd.Parameters.AddWithValue("@Month", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@NoPayValue", noPayValue);
                cmd.Parameters.AddWithValue("@BasePayValue",basePay);
                cmd.Parameters.AddWithValue("@GrossPay", grossPay);

                
                cmd.ExecuteNonQuery();
                Connection.closeConnection();
            }
            MessageBox.Show("Salary data added to the database.");
        }

        // Click event handler for the view button to display salary data in a DataGridView
        private void button3_Click(object sender, EventArgs e)

        {
            // Retrieve and display salary data for the employee from the database
            SqlCommand cmd = new SqlCommand("SELECT * FROM Salary where employee_id = @EmployeeId", Connection.getConnection());
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable table = new DataTable();
            da.Fill(table);
            Connection.closeConnection();
            dataGridView1.DataSource = table;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}