namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_ATTENDANCE_REQ : GameClientPacket
    {
        private EventErrorEnum eventErrorEnum_0 = ((EventErrorEnum) (-2147478268));
        private int int_0;
        private int int_1;
        private EventVisitModel eventVisitModel_0;

        public override void Read()
        {
            this.int_0 = base.ReadD();
            this.int_1 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    if (!string.IsNullOrEmpty(player.Nickname))
                    {
                        PlayerEvent event2 = player.Event;
                        if (event2 == null)
                        {
                            this.eventErrorEnum_0 = (EventErrorEnum) (-2147478267);
                        }
                        else
                        {
                            int num2 = this.int_1 + 1;
                            if ((uint.Parse($"{DateTimeUtil.Convert($"{event2.LastVisitDate}"):yyMMdd}") >= uint.Parse(DateTimeUtil.Now("yyMMdd"))) || (event2.LastVisitCheckDay >= num2))
                            {
                                this.eventErrorEnum_0 = (EventErrorEnum) (-2147478270);
                            }
                            else
                            {
                                this.eventVisitModel_0 = EventVisitXML.GetEvent(this.int_0);
                                if (this.eventVisitModel_0 != null)
                                {
                                    if ((this.eventVisitModel_0.Boxes[this.int_1] == null) || !this.eventVisitModel_0.EventIsEnabled())
                                    {
                                        this.eventErrorEnum_0 = (EventErrorEnum) (-2147478269);
                                    }
                                    else
                                    {
                                        event2.LastVisitCheckDay = num2;
                                        event2.LastVisitDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
                                        string[] cOLUMNS = new string[] { "last_visit_check_day", "last_visit_date" };
                                        object[] vALUES = new object[] { event2.LastVisitCheckDay, event2.LastVisitDate };
                                        ComDiv.UpdateDB("player_events", "owner_id", player.PlayerId, cOLUMNS, vALUES);
                                    }
                                }
                                else
                                {
                                    base.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_ACK((EventErrorEnum) (-2147478267), null, null));
                                    return;
                                }
                            }
                        }
                        base.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_ACK(this.eventErrorEnum_0, this.eventVisitModel_0, event2));
                    }
                    else
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_ACK((EventErrorEnum) (-2147478272), null, null));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_ATTENDANCE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

