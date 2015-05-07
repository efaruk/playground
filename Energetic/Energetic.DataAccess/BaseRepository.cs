using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.DataAccess
{
    public class BaseRepository: IDisposable
    {
        public BaseRepository(string connectionString)
        {
            ConnectionString = connectionString.Replace("{DataDirectory}", AppDomain.CurrentDomain.GetData("DataDirectory").ToString());
            _connection = new SQLiteConnection(ConnectionString);
            _connection.Open();
        }

        public string ConnectionString
        {
            get; private set;
        }

        private SQLiteConnection _connection;
        public SQLiteConnection Connection
        {
            get { return _connection; }
        }


        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Open) _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
