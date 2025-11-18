// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_COUNT_DOWN_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_COUNT_DOWN_ACK : GameServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 2329);
    this.WriteD(((PROTOCOL_BASE_USER_LEAVE_ACK) this).int_0);
    this.WriteH((short) 0);
  }

  public PROTOCOL_BATTLE_COUNT_DOWN_ACK(ShopData uint_1, int int_1)
  {
    ((PROTOCOL_BATTLEBOX_GET_LIST_ACK) this).shopData_0 = uint_1;
    ((PROTOCOL_BATTLEBOX_GET_LIST_ACK) this).int_0 = int_1;
  }
}
