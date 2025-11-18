using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_CHATTING_REQ : GameClientPacket
	{
		private ChattingType chattingType_0;

		private string string_0;

		public PROTOCOL_CS_CHATTING_REQ()
		{
		}

		public override void Read()
		{
			this.chattingType_0 = (ChattingType)base.ReadH();
			this.string_0 = base.ReadU(base.ReadH() * 2);
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					int ınt32 = -1;
					bool flag = true;
					bool flag1 = true;
					if (this.string_0.Length <= 60 && this.chattingType_0 == ChattingType.Clan)
					{
						using (PROTOCOL_CS_CHATTING_ACK pROTOCOLCSCHATTINGACK = new PROTOCOL_CS_CHATTING_ACK(this.string_0, player))
						{
							ClanManager.SendPacket(pROTOCOLCSCHATTINGACK, player.ClanId, (long)ınt32, flag1, flag);
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