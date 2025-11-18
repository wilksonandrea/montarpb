// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.Event.ActionStateInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Server.Match.Data.Enums;

#nullable disable
namespace Server.Match.Data.Models.Event;

public class ActionStateInfo
{
  public ActionFlag Action;
  public byte Value;
  public WeaponSyncType Flag;
}
