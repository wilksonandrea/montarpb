using System;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CLIENT_ENTER_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly int int_1;

	public PROTOCOL_CS_CLIENT_ENTER_ACK(int int_2, int int_3)
	{
		int_1 = int_2;
		int_0 = int_3;
	}

	public override void Write()
	{
		WriteH(770);
		WriteD(0);
		WriteD(int_1);
		WriteD(int_0);
		if (int_1 == 0 || int_0 == 0)
		{
			WriteD(ClanManager.Clans.Count);
			WriteC(15);
			WriteH((ushort)Math.Ceiling((double)ClanManager.Clans.Count / 15.0));
			WriteD(uint.Parse(DateTimeUtil.Now("MMddHHmmss")));
		}
	}
}
