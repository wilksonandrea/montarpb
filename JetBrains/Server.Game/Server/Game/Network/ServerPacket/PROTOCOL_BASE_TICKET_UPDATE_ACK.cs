// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_TICKET_UPDATE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_TICKET_UPDATE_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 2411);
    this.WriteD(((PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK) this).list_0.Count);
    this.WriteD(((PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK) this).list_1.Count);
    this.WriteD(((PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK) this).list_2.Count);
    this.WriteD(0);
    foreach (ItemsModel itemsModel in ((PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK) this).list_0)
    {
      this.WriteQ(itemsModel.ObjectId);
      this.WriteD(itemsModel.Id);
      this.WriteC((byte) itemsModel.Equip);
      this.WriteD(itemsModel.Count);
    }
    foreach (ItemsModel itemsModel in ((PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK) this).list_1)
    {
      this.WriteQ(itemsModel.ObjectId);
      this.WriteD(itemsModel.Id);
      this.WriteC((byte) itemsModel.Equip);
      this.WriteD(itemsModel.Count);
    }
    foreach (ItemsModel itemsModel in ((PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK) this).list_2)
    {
      this.WriteQ(itemsModel.ObjectId);
      this.WriteD(itemsModel.Id);
      this.WriteC((byte) itemsModel.Equip);
      this.WriteD(itemsModel.Count);
    }
  }

  public PROTOCOL_BASE_TICKET_UPDATE_ACK(string bool_1, [In] string obj1)
  {
    ((PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK) this).string_0 = bool_1;
    ((PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK) this).string_1 = obj1;
  }
}
