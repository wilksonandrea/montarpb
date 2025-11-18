// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_CHECK_MARK_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CHECK_MARK_REQ : GameClientPacket
{
  private uint uint_0;
  private uint uint_1;

  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_CHECK_MARK_ACK(!CommandManager.IsClanNameExist(((PROTOCOL_CS_CHECK_DUPLICATE_REQ) this).string_0) ? 0U : 2147483648U /*0x80000000*/));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ) this).int_0 = this.ReadD();
}
