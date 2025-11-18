// Decompiled with JetBrains decompiler
// Type: Class0`2
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
[CompilerGenerated]
internal sealed class Class0<T, U>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly T gparam_0;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly U gparam_1;

  public abstract void m000001();

  public abstract void m000002();

  public abstract void m000003();

  public abstract void m000004();

  public abstract void m000005();

  public T item => this.gparam_0;

  public U inx
  {
    [SpecialName] get => ((Class0<T0, T1>) this).gparam_1;
  }
}
