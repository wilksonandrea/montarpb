// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerCharacters
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerCharacters
{
  [CompilerGenerated]
  [SpecialName]
  public int get_DailyPoints() => ((PlayerBattlepass) this).int_3;

  [CompilerGenerated]
  [SpecialName]
  public void set_DailyPoints(int cafeEnum_1) => ((PlayerBattlepass) this).int_3 = cafeEnum_1;

  [CompilerGenerated]
  [SpecialName]
  public uint get_LastRecord() => ((PlayerBattlepass) this).uint_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_LastRecord(uint value) => ((PlayerBattlepass) this).uint_0 = value;

  public PlayerCharacters()
  {
  }

  public List<CharacterModel> Characters { get; set; }

  public PlayerCharacters() => this.Characters = new List<CharacterModel>();

  public int GetCharacterIdx(int value)
  {
    lock (this.Characters)
    {
      for (int index = 0; index < this.Characters.Count; ++index)
      {
        if (this.Characters[index].Slot == value)
          return index;
      }
    }
    return -1;
  }

  public CharacterModel GetCharacter(int value)
  {
    lock (this.Characters)
    {
      foreach (CharacterModel character in this.Characters)
      {
        if (character.Id == value)
          return character;
      }
    }
    return (CharacterModel) null;
  }
}
