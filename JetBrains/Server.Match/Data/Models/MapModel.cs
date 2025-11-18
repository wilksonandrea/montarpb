// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.MapModel
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Match.Data.Models;

public class MapModel
{
  [CompilerGenerated]
  [SpecialName]
  public int get_Id() => ((DeffectModel) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Id(int value) => ((DeffectModel) this).int_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_Life() => ((DeffectModel) this).int_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_Life(int value) => ((DeffectModel) this).int_1 = value;

  public int Id { get; set; }

  public List<ObjectModel> Objects
  {
    get => this.list_0;
    [CompilerGenerated, SpecialName] set => ((MapModel) this).list_0 = value;
  }

  public List<BombPosition> Bombs
  {
    [CompilerGenerated, SpecialName] get => ((MapModel) this).list_1;
    [CompilerGenerated, SpecialName] set => ((MapModel) this).list_1 = value;
  }
}
