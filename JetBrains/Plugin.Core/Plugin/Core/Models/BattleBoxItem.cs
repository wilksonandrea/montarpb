// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.BattleBoxItem
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Network;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Plugin.Core.Models;

public class BattleBoxItem
{
  protected internal void WriteQ(ulong Value) => ((BaseServerPacket) this).BWriter.Write(Value);

  protected internal void WriteQ(long Value) => ((BaseServerPacket) this).BWriter.Write(Value);

  protected internal void WriteN(string Value, [In] int obj1, [In] string obj2)
  {
    if (Value == null)
      return;
    ((BaseServerPacket) this).WriteB(Encoding.GetEncoding(obj2).GetBytes(Value));
    ((BaseServerPacket) this).WriteB(new byte[obj1 - Value.Length]);
  }

  protected internal void WriteS(string Value, [In] int obj1)
  {
    if (Value == null)
      return;
    ((BaseServerPacket) this).WriteB(Encoding.UTF8.GetBytes(Value));
    ((BaseServerPacket) this).WriteB(new byte[obj1 - Value.Length]);
  }

  protected internal void WriteU(string Text, int Count)
  {
    if (Text == null)
      return;
    ((BaseServerPacket) this).WriteB(Encoding.Unicode.GetBytes(Text));
    ((BaseServerPacket) this).WriteB(new byte[Count - Text.Length * 2]);
  }

  public int GoodsId
  {
    [CompilerGenerated, SpecialName] get => ((BattleBoxItem) this).int_0;
    [CompilerGenerated, SpecialName] [param: In] set => ((BattleBoxItem) this).int_0 = value;
  }

  public int Percent
  {
    [CompilerGenerated, SpecialName] get => ((BattleBoxItem) this).int_1;
    [CompilerGenerated, SpecialName] set => ((BattleBoxItem) this).int_1 = value;
  }
}
