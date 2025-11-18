using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_EVENT_PORTAL_REQ : GameClientPacket
	{
		private string string_0;

		public PROTOCOL_BASE_EVENT_PORTAL_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadS(32);
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (!player.LoadedShop)
					{
						player.LoadedShop = true;
					}
					if (Bitwise.ReadFile(string.Concat(Environment.CurrentDirectory, "/Data/Raws/EventPortal.dat")) != this.string_0)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_EVENT_PORTAL_ACK(true));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_BASE_EVENT_PORTAL_ACK(false));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_EVENT_PORTAL_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}