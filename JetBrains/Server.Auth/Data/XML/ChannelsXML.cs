// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.XML.ChannelsXML
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;
using Server.Auth.Network;
using Server.Auth.Network.ClientPacket;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Server.Auth.Data.XML;

public static class ChannelsXML
{
  public static List<ChannelModel> Channels;

  public virtual void Run()
  {
    try
    {
      ((AuthClientPacket) this).Client.SendPacket((AuthServerPacket) new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(0));
      ((AuthClientPacket) this).Client.Close(0, false);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public ChannelsXML()
    : this()
  {
  }

  public virtual void Read()
  {
    ((PROTOCOL_MATCH_SERVER_IDX_REQ) this).short_0 = ((BaseClientPacket) this).ReadH();
    int num = (int) ((BaseClientPacket) this).ReadC();
  }

  public virtual void Run()
  {
    try
    {
      ((AuthClientPacket) this).Client.SendPacket((AuthServerPacket) new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(((PROTOCOL_MATCH_SERVER_IDX_REQ) this).short_0));
      ((AuthClientPacket) this).Client.Close(0, false);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_MATCH_SERVER_IDX_REQ: " + ex.Message, LoggerType.Warning, (Exception) null);
    }
  }

  public ChannelsXML()
    : this()
  {
  }

  public static void Load()
  {
    string str = "Data/Server/Channels.xml";
    if (File.Exists(str))
      AllUtils.smethod_0(str);
    else
      CLogger.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
  }

  public static void Reload()
  {
    ChannelsXML.Channels.Clear();
    ChannelsXML.Load();
  }
}
