using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_USER_EVENT_SYNC_ACK : AuthServerPacket
	{
		public PROTOCOL_BASE_USER_EVENT_SYNC_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(2473);
			base.WriteH(0);
		}
	}
}