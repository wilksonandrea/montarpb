using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Sync.Server;
using Server.Game.Network;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Server.Game.Data.Models
{
	public class ChannelModel
	{
		public int CashBonus
		{
			get;
			set;
		}

		private DateTime DateTime_0
		{
			get;
			set;
		}

		public int ExpBonus
		{
			get;
			set;
		}

		public int GoldBonus
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public List<MatchModel> Matches
		{
			get;
			set;
		}

		public int MaxRooms
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}

		public List<PlayerSession> Players
		{
			get;
			set;
		}

		public List<RoomModel> Rooms
		{
			get;
			set;
		}

		public int ServerId
		{
			get;
			set;
		}

		public ChannelType Type
		{
			get;
			set;
		}

		public ChannelModel(int int_6)
		{
			this.ServerId = int_6;
			this.Players = new List<PlayerSession>();
			this.Rooms = new List<RoomModel>();
			this.Matches = new List<MatchModel>();
			this.DateTime_0 = DateTimeUtil.Now();
		}

		public void AddMatch(MatchModel match)
		{
			lock (this.Matches)
			{
				if (!this.Matches.Contains(match))
				{
					this.Matches.Add(match);
				}
			}
		}

		public bool AddPlayer(PlayerSession pS)
		{
			bool flag;
			lock (this.Players)
			{
				if (this.Players.Contains(pS))
				{
					flag = false;
				}
				else
				{
					this.Players.Add(pS);
					UpdateServer.RefreshSChannel(this.ServerId);
					UpdateChannel.RefreshChannel(this.ServerId, this.Id, this.Players.Count);
					flag = true;
				}
			}
			return flag;
		}

		public void AddRoom(RoomModel room)
		{
			lock (this.Rooms)
			{
				this.Rooms.Add(room);
			}
		}

		public MatchModel GetMatch(int id)
		{
			MatchModel matchModel;
			lock (this.Matches)
			{
				foreach (MatchModel match in this.Matches)
				{
					if (match.MatchId != id)
					{
						continue;
					}
					matchModel = match;
					return matchModel;
				}
				matchModel = null;
			}
			return matchModel;
		}

		public MatchModel GetMatch(int id, int clan)
		{
			MatchModel matchModel;
			lock (this.Matches)
			{
				foreach (MatchModel match in this.Matches)
				{
					if (match.FriendId != id || match.Clan.Id != clan)
					{
						continue;
					}
					matchModel = match;
					return matchModel;
				}
				matchModel = null;
			}
			return matchModel;
		}

		public PlayerSession GetPlayer(int session)
		{
			PlayerSession playerSession;
			lock (this.Players)
			{
				foreach (PlayerSession player in this.Players)
				{
					if (player.SessionId != session)
					{
						continue;
					}
					playerSession = player;
					return playerSession;
				}
				playerSession = null;
			}
			return playerSession;
		}

		public PlayerSession GetPlayer(int SessionId, out int Index)
		{
			PlayerSession playerSession;
			Index = -1;
			lock (this.Players)
			{
				int ınt32 = 0;
				while (ınt32 < this.Players.Count)
				{
					PlayerSession ıtem = this.Players[ınt32];
					if (ıtem.SessionId != SessionId)
					{
						ınt32++;
					}
					else
					{
						Index = ınt32;
						playerSession = ıtem;
						return playerSession;
					}
				}
				playerSession = null;
			}
			return playerSession;
		}

		public RoomModel GetRoom(int id)
		{
			RoomModel roomModel;
			lock (this.Rooms)
			{
				foreach (RoomModel room in this.Rooms)
				{
					if (room.RoomId != id)
					{
						continue;
					}
					roomModel = room;
					return roomModel;
				}
				roomModel = null;
			}
			return roomModel;
		}

		public List<Account> GetWaitPlayers()
		{
			List<Account> accounts = new List<Account>();
			lock (this.Players)
			{
				foreach (PlayerSession player in this.Players)
				{
					Account account = AccountManager.GetAccount(player.PlayerId, true);
					if (account == null || account.Room != null || string.IsNullOrEmpty(account.Nickname))
					{
						continue;
					}
					accounts.Add(account);
				}
			}
			return accounts;
		}

		public void RemoveEmptyRooms()
		{
			lock (this.Rooms)
			{
				if (ComDiv.GetDuration(this.DateTime_0) >= (double)ConfigLoader.EmptyRoomRemovalInterval)
				{
					this.DateTime_0 = DateTimeUtil.Now();
					for (int i = 0; i < this.Rooms.Count; i++)
					{
						if (this.Rooms[i].GetCountPlayers() < 1)
						{
							int ınt32 = i;
							i = ınt32 - 1;
							this.Rooms.RemoveAt(ınt32);
						}
					}
				}
			}
		}

		public void RemoveMatch(int matchId)
		{
			lock (this.Matches)
			{
				int ınt32 = 0;
				while (ınt32 < this.Matches.Count)
				{
					if (matchId != this.Matches[ınt32].MatchId)
					{
						ınt32++;
					}
					else
					{
						this.Matches.RemoveAt(ınt32);
						return;
					}
				}
			}
		}

		public bool RemovePlayer(Account Player)
		{
			bool flag = false;
			Player.ChannelId = -1;
			Player.ServerId = -1;
			if (Player.Session != null)
			{
				lock (this.Players)
				{
					flag = this.Players.Remove(Player.Session);
				}
				UpdateChannel.RefreshChannel(this.ServerId, this.Id, this.Players.Count);
				if (flag)
				{
					UpdateServer.RefreshSChannel(this.ServerId);
				}
			}
			return flag;
		}

		public void SendPacketToWaitPlayers(GameServerPacket Packet)
		{
			List<Account> waitPlayers = this.GetWaitPlayers();
			if (waitPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Channel.SendPacketToWaitPlayers");
			foreach (Account waitPlayer in waitPlayers)
			{
				waitPlayer.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}
	}
}