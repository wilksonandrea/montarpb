// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_ATTENDANCE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_ATTENDANCE_REQ : GameClientPacket
{
  private EventErrorEnum eventErrorEnum_0;
  private int int_0;
  private int int_1;
  private EventVisitModel eventVisitModel_0;

  public virtual void Read()
  {
    ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).int_0 = this.ReadD();
    ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).int_1 = (int) this.ReadC();
    ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).int_2 = (int) this.ReadC();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (!string.IsNullOrEmpty(player.Nickname) && ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).int_1 <= 2)
      {
        PlayerEvent playerEvent = player.Event;
        if (playerEvent != null)
        {
          uint num1 = uint.Parse(DateTimeUtil.Now("yyMMdd"));
          uint num2 = uint.Parse($"{DateTimeUtil.Convert($"{playerEvent.LastVisitDate}"):yyMMdd}");
          int num3 = ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).int_2 + 1;
          if (playerEvent.LastVisitCheckDay >= num3 && num2 >= num1)
          {
            EventVisitModel eventVisitModel = EventVisitXML.GetEvent(((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).int_0);
            if (eventVisitModel == null)
            {
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_CREATE_NICK_ACK(EventErrorEnum.VISIT_EVENT_UNKNOWN));
              return;
            }
            if (eventVisitModel.Boxes[((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).int_2] != null && eventVisitModel.EventIsEnabled())
            {
              List<VisitItemModel> reward = eventVisitModel.GetReward(((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).int_2, ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).int_1);
              if (reward.Count > 0)
              {
                if (((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ) this).method_0(player, reward))
                {
                  playerEvent.LastVisitCheckDay = num3;
                  playerEvent.LastVisitSeqType = ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).int_1;
                  playerEvent.LastVisitDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
                  ComDiv.UpdateDB("player_events", "owner_id", (object) player.PlayerId, new string[3]
                  {
                    "last_visit_check_day",
                    "last_visit_seq_type",
                    "last_visit_date"
                  }, new object[3]
                  {
                    (object) playerEvent.LastVisitCheckDay,
                    (object) playerEvent.LastVisitSeqType,
                    (object) (long) playerEvent.LastVisitDate
                  });
                }
                else
                  ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_NOTENOUGH;
              }
              else
                ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_UNKNOWN;
            }
            else
              ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_WRONGVERSION;
          }
          else
            ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_ALREADYCHECK;
        }
        else
          ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_UNKNOWN;
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_CREATE_NICK_ACK(((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).eventErrorEnum_0));
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_CREATE_NICK_ACK(EventErrorEnum.VISIT_EVENT_USERFAIL));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
