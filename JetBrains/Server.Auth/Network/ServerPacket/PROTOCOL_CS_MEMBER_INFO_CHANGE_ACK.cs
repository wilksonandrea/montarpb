// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK : AuthServerPacket
{
  private readonly ulong ulong_0;
  private readonly Account account_0;

  public virtual void Write()
  {
    this.WriteH((short) 1042);
    this.WriteC((byte) 0);
    this.WriteC((byte) ((PROTOCOL_BASE_USER_GIFTLIST_ACK) this).list_0.Count);
    for (int index = 0; index < ((PROTOCOL_BASE_USER_GIFTLIST_ACK) this).list_0.Count; ++index)
    {
      MessageModel messageModel = ((PROTOCOL_BASE_USER_GIFTLIST_ACK) this).list_0[index];
    }
    this.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
  }

  public PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK([In] int obj0)
  {
    ((PROTOCOL_BASE_USER_LEAVE_ACK) this).int_0 = obj0;
  }

  public virtual void Write()
  {
    this.WriteH((short) 2329);
    this.WriteD(((PROTOCOL_BASE_USER_LEAVE_ACK) this).int_0);
    this.WriteH((short) 0);
  }
}
