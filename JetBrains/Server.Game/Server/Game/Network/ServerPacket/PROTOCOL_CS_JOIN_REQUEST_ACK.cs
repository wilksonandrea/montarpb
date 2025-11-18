// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_JOIN_REQUEST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Network;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_JOIN_REQUEST_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly uint uint_0;

  private byte[] method_0([In] Account obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      if (obj0 != null)
      {
        syncServerPacket.WriteU(obj0.Nickname, 66);
        syncServerPacket.WriteC((byte) obj0.NickColor);
        syncServerPacket.WriteC((byte) obj0.Rank);
      }
      else
      {
        syncServerPacket.WriteU("", 66);
        syncServerPacket.WriteC((byte) 0);
        syncServerPacket.WriteC((byte) 0);
      }
      return syncServerPacket.ToArray();
    }
  }

  public PROTOCOL_CS_JOIN_REQUEST_ACK(uint int_1)
  {
    ((PROTOCOL_CS_INVITE_ACCEPT_ACK) this).uint_0 = int_1;
  }
}
