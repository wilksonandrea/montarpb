using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_ROOM_INVITED_REQ : GameClientPacket
	{
		private long long_0;

		public PROTOCOL_CS_ROOM_INVITED_REQ()
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
				if (player != null && player.ClanId != 0)
				{
					Account account = AccountManager.GetAccount(this.long_0, 31);
					if (account != null && account.ClanId == player.ClanId)
					{
						account.SendPacket(new PROTOCOL_CS_ROOM_INVITED_RESULT_ACK(this.Client.PlayerId), false);
					}
					player.SendPacket(new PROTOCOL_CS_ROOM_INVITED_ACK(0));
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