using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000137 RID: 311
	public class PROTOCOL_BASE_ATTENDANCE_REQ : GameClientPacket
	{
		// Token: 0x06000303 RID: 771 RVA: 0x00004EC9 File Offset: 0x000030C9
		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.int_1 = (int)base.ReadC();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000183C4 File Offset: 0x000165C4
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (string.IsNullOrEmpty(player.Nickname))
					{
						this.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_ACK((EventErrorEnum)2147489024U, null, null));
					}
					else
					{
						PlayerEvent @event = player.Event;
						if (@event != null)
						{
							uint num = uint.Parse(DateTimeUtil.Now("yyMMdd"));
							uint num2 = uint.Parse(string.Format("{0:yyMMdd}", DateTimeUtil.Convert(string.Format("{0}", @event.LastVisitDate))));
							int num3 = this.int_1 + 1;
							if (num2 < num && @event.LastVisitCheckDay < num3)
							{
								this.eventVisitModel_0 = EventVisitXML.GetEvent(this.int_0);
								if (this.eventVisitModel_0 == null)
								{
									this.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_ACK((EventErrorEnum)2147489029U, null, null));
									return;
								}
								if (this.eventVisitModel_0.Boxes[this.int_1] != null && this.eventVisitModel_0.EventIsEnabled())
								{
									@event.LastVisitCheckDay = num3;
									@event.LastVisitDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
									ComDiv.UpdateDB("player_events", "owner_id", player.PlayerId, new string[] { "last_visit_check_day", "last_visit_date" }, new object[]
									{
										@event.LastVisitCheckDay,
										(long)((ulong)@event.LastVisitDate)
									});
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
						this.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_ACK(this.eventErrorEnum_0, this.eventVisitModel_0, @event));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_ATTENDANCE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00004EE3 File Offset: 0x000030E3
		public PROTOCOL_BASE_ATTENDANCE_REQ()
		{
		}

		// Token: 0x04000236 RID: 566
		private EventErrorEnum eventErrorEnum_0 = (EventErrorEnum)2147489028U;

		// Token: 0x04000237 RID: 567
		private int int_0;

		// Token: 0x04000238 RID: 568
		private int int_1;

		// Token: 0x04000239 RID: 569
		private EventVisitModel eventVisitModel_0;
	}
}
