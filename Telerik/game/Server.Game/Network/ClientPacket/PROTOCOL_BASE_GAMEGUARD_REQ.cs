using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_GAMEGUARD_REQ : GameClientPacket
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
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}