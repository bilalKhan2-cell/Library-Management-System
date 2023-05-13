using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class ListStudents : Form
    {
        public ListStudents()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection();
        MySqlCommand com = new MySqlCommand();
        ApplicationConfiguration app = new ApplicationConfiguration();

        private void ListStudents_Load(object sender, EventArgs e)
        {
            this.Text = "Library Management System - Students..";

            this.FormBorderStyle = FormBorderStyle.None;

            this.BackColor = Color.White;


            con.ConnectionString = app.constr;
            GetDepartments();
        }

        private void GetDepartments()
        {
            con.Open();

            com.CommandText = "select * from departments;";
            com.Connection = con;

            MySqlDataAdapter adapter = new MySqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            kryptonComboBox1.DataSource = dt;
            kryptonComboBox1.DisplayMember = "name";
            kryptonComboBox1.ValueMember = "id";

            int year = DateTime.Now.Year;
            for (int a = 1970; a <= year; a++)
            {
                kryptonComboBox2.Items.Add(a);
            }

            con.Close();
        }

        private string GetStudents(string department,string batch)
        {
            return "select students.id,students.rollno as Enrollment_No,students.name as Name,students.fname as Father_Name,students.email as Email_Address,students.contactinfo as ContactNo,departments.name as Department from students inner join departments on students.department=departments.id where students.department="+(department)+" and students.batch="+(batch)+";";
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Students std = new Students(null) { TopMost=true,TopLevel=false };

            std.FormBorderStyle = FormBorderStyle.None;
            std.Dock = DockStyle.Fill;

            panel1.Controls.Clear();
            panel1.Controls.Add(std);

            std.Show();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string str = (GetStudents(kryptonComboBox1.SelectedValue.ToString(), (kryptonComboBox2.Text)));

                con.Open();
                //MessageBox.Show(kryptonComboBox1.SelectedValue.ToString());
                //MessageBox.Show(kryptonComboBox2.Text);
         
                com.Connection = con;
                //com.CommandText = GetStudents((int)kryptonComboBox1.SelectedValue,Convert.ToInt32(kryptonComboBox2.SelectedText));
                com.CommandText = str;

                MySqlDataAdapter adapter = new MySqlDataAdapter(com);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                kryptonDataGridView1.DataSource = dt;
                kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                kryptonDataGridView1.Columns[0].Visible = false;

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Library Management System..",MessageBoxButtons.OK,MessageBoxIcon.Error);
                con.Close();
            }
        }
    }
}
