using System;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models;

public class ObjectInfo
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private AnimModel animModel_0;

	[CompilerGenerated]
	private DateTime dateTime_0;

	[CompilerGenerated]
	private ObjectModel objectModel_0;

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

	public int Life
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

	public int DestroyState
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		set
		{
			int_2 = value;
		}
	}

	public AnimModel Animation
	{
		[CompilerGenerated]
		get
		{
			return animModel_0;
		}
		[CompilerGenerated]
		set
		{
			animModel_0 = value;
		}
	}

	public DateTime UseDate
	{
		[CompilerGenerated]
		get
		{
			return dateTime_0;
		}
		[CompilerGenerated]
		set
		{
			dateTime_0 = value;
		}
	}

	public ObjectModel Model
	{
		[CompilerGenerated]
		get
		{
			return objectModel_0;
		}
		[CompilerGenerated]
		set
		{
			objectModel_0 = value;
		}
	}

	public ObjectInfo(int int_3)
	{
		Id = int_3;
		Life = 100;
	}
}
