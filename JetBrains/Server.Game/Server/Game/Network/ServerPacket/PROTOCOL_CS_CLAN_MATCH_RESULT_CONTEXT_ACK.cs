// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK : GameServerPacket
{
  private readonly int int_0;

  public PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK([In] uint obj0)
  {
    ((PROTOCOL_CS_CHECK_MARK_ACK) this).uint_0 = obj0;
  }

  public virtual void Write()
  {
    this.WriteH((short) 857);
    this.WriteD(((PROTOCOL_CS_CHECK_MARK_ACK) this).uint_0);
  }
}
