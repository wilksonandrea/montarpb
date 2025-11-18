// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_GMCHAT_SEND_CHAT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GMCHAT_SEND_CHAT_REQ : GameClientPacket
{
  private long long_0;
  private string string_0;
  private string string_1;
  private string string_2;

  public virtual void Run()
  {
    try
    {
      CLogger.Print($"{this.GetType().Name}; Unk1: {((PROTOCOL_BASE_UNKNOWN_3635_REQ) this).byte_0}; Nickname: {((PROTOCOL_BASE_UNKNOWN_3635_REQ) this).string_0}; Unk2: {((PROTOCOL_BASE_UNKNOWN_3635_REQ) this).byte_1}; Unk3: {((PROTOCOL_BASE_UNKNOWN_3635_REQ) this).short_0}; Unk4: {((PROTOCOL_BASE_UNKNOWN_3635_REQ) this).byte_2}", LoggerType.Warning, (Exception) null);
    }
    catch (Exception ex)
    {
      CLogger.Print($"{this.GetType().Name} Error: {ex.Message}", LoggerType.Error, (Exception) null);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ) this).int_0 = this.ReadD();
    ((PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ) this).byte_0 = this.ReadC();
    ((PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ) this).long_0 = this.ReadQ();
  }
}
