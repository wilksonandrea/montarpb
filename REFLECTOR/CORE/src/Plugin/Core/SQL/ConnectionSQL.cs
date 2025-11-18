namespace Plugin.Core.SQL
{
    using Npgsql;
    using Plugin.Core;
    using System;
    using System.Runtime.Remoting.Contexts;

    [Synchronization]
    public class ConnectionSQL
    {
        private static ConnectionSQL connectionSQL_0 = new ConnectionSQL();
        protected NpgsqlConnectionStringBuilder ConnBuilder;

        public ConnectionSQL()
        {
            NpgsqlConnectionStringBuilder builder1 = new NpgsqlConnectionStringBuilder();
            builder1.Database = ConfigLoader.DatabaseName;
            builder1.Host = ConfigLoader.DatabaseHost;
            builder1.Username = ConfigLoader.DatabaseUsername;
            builder1.Password = ConfigLoader.DatabasePassword;
            builder1.Port = ConfigLoader.DatabasePort;
            this.ConnBuilder = builder1;
        }

        public NpgsqlConnection Conn() => 
            new NpgsqlConnection(this.ConnBuilder.ConnectionString);

        public static ConnectionSQL GetInstance() => 
            connectionSQL_0;
    }
}

