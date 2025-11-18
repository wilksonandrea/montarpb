// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_MATCH_SERVER_IDX_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using Plugin.Core.Utility;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_MATCH_SERVER_IDX_REQ : AuthClientPacket
{
  private short short_0;

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
  }
}
