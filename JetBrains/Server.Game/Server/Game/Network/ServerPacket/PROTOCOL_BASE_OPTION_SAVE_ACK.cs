// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_OPTION_SAVE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_OPTION_SAVE_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 2395);
    this.WriteC((byte) ((PROTOCOL_BASE_INV_ITEM_DATA_ACK) this).int_0);
    this.WriteC((byte) ((PROTOCOL_BASE_INV_ITEM_DATA_ACK) this).account_0.NickColor);
    this.WriteD(((PROTOCOL_BASE_INV_ITEM_DATA_ACK) this).account_0.Bonus.FakeRank);
    this.WriteD(((PROTOCOL_BASE_INV_ITEM_DATA_ACK) this).account_0.Bonus.FakeRank);
    this.WriteU(((PROTOCOL_BASE_INV_ITEM_DATA_ACK) this).account_0.Bonus.FakeNick, 66);
    this.WriteH((short) ((PROTOCOL_BASE_INV_ITEM_DATA_ACK) this).account_0.Bonus.CrosshairColor);
    this.WriteH((short) ((PROTOCOL_BASE_INV_ITEM_DATA_ACK) this).account_0.Bonus.MuzzleColor);
    this.WriteC((byte) ((PROTOCOL_BASE_INV_ITEM_DATA_ACK) this).account_0.Bonus.NickBorderColor);
  }
}
