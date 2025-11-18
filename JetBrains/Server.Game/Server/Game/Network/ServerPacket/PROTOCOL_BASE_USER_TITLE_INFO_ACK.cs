// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_USER_TITLE_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_USER_TITLE_INFO_ACK : GameServerPacket
{
  private readonly Account account_0;

  public virtual void Write()
  {
    this.WriteD(3851);
    this.WriteD(((PROTOCOL_COMMUNITY_USER_REPORT_ACK) this).uint_0);
  }

  public PROTOCOL_BASE_USER_TITLE_INFO_ACK([In] uint obj0, [In] int obj1)
  {
    ((PROTOCOL_BASE_USER_TITLE_CHANGE_ACK) this).uint_0 = obj0;
    ((PROTOCOL_BASE_USER_TITLE_CHANGE_ACK) this).int_0 = obj1;
  }
}
