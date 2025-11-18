// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_USER_TITLE_CHANGE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_USER_TITLE_CHANGE_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 2466);
    this.WriteH((short) 514);
    this.WriteH((ushort) ((PROTOCOL_BASE_URL_LIST_ACK) this).serverConfig_0.OfficialBanner.Length);
    this.WriteD(0);
    this.WriteC((byte) 2);
    this.WriteN(((PROTOCOL_BASE_URL_LIST_ACK) this).serverConfig_0.OfficialBanner, ((PROTOCOL_BASE_URL_LIST_ACK) this).serverConfig_0.OfficialBanner.Length, "UTF-16LE");
    this.WriteQ(0L);
  }
}
