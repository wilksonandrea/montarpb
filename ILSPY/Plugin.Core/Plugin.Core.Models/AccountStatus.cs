using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Plugin.Core.Models;

public class AccountStatus
{
	[CompilerGenerated]
	private long long_0;

	[CompilerGenerated]
	private byte byte_0;

	[CompilerGenerated]
	private byte byte_1;

	[CompilerGenerated]
	private byte byte_2;

	[CompilerGenerated]
	private byte byte_3;

	[CompilerGenerated]
	private byte[] byte_4;

	public long PlayerId
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

	public byte ChannelId
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

	public byte RoomId
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

	public byte ClanMatchId
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

	public byte ServerId
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

	public byte[] Buffer
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

	public AccountStatus()
	{
		Buffer = new byte[4];
	}

	public void ResetData(long PlayerId)
	{
		if (PlayerId != 0L)
		{
			byte channelId = ChannelId;
			int roomId = RoomId;
			int clanMatchId = ClanMatchId;
			int serverId = ServerId;
			SetData(uint.MaxValue, PlayerId);
			if (channelId != ChannelId || roomId != RoomId || clanMatchId != ClanMatchId || serverId != ServerId)
			{
				ComDiv.UpdateDB("accounts", "status", 4294967295L, "player_id", PlayerId);
			}
		}
	}

	public void SetData(uint Data, long PlayerId)
	{
		SetData(BitConverter.GetBytes(Data), PlayerId);
	}

	public void SetData(byte[] Buffer, long PlayerId)
	{
		this.PlayerId = PlayerId;
		this.Buffer = Buffer;
		ChannelId = Buffer[0];
		RoomId = Buffer[1];
		ServerId = Buffer[2];
		ClanMatchId = Buffer[3];
	}

	public void UpdateChannel(byte ChannelId)
	{
		this.ChannelId = ChannelId;
		Buffer[0] = ChannelId;
		method_0();
	}

	public void UpdateRoom(byte RoomId)
	{
		this.RoomId = RoomId;
		Buffer[1] = RoomId;
		method_0();
	}

	public void UpdateServer(byte ServerId)
	{
		this.ServerId = ServerId;
		Buffer[2] = ServerId;
		method_0();
	}

	public void UpdateClanMatch(byte ClanMatchId)
	{
		this.ClanMatchId = ClanMatchId;
		Buffer[3] = ClanMatchId;
		method_0();
	}

	private void method_0()
	{
		uint num = BitConverter.ToUInt32(Buffer, 0);
		ComDiv.UpdateDB("accounts", "status", (long)num, "player_id", PlayerId);
	}
}
