using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200015E RID: 350
	public class PROTOCOL_BASE_USER_TITLE_CHANGE_REQ : GameClientPacket
	{
		// Token: 0x0600037A RID: 890 RVA: 0x000050FD File Offset: 0x000032FD
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001AC3C File Offset: 0x00018E3C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && this.int_0 < 45)
				{
					if (player.Title.OwnerId == 0L)
					{
						DaoManagerSQL.CreatePlayerTitlesDB(player.PlayerId);
						player.Title = new PlayerTitles
						{
							OwnerId = player.PlayerId
						};
					}
					TitleModel title = TitleSystemXML.GetTitle(this.int_0, true);
					if (title != null)
					{
						TitleModel titleModel;
						TitleModel titleModel2;
						TitleSystemXML.Get2Titles(title.Req1, title.Req2, out titleModel, out titleModel2, false);
						if ((title.Req1 == 0 || titleModel != null) && (title.Req2 == 0 || titleModel2 != null) && player.Rank >= title.Rank && player.Ribbon >= title.Ribbon && player.Medal >= title.Medal && player.MasterMedal >= title.MasterMedal && player.Ensign >= title.Ensign && !player.Title.Contains(title.Flag) && player.Title.Contains(titleModel.Flag) && player.Title.Contains(titleModel2.Flag))
						{
							player.Ribbon -= title.Ribbon;
							player.Medal -= title.Medal;
							player.MasterMedal -= title.MasterMedal;
							player.Ensign -= title.Ensign;
							long num = player.Title.Add(title.Flag);
							DaoManagerSQL.UpdatePlayerTitlesFlags(player.PlayerId, num);
							List<ItemsModel> awards = TitleAwardXML.GetAwards(this.int_0);
							if (awards.Count > 0)
							{
								this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, awards));
							}
							this.Client.SendPacket(new PROTOCOL_BASE_MEDAL_GET_INFO_ACK(player));
							DaoManagerSQL.UpdatePlayerTitleRequi(player.PlayerId, player.Medal, player.Ensign, player.MasterMedal, player.Ribbon);
							if (player.Title.Slots < title.Slot)
							{
								player.Title.Slots = title.Slot;
								ComDiv.UpdateDB("player_titles", "slots", player.Title.Slots, "owner_id", player.PlayerId);
							}
						}
						else
						{
							this.uint_0 = 2147487875U;
						}
					}
					else
					{
						this.uint_0 = 2147487875U;
					}
					this.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_CHANGE_ACK(this.uint_0, player.Title.Slots));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_USER_TITLE_CHANGE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_USER_TITLE_CHANGE_REQ()
		{
		}

		// Token: 0x0400027E RID: 638
		private int int_0;

		// Token: 0x0400027F RID: 639
		private uint uint_0;
	}
}
