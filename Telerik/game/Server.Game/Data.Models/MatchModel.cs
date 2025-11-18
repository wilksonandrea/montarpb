using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Server.Game.Data.Models
{
	public class MatchModel
	{
		public int ChannelId
		{
			get;
			set;
		}

		public ClanModel Clan
		{
			get;
			set;
		}

		public int FriendId
		{
			get;
			set;
		}

		public int Leader
		{
			get;
			set;
		}

		public int MatchId
		{
			get;
			set;
		}

		public int ServerId
		{
			get;
			set;
		}

		public SlotMatch[] Slots
		{
			get;
			set;
		}

		public MatchState State
		{
			get;
			set;
		}

		public int Training
		{
			get;
			set;
		}

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

		public bool AddPlayer(Account Player)
		{
			bool flag;
			lock (this.Slots)
			{
				int ınt32 = 0;
				while (ınt32 < this.Training)
				{
					SlotMatch slots = this.Slots[ınt32];
					if (slots.PlayerId != 0 || slots.State != SlotMatchState.Empty)
					{
						ınt32++;
					}
					else
					{
						slots.PlayerId = Player.PlayerId;
						slots.State = SlotMatchState.Normal;
						Player.Match = this;
						Player.MatchSlot = ınt32;
						Player.Status.UpdateClanMatch((byte)this.FriendId);
						AllUtils.SyncPlayerToClanMembers(Player);
						flag = true;
						return flag;
					}
				}
				return false;
			}
			return flag;
		}

		public List<Account> GetAllPlayers(int Exception)
		{
			List<Account> accounts = new List<Account>();
			lock (this.Slots)
			{
				for (int i = 0; i < 9; i++)
				{
					long playerId = this.Slots[i].PlayerId;
					if (playerId > 0L && i != Exception)
					{
						Account account = AccountManager.GetAccount(playerId, true);
						if (account != null)
						{
							accounts.Add(account);
						}
					}
				}
			}
			return accounts;
		}

		public List<Account> GetAllPlayers()
		{
			List<Account> accounts = new List<Account>();
			lock (this.Slots)
			{
				for (int i = 0; i < 9; i++)
				{
					long playerId = this.Slots[i].PlayerId;
					if (playerId > 0L)
					{
						Account account = AccountManager.GetAccount(playerId, true);
						if (account != null)
						{
							accounts.Add(account);
						}
					}
				}
			}
			return accounts;
		}

		public int GetCountPlayers()
		{
			int i;
			lock (this.Slots)
			{
				int ınt32 = 0;
				SlotMatch[] slots = this.Slots;
				for (i = 0; i < (int)slots.Length; i++)
				{
					if (slots[i].PlayerId > 0L)
					{
						ınt32++;
					}
				}
				i = ınt32;
			}
			return i;
		}

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

		public Account GetPlayerBySlot(SlotMatch Slot)
		{
			Account account;
			Account account1;
			try
			{
				long playerId = Slot.PlayerId;
				if (playerId > 0L)
				{
					account1 = AccountManager.GetAccount(playerId, true);
				}
				else
				{
					account1 = null;
				}
				account = account1;
			}
			catch
			{
				account = null;
			}
			return account;
		}

		public Account GetPlayerBySlot(int SlotId)
		{
			Account account;
			Account account1;
			try
			{
				long playerId = this.Slots[SlotId].PlayerId;
				if (playerId > 0L)
				{
					account1 = AccountManager.GetAccount(playerId, true);
				}
				else
				{
					account1 = null;
				}
				account = account1;
			}
			catch
			{
				account = null;
			}
			return account;
		}

		public int GetServerInfo()
		{
			return this.ChannelId + this.ServerId * 10;
		}

		public bool GetSlot(int SlotId, out SlotMatch Slot)
		{
			bool slot;
			lock (this.Slots)
			{
				Slot = null;
				if (SlotId >= 0 && SlotId <= 17)
				{
					Slot = this.Slots[SlotId];
				}
				slot = Slot != null;
			}
			return slot;
		}

		public SlotMatch GetSlot(int SlotId)
		{
			SlotMatch slots;
			lock (this.Slots)
			{
				if (SlotId < 0 || SlotId > 17)
				{
					slots = null;
				}
				else
				{
					slots = this.Slots[SlotId];
				}
			}
			return slots;
		}

		private void method_0(Account account_0)
		{
			SlotMatch slotMatch;
			lock (this.Slots)
			{
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

		public bool RemovePlayer(Account Player)
		{
			ChannelModel channel = ChannelsXML.GetChannel(this.ServerId, this.ChannelId);
			if (channel == null)
			{
				return false;
			}
			this.method_0(Player);
			if (this.GetCountPlayers() != 0)
			{
				if (Player.MatchSlot == this.Leader)
				{
					this.SetNewLeader(-1, -1);
				}
				using (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK pROTOCOLCLANWARREGISTMERCENARYACK = new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(this))
				{
					this.SendPacketToPlayers(pROTOCOLCLANWARREGISTMERCENARYACK);
				}
			}
			else
			{
				channel.RemoveMatch(this.MatchId);
			}
			Player.MatchSlot = -1;
			Player.Match = null;
			return true;
		}

		public void SendPacketToPlayers(GameServerPacket Packet)
		{
			List<Account> allPlayers = this.GetAllPlayers();
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Match.SendPacketToPlayers(SendPacket)");
			foreach (Account allPlayer in allPlayers)
			{
				allPlayer.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		public void SendPacketToPlayers(GameServerPacket Packet, int Exception)
		{
			List<Account> allPlayers = this.GetAllPlayers(Exception);
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Match.SendPacketToPlayers(SendPacket,int)");
			foreach (Account allPlayer in allPlayers)
			{
				allPlayer.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		public void SetNewLeader(int Leader, int OldLeader)
		{
			lock (this.Slots)
			{
				if (Leader != -1)
				{
					this.Leader = Leader;
				}
				else
				{
					int ınt32 = 0;
					while (ınt32 < this.Training)
					{
						if (ınt32 == OldLeader || this.Slots[ınt32].PlayerId <= 0L)
						{
							ınt32++;
						}
						else
						{
							this.Leader = ınt32;
							return;
						}
					}
				}
			}
		}
	}
}