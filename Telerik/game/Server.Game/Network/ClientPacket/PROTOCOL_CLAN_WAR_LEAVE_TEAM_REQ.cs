using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CLAN_WAR_LEAVE_TEAM_REQ : GameClientPacket
	{
		private uint uint_0;

		public PROTOCOL_CLAN_WAR_LEAVE_TEAM_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					MatchModel match = player.Match;
					if (match == null || !match.RemovePlayer(player))
					{
						this.uint_0 = -2147483648;
					}
					this.Client.SendPacket(new PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK(this.uint_0));
					if (this.uint_0 == 0)
					{
						player.Status.UpdateClanMatch(255);
						AllUtils.SyncPlayerToClanMembers(player);
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