using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000153 RID: 339
	public class PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ : GameClientPacket
	{
		// Token: 0x06000359 RID: 857 RVA: 0x00005065 File Offset: 0x00003265
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001A0F8 File Offset: 0x000182F8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					PlayerMissions mission = player.Mission;
					bool flag = false;
					if (this.int_0 >= 3 || (this.int_0 == 0 && mission.Mission1 == 0) || (this.int_0 == 1 && mission.Mission2 == 0) || (this.int_0 == 2 && mission.Mission3 == 0))
					{
						flag = true;
					}
					if (!flag && DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, 0, this.int_0) && ComDiv.UpdateDB("player_missions", "owner_id", player.PlayerId, new string[]
					{
						string.Format("card{0}", this.int_0 + 1),
						string.Format("mission{0}_raw", this.int_0 + 1)
					}, new object[]
					{
						0,
						new byte[0]
					}))
					{
						if (this.int_0 == 0)
						{
							mission.Mission1 = 0;
							mission.Card1 = 0;
							mission.List1 = new byte[40];
						}
						else if (this.int_0 == 1)
						{
							mission.Mission2 = 0;
							mission.Card2 = 0;
							mission.List2 = new byte[40];
						}
						else if (this.int_0 == 2)
						{
							mission.Mission3 = 0;
							mission.Card3 = 0;
							mission.List3 = new byte[40];
						}
					}
					else
					{
						this.uint_0 = 2147487824U;
					}
					this.Client.SendPacket(new PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK(this.uint_0, player));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ()
		{
		}

		// Token: 0x04000272 RID: 626
		private uint uint_0;

		// Token: 0x04000273 RID: 627
		private int int_0;
	}
}
