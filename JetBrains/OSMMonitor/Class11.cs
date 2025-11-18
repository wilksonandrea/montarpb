// Decompiled with JetBrains decompiler
// Type: Class11
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Class11
{
  private static ResourceManager resourceManager_0;
  private static CultureInfo cultureInfo_0;

  internal Class11()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager_0
  {
    get
    {
      if (Class11.resourceManager_0 == null)
        Class11.resourceManager_0 = new ResourceManager(nameof (Class11), typeof (Class11).Assembly);
      return Class11.resourceManager_0;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo CultureInfo_0
  {
    get => Class11.cultureInfo_0;
    set => Class11.cultureInfo_0 = value;
  }

  internal static Bitmap Bitmap_0
  {
    get => (Bitmap) Class11.ResourceManager_0.GetObject("PBLOGO 256", Class11.cultureInfo_0);
  }

  internal static Bitmap Bitmap_1
  {
    get => (Bitmap) Class11.ResourceManager_0.GetObject("Setting", Class11.cultureInfo_0);
  }
}
