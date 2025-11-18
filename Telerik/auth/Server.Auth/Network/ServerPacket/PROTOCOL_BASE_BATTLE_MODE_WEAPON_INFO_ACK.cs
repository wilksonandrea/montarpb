using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_BATTLE_MODE_WEAPON_INFO_ACK : AuthServerPacket
	{
		public PROTOCOL_BASE_BATTLE_MODE_WEAPON_INFO_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(2484);
			base.WriteC(0);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteD(33602800);
		}
	}
}