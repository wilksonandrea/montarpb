using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_REQ : GameClientPacket
{
	private string string_0;

	public override void Read()
	{
		string_0 = ReadU(33);
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null && player.Nickname.Length != 0 && !(player.Nickname == string_0))
			{
				Client.SendPacket(new PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_ACK());
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
