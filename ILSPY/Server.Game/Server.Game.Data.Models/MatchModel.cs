using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Models;

public class MatchModel
{
	[CompilerGenerated]
	private ClanModel clanModel_0;

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
	private SlotMatch[] slotMatch_0;

	[CompilerGenerated]
	private MatchState matchState_0;

	public ClanModel Clan
	{
		[CompilerGenerated]
		get
		{
			return clanModel_0;
		}
		[CompilerGenerated]
		set
		{
			clanModel_0 = value;
		}
	}

	public int Training
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

	public int ServerId
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

	public int ChannelId
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

	public int MatchId
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

	public int Leader
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

	public int FriendId
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

	public SlotMatch[] Slots
	{
		[CompilerGenerated]
		get
		{
			return slotMatch_0;
		}
		[CompilerGenerated]
		set
		{
			slotMatch_0 = value;
		}
	}

	public MatchState State
	{
		[CompilerGenerated]
		get
		{
			return matchState_0;
		}
		[CompilerGenerated]
		set
		{
			matchState_0 = value;
		}
	}

	public MatchModel(ClanModel clanModel_1)
	{
		Clan = clanModel_1;
		MatchId = -1;
		Slots = new SlotMatch[9];
		State = MatchState.Ready;
		for (int i = 0; i < 9; i++)
		{
			Slots[i] = new SlotMatch(i);
		}
	}

	public bool GetSlot(int SlotId, out SlotMatch Slot)
	{
		lock (Slots)
		{
			Slot = null;
			if (SlotId >= 0 && SlotId <= 17)
			{
				Slot = Slots[SlotId];
			}
			return Slot != null;
		}
	}

	public SlotMatch GetSlot(int SlotId)
	{
		lock (Slots)
		{
			if (SlotId >= 0 && SlotId <= 17)
			{
				return Slots[SlotId];
			}
			return null;
		}
	}

	public void SetNewLeader(int Leader, int OldLeader)
	{
		lock (Slots)
		{
			if (Leader == -1)
			{
				int num = 0;
				while (true)
				{
					if (num < Training)
					{
						if (num != OldLeader && Slots[num].PlayerId > 0L)
						{
							break;
						}
						num++;
						continue;
					}
					return;
				}
				this.Leader = num;
			}
			else
			{
				this.Leader = Leader;
			}
		}
	}

	public bool AddPlayer(Account Player)
	{
		lock (Slots)
		{
			for (int i = 0; i < Training; i++)
			{
				SlotMatch slotMatch = Slots[i];
				if (slotMatch.PlayerId == 0L && slotMatch.State == SlotMatchState.Empty)
				{
					slotMatch.PlayerId = Player.PlayerId;
					slotMatch.State = SlotMatchState.Normal;
					Player.Match = this;
					Player.MatchSlot = i;
					Player.Status.UpdateClanMatch((byte)FriendId);
					AllUtils.SyncPlayerToClanMembers(Player);
					return true;
				}
			}
		}
		return false;
	}

	public Account GetPlayerBySlot(SlotMatch Slot)
	{
		try
		{
			long playerId = Slot.PlayerId;
			return (playerId > 0L) ? AccountManager.GetAccount(playerId, noUseDB: true) : null;
		}
		catch
		{
			return null;
		}
	}

	public Account GetPlayerBySlot(int SlotId)
	{
		try
		{
			long playerId = Slots[SlotId].PlayerId;
			return (playerId > 0L) ? AccountManager.GetAccount(playerId, noUseDB: true) : null;
		}
		catch
		{
			return null;
		}
	}

	public List<Account> GetAllPlayers(int Exception)
	{
		List<Account> list = new List<Account>();
		lock (Slots)
		{
			for (int i = 0; i < 9; i++)
			{
				long playerId = Slots[i].PlayerId;
				if (playerId > 0L && i != Exception)
				{
					Account account = AccountManager.GetAccount(playerId, noUseDB: true);
					if (account != null)
					{
						list.Add(account);
					}
				}
			}
			return list;
		}
	}

	public List<Account> GetAllPlayers()
	{
		List<Account> list = new List<Account>();
		lock (Slots)
		{
			for (int i = 0; i < 9; i++)
			{
				long playerId = Slots[i].PlayerId;
				if (playerId > 0L)
				{
					Account account = AccountManager.GetAccount(playerId, noUseDB: true);
					if (account != null)
					{
						list.Add(account);
					}
				}
			}
			return list;
		}
	}

	public void SendPacketToPlayers(GameServerPacket Packet)
	{
		List<Account> allPlayers = GetAllPlayers();
		if (allPlayers.Count == 0)
		{
			return;
		}
		byte[] completeBytes = Packet.GetCompleteBytes("Match.SendPacketToPlayers(SendPacket)");
		foreach (Account item in allPlayers)
		{
			item.SendCompletePacket(completeBytes, Packet.GetType().Name);
		}
	}

	public void SendPacketToPlayers(GameServerPacket Packet, int Exception)
	{
		List<Account> allPlayers = GetAllPlayers(Exception);
		if (allPlayers.Count == 0)
		{
			return;
		}
		byte[] completeBytes = Packet.GetCompleteBytes("Match.SendPacketToPlayers(SendPacket,int)");
		foreach (Account item in allPlayers)
		{
			item.SendCompletePacket(completeBytes, Packet.GetType().Name);
		}
	}

	public Account GetLeader()
	{
		try
		{
			return AccountManager.GetAccount(Slots[Leader].PlayerId, noUseDB: true);
		}
		catch
		{
			return null;
		}
	}

	public int GetServerInfo()
	{
		return ChannelId + ServerId * 10;
	}

	public int GetCountPlayers()
	{
		lock (Slots)
		{
			int num = 0;
			SlotMatch[] slots = Slots;
			for (int i = 0; i < slots.Length; i++)
			{
				if (slots[i].PlayerId > 0L)
				{
					num++;
				}
			}
			return num;
		}
	}

	private void method_0(Account account_0)
	{
		lock (Slots)
		{
			if (GetSlot(account_0.MatchSlot, out var Slot) && Slot.PlayerId == account_0.PlayerId)
			{
				Slot.PlayerId = 0L;
				Slot.State = SlotMatchState.Empty;
			}
		}
	}

	public bool RemovePlayer(Account Player)
	{
		ChannelModel channel = ChannelsXML.GetChannel(ServerId, ChannelId);
		if (channel == null)
		{
			return false;
		}
		method_0(Player);
		if (GetCountPlayers() == 0)
		{
			channel.RemoveMatch(MatchId);
		}
		else
		{
			if (Player.MatchSlot == Leader)
			{
				SetNewLeader(-1, -1);
			}
			using PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK packet = new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(this);
			SendPacketToPlayers(packet);
		}
		Player.MatchSlot = -1;
		Player.Match = null;
		return true;
	}
}
