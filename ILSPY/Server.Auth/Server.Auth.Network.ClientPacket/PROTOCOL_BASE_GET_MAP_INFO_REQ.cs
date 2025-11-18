using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_GET_MAP_INFO_REQ : AuthClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Client.SendPacket(new PROTOCOL_BASE_MAP_RULELIST_ACK());
			foreach (IEnumerable<MapMatch> item in SystemMapXML.Matches.Split(100))
			{
				List<MapMatch> list = item.ToList();
				if (list.Count > 0)
				{
					Client.SendPacket(new PROTOCOL_BASE_MAP_MATCHINGLIST_ACK(list, list.Count));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_GET_MAP_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
