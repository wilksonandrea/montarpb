using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Reflection;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ : GameClientPacket
	{
		private long long_0;

		private int int_0;

		private byte byte_0;

		public PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.byte_0 = base.ReadC();
			this.long_0 = base.ReadQ();
		}

		public override void Run()
		{
			try
			{
				if (this.Client.Player != null)
				{
					Account account = AccountManager.GetAccount(this.long_0, 31);
					if (account == null)
					{
						this.Client.SendPacket(new PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK(-2147483648, account));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK(0, account));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat(base.GetType().Name, ": ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}