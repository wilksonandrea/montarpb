// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.SubHead.ObjectMoveInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

#nullable disable
namespace Server.Match.Data.Models.SubHead;

public class ObjectMoveInfo
{
  public byte[] Unk;

  public PlayerModel GetPlayer(int Client, bool Packet)
  {
    PlayerModel playerModel;
    try
    {
      playerModel = ((RoomModel) this).Players[Client];
    }
    catch
    {
      playerModel = (PlayerModel) null;
    }
    return playerModel != null && (!Packet || playerModel.Client != null) ? playerModel : (PlayerModel) null;
  }
}
