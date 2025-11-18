// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_ACCOUNT_KICK_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_ACCOUNT_KICK_ACK : GameServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 5270);
    this.WriteD(((PROTOCOL_BATTLE_GM_RESUME_ACK) this).uint_0);
  }

  public PROTOCOL_AUTH_ACCOUNT_KICK_ACK(
    uint uint_1,
    int account_1 = default (int),
    Account int_1 = 0,
    [In] ItemsModel obj3)
  {
    ((PROTOCOL_CHAR_DELETE_CHARA_ACK) this).uint_0 = uint_1;
    ((PROTOCOL_CHAR_DELETE_CHARA_ACK) this).int_0 = account_1;
    ((PROTOCOL_CHAR_DELETE_CHARA_ACK) this).itemsModel_0 = obj3;
    if (int_1 == null || obj3 == null)
      return;
    ((PROTOCOL_CHAR_DELETE_CHARA_ACK) this).characterModel_0 = int_1.Character.GetCharacter(obj3.Id);
  }
}
