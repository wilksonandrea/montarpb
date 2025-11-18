// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CHAR_DELETE_CHARA_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CHAR_DELETE_CHARA_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly int int_0;
  private readonly ItemsModel itemsModel_0;
  private readonly CharacterModel characterModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 7430);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_BATTLEBOX_AUTH_ACK) this).uint_0);
    if (((PROTOCOL_BATTLEBOX_AUTH_ACK) this).uint_0 != 0U)
      return;
    this.WriteD(((PROTOCOL_BATTLEBOX_AUTH_ACK) this).int_0);
    this.WriteB(new byte[5]);
    this.WriteD(((PROTOCOL_BATTLEBOX_AUTH_ACK) this).account_0.Tags);
  }

  public PROTOCOL_CHAR_DELETE_CHARA_ACK([In] uint obj0)
  {
    ((PROTOCOL_BATTLE_GM_PAUSE_ACK) this).uint_0 = obj0;
  }
}
