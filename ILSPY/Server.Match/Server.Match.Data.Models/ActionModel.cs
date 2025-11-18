using System.Runtime.CompilerServices;
using Server.Match.Data.Enums;

namespace Server.Match.Data.Models;

public class ActionModel
{
	[CompilerGenerated]
	private ushort ushort_0;

	[CompilerGenerated]
	private ushort ushort_1;

	[CompilerGenerated]
	private UdpGameEvent udpGameEvent_0;

	[CompilerGenerated]
	private UdpSubHead udpSubHead_0;

	[CompilerGenerated]
	private byte[] byte_0;

	public ushort Slot
	{
		[CompilerGenerated]
		get
		{
			return ushort_0;
		}
		[CompilerGenerated]
		set
		{
			ushort_0 = value;
		}
	}

	public ushort Length
	{
		[CompilerGenerated]
		get
		{
			return ushort_1;
		}
		[CompilerGenerated]
		set
		{
			ushort_1 = value;
		}
	}

	public UdpGameEvent Flag
	{
		[CompilerGenerated]
		get
		{
			return udpGameEvent_0;
		}
		[CompilerGenerated]
		set
		{
			udpGameEvent_0 = value;
		}
	}

	public UdpSubHead SubHead
	{
		[CompilerGenerated]
		get
		{
			return udpSubHead_0;
		}
		[CompilerGenerated]
		set
		{
			udpSubHead_0 = value;
		}
	}

	public byte[] Data
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
}
