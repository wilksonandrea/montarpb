using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_MESSENGER_NOTE_DELETE_REQ : GameClientPacket
	{
		private uint uint_0;

		private List<object> list_0 = new List<object>();

		public PROTOCOL_MESSENGER_NOTE_DELETE_REQ()
		{
		}

		public override void Read()
		{
			int ınt32 = base.ReadC();
			for (int i = 0; i < ınt32; i++)
			{
				long ınt64 = (long)base.ReadUD();
				this.list_0.Add(ınt64);
			}
		}

		public override void Run()
		{
			try
			{
				if (!DaoManagerSQL.DeleteMessages(this.list_0, this.Client.PlayerId))
				{
					this.uint_0 = -2147483648;
				}
				this.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_DELETE_ACK(this.uint_0, this.list_0));
				this.list_0 = null;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_MESSENGER_NOTE_DELETE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}