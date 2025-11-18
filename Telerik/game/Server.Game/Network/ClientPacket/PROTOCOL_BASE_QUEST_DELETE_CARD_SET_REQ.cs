using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ : GameClientPacket
	{
		private uint uint_0;

		private int int_0;

		public PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					PlayerMissions mission = player.Mission;
					bool flag = false;
					if (this.int_0 >= 3 || this.int_0 == 0 && mission.Mission1 == 0 || this.int_0 == 1 && mission.Mission2 == 0 || this.int_0 == 2 && mission.Mission3 == 0)
					{
						flag = true;
					}
					if (!flag && DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, 0, this.int_0))
					{
						if (!ComDiv.UpdateDB("player_missions", "owner_id", player.PlayerId, new string[] { string.Format("card{0}", this.int_0 + 1), string.Format("mission{0}_raw", this.int_0 + 1) }, new object[] { 0, new byte[0] }))
						{
							goto Label1;
						}
						if (this.int_0 == 0)
						{
							mission.Mission1 = 0;
							mission.Card1 = 0;
							mission.List1 = new byte[40];
							goto Label0;
						}
						else if (this.int_0 == 1)
						{
							mission.Mission2 = 0;
							mission.Card2 = 0;
							mission.List2 = new byte[40];
							goto Label0;
						}
						else if (this.int_0 == 2)
						{
							mission.Mission3 = 0;
							mission.Card3 = 0;
							mission.List3 = new byte[40];
							goto Label0;
						}
						else
						{
							goto Label0;
						}
					}
				Label1:
					this.uint_0 = -2147479472;
				Label0:
					this.Client.SendPacket(new PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK(this.uint_0, player));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}