using Npgsql;
using Plugin.Core;
using System;
using System.Data.Common;
using System.Runtime.Remoting.Contexts;

namespace Plugin.Core.SQL
{
	[Synchronization]
	public class ConnectionSQL
	{
		private static ConnectionSQL connectionSQL_0;

		protected NpgsqlConnectionStringBuilder ConnBuilder;

		static ConnectionSQL()
		{
			ConnectionSQL.connectionSQL_0 = new ConnectionSQL();
		}

		public ConnectionSQL()
		{
			NpgsqlConnectionStringBuilder npgsqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder();
			npgsqlConnectionStringBuilder.set_Database(ConfigLoader.DatabaseName);
			npgsqlConnectionStringBuilder.set_Host(ConfigLoader.DatabaseHost);
			npgsqlConnectionStringBuilder.set_Username(ConfigLoader.DatabaseUsername);
			npgsqlConnectionStringBuilder.set_Password(ConfigLoader.DatabasePassword);
			npgsqlConnectionStringBuilder.set_Port(ConfigLoader.DatabasePort);
			this.ConnBuilder = npgsqlConnectionStringBuilder;
		}

		public NpgsqlConnection Conn()
		{
			return new NpgsqlConnection(this.ConnBuilder.ConnectionString);
		}

		public static ConnectionSQL GetInstance()
		{
			return ConnectionSQL.connectionSQL_0;
		}
	}
}