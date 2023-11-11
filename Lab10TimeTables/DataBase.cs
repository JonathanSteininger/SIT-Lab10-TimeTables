using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Lab10TimeTables
{
    
    internal class DataBase
    {
        private string _connectionString;
        private MySqlConnection _conn;
        public DataBase(string ConnectionString) {
            _connectionString = ConnectionString;
            Connect();
        }
        public void Connect(string ConnectionString)
        {
            Close();
            _conn = new MySqlConnection(ConnectionString);
            _connectionString = ConnectionString;
        }
        public void Connect() => Connect(_connectionString);

        public void Close() { if (_conn != null) _conn.Close(); }
        public void Open() { if (_conn != null) _conn.Open(); }
        
        public List<object[]> Query(string QueryString)
        {
            List<object[]> table = new List<object[]>();
            Open();
            using(MySqlCommand cmd = new MySqlCommand(QueryString, _conn))
            using(MySqlDataReader reader = cmd.ExecuteReader()) {
                while(reader.Read())
                {
                    object[] temp = new object[reader.FieldCount];
                    for(int i = 0; i < reader.FieldCount; i++)
                    {
                        temp[i] = reader.GetValue(i);
                    }
                    table.Add(temp);
                }
                reader.Close();
            
            }
            Close();
            return table;
        }


    }
}
