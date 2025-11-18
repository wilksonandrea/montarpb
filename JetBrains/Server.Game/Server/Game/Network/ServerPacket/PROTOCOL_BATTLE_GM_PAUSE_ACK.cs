// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_GM_PAUSE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_GM_PAUSE_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 2383);
    this.WriteB(BitConverter.GetBytes(((PROTOCOL_BASE_USER_TITLE_INFO_ACK) this).account_0.PlayerId), 0, 4);
    this.WriteQ(((PROTOCOL_BASE_USER_TITLE_INFO_ACK) this).account_0.Title.Flags);
    this.WriteC((byte) ((PROTOCOL_BASE_USER_TITLE_INFO_ACK) this).account_0.Title.Equiped1);
    this.WriteC((byte) ((PROTOCOL_BASE_USER_TITLE_INFO_ACK) this).account_0.Title.Equiped2);
    this.WriteC((byte) ((PROTOCOL_BASE_USER_TITLE_INFO_ACK) this).account_0.Title.Equiped3);
    this.WriteD(((PROTOCOL_BASE_USER_TITLE_INFO_ACK) this).account_0.Title.Slots);
  }

  public PROTOCOL_BATTLE_GM_PAUSE_ACK([In] uint obj0, int int_1, [In] int obj2)
  {
    ((PROTOCOL_BASE_USER_TITLE_RELEASE_ACK) this).uint_0 = obj0;
    ((PROTOCOL_BASE_USER_TITLE_RELEASE_ACK) this).int_0 = int_1;
    ((PROTOCOL_BASE_USER_TITLE_RELEASE_ACK) this).int_1 = obj2;
  }
}
