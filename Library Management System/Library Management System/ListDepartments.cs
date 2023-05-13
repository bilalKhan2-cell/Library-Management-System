using System;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace Library_Management_System
{
    public partial class ListDepartments : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand com = new MySqlCommand();
        ApplicationConfiguration app = new ApplicationConfiguration();

        public ListDepartments()
        {
            InitializeComponent();
        }

        private void FetchDepartments()
        {
            con.Open();

            com.CommandText = "select id as ID, name as Title,comments as Comments from departments;";
            com.Connection = con;
            com.ExecuteNonQuery();

            MySqlDataAdapter adapter = new MySqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            kryptonDataGridView1.DataSource = dt;

            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            kryptonDataGridView1.Columns[0].Visible = false;

            con.Close();
        }

        private void ListDepartments_Load(object sender, EventArgs e)
        {
            this.Text = "Departments";

            this.BackColor = Color.White;

            this.FormBorderStyle = FormBorderStyle.None;

            con.ConnectionString = app.constr;

            FetchDepartments();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            Department dept = new Department(null) { TopMost = true, TopLevel = false};
            dept.Dock = DockStyle.Fill;
            dept.FormBorderStyle = FormBorderStyle.None;

            panel1.Controls.Clear();
            panel1.BackColor = Color.White;
            panel1.Controls.Add(dept);

            dept.Show();
        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kryptonDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = kryptonDataGridView1.Rows[e.RowIndex];

            Department dept = new Department(selectedRow.Cells["id"].Value.ToString()) { TopMost = true, TopLevel = false };

            dept.FormBorderStyle = FormBorderStyle.None;
            dept.Dock = DockStyle.Fill;

            panel1.Controls.Clear();
            panel1.Controls.Add(dept);

            dept.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
