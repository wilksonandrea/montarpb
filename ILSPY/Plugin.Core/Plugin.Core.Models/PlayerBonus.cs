using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class PlayerBonus
{
	[CompilerGenerated]
	private long long_0;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private int int_5;

	[CompilerGenerated]
	private string string_0;

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

	public int Bonuses
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

	public int CrosshairColor
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

	public int MuzzleColor
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

	public int FreePass
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		set
		{
			int_3 = value;
		}
	}

	public int FakeRank
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		set
		{
			int_4 = value;
		}
	}

	public int NickBorderColor
	{
		[CompilerGenerated]
		get
		{
			return int_5;
		}
		[CompilerGenerated]
		set
		{
			int_5 = value;
		}
	}

	public string FakeNick
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

	public PlayerBonus()
	{
		CrosshairColor = 4;
		FakeRank = 55;
		FakeNick = "";
	}

	public bool RemoveBonuses(int ItemId)
	{
		int bonuses = Bonuses;
		int freePass = FreePass;
		switch (ItemId)
		{
		case 1600037:
			method_0(8);
			break;
		case 1600011:
			method_2(128);
			break;
		case 1600001:
			method_0(1);
			break;
		case 1600002:
			method_0(2);
			break;
		case 1600003:
			method_0(4);
			break;
		case 1600004:
			method_0(16);
			break;
		case 1600201:
			method_0(512);
			break;
		case 1600202:
			method_0(1024);
			break;
		case 1600203:
			method_0(2048);
			break;
		case 1600204:
			method_0(4096);
			break;
		case 1600119:
			method_0(32);
			break;
		case 1600038:
			method_0(64);
			break;
		}
		if (Bonuses == bonuses)
		{
			return FreePass != freePass;
		}
		return true;
	}

	public bool AddBonuses(int ItemId)
	{
		int bonuses = Bonuses;
		int freePass = FreePass;
		switch (ItemId)
		{
		case 1600037:
			method_1(8);
			break;
		case 1600011:
			method_3(128);
			break;
		case 1600001:
			method_1(1);
			break;
		case 1600002:
			method_1(2);
			break;
		case 1600003:
			method_1(4);
			break;
		case 1600004:
			method_1(16);
			break;
		case 1600201:
			method_1(512);
			break;
		case 1600202:
			method_1(1024);
			break;
		case 1600203:
			method_1(2048);
			break;
		case 1600204:
			method_1(4096);
			break;
		case 1600119:
			method_1(32);
			break;
		case 1600038:
			method_1(64);
			break;
		}
		if (Bonuses == bonuses)
		{
			return FreePass != freePass;
		}
		return true;
	}

	private void method_0(int int_6)
	{
		Bonuses &= ~int_6;
	}

	private void method_1(int int_6)
	{
		Bonuses |= int_6;
	}

	private void method_2(int int_6)
	{
		FreePass &= ~int_6;
	}

	private void method_3(int int_6)
	{
		FreePass |= int_6;
	}
}
