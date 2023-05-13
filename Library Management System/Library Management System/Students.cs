using System;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace Library_Management_System
{
    public partial class Students : Form
    {

        MySqlConnection con = new MySqlConnection();
        ApplicationConfiguration app = new ApplicationConfiguration();
        MySqlCommand com = new MySqlCommand();

        public Students(string id)
        {
            InitializeComponent();

            if (id != null)
            {
                FetchStudentById(id);
                kryptonWrapLabel10.Text = id.ToString();
            }

            else
            {
                kryptonWrapLabel10.Text = "";
            }
        }

        private void FetchStudentById(string id)
        {
            con.ConnectionString = app.constr;

            con.Open();

            com.CommandText = "select id,name,fname,age,gender,batch,department,email,contactinfo,addres from students where id="+Convert.ToInt32(id);
            com.Connection = con;

            MySqlDataReader reader = com.ExecuteReader();

            reader.Read();

            con.Close();
        }

        private void Students_Load(object sender, EventArgs e)
        {
            this.Text = "Manage Students";
            this.BackColor = Color.White;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            con.ConnectionString = app.constr;

            FetchDepartment();
            SetBatch();

            txtName.Text = txtFatherName.Text = txtContactInfo.Text = txtAge.Text = txtEmailAddress.Text = txtRollNo.Text = txtAddress.Text = string.Empty;
        }

        private void FetchDepartment()
        {
            con.Open();

            com.CommandText = "select * from departments";
            com.Connection = con;

            MySqlDataAdapter adapter = new MySqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            cmbDepartment.DataSource = dt;
            cmbDepartment.DisplayMember = "name";
            cmbDepartment.ValueMember = "id";

            con.Close();
        }

        private void SetBatch()
        {
            int currentYear = DateTime.Now.Year;

            for (int a = 1970; a <= currentYear; a++)
            {
                cmbBatch.Items.Add(a);
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
         
            try
            {
                string[] cols = { "contactinfo","name", "fname", "age", "gender", "email", "addres", "rollno", "batch", "department" };
                object[] vals = { txtContactInfo.Text,txtName.Text, txtFatherName.Text, Convert.ToInt16(txtAge.Text), cmbGender.Text, txtEmailAddress.Text, txtAddress.Text, txtRollNo.Text.ToUpper().ToString(), cmbBatch.Text, (int)cmbDepartment.SelectedValue };
                string tableName = "students";

                con.Open();

                com.CommandText = app.InsertQuery(tableName, cols, vals);
                com.Connection = con;

                com.ExecuteNonQuery();

                txtAddress.Text = txtAge.Text = txtEmailAddress.Text = txtFatherName.Text = txtName.Text = txtRollNo.Text = string.Empty;

                MessageBox.Show("Student Registered Successfully..", "Library Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);

                con.Close();
            }   

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Library Management System..",MessageBoxButtons.OK,MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}