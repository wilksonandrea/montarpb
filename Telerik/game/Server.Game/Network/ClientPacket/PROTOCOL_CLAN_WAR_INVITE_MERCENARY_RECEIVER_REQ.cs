using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					MatchModel match = player.Match;
					if (match == null || player.MatchSlot != match.Leader)
					{
						this.Client.SendPacket(new PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(-2147483648, 0));
					}
					else
					{
						match.Training = this.int_0;
						using (PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK pROTOCOLCLANWARINVITEMERCENARYRECEIVERACK = new PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(0, this.int_0))
						{
							match.SendPacketToPlayers(pROTOCOLCLANWARINVITEMERCENARYRECEIVERACK);
						}
					}
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