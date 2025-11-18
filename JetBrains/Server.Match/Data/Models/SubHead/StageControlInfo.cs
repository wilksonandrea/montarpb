// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.SubHead.StageControlInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using System.Net;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Data.Models.SubHead;

public class StageControlInfo
{
  public byte[] Unk;

  public PlayerModel GetPlayer([In] int obj0, [In] IPEndPoint obj1)
  {
    PlayerModel playerModel;
    try
    {
      playerModel = ((RoomModel) this).Players[obj0];
    }
    catch
    {
      playerModel = (PlayerModel) null;
    }
    return playerModel != null && playerModel.CompareIp(obj1) ? playerModel : (PlayerModel) null;
  }
}
