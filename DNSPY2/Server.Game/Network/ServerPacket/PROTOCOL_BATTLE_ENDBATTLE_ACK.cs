using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000062 RID: 98
	public class PROTOCOL_BATTLE_ENDBATTLE_ACK : GameServerPacket
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0000C37C File Offset: 0x0000A57C
		public PROTOCOL_BATTLE_ENDBATTLE_ACK(Account account_1)
		{
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.roomModel_0 = account_1.Room;
				this.int_0 = (int)((this.roomModel_0.RoomType == RoomCondition.Tutorial) ? TeamEnum.FR_TEAM : AllUtils.GetWinnerTeam(this.roomModel_0));
				this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
				this.bool_0 = this.roomModel_0.IsBotMode();
				AllUtils.GetBattleResult(this.roomModel_0, out this.int_2, out this.int_1, out this.byte_0);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000C410 File Offset: 0x0000A610
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

		// Token: 0x06000107 RID: 263 RVA: 0x0000C410 File Offset: 0x0000A610
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

		// Token: 0x06000108 RID: 264 RVA: 0x0000C478 File Offset: 0x0000A678
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

		// Token: 0x06000109 RID: 265 RVA: 0x0000CB34 File Offset: 0x0000AD34
		private byte[] method_0(RoomModel roomModel_1, bool bool_1)
		{
			byte[] array2;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (bool_1)
				{
					foreach (SlotModel slotModel in roomModel_1.Slots)
					{
						syncServerPacket.WriteH((ushort)slotModel.Score);
					}
				}
				else if (roomModel_1.ThisModeHaveRounds() || roomModel_1.IsDinoMode(""))
				{
					syncServerPacket.WriteH((ushort)(roomModel_1.IsDinoMode("DE") ? roomModel_1.FRDino : (roomModel_1.IsDinoMode("CC") ? roomModel_1.FRKills : roomModel_1.FRRounds)));
					syncServerPacket.WriteH((ushort)(roomModel_1.IsDinoMode("DE") ? roomModel_1.CTDino : (roomModel_1.IsDinoMode("CC") ? roomModel_1.CTKills : roomModel_1.CTRounds)));
					foreach (SlotModel slotModel2 in roomModel_1.Slots)
					{
						syncServerPacket.WriteC((byte)slotModel2.Objects);
					}
					syncServerPacket.WriteH(0);
					syncServerPacket.WriteH(0);
				}
				array2 = syncServerPacket.ToArray();
			}
			return array2;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000CC58 File Offset: 0x0000AE58
		private byte[] method_1(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteH((ushort)(roomModel_1.ThisModeHaveRounds() ? roomModel_1.FRRounds : 0));
				syncServerPacket.WriteH((ushort)(roomModel_1.ThisModeHaveRounds() ? roomModel_1.CTRounds : 0));
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000CCC0 File Offset: 0x0000AEC0
		private byte[] method_2(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				foreach (SlotModel slotModel in roomModel_1.Slots)
				{
					Account account;
					if (roomModel_1.GetPlayerBySlot(slotModel, out account))
					{
						syncServerPacket.WriteC((byte)account.Rank);
					}
					else
					{
						syncServerPacket.WriteC((byte)AllUtils.InitBotRank((int)(roomModel_1.IsStartingMatch() ? roomModel_1.IngameAiLevel : roomModel_1.AiLevel)));
					}
					syncServerPacket.WriteH(0);
					syncServerPacket.WriteD(1);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000CD60 File Offset: 0x0000AF60
		private byte[] method_3(Account account_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				PlayerEvent @event = account_1.Event;
				if (@event != null)
				{
					syncServerPacket.WriteC((byte)@event.LastPlaytimeFinish);
					syncServerPacket.WriteD((uint)@event.LastPlaytimeValue);
				}
				else
				{
					syncServerPacket.WriteB(new byte[5]);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000CDCC File Offset: 0x0000AFCC
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

		// Token: 0x0600010E RID: 270 RVA: 0x0000CE84 File Offset: 0x0000B084
		private byte[] method_5(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				foreach (byte b in roomModel_1.SlotRewards.Item1)
				{
					syncServerPacket.WriteC(b);
				}
				foreach (int num in roomModel_1.SlotRewards.Item2)
				{
					syncServerPacket.WriteD(num);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x040000BF RID: 191
		private readonly RoomModel roomModel_0;

		// Token: 0x040000C0 RID: 192
		private readonly Account account_0;

		// Token: 0x040000C1 RID: 193
		private readonly ClanModel clanModel_0;

		// Token: 0x040000C2 RID: 194
		private readonly int int_0 = 2;

		// Token: 0x040000C3 RID: 195
		private readonly int int_1;

		// Token: 0x040000C4 RID: 196
		private readonly int int_2;

		// Token: 0x040000C5 RID: 197
		private readonly bool bool_0;

		// Token: 0x040000C6 RID: 198
		private readonly byte[] byte_0;
	}
}
