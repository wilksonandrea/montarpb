// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_EVENT_PORTAL_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.XML;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_EVENT_PORTAL_ACK : GameServerPacket
{
  private readonly bool bool_0;

  public virtual void Write()
  {
    this.WriteH((short) 2490);
    this.WriteB(((PROTOCOL_BASE_GAME_SERVER_STATE_ACK) this).method_0(SChannelXML.Servers));
  }
}
