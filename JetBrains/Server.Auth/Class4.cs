// Decompiled with JetBrains decompiler
// Type: Class4
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.XML;

#nullable disable
internal class Class4
{
  private static bool smethod_0(int ServerId)
  {
    switch (ServerId)
    {
      case 0:
        EventVisitXML.Reload();
        return true;
      case 1:
        EventLoginXML.Reload();
        return true;
      case 2:
        EventBoostXML.Reload();
        return true;
      case 3:
        EventPlaytimeXML.Reload();
        return true;
      case 4:
        EventQuestXML.Reload();
        return true;
      case 5:
        EventRankUpXML.Reload();
        return true;
      case 6:
        EventXmasXML.Reload();
        return true;
      default:
        return false;
    }
  }
}
