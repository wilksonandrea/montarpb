using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Auth;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Auth.Network.ClientPacket
{
	public class PROTOCOL_BASE_GET_MAP_INFO_REQ : AuthClientPacket
	{
		public PROTOCOL_BASE_GET_MAP_INFO_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_MAP_RULELIST_ACK());
				foreach (IEnumerable<MapMatch> mapMatches in SystemMapXML.Matches.Split<MapMatch>(100))
				{
					List<MapMatch> list = mapMatches.ToList<MapMatch>();
					if (list.Count <= 0)
					{
						continue;
					}
					this.Client.SendPacket(new PROTOCOL_BASE_MAP_MATCHINGLIST_ACK(list, list.Count));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_GET_MAP_INFO_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}