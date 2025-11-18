// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_MEMBER_CONTEXT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_MEMBER_CONTEXT_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;

  public virtual void Write()
  {
    this.WriteH((short) 891);
    this.WriteD(((PROTOCOL_CS_INVITE_ACCEPT_ACK) this).uint_0);
  }

  public PROTOCOL_CS_MEMBER_CONTEXT_ACK(uint uint_1)
  {
    ((PROTOCOL_CS_INVITE_ACK) this).uint_0 = uint_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 889);
    this.WriteD(((PROTOCOL_CS_INVITE_ACK) this).uint_0);
  }
}
