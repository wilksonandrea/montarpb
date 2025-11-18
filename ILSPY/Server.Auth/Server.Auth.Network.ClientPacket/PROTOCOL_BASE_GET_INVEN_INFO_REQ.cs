using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_GET_INVEN_INFO_REQ : AuthClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			int num = 0;
			List<ItemsModel> list = AllUtils.LimitationIndex(player, player.Inventory.Items);
			if (list.Count > 0)
			{
				num = TemplatePackXML.Basics.Count;
				if (TemplatePackXML.GetPCCafe(player.CafePC) != null)
				{
					num += TemplatePackXML.GetPCCafeRewards(player.CafePC).Count;
				}
				Client.SendPacket(new PROTOCOL_BASE_GET_INVEN_INFO_ACK(0u, list, num));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_GET_INVEN_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
