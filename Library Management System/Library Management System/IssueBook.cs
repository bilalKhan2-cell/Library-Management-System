using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class IssueBook : Form
    {
        public IssueBook()
        {
            InitializeComponent();
        }

        MySqlCommand com = new MySqlCommand();
        MySqlConnection con = new MySqlConnection();
        ApplicationConfiguration app = new ApplicationConfiguration();

        private void IssueBook_Load(object sender, EventArgs e)
        {
            this.Text = "Book Issuance Management";
            this.BackColor = Color.White;

            panel1.Hide();
            panel3.Hide();

            con.ConnectionString = app.constr;
            GenerateBooks();

            kryptonComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            kryptonComboBox1.MaxDropDownItems = 5;
        }


        private void GenerateBooks()
        {
            con.Open();

            com.CommandText = "select * from books order by name;";
            com.Connection = con;

            MySqlDataAdapter adapater = new MySqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapater.Fill(dt);

            kryptonComboBox1.DataSource = dt;
            kryptonComboBox1.DisplayMember = "name";
            kryptonComboBox1.ValueMember = "id";

            con.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text == "" || kryptonTextBox1.Text == string.Empty)
            {
                MessageBox.Show("Student Roll No is Required..", "Library Management System..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                con.Open();

                com.CommandText = "select students.id,students.name,students.fname,students.age,students.gender,students.email,students.rollno,students.addres,students.batch,departments.name as department_name from students inner join departments on students.department=departments.id where students.rollno='" + kryptonTextBox1.Text + "';";
                com.Connection = con;

                MySqlDataAdapter adapter = new MySqlDataAdapter(com);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    MySqlDataReader StudentReader = com.ExecuteReader();
                    StudentReader.Read();

                    lblName.Text = StudentReader.GetString(1);
                    lblFatherName.Text = StudentReader.GetString(2);
                    lblAge.Text = StudentReader.GetInt16(3).ToString();
                    lblGender.Text = StudentReader.GetString(4);
                    lblEmailAddress.Text = StudentReader.GetString(5);
                    lblRollNo.Text = StudentReader.GetString(6);
                    lblAddress.Text = StudentReader.GetString(7);
                    lblBatch.Text = StudentReader.GetString(8).ToString();
                    lblDepartment.Text = StudentReader.GetString("department_name");
                    lblStudentID.Text = StudentReader.GetInt32(0).ToString();
                    lblStudentID.Visible = false;

                    panel1.Show();
                    panel3.Show();
                }

                else
                {
                    MessageBox.Show("Record Not Found..", "Library Management System..", MessageBoxButtons.OK, MessageBoxIcon.None);
                }


                con.Close();
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string table = "issuance";
                string[] cols = { "student_id", "book_id", "_from", "_until" };
                object[] values = { lblStudentID.Text, (int)kryptonComboBox1.SelectedValue, kryptonDateTimePicker1.Value.ToString("yyyy-MM-dd"), kryptonDateTimePicker2.Value.ToString("yyyy-MM-dd") };

                com.CommandText = "select count(student_id) from issuance where student_id='" + Convert.ToInt32(lblStudentID.Text) + "' and return_date is null;";
                com.Connection = con;

                if (com.ExecuteScalar().ToString() == "3")
                {
                    MessageBox.Show("Maximum Numbers of Book Issued To The Student..", "Library Management System..", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                else
                {
                    com.CommandText = app.InsertQuery(table, cols, values);
                    com.Connection = con;

                    com.ExecuteNonQuery();

                    MessageBox.Show("Book Issued Successfully..", "Library Management System..", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    panel1.Hide();
                    panel3.Hide();
                }

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Library Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void lblStudentID_Click(object sender, EventArgs e)
        {

        }
    }
}
