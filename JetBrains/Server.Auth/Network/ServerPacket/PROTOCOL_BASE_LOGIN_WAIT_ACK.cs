// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_LOGIN_WAIT_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Network;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_LOGIN_WAIT_ACK : AuthServerPacket
{
  private readonly int int_0;

  private byte[] method_3([In] int obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) obj0);
      for (int index = 0; index < obj0; ++index)
      {
        syncServerPacket.WriteC((byte) 0);
        syncServerPacket.WriteC((byte) 3);
        syncServerPacket.WriteB(new byte[43]);
      }
      return syncServerPacket.ToArray();
    }
  }

  private byte[] method_4([In] int obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) obj0);
      for (int index = 0; index < obj0; ++index)
        syncServerPacket.WriteB(new byte[45]);
      return syncServerPacket.ToArray();
    }
  }
}
