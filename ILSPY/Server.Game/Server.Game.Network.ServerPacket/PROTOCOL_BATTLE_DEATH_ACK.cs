using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_DEATH_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	private readonly FragInfos fragInfos_0;

	private readonly SlotModel slotModel_0;

	public PROTOCOL_BATTLE_DEATH_ACK(RoomModel roomModel_1, FragInfos fragInfos_1, SlotModel slotModel_1)
	{
		roomModel_0 = roomModel_1;
		fragInfos_0 = fragInfos_1;
		slotModel_0 = slotModel_1;
	}

	public override void Write()
	{
		WriteH(5136);
		WriteC((byte)fragInfos_0.KillingType);
		WriteC(fragInfos_0.KillsCount);
		WriteC(fragInfos_0.KillerSlot);
		WriteD(fragInfos_0.WeaponId);
		WriteT(fragInfos_0.X);
		WriteT(fragInfos_0.Y);
		WriteT(fragInfos_0.Z);
		WriteC(fragInfos_0.Flag);
		WriteC(fragInfos_0.Unk);
		for (int i = 0; i < fragInfos_0.KillsCount; i++)
		{
			FragModel fragModel = fragInfos_0.Frags[i];
			WriteC(fragModel.VictimSlot);
			WriteC(fragModel.WeaponClass);
			WriteC(fragModel.HitspotInfo);
			WriteH((ushort)fragModel.KillFlag);
			WriteC(fragModel.Unk);
			WriteT(fragModel.X);
			WriteT(fragModel.Y);
			WriteT(fragModel.Z);
			WriteC(fragModel.AssistSlot);
			WriteB(fragModel.Unks);
		}
		WriteH((ushort)roomModel_0.FRKills);
		WriteH((ushort)roomModel_0.FRDeaths);
		WriteH((ushort)roomModel_0.FRAssists);
		WriteH((ushort)roomModel_0.CTKills);
		WriteH((ushort)roomModel_0.CTDeaths);
		WriteH((ushort)roomModel_0.CTAssists);
		SlotModel[] slots = roomModel_0.Slots;
		foreach (SlotModel slotModel in slots)
		{
			WriteH((ushort)slotModel.AllKills);
			WriteH((ushort)slotModel.AllDeaths);
			WriteH((ushort)slotModel.AllAssists);
		}
		WriteC((byte)((slotModel_0.KillsOnLife == 2) ? 1u : ((slotModel_0.KillsOnLife == 3) ? 2u : ((slotModel_0.KillsOnLife > 3) ? 3u : 0u))));
		WriteH((ushort)fragInfos_0.Score);
		if (roomModel_0.IsDinoMode("DE"))
		{
			WriteH((ushort)roomModel_0.FRDino);
			WriteH((ushort)roomModel_0.CTDino);
		}
	}
}
