// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_DAILY_RECORD_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_DAILY_RECORD_ACK : AuthServerPacket
{
  private readonly StatisticDaily statisticDaily_0;
  private readonly byte byte_0;
  private readonly uint uint_0;

  private byte[] method_0([In] List<SChannelModel> obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) obj0.Count);
      foreach (SChannelModel schannelModel in obj0)
      {
        syncServerPacket.WriteD(schannelModel.State ? 1 : 0);
        syncServerPacket.WriteB(ComDiv.AddressBytes(schannelModel.Host));
        syncServerPacket.WriteH(schannelModel.Port);
        syncServerPacket.WriteC((byte) schannelModel.Type);
        syncServerPacket.WriteH((ushort) schannelModel.MaxPlayers);
        syncServerPacket.WriteD(schannelModel.LastPlayers);
      }
      syncServerPacket.WriteC((byte) 0);
      return syncServerPacket.ToArray();
    }
  }
}
