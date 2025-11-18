// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_LOGOUT_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Enums;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_LOGOUT_ACK : AuthServerPacket
{
  public PROTOCOL_BASE_LOGOUT_ACK(EventErrorEnum account_1, Account eventVisitModel_1, [In] uint obj2)
  {
    ((PROTOCOL_BASE_LOGIN_ACK) this).eventErrorEnum_0 = account_1;
    ((PROTOCOL_BASE_LOGIN_ACK) this).account_0 = eventVisitModel_1;
    ((PROTOCOL_BASE_LOGIN_ACK) this).uint_0 = obj2;
  }

  public virtual void Write()
  {
    this.WriteH((short) 1283);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_BASE_LOGIN_ACK) this).uint_0);
    this.WriteB(new byte[12]);
    this.WriteD(AuthSync.GetFeatures());
    this.WriteH((short) 1402);
    this.WriteB(new byte[16 /*0x10*/]);
    this.WriteB(((PROTOCOL_BASE_MAP_MATCHINGLIST_ACK) this).method_0(((PROTOCOL_BASE_LOGIN_ACK) this).eventErrorEnum_0, ((PROTOCOL_BASE_LOGIN_ACK) this).account_0));
    this.WriteD((uint) ((PROTOCOL_BASE_LOGIN_ACK) this).eventErrorEnum_0);
  }
}
