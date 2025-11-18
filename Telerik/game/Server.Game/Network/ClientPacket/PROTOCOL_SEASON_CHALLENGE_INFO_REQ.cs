using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Reflection;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_SEASON_CHALLENGE_INFO_REQ : GameClientPacket
	{
		public PROTOCOL_SEASON_CHALLENGE_INFO_REQ()
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
					this.Client.SendPacket(new PROTOCOL_SEASON_CHALLENGE_INFO_ACK(player));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat(base.GetType().Name, "; ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}