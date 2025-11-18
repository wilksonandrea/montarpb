// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.ClanModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class ClanModel
{
  public int Id;
  public int Matches;
  public int MatchWins;
  public int MatchLoses;
  public int TotalKills;
  public int TotalHeadshots;
  public int TotalDeaths;
  public int TotalAssists;
  public int TotalEscapes;
  public int Authority;
  public int RankLimit;
  public int MinAgeLimit;
  public int MaxAgeLimit;
  public int Exp;
  public int Rank;
  public int NameColor;
  public int MaxPlayers;
  public int Effect;
  public string Name;
  public string Info;
  public string News;
  public long OwnerId;
  public uint Logo;
  public uint CreationDate;
  public float Points;
  public JoinClanType JoinType;
  public ClanBestPlayers BestPlayers;

  public void UpdateChannel(byte PlayerId)
  {
    ((AccountStatus) this).ChannelId = PlayerId;
    ((AccountStatus) this).Buffer[0] = PlayerId;
    ((ClanBestPlayers) this).method_0();
  }

  public void UpdateRoom(byte Data)
  {
    ((AccountStatus) this).RoomId = Data;
    ((AccountStatus) this).Buffer[1] = Data;
    ((ClanBestPlayers) this).method_0();
  }

  public void UpdateServer([In] byte obj0)
  {
    ((AccountStatus) this).ServerId = obj0;
    ((AccountStatus) this).Buffer[2] = obj0;
    ((ClanBestPlayers) this).method_0();
  }
}
