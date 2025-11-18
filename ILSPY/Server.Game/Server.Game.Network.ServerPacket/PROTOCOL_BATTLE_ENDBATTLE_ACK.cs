using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Network.ServerPacket;

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
		account_0 = account_1;
		if (account_1 != null)
		{
			roomModel_0 = account_1.Room;
			int_0 = (int)((roomModel_0.RoomType != RoomCondition.Tutorial) ? AllUtils.GetWinnerTeam(roomModel_0) : TeamEnum.FR_TEAM);
			clanModel_0 = ClanManager.GetClan(account_1.ClanId);
			bool_0 = roomModel_0.IsBotMode();
			AllUtils.GetBattleResult(roomModel_0, out int_2, out int_1, out byte_0);
		}
	}

	public PROTOCOL_BATTLE_ENDBATTLE_ACK(Account account_1, int int_3, int int_4, int int_5, bool bool_1, byte[] byte_1)
	{
		account_0 = account_1;
		int_0 = int_3;
		int_1 = int_4;
		int_2 = int_5;
		bool_0 = bool_1;
		byte_0 = byte_1;
		if (account_1 != null)
		{
			roomModel_0 = account_1.Room;
			clanModel_0 = ClanManager.GetClan(account_1.ClanId);
		}
	}

	public PROTOCOL_BATTLE_ENDBATTLE_ACK(Account account_1, TeamEnum teamEnum_0, int int_3, int int_4, bool bool_1, byte[] byte_1)
	{
		account_0 = account_1;
		int_0 = (int)teamEnum_0;
		int_1 = int_3;
		int_2 = int_4;
		bool_0 = bool_1;
		byte_0 = byte_1;
		if (account_1 != null)
		{
			roomModel_0 = account_1.Room;
			clanModel_0 = ClanManager.GetClan(account_1.ClanId);
		}
	}

	public override void Write()
	{
		WriteH(5140);
		WriteD(int_1);
		WriteC((byte)int_0);
		WriteB(byte_0);
		WriteD(int_2);
		WriteB(method_0(roomModel_0, bool_0));
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteB(new byte[5]);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteH(0);
		WriteH(0);
		WriteB(new byte[14]);
		WriteB(method_1(roomModel_0));
		WriteB(new byte[27]);
		WriteB(method_5(roomModel_0));
		WriteB(method_2(roomModel_0));
		WriteC((byte)(account_0.Nickname.Length * 2));
		WriteU(account_0.Nickname, account_0.Nickname.Length * 2);
		WriteD(account_0.GetRank());
		WriteD(account_0.Rank);
		WriteD(account_0.Gold);
		WriteD(account_0.Exp);
		WriteD(0);
		WriteC(0);
		WriteQ(0L);
		WriteC((byte)account_0.AuthLevel());
		WriteC(0);
		WriteD(account_0.Tags);
		WriteD(account_0.Cash);
		WriteD(clanModel_0.Id);
		WriteD(account_0.ClanAccess);
		WriteQ(account_0.StatusId());
		WriteC((byte)account_0.CafePC);
		WriteC((byte)account_0.Country);
		WriteC((byte)(clanModel_0.Name.Length * 2));
		WriteU(clanModel_0.Name, clanModel_0.Name.Length * 2);
		WriteC((byte)clanModel_0.Rank);
		WriteC((byte)clanModel_0.GetClanUnit());
		WriteD(clanModel_0.Logo);
		WriteC((byte)clanModel_0.NameColor);
		WriteC((byte)clanModel_0.Effect);
		WriteD(account_0.Statistic.Season.Matches);
		WriteD(account_0.Statistic.Season.MatchWins);
		WriteD(account_0.Statistic.Season.MatchLoses);
		WriteD(account_0.Statistic.Season.MatchDraws);
		WriteD(account_0.Statistic.Season.KillsCount);
		WriteD(account_0.Statistic.Season.HeadshotsCount);
		WriteD(account_0.Statistic.Season.DeathsCount);
		WriteD(account_0.Statistic.Season.TotalMatchesCount);
		WriteD(account_0.Statistic.Season.TotalKillsCount);
		WriteD(account_0.Statistic.Season.EscapesCount);
		WriteD(account_0.Statistic.Season.AssistsCount);
		WriteD(account_0.Statistic.Season.MvpCount);
		WriteD(account_0.Statistic.Basic.Matches);
		WriteD(account_0.Statistic.Basic.MatchWins);
		WriteD(account_0.Statistic.Basic.MatchLoses);
		WriteD(account_0.Statistic.Basic.MatchDraws);
		WriteD(account_0.Statistic.Basic.KillsCount);
		WriteD(account_0.Statistic.Basic.HeadshotsCount);
		WriteD(account_0.Statistic.Basic.DeathsCount);
		WriteD(account_0.Statistic.Basic.TotalMatchesCount);
		WriteD(account_0.Statistic.Basic.TotalKillsCount);
		WriteD(account_0.Statistic.Basic.EscapesCount);
		WriteD(account_0.Statistic.Basic.AssistsCount);
		WriteD(account_0.Statistic.Basic.MvpCount);
		WriteH((ushort)account_0.Statistic.Daily.Matches);
		WriteH((ushort)account_0.Statistic.Daily.MatchWins);
		WriteH((ushort)account_0.Statistic.Daily.MatchLoses);
		WriteH((ushort)account_0.Statistic.Daily.MatchDraws);
		WriteH((ushort)account_0.Statistic.Daily.KillsCount);
		WriteH((ushort)account_0.Statistic.Daily.HeadshotsCount);
		WriteH((ushort)account_0.Statistic.Daily.DeathsCount);
		WriteD(account_0.Statistic.Daily.ExpGained);
		WriteD(account_0.Statistic.Daily.PointGained);
		WriteD(account_0.Statistic.Daily.Playtime);
		WriteB(method_3(account_0));
		WriteD(0);
		WriteC(0);
		WriteD(0);
		WriteC(0);
		WriteD(0);
		WriteH(0);
		WriteC(0);
		WriteB(method_4(account_0));
	}

	private byte[] method_0(RoomModel roomModel_1, bool bool_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		if (bool_1)
		{
			SlotModel[] slots = roomModel_1.Slots;
			foreach (SlotModel slotModel in slots)
			{
				syncServerPacket.WriteH((ushort)slotModel.Score);
			}
		}
		else if (roomModel_1.ThisModeHaveRounds() || roomModel_1.IsDinoMode())
		{
			syncServerPacket.WriteH((ushort)(roomModel_1.IsDinoMode("DE") ? roomModel_1.FRDino : (roomModel_1.IsDinoMode("CC") ? roomModel_1.FRKills : roomModel_1.FRRounds)));
			syncServerPacket.WriteH((ushort)(roomModel_1.IsDinoMode("DE") ? roomModel_1.CTDino : (roomModel_1.IsDinoMode("CC") ? roomModel_1.CTKills : roomModel_1.CTRounds)));
			SlotModel[] slots = roomModel_1.Slots;
			foreach (SlotModel slotModel2 in slots)
			{
				syncServerPacket.WriteC((byte)slotModel2.Objects);
			}
			syncServerPacket.WriteH(0);
			syncServerPacket.WriteH(0);
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_1(RoomModel roomModel_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteH((ushort)(roomModel_1.ThisModeHaveRounds() ? ((uint)roomModel_1.FRRounds) : 0u));
		syncServerPacket.WriteH((ushort)(roomModel_1.ThisModeHaveRounds() ? ((uint)roomModel_1.CTRounds) : 0u));
		return syncServerPacket.ToArray();
	}

	private byte[] method_2(RoomModel roomModel_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		SlotModel[] slots = roomModel_1.Slots;
		foreach (SlotModel slot in slots)
		{
			if (roomModel_1.GetPlayerBySlot(slot, out var Player))
			{
				syncServerPacket.WriteC((byte)Player.Rank);
			}
			else
			{
				syncServerPacket.WriteC((byte)AllUtils.InitBotRank(roomModel_1.IsStartingMatch() ? roomModel_1.IngameAiLevel : roomModel_1.AiLevel));
			}
			syncServerPacket.WriteH(0);
			syncServerPacket.WriteD(1);
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_3(Account account_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
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
		return syncServerPacket.ToArray();
	}

	private byte[] method_4(Account account_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		SlotModel slot = roomModel_0.GetSlot(account_1.SlotId);
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
		return syncServerPacket.ToArray();
	}

	private byte[] method_5(RoomModel roomModel_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		byte[] ıtem = roomModel_1.SlotRewards.Item1;
		foreach (byte value in ıtem)
		{
			syncServerPacket.WriteC(value);
		}
		int[] ıtem2 = roomModel_1.SlotRewards.Item2;
		foreach (int value2 in ıtem2)
		{
			syncServerPacket.WriteD(value2);
		}
		return syncServerPacket.ToArray();
	}
}
