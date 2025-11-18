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

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ : GameClientPacket
{
	private EventErrorEnum eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_SUCCESS;

	private int int_0;

	private int int_1;

	private int int_2;

	public override void Read()
	{
		int_0 = ReadD();
		int_1 = ReadC();
		int_2 = ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			if (!string.IsNullOrEmpty(player.Nickname) && int_1 <= 2)
			{
				PlayerEvent @event = player.Event;
				if (@event != null)
				{
					uint num = uint.Parse(DateTimeUtil.Now("yyMMdd"));
					uint num2 = uint.Parse($"{DateTimeUtil.Convert($"{@event.LastVisitDate}"):yyMMdd}");
					int num3 = int_2 + 1;
					if (@event.LastVisitCheckDay >= num3 && num2 >= num)
					{
						EventVisitModel event2 = EventVisitXML.GetEvent(int_0);
						if (event2 == null)
						{
							Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK(EventErrorEnum.VISIT_EVENT_UNKNOWN));
							return;
						}
						if (event2.Boxes[int_2] != null && event2.EventIsEnabled())
						{
							List<VisitItemModel> reward = event2.GetReward(int_2, int_1);
							if (reward.Count > 0)
							{
								if (method_0(player, reward))
								{
									@event.LastVisitCheckDay = num3;
									@event.LastVisitSeqType = int_1;
									@event.LastVisitDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
									ComDiv.UpdateDB("player_events", "owner_id", player.PlayerId, new string[3] { "last_visit_check_day", "last_visit_seq_type", "last_visit_date" }, @event.LastVisitCheckDay, @event.LastVisitSeqType, (long)@event.LastVisitDate);
								}
								else
								{
									eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_NOTENOUGH;
								}
							}
							else
							{
								eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_UNKNOWN;
							}
						}
						else
						{
							eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_WRONGVERSION;
						}
					}
					else
					{
						eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_ALREADYCHECK;
					}
				}
				else
				{
					eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_UNKNOWN;
				}
				Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK(eventErrorEnum_0));
			}
			else
			{
				Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK(EventErrorEnum.VISIT_EVENT_USERFAIL));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	private bool method_0(Account account_0, List<VisitItemModel> list_0)
	{
		try
		{
			int num = 0;
			foreach (VisitItemModel item in list_0)
			{
				GoodsItem good = ShopManager.GetGood(item.GoodId);
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
				return true;
			}
			return false;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}
}
