using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Auth;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Auth.Network.ClientPacket
{
	public class PROTOCOL_BASE_GET_INVEN_INFO_REQ : AuthClientPacket
	{
		public PROTOCOL_BASE_GET_INVEN_INFO_REQ()
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
					int count = 0;
					List<ItemsModel> ıtemsModels = AllUtils.LimitationIndex(player, player.Inventory.Items);
					if (ıtemsModels.Count > 0)
					{
						count = TemplatePackXML.Basics.Count;
						if (TemplatePackXML.GetPCCafe(player.CafePC) != null)
						{
							count += TemplatePackXML.GetPCCafeRewards(player.CafePC).Count;
						}
						this.Client.SendPacket(new PROTOCOL_BASE_GET_INVEN_INFO_ACK(0, ıtemsModels, count));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_GET_INVEN_INFO_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}