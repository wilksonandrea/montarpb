using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_ENTER_PASS_REQ : GameClientPacket
	{
		private int int_0;

		private string string_0;

		public PROTOCOL_BASE_ENTER_PASS_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadH();
			this.string_0 = base.ReadS(16);
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.ChannelId < 0)
				{
					ChannelModel channel = ChannelsXML.GetChannel(this.Client.ServerId, this.int_0);
					if (channel != null)
					{
						if (this.string_0.Equals(channel.Password))
						{
							this.Client.SendPacket(new PROTOCOL_BASE_ENTER_PASS_ACK(0));
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_BASE_ENTER_PASS_ACK(-2147483648));
						}
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