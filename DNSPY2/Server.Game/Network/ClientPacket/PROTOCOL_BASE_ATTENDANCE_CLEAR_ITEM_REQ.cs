using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000136 RID: 310
	public class PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ : GameClientPacket
	{
		// Token: 0x060002FF RID: 767 RVA: 0x00004E90 File Offset: 0x00003090
		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.int_1 = (int)base.ReadC();
			this.int_2 = (int)base.ReadC();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00018088 File Offset: 0x00016288
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (!string.IsNullOrEmpty(player.Nickname) && this.int_1 <= 2)
					{
						PlayerEvent @event = player.Event;
						if (@event != null)
						{
							uint num = uint.Parse(DateTimeUtil.Now("yyMMdd"));
							uint num2 = uint.Parse(string.Format("{0:yyMMdd}", DateTimeUtil.Convert(string.Format("{0}", @event.LastVisitDate))));
							int num3 = this.int_2 + 1;
							if (@event.LastVisitCheckDay >= num3 && num2 >= num)
							{
								EventVisitModel event2 = EventVisitXML.GetEvent(this.int_0);
								if (event2 == null)
								{
									this.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK((EventErrorEnum)2147489029U));
									return;
								}
								if (event2.Boxes[this.int_2] != null && event2.EventIsEnabled())
								{
									List<VisitItemModel> reward = event2.GetReward(this.int_2, this.int_1);
									if (reward.Count > 0)
									{
										if (this.method_0(player, reward))
										{
											@event.LastVisitCheckDay = num3;
											@event.LastVisitSeqType = this.int_1;
											@event.LastVisitDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
											ComDiv.UpdateDB("player_events", "owner_id", player.PlayerId, new string[] { "last_visit_check_day", "last_visit_seq_type", "last_visit_date" }, new object[]
											{
												@event.LastVisitCheckDay,
												@event.LastVisitSeqType,
												(long)((ulong)@event.LastVisitDate)
											});
										}
										else
										{
											this.eventErrorEnum_0 = (EventErrorEnum)2147489025U;
										}
									}
									else
									{
										this.eventErrorEnum_0 = (EventErrorEnum)2147489029U;
									}
								}
								else
								{
									this.eventErrorEnum_0 = (EventErrorEnum)2147489027U;
								}
							}
							else
							{
								this.eventErrorEnum_0 = (EventErrorEnum)2147489026U;
							}
						}
						else
						{
							this.eventErrorEnum_0 = (EventErrorEnum)2147489029U;
						}
						this.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK(this.eventErrorEnum_0));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK((EventErrorEnum)2147489024U));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000182EC File Offset: 0x000164EC
		private bool method_0(Account account_0, List<VisitItemModel> list_0)
		{
			bool flag;
			try
			{
				int num = 0;
				foreach (VisitItemModel visitItemModel in list_0)
				{
					GoodsItem good = ShopManager.GetGood(visitItemModel.GoodId);
					if (good != null)
					{
						if (ComDiv.GetIdStatics(good.Item.Id, 1) == 6 && account_0.Character.GetCharacter(good.Item.Id) == null)
						{
							AllUtils.CreateCharacter(account_0, good.Item);
						}
						else
						{
							account_0.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, good.Item));
						}
						num++;
					}
				}
				if (num > 0)
				{
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00004EB6 File Offset: 0x000030B6
		public PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ()
		{
		}

		// Token: 0x04000232 RID: 562
		private EventErrorEnum eventErrorEnum_0 = (EventErrorEnum)2147489028U;

		// Token: 0x04000233 RID: 563
		private int int_0;

		// Token: 0x04000234 RID: 564
		private int int_1;

		// Token: 0x04000235 RID: 565
		private int int_2;
	}
}
