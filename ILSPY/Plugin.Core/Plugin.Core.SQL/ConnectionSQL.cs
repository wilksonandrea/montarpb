using System.Data.Common;
using System.Runtime.Remoting.Contexts;
using Npgsql;

namespace Plugin.Core.SQL;

[Synchronization]
public class ConnectionSQL
{
	private static ConnectionSQL connectionSQL_0 = new ConnectionSQL();

	protected NpgsqlConnectionStringBuilder ConnBuilder;

	public ConnectionSQL()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		ConnBuilder = new NpgsqlConnectionStringBuilder
		{
			Database = ConfigLoader.DatabaseName,
			Host = ConfigLoader.DatabaseHost,
			Username = ConfigLoader.DatabaseUsername,
			Password = ConfigLoader.DatabasePassword,
			Port = ConfigLoader.DatabasePort
		};
	}

	public static ConnectionSQL GetInstance()
	{
		return connectionSQL_0;
	}

	public NpgsqlConnection Conn()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		return new NpgsqlConnection(((DbConnectionStringBuilder)(object)ConnBuilder).ConnectionString);
	}
}
