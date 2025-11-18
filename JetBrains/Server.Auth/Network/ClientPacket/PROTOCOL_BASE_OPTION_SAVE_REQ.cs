// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_BASE_OPTION_SAVE_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_OPTION_SAVE_REQ : AuthClientPacket
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
      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_MAP_MATCHINGLIST_ACK(0));
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
      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_MAP_RULELIST_ACK());
      this.Client.Close(5000, true);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_LOGOUT_REQ: " + ex.Message, LoggerType.Error, ex);
      this.Client.Close(0, true);
    }
  }

  public PROTOCOL_BASE_OPTION_SAVE_REQ()
  {
  }
}
