using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace GelreAirport.Data.Infrastructure
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }

    public class ConnectionFactory : IConnectionFactory
    {
        private readonly DbProviderFactory _provider;
        private readonly string _connectionString;
        private readonly string _name;

        public ConnectionFactory(string connectionName)
        {
            if (connectionName == null) throw new ArgumentNullException("connectionName");

            var conStr = ConfigurationManager.ConnectionStrings[connectionName];
            if (conStr == null)
                throw new ConfigurationErrorsException(string.Format("Failed to find connection string named {0}", connectionName));

            _name = conStr.ProviderName;
            _provider = DbProviderFactories.GetFactory(conStr.ProviderName);
            _connectionString = conStr.ConnectionString;

        }

        public IDbConnection Create()
        {
            var connection = _provider.CreateConnection();
            if (connection == null)
                throw new ConfigurationErrorsException("Failed to create connection");

            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
    }
}