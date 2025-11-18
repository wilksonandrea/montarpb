// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_MESSENGER_NOTE_DELETE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Utility;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_MESSENGER_NOTE_DELETE_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly List<object> list_0;

  public virtual void Write()
  {
    this.WriteH((short) 7700);
    this.WriteH((short) 0);
    this.WriteD(2);
    this.WriteD(((PROTOCOL_MATCH_CLAN_SEASON_ACK) this).bool_0 ? 1 : 0);
    this.WriteB(ComDiv.AddressBytes("127.0.0.1"));
    this.WriteB(ComDiv.AddressBytes("255.255.255.255"));
    this.WriteB(new byte[109]);
    this.WriteB(ComDiv.AddressBytes("127.0.0.1"));
    this.WriteB(ComDiv.AddressBytes("127.0.0.1"));
    this.WriteB(ComDiv.AddressBytes("255.255.255.255"));
    this.WriteB(new byte[103]);
  }

  public PROTOCOL_MESSENGER_NOTE_DELETE_ACK([In] short obj0)
  {
    ((PROTOCOL_MATCH_SERVER_IDX_ACK) this).short_0 = obj0;
  }
}
