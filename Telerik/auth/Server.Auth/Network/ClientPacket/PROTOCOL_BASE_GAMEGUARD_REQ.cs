using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Auth;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;

namespace Server.Auth.Network.ClientPacket
{
	public class PROTOCOL_BASE_GAMEGUARD_REQ : AuthClientPacket
	{
		private byte[] byte_0;

		public PROTOCOL_BASE_GAMEGUARD_REQ()
		{
		}

		public override void Read()
		{
			base.ReadB(48);
			this.byte_0 = base.ReadB(3);
		}

		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_GAMEGUARD_ACK(0, this.byte_0));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_GAMEGUARD_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}