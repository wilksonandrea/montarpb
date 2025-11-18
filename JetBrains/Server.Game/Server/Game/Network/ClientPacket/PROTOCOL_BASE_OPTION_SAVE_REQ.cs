// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_OPTION_SAVE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_OPTION_SAVE_REQ : GameClientPacket
{
  private byte[] byte_0;
  private string string_0;
  private string string_1;
  private string string_2;
  private string string_3;
  private string string_4;
  private int int_0;
  private int int_1;
  private int int_2;
  private int int_3;
  private int int_4;
  private int int_5;
  private int int_6;
  private int int_7;
  private int int_8;
  private int int_9;
  private int int_10;
  private int int_11;
  private int int_12;
  private int int_13;
  private int int_14;
  private int int_15;

  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_LOGOUT_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_BASE_OPTION_SAVE_REQ()
  {
  }

  public virtual void Read()
  {
  }

  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_OPTION_SAVE_ACK());
      this.Client.Close(1000, true, false);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      this.Client.Close(0, true, false);
    }
  }

  public PROTOCOL_BASE_OPTION_SAVE_REQ()
  {
  }
}
