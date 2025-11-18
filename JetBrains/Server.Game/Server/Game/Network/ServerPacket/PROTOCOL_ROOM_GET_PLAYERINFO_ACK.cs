// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_GET_PLAYERINFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_PLAYERINFO_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly Account account_0;
  private readonly PlayerInventory playerInventory_0;
  private readonly PlayerEquipment playerEquipment_0;
  private readonly StatisticTotal statisticTotal_0;
  private readonly StatisticSeason statisticSeason_0;
  private readonly ClanModel clanModel_0;
  private readonly int[] int_0;

  public PROTOCOL_ROOM_GET_PLAYERINFO_ACK([In] ChannelModel obj0)
  {
    ((PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK) this).list_0 = ((MatchModel) obj0).GetWaitPlayers();
    ((PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK) this).list_1 = ((PROTOCOL_ROOM_GET_RANK_ACK) this).method_0(((PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK) this).list_0.Count, ((PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK) this).list_0.Count >= 8 ? 8 : ((PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK) this).list_0.Count);
  }

  public virtual void Write()
  {
    this.WriteH((short) 3676);
    this.WriteD(((PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK) this).list_1.Count);
    foreach (int index in ((PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK) this).list_1)
    {
      Account account = ((PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK) this).list_0[index];
      this.WriteD(account.GetSessionId());
      this.WriteD(account.GetRank());
      this.WriteC((byte) (account.Nickname.Length + 1));
      this.WriteN(account.Nickname, account.Nickname.Length + 2, "UTF-16LE");
      this.WriteC((byte) account.NickColor);
    }
  }
}
