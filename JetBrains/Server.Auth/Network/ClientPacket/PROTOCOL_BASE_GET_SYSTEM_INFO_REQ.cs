// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_BASE_GET_SYSTEM_INFO_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_GET_SYSTEM_INFO_REQ : AuthClientPacket
{
  private byte[] method_0(int value, [In] ref int obj1, [In] List<MessageModel> obj2)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      for (int index = value * 25; index < obj2.Count; ++index)
      {
        this.method_1(obj2[index], syncServerPacket);
        if (++obj1 == 25)
          break;
      }
      return syncServerPacket.ToArray();
    }
  }

  private void method_1(MessageModel string_0, [In] SyncServerPacket obj1)
  {
    obj1.WriteD((uint) string_0.ObjectId);
    obj1.WriteQ(string_0.SenderId);
    obj1.WriteC((byte) string_0.Type);
    obj1.WriteC((byte) string_0.State);
    obj1.WriteC((byte) string_0.DaysRemaining);
    obj1.WriteD(string_0.ClanId);
  }

  private byte[] method_2(int int_0, ref int int_1, List<MessageModel> list_0)
  {
    using (SyncServerPacket syncServerPacket_0 = new SyncServerPacket())
    {
      for (int index = int_0 * 25; index < list_0.Count; ++index)
      {
        ((PROTOCOL_BASE_GET_USER_INFO_REQ) this).method_3(list_0[index], syncServerPacket_0);
        if (++int_1 == 25)
          break;
      }
      return syncServerPacket_0.ToArray();
    }
  }
}
