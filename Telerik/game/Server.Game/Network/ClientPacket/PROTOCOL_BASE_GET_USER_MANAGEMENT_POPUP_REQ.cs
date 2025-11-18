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
	public class PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_REQ : GameClientPacket
	{
		private string string_0;

		public PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadU(33);
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.Nickname.Length != 0 && !(player.Nickname == this.string_0))
				{
					this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_ACK());
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