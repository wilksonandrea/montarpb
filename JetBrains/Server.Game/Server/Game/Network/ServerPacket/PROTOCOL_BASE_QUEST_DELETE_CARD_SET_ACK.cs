// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly Account account_0;

  public virtual void Write()
  {
    this.WriteH((short) 2361);
    this.WriteD(((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK) this).uint_0);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK) this).int_0);
    if (((int) ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK) this).uint_0 & 1) != 1)
      return;
    this.WriteD(((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK) this).account_0.Exp);
    this.WriteD(((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK) this).account_0.Gold);
    this.WriteD(((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK) this).account_0.Ribbon);
    this.WriteD(((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK) this).account_0.Ensign);
    this.WriteD(((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK) this).account_0.Medal);
    this.WriteD(((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK) this).account_0.MasterMedal);
    this.WriteD(((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK) this).account_0.Rank);
  }

  public PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK(EventErrorEnum account_1, [In] Account obj1)
  {
    ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).eventErrorEnum_0 = account_1;
    ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0 = obj1;
  }
}
