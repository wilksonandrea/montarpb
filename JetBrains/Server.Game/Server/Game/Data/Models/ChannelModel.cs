// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Models.ChannelModel
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Sync.Client;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Models;

public class ChannelModel
{
  public bool HavePermission([In] string obj0)
  {
    return PermissionXML.HavePermission(obj0, ((Account) this).Access);
  }

  public float PointUp()
  {
    PCCafeModel pcCafe = TemplatePackXML.GetPCCafe(((Account) this).CafePC);
    return pcCafe != null ? (float) (pcCafe.PointUp / 100) : 0.0f;
  }

  public float ExpUp()
  {
    PCCafeModel pcCafe = TemplatePackXML.GetPCCafe(((Account) this).CafePC);
    return pcCafe != null ? (float) (pcCafe.ExpUp / 100) : 0.0f;
  }

  public int TourneyLevel()
  {
    CompetitiveRank rank = CompetitiveXML.GetRank(((Account) this).Competitive.Level);
    return rank != null ? rank.TourneyLevel : 0;
  }

  public void SetCountry([In] string obj0)
  {
    if (string.IsNullOrEmpty(obj0))
      return;
    CountryFlags countryByIp = CountryDetector.GetCountryByIp(obj0);
    ((Account) this).Country = countryByIp;
    ComDiv.UpdateDB("accounts", "country_flag", (object) (int) countryByIp, "player_id", (object) ((Account) this).PlayerId);
    if (countryByIp == CountryFlags.None)
      return;
    CLogger.Print($"Connect {((Account) this).Username} from: {countryByIp}", LoggerType.Info, (Exception) null);
  }

  public int Id { get; set; }

  public ChannelType Type { get; [param: In] set; }

  public int ServerId { get; set; }

  public int MaxRooms { get; set; }

  public int ExpBonus { get; set; }

  public int GoldBonus { get; set; }

  public int CashBonus { get; set; }

  public string Password { get; set; }

  public List<PlayerSession> Players { get; set; }

  public List<RoomModel> Rooms { get; set; }

  public List<MatchModel> Matches { get; set; }

  private DateTime DateTime_0 { get; set; }

  public ChannelModel(int value)
  {
    this.ServerId = value;
    this.Players = new List<PlayerSession>();
    this.Rooms = new List<RoomModel>();
    this.Matches = new List<MatchModel>();
    this.DateTime_0 = DateTimeUtil.Now();
  }

  public PlayerSession GetPlayer(int value)
  {
    lock (this.Players)
    {
      foreach (PlayerSession player in this.Players)
      {
        if (player.SessionId == value)
          return player;
      }
      return (PlayerSession) null;
    }
  }

  public PlayerSession GetPlayer(int value, [In] ref int obj1)
  {
    obj1 = -1;
    lock (this.Players)
    {
      for (int index = 0; index < this.Players.Count; ++index)
      {
        PlayerSession player = this.Players[index];
        if (player.SessionId == value)
        {
          obj1 = index;
          return player;
        }
      }
      return (PlayerSession) null;
    }
  }

  public bool AddPlayer(PlayerSession value)
  {
    lock (this.Players)
    {
      if (this.Players.Contains(value))
        return false;
      this.Players.Add(value);
      AuthLogin.RefreshSChannel(this.ServerId);
      AccountInfo.RefreshChannel(this.ServerId, this.Id, this.Players.Count);
      return true;
    }
  }

  public void RemoveMatch(int int_6)
  {
    lock (this.Matches)
    {
      for (int index = 0; index < this.Matches.Count; ++index)
      {
        if (int_6 == this.Matches[index].MatchId)
        {
          this.Matches.RemoveAt(index);
          break;
        }
      }
    }
  }

  public void AddMatch(MatchModel session)
  {
    lock (this.Matches)
    {
      if (this.Matches.Contains(session))
        return;
      this.Matches.Add(session);
    }
  }

  public void AddRoom(RoomModel SessionId)
  {
    lock (this.Rooms)
      this.Rooms.Add(SessionId);
  }

  public void RemoveEmptyRooms()
  {
    lock (this.Rooms)
    {
      if (ComDiv.GetDuration(this.DateTime_0) < (double) ConfigLoader.EmptyRoomRemovalInterval)
        return;
      this.DateTime_0 = DateTimeUtil.Now();
      for (int index = 0; index < this.Rooms.Count; ++index)
      {
        if (this.Rooms[index].GetCountPlayers() < 1)
          this.Rooms.RemoveAt(index--);
      }
    }
  }

  public MatchModel GetMatch([In] int obj0)
  {
    lock (this.Matches)
    {
      foreach (MatchModel match in this.Matches)
      {
        if (match.MatchId == obj0)
          return match;
      }
      return (MatchModel) null;
    }
  }
}
