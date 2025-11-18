// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_NOTICE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_NOTICE_ACK : GameServerPacket
{
  private readonly ServerConfig serverConfig_0;
  private readonly string string_0;
  private readonly string string_1;

  private byte[] method_0(List<SChannelModel> Name)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) Name.Count);
      foreach (SChannelModel schannelModel in Name)
      {
        syncServerPacket.WriteD(schannelModel.State ? 1 : 0);
        syncServerPacket.WriteB(ComDiv.AddressBytes(schannelModel.Host));
        syncServerPacket.WriteB(ComDiv.AddressBytes(schannelModel.Host));
        syncServerPacket.WriteH(schannelModel.Port);
        syncServerPacket.WriteC((byte) schannelModel.Type);
        syncServerPacket.WriteH((ushort) schannelModel.MaxPlayers);
        syncServerPacket.WriteD(schannelModel.LastPlayers);
        if (schannelModel.Id == 0)
        {
          syncServerPacket.WriteB(Bitwise.HexStringToByteArray("01 01 01 01 01 01 01 01 01 01 0E 00 00 00 00"));
        }
        else
        {
          foreach (ChannelModel channel in AllUtils.GetChannels(schannelModel.Id))
            syncServerPacket.WriteC((byte) channel.Type);
          syncServerPacket.WriteC((byte) schannelModel.Type);
          syncServerPacket.WriteC((byte) 0);
          syncServerPacket.WriteH((short) 0);
        }
      }
      return syncServerPacket.ToArray();
    }
  }
}
