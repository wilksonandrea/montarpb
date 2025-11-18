// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CHAR_CHANGE_STATE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CHAR_CHANGE_STATE_ACK : GameServerPacket
{
  private readonly CharacterModel characterModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 5277);
    this.WriteD(((PROTOCOL_BATTLE_USER_SOPETYPE_ACK) this).account_0.SlotId);
    this.WriteC((byte) ((PROTOCOL_BATTLE_USER_SOPETYPE_ACK) this).account_0.Sight);
  }

  public PROTOCOL_CHAR_CHANGE_STATE_ACK([In] uint obj0)
  {
    ((PROTOCOL_BATTLE_VOTE_KICKVOTE_ACK) this).uint_0 = obj0;
  }
}
