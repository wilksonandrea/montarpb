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
	public class PROTOCOL_CS_PAGE_CHATTING_REQ : GameClientPacket
	{
		private ChattingType chattingType_0;

		private string string_0;

		public PROTOCOL_CS_PAGE_CHATTING_REQ()
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
					if (this.chattingType_0 == ChattingType.ClanMemberPage)
					{
						using (PROTOCOL_CS_PAGE_CHATTING_ACK pROTOCOLCSPAGECHATTINGACK = new PROTOCOL_CS_PAGE_CHATTING_ACK(player, this.string_0))
						{
							ClanManager.SendPacket(pROTOCOLCSPAGECHATTINGACK, player.ClanId, -1L, true, true);
						}
						return;
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_PAGE_CHATTING_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}