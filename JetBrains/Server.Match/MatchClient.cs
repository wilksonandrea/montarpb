// Decompiled with JetBrains decompiler
// Type: Server.Match.MatchClient
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using dummy_ptr;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.SharpDX;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using Server.Match.Data.Models.Event.Event;
using Server.Match.Data.Models.SubHead;
using Server.Match.Data.Sync;
using Server.Match.Data.Sync.Client;
using Server.Match.Data.Sync.Server;
using Server.Match.Data.Utils;
using Server.Match.Data.XML;
using Server.Match.Network.Actions.Event;
using Server.Match.Network.Actions.SubHead;
using Server.Match.Network.Packets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match;

public class MatchClient
{
  private readonly Socket socket_0;
  private readonly IPEndPoint ipendPoint_0;

  public abstract void m000001();

  public abstract void m000002();

  public abstract void m000003();

  public abstract void m000004();

  public abstract void m000005();

  public MatchClient([In] Socket obj0, [In] IPEndPoint obj1)
  {
    this.socket_0 = obj0;
    this.ipendPoint_0 = obj1;
  }

  public void BeginReceive([In] byte[] obj0, [In] DateTime obj1)
  {
    PlayerModel playerModel = new PlayerModel();
    ((PacketModel) playerModel).Data = obj0;
    playerModel.set_ReceiveDate(obj1);
    PacketModel packetModel = (PacketModel) playerModel;
    SyncClientPacket syncClientPacket = new SyncClientPacket(packetModel.Data);
    packetModel.Opcode = (int) syncClientPacket.ReadC();
    packetModel.Slot = (int) syncClientPacket.ReadC();
    packetModel.Time = syncClientPacket.ReadT();
    packetModel.Round = (int) syncClientPacket.ReadC();
    packetModel.Length = (int) syncClientPacket.ReadUH();
    packetModel.Respawn = (int) syncClientPacket.ReadC();
    packetModel.RoundNumber = (int) syncClientPacket.ReadC();
    packetModel.AccountId = (int) syncClientPacket.ReadC();
    packetModel.Unk1 = (int) syncClientPacket.ReadC();
    packetModel.Unk2 = syncClientPacket.ReadD();
    if (packetModel.Length > packetModel.Data.Length)
    {
      CLogger.Print($"Packet with invalid size canceled. [ Length: {packetModel.Length} DataLength: {packetModel.Data.Length} ]", LoggerType.Warning, (Exception) null);
    }
    else
    {
      AllUtils.GetDecryptedData(packetModel);
      if (ConfigLoader.IsTestMode && packetModel.Unk1 > 0)
      {
        CLogger.Print(Bitwise.ToHexData($"[N] Test Mode, Packet Unk: {packetModel.Unk1}", packetModel.Data), LoggerType.Opcode, (Exception) null);
        CLogger.Print(Bitwise.ToHexData($"[D] Test Mode, Packet Unk: {packetModel.Unk1}", ((PlayerModel) packetModel).get_WithoutEndData()), LoggerType.Opcode, (Exception) null);
      }
      if (ConfigLoader.EnableLog && packetModel.Opcode != 131 && packetModel.Opcode != 132 && packetModel.Opcode != 3)
      {
        int opcode = packetModel.Opcode;
      }
      this.ReadPacket(packetModel);
    }
  }

  public void ReadPacket([In] PacketModel obj0)
  {
    byte[] withEndData = obj0.WithEndData;
    byte[] numArray = ((PlayerModel) obj0).get_WithoutEndData();
    SyncClientPacket syncClientPacket = new SyncClientPacket(withEndData);
    int length = numArray.Length;
    try
    {
      int num1 = 0;
      switch (obj0.Opcode)
      {
        case 3:
          syncClientPacket.Advance(length);
          int Assist1 = (int) syncClientPacket.ReadUD();
          int num2 = (int) syncClientPacket.ReadC();
          int num3 = (int) syncClientPacket.ReadUD();
          RoomModel room1 = DamageManager.GetRoom((uint) Assist1, (uint) num3);
          if (room1 == null)
            break;
          PlayerModel player1 = ((StageControlInfo) room1).GetPlayer(obj0.Slot, this.ipendPoint_0);
          if (player1 == null || player1.PlayerIdByServer != obj0.AccountId)
            break;
          player1.RespawnByUser = obj0.Respawn;
          if (room1.StartTime == new DateTime())
            break;
          byte[] actionModel_0_1 = AllUtils.BaseWriteCode(4, room1.BotMode ? ((MatchXender) this).WriteBotActionData(numArray, room1) : ((MatchXender) this).WritePlayerActionData(numArray, room1, AllUtils.GetDuration(player1.StartTime), obj0), room1.BotMode ? obj0.Slot : (int) byte.MaxValue, AllUtils.GetDuration(room1.StartTime), obj0.Round, obj0.Respawn, obj0.RoundNumber, obj0.AccountId);
          bool flag1 = !room1.BotMode && num2 != (int) byte.MaxValue;
          foreach (PlayerModel player2 in room1.Players)
          {
            bool flag2 = player2.Slot != obj0.Slot;
            if (player2.Client != null && player1.AccountIdIsValid() && ((num2 == (int) byte.MaxValue & flag2 ? 1 : (room1.BotMode & flag2 ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
              ((MatchXender) this).SendPacket(actionModel_0_1, player2.Client);
          }
          break;
        case 4:
          syncClientPacket.Advance(length);
          int Assist2 = (int) syncClientPacket.ReadUD();
          int num4 = (int) syncClientPacket.ReadC();
          int num5 = (int) syncClientPacket.ReadUD();
          RoomModel room2 = DamageManager.GetRoom((uint) Assist2, (uint) num5);
          if (room2 == null)
            break;
          PlayerModel player3 = ((StageControlInfo) room2).GetPlayer(obj0.Slot, this.ipendPoint_0);
          if (player3 == null || player3.PlayerIdByServer != obj0.AccountId)
            break;
          player3.RespawnByUser = obj0.Respawn;
          room2.BotMode = true;
          if (room2.StartTime == new DateTime())
            break;
          byte[] actionModel_0_2 = AllUtils.BaseWriteCode(4, ((MatchXender) this).WriteBotActionData(numArray, room2), obj0.Slot, AllUtils.GetDuration(player3.StartTime), obj0.Round, obj0.Respawn, obj0.RoundNumber, obj0.AccountId);
          foreach (PlayerModel player4 in room2.Players)
          {
            bool flag3 = player4.Slot != obj0.Slot;
            if (player4.Client != null && player3.AccountIdIsValid() && num4 == (int) byte.MaxValue & flag3)
              ((MatchXender) this).SendPacket(actionModel_0_2, player4.Client);
          }
          break;
        case 65:
          string str1 = $"{syncClientPacket.ReadH()}.{syncClientPacket.ReadH()}";
          int num6 = (int) syncClientPacket.ReadUD();
          uint num7 = syncClientPacket.ReadUD();
          num1 = (int) syncClientPacket.ReadC();
          int num8 = (int) num7;
          RoomModel orGetRoom = DamageManager.CreateOrGetRoom((uint) num6, (uint) num8);
          if (orGetRoom == null)
            break;
          PlayerModel playerModel = orGetRoom.AddPlayer(this.ipendPoint_0, obj0, str1);
          if (playerModel == null)
            break;
          if (!playerModel.Integrity)
            ((Equipment) playerModel).ResetBattleInfos();
          ((MatchXender) this).SendPacket(ObjectMove.GET_CODE(), playerModel.Client);
          if (!ConfigLoader.IsTestMode)
            break;
          CLogger.Print($"Player Connected. [{playerModel.Client.Address}:{playerModel.Client.Port}]", LoggerType.Warning, (Exception) null);
          break;
        case 67:
          string str2 = $"{syncClientPacket.ReadH()}.{syncClientPacket.ReadH()}";
          int Assist3 = (int) syncClientPacket.ReadUD();
          uint num9 = syncClientPacket.ReadUD();
          num1 = (int) syncClientPacket.ReadC();
          int num10 = (int) num9;
          RoomModel room3 = DamageManager.GetRoom((uint) Assist3, (uint) num10);
          if (room3 == null)
            break;
          if (((GrenadeInfo) room3).RemovePlayer(this.ipendPoint_0, obj0, str2) && ConfigLoader.IsTestMode)
            CLogger.Print($"Player Disconnected. [{this.ipendPoint_0.Address}:{this.ipendPoint_0.Port}]", LoggerType.Warning, (Exception) null);
          if (((ObjectAnimInfo) room3).GetPlayersCount() != 0)
            break;
          DamageManager.RemoveRoom(room3.UniqueRoomId, room3.RoomSeed);
          break;
        case 97:
          int Assist4 = (int) syncClientPacket.ReadUD();
          num1 = (int) syncClientPacket.ReadC();
          int num11 = (int) syncClientPacket.ReadUD();
          RoomModel room4 = DamageManager.GetRoom((uint) Assist4, (uint) num11);
          byte[] data = obj0.Data;
          if (room4 == null)
            break;
          PlayerModel player5 = ((StageControlInfo) room4).GetPlayer(obj0.Slot, this.ipendPoint_0);
          if (player5 == null)
            break;
          player5.LastPing = ((PlayerModel) obj0).get_ReceiveDate();
          ((MatchXender) this).SendPacket(data, this.ipendPoint_0);
          if (!ConfigLoader.SendPingSync)
            break;
          int num12;
          player5.Latency = MatchSync.PingTime($"{this.ipendPoint_0.Address}", data, (int) this.socket_0.Ttl, 120, false, ref num12);
          player5.Ping = num12;
          RemovePlayerSync.SendPingSync(room4, player5);
          break;
        case 131:
          syncClientPacket.Advance(length);
          int Assist5 = (int) syncClientPacket.ReadUD();
          int num13 = (int) syncClientPacket.ReadC();
          int num14 = (int) syncClientPacket.ReadUD();
          RoomModel room5 = DamageManager.GetRoom((uint) Assist5, (uint) num14);
          if (room5 == null)
            break;
          PlayerModel player6 = ((StageControlInfo) room5).GetPlayer(obj0.Slot, this.ipendPoint_0);
          if (player6 == null || player6.PlayerIdByServer != obj0.AccountId)
            break;
          room5.BotMode = true;
          PlayerModel player7 = ((ObjectMoveInfo) room5).GetPlayer(num13, false);
          byte[] actionModel_0_3 = AllUtils.BaseWriteCode(132, PROTOCOL_EVENTS_ACTION.GET_CODE(numArray), num13, AllUtils.GetDuration(player7.StartTime), obj0.Round, obj0.Respawn, obj0.RoundNumber, obj0.AccountId);
          foreach (PlayerModel player8 in room5.Players)
          {
            if (player8.Client != null && player6.AccountIdIsValid() && player8.Slot != obj0.Slot)
              ((MatchXender) this).SendPacket(actionModel_0_3, player8.Client);
          }
          break;
        case 132:
          syncClientPacket.Advance(length);
          int Assist6 = (int) syncClientPacket.ReadUD();
          num1 = (int) syncClientPacket.ReadC();
          int num15 = (int) syncClientPacket.ReadUD();
          RoomModel room6 = DamageManager.GetRoom((uint) Assist6, (uint) num15);
          if (room6 == null)
            break;
          PlayerModel player9 = ((StageControlInfo) room6).GetPlayer(obj0.Slot, this.ipendPoint_0);
          if (player9 == null || player9.PlayerIdByServer != obj0.AccountId)
            break;
          room6.BotMode = true;
          byte[] actionModel_0_4 = AllUtils.BaseWriteCode(132, PROTOCOL_EVENTS_ACTION.GET_CODE(numArray), obj0.Slot, AllUtils.GetDuration(player9.StartTime), obj0.Round, obj0.Respawn, obj0.RoundNumber, obj0.AccountId);
          foreach (PlayerModel player10 in room6.Players)
          {
            if (player10.Client != null && player9.AccountIdIsValid() && player10.Slot != obj0.Slot)
              ((MatchXender) this).SendPacket(actionModel_0_4, player10.Client);
          }
          break;
        default:
          CLogger.Print(Bitwise.ToHexData($"Opcode Not Found: [{obj0.Opcode}]", withEndData), LoggerType.Opcode, (Exception) null);
          break;
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private List<ObjectHitInfo> method_0(
    ActionModel socket_1,
    RoomModel ipendPoint_1,
    [In] float obj2,
    [In] ref byte[] obj3)
  {
    obj3 = new byte[0];
    if (ipendPoint_1 == null)
      return (List<ObjectHitInfo>) null;
    if (((AnimModel) socket_1).get_Data().Length == 0)
      return new List<ObjectHitInfo>();
    byte[] Length = ((AnimModel) socket_1).get_Data();
    List<ObjectHitInfo> objectHitInfoList = new List<ObjectHitInfo>();
    SyncClientPacket syncClientPacket = new SyncClientPacket(Length);
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      uint num1 = 0;
      PlayerModel player = ((ObjectMoveInfo) ipendPoint_1).GetPlayer((int) socket_1.Slot, false);
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.ActionState))
      {
        num1 += 256U /*0x0100*/;
        ActionStateInfo Info = Animation.ReadInfo(socket_1, syncClientPacket, false);
        if (!ipendPoint_1.BotMode)
        {
          Equipment equip = player.Equip;
          if (player != null && equip != null)
          {
            int num2 = 0;
            byte num3 = 0;
            byte num4 = 0;
            if (Info.Flag.HasFlag((System.Enum) WeaponSyncType.Primary))
            {
              num2 = equip.WpnPrimary;
              int idStatics1 = ComDiv.GetIdStatics(((RoomModel) equip).get_Accessory(), 1);
              int idStatics2 = ComDiv.GetIdStatics(((RoomModel) equip).get_Accessory(), 3);
              num3 = idStatics1 == 30 ? (byte) idStatics2 : num3;
            }
            if (Info.Flag.HasFlag((System.Enum) WeaponSyncType.Secondary))
              num2 = equip.WpnSecondary;
            if (Info.Flag.HasFlag((System.Enum) WeaponSyncType.Melee))
              num2 = equip.WpnMelee;
            if (Info.Flag.HasFlag((System.Enum) WeaponSyncType.Explosive))
              num2 = equip.WpnExplosive;
            if (Info.Flag.HasFlag((System.Enum) WeaponSyncType.Special))
              num2 = ((RoomModel) equip).get_WpnSpecial();
            if (Info.Flag.HasFlag((System.Enum) WeaponSyncType.Mission) && ipendPoint_1.RoomType == RoomCondition.Bomb)
              num2 = 5009001;
            if (Info.Flag.HasFlag((System.Enum) WeaponSyncType.Dual))
            {
              num4 = (byte) 16 /*0x10*/;
              if (Info.Action.HasFlag((System.Enum) ActionFlag.Unk2048))
                num2 = equip.WpnPrimary;
              if (Info.Action.HasFlag((System.Enum) ActionFlag.Unk4096))
                num2 = equip.WpnPrimary;
            }
            if (Info.Flag.HasFlag((System.Enum) WeaponSyncType.Ext))
            {
              num4 = (byte) 16 /*0x10*/;
              if (Info.Action.HasFlag((System.Enum) ActionFlag.Unk2048))
                num2 = equip.WpnSecondary;
              if (Info.Action.HasFlag((System.Enum) ActionFlag.Unk4096))
                num2 = equip.WpnSecondary;
            }
            ObjectInfo objectInfo = new ObjectInfo(6);
            ((ObjectHitInfo) objectInfo).ObjId = player.Slot;
            ((ObjectHitInfo) objectInfo).WeaponId = num2;
            ((ObjectHitInfo) objectInfo).Accessory = num3;
            ((ObjectHitInfo) objectInfo).Extensions = num4;
            ObjectHitInfo objectHitInfo = (ObjectHitInfo) objectInfo;
            objectHitInfoList.Add(objectHitInfo);
          }
        }
        CharaFireNHitData.WriteInfo(syncServerPacket, Info);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.Animation))
      {
        num1 += 2U;
        AnimationInfo Info = CharaFireNHitData.ReadInfo(socket_1, syncClientPacket, false);
        DropWeapon.WriteInfo(syncServerPacket, Info);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.PosRotation))
      {
        num1 += 134217728U /*0x08000000*/;
        PosRotationInfo Info = RadioChat.ReadInfo(socket_1, syncClientPacket, false);
        if (player != null)
          player.Position = new Half3(Info.RotationX, Info.RotationY, Info.RotationZ);
        socket_1.Flag |= UdpGameEvent.SoundPosRotation;
        SeizeDataForClient.WriteInfo(syncServerPacket, Info);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.SoundPosRotation))
      {
        num1 += 8388608U /*0x800000*/;
        SoundPosRotationInfo soundPosRotationInfo = Suicide.ReadInfo(socket_1, syncClientPacket, obj2, false);
        UseObject.WriteInfo(syncServerPacket, soundPosRotationInfo);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.UseObject))
      {
        num1 += 4U;
        List<UseObjectInfo> Hits = WeaponRecoil.ReadInfo(socket_1, syncClientPacket, false);
        for (int index = 0; index < Hits.Count; ++index)
        {
          UseObjectInfo useObjectInfo = Hits[index];
          if (!ipendPoint_1.BotMode && useObjectInfo.ObjectId != ushort.MaxValue)
          {
            ObjectInfo objectInfo1 = ((DropedWeaponInfo) ipendPoint_1).GetObject((int) useObjectInfo.ObjectId);
            if (objectInfo1 != null)
            {
              bool flag = false;
              if (useObjectInfo.SpaceFlags.HasFlag((System.Enum) CharaMoves.HeliInMove) && ((ObjectModel) objectInfo1).get_UseDate().ToString("yyMMddHHmm").Equals("0101010000"))
                flag = true;
              useObjectInfo.SpaceFlags.HasFlag((System.Enum) CharaMoves.HeliUnknown);
              useObjectInfo.SpaceFlags.HasFlag((System.Enum) CharaMoves.HeliLeave);
              if (useObjectInfo.SpaceFlags.HasFlag((System.Enum) CharaMoves.HeliStopped))
              {
                AnimModel animation = objectInfo1.Animation;
                if (animation != null && animation.Id == 0)
                  ((PacketModel) ((ObjectModel) objectInfo1).get_Model()).GetAnim(animation.NextAnim, 0.0f, 0.0f, objectInfo1);
              }
              if (!flag)
              {
                ObjectInfo objectInfo2 = new ObjectInfo(3);
                ((ObjectHitInfo) objectInfo2).ObjSyncId = 1;
                ((ObjectHitInfo) objectInfo2).ObjId = objectInfo1.Id;
                ((ObjectHitInfo) objectInfo2).ObjLife = objectInfo1.Life;
                ((ObjectHitInfo) objectInfo2).AnimId1 = (int) byte.MaxValue;
                ((ObjectHitInfo) objectInfo2).AnimId2 = objectInfo1.Animation != null ? objectInfo1.Animation.Id : (int) byte.MaxValue;
                ((ObjectHitInfo) objectInfo2).SpecialUse = AllUtils.GetDuration(((ObjectModel) objectInfo1).get_UseDate());
                ObjectHitInfo objectHitInfo = (ObjectHitInfo) objectInfo2;
                objectHitInfoList.Add(objectHitInfo);
              }
            }
          }
          else
            MatchSync.RemoveHit((IList) Hits, index--);
        }
        WeaponSync.WriteInfo(syncServerPacket, Hits);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.ActionForObjectSync))
      {
        num1 += 16U /*0x10*/;
        ActionObjectInfo Info = ActionState.ReadInfo(socket_1, syncClientPacket, false);
        if (player != null)
          ipendPoint_1.SyncInfo(objectHitInfoList, 1);
        Animation.WriteInfo(syncServerPacket, Info);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.RadioChat))
      {
        num1 += 32U /*0x20*/;
        RadioChatInfo Info = SeizeDataForClient.ReadInfo(socket_1, syncClientPacket, false);
        SoundPosRotation.WriteInfo(syncServerPacket, Info);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.WeaponSync))
      {
        num1 += 67108864U /*0x04000000*/;
        WeaponSyncInfo Info = CharaStructureXML.ReadInfo(socket_1, syncClientPacket, false, false);
        if (player != null)
        {
          player.WeaponId = Info.WeaponId;
          player.Accessory = Info.Accessory;
          player.Extensions = Info.Extensions;
          player.WeaponClass = Info.WeaponClass;
          ipendPoint_1.SyncInfo(objectHitInfoList, 2);
        }
        CharaStructureXML.WriteInfo(syncServerPacket, Info);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.WeaponRecoil))
      {
        num1 += 128U /*0x80*/;
        WeaponRecoilInfo Infos = WeaponSync.ReadInfo(socket_1, syncClientPacket, false);
        CharaStructureXML.WriteInfo(syncServerPacket, Infos);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.HpSync))
      {
        num1 += 8U;
        HPSyncInfo Hits = MissionData.ReadInfo(socket_1, syncClientPacket, false);
        PosRotation.WriteInfo(syncServerPacket, Hits);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.Suicide))
      {
        num1 += 1048576U /*0x100000*/;
        List<SuicideInfo> Info = UseObject.ReadInfo(socket_1, syncClientPacket, false, false);
        List<DeathServerData> deathServerDataList1 = new List<DeathServerData>();
        List<DeathServerData> deathServerDataList2;
        if (player != null)
        {
          int Portal = -1;
          int num5 = 0;
          for (int index = 0; index < Info.Count; ++index)
          {
            SuicideInfo suicideInfo = Info[index];
            if (!player.Dead && player.Life > 0)
            {
              int hitDamageBot = AllUtils.GetHitDamageBot(suicideInfo.HitInfo);
              int killerId = AllUtils.GetKillerId(suicideInfo.HitInfo);
              int objectType = AllUtils.GetObjectType(suicideInfo.HitInfo);
              CharaHitPart HitPart = (CharaHitPart) ((int) (suicideInfo.HitInfo >> 4) & 63 /*0x3F*/);
              CharaDeath charaDeath = AllUtils.GetCharaDeath(suicideInfo.HitInfo);
              if (objectType == 1 || objectType == 0)
                Portal = killerId;
              num5 = suicideInfo.WeaponId;
              \u007B3c03a772\u002Dca25\u002D42e6\u002Db519\u002D6caf572c6109\u007D.SimpleDeath(ipendPoint_1, deathServerDataList1, objectHitInfoList, player, player, hitDamageBot, num5, HitPart, charaDeath);
              if (hitDamageBot > 0)
              {
                if (ConfigLoader.UseHitMarker)
                  MatchRoundSync.SendHitMarkerSync(ipendPoint_1, player, charaDeath, HitType.Normal, hitDamageBot);
                ObjectInfo objectInfo = new ObjectInfo(2);
                ((ObjectHitInfo) objectInfo).ObjId = player.Slot;
                ((ObjectHitInfo) objectInfo).ObjLife = player.Life;
                objectInfo.set_HitPart(HitPart);
                ((ObjectHitInfo) objectInfo).KillerSlot = Portal;
                ((ObjectHitInfo) objectInfo).Position = suicideInfo.PlayerPos;
                objectInfo.set_DeathType(charaDeath);
                ((ObjectHitInfo) objectInfo).WeaponId = num5;
                ObjectHitInfo objectHitInfo = (ObjectHitInfo) objectInfo;
                objectHitInfoList.Add(objectHitInfo);
              }
            }
            else
              MatchSync.RemoveHit((IList) Info, index--);
          }
          if (deathServerDataList1.Count > 0)
            MatchRoundSync.SendDeathSync(ipendPoint_1, player, Portal, num5, deathServerDataList1);
          deathServerDataList2 = (List<DeathServerData>) null;
        }
        else
          Info = new List<SuicideInfo>();
        WeaponRecoil.WriteInfo(syncServerPacket, Info);
        deathServerDataList2 = (List<DeathServerData>) null;
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.MissionData))
      {
        num1 += 2048U /*0x0800*/;
        MissionDataInfo C = PosRotation.ReadInfo(socket_1, syncClientPacket, obj2, false, false);
        if (ipendPoint_1.Map != null && player != null && !player.Dead && (double) C.PlantTime > 0.0 && !C.BombEnum.HasFlag((System.Enum) BombFlag.Stop))
        {
          BombPosition bomb = ((ObjectHitInfo) ipendPoint_1.Map).GetBomb(C.BombId);
          if (bomb != null)
          {
            bool flag;
            Vector3 float_1 = (Vector3) ((flag = C.BombEnum.HasFlag((System.Enum) BombFlag.Defuse)) ? ipendPoint_1.BombPosition : (C.BombEnum.HasFlag((System.Enum) BombFlag.Start) ? ((CharaModel) bomb).get_Position() : new Half3((ushort) 0, (ushort) 0, (ushort) 0)));
            double num6 = (double) Vector3.Distance((Vector3) player.Position, float_1);
            TeamEnum swappedTeam = MatchSync.GetSwappedTeam(player, ipendPoint_1);
            if ((((CharaModel) bomb).get_EveryWhere() || num6 <= 2.0) && (swappedTeam == TeamEnum.CT_TEAM & flag || swappedTeam == TeamEnum.FR_TEAM && !flag))
            {
              if ((double) player.C4Time != (double) C.PlantTime)
              {
                player.C4First = DateTimeUtil.Now();
                player.C4Time = C.PlantTime;
              }
              double duration = ComDiv.GetDuration(player.C4First);
              float num7 = flag ? player.DefuseDuration : player.PlantDuration;
              if (((double) obj2 >= (double) C.PlantTime + (double) num7 || duration >= (double) num7) && (!ipendPoint_1.HasC4 && C.BombEnum.HasFlag((System.Enum) BombFlag.Start) || ipendPoint_1.HasC4 & flag))
              {
                ipendPoint_1.HasC4 = !ipendPoint_1.HasC4;
                C.Bomb |= 2;
                SendMatchInfo.SendBombSync(ipendPoint_1, player, C.BombEnum.HasFlag((System.Enum) BombFlag.Defuse) ? 1 : 0, C.BombId);
              }
            }
          }
        }
        RadioChat.WriteInfo(syncServerPacket, C);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.DropWeapon))
      {
        num1 += 4194304U /*0x400000*/;
        DropWeaponInfo Hits = FireData.ReadInfo(socket_1, syncClientPacket, false);
        if (!ipendPoint_1.BotMode)
        {
          ++ipendPoint_1.DropCounter;
          if ((int) ipendPoint_1.DropCounter > (int) ConfigLoader.MaxDropWpnCount)
            ipendPoint_1.DropCounter = (byte) 0;
          Hits.Counter = ipendPoint_1.DropCounter;
          Equipment equip = player.Equip;
          if (player != null && equip != null)
          {
            int idStatics = ComDiv.GetIdStatics(Hits.WeaponId, 1);
            if (idStatics == 1)
              equip.WpnPrimary = 0;
            if (idStatics == 2)
              equip.WpnSecondary = 0;
          }
        }
        FireDataOnObject.WriteInfo(syncServerPacket, Hits);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.GetWeaponForClient))
      {
        num1 += 16777216U /*0x01000000*/;
        WeaponClient Info = GetWeaponForHost.ReadInfo(socket_1, syncClientPacket, false);
        if (!ipendPoint_1.BotMode)
        {
          Equipment equip = player.Equip;
          if (player != null && equip != null)
          {
            int idStatics = ComDiv.GetIdStatics(Info.WeaponId, 1);
            if (idStatics == 1)
              equip.WpnPrimary = Info.WeaponId;
            if (idStatics == 2)
              equip.WpnSecondary = Info.WeaponId;
          }
        }
        GrenadeHit.WriteInfo(syncServerPacket, Info);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.FireData))
      {
        num1 += 33554432U /*0x02000000*/;
        FireDataInfo Info = FireDataOnObject.ReadInfo(socket_1, syncClientPacket, false);
        FireNHitDataOnObject.WriteInfo(syncServerPacket, Info);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.CharaFireNHitData))
      {
        num1 += 1024U /*0x0400*/;
        List<CharaFireNHitDataInfo> Info = DropWeapon.ReadInfo(socket_1, syncClientPacket, false, false);
        FireData.WriteInfo(syncServerPacket, Info);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.HitData))
      {
        num1 += 131072U /*0x020000*/;
        List<HitDataInfo> Hits = HpSync.ReadInfo(socket_1, syncClientPacket, false);
        List<DeathServerData> charaHitPart_0 = new List<DeathServerData>();
        if (player != null)
        {
          int num8 = 0;
          for (int index = 0; index < Hits.Count; ++index)
          {
            HitDataInfo hitDataInfo = Hits[index];
            if (hitDataInfo.HitEnum != HitType.HelmetProtection && hitDataInfo.HitEnum != HitType.HeadshotProtection)
            {
              int num9;
              if (AllUtils.ValidateHitData(AllUtils.GetHitDamageNormal(hitDataInfo.HitIndex), hitDataInfo, ref num9))
              {
                int objectId = (int) hitDataInfo.ObjectId;
                CharaHitPart hitPart = AllUtils.GetHitPart(hitDataInfo.HitIndex);
                CharaDeath charaDeath = CharaDeath.DEFAULT;
                num8 = hitDataInfo.WeaponId;
                ObjectType hitType = AllUtils.GetHitType(hitDataInfo.HitIndex);
                switch (hitType)
                {
                  case ObjectType.User:
                    PlayerModel Deaths;
                    if (ipendPoint_1.GetPlayer(objectId, ref Deaths) && player.RespawnIsValid() && !player.Dead && !Deaths.Dead && !Deaths.Immortal)
                    {
                      if (hitPart == CharaHitPart.HEAD)
                        charaDeath = CharaDeath.HEADSHOT;
                      if (ipendPoint_1.RoomType == RoomCondition.DeathMatch && ipendPoint_1.Rule == MapRules.HeadHunter && charaDeath != CharaDeath.HEADSHOT)
                        num9 = 1;
                      else if (ipendPoint_1.RoomType == RoomCondition.Boss && charaDeath == CharaDeath.HEADSHOT)
                      {
                        if (ipendPoint_1.LastRound == 1 && Deaths.Team == TeamEnum.FR_TEAM || ipendPoint_1.LastRound == 2 && Deaths.Team == TeamEnum.CT_TEAM)
                          num9 /= 10;
                      }
                      else if (ipendPoint_1.RoomType == RoomCondition.DeathMatch && ipendPoint_1.Rule == MapRules.Chaos)
                        num9 = 200;
                      \u007B3c03a772\u002Dca25\u002D42e6\u002Db519\u002D6caf572c6109\u007D.SimpleDeath(ipendPoint_1, charaHitPart_0, objectHitInfoList, player, Deaths, num9, num8, hitPart, charaDeath);
                      if (num9 > 0)
                      {
                        if (ConfigLoader.UseHitMarker)
                          MatchRoundSync.SendHitMarkerSync(ipendPoint_1, player, charaDeath, hitDataInfo.HitEnum, num9);
                        ObjectInfo objectInfo = new ObjectInfo(2);
                        ((ObjectHitInfo) objectInfo).ObjId = Deaths.Slot;
                        ((ObjectHitInfo) objectInfo).ObjLife = Deaths.Life;
                        objectInfo.set_HitPart(hitPart);
                        ((ObjectHitInfo) objectInfo).KillerSlot = player.Slot;
                        ((ObjectHitInfo) objectInfo).Position = (Half3) ((Vector3) Deaths.Position - (Vector3) player.Position);
                        objectInfo.set_DeathType(charaDeath);
                        ((ObjectHitInfo) objectInfo).WeaponId = num8;
                        ObjectHitInfo objectHitInfo = (ObjectHitInfo) objectInfo;
                        objectHitInfoList.Add(objectHitInfo);
                        continue;
                      }
                      continue;
                    }
                    MatchSync.RemoveHit((IList) Hits, index--);
                    continue;
                  case ObjectType.UserObject:
                    continue;
                  case ObjectType.Object:
                    ObjectInfo objectInfo3 = ((DropedWeaponInfo) ipendPoint_1).GetObject(objectId);
                    ObjectModel objectModel = ((ObjectModel) objectInfo3)?.get_Model();
                    if (objectModel != null && objectModel.Destroyable)
                    {
                      if (objectInfo3.Life > 0)
                      {
                        objectInfo3.Life -= num9;
                        if (objectInfo3.Life <= 0)
                        {
                          objectInfo3.Life = 0;
                          \u007B3c03a772\u002Dca25\u002D42e6\u002Db519\u002D6caf572c6109\u007D.BoomDeath(ipendPoint_1, player, num9, num8, charaHitPart_0, objectHitInfoList, hitDataInfo.BoomPlayers, CharaHitPart.ALL, CharaDeath.OBJECT_EXPLOSION);
                        }
                        objectInfo3.DestroyState = ((PacketModel) objectModel).CheckDestroyState(objectInfo3.Life);
                        DamageManager.SabotageDestroy(ipendPoint_1, player, objectModel, objectInfo3, num9);
                        float duration = AllUtils.GetDuration(((ObjectModel) objectInfo3).get_UseDate());
                        if (objectInfo3.Animation != null && (double) ((AssistServerData) objectInfo3.Animation).get_Duration() > 0.0 && (double) duration >= (double) ((AssistServerData) objectInfo3.Animation).get_Duration())
                          ((PacketModel) ((ObjectModel) objectInfo3).get_Model()).GetAnim(objectInfo3.Animation.NextAnim, duration, ((AssistServerData) objectInfo3.Animation).get_Duration(), objectInfo3);
                        ObjectInfo objectInfo4 = new ObjectInfo(objectModel.UpdateId);
                        ((ObjectHitInfo) objectInfo4).ObjId = objectInfo3.Id;
                        ((ObjectHitInfo) objectInfo4).ObjLife = objectInfo3.Life;
                        ((ObjectHitInfo) objectInfo4).KillerSlot = (int) socket_1.Slot;
                        ((ObjectHitInfo) objectInfo4).ObjSyncId = objectModel.NeedSync ? 1 : 0;
                        ((ObjectHitInfo) objectInfo4).SpecialUse = duration;
                        ((ObjectHitInfo) objectInfo4).AnimId1 = objectModel.Animation;
                        ((ObjectHitInfo) objectInfo4).AnimId2 = objectInfo3.Animation != null ? objectInfo3.Animation.Id : (int) byte.MaxValue;
                        ((ObjectHitInfo) objectInfo4).DestroyState = objectInfo3.DestroyState;
                        ObjectHitInfo objectHitInfo = (ObjectHitInfo) objectInfo4;
                        objectHitInfoList.Add(objectHitInfo);
                        continue;
                      }
                      continue;
                    }
                    if (ConfigLoader.SendFailMsg && objectModel == null)
                    {
                      CLogger.Print($"Fire Obj: {objectId} Map: {ipendPoint_1.MapId} Invalid Object.", LoggerType.Warning, (Exception) null);
                      ((Equipment) player).LogPlayerPos(hitDataInfo.EndBullet);
                      continue;
                    }
                    continue;
                  default:
                    CLogger.Print($"HitType: ({hitType}/{(int) hitType}) Slot: {socket_1.Slot}", LoggerType.Warning, (Exception) null);
                    CLogger.Print($"BoomPlayers: {hitDataInfo.BoomInfo} {hitDataInfo.BoomPlayers.Count}", LoggerType.Warning, (Exception) null);
                    continue;
                }
              }
              else
                MatchSync.RemoveHit((IList) Hits, index--);
            }
          }
          if (charaHitPart_0.Count > 0)
            MatchRoundSync.SendDeathSync(ipendPoint_1, player, (int) byte.MaxValue, num8, charaHitPart_0);
        }
        else
          Hits = new List<HitDataInfo>();
        MissionData.WriteInfo(syncServerPacket, Hits);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.GrenadeHit))
      {
        num1 += 268435456U /*0x10000000*/;
        List<GrenadeHitInfo> Info = HitData.ReadInfo(socket_1, syncClientPacket, false, false);
        List<DeathServerData> charaHitPart_0 = new List<DeathServerData>();
        if (player != null)
        {
          int num10 = -1;
          int num11 = 0;
          for (int index = 0; index < Info.Count; ++index)
          {
            GrenadeHitInfo grenadeHitInfo = Info[index];
            int num12;
            if (AllUtils.ValidateGrenadeHit(AllUtils.GetHitDamageNormal(grenadeHitInfo.HitInfo), grenadeHitInfo, ref num12))
            {
              int objectId = (int) grenadeHitInfo.ObjectId;
              CharaHitPart hitPart = AllUtils.GetHitPart(grenadeHitInfo.HitInfo);
              num11 = grenadeHitInfo.WeaponId;
              ObjectType hitType = AllUtils.GetHitType(grenadeHitInfo.HitInfo);
              switch (hitType)
              {
                case ObjectType.User:
                  ++num10;
                  PlayerModel Deaths;
                  if (num12 > 0 && ipendPoint_1.GetPlayer(objectId, ref Deaths) && player.RespawnIsValid() && !Deaths.Dead && !Deaths.Immortal)
                  {
                    TeamEnum teamEnum = num10 % 2 == 0 ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM;
                    if (grenadeHitInfo.DeathType == CharaDeath.MEDICAL_KIT)
                    {
                      Deaths.Life += num12;
                      ((Equipment) Deaths).CheckLifeValue();
                    }
                    else if (grenadeHitInfo.DeathType == CharaDeath.BOOM && ClassType.Dino != grenadeHitInfo.WeaponClass && (teamEnum == TeamEnum.FR_TEAM || teamEnum == TeamEnum.CT_TEAM))
                    {
                      num12 = (int) Math.Ceiling((double) num12 / 2.7);
                      \u007B3c03a772\u002Dca25\u002D42e6\u002Db519\u002D6caf572c6109\u007D.SimpleDeath(ipendPoint_1, charaHitPart_0, objectHitInfoList, player, Deaths, num12, num11, hitPart, grenadeHitInfo.DeathType);
                    }
                    else
                      \u007B3c03a772\u002Dca25\u002D42e6\u002Db519\u002D6caf572c6109\u007D.SimpleDeath(ipendPoint_1, charaHitPart_0, objectHitInfoList, player, Deaths, num12, num11, hitPart, grenadeHitInfo.DeathType);
                    if (num12 > 0)
                    {
                      if (ConfigLoader.UseHitMarker)
                        MatchRoundSync.SendHitMarkerSync(ipendPoint_1, player, grenadeHitInfo.DeathType, grenadeHitInfo.HitEnum, num12);
                      ObjectInfo objectInfo = new ObjectInfo(2);
                      ((ObjectHitInfo) objectInfo).ObjId = Deaths.Slot;
                      ((ObjectHitInfo) objectInfo).ObjLife = Deaths.Life;
                      objectInfo.set_HitPart(hitPart);
                      ((ObjectHitInfo) objectInfo).KillerSlot = player.Slot;
                      ((ObjectHitInfo) objectInfo).Position = (Half3) ((Vector3) Deaths.Position - (Vector3) player.Position);
                      objectInfo.set_DeathType(grenadeHitInfo.DeathType);
                      ((ObjectHitInfo) objectInfo).WeaponId = num11;
                      ObjectHitInfo objectHitInfo = (ObjectHitInfo) objectInfo;
                      objectHitInfoList.Add(objectHitInfo);
                      continue;
                    }
                    continue;
                  }
                  MatchSync.RemoveHit((IList) Info, index--);
                  continue;
                case ObjectType.UserObject:
                  continue;
                case ObjectType.Object:
                  ObjectInfo objectInfo5 = ((DropedWeaponInfo) ipendPoint_1).GetObject(objectId);
                  ObjectModel objectModel = ((ObjectModel) objectInfo5)?.get_Model();
                  if (objectModel != null && objectModel.Destroyable && objectInfo5.Life > 0)
                  {
                    objectInfo5.Life -= num12;
                    if (objectInfo5.Life <= 0)
                    {
                      objectInfo5.Life = 0;
                      \u007B3c03a772\u002Dca25\u002D42e6\u002Db519\u002D6caf572c6109\u007D.BoomDeath(ipendPoint_1, player, num12, num11, charaHitPart_0, objectHitInfoList, grenadeHitInfo.BoomPlayers, CharaHitPart.ALL, CharaDeath.OBJECT_EXPLOSION);
                    }
                    objectInfo5.DestroyState = ((PacketModel) objectModel).CheckDestroyState(objectInfo5.Life);
                    DamageManager.SabotageDestroy(ipendPoint_1, player, objectModel, objectInfo5, num12);
                    if (num12 > 0)
                    {
                      ObjectInfo objectInfo6 = new ObjectInfo(objectModel.UpdateId);
                      ((ObjectHitInfo) objectInfo6).ObjId = objectInfo5.Id;
                      ((ObjectHitInfo) objectInfo6).ObjLife = objectInfo5.Life;
                      ((ObjectHitInfo) objectInfo6).KillerSlot = (int) socket_1.Slot;
                      ((ObjectHitInfo) objectInfo6).ObjSyncId = objectModel.NeedSync ? 1 : 0;
                      ((ObjectHitInfo) objectInfo6).AnimId1 = objectModel.Animation;
                      ((ObjectHitInfo) objectInfo6).AnimId2 = objectInfo5.Animation != null ? objectInfo5.Animation.Id : (int) byte.MaxValue;
                      ((ObjectHitInfo) objectInfo6).DestroyState = objectInfo5.DestroyState;
                      ((ObjectHitInfo) objectInfo6).SpecialUse = AllUtils.GetDuration(((ObjectModel) objectInfo5).get_UseDate());
                      ObjectHitInfo objectHitInfo = (ObjectHitInfo) objectInfo6;
                      objectHitInfoList.Add(objectHitInfo);
                      continue;
                    }
                    continue;
                  }
                  if (ConfigLoader.SendFailMsg && objectModel == null)
                  {
                    CLogger.Print($"Boom Obj: {objectId} Map: {ipendPoint_1.MapId} Invalid Object.", LoggerType.Warning, (Exception) null);
                    ((Equipment) player).LogPlayerPos(grenadeHitInfo.HitPos);
                    continue;
                  }
                  continue;
                default:
                  CLogger.Print($"Grenade Boom, HitType: ({hitType}/{(int) hitType})", LoggerType.Warning, (Exception) null);
                  continue;
              }
            }
            else
              MatchSync.RemoveHit((IList) Info, index--);
          }
          if (charaHitPart_0.Count > 0)
            MatchRoundSync.SendDeathSync(ipendPoint_1, player, (int) byte.MaxValue, num11, charaHitPart_0);
        }
        else
          Info = new List<GrenadeHitInfo>();
        HpSync.WriteInfo(syncServerPacket, Info);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.GetWeaponForHost))
      {
        num1 += 512U /*0x0200*/;
        WeaponHost Info = GrenadeHit.ReadInfo(socket_1, syncClientPacket, false);
        HitData.WriteInfo(syncServerPacket, Info);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.FireDataOnObject))
      {
        num1 += 1073741824U /*0x40000000*/;
        FireDataObjectInfo Info = FireNHitDataOnObject.ReadInfo(socket_1, syncClientPacket, false);
        GetWeaponForClient.WriteInfo(syncServerPacket, Info);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.FireNHitDataOnObject))
      {
        num1 += 8192U /*0x2000*/;
        FireNHitDataObjectInfo Info = GetWeaponForClient.ReadInfo(socket_1, syncClientPacket, false);
        if (player != null && !player.Dead)
          SendMatchInfo.SendPortalPass(ipendPoint_1, player, (int) Info.Portal);
        GetWeaponForHost.WriteInfo(syncServerPacket, Info);
      }
      if (socket_1.Flag.HasFlag((System.Enum) UdpGameEvent.SeizeDataForClient))
      {
        num1 += 32768U /*0x8000*/;
        SeizeDataForClientInfo Info = SoundPosRotation.ReadInfo(socket_1, syncClientPacket, true);
        if (player != null)
          ;
        SoundPosRotation.WriteInfo(syncServerPacket, Info);
      }
      obj3 = syncServerPacket.ToArray();
      if ((UdpGameEvent) num1 != socket_1.Flag && ConfigLoader.IsTestMode)
        CLogger.Print(Bitwise.ToHexData($"PVP - Missing Flag Events: '{(ValueType) (uint) socket_1.Flag}' | '{(ValueType) (uint) (socket_1.Flag - num1)}'", Length), LoggerType.Opcode, (Exception) null);
      return objectHitInfoList;
    }
  }
}
