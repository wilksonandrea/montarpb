// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_GAME_SERVER_STATE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GAME_SERVER_STATE_ACK : GameServerPacket
{
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

  public PROTOCOL_BASE_GAME_SERVER_STATE_ACK(uint disposing)
  {
    ((PROTOCOL_BASE_ENTER_PASS_ACK) this).uint_0 = disposing;
  }

  public virtual void Write()
  {
    this.WriteH((short) 2402);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_BASE_ENTER_PASS_ACK) this).uint_0);
  }
}
