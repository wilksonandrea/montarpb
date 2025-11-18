using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class MissionItemAward
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private ItemsModel itemsModel_0;

	public int MissionId
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

	public ItemsModel Item
	{
		[CompilerGenerated]
		get
		{
			return itemsModel_0;
		}
		[CompilerGenerated]
		set
		{
			itemsModel_0 = value;
		}
	}
}
