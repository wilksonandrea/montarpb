using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000152 RID: 338
	public class PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ : GameClientPacket
	{
		// Token: 0x06000356 RID: 854 RVA: 0x00005050 File Offset: 0x00003250
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
			base.ReadC();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00019E74 File Offset: 0x00018074
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					PlayerMissions mission = player.Mission;
					if (mission != null && mission.Mission1 != this.int_0 && mission.Mission2 != this.int_0)
					{
						if (mission.Mission3 != this.int_0)
						{
							MissionStore mission2 = MissionConfigXML.GetMission(this.int_0);
							if (mission2 != null && ShopManager.GetItemId(mission2.ItemId) != null)
							{
								if (mission.Mission1 == 0)
								{
									if (DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, this.int_0, 0))
									{
										mission.Mission1 = this.int_0;
										mission.List1 = new byte[40];
										mission.ActualMission = 0;
										mission.Card1 = 0;
									}
									else
									{
										this.eventErrorEnum_0 = (EventErrorEnum)2147487820U;
									}
								}
								else if (mission.Mission2 == 0)
								{
									if (DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, this.int_0, 1))
									{
										mission.Mission2 = this.int_0;
										mission.List2 = new byte[40];
										mission.ActualMission = 1;
										mission.Card2 = 0;
									}
									else
									{
										this.eventErrorEnum_0 = (EventErrorEnum)2147487820U;
									}
								}
								else if (mission.Mission3 == 0)
								{
									if (DaoManagerSQL.UpdatePlayerMissionId(player.PlayerId, this.int_0, 2))
									{
										mission.Mission3 = this.int_0;
										mission.List3 = new byte[40];
										mission.ActualMission = 2;
										mission.Card3 = 0;
									}
									else
									{
										this.eventErrorEnum_0 = (EventErrorEnum)2147487820U;
									}
								}
								else
								{
									this.eventErrorEnum_0 = (EventErrorEnum)2147487822U;
								}
							}
							else
							{
								CLogger.Print("There is an error on Mission Config. Please check the configuration!", LoggerType.Warning, null);
								this.eventErrorEnum_0 = (EventErrorEnum)2147487821U;
							}
							int priceGold = ShopManager.GetItemId(mission2.ItemId).PriceGold;
							if (this.eventErrorEnum_0 == EventErrorEnum.SUCCESS && 0 <= player.Gold - priceGold)
							{
								if (priceGold != 0 && !DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold - priceGold))
								{
									this.eventErrorEnum_0 = (EventErrorEnum)2147487820U;
								}
								else
								{
									player.Gold -= priceGold;
								}
							}
							else
							{
								this.eventErrorEnum_0 = (EventErrorEnum)2147487821U;
							}
							this.Client.SendPacket(new PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK(this.eventErrorEnum_0, player));
							return;
						}
					}
					this.Client.SendPacket(new PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK((EventErrorEnum)2147487821U, null));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(base.GetType().Name + ": " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ()
		{
		}

		// Token: 0x04000270 RID: 624
		private int int_0;

		// Token: 0x04000271 RID: 625
		private EventErrorEnum eventErrorEnum_0;
	}
}
