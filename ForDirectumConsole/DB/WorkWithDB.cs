using System.Data;
using Npgsql;

namespace ForDirectumConsole.Data
{
    internal class WorkWithDB
    {
        public string _ConnectionString { get; set; }
        private NpgsqlConnection _myConnection;
        public void OpenConnection()
        {
            var cc = new ConfigurationConnect();
            _ConnectionString = $"Server={cc.Server};Port={cc.Port};User Id={cc.User_Id};Password={cc.Password};Database={cc.Database}";
            _myConnection = new NpgsqlConnection(_ConnectionString);
            _myConnection.Open();
        }
        public List<Meets> QuerySelect(string query)
        {
            var list = new List<Meets>();
            var cmd = new NpgsqlCommand(query, _myConnection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Meets meet = new Meets()
                {
                    Id = (int)reader["Id"],
                    Title = (string)reader["Title"],
                    StartMeet = (DateTime)reader["StartMeet"],
                    EndMeet = (DateTime)reader["EndMeet"],
                    Alert = (int)reader["Alert"]
                };
                list.Add(meet);
            }
            reader.Close();
            return list;
        }
        public int QuerySelectCheck(string query)
        {
            var cmd = new NpgsqlCommand(query, _myConnection);
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            int numRows = dt.Rows.Count;
            return numRows;
        }
        public void QueryOther(string query)
        {
            var cmd = new NpgsqlCommand(query, _myConnection);
            cmd.ExecuteNonQuery();
        }
        public void CloseConnection()
        {
            _myConnection.Close();
        }
    }
}
