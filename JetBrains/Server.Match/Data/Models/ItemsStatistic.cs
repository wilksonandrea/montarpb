// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.ItemsStatistic
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Managers;
using Server.Match.Data.Models.SubHead;
using Server.Match.Data.Sync.Client;
using Server.Match.Data.Utils;
using Server.Match.Data.XML;
using Server.Match.Network.Actions.SubHead;
using Server.Match.Network.Packets;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Data.Models;

public class ItemsStatistic
{
  public ItemsStatistic()
  {
  }

  public static void Load([In] SyncClientPacket obj0)
  {
    // ISSUE: variable of a compiler-generated type
    RespawnSync.Class1 class1 = (RespawnSync.Class1) new ItemsStatistic();
    uint Assist = obj0.ReadUD();
    uint num1 = obj0.ReadUD();
    long num2 = obj0.ReadQ();
    int num3 = (int) obj0.ReadC();
    int Objs = (int) obj0.ReadC();
    int Client = (int) obj0.ReadC();
    int num4 = (int) obj0.ReadC();
    int num5 = (int) obj0.ReadC();
    int num6 = 0;
    int num7 = 0;
    int num8 = 0;
    int num9 = 0;
    int num10 = 0;
    int num11 = 0;
    int EndBullet = 0;
    int num12 = 0;
    bool flag = false;
    if (num3 != 0 && num3 != 2)
    {
      if (obj0.ToArray().Length > 23)
        CLogger.Print($"RespawnSync (Length > 23): {obj0.ToArray().Length}", LoggerType.Warning, (Exception) null);
    }
    else
    {
      num6 = obj0.ReadD();
      num7 = (int) obj0.ReadC();
      flag = obj0.ReadC() == (byte) 1;
      num8 = obj0.ReadD();
      num9 = obj0.ReadD();
      num10 = obj0.ReadD();
      num11 = obj0.ReadD();
      EndBullet = obj0.ReadD();
      num12 = obj0.ReadD();
      if (obj0.ToArray().Length > 53)
        CLogger.Print($"RespawnSync (Length > 53): {obj0.ToArray().Length}", LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated field
    class1.roomModel_0 = DamageManager.GetRoom(Assist, num1);
    // ISSUE: reference to a compiler-generated field
    if (class1.roomModel_0 == null)
      return;
    // ISSUE: reference to a compiler-generated field
    class1.roomModel_0.ResyncTick(num2, num1);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    class1.playerModel_0 = ((ObjectMoveInfo) class1.roomModel_0).GetPlayer(Client, true);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (class1.playerModel_0 != null && class1.playerModel_0.PlayerIdByUser != num5)
    {
      // ISSUE: reference to a compiler-generated field
      CLogger.Print($"Invalid User Ids: [By User: {class1.playerModel_0.PlayerIdByUser} / Server: {num5}]", LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (class1.playerModel_0 == null || class1.playerModel_0.PlayerIdByUser != num5)
      return;
    // ISSUE: reference to a compiler-generated field
    class1.playerModel_0.PlayerIdByServer = num5;
    // ISSUE: reference to a compiler-generated field
    class1.playerModel_0.RespawnByServer = num4;
    // ISSUE: reference to a compiler-generated field
    class1.playerModel_0.Integrity = false;
    // ISSUE: reference to a compiler-generated field
    if (Objs > class1.roomModel_0.ServerRound)
    {
      // ISSUE: reference to a compiler-generated field
      class1.roomModel_0.ServerRound = Objs;
    }
    if (num3 == 0 || num3 == 2)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      AssistServerData assist = RoomsManager.GetAssist(class1.playerModel_0.Slot, class1.roomModel_0.RoomId);
      if (assist != null && RoomsManager.RemoveAssist(assist))
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        foreach (AssistServerData Id in AssistManager.Assists.FindAll(class1.predicate_0 ?? (class1.predicate_0 = new Predicate<AssistServerData>(((ItemsStatistic) class1).method_0))))
          RoomsManager.RemoveAssist(Id);
      }
      RoomModel roomModel = new RoomModel();
      ((Equipment) roomModel).WpnPrimary = num8;
      ((Equipment) roomModel).WpnSecondary = num9;
      ((Equipment) roomModel).WpnMelee = num10;
      ((Equipment) roomModel).WpnExplosive = num11;
      roomModel.set_WpnSpecial(EndBullet);
      roomModel.set_Accessory(num12);
      Equipment equipment = (Equipment) roomModel;
      // ISSUE: reference to a compiler-generated field
      class1.playerModel_0.Dead = false;
      // ISSUE: reference to a compiler-generated field
      class1.playerModel_0.PlantDuration = ConfigLoader.PlantDuration;
      // ISSUE: reference to a compiler-generated field
      class1.playerModel_0.DefuseDuration = ConfigLoader.DefuseDuration;
      // ISSUE: reference to a compiler-generated field
      class1.playerModel_0.Equip = equipment;
      if (flag)
      {
        // ISSUE: reference to a compiler-generated field
        class1.playerModel_0.PlantDuration -= ComDiv.Percentage(ConfigLoader.PlantDuration, 50);
        // ISSUE: reference to a compiler-generated field
        class1.playerModel_0.DefuseDuration -= ComDiv.Percentage(ConfigLoader.DefuseDuration, 25);
      }
      // ISSUE: reference to a compiler-generated field
      if (!class1.roomModel_0.BotMode)
      {
        // ISSUE: reference to a compiler-generated field
        if (class1.roomModel_0.SourceToMap == -1)
        {
          // ISSUE: reference to a compiler-generated field
          class1.roomModel_0.RoundResetRoomF1(Objs);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          class1.roomModel_0.RoundResetRoomS1(Objs);
        }
      }
      if (num6 == -1)
      {
        // ISSUE: reference to a compiler-generated field
        class1.playerModel_0.Immortal = true;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        class1.playerModel_0.Immortal = false;
        int charaHp = ItemStatisticXML.GetCharaHP(num6);
        int num13 = charaHp + ComDiv.Percentage(charaHp, num7);
        // ISSUE: reference to a compiler-generated field
        class1.playerModel_0.MaxLife = num13;
        // ISSUE: reference to a compiler-generated field
        ((Equipment) class1.playerModel_0).ResetLife();
      }
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (class1.roomModel_0.BotMode || num3 == 2 || !class1.roomModel_0.ObjectsIsValid())
      return;
    List<ObjectHitInfo> Data = new List<ObjectHitInfo>();
    // ISSUE: reference to a compiler-generated field
    foreach (ObjectInfo objectInfo1 in class1.roomModel_0.Objects)
    {
      ObjectModel objectModel = ((ObjectModel) objectInfo1).get_Model();
      if (objectModel != null && (num3 != 2 && objectModel.Destroyable && objectInfo1.Life != objectModel.Life || objectModel.NeedSync))
      {
        ObjectInfo objectInfo2 = new ObjectInfo(3);
        ((ObjectHitInfo) objectInfo2).ObjSyncId = objectModel.NeedSync ? 1 : 0;
        ((ObjectHitInfo) objectInfo2).AnimId1 = objectModel.Animation;
        ((ObjectHitInfo) objectInfo2).AnimId2 = objectInfo1.Animation != null ? objectInfo1.Animation.Id : (int) byte.MaxValue;
        ((ObjectHitInfo) objectInfo2).DestroyState = objectInfo1.DestroyState;
        ((ObjectHitInfo) objectInfo2).ObjId = objectModel.Id;
        ((ObjectHitInfo) objectInfo2).ObjLife = objectInfo1.Life;
        ((ObjectHitInfo) objectInfo2).SpecialUse = AllUtils.GetDuration(((ObjectModel) objectInfo1).get_UseDate());
        ObjectHitInfo objectHitInfo = (ObjectHitInfo) objectInfo2;
        Data.Add(objectHitInfo);
      }
    }
    // ISSUE: reference to a compiler-generated field
    foreach (PlayerModel player in class1.roomModel_0.Players)
    {
      if (player.Slot != Client && player.AccountIdIsValid() && !player.Immortal && player.StartTime != new DateTime() && (player.MaxLife != player.Life || player.Dead))
      {
        ObjectInfo objectInfo = new ObjectInfo(4);
        ((ObjectHitInfo) objectInfo).ObjId = player.Slot;
        ((ObjectHitInfo) objectInfo).ObjLife = player.Life;
        ObjectHitInfo objectHitInfo = (ObjectHitInfo) objectInfo;
        Data.Add(objectHitInfo);
      }
    }
    if (Data.Count > 0)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ((PROTOCOL_CONNECT) MatchXender.Client).SendPacket(AllUtils.BaseWriteCode(4, StageInfoObjControl.GET_CODE(Data), (int) byte.MaxValue, AllUtils.GetDuration(class1.roomModel_0.StartTime), Objs, num4, 0, num5), class1.playerModel_0.Client);
    }
    Data.Clear();
  }

  public ItemsStatistic()
  {
  }

  public ItemsStatistic()
  {
  }

  internal bool method_0(AssistServerData Room)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return Room.Victim == ((RespawnSync.Class1) this).playerModel_0.Slot && Room.RoomId == ((RespawnSync.Class1) this).roomModel_0.RoomId;
  }

  public int Id { get; [param: In] set; }

  public string Name { get; set; }

  public int Damage { get; set; }

  public int HelmetPenetrate { get; set; }

  public int BulletLoaded { get; set; }

  public int BulletTotal { get; set; }

  public float FireDelay
  {
    [CompilerGenerated, SpecialName] get => ((ItemsStatistic) this).float_0;
    [CompilerGenerated, SpecialName] set => ((ItemsStatistic) this).float_0 = value;
  }

  public float Range
  {
    [CompilerGenerated, SpecialName] get => ((ItemsStatistic) this).float_1;
    [CompilerGenerated, SpecialName] set => ((ItemsStatistic) this).float_1 = value;
  }
}
