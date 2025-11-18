// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_ENDBATTLE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_ENDBATTLE_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;
  private readonly Account account_0;
  private readonly ClanModel clanModel_0;
  private readonly int int_0;
  private readonly int int_1;
  private readonly int int_2;
  private readonly bool bool_0;
  private readonly byte[] byte_0;

  public virtual void Write()
  {
    this.WriteH((short) 5173);
    this.WriteC(((PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK) this).roomModel_0.IngameAiLevel);
    for (int index = 0; index < 18; ++index)
      this.WriteD(((PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK) this).roomModel_0.Slots[index].AiLevel);
  }

  public PROTOCOL_BATTLE_ENDBATTLE_ACK(int uint_1)
  {
    ((PROTOCOL_BATTLE_COUNT_DOWN_ACK) this).int_0 = uint_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 5275);
    this.WriteC((byte) ((PROTOCOL_BATTLE_COUNT_DOWN_ACK) this).int_0);
  }

  public PROTOCOL_BATTLE_ENDBATTLE_ACK(RoomModel int_1_1, FragInfos int_1_2, [In] SlotModel obj2)
  {
    ((PROTOCOL_BATTLE_DEATH_ACK) this).roomModel_0 = int_1_1;
    ((PROTOCOL_BATTLE_DEATH_ACK) this).fragInfos_0 = int_1_2;
    ((PROTOCOL_BATTLE_DEATH_ACK) this).slotModel_0 = obj2;
  }

  public virtual void Write()
  {
    this.WriteH((short) 5136);
    this.WriteC((byte) ((PROTOCOL_BATTLE_DEATH_ACK) this).fragInfos_0.KillingType);
    this.WriteC(((PROTOCOL_BATTLE_DEATH_ACK) this).fragInfos_0.KillsCount);
    this.WriteC(((PROTOCOL_BATTLE_DEATH_ACK) this).fragInfos_0.KillerSlot);
    this.WriteD(((PROTOCOL_BATTLE_DEATH_ACK) this).fragInfos_0.WeaponId);
    this.WriteT(((PROTOCOL_BATTLE_DEATH_ACK) this).fragInfos_0.X);
    this.WriteT(((PROTOCOL_BATTLE_DEATH_ACK) this).fragInfos_0.Y);
    this.WriteT(((PROTOCOL_BATTLE_DEATH_ACK) this).fragInfos_0.Z);
    this.WriteC(((PROTOCOL_BATTLE_DEATH_ACK) this).fragInfos_0.Flag);
    this.WriteC(((PROTOCOL_BATTLE_DEATH_ACK) this).fragInfos_0.Unk);
    for (int index = 0; index < (int) ((PROTOCOL_BATTLE_DEATH_ACK) this).fragInfos_0.KillsCount; ++index)
    {
      FragModel frag = ((PROTOCOL_BATTLE_DEATH_ACK) this).fragInfos_0.Frags[index];
      this.WriteC(frag.VictimSlot);
      this.WriteC(frag.WeaponClass);
      this.WriteC(frag.HitspotInfo);
      this.WriteH((ushort) frag.KillFlag);
      this.WriteC(frag.Unk);
      this.WriteT(frag.X);
      this.WriteT(frag.Y);
      this.WriteT(frag.Z);
      this.WriteC(frag.AssistSlot);
      this.WriteB(frag.Unks);
    }
    this.WriteH((ushort) ((PROTOCOL_BATTLE_DEATH_ACK) this).roomModel_0.FRKills);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_DEATH_ACK) this).roomModel_0.FRDeaths);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_DEATH_ACK) this).roomModel_0.FRAssists);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_DEATH_ACK) this).roomModel_0.CTKills);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_DEATH_ACK) this).roomModel_0.CTDeaths);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_DEATH_ACK) this).roomModel_0.CTAssists);
    foreach (SlotModel slot in ((PROTOCOL_BATTLE_DEATH_ACK) this).roomModel_0.Slots)
    {
      this.WriteH((ushort) slot.AllKills);
      this.WriteH((ushort) slot.AllDeaths);
      this.WriteH((ushort) slot.AllAssists);
    }
    this.WriteC(((PROTOCOL_BATTLE_DEATH_ACK) this).slotModel_0.KillsOnLife == 2 ? (byte) 1 : (((PROTOCOL_BATTLE_DEATH_ACK) this).slotModel_0.KillsOnLife == 3 ? (byte) 2 : (((PROTOCOL_BATTLE_DEATH_ACK) this).slotModel_0.KillsOnLife > 3 ? (byte) 3 : (byte) 0)));
    this.WriteH((ushort) ((PROTOCOL_BATTLE_DEATH_ACK) this).fragInfos_0.Score);
    if (!((PROTOCOL_BATTLE_DEATH_ACK) this).roomModel_0.IsDinoMode("DE"))
      return;
    this.WriteH((ushort) ((PROTOCOL_BATTLE_DEATH_ACK) this).roomModel_0.FRDino);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_DEATH_ACK) this).roomModel_0.CTDino);
  }

  public PROTOCOL_BATTLE_ENDBATTLE_ACK(Account roomModel_1)
  {
    this.account_0 = roomModel_1;
    if (roomModel_1 == null)
      return;
    this.roomModel_0 = roomModel_1.Room;
    this.int_0 = this.roomModel_0.RoomType == RoomCondition.Tutorial ? 0 : (int) AllUtils.GetWinnerTeam(this.roomModel_0);
    this.clanModel_0 = ClanManager.GetClan(roomModel_1.ClanId);
    this.bool_0 = this.roomModel_0.IsBotMode();
    AllUtils.GetBattleResult(this.roomModel_0, ref this.int_2, ref this.int_1, ref this.byte_0);
  }

  public PROTOCOL_BATTLE_ENDBATTLE_ACK(
    Account int_1,
    int fragInfos_1,
    int slotModel_1,
    [In] int obj3,
    [In] bool obj4,
    [In] byte[] obj5)
  {
    this.account_0 = int_1;
    this.int_0 = fragInfos_1;
    this.int_1 = slotModel_1;
    this.int_2 = obj3;
    this.bool_0 = obj4;
    this.byte_0 = obj5;
    if (int_1 == null)
      return;
    this.roomModel_0 = int_1.Room;
    this.clanModel_0 = ClanManager.GetClan(int_1.ClanId);
  }

  public PROTOCOL_BATTLE_ENDBATTLE_ACK(
    [In] Account obj0,
    TeamEnum int_3,
    int int_4,
    int int_5,
    bool bool_1,
    byte[] byte_1)
  {
    this.account_0 = obj0;
    this.int_0 = (int) int_3;
    this.int_1 = int_4;
    this.int_2 = int_5;
    this.bool_0 = bool_1;
    this.byte_0 = byte_1;
    if (obj0 == null)
      return;
    this.roomModel_0 = obj0.Room;
    this.clanModel_0 = ClanManager.GetClan(obj0.ClanId);
  }

  public virtual void Write()
  {
    this.WriteH((short) 5140);
    this.WriteD(this.int_1);
    this.WriteC((byte) this.int_0);
    this.WriteB(this.byte_0);
    this.WriteD(this.int_2);
    this.WriteB(this.method_0(this.roomModel_0, this.bool_0));
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteB(new byte[5]);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteH((short) 0);
    this.WriteH((short) 0);
    this.WriteB(new byte[14]);
    this.WriteB(((PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) this).method_1(this.roomModel_0));
    this.WriteB(new byte[27]);
    this.WriteB(((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) this).method_5(this.roomModel_0));
    this.WriteB(((PROTOCOL_BATTLE_GIVEUPBATTLE_ACK) this).method_2(this.roomModel_0));
    this.WriteC((byte) (this.account_0.Nickname.Length * 2));
    this.WriteU(this.account_0.Nickname, this.account_0.Nickname.Length * 2);
    this.WriteD(this.account_0.GetRank());
    this.WriteD(this.account_0.Rank);
    this.WriteD(this.account_0.Gold);
    this.WriteD(this.account_0.Exp);
    this.WriteD(0);
    this.WriteC((byte) 0);
    this.WriteQ(0L);
    this.WriteC((byte) this.account_0.AuthLevel());
    this.WriteC((byte) 0);
    this.WriteD(this.account_0.Tags);
    this.WriteD(this.account_0.Cash);
    this.WriteD(this.clanModel_0.Id);
    this.WriteD(this.account_0.ClanAccess);
    this.WriteQ(this.account_0.StatusId());
    this.WriteC((byte) this.account_0.CafePC);
    this.WriteC((byte) this.account_0.Country);
    this.WriteC((byte) (this.clanModel_0.Name.Length * 2));
    this.WriteU(this.clanModel_0.Name, this.clanModel_0.Name.Length * 2);
    this.WriteC((byte) this.clanModel_0.Rank);
    this.WriteC((byte) this.clanModel_0.GetClanUnit());
    this.WriteD(this.clanModel_0.Logo);
    this.WriteC((byte) this.clanModel_0.NameColor);
    this.WriteC((byte) this.clanModel_0.Effect);
    this.WriteD(this.account_0.Statistic.Season.Matches);
    this.WriteD(this.account_0.Statistic.Season.MatchWins);
    this.WriteD(this.account_0.Statistic.Season.MatchLoses);
    this.WriteD(this.account_0.Statistic.Season.MatchDraws);
    this.WriteD(this.account_0.Statistic.Season.KillsCount);
    this.WriteD(this.account_0.Statistic.Season.HeadshotsCount);
    this.WriteD(this.account_0.Statistic.Season.DeathsCount);
    this.WriteD(this.account_0.Statistic.Season.TotalMatchesCount);
    this.WriteD(this.account_0.Statistic.Season.TotalKillsCount);
    this.WriteD(this.account_0.Statistic.Season.EscapesCount);
    this.WriteD(this.account_0.Statistic.Season.AssistsCount);
    this.WriteD(this.account_0.Statistic.Season.MvpCount);
    this.WriteD(this.account_0.Statistic.Basic.Matches);
    this.WriteD(this.account_0.Statistic.Basic.MatchWins);
    this.WriteD(this.account_0.Statistic.Basic.MatchLoses);
    this.WriteD(this.account_0.Statistic.Basic.MatchDraws);
    this.WriteD(this.account_0.Statistic.Basic.KillsCount);
    this.WriteD(this.account_0.Statistic.Basic.HeadshotsCount);
    this.WriteD(this.account_0.Statistic.Basic.DeathsCount);
    this.WriteD(this.account_0.Statistic.Basic.get_TotalMatchesCount());
    this.WriteD(this.account_0.Statistic.Basic.get_TotalKillsCount());
    this.WriteD(this.account_0.Statistic.Basic.EscapesCount);
    this.WriteD(this.account_0.Statistic.Basic.AssistsCount);
    this.WriteD(this.account_0.Statistic.Basic.MvpCount);
    this.WriteH((ushort) this.account_0.Statistic.Daily.Matches);
    this.WriteH((ushort) this.account_0.Statistic.Daily.get_MatchWins());
    this.WriteH((ushort) this.account_0.Statistic.Daily.get_MatchLoses());
    this.WriteH((ushort) this.account_0.Statistic.Daily.MatchDraws);
    this.WriteH((ushort) this.account_0.Statistic.Daily.KillsCount);
    this.WriteH((ushort) this.account_0.Statistic.Daily.HeadshotsCount);
    this.WriteH((ushort) this.account_0.Statistic.Daily.DeathsCount);
    this.WriteD(this.account_0.Statistic.Daily.ExpGained);
    this.WriteD(this.account_0.Statistic.Daily.PointGained);
    this.WriteD(this.account_0.Statistic.Daily.Playtime);
    this.WriteB(((PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK) this).method_3(this.account_0));
    this.WriteD(0);
    this.WriteC((byte) 0);
    this.WriteD(0);
    this.WriteC((byte) 0);
    this.WriteD(0);
    this.WriteH((short) 0);
    this.WriteC((byte) 0);
    this.WriteB(((PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK) this).method_4(this.account_0));
  }

  private byte[] method_0([In] RoomModel obj0, bool teamEnum_0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      if (teamEnum_0)
      {
        foreach (SlotModel slot in obj0.Slots)
          syncServerPacket.WriteH((ushort) slot.Score);
      }
      else if (obj0.ThisModeHaveRounds() || obj0.IsDinoMode(""))
      {
        syncServerPacket.WriteH(obj0.IsDinoMode("DE") ? (ushort) obj0.FRDino : (obj0.IsDinoMode("CC") ? (ushort) obj0.FRKills : (ushort) obj0.FRRounds));
        syncServerPacket.WriteH(obj0.IsDinoMode("DE") ? (ushort) obj0.CTDino : (obj0.IsDinoMode("CC") ? (ushort) obj0.CTKills : (ushort) obj0.CTRounds));
        foreach (SlotModel slot in obj0.Slots)
          syncServerPacket.WriteC((byte) slot.Objects);
        syncServerPacket.WriteH((short) 0);
        syncServerPacket.WriteH((short) 0);
      }
      return syncServerPacket.ToArray();
    }
  }
}
