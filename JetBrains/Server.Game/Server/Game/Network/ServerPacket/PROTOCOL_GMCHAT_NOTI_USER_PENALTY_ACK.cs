// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 2455);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_BASE_NOTICE_ACK) this).serverConfig_0.ChatAnnounceColor);
    this.WriteD(((PROTOCOL_BASE_NOTICE_ACK) this).serverConfig_0.ChannelAnnounceColor);
    this.WriteH((short) 0);
    this.WriteH((ushort) ((PROTOCOL_BASE_NOTICE_ACK) this).string_1.Length);
    this.WriteN(((PROTOCOL_BASE_NOTICE_ACK) this).string_1, ((PROTOCOL_BASE_NOTICE_ACK) this).string_1.Length, "UTF-16LE");
    this.WriteH((ushort) ((PROTOCOL_BASE_NOTICE_ACK) this).string_0.Length);
    this.WriteN(((PROTOCOL_BASE_NOTICE_ACK) this).string_0, ((PROTOCOL_BASE_NOTICE_ACK) this).string_0.Length, "UTF-16LE");
  }

  public PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK(int list_0, [In] byte obj1)
  {
    ((PROTOCOL_BATTLE_MISSION_SEIZE_ACK) this).int_0 = list_0;
    ((PROTOCOL_BATTLE_MISSION_SEIZE_ACK) this).byte_0 = obj1;
  }
}
