using System;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class Department : Form
    {
        public Department(string id)
        {
            InitializeComponent();

           if(id!=null)
            {
                FetchDepartmentById(id);
            }
        }

        MySqlConnection con = new MySqlConnection();
        DataConfiguration database = new DataConfiguration();
        ApplicationConfiguration app = new ApplicationConfiguration();
        MySqlCommand com = new MySqlCommand();

        private void FetchDepartmentById(string id)
        {
            con.ConnectionString = app.constr;

            con.Open();

            com.CommandText = "select * from departments where id="+Convert.ToInt32(id);
            com.Connection = con;

            MySqlDataReader data = com.ExecuteReader();
            data.Read();
            textBox1.Text = data.GetString(1);
            textBox2.Text = data.GetString(2);
            label3.Text = id;

            con.Close();
        }

        private void Department_Load(object sender, EventArgs e)
        {
            this.Text = "Manage Departments";

            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.FormBorderStyle = FormBorderStyle.None;

            this.BackColor = Color.White;

            con.ConnectionString = app.constr;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click_2(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string[] cols = { "name", "comments" };
                object[] vals = { textBox1.Text.ToUpper().ToString(), textBox2.Text };

                com.Connection = con;

                if (label3.Text == "" || label3.Text == null || label3.Text == string.Empty)
                {
                    com.CommandText = app.InsertQuery("departments", cols, vals);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Department Created Successfully..", "Library Management System..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    com.CommandText = app.UpdateQuery("departments", cols, vals, "id=" + Convert.ToInt32(label3.Text));
                    com.ExecuteNonQuery();
                    MessageBox.Show("Department Detail's Updated Successfully..", "Library Management System..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Library Management System..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }
        }
    }
}
