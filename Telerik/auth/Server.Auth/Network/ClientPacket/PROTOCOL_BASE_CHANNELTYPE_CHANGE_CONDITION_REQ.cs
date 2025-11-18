using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;

namespace Server.Auth.Network.ClientPacket
{
	public class PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ : AuthClientPacket
	{
		public PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK());
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}