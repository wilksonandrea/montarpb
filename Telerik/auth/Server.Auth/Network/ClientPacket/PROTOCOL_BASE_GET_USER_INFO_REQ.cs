using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Auth;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Auth.Network.ClientPacket
{
	public class PROTOCOL_BASE_GET_USER_INFO_REQ : AuthClientPacket
	{
		public PROTOCOL_BASE_GET_USER_INFO_REQ()
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
					if (player.Inventory.Items.Count == 0)
					{
						AllUtils.ValidateAuthLevel(player);
						AllUtils.LoadPlayerInventory(player);
						AllUtils.LoadPlayerMissions(player);
						AllUtils.ValidatePlayerInventoryStatus(player);
						AllUtils.DiscountPlayerItems(player);
						AllUtils.CheckGameEvents(player);
						this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_INFO_ACK(player));
						this.Client.SendPacket(new PROTOCOL_BASE_GET_CHARA_INFO_ACK(player));
						AllUtils.ProcessBattlepass(player);
						this.Client.SendPacket(new PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE());
						this.Client.SendPacket(new PROTOCOL_SEASON_CHALLENGE_INFO_ACK(player));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_GET_USER_INFO_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}