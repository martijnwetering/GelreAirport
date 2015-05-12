using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace GelreAirport.Data.Infrastructure
{
    public class AdoNetContext
    {
        private readonly IDbConnection _connection;

        public AdoNetContext(IConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.Create();
        }

        public SqlCommand CreateCommand()
        {
            var cmd = _connection.CreateCommand();
            return (SqlCommand)cmd;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

    }
}