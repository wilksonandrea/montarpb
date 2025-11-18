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
	public class PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ()
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
					player.FindClanId = this.int_0;
					ClanModel clan = ClanManager.GetClan(player.FindClanId);
					if (clan.Id > 0)
					{
						this.Client.SendPacket(new PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK(0, clan));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}