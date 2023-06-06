using System;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class Form1 : Form
    {
        public Form1(string id)
        {
            InitializeComponent();

            if(id!=null)
            {
                FetchUserById(id);
                label1.Text = id;
            }

            else
            {
                label1.Text = string.Empty;
            }
        }

        ApplicationConfiguration app = new ApplicationConfiguration();
        MySqlConnection con = new MySqlConnection();
        MySqlCommand com = new MySqlCommand();

        private void FetchUserById(string id)
        {
            con.ConnectionString = app.constr;

            con.Open();

            com.CommandText = "select id,name,email,contactinfo,pasword from users where id="+Convert.ToInt32(id);
            com.Connection = con;

            MySqlDataReader UserReader = com.ExecuteReader();

            UserReader.Read();

            txtUsername.Text = UserReader.GetString(1);
            txtPassword.Text = txtConfirmPassword.Text = UserReader.GetString(4);
            txtEmail.Text = UserReader.GetString(2);
            txtContactInfo.Text = UserReader.GetString(3);

            label1.Text = id;

            txtPassword.ReadOnly = txtConfirmPassword.ReadOnly = true;

            con.Close();
                kryptonButton2.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Create User";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            this.FormBorderStyle = FormBorderStyle.None;

            con.ConnectionString = app.constr;

            label1.Hide();

            if (label1.Text == "" || label1.Text == string.Empty)
            {
                 kryptonButton2.Visible = false;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

            if(txtPassword.Text!=txtConfirmPassword.Text)
            {
                MessageBox.Show("Password Mismatch");
            }

            else
            {
                try
                {
                    con.Open();

                    string[] cols = { "name","email","contactinfo","pasword","pass_key" };
                    object[] vals = { txtUsername.Text,txtEmail.Text,txtContactInfo.Text,txtPassword.Text,0};
                    com.Connection = con;

                    if(label1.Text=="" || label1.Text==string.Empty || label1.Text==" ")
                    {
                        com.CommandText = app.InsertQuery("users",cols,vals);
                        com.ExecuteNonQuery();
                        MessageBox.Show("User Registered Successfully..","Library Management System",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }

                    else
                    {
                        string[] _cols = { "name", "email", "contactinfo", "pasword" };

                        com.CommandText = app.UpdateQuery("users",_cols,vals,"id="+Convert.ToInt32(label1.Text));
                        com.ExecuteNonQuery();
                        MessageBox.Show("User Updated Successfully..","Library Management System..",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }

                    con.Close();
                }

                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"Library Management System..",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            con.Open();

            com.Connection = con;
            com.CommandText = app.DeleteQuery("users","id="+Convert.ToInt32(label1.Text));
            com.ExecuteNonQuery();

            MessageBox.Show("User Deleted Successfully..","Library Management System..",MessageBoxButtons.OK,MessageBoxIcon.Information);
            kryptonButton2.Visible = false;
            label1.Text = "";

            con.Close();
        }
    }
}
