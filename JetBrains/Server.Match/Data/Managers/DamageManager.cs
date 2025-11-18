// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Managers.DamageManager
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Sync.Client;
using Server.Match.Data.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Data.Managers;

public static class DamageManager
{
  static DamageManager() => AssistManager.Assists = new List<AssistServerData>();

  public static RoomModel CreateOrGetRoom([In] uint obj0, [In] uint obj1)
  {
    lock (RoomsManager.list_0)
    {
      foreach (RoomModel orGetRoom in RoomsManager.list_0)
      {
        if ((int) orGetRoom.UniqueRoomId == (int) obj0 && (int) orGetRoom.RoomSeed == (int) obj1)
          return orGetRoom;
      }
      int roomInfo1 = AllUtils.GetRoomInfo(obj0, 2);
      int roomInfo2 = AllUtils.GetRoomInfo(obj0, 1);
      int roomInfo3 = AllUtils.GetRoomInfo(obj0, 0);
      RoomModel orGetRoom1 = new RoomModel(roomInfo1)
      {
        UniqueRoomId = obj0,
        RoomSeed = obj1,
        RoomId = roomInfo3,
        ChannelId = roomInfo2,
        MapId = (MapIdEnum) AllUtils.GetSeedInfo(obj1, 2),
        RoomType = (RoomCondition) AllUtils.GetSeedInfo(obj1, 0),
        Rule = (MapRules) AllUtils.GetSeedInfo(obj1, 1)
      };
      RoomsManager.list_0.Add(orGetRoom1);
      return orGetRoom1;
    }
  }

  public static RoomModel GetRoom(uint Assist, [In] uint obj1)
  {
    lock (RoomsManager.list_0)
    {
      foreach (RoomModel room in RoomsManager.list_0)
      {
        if (room != null && (int) room.UniqueRoomId == (int) Assist && (int) room.RoomSeed == (int) obj1)
          return room;
      }
      return (RoomModel) null;
    }
  }

  public static void RemoveRoom([In] uint obj0, uint RoomId)
  {
    lock (RoomsManager.list_0)
    {
      for (int index = 0; index < RoomsManager.list_0.Count; ++index)
      {
        RoomModel roomModel = RoomsManager.list_0[index];
        if ((int) roomModel.UniqueRoomId == (int) obj0 && (int) roomModel.RoomSeed == (int) RoomId)
        {
          RoomsManager.list_0.RemoveAt(index);
          break;
        }
      }
    }
  }

  static DamageManager() => RoomsManager.list_0 = new List<RoomModel>();

  public static void SabotageDestroy(
    [In] RoomModel obj0,
    PlayerModel Seed,
    [In] ObjectModel obj2,
    [In] ObjectInfo obj3,
    [In] int obj4)
  {
    if (obj2.UltraSync <= 0 || obj0.RoomType != RoomCondition.Destroy && obj0.RoomType != RoomCondition.Defense)
      return;
    if (obj2.UltraSync != 1 && obj2.UltraSync != 3)
    {
      if (obj2.UltraSync == 2 || obj2.UltraSync == 4)
        obj0.Bar2 = obj3.Life;
    }
    else
      obj0.Bar1 = obj3.Life;
    RemovePlayerSync.SendSabotageSync(obj0, Seed, obj4, obj2.UltraSync == 4 ? 2 : 1);
  }
}
