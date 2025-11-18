// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SERVER_MESSAGE_ITEM_RECEIVE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Utility;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_ITEM_RECEIVE_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 3074);
    this.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
    this.WriteD(((PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK) this).uint_0);
    this.WriteD(((PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK) this).bool_0 ? 1 : 0);
    if (!((PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK) this).bool_0)
      return;
    this.WriteD(0);
  }

  public PROTOCOL_SERVER_MESSAGE_ITEM_RECEIVE_ACK(uint string_1)
  {
    ((PROTOCOL_SERVER_MESSAGE_ERROR_ACK) this).uint_0 = string_1;
  }
}
