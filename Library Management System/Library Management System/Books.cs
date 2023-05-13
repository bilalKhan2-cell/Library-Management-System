using System;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class Books : Form
    {
        public Books(string id)
        {
            InitializeComponent();

            if(id!=null)
            {
                FetchBookById(id);
            }
        }

        MySqlConnection con = new MySqlConnection();
        MySqlCommand com = new MySqlCommand();
        ApplicationConfiguration app = new ApplicationConfiguration();

        private void FetchBookById(string id)
        {
            con.ConnectionString = app.constr;
            con.Open();

            com.CommandText = "select id,NAME,author,publication_date,publisher from books where id="+Convert.ToInt32(id);
            com.Connection = con;

            MySqlDataReader BookReader = com.ExecuteReader();

            BookReader.Read();

            kryptonTextBox1.Text = BookReader.GetString(1);
            kryptonTextBox2.Text = BookReader.GetString(2);
            kryptonTextBox3.Text = BookReader.GetString(4);
            kryptonDateTimePicker1.Value = BookReader.GetDateTime(3);
            kryptonWrapLabel5.Text = BookReader.GetString(0);

            con.Close();
        }

        private void Books_Load(object sender, EventArgs e)
        {
            this.Text = "Manage Books";
            this.BackColor = Color.White;

            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.FormBorderStyle = FormBorderStyle.None;

            kryptonWrapLabel5.Hide();

            con.ConnectionString = app.constr;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string[] cols = {"name","author","quantity","publisher","publication_date"};
                object[] vals = {kryptonTextBox1.Text.ToUpper().ToString(),kryptonTextBox2.Text,0,kryptonTextBox3.Text,kryptonDateTimePicker1.Value.ToString("yyyy/MM/dd")};
                string tableName = "books";

                com.Connection = con;

                if (kryptonWrapLabel5.Text=="" ||kryptonWrapLabel5.Text==string.Empty)
                {
                    com.CommandText = app.InsertQuery(tableName,cols,vals);
                    com.ExecuteNonQuery();

                    MessageBox.Show("Book Have Successfully Added..","Library Management System..",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }

                else
                {
                    com.CommandText = app.UpdateQuery(tableName,cols,vals,"id="+Convert.ToInt32(kryptonWrapLabel5.Text));
                    com.ExecuteNonQuery();

                    MessageBox.Show("Book Updated Successfully..","Library Management System..",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }

                kryptonTextBox1.Text = kryptonTextBox2.Text = kryptonTextBox3.Text = kryptonWrapLabel5.Text =  string.Empty;

                con.Close();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Library Management System..",MessageBoxButtons.OK,MessageBoxIcon.Error);

                con.Close();
            }
        }
    }
}
