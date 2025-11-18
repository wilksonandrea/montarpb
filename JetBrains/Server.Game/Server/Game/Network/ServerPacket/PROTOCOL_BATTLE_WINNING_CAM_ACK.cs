// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_WINNING_CAM_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_WINNING_CAM_ACK : GameServerPacket
{
  private readonly FragInfos fragInfos_0;

  public virtual void Write() => this.WriteH((short) 2444);

  public PROTOCOL_BATTLE_WINNING_CAM_ACK(string disposing)
  {
    ((PROTOCOL_BASE_NOTICE_ACK) this).serverConfig_0 = GameXender.Client.Config;
    if (((PROTOCOL_BASE_NOTICE_ACK) this).serverConfig_0 == null)
      return;
    ((PROTOCOL_BASE_NOTICE_ACK) this).string_0 = Translation.GetLabel(((PROTOCOL_BASE_NOTICE_ACK) this).serverConfig_0.ChannelAnnounce);
    ((PROTOCOL_BASE_NOTICE_ACK) this).string_1 = $"{Translation.GetLabel(((PROTOCOL_BASE_NOTICE_ACK) this).serverConfig_0.ChatAnnounce)} \n\r{disposing}";
  }
}
