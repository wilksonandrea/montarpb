using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class TRuleModel
{
	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private List<int> list_0;

	public string Name
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		set
		{
			string_0 = value;
		}
	}

	public List<int> BanIndexes
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
}
