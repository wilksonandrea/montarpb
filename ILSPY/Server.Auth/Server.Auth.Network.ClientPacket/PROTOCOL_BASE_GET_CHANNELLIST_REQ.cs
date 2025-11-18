using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using Server.Auth.Data.XML;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_GET_CHANNELLIST_REQ : AuthClientPacket
{
	private int int_0;

	public override void Read()
	{
		int_0 = ReadD();
	}

	public override void Run()
	{
		try
		{
			if (Client.Player != null)
			{
				List<ChannelModel> channels = ChannelsXML.GetChannels(int_0);
				if (channels.Count == 11)
				{
					Client.SendPacket(new PROTOCOL_BASE_GET_CHANNELLIST_ACK(SChannelXML.GetServer(int_0), channels));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
