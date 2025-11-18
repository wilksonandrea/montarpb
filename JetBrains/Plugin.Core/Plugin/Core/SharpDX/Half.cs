// Decompiled with JetBrains decompiler
// Type: Plugin.Core.SharpDX.Half
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Utility;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Plugin.Core.SharpDX;

public struct Half
{
  private ushort ushort_0;
  public const int PrecisionDigits = 3;
  public const int MantissaBits = 11;
  public const int MaximumDecimalExponent = 4;
  public const int MaximumBinaryExponent = 15;
  public const int MinimumDecimalExponent = -4;
  public const int MinimumBinaryExponent = -14;
  public const int ExponentRadix = 2;
  public const int AdditionRounding = 1;
  public static readonly float Epsilon;
  public static readonly float MaxValue;
  public static readonly float MinValue;

  public bool IsTimer() => (^(TimerState&) ref this).Timer != null;

  public void StartJob([In] int obj0, TimerCallback action)
  {
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    lock ((^(TimerState&) ref this).Sync)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      (^(TimerState&) ref this).Timer = new Timer(action, (object) ref this, obj0, -1);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      (^(TimerState&) ref this).EndDate = DBQuery.Now().AddMilliseconds((double) obj0);
    }
  }

  public void StopJob()
  {
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    lock ((^(TimerState&) ref this).Sync)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if ((^(TimerState&) ref this).Timer == null)
        return;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      (^(TimerState&) ref this).Timer.Dispose();
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      (^(TimerState&) ref this).Timer = (Timer) null;
    }
  }

  public void StartTimer(TimeSpan Value, [In] TimerCallback obj1)
  {
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    lock ((^(TimerState&) ref this).Sync)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      (^(TimerState&) ref this).Timer = new Timer(obj1, (object) ref this, Value, TimeSpan.Zero);
    }
  }

  public int GetTimeLeft()
  {
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    if ((^(TimerState&) ref this).Timer == null)
      return 0;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    int duration = (int) ComDiv.GetDuration((^(TimerState&) ref this).EndDate);
    return duration >= 0 ? duration : 0;
  }

  public Half(ushort Value) => this.ushort_0 = Value;

  public Half(float Period) => this.ushort_0 = MathUtil.smethod_0(Period);

  public ushort RawValue
  {
    get => this.ushort_0;
    [param: In] set => this.ushort_0 = value;
  }

  public static float[] ConvertToFloat(Half[] Period)
  {
    float[] numArray = new float[Period.Length];
    for (int index = 0; index < numArray.Length; ++index)
      numArray[index] = RawVector3.smethod_1(Period[index].ushort_0);
    return numArray;
  }

  public static Half[] ConvertToHalf([In] float[] obj0)
  {
    Half[] half = new Half[obj0.Length];
    for (int index = 0; index < half.Length; ++index)
      half[index] = new Half(obj0[index]);
    return half;
  }

  public static implicit operator Half(float ushort_1) => new Half(ushort_1);

  public static implicit operator float(Half float_0) => RawVector3.smethod_1(float_0.ushort_0);

  public static bool operator ==(Half value, [In] Half obj1)
  {
    return (int) value.ushort_0 == (int) obj1.ushort_0;
  }

  public static bool operator !=(Half values, [In] Half obj1)
  {
    return (int) values.ushort_0 != (int) obj1.ushort_0;
  }

  public override string ToString()
  {
    return ((float) this).ToString((IFormatProvider) CultureInfo.CurrentCulture);
  }
}
