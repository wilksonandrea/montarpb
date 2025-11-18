// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Managers;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly uint uint_0;
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 3650);
    this.WriteD(((PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_ACK) this).int_0);
  }

  public PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK(Account string_3)
  {
    ((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0 = string_3;
    if (string_3 == null)
      return;
    ((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).statisticAcemode_0 = string_3.Statistic.Acemode;
    ((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).clanModel_0 = ClanManager.GetClan(string_3.ClanId);
  }
}
