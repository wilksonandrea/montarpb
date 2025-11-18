// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK : GameServerPacket
{
  private readonly ClanModel clanModel_0;
  private readonly Account account_0;
  private readonly int int_0;

  public PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK([In] string obj0, string clanModel_1)
  {
    ((PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) this).string_1 = obj0;
    ((PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) this).string_0 = clanModel_1;
  }

  public PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(int list_1, int int_2)
  {
    ((PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) this).int_0 = list_1;
    ((PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) this).int_1 = int_2;
  }

  public virtual void Write()
  {
    this.WriteH((short) 6929);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) this).int_0);
    if (((PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) this).int_0 == 0)
    {
      this.WriteC((byte) (((PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) this).string_1.Length + 1));
      this.WriteN(((PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) this).string_1, ((PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) this).string_1.Length + 2, "UTF-16LE");
      this.WriteC((byte) (((PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) this).string_0.Length + 1));
      this.WriteN(((PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) this).string_0, ((PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) this).string_0.Length + 2, "UTF-16LE");
    }
    else
      this.WriteD(((PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) this).int_1);
  }
}
