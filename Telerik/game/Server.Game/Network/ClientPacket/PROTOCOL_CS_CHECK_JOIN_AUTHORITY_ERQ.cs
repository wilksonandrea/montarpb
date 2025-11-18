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
	public class PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ : GameClientPacket
	{
		private int int_0;

		private uint uint_0;

		public PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanModel clan = ClanManager.GetClan(this.int_0);
					if (clan.Id == 0)
					{
						this.uint_0 = -2147483648;
					}
					else if (clan.RankLimit > player.Rank)
					{
						this.uint_0 = -2147479429;
					}
					this.Client.SendPacket(new PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ACK(this.uint_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}