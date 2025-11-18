// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK : GameServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 1077);
    this.WriteH((short) 0);
    if (((PROTOCOL_SHOP_REPAIR_ACK) this).uint_0 == 1U)
    {
      this.WriteC((byte) ((PROTOCOL_SHOP_REPAIR_ACK) this).list_0.Count);
      foreach (ItemsModel itemsModel in ((PROTOCOL_SHOP_REPAIR_ACK) this).list_0)
      {
        this.WriteD((uint) itemsModel.ObjectId);
        this.WriteD(itemsModel.Id);
      }
      this.WriteD(((PROTOCOL_SHOP_REPAIR_ACK) this).account_0.Cash);
      this.WriteD(((PROTOCOL_SHOP_REPAIR_ACK) this).account_0.Gold);
    }
    else
      this.WriteD(((PROTOCOL_SHOP_REPAIR_ACK) this).uint_0);
  }
}
