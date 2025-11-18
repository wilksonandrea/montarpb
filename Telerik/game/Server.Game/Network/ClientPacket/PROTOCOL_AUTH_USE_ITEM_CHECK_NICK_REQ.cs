using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ : GameClientPacket
	{
		private string string_0;

		public PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadU(66);
		}

		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK((uint)((!DaoManagerSQL.IsPlayerNameExist(this.string_0) ? 0 : -2147483373))));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}