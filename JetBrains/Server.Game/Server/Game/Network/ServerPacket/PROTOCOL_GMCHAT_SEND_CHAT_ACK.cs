// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_GMCHAT_SEND_CHAT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_GMCHAT_SEND_CHAT_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly string string_0;
  private readonly string string_1;
  private readonly string string_2;

  public virtual void Write()
  {
    this.WriteH((short) 5292);
    this.WriteD(((PROTOCOL_BATTLE_MISSION_SEIZE_ACK) this).int_0);
    this.WriteC(((PROTOCOL_BATTLE_MISSION_SEIZE_ACK) this).byte_0);
  }

  public PROTOCOL_GMCHAT_SEND_CHAT_ACK(FragInfos bool_1)
  {
    ((PROTOCOL_BATTLE_WINNING_CAM_ACK) this).fragInfos_0 = bool_1;
  }
}
