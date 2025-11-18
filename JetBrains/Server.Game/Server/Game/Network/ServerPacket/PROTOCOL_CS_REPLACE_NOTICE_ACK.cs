// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_REPLACE_NOTICE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REPLACE_NOTICE_ACK : GameServerPacket
{
  private readonly EventErrorEnum eventErrorEnum_0;

  public virtual void Write()
  {
    this.WriteH((short) 867);
    this.WriteD(((PROTOCOL_CS_REPLACE_MARK_RESULT_ACK) this).uint_0);
  }

  public PROTOCOL_CS_REPLACE_NOTICE_ACK(Account int_1)
  {
    ((PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK) this).account_0 = int_1;
    ((PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK) this).ulong_0 = ComDiv.GetClanStatus(int_1.Status, int_1.IsOnline);
  }
}
