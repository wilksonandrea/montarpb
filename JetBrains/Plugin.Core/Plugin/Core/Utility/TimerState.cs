// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Utility.TimerState
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Threading;

#nullable disable
namespace Plugin.Core.Utility;

public class TimerState
{
  protected Timer Timer;
  protected DateTime EndDate;
  protected object Sync = new object();

  public void Clear()
  {
    lock (((SafeList<T0>) this).object_0)
      ((SafeList<T0>) this).list_0.Clear();
  }

  public bool Contains(T0 OBJ)
  {
    lock (((SafeList<T0>) this).object_0)
      return ((SafeList<T0>) this).list_0.Contains(OBJ);
  }

  public int Count()
  {
    lock (((SafeList<T0>) this).object_0)
      return ((SafeList<T0>) this).list_0.Count;
  }

  public bool Remove(T0 array)
  {
    lock (((SafeList<T0>) this).object_0)
      return ((SafeList<T0>) this).list_0.Remove(array);
  }

  public TimerState()
  {
    ((StateObject) this).TcpBuffer = new byte[8912];
    ((StateObject) this).UdpBuffer = new byte[8096];
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public TimerState()
  {
  }
}
