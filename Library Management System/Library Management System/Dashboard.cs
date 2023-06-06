using System;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Library_Management_System
{
    public partial class Dashboard : Form
    {
        public Dashboard(int flag)
        {
            InitializeComponent();

            if (flag == 2)
            {
                button2.Visible = false;
            }
        }

        MySqlConnection con = new MySqlConnection();
        MySqlCommand com = new MySqlCommand();
        ApplicationConfiguration app = new ApplicationConfiguration();

        private void Dashboard_Load(object sender, EventArgs e)
        {
            this.Text = "Welcome";

            this.BackColor = Color.White;

            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;

            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;

            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;

            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;

            button6.FlatStyle = FlatStyle.Flat;
            button6.FlatAppearance.BorderSize = 0;

            con.ConnectionString = app.constr;

            con.Open();
            label2.Text = "Total Registered Students: "+FetchTotalStudents().ToString();
            label3.Text = "Total Registered Books: " +FetchTotalBooks().ToString();
            con.Close();
        }

        private int FetchTotalStudents()
        {

            com.Connection = con;
            com.CommandText = "select count(id) from students;";

            return Convert.ToInt32(com.ExecuteScalar().ToString());
        }

        private int FetchTotalBooks()
        {
            com.Connection = con;
            com.CommandText = "select count(id) from books";

            return Convert.ToInt32(com.ExecuteScalar().ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListBooks books = new ListBooks() { TopMost = true, TopLevel = false };
            books.Dock = DockStyle.Fill;
            books.FormBorderStyle = FormBorderStyle.None;
            panel2.Controls.Clear();
            panel2.Controls.Add(books);
            books.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListUsers frm1 = new ListUsers() { TopMost = true, TopLevel = false };
            frm1.Dock = DockStyle.Fill;
            frm1.FormBorderStyle = FormBorderStyle.None;
            panel2.Controls.Clear();
            panel2.Controls.Add(frm1);

            frm1.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListDepartments dept = new ListDepartments() { TopMost = true,TopLevel = false };

            dept.FormBorderStyle = FormBorderStyle.None;
            dept.Dock = DockStyle.Fill;

            panel2.Controls.Clear();
            panel2.Controls.Add(dept);

            dept.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IssueBook issue = new IssueBook() { TopMost=true,TopLevel = false };

            issue.FormBorderStyle = FormBorderStyle.None;
            issue.Dock = DockStyle.Fill;

            panel2.Controls.Clear();
            panel2.Controls.Add(issue);

            issue.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReturnBook retBook = new ReturnBook() { TopMost=true,TopLevel = false };

            retBook.FormBorderStyle = FormBorderStyle.None;
            retBook.Dock = DockStyle.Fill;

            panel2.Controls.Clear();
            panel2.Controls.Add(retBook);

            retBook.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ListStudents students = new ListStudents() { TopMost =true, TopLevel = false };

            students.Dock = DockStyle.Fill;
            students.FormBorderStyle = FormBorderStyle.None;

            panel2.Controls.Clear();
            panel2.Controls.Add(students);

            students.Show();
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Login log = new Login();

            log.Show();
        }
    }
}
