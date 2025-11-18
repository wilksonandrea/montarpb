// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly StatisticAcemode statisticAcemode_0;
  private readonly ClanModel clanModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 6658);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK) this).uint_0);
    if (((PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK) this).uint_0 != 0U)
      return;
    this.WriteC((byte) (((PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK) this).account_0.Nickname.Length + 1));
    this.WriteN(((PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK) this).account_0.Nickname, ((PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK) this).account_0.Nickname.Length + 2, "UTF-16LE");
    this.WriteQ(((PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK) this).account_0.PlayerId);
  }

  public PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK(
    string int_1,
    string byte_1,
    [In] string obj2,
    [In] Account obj3)
  {
    ((PROTOCOL_GMCHAT_SEND_CHAT_ACK) this).string_0 = int_1;
    ((PROTOCOL_GMCHAT_SEND_CHAT_ACK) this).string_2 = byte_1;
    ((PROTOCOL_GMCHAT_SEND_CHAT_ACK) this).string_1 = obj2;
    ((PROTOCOL_GMCHAT_SEND_CHAT_ACK) this).account_0 = obj3;
  }
}
