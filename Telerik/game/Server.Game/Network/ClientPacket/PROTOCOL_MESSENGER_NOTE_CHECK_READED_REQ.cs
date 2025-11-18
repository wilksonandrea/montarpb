using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ : GameClientPacket
	{
		private readonly List<int> list_0 = new List<int>();

		public PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ()
		{
		}

		public override void Read()
		{
			int ınt32 = base.ReadC();
			for (int i = 0; i < ınt32; i++)
			{
				this.list_0.Add(base.ReadD());
			}
		}

		public override void Run()
		{
			try
			{
				int[] array = this.list_0.ToArray();
				object playerId = this.Client.PlayerId;
				string[] strArrays = new string[] { "expire_date", "state" };
				object[] objArray = new object[2];
				DateTime dateTime = DateTimeUtil.Now().AddDays(7);
				objArray[0] = long.Parse(dateTime.ToString("yyMMddHHmm"));
				objArray[1] = 0;
				if (ComDiv.UpdateDB("player_messages", "object_id", array, "owner_id", playerId, strArrays, objArray))
				{
					this.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK(this.list_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}