// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ : GameClientPacket
{
  private long long_0;
  private string string_0;
  private uint uint_0;

  public virtual void Run()
  {
    try
    {
      int[] array = ((PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ) this).list_0.ToArray();
      // ISSUE: variable of a boxed type
      __Boxed<long> playerId = (ValueType) this.Client.PlayerId;
      string[] COLUMNS = new string[2]
      {
        "expire_date",
        "state"
      };
      object[] objArray = new object[2];
      DateTime dateTime = DateTimeUtil.Now();
      dateTime = dateTime.AddDays(7.0);
      objArray[0] = (object) long.Parse(dateTime.ToString("yyMMddHHmm"));
      objArray[1] = (object) 0;
      if (!ComDiv.UpdateDB("player_messages", "object_id", array, "owner_id", (object) playerId, COLUMNS, objArray))
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(((PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ) this).list_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ()
  {
    ((PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ) this).list_0 = new List<int>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    int num = (int) this.ReadC();
    for (int index = 0; index < num; ++index)
      ((PROTOCOL_MESSENGER_NOTE_DELETE_REQ) this).list_0.Add((object) (long) this.ReadUD());
  }

  public virtual void Run()
  {
    try
    {
      if (!DaoManagerSQL.DeleteMessages(((PROTOCOL_MESSENGER_NOTE_DELETE_REQ) this).list_0, this.Client.PlayerId))
        ((PROTOCOL_MESSENGER_NOTE_DELETE_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_MESSENGER_NOTE_SEND_ACK(((PROTOCOL_MESSENGER_NOTE_DELETE_REQ) this).uint_0, ((PROTOCOL_MESSENGER_NOTE_DELETE_REQ) this).list_0));
      ((PROTOCOL_MESSENGER_NOTE_DELETE_REQ) this).list_0 = (List<object>) null;
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_MESSENGER_NOTE_DELETE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
