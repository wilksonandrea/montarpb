// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.SChannelModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class SChannelModel
{
  [CompilerGenerated]
  [SpecialName]
  public void set_Votes(List<int> value) => ((VoteKickModel) this).list_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public bool[] get_TotalArray() => ((VoteKickModel) this).bool_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_TotalArray(bool[] value) => ((VoteKickModel) this).bool_0 = value;

  public SChannelModel(int value, [In] int obj1)
  {
    ((VoteKickModel) this).Accept = 1;
    ((VoteKickModel) this).Denie = 1;
    ((VoteKickModel) this).CreatorIdx = value;
    ((VoteKickModel) this).VictimIdx = obj1;
    this.set_Votes(new List<int>());
    ((VoteKickModel) this).Votes.Add(value);
    ((VoteKickModel) this).Votes.Add(obj1);
    this.set_TotalArray(new bool[18]);
  }

  public int GetInGamePlayers()
  {
    int inGamePlayers = 0;
    for (int index = 0; index < 18; ++index)
    {
      if (this.get_TotalArray()[index])
        ++inGamePlayers;
    }
    return inGamePlayers;
  }

  public int Id { get; set; }

  public int LastPlayers { get; set; }

  public int MaxPlayers { get; set; }

  public int ChannelPlayers { get; set; }

  public bool State { get; [param: In] set; }

  public SChannelType Type { get; set; }

  public string Host { get; set; }

  public ushort Port
  {
    [CompilerGenerated, SpecialName] get => ((SChannelModel) this).ushort_0;
    [CompilerGenerated, SpecialName] set => ((SChannelModel) this).ushort_0 = value;
  }

  public bool IsMobile
  {
    [CompilerGenerated, SpecialName] get => ((SChannelModel) this).bool_1;
    [CompilerGenerated, SpecialName] set => ((SChannelModel) this).bool_1 = value;
  }
}
