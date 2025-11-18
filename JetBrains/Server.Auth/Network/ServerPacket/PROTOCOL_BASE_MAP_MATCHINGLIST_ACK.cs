// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_MAP_MATCHINGLIST_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_MAP_MATCHINGLIST_ACK : AuthServerPacket
{
  private readonly List<MapMatch> list_0;
  private readonly int int_0;

  private byte[] method_0(EventErrorEnum int_0, [In] Account obj1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      if (int_0.Equals((object) EventErrorEnum.SUCCESS))
      {
        syncServerPacket.WriteC((byte) $"{obj1.PlayerId}".Length);
        syncServerPacket.WriteS($"{obj1.PlayerId}", $"{obj1.PlayerId}".Length);
        syncServerPacket.WriteC((byte) 0);
        syncServerPacket.WriteC((byte) obj1.Username.Length);
        syncServerPacket.WriteS(obj1.Username, obj1.Username.Length);
        syncServerPacket.WriteQ(obj1.PlayerId);
      }
      else
        syncServerPacket.WriteB(Bitwise.HexStringToByteArray("00 00 00 00 00 00 00 00 00 00 00"));
      return syncServerPacket.ToArray();
    }
  }

  public PROTOCOL_BASE_MAP_MATCHINGLIST_ACK(int eventErrorEnum_1)
  {
    ((PROTOCOL_BASE_LOGIN_WAIT_ACK) this).int_0 = eventErrorEnum_1;
  }
}
