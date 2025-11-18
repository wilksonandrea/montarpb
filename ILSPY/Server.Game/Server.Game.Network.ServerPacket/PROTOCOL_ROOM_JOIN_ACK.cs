using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_JOIN_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly RoomModel roomModel_0;

	private readonly int int_0;

	public PROTOCOL_ROOM_JOIN_ACK(uint uint_1, Account account_0)
	{
		uint_0 = uint_1;
		if (account_0 != null)
		{
			int_0 = account_0.SlotId;
			roomModel_0 = account_0.Room;
		}
	}

	public override void Write()
	{
		WriteH(3586);
		WriteH(0);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			lock (roomModel_0.Slots)
			{
				WriteB(method_0(roomModel_0));
				WriteB(method_1(roomModel_0));
				WriteC(roomModel_0.AiType);
				WriteC(roomModel_0.IsStartingMatch() ? roomModel_0.IngameAiLevel : roomModel_0.AiLevel);
				WriteC(roomModel_0.AiCount);
				WriteC((byte)roomModel_0.GetAllPlayers().Count);
				WriteC((byte)roomModel_0.LeaderSlot);
				WriteC((byte)roomModel_0.CountdownTime.GetTimeLeft());
				WriteC((byte)roomModel_0.Password.Length);
				WriteS(roomModel_0.Password, roomModel_0.Password.Length);
				WriteB(new byte[17]);
				WriteU(roomModel_0.LeaderName, 66);
				WriteD(roomModel_0.KillTime);
				WriteC(roomModel_0.Limit);
				WriteC(roomModel_0.WatchRuleFlag);
				WriteH((ushort)roomModel_0.BalanceType);
				WriteB(roomModel_0.RandomMaps);
				WriteC(roomModel_0.CountdownIG);
				WriteB(roomModel_0.LeaderAddr);
				WriteC(roomModel_0.KillCam);
				WriteH(0);
				WriteD(roomModel_0.RoomId);
				WriteU(roomModel_0.Name, 46);
				WriteC((byte)roomModel_0.MapId);
				WriteC((byte)roomModel_0.Rule);
				WriteC((byte)roomModel_0.Stage);
				WriteC((byte)roomModel_0.RoomType);
				WriteC((byte)roomModel_0.State);
				WriteC((byte)roomModel_0.GetCountPlayers());
				WriteC((byte)roomModel_0.GetSlotCount());
				WriteC((byte)roomModel_0.Ping);
				WriteH((ushort)roomModel_0.WeaponsFlag);
				WriteD(roomModel_0.GetFlag());
				WriteH(0);
				WriteB(new byte[4]);
				WriteC(0);
				WriteC((byte)int_0);
			}
		}
	}

	private byte[] method_0(RoomModel roomModel_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)roomModel_1.Slots.Length);
		SlotModel[] slots = roomModel_1.Slots;
		foreach (SlotModel slotModel in slots)
		{
			syncServerPacket.WriteC((byte)slotModel.Team);
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_1(RoomModel roomModel_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)roomModel_1.Slots.Length);
		SlotModel[] slots = roomModel_1.Slots;
		foreach (SlotModel slotModel in slots)
		{
			syncServerPacket.WriteC((byte)slotModel.State);
			Account playerBySlot = roomModel_1.GetPlayerBySlot(slotModel);
			if (playerBySlot != null)
			{
				ClanModel clan = ClanManager.GetClan(playerBySlot.ClanId);
				syncServerPacket.WriteC((byte)playerBySlot.GetRank());
				syncServerPacket.WriteD(clan.Id);
				syncServerPacket.WriteD(playerBySlot.ClanAccess);
				syncServerPacket.WriteC((byte)clan.Rank);
				syncServerPacket.WriteD(clan.Logo);
				syncServerPacket.WriteC((byte)playerBySlot.CafePC);
				syncServerPacket.WriteC((byte)playerBySlot.Country);
				syncServerPacket.WriteQ((long)playerBySlot.Effects);
				syncServerPacket.WriteC((byte)clan.Effect);
				syncServerPacket.WriteC(0);
				syncServerPacket.WriteC((byte)NATIONS);
				syncServerPacket.WriteC(0);
				syncServerPacket.WriteD(playerBySlot.Equipment.NameCardId);
				syncServerPacket.WriteC((byte)playerBySlot.Bonus.NickBorderColor);
				syncServerPacket.WriteC((byte)playerBySlot.AuthLevel());
				syncServerPacket.WriteU(clan.Name, 34);
				syncServerPacket.WriteC((byte)playerBySlot.SlotId);
				syncServerPacket.WriteU(playerBySlot.Nickname, 66);
				syncServerPacket.WriteC((byte)playerBySlot.NickColor);
				syncServerPacket.WriteC((byte)playerBySlot.Bonus.MuzzleColor);
				syncServerPacket.WriteC(0);
				syncServerPacket.WriteC(byte.MaxValue);
				syncServerPacket.WriteC(byte.MaxValue);
			}
			else
			{
				syncServerPacket.WriteB(new byte[10]);
				syncServerPacket.WriteD(uint.MaxValue);
				syncServerPacket.WriteB(new byte[54]);
				syncServerPacket.WriteC((byte)slotModel.Id);
				syncServerPacket.WriteB(new byte[68]);
				syncServerPacket.WriteC(0);
				syncServerPacket.WriteC(byte.MaxValue);
				syncServerPacket.WriteC(byte.MaxValue);
			}
		}
		return syncServerPacket.ToArray();
	}
}
