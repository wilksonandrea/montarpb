using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core.Models;

public class MessageModel
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private long long_0;

	[CompilerGenerated]
	private long long_1;

	[CompilerGenerated]
	private long long_2;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private string string_1;

	[CompilerGenerated]
	private NoteMessageState noteMessageState_0;

	[CompilerGenerated]
	private NoteMessageType noteMessageType_0;

	[CompilerGenerated]
	private NoteMessageClan noteMessageClan_0;

	[CompilerGenerated]
	private int int_1;

	public int ClanId
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

	public long ObjectId
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

	public long SenderId
	{
		[CompilerGenerated]
		get
		{
			return long_1;
		}
		[CompilerGenerated]
		set
		{
			long_1 = value;
		}
	}

	public long ExpireDate
	{
		[CompilerGenerated]
		get
		{
			return long_2;
		}
		[CompilerGenerated]
		set
		{
			long_2 = value;
		}
	}

	public string SenderName
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

	public string Text
	{
		[CompilerGenerated]
		get
		{
			return string_1;
		}
		[CompilerGenerated]
		set
		{
			string_1 = value;
		}
	}

	public NoteMessageState State
	{
		[CompilerGenerated]
		get
		{
			return noteMessageState_0;
		}
		[CompilerGenerated]
		set
		{
			noteMessageState_0 = value;
		}
	}

	public NoteMessageType Type
	{
		[CompilerGenerated]
		get
		{
			return noteMessageType_0;
		}
		[CompilerGenerated]
		set
		{
			noteMessageType_0 = value;
		}
	}

	public NoteMessageClan ClanNote
	{
		[CompilerGenerated]
		get
		{
			return noteMessageClan_0;
		}
		[CompilerGenerated]
		set
		{
			noteMessageClan_0 = value;
		}
	}

	public int DaysRemaining
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

	public MessageModel()
	{
		SenderName = "";
		Text = "";
	}

	public MessageModel(long long_3, DateTime dateTime_0)
	{
		SenderName = "";
		Text = "";
		ExpireDate = long_3;
		method_0(dateTime_0);
	}

	public MessageModel(double double_0)
	{
		SenderName = "";
		Text = "";
		DateTime dateTime_ = DateTimeUtil.Now().AddDays(double_0);
		ExpireDate = long.Parse(dateTime_.ToString("yyMMddHHmm"));
		method_1(dateTime_, DateTimeUtil.Now());
	}

	private void method_0(DateTime dateTime_0)
	{
		DateTime dateTime_ = DateTime.ParseExact(ExpireDate.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture);
		method_1(dateTime_, dateTime_0);
	}

	private void method_1(DateTime dateTime_0, DateTime dateTime_1)
	{
		int num = (int)Math.Ceiling((dateTime_0 - dateTime_1).TotalDays);
		DaysRemaining = ((num >= 0) ? num : 0);
	}
}
