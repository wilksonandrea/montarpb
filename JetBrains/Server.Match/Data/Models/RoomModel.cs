// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.RoomModel
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SharpDX;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Match.Data.Utils;
using Server.Match.Data.XML;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Data.Models;

public class RoomModel
{
  public PlayerModel[] Players;
  public ObjectInfo[] Objects;
  public long LastStartTick;
  public uint UniqueRoomId;
  public uint RoomSeed;
  public int ObjsSyncRound;
  public int ServerRound;
  public int SourceToMap;
  public int ServerId;
  public int RoomId;
  public int ChannelId;
  public int LastRound;
  public int Bar1;
  public int Bar2;
  public int Default1;
  public int Default2;
  public byte DropCounter;
  public bool BotMode;
  public bool HasC4;
  public bool IsTeamSwap;
  public MapIdEnum MapId;
  public MapRules Rule;
  public RoomCondition RoomType;
  public SChannelModel Server;
  public MapModel Map;
  public Half3 BombPosition;
  public DateTime StartTime;
  public DateTime LastObjsSync;
  public DateTime LastPlayersSync;
  private readonly object object_0;
  private readonly object object_1;

  [CompilerGenerated]
  [SpecialName]
  public int get_WpnSpecial() => ((Equipment) this).int_4;

  [CompilerGenerated]
  [SpecialName]
  public void set_WpnSpecial(int EndBullet) => ((Equipment) this).int_4 = EndBullet;

  [CompilerGenerated]
  [SpecialName]
  public int get_Accessory() => ((Equipment) this).int_5;

  [CompilerGenerated]
  [SpecialName]
  public void set_Accessory(int value) => ((Equipment) this).int_5 = value;

  public RoomModel()
  {
  }

  public RoomModel(int value)
  {
    this.Server = SChannelXML.GetServer(value);
    if (this.Server == null)
      return;
    this.ServerId = value;
    for (int index = 0; index < 18; ++index)
      this.Players[index] = new PlayerModel(index);
    for (int index = 0; index < 200; ++index)
      this.Objects[index] = (ObjectInfo) new ObjectModel(index);
  }

  public void SyncInfo(List<ObjectHitInfo> value, [In] int obj1)
  {
    lock (this.object_1)
    {
      if (this.BotMode || !this.ObjectsIsValid())
        return;
      double duration1 = ComDiv.GetDuration(this.LastObjsSync);
      double duration2 = ComDiv.GetDuration(this.LastPlayersSync);
      if (duration1 >= 2.5 && (obj1 & 1) == 1)
      {
        this.LastObjsSync = DateTimeUtil.Now();
        foreach (ObjectInfo objectInfo1 in this.Objects)
        {
          ObjectModel objectModel = ((ObjectModel) objectInfo1).get_Model();
          if (objectModel != null && (objectModel.Destroyable && objectInfo1.Life != objectModel.Life || objectModel.NeedSync))
          {
            float duration3 = AllUtils.GetDuration(((ObjectModel) objectInfo1).get_UseDate());
            AnimModel animation = objectInfo1.Animation;
            if (animation != null && (double) ((AssistServerData) animation).get_Duration() > 0.0 && (double) duration3 >= (double) ((AssistServerData) animation).get_Duration())
              ((PacketModel) objectModel).GetAnim(animation.NextAnim, duration3, ((AssistServerData) animation).get_Duration(), objectInfo1);
            ObjectInfo objectInfo2 = new ObjectInfo(objectModel.UpdateId);
            ((ObjectHitInfo) objectInfo2).ObjSyncId = objectModel.NeedSync ? 1 : 0;
            ((ObjectHitInfo) objectInfo2).AnimId1 = objectModel.Animation;
            ((ObjectHitInfo) objectInfo2).AnimId2 = objectInfo1.Animation != null ? objectInfo1.Animation.Id : (int) byte.MaxValue;
            ((ObjectHitInfo) objectInfo2).DestroyState = objectInfo1.DestroyState;
            ((ObjectHitInfo) objectInfo2).ObjId = objectModel.Id;
            ((ObjectHitInfo) objectInfo2).ObjLife = objectInfo1.Life;
            ((ObjectHitInfo) objectInfo2).SpecialUse = duration3;
            ObjectHitInfo objectHitInfo = (ObjectHitInfo) objectInfo2;
            value.Add(objectHitInfo);
          }
        }
      }
      if (duration2 < 1.5 || (obj1 & 2) != 2)
        return;
      this.LastPlayersSync = DateTimeUtil.Now();
      foreach (PlayerModel player in this.Players)
      {
        if (!player.Immortal && (player.MaxLife != player.Life || player.Dead))
        {
          ObjectInfo objectInfo = new ObjectInfo(4);
          ((ObjectHitInfo) objectInfo).ObjId = player.Slot;
          ((ObjectHitInfo) objectInfo).ObjLife = player.Life;
          ObjectHitInfo objectHitInfo = (ObjectHitInfo) objectInfo;
          value.Add(objectHitInfo);
        }
      }
    }
  }

  public bool ObjectsIsValid() => this.ServerRound == this.ObjsSyncRound;

  public void ResyncTick(long value, [In] uint obj1)
  {
    if (value <= this.LastStartTick)
      return;
    this.StartTime = new DateTime(value);
    if (this.LastStartTick > 0L)
      this.ResetRoomInfo(obj1);
    this.LastStartTick = value;
    if (!ConfigLoader.IsTestMode)
      return;
    CLogger.Print($"New tick is defined. [{this.LastStartTick}]", LoggerType.Warning, (Exception) null);
  }

  public void ResetRoomInfo(uint int_0)
  {
    for (int index = 0; index < 200; ++index)
      this.Objects[index] = (ObjectInfo) new ObjectModel(index);
    this.MapId = (MapIdEnum) AllUtils.GetSeedInfo(int_0, 2);
    this.RoomType = (RoomCondition) AllUtils.GetSeedInfo(int_0, 0);
    this.Rule = (MapRules) AllUtils.GetSeedInfo(int_0, 1);
    this.SourceToMap = -1;
    this.Map = (MapModel) null;
    this.LastRound = 0;
    this.DropCounter = (byte) 0;
    this.BotMode = false;
    this.HasC4 = false;
    this.IsTeamSwap = false;
    this.ServerRound = 0;
    this.ObjsSyncRound = 0;
    this.LastObjsSync = new DateTime();
    this.LastPlayersSync = new DateTime();
    this.BombPosition = new Half3();
    if (!ConfigLoader.IsTestMode)
      return;
    CLogger.Print("A room has been reseted by server.", LoggerType.Warning, (Exception) null);
  }

  public bool RoundResetRoomF1(int Objs)
  {
    lock (this.object_0)
    {
      if (this.LastRound != Objs)
      {
        if (ConfigLoader.IsTestMode)
          CLogger.Print($"Reseting room. [Last: {this.LastRound}; New: {Objs}]", LoggerType.Warning, (Exception) null);
        DateTime int_9 = DateTimeUtil.Now();
        this.LastRound = Objs;
        this.HasC4 = false;
        this.BombPosition = new Half3();
        this.DropCounter = (byte) 0;
        this.ObjsSyncRound = 0;
        this.SourceToMap = (int) this.MapId;
        if (!this.BotMode)
        {
          for (int index = 0; index < 18; ++index)
          {
            PlayerModel player = this.Players[index];
            player.Life = player.MaxLife;
          }
          this.LastPlayersSync = int_9;
          this.Map = MapStructureXML.GetMapId((int) this.MapId);
          List<ObjectModel> objects = this.Map?.Objects;
          if (objects != null)
          {
            foreach (ObjectModel CharaId in objects)
            {
              ObjectInfo objectInfo1 = this.Objects[CharaId.Id];
              objectInfo1.Life = CharaId.Life;
              if (!CharaId.NoInstaSync)
              {
                ((PacketModel) CharaId).GetRandomAnimation(this, objectInfo1);
              }
              else
              {
                ObjectInfo objectInfo2 = objectInfo1;
                AssistServerData assistServerData = new AssistServerData();
                ((AnimModel) assistServerData).NextAnim = 1;
                objectInfo2.Animation = (AnimModel) assistServerData;
                ((ObjectModel) objectInfo1).set_UseDate(int_9);
              }
              ((ObjectModel) objectInfo1).set_Model(CharaId);
              objectInfo1.DestroyState = 0;
              MapStructureXML.SetObjectives(CharaId, this);
            }
          }
          this.LastObjsSync = int_9;
          this.ObjsSyncRound = Objs;
        }
        return true;
      }
    }
    return false;
  }

  public bool RoundResetRoomS1([In] int obj0)
  {
    lock (this.object_0)
    {
      if (this.LastRound != obj0)
      {
        if (ConfigLoader.IsTestMode)
          CLogger.Print($"Reseting room. [Last: {this.LastRound}; New: {obj0}]", LoggerType.Warning, (Exception) null);
        this.LastRound = obj0;
        this.HasC4 = false;
        this.DropCounter = (byte) 0;
        this.BombPosition = new Half3();
        if (!this.BotMode)
        {
          for (int index = 0; index < 18; ++index)
          {
            PlayerModel player = this.Players[index];
            player.Life = player.MaxLife;
          }
          DateTime int_9 = DateTimeUtil.Now();
          this.LastPlayersSync = int_9;
          foreach (ObjectInfo objectInfo1 in this.Objects)
          {
            ObjectModel objectModel = ((ObjectModel) objectInfo1).get_Model();
            if (objectModel != null)
            {
              objectInfo1.Life = objectModel.Life;
              if (!objectModel.NoInstaSync)
              {
                ((PacketModel) objectModel).GetRandomAnimation(this, objectInfo1);
              }
              else
              {
                ObjectInfo objectInfo2 = objectInfo1;
                AssistServerData assistServerData = new AssistServerData();
                ((AnimModel) assistServerData).NextAnim = 1;
                objectInfo2.Animation = (AnimModel) assistServerData;
                ((ObjectModel) objectInfo1).set_UseDate(int_9);
              }
              objectInfo1.DestroyState = 0;
            }
          }
          this.LastObjsSync = int_9;
          this.ObjsSyncRound = obj0;
          if (this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Defense)
          {
            this.Bar1 = this.Default1;
            this.Bar2 = this.Default2;
          }
        }
        return true;
      }
    }
    return false;
  }

  public PlayerModel AddPlayer(IPEndPoint StartTick, PacketModel Seed, [In] string obj2)
  {
    if (ConfigLoader.UdpVersion != obj2)
    {
      if (ConfigLoader.IsTestMode)
        CLogger.Print($"Wrong UDP Version ({obj2}); Player can't be connected into it!", LoggerType.Warning, (Exception) null);
      return (PlayerModel) null;
    }
    try
    {
      PlayerModel player = this.Players[Seed.Slot];
      if (!player.CompareIp(StartTick))
      {
        player.Client = StartTick;
        player.StartTime = ((PlayerModel) Seed).get_ReceiveDate();
        player.PlayerIdByUser = Seed.AccountId;
        return player;
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
    return (PlayerModel) null;
  }

  public bool GetPlayer(int Round, [In] ref PlayerModel obj1)
  {
    try
    {
      obj1 = this.Players[Round];
    }
    catch
    {
      obj1 = (PlayerModel) null;
    }
    return obj1 != null;
  }
}
