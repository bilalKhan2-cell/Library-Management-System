using System;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace Library_Management_System
{
    public partial class ListUsers : Form
    {
        public ListUsers()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection();
        MySqlCommand com = new MySqlCommand();
        ApplicationConfiguration app = new ApplicationConfiguration();

        private void ListUsers_Load(object sender, EventArgs e)
        {
            this.Text = "Users";
            this.BackColor = Color.White;

            this.FormBorderStyle = FormBorderStyle.None;

            con.ConnectionString = app.constr;

            con.Open();
            FetchUsers();
            con.Close();
        }

        private void FetchUsers()
        {
            com.CommandText = "select id,name as Name,email as Email_Address,contactinfo as Contact_No from users;";
            com.Connection = con;

            com.ExecuteNonQuery();

            MySqlDataAdapter adapter = new MySqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            kryptonDataGridView1.DataSource = dt;

            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            kryptonDataGridView1.Columns[0].Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1(null) { TopMost=true,TopLevel=false };

            frm1.Dock = DockStyle.Fill;
            frm1.FormBorderStyle = FormBorderStyle.None;

            panel1.Controls.Clear();
            panel1.Controls.Add(frm1);

            frm1.Show();
        }

        private void kryptonDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = kryptonDataGridView1.Rows[e.RowIndex];

            Form1 frm1 = new Form1(selectedRow.Cells["id"].Value.ToString()) { TopMost=true,TopLevel=false };

            frm1.Dock = DockStyle.Fill;
            frm1.FormBorderStyle = FormBorderStyle.None;

            panel1.Controls.Clear();
            panel1.Controls.Add(frm1);

            frm1.Show();
        }
    }
}
