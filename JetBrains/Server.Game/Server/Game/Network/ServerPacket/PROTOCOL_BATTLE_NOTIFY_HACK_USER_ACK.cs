// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_NOTIFY_HACK_USER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_NOTIFY_HACK_USER_ACK : GameServerPacket
{
  private readonly int int_0;

  public int GetSlotKill()
  {
    int[] numArray = new int[18];
    for (int index = 0; index < numArray.Length; ++index)
      numArray[index] = ((PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK) this).roomModel_0.Slots[index].AllKills;
    int slotKill = 0;
    for (int index = 0; index < numArray.Length; ++index)
    {
      if (numArray[index] > numArray[slotKill])
        slotKill = index;
    }
    return slotKill;
  }
}
