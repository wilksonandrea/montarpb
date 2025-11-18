// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_CHANGE_CLAN_EXP_RANK_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CHANGE_CLAN_EXP_RANK_ACK : GameServerPacket
{
  private readonly int int_0;

  public PROTOCOL_CS_CHANGE_CLAN_EXP_RANK_ACK(ClanModel string_2, Account string_3, [In] int obj2)
  {
    ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0 = string_2;
    ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).account_0 = string_3;
    ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).int_0 = obj2;
  }

  public PROTOCOL_CS_CHANGE_CLAN_EXP_RANK_ACK([In] ClanModel obj0, int int_3)
  {
    ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0 = obj0;
    ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).account_0 = ClanManager.GetAccount(obj0.OwnerId, 31 /*0x1F*/);
    ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).int_0 = int_3;
  }
}
