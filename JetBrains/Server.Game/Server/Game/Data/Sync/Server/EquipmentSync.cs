// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Server.EquipmentSync
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Server;

public class EquipmentSync
{
  internal void method_0([In] object obj0)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ((GameSync.Class10) this).gameSync_0.method_2(((GameSync.Class10) this).byte_0);
    }
    catch (Exception ex)
    {
      CLogger.Print("Error processing AuthSync packet in thread pool: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public static void GenDeath(RoomModel Data, SlotModel Address, [In] FragInfos obj2, [In] bool obj3)
  {
    bool Score = Data.IsBotMode();
    int num;
    RoomHitMarker.RegistryFragInfos(Data, Address, ref num, Score, obj3, obj2);
    if (Score)
    {
      Address.Score += Address.KillsOnLife + (int) Data.IngameAiLevel + num;
      if (Address.Score > (int) ushort.MaxValue)
      {
        Address.Score = (int) ushort.MaxValue;
        CLogger.Print($"[PlayerId: {Address.Id.ToString()}] reached the maximum score of the BOT.", LoggerType.Warning, (Exception) null);
      }
      obj2.Score = Address.Score;
    }
    else
    {
      Address.Score += num;
      AllUtils.CompleteMission(Data, Address, obj2, MissionType.NA, 0);
      obj2.Score = num;
    }
    using (PROTOCOL_BATTLE_DEATH_ACK protocolBattleDeathAck = (PROTOCOL_BATTLE_DEATH_ACK) new PROTOCOL_BATTLE_ENDBATTLE_ACK(Data, obj2, Address))
      Data.SendPacketToPlayers((GameServerPacket) protocolBattleDeathAck, SlotState.BATTLE, 0);
    RoomPassPortal.EndBattleByDeath(Data, Address, Score, obj3, obj2);
  }
}
