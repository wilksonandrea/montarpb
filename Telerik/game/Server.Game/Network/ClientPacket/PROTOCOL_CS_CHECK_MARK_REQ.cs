using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_CHECK_MARK_REQ : GameClientPacket
	{
		private uint uint_0;

		private uint uint_1;

		public PROTOCOL_CS_CHECK_MARK_REQ()
		{
		}

		public override void Read()
		{
			this.uint_0 = base.ReadUD();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player == null || ClanManager.GetClan(player.ClanId).Logo == this.uint_0 || ClanManager.IsClanLogoExist(this.uint_0))
				{
					this.uint_1 = -2147483648;
				}
				this.Client.SendPacket(new PROTOCOL_CS_CHECK_MARK_ACK(this.uint_1));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}