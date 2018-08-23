using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Repositories
{
    /// <summary>
    /// Sqlite 代理类
    /// </summary>
    public sealed class SqliteProxy
    {
        private static string ConnectionString;
        public static void Init(string dbPath)
        {
            ConnectionString = $"Data Source={dbPath};Pooling=true;FailIfMissing=false";
        }

        public static IList<TData> Select<TData>(string commandText, Func<IDataRecord,TData> converter, CommandType commandType = CommandType.Text, params SQLiteParameter[] parameters)
        {
            IList<TData> result = new List<TData>();
            using (var dataReader = GetDataReader(commandText, commandType, parameters))
            {
                while (dataReader.Read())
                {
                    result.Add(converter(dataReader));
                }
            }
            return result;
        }

        public static TData SelectSingle<TData>(string commandText, Func<IDataRecord, TData> converter, CommandType commandType = CommandType.Text, params SQLiteParameter[] parameters)
        {
            using (var dataReader = GetDataReader(commandText, commandType, parameters))
            {
                if (dataReader.Read())
                {
                    return converter(dataReader);
                }
            }
            return default(TData);
        }

        public static int Execute(string commandText, CommandType commandType = CommandType.Text, params SQLiteParameter[] parameters)
        {
            IDbConnection connection = null;
            try
            {
                var command = GetCommand(commandText, commandType, parameters);
                connection = command.Connection;
                return command.ExecuteNonQuery();
            }
            finally
            {
                connection?.Dispose();
            }
        }       

        private static IDataReader GetDataReader(string commandText, CommandType commandType = CommandType.Text, params SQLiteParameter[] parameters)
        {            
            return GetCommand(commandText, commandType, parameters).ExecuteReader(CommandBehavior.CloseConnection);
        }

        private static SQLiteCommand GetCommand(string commandText, CommandType commandType = CommandType.Text, params SQLiteParameter[] parameters)
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            return command;
        }
    }
}
