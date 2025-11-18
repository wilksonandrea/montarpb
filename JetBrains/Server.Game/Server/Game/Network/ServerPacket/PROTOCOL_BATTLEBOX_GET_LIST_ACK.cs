// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLEBOX_GET_LIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLEBOX_GET_LIST_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly ShopData shopData_0;

  public virtual void Write()
  {
    this.WriteH((short) 2335);
    this.WriteD(0);
    this.WriteD(((PROTOCOL_BASE_SELECT_CHANNEL_ACK) this).uint_0);
    if (((PROTOCOL_BASE_SELECT_CHANNEL_ACK) this).uint_0 != 0U)
      return;
    this.WriteD(((PROTOCOL_BASE_SELECT_CHANNEL_ACK) this).int_0);
    this.WriteH(((PROTOCOL_BASE_SELECT_CHANNEL_ACK) this).ushort_0);
  }

  public PROTOCOL_BATTLEBOX_GET_LIST_ACK(uint int_2)
  {
    ((PROTOCOL_BASE_USER_ENTER_ACK) this).uint_0 = int_2;
  }
}
