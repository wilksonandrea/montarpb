// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_LOGIN_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_LOGIN_ACK : AuthServerPacket
{
  private readonly EventErrorEnum eventErrorEnum_0;
  private readonly Account account_0;
  private readonly uint uint_0;

  private byte[] method_0(Account serverConfig_1, [In] EventVisitModel obj1, [In] uint obj2)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      PlayerEvent playerEvent = serverConfig_1.Event;
      if (obj1 != null && playerEvent != null && obj1.EventIsEnabled())
      {
        uint num1 = uint.Parse($"{DateTimeUtil.Convert($"{obj2}"):yyMMdd}");
        uint num2 = uint.Parse($"{DateTimeUtil.Convert($"{playerEvent.LastVisitDate}"):yyMMdd}");
        syncServerPacket.WriteD(obj1.Id);
        syncServerPacket.WriteC((byte) playerEvent.LastVisitCheckDay);
        syncServerPacket.WriteC((byte) (playerEvent.LastVisitCheckDay - 1));
        syncServerPacket.WriteC(num2 < num1 ? (byte) 1 : (byte) 2);
        syncServerPacket.WriteC((byte) playerEvent.LastVisitSeqType);
        syncServerPacket.WriteC((byte) 1);
      }
      else
        syncServerPacket.WriteB(new byte[9]);
      return syncServerPacket.ToArray();
    }
  }

  private byte[] method_1(Account eventPlaytimeModel_1, [In] EventVisitModel obj1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      PlayerEvent playerEvent = eventPlaytimeModel_1.Event;
      if (obj1 != null && obj1.EventIsEnabled())
      {
        EventVisitModel eventVisitModel = EventVisitXML.GetEvent(obj1.Id + 1);
        syncServerPacket.WriteU(obj1.Title, 70);
        syncServerPacket.WriteC((byte) playerEvent.LastVisitCheckDay);
        syncServerPacket.WriteC((byte) obj1.Checks);
        syncServerPacket.WriteD(obj1.Id);
        syncServerPacket.WriteD(obj1.BeginDate);
        syncServerPacket.WriteD(obj1.EndedDate);
        syncServerPacket.WriteD(eventVisitModel != null ? eventVisitModel.BeginDate : 0U);
        syncServerPacket.WriteD(eventVisitModel != null ? eventVisitModel.EndedDate : 0U);
        syncServerPacket.WriteD(0);
        for (int index = 0; index < 31 /*0x1F*/; ++index)
        {
          VisitBoxModel box = obj1.Boxes[index];
          syncServerPacket.WriteC((byte) box.IsBothReward);
          syncServerPacket.WriteC((byte) box.RewardCount);
          syncServerPacket.WriteD(box.Reward1.GoodId);
          syncServerPacket.WriteD(box.Reward2.GoodId);
        }
      }
      else
        syncServerPacket.WriteB(new byte[406]);
      return syncServerPacket.ToArray();
    }
  }

  private byte[] method_2(List<QuickstartModel> account_1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) account_1.Count);
      foreach (QuickstartModel quickstartModel in account_1)
      {
        syncServerPacket.WriteC((byte) quickstartModel.MapId);
        syncServerPacket.WriteC((byte) quickstartModel.Rule);
        syncServerPacket.WriteC((byte) quickstartModel.StageOptions);
        syncServerPacket.WriteC((byte) quickstartModel.Type);
      }
      return syncServerPacket.ToArray();
    }
  }
}
