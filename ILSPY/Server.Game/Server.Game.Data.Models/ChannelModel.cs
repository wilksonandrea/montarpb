using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Sync.Server;
using Server.Game.Network;

namespace Server.Game.Data.Models;

public class ChannelModel
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private ChannelType channelType_0;

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

	[CompilerGenerated]
	private List<PlayerSession> list_0;

	[CompilerGenerated]
	private List<RoomModel> list_1;

	[CompilerGenerated]
	private List<MatchModel> list_2;

	[CompilerGenerated]
	private DateTime dateTime_0;

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

	public ChannelType Type
	{
		[CompilerGenerated]
		get
		{
			return channelType_0;
		}
		[CompilerGenerated]
		set
		{
			channelType_0 = value;
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

	public int MaxRooms
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

	public int ExpBonus
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

	public int GoldBonus
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

	public int CashBonus
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

	public string Password
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

	public List<PlayerSession> Players
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

	public List<RoomModel> Rooms
	{
		[CompilerGenerated]
		get
		{
			return list_1;
		}
		[CompilerGenerated]
		set
		{
			list_1 = value;
		}
	}

	public List<MatchModel> Matches
	{
		[CompilerGenerated]
		get
		{
			return list_2;
		}
		[CompilerGenerated]
		set
		{
			list_2 = value;
		}
	}

	private DateTime DateTime_0
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

	public ChannelModel(int int_6)
	{
		ServerId = int_6;
		Players = new List<PlayerSession>();
		Rooms = new List<RoomModel>();
		Matches = new List<MatchModel>();
		DateTime_0 = DateTimeUtil.Now();
	}

	public PlayerSession GetPlayer(int session)
	{
		lock (Players)
		{
			foreach (PlayerSession player in Players)
			{
				if (player.SessionId == session)
				{
					return player;
				}
			}
			return null;
		}
	}

	public PlayerSession GetPlayer(int SessionId, out int Index)
	{
		Index = -1;
		lock (Players)
		{
			int num = 0;
			PlayerSession playerSession;
			while (true)
			{
				if (num < Players.Count)
				{
					playerSession = Players[num];
					if (playerSession.SessionId == SessionId)
					{
						break;
					}
					num++;
					continue;
				}
				return null;
			}
			Index = num;
			return playerSession;
		}
	}

	public bool AddPlayer(PlayerSession pS)
	{
		lock (Players)
		{
			if (!Players.Contains(pS))
			{
				Players.Add(pS);
				UpdateServer.RefreshSChannel(ServerId);
				UpdateChannel.RefreshChannel(ServerId, Id, Players.Count);
				return true;
			}
			return false;
		}
	}

	public void RemoveMatch(int matchId)
	{
		lock (Matches)
		{
			int num = 0;
			while (true)
			{
				if (num < Matches.Count)
				{
					if (matchId == Matches[num].MatchId)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			Matches.RemoveAt(num);
		}
	}

	public void AddMatch(MatchModel match)
	{
		lock (Matches)
		{
			if (!Matches.Contains(match))
			{
				Matches.Add(match);
			}
		}
	}

	public void AddRoom(RoomModel room)
	{
		lock (Rooms)
		{
			Rooms.Add(room);
		}
	}

	public void RemoveEmptyRooms()
	{
		lock (Rooms)
		{
			if (!(ComDiv.GetDuration(DateTime_0) >= (double)ConfigLoader.EmptyRoomRemovalInterval))
			{
				return;
			}
			DateTime_0 = DateTimeUtil.Now();
			for (int i = 0; i < Rooms.Count; i++)
			{
				if (Rooms[i].GetCountPlayers() < 1)
				{
					Rooms.RemoveAt(i--);
				}
			}
		}
	}

	public MatchModel GetMatch(int id)
	{
		lock (Matches)
		{
			foreach (MatchModel match in Matches)
			{
				if (match.MatchId == id)
				{
					return match;
				}
			}
			return null;
		}
	}

	public MatchModel GetMatch(int id, int clan)
	{
		lock (Matches)
		{
			foreach (MatchModel match in Matches)
			{
				if (match.FriendId == id && match.Clan.Id == clan)
				{
					return match;
				}
			}
			return null;
		}
	}

	public RoomModel GetRoom(int id)
	{
		lock (Rooms)
		{
			foreach (RoomModel room in Rooms)
			{
				if (room.RoomId == id)
				{
					return room;
				}
			}
			return null;
		}
	}

	public List<Account> GetWaitPlayers()
	{
		List<Account> list = new List<Account>();
		lock (Players)
		{
			foreach (PlayerSession player in Players)
			{
				Account account = AccountManager.GetAccount(player.PlayerId, noUseDB: true);
				if (account != null && account.Room == null && !string.IsNullOrEmpty(account.Nickname))
				{
					list.Add(account);
				}
			}
			return list;
		}
	}

	public void SendPacketToWaitPlayers(GameServerPacket Packet)
	{
		List<Account> waitPlayers = GetWaitPlayers();
		if (waitPlayers.Count == 0)
		{
			return;
		}
		byte[] completeBytes = Packet.GetCompleteBytes("Channel.SendPacketToWaitPlayers");
		foreach (Account item in waitPlayers)
		{
			item.SendCompletePacket(completeBytes, Packet.GetType().Name);
		}
	}

	public bool RemovePlayer(Account Player)
	{
		bool flag = false;
		Player.ChannelId = -1;
		Player.ServerId = -1;
		if (Player.Session != null)
		{
			lock (Players)
			{
				flag = Players.Remove(Player.Session);
			}
			UpdateChannel.RefreshChannel(ServerId, Id, Players.Count);
			if (flag)
			{
				UpdateServer.RefreshSChannel(ServerId);
			}
		}
		return flag;
	}
}
