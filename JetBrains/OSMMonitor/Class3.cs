// Decompiled with JetBrains decompiler
// Type: Class3
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using System.Diagnostics;

#nullable disable
internal class Class3
{
  public Class3()
  {
  }

  public Class3(bool bool_3 = false, bool bool_4 = true, ProcessWindowStyle processWindowStyle_1 = ProcessWindowStyle.Hidden, bool bool_5 = true)
    : this()
  {
    this.Boolean_0 = bool_3;
    this.Boolean_1 = bool_4;
    this.ProcessWindowStyle_0 = processWindowStyle_1;
    this.Boolean_2 = bool_5;
  }

  public static Class3 Class3_0 { get; } = new Class3();

  public bool Boolean_0 { get; set; }

  public bool Boolean_1 { get; set; } = true;

  public ProcessWindowStyle ProcessWindowStyle_0 { get; set; } = ProcessWindowStyle.Hidden;

  public bool Boolean_2 { get; set; } = true;

  public string String_0 { get; set; }
}
