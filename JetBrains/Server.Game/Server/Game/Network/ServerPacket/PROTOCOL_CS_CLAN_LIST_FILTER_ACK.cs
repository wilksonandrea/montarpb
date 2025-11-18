// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_CLAN_LIST_FILTER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Network;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CLAN_LIST_FILTER_ACK : GameServerPacket
{
  private readonly byte byte_0;
  private readonly int int_0;
  private readonly byte[] byte_1;

  private byte[] method_0(Account uint_1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      if (uint_1 != null)
      {
        syncServerPacket.WriteC((byte) (uint_1.Nickname.Length + 1));
        syncServerPacket.WriteN(uint_1.Nickname, uint_1.Nickname.Length + 2, "UTF-16LE");
      }
      else
        syncServerPacket.WriteC((byte) 0);
      return syncServerPacket.ToArray();
    }
  }

  public PROTOCOL_CS_CLAN_LIST_FILTER_ACK(int uint_1)
  {
    ((PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK) this).int_0 = uint_1;
  }
}
