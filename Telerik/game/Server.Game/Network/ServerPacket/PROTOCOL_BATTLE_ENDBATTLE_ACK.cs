using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_ENDBATTLE_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		private readonly Account account_0;

		private readonly ClanModel clanModel_0;

		private readonly int int_0 = 2;

		private readonly int int_1;

		private readonly int int_2;

		private readonly bool bool_0;

		private readonly byte[] byte_0;

		public PROTOCOL_BATTLE_ENDBATTLE_ACK(Account account_1)
		{
			int winnerTeam;
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.roomModel_0 = account_1.Room;
				if (this.roomModel_0.RoomType == RoomCondition.Tutorial)
				{
					winnerTeam = 0;
				}
				else
				{
					winnerTeam = (int)AllUtils.GetWinnerTeam(this.roomModel_0);
				}
				this.int_0 = winnerTeam;
				this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
				this.bool_0 = this.roomModel_0.IsBotMode();
				AllUtils.GetBattleResult(this.roomModel_0, out this.int_2, out this.int_1, out this.byte_0);
			}
		}

		public PROTOCOL_BATTLE_ENDBATTLE_ACK(Account account_1, int int_3, int int_4, int int_5, bool bool_1, byte[] byte_1)
		{
			this.account_0 = account_1;
			this.int_0 = int_3;
			this.int_1 = int_4;
			this.int_2 = int_5;
			this.bool_0 = bool_1;
			this.byte_0 = byte_1;
			if (account_1 != null)
			{
				this.roomModel_0 = account_1.Room;
				this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
			}
		}

		public PROTOCOL_BATTLE_ENDBATTLE_ACK(Account account_1, TeamEnum teamEnum_0, int int_3, int int_4, bool bool_1, byte[] byte_1)
		{
			this.account_0 = account_1;
			this.int_0 = (int)teamEnum_0;
			this.int_1 = int_3;
			this.int_2 = int_4;
			this.bool_0 = bool_1;
			this.byte_0 = byte_1;
			if (account_1 != null)
			{
				this.roomModel_0 = account_1.Room;
				this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
			}
		}

		private byte[] method_0(RoomModel roomModel_1, bool bool_1)
		{
			SlotModel[] slots;
			int i;
			byte[] array;
			object fRDino;
			object cTDino;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (bool_1)
				{
					slots = roomModel_1.Slots;
					for (i = 0; i < (int)slots.Length; i++)
					{
						syncServerPacket.WriteH((ushort)slots[i].Score);
					}
				}
				else if (roomModel_1.ThisModeHaveRounds() || roomModel_1.IsDinoMode(""))
				{
					SyncServerPacket syncServerPacket1 = syncServerPacket;
					if (roomModel_1.IsDinoMode("DE"))
					{
						fRDino = roomModel_1.FRDino;
					}
					else
					{
						fRDino = (roomModel_1.IsDinoMode("CC") ? roomModel_1.FRKills : roomModel_1.FRRounds);
					}
					syncServerPacket1.WriteH((ushort)fRDino);
					SyncServerPacket syncServerPacket2 = syncServerPacket;
					if (roomModel_1.IsDinoMode("DE"))
					{
						cTDino = roomModel_1.CTDino;
					}
					else
					{
						cTDino = (roomModel_1.IsDinoMode("CC") ? roomModel_1.CTKills : roomModel_1.CTRounds);
					}
					syncServerPacket2.WriteH((ushort)cTDino);
					slots = roomModel_1.Slots;
					for (i = 0; i < (int)slots.Length; i++)
					{
						syncServerPacket.WriteC((byte)slots[i].Objects);
					}
					syncServerPacket.WriteH(0);
					syncServerPacket.WriteH(0);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_1(RoomModel roomModel_1)
		{
			byte[] array;
			object fRRounds;
			object cTRounds;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				SyncServerPacket syncServerPacket1 = syncServerPacket;
				if (roomModel_1.ThisModeHaveRounds())
				{
					fRRounds = roomModel_1.FRRounds;
				}
				else
				{
					fRRounds = null;
				}
				syncServerPacket1.WriteH((ushort)fRRounds);
				SyncServerPacket syncServerPacket2 = syncServerPacket;
				if (roomModel_1.ThisModeHaveRounds())
				{
					cTRounds = roomModel_1.CTRounds;
				}
				else
				{
					cTRounds = null;
				}
				syncServerPacket2.WriteH((ushort)cTRounds);
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_2(RoomModel roomModel_1)
		{
			Account account;
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				SlotModel[] slots = roomModel_1.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					if (!roomModel_1.GetPlayerBySlot(slots[i], out account))
					{
						syncServerPacket.WriteC((byte)AllUtils.InitBotRank((roomModel_1.IsStartingMatch() ? (int)roomModel_1.IngameAiLevel : (int)roomModel_1.AiLevel)));
					}
					else
					{
						syncServerPacket.WriteC((byte)account.Rank);
					}
					syncServerPacket.WriteH(0);
					syncServerPacket.WriteD(1);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_3(Account account_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				PlayerEvent @event = account_1.Event;
				if (@event == null)
				{
					syncServerPacket.WriteB(new byte[5]);
				}
				else
				{
					syncServerPacket.WriteC((byte)@event.LastPlaytimeFinish);
					syncServerPacket.WriteD((uint)@event.LastPlaytimeValue);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_4(Account account_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				SlotModel slot = this.roomModel_0.GetSlot(account_1.SlotId);
				if (slot != null)
				{
					syncServerPacket.WriteB(new byte[44]);
					syncServerPacket.WriteD(0);
					syncServerPacket.WriteB(new byte[16]);
					syncServerPacket.WriteH((ushort)slot.SeasonPoint);
					syncServerPacket.WriteH((ushort)slot.BonusBattlePass);
					syncServerPacket.WriteC(0);
					syncServerPacket.WriteB(new byte[20]);
					syncServerPacket.WriteD(0);
					syncServerPacket.WriteH((ushort)(600 + account_1.InventoryPlus + 8));
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_5(RoomModel roomModel_1)
		{
			byte[] ıtem1;
			int i;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				ıtem1 = roomModel_1.SlotRewards.Item1;
				for (i = 0; i < (int)ıtem1.Length; i++)
				{
					syncServerPacket.WriteC(ıtem1[i]);
				}
				int[] ıtem2 = roomModel_1.SlotRewards.Item2;
				for (i = 0; i < (int)ıtem2.Length; i++)
				{
					syncServerPacket.WriteD(ıtem2[i]);
				}
				ıtem1 = syncServerPacket.ToArray();
			}
			return ıtem1;
		}

		public override void Write()
		{
			base.WriteH(5140);
			base.WriteD(this.int_1);
			base.WriteC((byte)this.int_0);
			base.WriteB(this.byte_0);
			base.WriteD(this.int_2);
			base.WriteB(this.method_0(this.roomModel_0, this.bool_0));
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteB(new byte[5]);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteH(0);
			base.WriteH(0);
			base.WriteB(new byte[14]);
			base.WriteB(this.method_1(this.roomModel_0));
			base.WriteB(new byte[27]);
			base.WriteB(this.method_5(this.roomModel_0));
			base.WriteB(this.method_2(this.roomModel_0));
			base.WriteC((byte)(this.account_0.Nickname.Length * 2));
			base.WriteU(this.account_0.Nickname, this.account_0.Nickname.Length * 2);
			base.WriteD(this.account_0.GetRank());
			base.WriteD(this.account_0.Rank);
			base.WriteD(this.account_0.Gold);
			base.WriteD(this.account_0.Exp);
			base.WriteD(0);
			base.WriteC(0);
			base.WriteQ(0L);
			base.WriteC((byte)this.account_0.AuthLevel());
			base.WriteC(0);
			base.WriteD(this.account_0.Tags);
			base.WriteD(this.account_0.Cash);
			base.WriteD(this.clanModel_0.Id);
			base.WriteD(this.account_0.ClanAccess);
			base.WriteQ(this.account_0.StatusId());
			base.WriteC((byte)this.account_0.CafePC);
			base.WriteC((byte)this.account_0.Country);
			base.WriteC((byte)(this.clanModel_0.Name.Length * 2));
			base.WriteU(this.clanModel_0.Name, this.clanModel_0.Name.Length * 2);
			base.WriteC((byte)this.clanModel_0.Rank);
			base.WriteC((byte)this.clanModel_0.GetClanUnit());
			base.WriteD(this.clanModel_0.Logo);
			base.WriteC((byte)this.clanModel_0.NameColor);
			base.WriteC((byte)this.clanModel_0.Effect);
			base.WriteD(this.account_0.Statistic.Season.Matches);
			base.WriteD(this.account_0.Statistic.Season.MatchWins);
			base.WriteD(this.account_0.Statistic.Season.MatchLoses);
			base.WriteD(this.account_0.Statistic.Season.MatchDraws);
			base.WriteD(this.account_0.Statistic.Season.KillsCount);
			base.WriteD(this.account_0.Statistic.Season.HeadshotsCount);
			base.WriteD(this.account_0.Statistic.Season.DeathsCount);
			base.WriteD(this.account_0.Statistic.Season.TotalMatchesCount);
			base.WriteD(this.account_0.Statistic.Season.TotalKillsCount);
			base.WriteD(this.account_0.Statistic.Season.EscapesCount);
			base.WriteD(this.account_0.Statistic.Season.AssistsCount);
			base.WriteD(this.account_0.Statistic.Season.MvpCount);
			base.WriteD(this.account_0.Statistic.Basic.Matches);
			base.WriteD(this.account_0.Statistic.Basic.MatchWins);
			base.WriteD(this.account_0.Statistic.Basic.MatchLoses);
			base.WriteD(this.account_0.Statistic.Basic.MatchDraws);
			base.WriteD(this.account_0.Statistic.Basic.KillsCount);
			base.WriteD(this.account_0.Statistic.Basic.HeadshotsCount);
			base.WriteD(this.account_0.Statistic.Basic.DeathsCount);
			base.WriteD(this.account_0.Statistic.Basic.TotalMatchesCount);
			base.WriteD(this.account_0.Statistic.Basic.TotalKillsCount);
			base.WriteD(this.account_0.Statistic.Basic.EscapesCount);
			base.WriteD(this.account_0.Statistic.Basic.AssistsCount);
			base.WriteD(this.account_0.Statistic.Basic.MvpCount);
			base.WriteH((ushort)this.account_0.Statistic.Daily.Matches);
			base.WriteH((ushort)this.account_0.Statistic.Daily.MatchWins);
			base.WriteH((ushort)this.account_0.Statistic.Daily.MatchLoses);
			base.WriteH((ushort)this.account_0.Statistic.Daily.MatchDraws);
			base.WriteH((ushort)this.account_0.Statistic.Daily.KillsCount);
			base.WriteH((ushort)this.account_0.Statistic.Daily.HeadshotsCount);
			base.WriteH((ushort)this.account_0.Statistic.Daily.DeathsCount);
			base.WriteD(this.account_0.Statistic.Daily.ExpGained);
			base.WriteD(this.account_0.Statistic.Daily.PointGained);
			base.WriteD(this.account_0.Statistic.Daily.Playtime);
			base.WriteB(this.method_3(this.account_0));
			base.WriteD(0);
			base.WriteC(0);
			base.WriteD(0);
			base.WriteC(0);
			base.WriteD(0);
			base.WriteH(0);
			base.WriteC(0);
			base.WriteB(this.method_4(this.account_0));
		}
	}
}