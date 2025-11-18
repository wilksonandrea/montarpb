using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_ATTENDANCE_REQ : GameClientPacket
	{
		private EventErrorEnum eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_SUCCESS;

		private int int_0;

		private int int_1;

		private EventVisitModel eventVisitModel_0;

		public PROTOCOL_BASE_ATTENDANCE_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.int_1 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (!string.IsNullOrEmpty(player.Nickname))
					{
						PlayerEvent @event = player.Event;
						if (@event == null)
						{
							this.eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_UNKNOWN;
						}
						else
						{
							uint uInt32 = uint.Parse(DateTimeUtil.Now("yyMMdd"));
							int int1 = this.int_1 + 1;
							if (uint.Parse(string.Format("{0:yyMMdd}", DateTimeUtil.Convert(string.Format("{0}", @event.LastVisitDate)))) >= uInt32 || @event.LastVisitCheckDay >= int1)
							{
								this.eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_ALREADYCHECK;
							}
							else
							{
								this.eventVisitModel_0 = EventVisitXML.GetEvent(this.int_0);
								if (this.eventVisitModel_0 == null)
								{
									this.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_ACK(EventErrorEnum.VISIT_EVENT_UNKNOWN, null, null));
									return;
								}
								else if (this.eventVisitModel_0.Boxes[this.int_1] == null || !this.eventVisitModel_0.EventIsEnabled())
								{
									this.eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_WRONGVERSION;
								}
								else
								{
									@event.LastVisitCheckDay = int1;
									@event.LastVisitDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
									ComDiv.UpdateDB("player_events", "owner_id", player.PlayerId, new string[] { "last_visit_check_day", "last_visit_date" }, new object[] { @event.LastVisitCheckDay, (long)((ulong)@event.LastVisitDate) });
								}
							}
						}
						this.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_ACK(this.eventErrorEnum_0, this.eventVisitModel_0, @event));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_ACK(EventErrorEnum.VISIT_EVENT_USERFAIL, null, null));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_ATTENDANCE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}