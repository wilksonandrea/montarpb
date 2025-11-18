// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Settings.ConfigEngine
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Filters;
using Plugin.Core.RAW;
using Plugin.Core.SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Plugin.Core.Settings;

public class ConfigEngine
{
  private readonly FileInfo fileInfo_0;
  private readonly FileAccess fileAccess_0;
  private readonly string string_0 = Assembly.GetExecutingAssembly().GetName().Name;

  static ConfigEngine()
  {
    Vector3.Zero = new Vector3();
    Vector3.UnitX = new Vector3(1f, 0.0f, 0.0f);
    Vector3.UnitY = new Vector3(0.0f, 1f, 0.0f);
    Vector3.UnitZ = new Vector3(0.0f, 0.0f, 1f);
    Vector3.One = new Vector3(1f, 1f, 1f);
    Vector3.Up = new Vector3(0.0f, 1f, 0.0f);
    Vector3.Down = new Vector3(0.0f, -1f, 0.0f);
    Vector3.Left = new Vector3(-1f, 0.0f, 0.0f);
    Vector3.Right = new Vector3(1f, 0.0f, 0.0f);
    Vector3.ForwardRH = new Vector3(0.0f, 0.0f, -1f);
    Vector3.ForwardLH = new Vector3(0.0f, 0.0f, 1f);
    Vector3.BackwardRH = new Vector3(0.0f, 0.0f, 1f);
    Vector3.BackwardLH = new Vector3(0.0f, 0.0f, -1f);
  }

  public static void Load()
  {
    string str = "Config/Filters/Nicks.txt";
    if (File.Exists(str))
    {
      ConfigEngine.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {NickFilter.Filters.Count} Nick Filters", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    NickFilter.Filters.Clear();
    ConfigEngine.Load();
  }

  private static void smethod_0(string other)
  {
    try
    {
      using (StreamReader streamReader = new StreamReader(other))
      {
        string str;
        while ((str = streamReader.ReadLine()) != null)
          NickFilter.Filters.Add(str);
        streamReader.Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Filter: " + ex.Message, LoggerType.Error, ex);
    }
  }

  static ConfigEngine() => NickFilter.Filters = new List<string>();

  [DllImport("kernel32", CharSet = CharSet.Unicode)]
  private static extern long WritePrivateProfileString(
    string other,
    [In] string obj1,
    [In] string obj2,
    [In] string obj3);

  [DllImport("kernel32", CharSet = CharSet.Unicode)]
  private static extern int GetPrivateProfileString(
    string string_0,
    string string_2,
    string string_3,
    StringBuilder string_4,
    [In] int obj4,
    [In] string obj5);

  public ConfigEngine([In] string obj0, FileAccess string_2)
  {
    this.fileAccess_0 = string_2;
    this.fileInfo_0 = new FileInfo(obj0 ?? this.string_0);
  }

  public byte ReadC([In] string obj0, [In] byte obj1, [In] string obj2)
  {
    try
    {
      return byte.Parse(this.method_0(obj0, obj2));
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Read Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
      return obj1;
    }
  }

  public short ReadH(string string_1 = null, short fileAccess_1 = 3, [In] string obj2)
  {
    try
    {
      return short.Parse(this.method_0(string_1, obj2));
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Read Parameter Failure: " + string_1, LoggerType.Warning, (Exception) null);
      return fileAccess_1;
    }
  }

  public ushort ReadUH([In] string obj0, ushort Defaultprop, string Section = null)
  {
    try
    {
      return ushort.Parse(this.method_0(obj0, Section));
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Read Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
      return Defaultprop;
    }
  }

  public int ReadD([In] string obj0, int Defaultprop, string Section = null)
  {
    try
    {
      return int.Parse(this.method_0(obj0, Section));
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Read Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
      return Defaultprop;
    }
  }

  public uint ReadUD([In] string obj0, uint Defaultprop, string Section = null)
  {
    try
    {
      return uint.Parse(this.method_0(obj0, Section));
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Read Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
      return Defaultprop;
    }
  }

  public long ReadQ([In] string obj0, long Defaultprop, string Section = null)
  {
    try
    {
      return long.Parse(this.method_0(obj0, Section));
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Read Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
      return Defaultprop;
    }
  }

  public ulong ReadUQ([In] string obj0, ulong Defaultprop, string Section = null)
  {
    try
    {
      return ulong.Parse(this.method_0(obj0, Section));
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Read Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
      return Defaultprop;
    }
  }

  public double ReadF([In] string obj0, double Defaultprop, string Section = null)
  {
    try
    {
      return double.Parse(this.method_0(obj0, Section));
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Read Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
      return Defaultprop;
    }
  }

  public float ReadT([In] string obj0, float Defaultprop, string Section = null)
  {
    try
    {
      return float.Parse(this.method_0(obj0, Section));
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Read Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
      return Defaultprop;
    }
  }

  public bool ReadX([In] string obj0, bool Defaultprop, string Section = null)
  {
    try
    {
      return bool.Parse(this.method_0(obj0, Section));
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Read Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
      return Defaultprop;
    }
  }

  public string ReadS([In] string obj0, string Defaultprop, string Section = null)
  {
    try
    {
      return this.method_0(obj0, Section);
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Read Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
      return Defaultprop;
    }
  }

  private string method_0([In] string obj0, string Defaultprop)
  {
    StringBuilder string_4 = new StringBuilder(65025);
    if (this.fileAccess_0 == FileAccess.Write)
      throw new Exception("Can`t read the file! No access!");
    ConfigEngine.GetPrivateProfileString(Defaultprop ?? this.string_0, obj0, "", string_4, 65025, this.fileInfo_0.FullName);
    return string_4.ToString();
  }

  public void WriteC(string Key, byte Defaultprop, string Section = null)
  {
    try
    {
      ((MissionCardRAW) this).method_1(Key, Defaultprop.ToString(), Section);
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Write Parameter Failure: " + Key, LoggerType.Warning, (Exception) null);
    }
  }

  public void WriteH(string string_1, short string_2 = default (short), [In] string obj2)
  {
    try
    {
      ((MissionCardRAW) this).method_1(string_1, string_2.ToString(), obj2);
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Write Parameter Failure: " + string_1, LoggerType.Warning, (Exception) null);
    }
  }

  public void WriteH([In] string obj0, ushort Value, string Section = null)
  {
    try
    {
      ((MissionCardRAW) this).method_1(obj0, Value.ToString(), Section);
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Write Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
    }
  }

  public void WriteD([In] string obj0, int Value, string Section = null)
  {
    try
    {
      ((MissionCardRAW) this).method_1(obj0, Value.ToString(), Section);
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Write Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
    }
  }

  public void WriteD([In] string obj0, uint Value, string Section = null)
  {
    try
    {
      ((MissionCardRAW) this).method_1(obj0, Value.ToString(), Section);
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Write Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
    }
  }

  public void WriteQ([In] string obj0, long Value, string Section = null)
  {
    try
    {
      ((MissionCardRAW) this).method_1(obj0, Value.ToString(), Section);
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Write Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
    }
  }

  public void WriteQ([In] string obj0, ulong Value, string Section = null)
  {
    try
    {
      ((MissionCardRAW) this).method_1(obj0, Value.ToString(), Section);
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Write Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
    }
  }

  public void WriteF([In] string obj0, double Value, string Section = null)
  {
    try
    {
      ((MissionCardRAW) this).method_1(obj0, Value.ToString(), Section);
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Write Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
    }
  }

  public void WriteT([In] string obj0, float Value, string Section = null)
  {
    try
    {
      ((MissionCardRAW) this).method_1(obj0, Value.ToString(), Section);
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Write Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
    }
  }

  public void WriteX([In] string obj0, bool Value, string Section = null)
  {
    try
    {
      ((MissionCardRAW) this).method_1(obj0, Value.ToString(), Section);
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Write Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
    }
  }
}
