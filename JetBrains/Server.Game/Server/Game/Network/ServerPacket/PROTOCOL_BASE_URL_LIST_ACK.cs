// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_URL_LIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_URL_LIST_ACK : GameServerPacket
{
  private readonly ServerConfig serverConfig_0;

  public virtual void Write()
  {
    this.WriteH((short) 2409);
    this.WriteD(0);
  }

  public PROTOCOL_BASE_URL_LIST_ACK([In] uint obj0, ItemsModel account_1, [In] Account obj2)
  {
    ((PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK) this).list_0 = new List<ItemsModel>();
    ((PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK) this).list_1 = new List<ItemsModel>();
    ((PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK) this).list_2 = new List<ItemsModel>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
    ((PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK) this).uint_0 = obj0;
    ItemsModel Type = new ItemsModel(account_1);
    if (Type == null)
      return;
    ComDiv.TryCreateItem(Type, obj2.Inventory, obj2.PlayerId);
    UpdateServer.LoadItem(obj2, Type);
    if (Type.Category == ItemCategory.Weapon)
      ((PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK) this).list_1.Add(Type);
    else if (Type.Category == ItemCategory.Character)
    {
      ((PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK) this).list_0.Add(Type);
    }
    else
    {
      if (Type.Category != ItemCategory.Coupon)
        return;
      ((PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK) this).list_2.Add(Type);
    }
  }
}
