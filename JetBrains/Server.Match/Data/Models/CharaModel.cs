// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.CharaModel
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.SharpDX;
using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Match.Data.Models;

public class CharaModel
{
  [CompilerGenerated]
  [SpecialName]
  public Half3 get_Position() => ((BombPosition) this).half3_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Position(Half3 value) => ((BombPosition) this).half3_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public bool get_EveryWhere() => ((BombPosition) this).bool_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_EveryWhere(bool value) => ((BombPosition) this).bool_0 = value;

  public int Id
  {
    [CompilerGenerated, SpecialName] get => ((CharaModel) this).int_0;
    [CompilerGenerated, SpecialName] set => ((CharaModel) this).int_0 = value;
  }

  public int HP
  {
    [CompilerGenerated, SpecialName] get => ((CharaModel) this).int_1;
    [CompilerGenerated, SpecialName] set => ((CharaModel) this).int_1 = value;
  }
}
