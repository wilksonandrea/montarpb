using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class PlayerQuickstart
{
	[CompilerGenerated]
	private long long_0;

	[CompilerGenerated]
	private List<QuickstartModel> list_0;

	public long OwnerId
	{
		[CompilerGenerated]
		get
		{
			return long_0;
		}
		[CompilerGenerated]
		set
		{
			long_0 = value;
		}
	}

	public List<QuickstartModel> Quickjoins
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		set
		{
			list_0 = value;
		}
	}

	public PlayerQuickstart()
	{
		Quickjoins = new List<QuickstartModel>();
	}

	public QuickstartModel GetMapList(byte MapId)
	{
		lock (Quickjoins)
		{
			foreach (QuickstartModel quickjoin in Quickjoins)
			{
				if (quickjoin.MapId == MapId)
				{
					return quickjoin;
				}
			}
		}
		return null;
	}
}
