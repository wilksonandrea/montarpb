using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_REQUEST_INFO_REQ : GameClientPacket
	{
		private long long_0;

		public PROTOCOL_CS_REQUEST_INFO_REQ()
		{
		}

		public override void Read()
		{
			this.long_0 = base.ReadQ();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					this.Client.SendPacket(new PROTOCOL_CS_REQUEST_INFO_ACK(this.long_0, DaoManagerSQL.GetRequestClanInviteText(player.ClanId, this.long_0)));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}