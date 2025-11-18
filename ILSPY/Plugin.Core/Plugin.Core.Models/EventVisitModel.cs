using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Plugin.Core.Models;

public class EventVisitModel
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private uint uint_0;

	[CompilerGenerated]
	private uint uint_1;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private List<VisitBoxModel> list_0;

	public int Id
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

	public uint BeginDate
	{
		[CompilerGenerated]
		get
		{
			return uint_0;
		}
		[CompilerGenerated]
		set
		{
			uint_0 = value;
		}
	}

	public uint EndedDate
	{
		[CompilerGenerated]
		get
		{
			return uint_1;
		}
		[CompilerGenerated]
		set
		{
			uint_1 = value;
		}
	}

	public int Checks
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		set
		{
			int_1 = value;
		}
	}

	public string Title
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

	public List<VisitBoxModel> Boxes
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

	public EventVisitModel()
	{
		Checks = 31;
		Title = "";
	}

	public bool EventIsEnabled()
	{
		uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
		if (BeginDate <= num)
		{
			return num < EndedDate;
		}
		return false;
	}

	public List<VisitItemModel> GetReward(int Day, int Type)
	{
		List<VisitItemModel> list = new List<VisitItemModel>();
		switch (Type)
		{
		case 0:
			list.Add(Boxes[Day].Reward1);
			break;
		case 1:
			list.Add(Boxes[Day].Reward2);
			break;
		default:
			list.Add(Boxes[Day].Reward1);
			list.Add(Boxes[Day].Reward2);
			break;
		}
		return list;
	}

	public void SetBoxCounts()
	{
		for (int i = 0; i < 31; i++)
		{
			Boxes[i].SetCount();
		}
	}
}
