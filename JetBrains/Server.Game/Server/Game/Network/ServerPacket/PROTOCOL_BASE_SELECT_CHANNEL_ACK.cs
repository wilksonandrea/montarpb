// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_SELECT_CHANNEL_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_SELECT_CHANNEL_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly ushort ushort_0;
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 2430);
    this.WriteH((short) 0);
    this.WriteH((ushort) ((PROTOCOL_BASE_NEW_REWARD_POPUP_ACK) this).playerInventory_0.Items.Count);
    this.WriteC((byte) 1);
    this.WriteD(((PROTOCOL_BASE_NEW_REWARD_POPUP_ACK) this).itemsModel_0.Id);
  }
}
