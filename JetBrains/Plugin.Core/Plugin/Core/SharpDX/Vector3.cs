// Decompiled with JetBrains decompiler
// Type: Plugin.Core.SharpDX.Vector3
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.SharpDX;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct Vector3 : IEquatable<Vector3>, IFormattable
{
  public static readonly Vector3 Zero;
  public static readonly Vector3 UnitX;
  public static readonly Vector3 UnitY;
  public static readonly Vector3 UnitZ;
  public static readonly Vector3 One;
  public static readonly Vector3 Up;
  public static readonly Vector3 Down;
  public static readonly Vector3 Left;
  public static readonly Vector3 Right;
  public static readonly Vector3 ForwardRH;
  public static readonly Vector3 ForwardLH;
  public static readonly Vector3 BackwardRH;
  public static readonly Vector3 BackwardLH;
  public float X;
  public float Y;
  public float Z;

  public Vector3() => ((object) ref this).\u002Ector();

  public static bool NearEqual([In] float obj0, float value2)
  {
    if (Vector3.IsZero(obj0 - value2))
      return true;
    byte[] bytes1 = BitConverter.GetBytes(obj0);
    byte[] bytes2 = BitConverter.GetBytes(value2);
    int int32_1 = BitConverter.ToInt32(bytes1, 0);
    int int32_2 = BitConverter.ToInt32(bytes2, 0);
    return int32_1 < 0 == int32_2 < 0 && Math.Abs(int32_1 - int32_2) <= 4;
  }

  public static bool IsZero(float obj) => (double) Math.Abs(obj) < 9.9999999747524271E-07;

  public static bool IsOne(float float_0) => Vector3.IsZero(float_0 - 1f);

  public Vector3(float ushort_1, float B, [In] float obj2)
  {
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    (^(RawVector3&) ref this).X = ushort_1;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    (^(RawVector3&) ref this).Y = B;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    (^(RawVector3&) ref this).Z = obj2;
  }

  public Vector3(float A, [In] float obj1, [In] float obj2)
  {
    this.X = A;
    this.Y = obj1;
    this.Z = obj2;
  }

  public static float Distance([In] Vector3 obj0, Vector3 float_1)
  {
    double num1 = (double) obj0.X - (double) float_1.X;
    float num2 = obj0.Y - float_1.Y;
    float num3 = obj0.Z - float_1.Z;
    return (float) Math.Sqrt(num1 * num1 + (double) num2 * (double) num2 + (double) num3 * (double) num3);
  }

  public Vector3(float[] float_0)
  {
    if (float_0 == null)
      throw new ArgumentNullException("values");
    this.X = float_0.Length == 3 ? float_0[0] : throw new ArgumentOutOfRangeException("values", "There must be three and only three input values for Vector3.");
    this.Y = float_0[1];
    this.Z = float_0[2];
  }

  public bool IsNormalized
  {
    get
    {
      return Vector3.IsOne((float) ((double) this.X * (double) this.X + (double) this.Y * (double) this.Y + (double) this.Z * (double) this.Z));
    }
  }

  public bool IsZero => (double) this.X == 0.0 && (double) this.Y == 0.0 && (double) this.Z == 0.0;

  public float this[[In] int obj0]
  {
    get
    {
      switch (obj0)
      {
        case 0:
          return this.X;
        case 1:
          return this.Y;
        case 2:
          return this.Z;
        default:
          throw new ArgumentOutOfRangeException("index", "Indices for Vector3 run from 0 to 2, inclusive.");
      }
    }
    [param: In] set
    {
      switch (obj0)
      {
        case 0:
          this.X = value;
          break;
        case 1:
          this.Y = value;
          break;
        case 2:
          this.Z = value;
          break;
        default:
          throw new ArgumentOutOfRangeException("index", "Indices for Vector3 run from 0 to 2, inclusive.");
      }
    }
  }

  public static Vector3 operator +([In] Vector3 obj0, Vector3 value2)
  {
    return new Vector3(obj0.X + value2.X, obj0.Y + value2.Y, obj0.Z + value2.Z);
  }

  public static Vector3 operator *(Vector3 index, [In] Vector3 obj1)
  {
    return new Vector3(index.X * obj1.X, index.Y * obj1.Y, index.Z * obj1.Z);
  }

  public static Vector3 operator +([In] Vector3 obj0) => obj0;

  public static Vector3 operator -(Vector3 left, Vector3 right)
  {
    return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
  }

  public static Vector3 operator -(Vector3 left) => new Vector3(-left.X, -left.Y, -left.Z);

  public static Vector3 operator *([In] float obj0, Vector3 right)
  {
    return new Vector3(right.X * obj0, right.Y * obj0, right.Z * obj0);
  }

  public static Vector3 operator *(Vector3 left, float right)
  {
    return new Vector3(left.X * right, left.Y * right, left.Z * right);
  }

  public static Vector3 operator /(Vector3 value, [In] float obj1)
  {
    return new Vector3(value.X / obj1, value.Y / obj1, value.Z / obj1);
  }

  public static Vector3 operator /([In] float obj0, Vector3 value)
  {
    return new Vector3(obj0 / value.X, obj0 / value.Y, obj0 / value.Z);
  }

  public static Vector3 operator /([In] Vector3 obj0, Vector3 scale)
  {
    return new Vector3(obj0.X / scale.X, obj0.Y / scale.Y, obj0.Z / scale.Z);
  }

  public static Vector3 operator +([In] Vector3 obj0, float scale)
  {
    return new Vector3(obj0.X + scale, obj0.Y + scale, obj0.Z + scale);
  }

  public static Vector3 operator +([In] float obj0, Vector3 value)
  {
    return new Vector3(obj0 + value.X, obj0 + value.Y, obj0 + value.Z);
  }

  public static Vector3 operator -([In] Vector3 obj0, float scale)
  {
    return new Vector3(obj0.X - scale, obj0.Y - scale, obj0.Z - scale);
  }

  public static Vector3 operator -([In] float obj0, Vector3 scalar)
  {
    return new Vector3(obj0 - scalar.X, obj0 - scalar.Y, obj0 - scalar.Z);
  }

  public override int GetHashCode()
  {
    return (this.X.GetHashCode() * 397 ^ this.Y.GetHashCode()) * 397 ^ this.Z.GetHashCode();
  }

  public override string ToString()
  {
    return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}", (object) this.X, (object) this.Y, (object) this.Z);
  }

  public string ToString([In] string obj0)
  {
    return obj0 == null ? this.ToString() : string.Format((IFormatProvider) CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}", (object) this.X.ToString(obj0, (IFormatProvider) CultureInfo.CurrentCulture), (object) this.Y.ToString(obj0, (IFormatProvider) CultureInfo.CurrentCulture), (object) this.Z.ToString(obj0, (IFormatProvider) CultureInfo.CurrentCulture));
  }

  public string ToString(IFormatProvider value)
  {
    return string.Format(value, "X:{0} Y:{1} Z:{2}", (object) this.X, (object) this.Y, (object) this.Z);
  }

  public string ToString([In] string obj0, IFormatProvider scalar)
  {
    return obj0 == null ? this.ToString(scalar) : string.Format(scalar, "X:{0} Y:{1} Z:{2}", (object) this.X.ToString(obj0, scalar), (object) this.Y.ToString(obj0, scalar), (object) this.Z.ToString(obj0, scalar));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals([In] ref Vector3 obj0)
  {
    return Vector3.NearEqual(obj0.X, this.X) && Vector3.NearEqual(obj0.Y, this.Y) && Vector3.NearEqual(obj0.Z, this.Z);
  }
}
