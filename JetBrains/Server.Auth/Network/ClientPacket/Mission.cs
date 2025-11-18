// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.Mission
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Network;
using Server.Auth.Network.ServerPacket;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class Mission
{
  public Mission([In] uint obj0)
    : this()
  {
    ((PROTOCOL_SERVER_MESSAGE_ERROR_ACK) this).uint_0 = obj0;
  }

  public virtual void Write()
  {
    ((BaseServerPacket) this).WriteH((short) 3078);
    ((BaseServerPacket) this).WriteD(((PROTOCOL_SERVER_MESSAGE_ERROR_ACK) this).uint_0);
  }

  public virtual void Read()
  {
  }

  public virtual void Run()
  {
  }

  public Mission()
    : this()
  {
  }

  public int Id { get; [param: In] set; }

  public int RewardId { get; set; }

  public int RewardCount { get; set; }

  public string Name { get; [param: In] set; }

  public string Description
  {
    [CompilerGenerated, SpecialName] get => ((Mission) this).string_1;
    [CompilerGenerated, SpecialName] set => ((Mission) this).string_1 = value;
  }

  public byte[] ObjectivesData
  {
    [CompilerGenerated, SpecialName] get => ((Mission) this).byte_0;
    [CompilerGenerated, SpecialName] set => ((Mission) this).byte_0 = value;
  }
}
