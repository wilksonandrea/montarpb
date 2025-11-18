namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ : GameClientPacket
    {
        private EventErrorEnum eventErrorEnum_0 = ((EventErrorEnum) (-2147478268));
        private int int_0;
        private int int_1;
        private int int_2;

        private bool method_0(Account account_0, List<VisitItemModel> list_0)
        {
            try
            {
                int num = 0;
                using (List<VisitItemModel>.Enumerator enumerator = list_0.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        GoodsItem good = ShopManager.GetGood(enumerator.Current.GoodId);
                        if (good != null)
                        {
                            if ((ComDiv.GetIdStatics(good.Item.Id, 1) == 6) && (account_0.Character.GetCharacter(good.Item.Id) == null))
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
                }
                return (num > 0);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
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
                Account player = base.Client.Player;
                if (player != null)
                {
                    if (string.IsNullOrEmpty(player.Nickname) || (this.int_1 > 2))
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK((EventErrorEnum) (-2147478272)));
                    }
                    else
                    {
                        PlayerEvent event2 = player.Event;
                        if (event2 == null)
                        {
                            this.eventErrorEnum_0 = (EventErrorEnum) (-2147478267);
                        }
                        else
                        {
                            uint num = uint.Parse(DateTimeUtil.Now("yyMMdd"));
                            uint num2 = uint.Parse($"{DateTimeUtil.Convert($"{event2.LastVisitDate}"):yyMMdd}");
                            int num3 = this.int_2 + 1;
                            if ((event2.LastVisitCheckDay < num3) || (num2 < num))
                            {
                                this.eventErrorEnum_0 = (EventErrorEnum) (-2147478270);
                            }
                            else
                            {
                                EventVisitModel model = EventVisitXML.GetEvent(this.int_0);
                                if (model != null)
                                {
                                    if ((model.Boxes[this.int_2] == null) || !model.EventIsEnabled())
                                    {
                                        this.eventErrorEnum_0 = (EventErrorEnum) (-2147478269);
                                    }
                                    else
                                    {
                                        List<VisitItemModel> reward = model.GetReward(this.int_2, this.int_1);
                                        if (reward.Count <= 0)
                                        {
                                            this.eventErrorEnum_0 = (EventErrorEnum) (-2147478267);
                                        }
                                        else if (!this.method_0(player, reward))
                                        {
                                            this.eventErrorEnum_0 = (EventErrorEnum) (-2147478271);
                                        }
                                        else
                                        {
                                            event2.LastVisitCheckDay = num3;
                                            event2.LastVisitSeqType = this.int_1;
                                            event2.LastVisitDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
                                            string[] cOLUMNS = new string[] { "last_visit_check_day", "last_visit_seq_type", "last_visit_date" };
                                            object[] vALUES = new object[] { event2.LastVisitCheckDay, event2.LastVisitSeqType, event2.LastVisitDate };
                                            ComDiv.UpdateDB("player_events", "owner_id", player.PlayerId, cOLUMNS, vALUES);
                                        }
                                    }
                                }
                                else
                                {
                                    base.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK((EventErrorEnum) (-2147478267)));
                                    return;
                                }
                            }
                        }
                        base.Client.SendPacket(new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK(this.eventErrorEnum_0));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

