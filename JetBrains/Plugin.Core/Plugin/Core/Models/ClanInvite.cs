// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.ClanInvite
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Utility;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class ClanInvite
{
  public void SetBestHeadshot([In] SlotModel obj0)
  {
    if (obj0.AllHeadshots <= ((ClanBestPlayers) this).Headshots.RecordValue)
      return;
    ((ClanBestPlayers) this).Headshots.PlayerId = obj0.PlayerId;
    ((RHistoryModel) ((ClanBestPlayers) this).Headshots).set_RecordValue(obj0.AllHeadshots);
  }

  public void SetBestKills([In] SlotModel obj0)
  {
    if (obj0.AllKills <= ((ClanBestPlayers) this).Kills.RecordValue)
      return;
    ((ClanBestPlayers) this).Kills.PlayerId = obj0.PlayerId;
    ((RHistoryModel) ((ClanBestPlayers) this).Kills).set_RecordValue(obj0.AllKills);
  }

  public void SetBestWins(PlayerStatistic Split, [In] SlotModel obj1, [In] bool obj2)
  {
    if (!obj2)
      return;
    StatisticClan clan = Split.Clan;
    int ValueReq1 = ((StatisticDaily) clan).get_MatchWins() + 1;
    ((StatisticDaily) clan).set_MatchWins(ValueReq1);
    ComDiv.UpdateDB("player_stat_clans", "clan_match_wins", (object) ValueReq1, "owner_id", (object) obj1.PlayerId);
    if (((StatisticDaily) Split.Clan).get_MatchWins() <= ((ClanBestPlayers) this).Wins.RecordValue)
      return;
    ((ClanBestPlayers) this).Wins.PlayerId = obj1.PlayerId;
    ((RHistoryModel) ((ClanBestPlayers) this).Wins).set_RecordValue(((StatisticDaily) Split.Clan).get_MatchWins());
  }

  public void SetBestParticipation(PlayerStatistic Slot, [In] SlotModel obj1)
  {
    ComDiv.UpdateDB("player_stat_clans", "clan_matches", (object) ++Slot.Clan.Matches, "owner_id", (object) obj1.PlayerId);
    if (Slot.Clan.Matches <= ((ClanBestPlayers) this).Participation.RecordValue)
      return;
    ((ClanBestPlayers) this).Participation.PlayerId = obj1.PlayerId;
    ((RHistoryModel) ((ClanBestPlayers) this).Participation).set_RecordValue(Slot.Clan.Matches);
  }

  public int Id { get; set; }

  public uint InviteDate { get; [param: In] set; }

  public long PlayerId
  {
    [CompilerGenerated, SpecialName] get => ((ClanInvite) this).long_0;
    [CompilerGenerated, SpecialName] [param: In] set => ((ClanInvite) this).long_0 = value;
  }

  public string Text
  {
    [CompilerGenerated, SpecialName] get => ((ClanInvite) this).string_0;
    [CompilerGenerated, SpecialName] set => ((ClanInvite) this).string_0 = value;
  }
}
