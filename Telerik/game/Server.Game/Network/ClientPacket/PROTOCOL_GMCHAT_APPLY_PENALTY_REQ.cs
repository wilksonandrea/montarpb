using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_GMCHAT_APPLY_PENALTY_REQ : GameClientPacket
	{
		private long long_0;

		private string string_0;

		private string string_1;

		private int int_0;

		private byte byte_0;

		public PROTOCOL_GMCHAT_APPLY_PENALTY_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadU(base.ReadC() * 2);
			this.string_1 = base.ReadU(base.ReadC() * 2);
			this.byte_0 = base.ReadC();
			this.int_0 = base.ReadD();
			base.ReadC();
			this.long_0 = base.ReadQ();
		}

		public override void Run()
		{
			try
			{
				if (this.Client.Player != null)
				{
					if (AccountManager.GetAccount(this.long_0, 31) == null)
					{
						this.Client.SendPacket(new PROTOCOL_GMCHAT_APPLY_PENALTY_ACK(-2147483648));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_GMCHAT_APPLY_PENALTY_ACK(0));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_GMCHAT_APPLY_PENALTY_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}