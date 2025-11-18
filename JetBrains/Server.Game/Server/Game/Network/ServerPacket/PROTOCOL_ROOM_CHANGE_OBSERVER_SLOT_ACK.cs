// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_ACK : GameServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 5279);
    this.WriteC((byte) ((PROTOCOL_BATTLE_WINNING_CAM_ACK) this).fragInfos_0.KillingType);
    this.WriteC(((PROTOCOL_BATTLE_WINNING_CAM_ACK) this).fragInfos_0.KillsCount);
    this.WriteC(((PROTOCOL_BATTLE_WINNING_CAM_ACK) this).fragInfos_0.KillerSlot);
    this.WriteD(((PROTOCOL_BATTLE_WINNING_CAM_ACK) this).fragInfos_0.WeaponId);
    this.WriteT(((PROTOCOL_BATTLE_WINNING_CAM_ACK) this).fragInfos_0.X);
    this.WriteT(((PROTOCOL_BATTLE_WINNING_CAM_ACK) this).fragInfos_0.Y);
    this.WriteT(((PROTOCOL_BATTLE_WINNING_CAM_ACK) this).fragInfos_0.Z);
    this.WriteC(((PROTOCOL_BATTLE_WINNING_CAM_ACK) this).fragInfos_0.Flag);
    this.WriteC(((PROTOCOL_BATTLE_WINNING_CAM_ACK) this).fragInfos_0.Unk);
    for (int index = 0; index < (int) ((PROTOCOL_BATTLE_WINNING_CAM_ACK) this).fragInfos_0.KillsCount; ++index)
    {
      FragModel frag = ((PROTOCOL_BATTLE_WINNING_CAM_ACK) this).fragInfos_0.Frags[index];
      this.WriteC(frag.VictimSlot);
      this.WriteC(frag.WeaponClass);
      this.WriteC(frag.HitspotInfo);
      this.WriteH((ushort) frag.KillFlag);
      this.WriteC(frag.Unk);
      this.WriteT(frag.X);
      this.WriteT(frag.Y);
      this.WriteT(frag.Z);
      this.WriteC(frag.AssistSlot);
      this.WriteB(frag.Unks);
    }
  }

  public PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_ACK(uint list_0, [In] Account obj1)
  {
    ((PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK) this).uint_0 = list_0;
    ((PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK) this).account_0 = obj1;
  }
}
