using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000151 RID: 337
	public class PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ : GameClientPacket
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000502A File Offset: 0x0000322A
		public override void Read()
		{
			this.int_1 = (int)base.ReadC();
			this.int_0 = (int)base.ReadC();
			this.int_2 = (int)base.ReadUH();
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00019D04 File Offset: 0x00017F04
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					DBQuery dbquery = new DBQuery();
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
						dbquery.AddQuery(string.Format("card{0}", this.int_1 + 1), this.int_0);
					}
					mission.SelectedCard = this.int_2 == 65535;
					if (mission.ActualMission != this.int_1)
					{
						dbquery.AddQuery("current_mission", this.int_1);
						mission.ActualMission = this.int_1;
					}
					ComDiv.UpdateDB("player_missions", "owner_id", this.Client.PlayerId, dbquery.GetTables(), dbquery.GetValues());
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ()
		{
		}

		// Token: 0x0400026D RID: 621
		private int int_0;

		// Token: 0x0400026E RID: 622
		private int int_1;

		// Token: 0x0400026F RID: 623
		private int int_2;
	}
}
