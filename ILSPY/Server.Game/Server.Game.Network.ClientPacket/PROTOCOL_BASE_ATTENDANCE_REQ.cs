using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_ATTENDANCE_REQ : GameClientPacket
{
	private EventErrorEnum eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_SUCCESS;

	private int int_0;

	private int int_1;

	private EventVisitModel eventVisitModel_0;

	public override void Read()
	{
		int_0 = ReadD();
		int_1 = ReadC();
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
			if (string.IsNullOrEmpty(player.Nickname))
			{
				Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_ACK(EventErrorEnum.VISIT_EVENT_USERFAIL, null, null));
				return;
			}
			PlayerEvent @event = player.Event;
			if (@event != null)
			{
				uint num = uint.Parse(DateTimeUtil.Now("yyMMdd"));
				uint num2 = uint.Parse($"{DateTimeUtil.Convert($"{@event.LastVisitDate}"):yyMMdd}");
				int num3 = int_1 + 1;
				if (num2 < num && @event.LastVisitCheckDay < num3)
				{
					eventVisitModel_0 = EventVisitXML.GetEvent(int_0);
					if (eventVisitModel_0 == null)
					{
						Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_ACK(EventErrorEnum.VISIT_EVENT_UNKNOWN, null, null));
						return;
					}
					if (eventVisitModel_0.Boxes[int_1] != null && eventVisitModel_0.EventIsEnabled())
					{
						@event.LastVisitCheckDay = num3;
						@event.LastVisitDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
						ComDiv.UpdateDB("player_events", "owner_id", player.PlayerId, new string[2] { "last_visit_check_day", "last_visit_date" }, @event.LastVisitCheckDay, (long)@event.LastVisitDate);
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
			Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_ACK(eventErrorEnum_0, eventVisitModel_0, @event));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_ATTENDANCE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
