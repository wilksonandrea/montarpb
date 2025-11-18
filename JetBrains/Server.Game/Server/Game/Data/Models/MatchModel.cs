// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Models.MatchModel
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Sync.Client;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Models;

public class MatchModel
{
  public MatchModel GetMatch(int pS, [In] int obj1)
  {
    lock (((ChannelModel) this).Matches)
    {
      foreach (MatchModel match in ((ChannelModel) this).Matches)
      {
        if (match.FriendId == pS && match.Clan.Id == obj1)
          return match;
      }
      return (MatchModel) null;
    }
  }

  public RoomModel GetRoom(int match)
  {
    lock (((ChannelModel) this).Rooms)
    {
      foreach (RoomModel room in ((ChannelModel) this).Rooms)
      {
        if (room.RoomId == match)
          return room;
      }
      return (RoomModel) null;
    }
  }

  public List<Account> GetWaitPlayers()
  {
    List<Account> waitPlayers = new List<Account>();
    lock (((ChannelModel) this).Players)
    {
      foreach (PlayerSession player in ((ChannelModel) this).Players)
      {
        Account account = ClanManager.GetAccount(player.PlayerId, true);
        if (account != null && account.Room == null && !string.IsNullOrEmpty(account.Nickname))
          waitPlayers.Add(account);
      }
    }
    return waitPlayers;
  }

  public void SendPacketToWaitPlayers(GameServerPacket room)
  {
    List<Account> waitPlayers = this.GetWaitPlayers();
    if (waitPlayers.Count == 0)
      return;
    byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) room).GetCompleteBytes("Channel.SendPacketToWaitPlayers");
    foreach (Account account in waitPlayers)
      account.SendCompletePacket(completeBytes, room.GetType().Name);
  }

  public bool RemovePlayer(Account id)
  {
    bool flag = false;
    id.ChannelId = -1;
    id.ServerId = -1;
    if (id.Session != null)
    {
      lock (((ChannelModel) this).Players)
        flag = ((ChannelModel) this).Players.Remove(id.Session);
      AccountInfo.RefreshChannel(((ChannelModel) this).ServerId, ((ChannelModel) this).Id, ((ChannelModel) this).Players.Count);
      if (flag)
        AuthLogin.RefreshSChannel(((ChannelModel) this).ServerId);
    }
    return flag;
  }

  public ClanModel Clan { get; set; }

  public int Training { get; [param: In] set; }

  public int ServerId { get; set; }

  public int ChannelId { get; set; }

  public int MatchId { get; set; }

  public int Leader { get; set; }

  public int FriendId { get; set; }

  public SlotMatch[] Slots { get; set; }

  public MatchState State { get; set; }

  public MatchModel(ClanModel value)
  {
    this.Clan = value;
    this.MatchId = -1;
    this.Slots = new SlotMatch[9];
    this.State = MatchState.Ready;
    for (int index = 0; index < 9; ++index)
      this.Slots[index] = new SlotMatch(index);
  }

  public bool GetSlot(int value, [In] ref SlotMatch obj1)
  {
    lock (this.Slots)
    {
      obj1 = (SlotMatch) null;
      if (value >= 0 && value <= 17)
        obj1 = this.Slots[value];
      return obj1 != null;
    }
  }

  public SlotMatch GetSlot(int value)
  {
    lock (this.Slots)
      return value >= 0 && value <= 17 ? this.Slots[value] : (SlotMatch) null;
  }

  public void SetNewLeader(int value, [In] int obj1)
  {
    lock (this.Slots)
    {
      if (value == -1)
      {
        for (int index = 0; index < this.Training; ++index)
        {
          if (index != obj1 && this.Slots[index].PlayerId > 0L)
          {
            this.Leader = index;
            break;
          }
        }
      }
      else
        this.Leader = value;
    }
  }

  public bool AddPlayer(Account SlotId)
  {
    lock (this.Slots)
    {
      for (int index = 0; index < this.Training; ++index)
      {
        SlotMatch slot = this.Slots[index];
        if (slot.PlayerId == 0L && slot.State == SlotMatchState.Empty)
        {
          slot.PlayerId = SlotId.PlayerId;
          slot.State = SlotMatchState.Normal;
          SlotId.Match = this;
          SlotId.MatchSlot = index;
          SlotId.Status.UpdateClanMatch((byte) this.FriendId);
          AllUtils.SyncPlayerToClanMembers(SlotId);
          return true;
        }
      }
    }
    return false;
  }

  public Account GetPlayerBySlot([In] SlotMatch obj0)
  {
    try
    {
      long playerId = obj0.PlayerId;
      return playerId > 0L ? ClanManager.GetAccount(playerId, true) : (Account) null;
    }
    catch
    {
      return (Account) null;
    }
  }

  public Account GetPlayerBySlot(int SlotId)
  {
    try
    {
      long playerId = this.Slots[SlotId].PlayerId;
      return playerId > 0L ? ClanManager.GetAccount(playerId, true) : (Account) null;
    }
    catch
    {
      return (Account) null;
    }
  }

  public List<Account> GetAllPlayers(int Leader)
  {
    List<Account> allPlayers = new List<Account>();
    lock (this.Slots)
    {
      for (int index = 0; index < 9; ++index)
      {
        long playerId = this.Slots[index].PlayerId;
        if (playerId > 0L && index != Leader)
        {
          Account account = ClanManager.GetAccount(playerId, true);
          if (account != null)
            allPlayers.Add(account);
        }
      }
    }
    return allPlayers;
  }

  public List<Account> GetAllPlayers()
  {
    List<Account> allPlayers = new List<Account>();
    lock (this.Slots)
    {
      for (int index = 0; index < 9; ++index)
      {
        long playerId = this.Slots[index].PlayerId;
        if (playerId > 0L)
        {
          Account account = ClanManager.GetAccount(playerId, true);
          if (account != null)
            allPlayers.Add(account);
        }
      }
    }
    return allPlayers;
  }

  public void SendPacketToPlayers([In] GameServerPacket obj0)
  {
    List<Account> allPlayers = this.GetAllPlayers();
    if (allPlayers.Count == 0)
      return;
    byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) obj0).GetCompleteBytes("Match.SendPacketToPlayers(SendPacket)");
    foreach (Account account in allPlayers)
      account.SendCompletePacket(completeBytes, obj0.GetType().Name);
  }

  public void SendPacketToPlayers(GameServerPacket Player, [In] int obj1)
  {
    List<Account> allPlayers = this.GetAllPlayers(obj1);
    if (allPlayers.Count == 0)
      return;
    byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) Player).GetCompleteBytes("Match.SendPacketToPlayers(SendPacket,int)");
    foreach (Account account in allPlayers)
      account.SendCompletePacket(completeBytes, Player.GetType().Name);
  }
}
