// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_OPTION_SAVE_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using Plugin.Core.XML;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_OPTION_SAVE_ACK : AuthServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 2464);
    this.WriteH((short) 0);
    this.WriteC((byte) ((PROTOCOL_BASE_MAP_MATCHINGLIST_ACK) this).list_0.Count);
    foreach (MapMatch mapMatch in ((PROTOCOL_BASE_MAP_MATCHINGLIST_ACK) this).list_0)
    {
      this.WriteD(mapMatch.Mode);
      this.WriteC((byte) mapMatch.Id);
      this.WriteC((byte) SystemMapXML.GetMapRule(mapMatch.Mode).Rule);
      this.WriteC((byte) SystemMapXML.GetMapRule(mapMatch.Mode).StageOptions);
      this.WriteC((byte) SystemMapXML.GetMapRule(mapMatch.Mode).Conditions);
      this.WriteC((byte) mapMatch.Limit);
      this.WriteC((byte) mapMatch.Tag);
      this.WriteC(mapMatch.Tag == 2 || mapMatch.Tag == 3 ? (byte) 1 : (byte) 0);
      this.WriteC((byte) 1);
    }
    this.WriteD(((PROTOCOL_BASE_MAP_MATCHINGLIST_ACK) this).list_0.Count != 100 ? 1 : 0);
    this.WriteH((ushort) (((PROTOCOL_BASE_MAP_MATCHINGLIST_ACK) this).int_0 - 100));
    this.WriteH((ushort) SystemMapXML.Matches.Count);
  }
}
