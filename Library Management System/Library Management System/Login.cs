using System;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class Login : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand com = new MySqlCommand();
        ApplicationConfiguration app = new ApplicationConfiguration();

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.Text = "Library Management System - Login";
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Dock = DockStyle.Fill;

            kryptonTextBox2.UseSystemPasswordChar = true;

            con.ConnectionString = app.constr;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            kryptonTextBox1.Text = kryptonTextBox1.Text.ToUpper().ToString();
        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            con.Open();

            if (kryptonTextBox1.Text == "ADMIN" && kryptonTextBox2.Text == "Asd_1212")
            {
                Dashboard dash = new Dashboard(1);

                dash.Show();

                this.Hide();
            }

            else
            {
                com.CommandText = "select * from users where email='" + (kryptonTextBox1.Text) + "' and pasword='" + kryptonTextBox2.Text + "';";
                com.Connection = con;

                MySqlDataAdapter adapter = new MySqlDataAdapter(com);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Dashboard dash = new Dashboard(2);

                    dash.Show();

                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Invalid Email or Password..", "Library Management System..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            con.Close();
        }
    }
}
