using System;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace Library_Management_System
{
    public partial class ListBooks : Form
    {
        public ListBooks()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection();
        MySqlCommand com = new MySqlCommand();
        ApplicationConfiguration app = new ApplicationConfiguration();

        private void ListBooks_Load(object sender, EventArgs e)
        {
            this.Text = "Books";

            this.BackColor = Color.White;

            this.FormBorderStyle = FormBorderStyle.None;

            con.ConnectionString = app.constr;

            FetchBooks();
        }

        private void FetchBooks()
        {
            con.ConnectionString = app.constr;

            con.Open();

            com.CommandText = "select id,NAME as Name,author as Author,publication_date as Publishment_Date, publisher as Publisher from books";

            com.Connection = con;

            com.ExecuteNonQuery();

            MySqlDataAdapter BookAdapter = new MySqlDataAdapter(com);
            DataTable dt = new DataTable();
            BookAdapter.Fill(dt);

            kryptonDataGridView1.DataSource = dt;
            kryptonDataGridView1.Columns[0].Visible = false;

            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            con.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            Books book = new Books(null) { TopMost = true, TopLevel = false };

            book.FormBorderStyle = FormBorderStyle.None;
            book.Dock = DockStyle.Fill;

            panel1.Controls.Clear();
            panel1.Controls.Add(book);

            book.Show();
        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kryptonDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = kryptonDataGridView1.Rows[e.RowIndex];

            Books book = new Books(selectedRow.Cells["id"].Value.ToString()) { TopMost=true,TopLevel = false };

            book.Dock = DockStyle.Fill;
            book.FormBorderStyle = FormBorderStyle.None;

            panel1.Controls.Clear();
            panel1.Controls.Add(book);

            book.Show();   
        }
    }
}
