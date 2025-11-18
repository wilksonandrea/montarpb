// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_CHECK_MAIN_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CHECK_MAIN_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 3636);
    this.WriteC((byte) 0);
    this.WriteU(((PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK) this).roomModel_0.LeaderName, 66);
    this.WriteD(((PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK) this).roomModel_0.KillTime);
    this.WriteC(((PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK) this).roomModel_0.Limit);
    this.WriteC(((PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK) this).roomModel_0.WatchRuleFlag);
    this.WriteH((ushort) ((PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK) this).roomModel_0.BalanceType);
    this.WriteB(((PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK) this).roomModel_0.RandomMaps);
    this.WriteC(((PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK) this).roomModel_0.CountdownIG);
    this.WriteB(((PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK) this).roomModel_0.LeaderAddr);
    this.WriteC(((PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK) this).roomModel_0.KillCam);
    this.WriteH((short) 0);
  }

  public PROTOCOL_ROOM_CHECK_MAIN_ACK(uint string_1)
  {
    ((PROTOCOL_ROOM_CHANGE_SLOT_ACK) this).uint_0 = string_1;
  }
}
