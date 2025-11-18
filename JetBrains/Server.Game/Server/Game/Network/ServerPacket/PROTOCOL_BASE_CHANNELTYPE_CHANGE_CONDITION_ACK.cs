// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK : GameServerPacket
{
  public byte[] GetBytes([In] string obj0)
  {
    try
    {
      ((PROTOCOL_BASE_ENTER_PASS_ACK) this).Write();
      return this.MStream.ToArray();
    }
    catch (Exception ex)
    {
      CLogger.Print($"GetBytes problem at: {obj0}; {ex.Message}", LoggerType.Error, ex);
      return new byte[0];
    }
  }

  public byte[] GetCompleteBytes([In] string obj0)
  {
    try
    {
      byte[] bytes = this.GetBytes("GameServerPacket.GetCompleteBytes");
      return bytes.Length >= 2 ? bytes : new byte[0];
    }
    catch (Exception ex)
    {
      CLogger.Print($"GetCompleteBytes problem at: {obj0}; {ex.Message}", LoggerType.Error, ex);
      return new byte[0];
    }
  }

  public new void Dispose()
  {
    try
    {
      ((PROTOCOL_BASE_ENTER_PASS_ACK) this).Dispose(true);
      GC.SuppressFinalize((object) this);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
