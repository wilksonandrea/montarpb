// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ : GameClientPacket
{
  private void method_1([In] GameServerPacket obj0, List<Account> roomModel_0)
  {
    byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) obj0).GetCompleteBytes("PROTOCOL_ROOM_REQUEST_MAIN_REQ");
    foreach (Account account in roomModel_0)
      account.SendCompletePacket(completeBytes, obj0.GetType().Name);
  }

  public virtual void Read()
  {
    ((PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ) this).int_0 = (int) this.ReadC();
  }
}
