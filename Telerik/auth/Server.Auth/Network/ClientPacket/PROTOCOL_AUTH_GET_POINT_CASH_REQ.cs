using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth;
using Server.Auth.Data.Models;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;

namespace Server.Auth.Network.ClientPacket
{
	public class PROTOCOL_AUTH_GET_POINT_CASH_REQ : AuthClientPacket
	{
		public PROTOCOL_AUTH_GET_POINT_CASH_REQ()
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
					this.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, player));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_AUTH_GET_POINT_CASH_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}