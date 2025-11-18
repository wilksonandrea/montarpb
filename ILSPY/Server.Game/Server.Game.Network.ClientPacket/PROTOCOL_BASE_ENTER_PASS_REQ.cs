using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_ENTER_PASS_REQ : GameClientPacket
{
	private int int_0;

	private string string_0;

	public override void Read()
	{
		int_0 = ReadH();
		string_0 = ReadS(16);
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null || player.ChannelId >= 0)
			{
				return;
			}
			ChannelModel channel = ChannelsXML.GetChannel(Client.ServerId, int_0);
			if (channel != null)
			{
				if (!string_0.Equals(channel.Password))
				{
					Client.SendPacket(new PROTOCOL_BASE_ENTER_PASS_ACK(2147483648u));
				}
				else
				{
					Client.SendPacket(new PROTOCOL_BASE_ENTER_PASS_ACK(0u));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
