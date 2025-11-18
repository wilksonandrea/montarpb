// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_URL_LIST_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Models;
using Plugin.Core.Models.Map;
using Plugin.Core.XML;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_URL_LIST_ACK : AuthServerPacket
{
  private readonly ServerConfig serverConfig_0;

  public virtual void Write()
  {
    this.WriteH((short) 2462);
    this.WriteH((short) 0);
    this.WriteH((ushort) SystemMapXML.Rules.Count);
    foreach (MapRule rule in SystemMapXML.Rules)
    {
      this.WriteD(rule.Id);
      this.WriteC((byte) 0);
      this.WriteC((byte) rule.Rule);
      this.WriteC((byte) rule.StageOptions);
      this.WriteC((byte) rule.Conditions);
      this.WriteC((byte) 0);
      this.WriteS(rule.Name, 67);
    }
  }

  public PROTOCOL_BASE_URL_LIST_ACK(ServerConfig eventErrorEnum_1)
  {
    ((PROTOCOL_BASE_NOTICE_ACK) this).serverConfig_0 = eventErrorEnum_1;
    if (eventErrorEnum_1 == null)
      return;
    ((PROTOCOL_BASE_NOTICE_ACK) this).string_0 = Translation.GetLabel(eventErrorEnum_1.ChannelAnnounce);
    ((PROTOCOL_BASE_NOTICE_ACK) this).string_1 = Translation.GetLabel(eventErrorEnum_1.ChatAnnounce);
  }
}
