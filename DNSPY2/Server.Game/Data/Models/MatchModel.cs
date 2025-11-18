using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Models
{
	// Token: 0x02000203 RID: 515
	public class MatchModel
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00005E24 File Offset: 0x00004024
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x00005E2C File Offset: 0x0000402C
		public ClanModel Clan
		{
			[CompilerGenerated]
			get
			{
				return this.clanModel_0;
			}
			[CompilerGenerated]
			set
			{
				this.clanModel_0 = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00005E35 File Offset: 0x00004035
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x00005E3D File Offset: 0x0000403D
		public int Training
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00005E46 File Offset: 0x00004046
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00005E4E File Offset: 0x0000404E
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00005E57 File Offset: 0x00004057
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00005E5F File Offset: 0x0000405F
		public int ChannelId
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00005E68 File Offset: 0x00004068
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x00005E70 File Offset: 0x00004070
		public int MatchId
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00005E79 File Offset: 0x00004079
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x00005E81 File Offset: 0x00004081
		public int Leader
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00005E8A File Offset: 0x0000408A
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00005E92 File Offset: 0x00004092
		public int FriendId
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00005E9B File Offset: 0x0000409B
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x00005EA3 File Offset: 0x000040A3
		public SlotMatch[] Slots
		{
			[CompilerGenerated]
			get
			{
				return this.slotMatch_0;
			}
			[CompilerGenerated]
			set
			{
				this.slotMatch_0 = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00005EAC File Offset: 0x000040AC
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x00005EB4 File Offset: 0x000040B4
		public MatchState State
		{
			[CompilerGenerated]
			get
			{
				return this.matchState_0;
			}
			[CompilerGenerated]
			set
			{
				this.matchState_0 = value;
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00031F00 File Offset: 0x00030100
		public MatchModel(ClanModel clanModel_1)
		{
			this.Clan = clanModel_1;
			this.MatchId = -1;
			this.Slots = new SlotMatch[9];
			this.State = MatchState.Ready;
			for (int i = 0; i < 9; i++)
			{
				this.Slots[i] = new SlotMatch(i);
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00031F50 File Offset: 0x00030150
		public bool GetSlot(int SlotId, out SlotMatch Slot)
		{
			SlotMatch[] slots = this.Slots;
			bool flag2;
			lock (slots)
			{
				Slot = null;
				if (SlotId >= 0 && SlotId <= 17)
				{
					Slot = this.Slots[SlotId];
				}
				flag2 = Slot != null;
			}
			return flag2;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00031FA8 File Offset: 0x000301A8
		public SlotMatch GetSlot(int SlotId)
		{
			SlotMatch[] slots = this.Slots;
			SlotMatch slotMatch;
			lock (slots)
			{
				if (SlotId >= 0 && SlotId <= 17)
				{
					slotMatch = this.Slots[SlotId];
				}
				else
				{
					slotMatch = null;
				}
			}
			return slotMatch;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00031FFC File Offset: 0x000301FC
		public void SetNewLeader(int Leader, int OldLeader)
		{
			SlotMatch[] slots = this.Slots;
			lock (slots)
			{
				if (Leader == -1)
				{
					for (int i = 0; i < this.Training; i++)
					{
						if (i != OldLeader && this.Slots[i].PlayerId > 0L)
						{
							this.Leader = i;
							break;
						}
					}
				}
				else
				{
					this.Leader = Leader;
				}
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0003207C File Offset: 0x0003027C
		public bool AddPlayer(Account Player)
		{
			SlotMatch[] slots = this.Slots;
			lock (slots)
			{
				for (int i = 0; i < this.Training; i++)
				{
					SlotMatch slotMatch = this.Slots[i];
					if (slotMatch.PlayerId == 0L && slotMatch.State == SlotMatchState.Empty)
					{
						slotMatch.PlayerId = Player.PlayerId;
						slotMatch.State = SlotMatchState.Normal;
						Player.Match = this;
						Player.MatchSlot = i;
						Player.Status.UpdateClanMatch((byte)this.FriendId);
						AllUtils.SyncPlayerToClanMembers(Player);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00032124 File Offset: 0x00030324
		public Account GetPlayerBySlot(SlotMatch Slot)
		{
			Account account;
			try
			{
				long playerId = Slot.PlayerId;
				account = ((playerId > 0L) ? AccountManager.GetAccount(playerId, true) : null);
			}
			catch
			{
				account = null;
			}
			return account;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00032168 File Offset: 0x00030368
		public Account GetPlayerBySlot(int SlotId)
		{
			Account account;
			try
			{
				long playerId = this.Slots[SlotId].PlayerId;
				account = ((playerId > 0L) ? AccountManager.GetAccount(playerId, true) : null);
			}
			catch
			{
				account = null;
			}
			return account;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000321B4 File Offset: 0x000303B4
		public List<Account> GetAllPlayers(int Exception)
		{
			List<Account> list = new List<Account>();
			SlotMatch[] slots = this.Slots;
			lock (slots)
			{
				for (int i = 0; i < 9; i++)
				{
					long playerId = this.Slots[i].PlayerId;
					if (playerId > 0L && i != Exception)
					{
						Account account = AccountManager.GetAccount(playerId, true);
						if (account != null)
						{
							list.Add(account);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00032238 File Offset: 0x00030438
		public List<Account> GetAllPlayers()
		{
			List<Account> list = new List<Account>();
			SlotMatch[] slots = this.Slots;
			lock (slots)
			{
				for (int i = 0; i < 9; i++)
				{
					long playerId = this.Slots[i].PlayerId;
					if (playerId > 0L)
					{
						Account account = AccountManager.GetAccount(playerId, true);
						if (account != null)
						{
							list.Add(account);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000322B8 File Offset: 0x000304B8
		public void SendPacketToPlayers(GameServerPacket Packet)
		{
			List<Account> allPlayers = this.GetAllPlayers();
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Match.SendPacketToPlayers(SendPacket)");
			foreach (Account account in allPlayers)
			{
				account.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0003232C File Offset: 0x0003052C
		public void SendPacketToPlayers(GameServerPacket Packet, int Exception)
		{
			List<Account> allPlayers = this.GetAllPlayers(Exception);
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Match.SendPacketToPlayers(SendPacket,int)");
			foreach (Account account in allPlayers)
			{
				account.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000323A4 File Offset: 0x000305A4
		public Account GetLeader()
		{
			Account account;
			try
			{
				account = AccountManager.GetAccount(this.Slots[this.Leader].PlayerId, true);
			}
			catch
			{
				account = null;
			}
			return account;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00005EBD File Offset: 0x000040BD
		public int GetServerInfo()
		{
			return this.ChannelId + this.ServerId * 10;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000323E4 File Offset: 0x000305E4
		public int GetCountPlayers()
		{
			SlotMatch[] slots = this.Slots;
			int i;
			lock (slots)
			{
				int num = 0;
				SlotMatch[] slots2 = this.Slots;
				for (i = 0; i < slots2.Length; i++)
				{
					if (slots2[i].PlayerId > 0L)
					{
						num++;
					}
				}
				i = num;
			}
			return i;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00032458 File Offset: 0x00030658
		private void method_0(Account account_0)
		{
			SlotMatch[] slots = this.Slots;
			lock (slots)
			{
				SlotMatch slotMatch;
				if (this.GetSlot(account_0.MatchSlot, out slotMatch))
				{
					if (slotMatch.PlayerId == account_0.PlayerId)
					{
						slotMatch.PlayerId = 0L;
						slotMatch.State = SlotMatchState.Empty;
					}
				}
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000324C8 File Offset: 0x000306C8
		public bool RemovePlayer(Account Player)
		{
			ChannelModel channel = ChannelsXML.GetChannel(this.ServerId, this.ChannelId);
			if (channel == null)
			{
				return false;
			}
			this.method_0(Player);
			if (this.GetCountPlayers() == 0)
			{
				channel.RemoveMatch(this.MatchId);
			}
			else
			{
				if (Player.MatchSlot == this.Leader)
				{
					this.SetNewLeader(-1, -1);
				}
				using (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK protocol_CLAN_WAR_REGIST_MERCENARY_ACK = new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(this))
				{
					this.SendPacketToPlayers(protocol_CLAN_WAR_REGIST_MERCENARY_ACK);
				}
			}
			Player.MatchSlot = -1;
			Player.Match = null;
			return true;
		}

		// Token: 0x040003E8 RID: 1000
		[CompilerGenerated]
		private ClanModel clanModel_0;

		// Token: 0x040003E9 RID: 1001
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040003EA RID: 1002
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040003EB RID: 1003
		[CompilerGenerated]
		private int int_2;

		// Token: 0x040003EC RID: 1004
		[CompilerGenerated]
		private int int_3;

		// Token: 0x040003ED RID: 1005
		[CompilerGenerated]
		private int int_4;

		// Token: 0x040003EE RID: 1006
		[CompilerGenerated]
		private int int_5;

		// Token: 0x040003EF RID: 1007
		[CompilerGenerated]
		private SlotMatch[] slotMatch_0;

		// Token: 0x040003F0 RID: 1008
		[CompilerGenerated]
		private MatchState matchState_0;
	}
}
