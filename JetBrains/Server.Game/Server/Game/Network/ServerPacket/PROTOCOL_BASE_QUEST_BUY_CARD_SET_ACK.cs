// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly EventErrorEnum eventErrorEnum_0;

  public virtual void Write()
  {
    this.WriteH((short) 3681);
    this.WriteH((short) 0);
    this.WriteD((uint) ((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.PlayerId);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).statisticAcemode_0.Matches);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).statisticAcemode_0.MatchWins);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).statisticAcemode_0.MatchLoses);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).statisticAcemode_0.Kills);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).statisticAcemode_0.Deaths);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).statisticAcemode_0.Headshots);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).statisticAcemode_0.Assists);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).statisticAcemode_0.Escapes);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).statisticAcemode_0.Winstreaks);
    this.WriteB(new byte[122]);
    this.WriteU(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.Nickname, 66);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.Rank);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.GetRank());
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.Gold);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.Exp);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.Tags);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.Cash);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.ClanId);
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.ClanAccess);
    this.WriteQ(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.StatusId());
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.CafePC);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.Country);
    this.WriteU(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).clanModel_0.Name, 34);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).clanModel_0.Rank);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).clanModel_0.GetClanUnit());
    this.WriteD(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).clanModel_0.Logo);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).clanModel_0.NameColor);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).clanModel_0.Effect);
    this.WriteC(GameXender.Client.Config.EnableBlood ? (byte) ((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK) this).account_0.Age : (byte) 24);
  }

  public PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK([In] Account obj0)
  {
    ((PROTOCOL_BASE_MEDAL_GET_INFO_ACK) this).account_0 = obj0;
  }
}
