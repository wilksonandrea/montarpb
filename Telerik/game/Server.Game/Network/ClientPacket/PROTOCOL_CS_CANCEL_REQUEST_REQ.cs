using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_CANCEL_REQUEST_REQ : GameClientPacket
	{
		private uint uint_0;

		public PROTOCOL_CS_CANCEL_REQUEST_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				if (this.Client.Player == null || !DaoManagerSQL.DeleteClanInviteDB(this.Client.PlayerId))
				{
					this.uint_0 = -2147479461;
				}
				this.Client.SendPacket(new PROTOCOL_CS_CANCEL_REQUEST_ACK(this.uint_0));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}