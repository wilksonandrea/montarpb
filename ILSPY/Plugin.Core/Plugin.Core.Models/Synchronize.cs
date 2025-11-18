using System.Net;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class Synchronize
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private IPEndPoint ipendPoint_0;

	public int RemotePort
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	public IPEndPoint Connection
	{
		[CompilerGenerated]
		get
		{
			return ipendPoint_0;
		}
		[CompilerGenerated]
		set
		{
			ipendPoint_0 = value;
		}
	}

	public Synchronize(string string_0, int int_1)
	{
		Connection = new IPEndPoint(IPAddress.Parse(string_0), int_1);
	}
}
