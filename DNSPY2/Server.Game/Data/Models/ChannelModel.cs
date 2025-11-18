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

namespace Server.Game.Data.Models
{
	// Token: 0x02000202 RID: 514
	public class ChannelModel
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00005D1D File Offset: 0x00003F1D
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00005D25 File Offset: 0x00003F25
		public int Id
		{
			[CompilerGenerated]
			get
			{
				return this.int_0;
			}
			[CompilerGenerated]
			set
			{
				this.int_0 = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00005D2E File Offset: 0x00003F2E
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x00005D36 File Offset: 0x00003F36
		public ChannelType Type
		{
			[CompilerGenerated]
			get
			{
				return this.channelType_0;
			}
			[CompilerGenerated]
			set
			{
				this.channelType_0 = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00005D3F File Offset: 0x00003F3F
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x00005D47 File Offset: 0x00003F47
		public int ServerId
		{
			[CompilerGenerated]
			get
			{
				return this.int_1;
			}
			[CompilerGenerated]
			set
			{
				this.int_1 = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00005D50 File Offset: 0x00003F50
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x00005D58 File Offset: 0x00003F58
		public int MaxRooms
		{
			[CompilerGenerated]
			get
			{
				return this.int_2;
			}
			[CompilerGenerated]
			set
			{
				this.int_2 = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00005D61 File Offset: 0x00003F61
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x00005D69 File Offset: 0x00003F69
		public int ExpBonus
		{
			[CompilerGenerated]
			get
			{
				return this.int_3;
			}
			[CompilerGenerated]
			set
			{
				this.int_3 = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00005D72 File Offset: 0x00003F72
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x00005D7A File Offset: 0x00003F7A
		public int GoldBonus
		{
			[CompilerGenerated]
			get
			{
				return this.int_4;
			}
			[CompilerGenerated]
			set
			{
				this.int_4 = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00005D83 File Offset: 0x00003F83
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x00005D8B File Offset: 0x00003F8B
		public int CashBonus
		{
			[CompilerGenerated]
			get
			{
				return this.int_5;
			}
			[CompilerGenerated]
			set
			{
				this.int_5 = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00005D94 File Offset: 0x00003F94
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x00005D9C File Offset: 0x00003F9C
		public string Password
		{
			[CompilerGenerated]
			get
			{
				return this.string_0;
			}
			[CompilerGenerated]
			set
			{
				this.string_0 = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00005DA5 File Offset: 0x00003FA5
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00005DAD File Offset: 0x00003FAD
		public List<PlayerSession> Players
		{
			[CompilerGenerated]
			get
			{
				return this.list_0;
			}
			[CompilerGenerated]
			set
			{
				this.list_0 = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00005DB6 File Offset: 0x00003FB6
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00005DBE File Offset: 0x00003FBE
		public List<RoomModel> Rooms
		{
			[CompilerGenerated]
			get
			{
				return this.list_1;
			}
			[CompilerGenerated]
			set
			{
				this.list_1 = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00005DC7 File Offset: 0x00003FC7
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x00005DCF File Offset: 0x00003FCF
		public List<MatchModel> Matches
		{
			[CompilerGenerated]
			get
			{
				return this.list_2;
			}
			[CompilerGenerated]
			set
			{
				this.list_2 = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00005DD8 File Offset: 0x00003FD8
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x00005DE0 File Offset: 0x00003FE0
		private DateTime DateTime_0
		{
			[CompilerGenerated]
			get
			{
				return this.dateTime_0;
			}
			[CompilerGenerated]
			set
			{
				this.dateTime_0 = value;
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00005DE9 File Offset: 0x00003FE9
		public ChannelModel(int int_6)
		{
			this.ServerId = int_6;
			this.Players = new List<PlayerSession>();
			this.Rooms = new List<RoomModel>();
			this.Matches = new List<MatchModel>();
			this.DateTime_0 = DateTimeUtil.Now();
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00031870 File Offset: 0x0002FA70
		public PlayerSession GetPlayer(int session)
		{
			List<PlayerSession> players = this.Players;
			PlayerSession playerSession2;
			lock (players)
			{
				foreach (PlayerSession playerSession in this.Players)
				{
					if (playerSession.SessionId == session)
					{
						return playerSession;
					}
				}
				playerSession2 = null;
			}
			return playerSession2;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000318FC File Offset: 0x0002FAFC
		public PlayerSession GetPlayer(int SessionId, out int Index)
		{
			Index = -1;
			List<PlayerSession> players = this.Players;
			PlayerSession playerSession2;
			lock (players)
			{
				for (int i = 0; i < this.Players.Count; i++)
				{
					PlayerSession playerSession = this.Players[i];
					if (playerSession.SessionId == SessionId)
					{
						Index = i;
						return playerSession;
					}
				}
				playerSession2 = null;
			}
			return playerSession2;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00031974 File Offset: 0x0002FB74
		public bool AddPlayer(PlayerSession pS)
		{
			List<PlayerSession> players = this.Players;
			bool flag2;
			lock (players)
			{
				if (!this.Players.Contains(pS))
				{
					this.Players.Add(pS);
					UpdateServer.RefreshSChannel(this.ServerId);
					UpdateChannel.RefreshChannel(this.ServerId, this.Id, this.Players.Count);
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000319F8 File Offset: 0x0002FBF8
		public void RemoveMatch(int matchId)
		{
			List<MatchModel> matches = this.Matches;
			lock (matches)
			{
				for (int i = 0; i < this.Matches.Count; i++)
				{
					if (matchId == this.Matches[i].MatchId)
					{
						this.Matches.RemoveAt(i);
						break;
					}
				}
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00031A6C File Offset: 0x0002FC6C
		public void AddMatch(MatchModel match)
		{
			List<MatchModel> matches = this.Matches;
			lock (matches)
			{
				if (!this.Matches.Contains(match))
				{
					this.Matches.Add(match);
				}
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00031AC0 File Offset: 0x0002FCC0
		public void AddRoom(RoomModel room)
		{
			List<RoomModel> rooms = this.Rooms;
			lock (rooms)
			{
				this.Rooms.Add(room);
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00031B08 File Offset: 0x0002FD08
		public void RemoveEmptyRooms()
		{
			List<RoomModel> rooms = this.Rooms;
			lock (rooms)
			{
				if (ComDiv.GetDuration(this.DateTime_0) >= (double)ConfigLoader.EmptyRoomRemovalInterval)
				{
					this.DateTime_0 = DateTimeUtil.Now();
					for (int i = 0; i < this.Rooms.Count; i++)
					{
						if (this.Rooms[i].GetCountPlayers() < 1)
						{
							this.Rooms.RemoveAt(i--);
						}
					}
				}
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00031B9C File Offset: 0x0002FD9C
		public MatchModel GetMatch(int id)
		{
			List<MatchModel> matches = this.Matches;
			MatchModel matchModel2;
			lock (matches)
			{
				foreach (MatchModel matchModel in this.Matches)
				{
					if (matchModel.MatchId == id)
					{
						return matchModel;
					}
				}
				matchModel2 = null;
			}
			return matchModel2;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00031C28 File Offset: 0x0002FE28
		public MatchModel GetMatch(int id, int clan)
		{
			List<MatchModel> matches = this.Matches;
			MatchModel matchModel2;
			lock (matches)
			{
				foreach (MatchModel matchModel in this.Matches)
				{
					if (matchModel.FriendId == id && matchModel.Clan.Id == clan)
					{
						return matchModel;
					}
				}
				matchModel2 = null;
			}
			return matchModel2;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00031CC4 File Offset: 0x0002FEC4
		public RoomModel GetRoom(int id)
		{
			List<RoomModel> rooms = this.Rooms;
			RoomModel roomModel2;
			lock (rooms)
			{
				foreach (RoomModel roomModel in this.Rooms)
				{
					if (roomModel.RoomId == id)
					{
						return roomModel;
					}
				}
				roomModel2 = null;
			}
			return roomModel2;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00031D50 File Offset: 0x0002FF50
		public List<Account> GetWaitPlayers()
		{
			List<Account> list = new List<Account>();
			List<PlayerSession> players = this.Players;
			lock (players)
			{
				foreach (PlayerSession playerSession in this.Players)
				{
					Account account = AccountManager.GetAccount(playerSession.PlayerId, true);
					if (account != null && account.Room == null && !string.IsNullOrEmpty(account.Nickname))
					{
						list.Add(account);
					}
				}
			}
			return list;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00031DFC File Offset: 0x0002FFFC
		public void SendPacketToWaitPlayers(GameServerPacket Packet)
		{
			List<Account> waitPlayers = this.GetWaitPlayers();
			if (waitPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Channel.SendPacketToWaitPlayers");
			foreach (Account account in waitPlayers)
			{
				account.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00031E70 File Offset: 0x00030070
		public bool RemovePlayer(Account Player)
		{
			bool flag = false;
			Player.ChannelId = -1;
			Player.ServerId = -1;
			if (Player.Session != null)
			{
				List<PlayerSession> players = this.Players;
				lock (players)
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

		// Token: 0x040003DC RID: 988
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040003DD RID: 989
		[CompilerGenerated]
		private ChannelType channelType_0;

		// Token: 0x040003DE RID: 990
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040003DF RID: 991
		[CompilerGenerated]
		private int int_2;

		// Token: 0x040003E0 RID: 992
		[CompilerGenerated]
		private int int_3;

		// Token: 0x040003E1 RID: 993
		[CompilerGenerated]
		private int int_4;

		// Token: 0x040003E2 RID: 994
		[CompilerGenerated]
		private int int_5;

		// Token: 0x040003E3 RID: 995
		[CompilerGenerated]
		private string string_0;

		// Token: 0x040003E4 RID: 996
		[CompilerGenerated]
		private List<PlayerSession> list_0;

		// Token: 0x040003E5 RID: 997
		[CompilerGenerated]
		private List<RoomModel> list_1;

		// Token: 0x040003E6 RID: 998
		[CompilerGenerated]
		private List<MatchModel> list_2;

		// Token: 0x040003E7 RID: 999
		[CompilerGenerated]
		private DateTime dateTime_0;
	}
}
