// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_GET_UID_ROOM_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.XML;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_UID_ROOM_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 2400);
    this.WriteB(((PROTOCOL_BASE_NOTICE_ACK) this).method_0(SChannelXML.Servers));
    this.WriteC((byte) 0);
  }
}
