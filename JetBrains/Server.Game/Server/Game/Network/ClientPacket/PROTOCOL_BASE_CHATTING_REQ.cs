// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_CHATTING_REQ
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

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_CHATTING_REQ : GameClientPacket
{
  private string string_0;
  private ChattingType chattingType_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (string.IsNullOrEmpty(player.Nickname))
      {
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_CONNECT_ACK(EventErrorEnum.VISIT_EVENT_USERFAIL, (EventVisitModel) null, (PlayerEvent) null));
      }
      else
      {
        PlayerEvent playerEvent = player.Event;
        if (playerEvent != null)
        {
          uint num1 = uint.Parse(DateTimeUtil.Now("yyMMdd"));
          int num2 = (int) uint.Parse($"{DateTimeUtil.Convert($"{playerEvent.LastVisitDate}"):yyMMdd}");
          int num3 = ((PROTOCOL_BASE_ATTENDANCE_REQ) this).int_1 + 1;
          int num4 = (int) num1;
          if ((uint) num2 < (uint) num4 && playerEvent.LastVisitCheckDay < num3)
          {
            ((PROTOCOL_BASE_ATTENDANCE_REQ) this).eventVisitModel_0 = EventVisitXML.GetEvent(((PROTOCOL_BASE_ATTENDANCE_REQ) this).int_0);
            if (((PROTOCOL_BASE_ATTENDANCE_REQ) this).eventVisitModel_0 == null)
            {
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_CONNECT_ACK(EventErrorEnum.VISIT_EVENT_UNKNOWN, (EventVisitModel) null, (PlayerEvent) null));
              return;
            }
            if (((PROTOCOL_BASE_ATTENDANCE_REQ) this).eventVisitModel_0.Boxes[((PROTOCOL_BASE_ATTENDANCE_REQ) this).int_1] != null && ((PROTOCOL_BASE_ATTENDANCE_REQ) this).eventVisitModel_0.EventIsEnabled())
            {
              playerEvent.LastVisitCheckDay = num3;
              playerEvent.LastVisitDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
              ComDiv.UpdateDB("player_events", "owner_id", (object) player.PlayerId, new string[2]
              {
                "last_visit_check_day",
                "last_visit_date"
              }, new object[2]
              {
                (object) playerEvent.LastVisitCheckDay,
                (object) (long) playerEvent.LastVisitDate
              });
            }
            else
              ((PROTOCOL_BASE_ATTENDANCE_REQ) this).eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_WRONGVERSION;
          }
          else
            ((PROTOCOL_BASE_ATTENDANCE_REQ) this).eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_ALREADYCHECK;
        }
        else
          ((PROTOCOL_BASE_ATTENDANCE_REQ) this).eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_UNKNOWN;
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_CONNECT_ACK(((PROTOCOL_BASE_ATTENDANCE_REQ) this).eventErrorEnum_0, ((PROTOCOL_BASE_ATTENDANCE_REQ) this).eventVisitModel_0, playerEvent));
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_ATTENDANCE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_BASE_CHATTING_REQ()
  {
    ((PROTOCOL_BASE_ATTENDANCE_REQ) this).eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_SUCCESS;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
  }
}
