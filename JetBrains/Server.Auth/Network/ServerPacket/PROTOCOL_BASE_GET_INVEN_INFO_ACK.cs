// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_GET_INVEN_INFO_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_GET_INVEN_INFO_ACK : AuthServerPacket
{
  private readonly uint uint_0;
  private readonly int int_0;
  private readonly List<ItemsModel> list_0;

  private byte[] method_0(List<SChannelModel> statisticDaily_1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) statisticDaily_1.Count);
      foreach (SChannelModel schannelModel in statisticDaily_1)
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

  public PROTOCOL_BASE_GET_INVEN_INFO_ACK([In] SChannelModel obj0, List<ChannelModel> byte_1)
  {
    ((PROTOCOL_BASE_GET_CHANNELLIST_ACK) this).schannelModel_0 = obj0;
    ((PROTOCOL_BASE_GET_CHANNELLIST_ACK) this).list_0 = byte_1;
  }
}
