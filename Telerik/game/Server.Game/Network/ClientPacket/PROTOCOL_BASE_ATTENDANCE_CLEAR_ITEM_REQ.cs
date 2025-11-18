using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ : GameClientPacket
	{
		private EventErrorEnum eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_SUCCESS;

		private int int_0;

		private int int_1;

		private int int_2;

		public PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ()
		{
		}

		private bool method_0(Account account_0, List<VisitItemModel> list_0)
		{
			bool flag;
			try
			{
				int ınt32 = 0;
				foreach (VisitItemModel list0 in list_0)
				{
					GoodsItem good = ShopManager.GetGood(list0.GoodId);
					if (good == null)
					{
						continue;
					}
					if (ComDiv.GetIdStatics(good.Item.Id, 1) != 6 || account_0.Character.GetCharacter(good.Item.Id) != null)
					{
						account_0.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, good.Item));
					}
					else
					{
						AllUtils.CreateCharacter(account_0, good.Item);
					}
					ınt32++;
				}
				flag = (ınt32 <= 0 ? false : true);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.int_1 = base.ReadC();
			this.int_2 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (string.IsNullOrEmpty(player.Nickname) || this.int_1 > 2)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK(EventErrorEnum.VISIT_EVENT_USERFAIL));
					}
					else
					{
						PlayerEvent @event = player.Event;
						if (@event == null)
						{
							this.eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_UNKNOWN;
						}
						else
						{
							uint uInt32 = uint.Parse(DateTimeUtil.Now("yyMMdd"));
							uint uInt321 = uint.Parse(string.Format("{0:yyMMdd}", DateTimeUtil.Convert(string.Format("{0}", @event.LastVisitDate))));
							int int2 = this.int_2 + 1;
							if (@event.LastVisitCheckDay < int2 || uInt321 < uInt32)
							{
								this.eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_ALREADYCHECK;
							}
							else
							{
								EventVisitModel eventVisitModel = EventVisitXML.GetEvent(this.int_0);
								if (eventVisitModel == null)
								{
									this.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK(EventErrorEnum.VISIT_EVENT_UNKNOWN));
									return;
								}
								else if (eventVisitModel.Boxes[this.int_2] == null || !eventVisitModel.EventIsEnabled())
								{
									this.eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_WRONGVERSION;
								}
								else
								{
									List<VisitItemModel> reward = eventVisitModel.GetReward(this.int_2, this.int_1);
									if (reward.Count <= 0)
									{
										this.eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_UNKNOWN;
									}
									else if (!this.method_0(player, reward))
									{
										this.eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_NOTENOUGH;
									}
									else
									{
										@event.LastVisitCheckDay = int2;
										@event.LastVisitSeqType = this.int_1;
										@event.LastVisitDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
										ComDiv.UpdateDB("player_events", "owner_id", player.PlayerId, new string[] { "last_visit_check_day", "last_visit_seq_type", "last_visit_date" }, new object[] { @event.LastVisitCheckDay, @event.LastVisitSeqType, (long)((ulong)@event.LastVisitDate) });
									}
								}
							}
						}
						this.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK(this.eventErrorEnum_0));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}