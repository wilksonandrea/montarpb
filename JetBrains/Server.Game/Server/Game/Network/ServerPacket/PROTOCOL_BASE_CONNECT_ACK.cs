// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_CONNECT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_CONNECT_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;
  private readonly ushort ushort_0;
  private readonly List<byte[]> list_0;

  public virtual void Write()
  {
    this.WriteH((short) 1062);
    this.WriteD(((PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK) this).uint_0);
  }

  public PROTOCOL_BASE_CONNECT_ACK(
    EventErrorEnum messageModel_1,
    EventVisitModel int_1,
    [In] PlayerEvent obj2)
  {
    ((PROTOCOL_BASE_ATTENDANCE_ACK) this).eventErrorEnum_0 = messageModel_1;
    ((PROTOCOL_BASE_ATTENDANCE_ACK) this).eventVisitModel_0 = int_1;
    ((PROTOCOL_BASE_ATTENDANCE_ACK) this).playerEvent_0 = obj2;
    if (obj2 == null)
      return;
    ((PROTOCOL_BASE_ATTENDANCE_ACK) this).uint_0 = uint.Parse(DateTimeUtil.Now("yyMMdd"));
    ((PROTOCOL_BASE_ATTENDANCE_ACK) this).uint_1 = uint.Parse($"{DateTimeUtil.Convert($"{obj2.LastVisitDate}"):yyMMdd}");
  }
}
