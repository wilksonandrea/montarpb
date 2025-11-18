// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_COMMISSION_MASTER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using System;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_COMMISSION_MASTER_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 770);
    this.WriteD(0);
    this.WriteD(((PROTOCOL_CS_CLIENT_ENTER_ACK) this).int_1);
    this.WriteD(((PROTOCOL_CS_CLIENT_ENTER_ACK) this).int_0);
    if (((PROTOCOL_CS_CLIENT_ENTER_ACK) this).int_1 != 0 && ((PROTOCOL_CS_CLIENT_ENTER_ACK) this).int_0 != 0)
      return;
    this.WriteD(ClanManager.Clans.Count);
    this.WriteC((byte) 15);
    this.WriteH((ushort) Math.Ceiling((double) ClanManager.Clans.Count / 15.0));
    this.WriteD(uint.Parse(DateTimeUtil.Now("MMddHHmmss")));
  }
}
