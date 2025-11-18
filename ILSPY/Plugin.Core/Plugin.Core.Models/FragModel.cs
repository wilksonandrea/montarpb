using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models;

public class FragModel
{
	[CompilerGenerated]
	private byte byte_0;

	[CompilerGenerated]
	private byte byte_1;

	[CompilerGenerated]
	private byte byte_2;

	[CompilerGenerated]
	private KillingMessage killingMessage_0;

	[CompilerGenerated]
	private float float_0;

	[CompilerGenerated]
	private float float_1;

	[CompilerGenerated]
	private float float_2;

	[CompilerGenerated]
	private byte byte_3;

	[CompilerGenerated]
	private byte byte_4;

	[CompilerGenerated]
	private byte[] byte_5;

	public byte WeaponClass
	{
		[CompilerGenerated]
		get
		{
			return byte_0;
		}
		[CompilerGenerated]
		set
		{
			byte_0 = value;
		}
	}

	public byte HitspotInfo
	{
		[CompilerGenerated]
		get
		{
			return byte_1;
		}
		[CompilerGenerated]
		set
		{
			byte_1 = value;
		}
	}

	public byte Unk
	{
		[CompilerGenerated]
		get
		{
			return byte_2;
		}
		[CompilerGenerated]
		set
		{
			byte_2 = value;
		}
	}

	public KillingMessage KillFlag
	{
		[CompilerGenerated]
		get
		{
			return killingMessage_0;
		}
		[CompilerGenerated]
		set
		{
			killingMessage_0 = value;
		}
	}

	public float X
	{
		[CompilerGenerated]
		get
		{
			return float_0;
		}
		[CompilerGenerated]
		set
		{
			float_0 = value;
		}
	}

	public float Y
	{
		[CompilerGenerated]
		get
		{
			return float_1;
		}
		[CompilerGenerated]
		set
		{
			float_1 = value;
		}
	}

	public float Z
	{
		[CompilerGenerated]
		get
		{
			return float_2;
		}
		[CompilerGenerated]
		set
		{
			float_2 = value;
		}
	}

	public byte VictimSlot
	{
		[CompilerGenerated]
		get
		{
			return byte_3;
		}
		[CompilerGenerated]
		set
		{
			byte_3 = value;
		}
	}

	public byte AssistSlot
	{
		[CompilerGenerated]
		get
		{
			return byte_4;
		}
		[CompilerGenerated]
		set
		{
			byte_4 = value;
		}
	}

	public byte[] Unks
	{
		[CompilerGenerated]
		get
		{
			return byte_5;
		}
		[CompilerGenerated]
		set
		{
			byte_5 = value;
		}
	}
}
