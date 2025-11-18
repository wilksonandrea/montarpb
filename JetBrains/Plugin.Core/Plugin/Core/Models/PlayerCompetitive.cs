// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerCompetitive
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerCompetitive
{
  public int GenSlotId(int Slot)
  {
    int num = -1;
    switch (Slot)
    {
      case 601001:
        num = 0;
        break;
      case 602002:
        num = 1;
        break;
      default:
        for (int index = 2; index < ((PlayerCharacters) this).Characters.Count + 1; ++index)
        {
          bool flag = false;
          foreach (CharacterModel character in ((PlayerCharacters) this).Characters)
          {
            if (character.Slot == index)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            num = index;
            break;
          }
        }
        if (num == -1)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print($"No available slot for character ID: {Slot}", LoggerType.Warning, (Exception) null);
          return -1;
        }
        break;
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if ((Slot != 601001 || !((PlayerCharacters) this).Characters.Any<CharacterModel>(PlayerCharacters.Class10.\u003C\u003E9__11_0 ?? (PlayerCharacters.Class10.\u003C\u003E9__11_0 = new Func<CharacterModel, bool>(((PlayerCompetitive) PlayerCharacters.Class10.\u003C\u003E9).method_0)))) && (Slot != 602002 || !((PlayerCharacters) this).Characters.Any<CharacterModel>(PlayerCharacters.Class10.\u003C\u003E9__11_1 ?? (PlayerCharacters.Class10.\u003C\u003E9__11_1 = new Func<CharacterModel, bool>(((PlayerCompetitive) PlayerCharacters.Class10.\u003C\u003E9).method_1)))))
      return num;
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Desired slot for character ID: {Slot} is already taken.", LoggerType.Warning, (Exception) null);
    return -1;
  }

  static PlayerCompetitive()
  {
    // ISSUE: reference to a compiler-generated field
    PlayerCharacters.Class10.\u003C\u003E9 = (PlayerCharacters.Class10) new PlayerCompetitive();
  }

  internal bool method_0(CharacterModel Slot) => Slot.Slot == 0;

  internal bool method_1([In] CharacterModel obj0) => obj0.Slot == 1;

  public long OwnerId { get; set; }

  public int Level
  {
    get => this.int_0;
    [CompilerGenerated, SpecialName] set => ((PlayerCompetitive) this).int_0 = value;
  }

  public int Points
  {
    [CompilerGenerated, SpecialName] get => ((PlayerCompetitive) this).int_1;
    [CompilerGenerated, SpecialName] set => ((PlayerCompetitive) this).int_1 = value;
  }
}
