// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 6153);
    this.WriteH((short) 0);
    this.WriteD(0);
    this.WriteC((byte) 20);
    this.WriteC((byte) ((PROTOCOL_CHAR_CHANGE_STATE_ACK) this).characterModel_0.Slot);
  }

  public PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK(
    uint uint_1,
    [In] byte obj1,
    [In] CharacterModel obj2,
    [In] Account obj3)
  {
    ((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).uint_0 = uint_1;
    ((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).account_0 = obj3;
    if (obj3 != null)
    {
      ((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerInventory_0 = obj3.Inventory;
      ((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).playerEquipment_0 = obj3.Equipment;
    }
    ((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).characterModel_0 = obj2;
    ((PROTOCOL_CHAR_CREATE_CHARA_ACK) this).byte_0 = obj1;
  }
}
