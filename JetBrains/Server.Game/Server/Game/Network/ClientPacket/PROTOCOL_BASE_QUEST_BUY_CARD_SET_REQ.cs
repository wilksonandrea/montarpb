// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ : GameClientPacket
{
  private int int_0;
  private EventErrorEnum eventErrorEnum_0;

  private void method_1([In] DBQuery obj0, [In] PlayerConfig obj1)
  {
    if (obj1.Macro1 != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_0)
    {
      obj1.Macro1 = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_0;
      obj0.AddQuery("macro1", (object) obj1.Macro1);
    }
    if (obj1.Macro2 != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_1)
    {
      obj1.Macro2 = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_1;
      obj0.AddQuery("macro2", (object) obj1.Macro2);
    }
    if (obj1.Macro3 != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_2)
    {
      obj1.Macro3 = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_2;
      obj0.AddQuery("macro3", (object) obj1.Macro3);
    }
    if (obj1.Macro4 != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_3)
    {
      obj1.Macro4 = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_3;
      obj0.AddQuery("macro4", (object) obj1.Macro4);
    }
    if (!(obj1.Macro5 != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_4))
      return;
    obj1.Macro5 = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_4;
    obj0.AddQuery("macro5", (object) obj1.Macro5);
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_1 = (int) this.ReadC();
    ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_0 = (int) this.ReadC();
    ((PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ) this).int_2 = (int) this.ReadUH();
  }
}
