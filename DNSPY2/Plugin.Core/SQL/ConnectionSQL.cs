using System;
using System.Runtime.Remoting.Contexts;
using Npgsql;

namespace Plugin.Core.SQL
{
	// Token: 0x02000008 RID: 8
	[Synchronization]
	public class ConnectionSQL
	{
		// Token: 0x06000083 RID: 131 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		public ConnectionSQL()
		{
			this.ConnBuilder = new NpgsqlConnectionStringBuilder
			{
				Database = ConfigLoader.DatabaseName,
				Host = ConfigLoader.DatabaseHost,
				Username = ConfigLoader.DatabaseUsername,
				Password = ConfigLoader.DatabasePassword,
				Port = ConfigLoader.DatabasePort
			};
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000024BE File Offset: 0x000006BE
		public static ConnectionSQL GetInstance()
		{
			return ConnectionSQL.connectionSQL_0;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000024C5 File Offset: 0x000006C5
		public NpgsqlConnection Conn()
		{
			return new NpgsqlConnection(this.ConnBuilder.ConnectionString);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000024D7 File Offset: 0x000006D7
		// Note: this type is marked as 'beforefieldinit'.
		static ConnectionSQL()
		{
		}

		// Token: 0x0400004B RID: 75
		private static ConnectionSQL connectionSQL_0 = new ConnectionSQL();

		// Token: 0x0400004C RID: 76
		protected NpgsqlConnectionStringBuilder ConnBuilder;
	}
}
