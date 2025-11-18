using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_MEMBER_CONTEXT_REQ : GameClientPacket
	{
		public PROTOCOL_CS_MEMBER_CONTEXT_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			int ınt32;
			int clanPlayers;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					int ınt321 = (player.ClanId == 0 ? player.FindClanId : player.ClanId);
					if (ınt321 != 0)
					{
						ınt32 = 0;
						clanPlayers = DaoManagerSQL.GetClanPlayers(ınt321);
					}
					else
					{
						ınt32 = -1;
						clanPlayers = 0;
					}
					this.Client.SendPacket(new PROTOCOL_CS_MEMBER_CONTEXT_ACK(ınt32, clanPlayers));
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