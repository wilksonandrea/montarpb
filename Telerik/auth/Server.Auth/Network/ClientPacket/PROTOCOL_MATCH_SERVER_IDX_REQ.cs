using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Auth;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;

namespace Server.Auth.Network.ClientPacket
{
	public class PROTOCOL_MATCH_SERVER_IDX_REQ : AuthClientPacket
	{
		private short short_0;

		public PROTOCOL_MATCH_SERVER_IDX_REQ()
		{
		}

		public override void Read()
		{
			this.short_0 = base.ReadH();
			base.ReadC();
		}

		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_MATCH_SERVER_IDX_ACK(this.short_0));
				this.Client.Close(0, false);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_MATCH_SERVER_IDX_REQ: ", exception.Message), LoggerType.Warning, null);
			}
		}
	}
}