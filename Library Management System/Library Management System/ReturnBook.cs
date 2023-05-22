using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class ReturnBook : Form
    {
        public ReturnBook()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection();
        MySqlCommand com = new MySqlCommand();
        ApplicationConfiguration app = new ApplicationConfiguration();

        private void ReturnBook_Load(object sender, EventArgs e)
        {
            this.Text = "Return Book";
            this.FormBorderStyle = FormBorderStyle.None;
            panel2.Visible = false;

            //kryptonLabel1.ForeColor = Color.White;

            this.BackColor = Color.White;
            kryptonDataGridView1.Visible = false;

            con.ConnectionString = app.constr;

            kryptonWrapLabel15.Visible = kryptonWrapLabel17.Visible = false;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if(kryptonTextBox1.Text==""||kryptonTextBox1.Text==string.Empty)
            {
                MessageBox.Show("Roll Number is Required..","Libary Management System..",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }

            else
            {
                con.Open();

                com.CommandText = "SELECT s.id,s.rollno as Erollment_No, s.name as Name, d.name as Department, b.id,b.name as Book FROM students s JOIN departments d ON s.department = d.id JOIN issuance i ON s.id = i.student_id JOIN books b ON i.book_id = b.id WHERE s.rollno='"+kryptonTextBox1.Text.ToUpper().ToString()+"';";
                com.Connection = con;

                MySqlDataAdapter adapter = new MySqlDataAdapter(com);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                MySqlDataReader reader = com.ExecuteReader();


                if(dt.Rows.Count==0)
                {
                    MessageBox.Show("No Record Found..","Library Management System..",MessageBoxButtons.OK,MessageBoxIcon.None);
                }

                else
                {
                    reader.Read();
                    string id = reader.GetInt32(0).ToString();

                    com.CommandText = string.Empty;
                    //com.CommandText = "select ";
                    com.CommandText = "select books.name,students.name as Student_Name,students.rollno as Student_RollNo,issuance._from as Issuing_Date,issuance._until as Upto from issuance left join students on issuance.student_id=students.id right join books on issuance.book_id=books.id where issuance.student_id='" + id + "' and issuance.return_date is null;";
                    com.Connection = con;

                    kryptonDataGridView1.DataSource = dt;
                    kryptonDataGridView1.Columns[0].Visible = false;
                    kryptonDataGridView1.Columns[4].Visible = false;
                    kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    kryptonDataGridView1.Visible = true;

                    
                }

                con.Close();
            }
        }

        private void kryptonDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (kryptonDataGridView1.SelectedRows.Count > 0) // check if there is a selected row
            {
                DataGridViewRow selectedRow = kryptonDataGridView1.SelectedRows[0]; // get the selected row
                string firstColumnValue = selectedRow.Cells[0].Value.ToString(); // get the value of the first cell in the selected row
                FetchStudent(firstColumnValue);
                FetchBookById(selectedRow.Cells[4].Value.ToString()); // use the firstColumnValue variable as needed
                FetchIssuance(firstColumnValue,selectedRow.Cells[4].Value.ToString());
           }
        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FetchIssuance(string rollno,string book_id)
        {
            con.Open();

            com.CommandText = "select * from students where id='"+rollno.ToUpper().ToString()+"';";
            com.Connection = con;

            MySqlDataReader read = com.ExecuteReader();
            if(read.Read())
            {
                int student_id = read.GetInt32(0);

                con.Close();

                con.Open();

                com.CommandText = "select * from issuance where student_id=" + Convert.ToInt32(student_id) + " and book_id=" + Convert.ToInt32(book_id) + " and return_date is null;";
                com.Connection = con;

                MySqlDataReader IssuanceReader = com.ExecuteReader();

                IssuanceReader.Read();

                lblIssuingDate.Text = IssuanceReader.GetString(3);
                lblTillDate.Text = IssuanceReader.GetString(2);

                TimeSpan duration = DateTime.Parse(lblTillDate.Text) - DateTime.Parse(lblIssuingDate.Text);
                lblTotalDays.Text = duration.Days.ToString();

                con.Close();
            }

            else
            {
                MessageBox.Show("null");
                con.Close();
            }
        }

        private void FetchStudent(string id)
        {
            con.Open();

            com.CommandText = "select students.id,students.name,students.fname,students.email,departments.name as Name,students.batch,students.contactinfo,students.addres from students join departments on students.department=departments.id where students.id="+Convert.ToInt32(id)+";";
            com.Connection = con;

            MySqlDataReader StudentReader = com.ExecuteReader();

            StudentReader.Read();

            lblName.Text = StudentReader.GetString(1);
            lblFather.Text = StudentReader.GetString(2);
            lblEmailAddress.Text = StudentReader.GetString(3);
            lblDept.Text = StudentReader.GetString("Name");
            lblBatch.Text = StudentReader.GetInt32(5).ToString();
            lblContact.Text = StudentReader.GetString(4);
            lblAddress.Text = StudentReader.GetString(7);
            kryptonWrapLabel15.Text = StudentReader.GetInt32(0).ToString();

            con.Close();
        }

        private void FetchBookById(string bookID)
        {
            con.Open();

            com.CommandText = "select * from books where id=" + Convert.ToInt32(bookID) + ";";
            com.Connection = con;

            MySqlDataReader BookReader = com.ExecuteReader();

            BookReader.Read();

            lblBookName.Text = BookReader.GetString(1);
            lblAuthorName.Text = BookReader.GetString(2);
            lblPublishers.Text = BookReader.GetString(3);
            lblPublicationDate.Text = BookReader.GetString(4);
            kryptonWrapLabel17.Text = BookReader.GetInt32(0).ToString();

            con.Close();

            panel2.Visible = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            con.Open();

            string[] cols = {"return_date"};
            object[] values = {kryptonDateTimePicker1.Value.ToString("yyyy-MM-dd")};
            com.CommandText = app.UpdateQuery("issuance",cols,values,"book_id="+Convert.ToInt32(kryptonWrapLabel17.Text)+" and student_id="+Convert.ToInt32(kryptonWrapLabel15.Text)+";");
            com.Connection = con;

            com.ExecuteNonQuery();

            MessageBox.Show("Book Returned Succesfully..");

            panel2.Visible = false;
            kryptonTextBox1.Text = string.Empty;

            con.Close();
        }

        private void kryptonSeparator1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }
}
