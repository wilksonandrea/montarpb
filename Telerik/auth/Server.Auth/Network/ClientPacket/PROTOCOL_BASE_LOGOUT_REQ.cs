using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;

namespace Server.Auth.Network.ClientPacket
{
	public class PROTOCOL_BASE_LOGOUT_REQ : AuthClientPacket
	{
		public PROTOCOL_BASE_LOGOUT_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_LOGOUT_ACK());
				this.Client.Close(5000, true);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_LOGOUT_REQ: ", exception.Message), LoggerType.Error, exception);
				this.Client.Close(0, true);
			}
		}
	}
}