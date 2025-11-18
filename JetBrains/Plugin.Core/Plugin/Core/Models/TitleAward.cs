// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.TitleAward
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class TitleAward
{
  [CompilerGenerated]
  [SpecialName]
  public int get_RemotePort() => ((Synchronize) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_RemotePort(int value) => ((Synchronize) this).int_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public IPEndPoint get_Connection() => ((Synchronize) this).ipendPoint_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Connection(IPEndPoint value) => ((Synchronize) this).ipendPoint_0 = value;

  public TitleAward(string value, [In] int obj1)
  {
    this.set_Connection(new IPEndPoint(IPAddress.Parse(value), obj1));
  }

  public int Id
  {
    [CompilerGenerated, SpecialName] get => ((TitleAward) this).int_0;
    [CompilerGenerated, SpecialName] set => ((TitleAward) this).int_0 = value;
  }

  public ItemsModel Item
  {
    [CompilerGenerated, SpecialName] get => ((TitleAward) this).itemsModel_0;
    [CompilerGenerated, SpecialName] set => ((TitleAward) this).itemsModel_0 = value;
  }
}
