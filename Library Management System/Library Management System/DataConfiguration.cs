using MySql.Data.MySqlClient;

namespace Library_Management_System
{
    class DataConfiguration
    {
        ApplicationConfiguration app = new ApplicationConfiguration();
        MySqlCommand com = new MySqlCommand();
        MySqlConnection con = new MySqlConnection();

        public DataConfiguration()
        {
            con.ConnectionString = app.constr;
        }

        public bool IsDepartmentExists(string department)
        {
            com.CommandText = "select count(*) from departments where name='"+department.ToUpper().ToString()+"';";
            com.Connection = con;

            object result = com.ExecuteScalar();
            if(result!=null && result.ToString()=="1")
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool IsStudentEmailExists(string email)
        {
            com.CommandText = "select count(email) from students where email='"+email+"';";
            com.Connection = con;

            return com.ExecuteScalar().ToString() == "1" ? true : false;
        }

        public bool IsBookRegistered(string BookName)
        {
            com.CommandText = "select count(name) from books where name='"+BookName+"';";
            com.Connection = con;

            return com.ExecuteScalar().ToString() == "1" ? true : false;
        }
    }
}
