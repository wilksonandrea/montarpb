// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.ObjectModel
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Match.Data.Models;

public class ObjectModel
{
  [CompilerGenerated]
  [SpecialName]
  public DateTime get_UseDate() => ((ObjectInfo) this).dateTime_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_UseDate(DateTime int_9) => ((ObjectInfo) this).dateTime_0 = int_9;

  [CompilerGenerated]
  [SpecialName]
  public ObjectModel get_Model() => ((ObjectInfo) this).objectModel_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Model(ObjectModel value) => ((ObjectInfo) this).objectModel_0 = value;

  public ObjectModel(int value)
  {
    ((ObjectInfo) this).Id = value;
    ((ObjectInfo) this).Life = 100;
  }

  public int Id { get; set; }

  public int Life { get; set; }

  public int Animation { get; set; }

  public int UltraSync { get; set; }

  public int UpdateId { get; set; }

  public bool NeedSync { get; set; }

  public bool Destroyable { get; set; }

  public bool NoInstaSync { get; set; }

  public List<AnimModel> Animations { get; set; }

  public List<DeffectModel> Effects
  {
    get => this.list_1;
    [CompilerGenerated, SpecialName] set => ((ObjectModel) this).list_1 = value;
  }
}
