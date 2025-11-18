// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.SubHead.ObjectAnimInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

#nullable disable
namespace Server.Match.Data.Models.SubHead;

public class ObjectAnimInfo
{
  public float SyncDate;
  public byte Anim1;
  public byte Anim2;
  public ushort Life;
  public byte[] Unknown;

  public int GetPlayersCount()
  {
    int playersCount = 0;
    for (int index = 0; index < 18; ++index)
    {
      if (((RoomModel) this).Players[index].Client != null)
        ++playersCount;
    }
    return playersCount;
  }
}
