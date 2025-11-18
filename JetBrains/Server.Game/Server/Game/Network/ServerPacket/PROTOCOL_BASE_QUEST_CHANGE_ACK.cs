// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_QUEST_CHANGE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_QUEST_CHANGE_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;

  public virtual void Write()
  {
    this.WriteH((short) 2363);
    this.WriteQ(((PROTOCOL_BASE_MEDAL_GET_INFO_ACK) this).account_0.PlayerId);
    this.WriteD(((PROTOCOL_BASE_MEDAL_GET_INFO_ACK) this).account_0.Ribbon);
    this.WriteD(((PROTOCOL_BASE_MEDAL_GET_INFO_ACK) this).account_0.Ensign);
    this.WriteD(((PROTOCOL_BASE_MEDAL_GET_INFO_ACK) this).account_0.Medal);
    this.WriteD(((PROTOCOL_BASE_MEDAL_GET_INFO_ACK) this).account_0.MasterMedal);
  }

  public PROTOCOL_BASE_QUEST_CHANGE_ACK([In] uint obj0, [In] int obj1, Account string_5)
  {
    ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK) this).uint_0 = obj0;
    ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK) this).int_0 = obj1;
    ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK) this).account_0 = string_5;
  }
}
