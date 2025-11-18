// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_GET_RANK_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_RANK_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;

  private List<int> method_0(int int_2, int int_3)
  {
    if (int_2 == 0 || int_3 == 0)
      return new List<int>();
    Random random = new Random();
    List<int> intList = new List<int>();
    for (int index = 0; index < int_2; ++index)
      intList.Add(index);
    for (int index1 = 0; index1 < intList.Count; ++index1)
    {
      int index2 = random.Next(intList.Count);
      int num = intList[index1];
      intList[index1] = intList[index2];
      intList[index2] = num;
    }
    return intList.GetRange(0, int_3);
  }

  public PROTOCOL_ROOM_GET_RANK_ACK(int int_2, string int_3)
  {
    ((PROTOCOL_ROOM_GET_NICKNAME_ACK) this).int_0 = int_2;
    ((PROTOCOL_ROOM_GET_NICKNAME_ACK) this).string_0 = int_3;
  }
}
