// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 837);
    this.WriteD(((PROTOCOL_CS_COMMISSION_STAFF_ACK) this).uint_0);
  }
}
