// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_START_GAME_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_START_GAME_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;

  public PROTOCOL_BATTLE_START_GAME_ACK()
  {
  }

  public virtual void Write()
  {
    this.WriteH((short) 5132);
    this.WriteH((short) 0);
    this.WriteD(0);
    this.WriteC((byte) 0);
    this.WriteB(this.method_0(((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0, ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).list_0));
    this.WriteC((byte) ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.Rounds);
    this.WriteD(AllUtils.GetSlotsFlag(((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0, true, false));
    this.WriteC(((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.ThisModeHaveRounds() || ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.IsDinoMode("") || ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.RoomType == RoomCondition.FreeForAll ? (byte) 2 : (byte) 0);
    if (((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.ThisModeHaveRounds() || ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.IsDinoMode("") || ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.RoomType == RoomCondition.FreeForAll)
    {
      this.WriteH(((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.IsDinoMode("DE") ? (ushort) ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.FRDino : (((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.IsDinoMode("CC") ? (ushort) ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.FRKills : (ushort) ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.FRRounds));
      this.WriteH(((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.IsDinoMode("DE") ? (ushort) ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.CTDino : (((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.IsDinoMode("CC") ? (ushort) ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.CTKills : (ushort) ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.CTRounds));
    }
    this.WriteC(((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.ThisModeHaveRounds() || ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.IsDinoMode("") || ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.RoomType == RoomCondition.FreeForAll ? (byte) 2 : (byte) 0);
    if (((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.ThisModeHaveRounds() || ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.IsDinoMode("") || ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.RoomType == RoomCondition.FreeForAll)
    {
      this.WriteH((short) 0);
      this.WriteH((short) 0);
    }
    this.WriteD(AllUtils.GetSlotsFlag(((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0, false, false));
    this.WriteC((byte) !((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).bool_0);
    this.WriteC((byte) ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).slotModel_0.Id);
  }

  private byte[] method_0(RoomModel byte_1, [In] List<int> obj1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      if (byte_1.IsDinoMode(""))
      {
        int Disposing1 = obj1.Count == 1 || byte_1.IsDinoMode("CC") ? (int) byte.MaxValue : byte_1.TRex;
        syncServerPacket.WriteC((byte) Disposing1);
        syncServerPacket.WriteC((byte) 10);
        for (int index = 0; index < obj1.Count; ++index)
        {
          int Disposing2 = obj1[index];
          if (Disposing2 != byte_1.TRex && byte_1.IsDinoMode("DE") || byte_1.IsDinoMode("CC"))
            syncServerPacket.WriteC((byte) Disposing2);
        }
        int num = 8 - obj1.Count - (Disposing1 == (int) byte.MaxValue ? 1 : 0);
        for (int index = 0; index < num; ++index)
          syncServerPacket.WriteC(byte.MaxValue);
        syncServerPacket.WriteC(byte.MaxValue);
      }
      else
        syncServerPacket.WriteB(new byte[10]);
      return syncServerPacket.ToArray();
    }
  }

  public PROTOCOL_BATTLE_START_GAME_ACK([In] CountDownEnum obj0)
  {
    ((PROTOCOL_BATTLE_START_COUNTDOWN_ACK) this).countDownEnum_0 = obj0;
  }

  public virtual void Write()
  {
    this.WriteH((short) 5126);
    this.WriteC((byte) ((PROTOCOL_BATTLE_START_COUNTDOWN_ACK) this).countDownEnum_0);
  }
}
