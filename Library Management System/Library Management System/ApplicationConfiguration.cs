using System.Linq;

namespace Library_Management_System { 
    class ApplicationConfiguration
    {
        public string constr = "server=localhost;user=root;pass=;database=library;";

        public string InsertQuery(string table,string[] cols,object[] values)
        {
            return "INSERT INTO "+table+"("+string.Join(", ",cols)+") values("+string.Join(", ", values.Select(v => $"'{v}'"))+")";
        }

        public string UpdateQuery(string table,string[] col, object[] val,string condition)
        {
            string setClause = string.Join(", ", col.Zip(val, (c, v) => $"{c}='{v}'"));

            return $"UPDATE {table} SET {setClause} WHERE {condition}";
        }

        public string DeleteQuery(string table, string condition)
        {
            return "DELETE FROM "+table+" WHERE "+condition;
        }
    }
}
