using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ : GameClientPacket
	{
		private int int_0;

		private int int_1;

		private int int_2;

		public PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ()
		{
		}

		public override void Read()
		{
			this.int_1 = base.ReadC();
			this.int_0 = base.ReadC();
			this.int_2 = base.ReadUH();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					DBQuery dBQuery = new DBQuery();
					PlayerMissions mission = player.Mission;
					if (mission.GetCard(this.int_1) != this.int_0)
					{
						if (this.int_1 == 0)
						{
							mission.Card1 = this.int_0;
						}
						else if (this.int_1 == 1)
						{
							mission.Card2 = this.int_0;
						}
						else if (this.int_1 == 2)
						{
							mission.Card3 = this.int_0;
						}
						else if (this.int_1 == 3)
						{
							mission.Card4 = this.int_0;
						}
						dBQuery.AddQuery(string.Format("card{0}", this.int_1 + 1), this.int_0);
					}
					mission.SelectedCard = this.int_2 == 65535;
					if (mission.ActualMission != this.int_1)
					{
						dBQuery.AddQuery("current_mission", this.int_1);
						mission.ActualMission = this.int_1;
					}
					ComDiv.UpdateDB("player_missions", "owner_id", this.Client.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}