using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_WINNING_CAM_ACK : GameServerPacket
{
	private readonly FragInfos fragInfos_0;

	public PROTOCOL_BATTLE_WINNING_CAM_ACK(FragInfos fragInfos_1)
	{
		fragInfos_0 = fragInfos_1;
	}

	public override void Write()
	{
		WriteH(5279);
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
	}
}
