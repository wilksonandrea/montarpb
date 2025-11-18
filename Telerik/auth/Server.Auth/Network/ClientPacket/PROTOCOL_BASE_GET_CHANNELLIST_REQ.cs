using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Auth;
using Server.Auth.Data.Models;
using Server.Auth.Data.XML;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Auth.Network.ClientPacket
{
	public class PROTOCOL_BASE_GET_CHANNELLIST_REQ : AuthClientPacket
	{
		private int int_0;

		public PROTOCOL_BASE_GET_CHANNELLIST_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		public override void Run()
		{
			try
			{
				if (this.Client.Player != null)
				{
					List<ChannelModel> channels = ChannelsXML.GetChannels(this.int_0);
					if (channels.Count == 11)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_GET_CHANNELLIST_ACK(SChannelXML.GetServer(this.int_0), channels));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}