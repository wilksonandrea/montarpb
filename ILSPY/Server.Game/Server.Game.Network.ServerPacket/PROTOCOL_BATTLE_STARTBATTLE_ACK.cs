using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_STARTBATTLE_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	private readonly SlotModel slotModel_0;

	private readonly bool bool_0;

	private readonly List<int> list_0;

	public PROTOCOL_BATTLE_STARTBATTLE_ACK(SlotModel slotModel_1, Account account_0, List<int> list_1, bool bool_1)
	{
		slotModel_0 = slotModel_1;
		roomModel_0 = account_0.Room;
		if (roomModel_0 != null)
		{
			bool_0 = bool_1;
			list_0 = list_1;
			if (!roomModel_0.IsBotMode() && roomModel_0.RoomType != RoomCondition.Tutorial)
			{
				AllUtils.CompleteMission(roomModel_0, account_0, slotModel_1, bool_1 ? MissionType.STAGE_ENTER : MissionType.STAGE_INTERCEPT, 0);
			}
		}
	}

	public PROTOCOL_BATTLE_STARTBATTLE_ACK()
	{
	}

	public override void Write()
	{
		WriteH(5132);
		WriteH(0);
		WriteD(0);
		WriteC(0);
		WriteB(method_0(roomModel_0, list_0));
		WriteC((byte)roomModel_0.Rounds);
		WriteD(AllUtils.GetSlotsFlag(roomModel_0, OnlyNoSpectators: true, MissionSuccess: false));
		WriteC((byte)((roomModel_0.ThisModeHaveRounds() || roomModel_0.IsDinoMode() || roomModel_0.RoomType == RoomCondition.FreeForAll) ? 2u : 0u));
		if (roomModel_0.ThisModeHaveRounds() || roomModel_0.IsDinoMode() || roomModel_0.RoomType == RoomCondition.FreeForAll)
		{
			WriteH((ushort)(roomModel_0.IsDinoMode("DE") ? roomModel_0.FRDino : (roomModel_0.IsDinoMode("CC") ? roomModel_0.FRKills : roomModel_0.FRRounds)));
			WriteH((ushort)(roomModel_0.IsDinoMode("DE") ? roomModel_0.CTDino : (roomModel_0.IsDinoMode("CC") ? roomModel_0.CTKills : roomModel_0.CTRounds)));
		}
		WriteC((byte)((roomModel_0.ThisModeHaveRounds() || roomModel_0.IsDinoMode() || roomModel_0.RoomType == RoomCondition.FreeForAll) ? 2u : 0u));
		if (roomModel_0.ThisModeHaveRounds() || roomModel_0.IsDinoMode() || roomModel_0.RoomType == RoomCondition.FreeForAll)
		{
			WriteH(0);
			WriteH(0);
		}
		WriteD(AllUtils.GetSlotsFlag(roomModel_0, OnlyNoSpectators: false, MissionSuccess: false));
		WriteC((!bool_0) ? ((byte)1) : ((byte)0));
		WriteC((byte)slotModel_0.Id);
	}

	private byte[] method_0(RoomModel roomModel_1, List<int> list_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		if (roomModel_1.IsDinoMode())
		{
			int num = ((list_1.Count == 1 || roomModel_1.IsDinoMode("CC")) ? 255 : roomModel_1.TRex);
			syncServerPacket.WriteC((byte)num);
			syncServerPacket.WriteC(10);
			for (int i = 0; i < list_1.Count; i++)
			{
				int num2 = list_1[i];
				if ((num2 != roomModel_1.TRex && roomModel_1.IsDinoMode("DE")) || roomModel_1.IsDinoMode("CC"))
				{
					syncServerPacket.WriteC((byte)num2);
				}
			}
			int num3 = 8 - list_1.Count - ((num == 255) ? 1 : 0);
			for (int j = 0; j < num3; j++)
			{
				syncServerPacket.WriteC(byte.MaxValue);
			}
			syncServerPacket.WriteC(byte.MaxValue);
		}
		else
		{
			syncServerPacket.WriteB(new byte[10]);
		}
		return syncServerPacket.ToArray();
	}
}
