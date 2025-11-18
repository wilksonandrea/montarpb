// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ : AuthClientPacket
{
  private static readonly List<Mission> list_0 = PROTOCOL_MATCH_CLAN_SEASON_REQ.smethod_0("Data/Missions/Basic");

  [CompilerGenerated]
  [SpecialName]
  public string get_Description() => ((Mission) this).string_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_Description(string uint_1) => ((Mission) this).string_1 = uint_1;

  [CompilerGenerated]
  [SpecialName]
  public byte[] get_ObjectivesData() => ((Mission) this).byte_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_ObjectivesData(byte[] value) => ((Mission) this).byte_0 = value;
}
