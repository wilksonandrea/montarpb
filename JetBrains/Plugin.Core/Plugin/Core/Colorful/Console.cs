// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.Console
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace Plugin.Core.Colorful;

public static class Console
{
  private static ColorStore colorStore_0;
  private static ColorManagerFactory colorManagerFactory_0;
  private static ColorManager colorManager_0;
  private static Dictionary<string, COLORREF> dictionary_0;
  private const int int_0 = 16 /*0x10*/;
  private const int int_1 = 1;
  private static readonly string string_0 = "\r\n";
  private static readonly string string_1 = "";
  private static readonly Color color_0 = Color.FromArgb(0, 0, 0);
  private static readonly Color color_1 = Color.FromArgb(0, 0, (int) byte.MaxValue);
  private static readonly Color color_2 = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue);
  private static readonly Color color_3 = Color.FromArgb(0, 0, 128 /*0x80*/);
  private static readonly Color color_4 = Color.FromArgb(0, 128 /*0x80*/, 128 /*0x80*/);
  private static readonly Color color_5 = Color.FromArgb(128 /*0x80*/, 128 /*0x80*/, 128 /*0x80*/);
  private static readonly Color color_6 = Color.FromArgb(0, 128 /*0x80*/, 0);
  private static readonly Color color_7 = Color.FromArgb(128 /*0x80*/, 0, 128 /*0x80*/);
  private static readonly Color color_8 = Color.FromArgb(128 /*0x80*/, 0, 0);
  private static readonly Color color_9 = Color.FromArgb(128 /*0x80*/, 128 /*0x80*/, 0);
  private static readonly Color color_10 = Color.FromArgb(192 /*0xC0*/, 192 /*0xC0*/, 192 /*0xC0*/);
  private static readonly Color color_11 = Color.FromArgb(0, (int) byte.MaxValue, 0);
  private static readonly Color color_12 = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
  private static readonly Color color_13 = Color.FromArgb((int) byte.MaxValue, 0, 0);
  private static readonly Color color_14 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
  private static readonly Color color_15 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0);

  protected abstract void TryIncrementColorIndex();

  public ColorAlternator GetAlternator(string[] ConfigId, [System.Runtime.InteropServices.In] Color[] obj1)
  {
    // ISSUE: object of a compiler-generated type is created
    return (ColorAlternator) new PatternBasedColorAlternator<string>((PatternCollection<string>) new TextPatternCollection.Class35(ConfigId), obj1);
  }

  public ColorAlternator GetAlternator(int value, [System.Runtime.InteropServices.In] Color[] obj1)
  {
    return (ColorAlternator) new GradientGenerator(value, obj1);
  }

  public static ConsoleColor ToNearestConsoleColor(this Color input)
  {
    ConsoleColor nearestConsoleColor1 = ConsoleColor.Black;
    double num1 = double.MaxValue;
    foreach (ConsoleColor nearestConsoleColor2 in Enum.GetValues(typeof (ConsoleColor)))
    {
      string name = Enum.GetName(typeof (ConsoleColor), (object) nearestConsoleColor2);
      Color color = Color.FromName(string.Equals(name, "DarkYellow", StringComparison.Ordinal) ? "Orange" : name);
      double num2 = Math.Pow((double) ((int) color.R - (int) input.R), 2.0) + Math.Pow((double) ((int) color.G - (int) input.G), 2.0) + Math.Pow((double) ((int) color.B - (int) input.B), 2.0);
      if (num2 == 0.0)
        return nearestConsoleColor2;
      if (num2 < num1)
      {
        num1 = num2;
        nearestConsoleColor1 = nearestConsoleColor2;
      }
    }
    return nearestConsoleColor1;
  }

  private static TaskQueue TaskQueue_0 { get; } = (TaskQueue) new TaskQueue.Struct5();

  private static void smethod_0(
    IEnumerable<KeyValuePair<string, Color>> patterns,
    params string colors)
  {
    // ISSUE: variable of a compiler-generated type
    Console.Class17 class17 = (Console.Class17) new ColorManager();
    // ISSUE: reference to a compiler-generated field
    class17.ienumerable_0 = patterns;
    // ISSUE: reference to a compiler-generated field
    class17.string_0 = colors;
    ((TextAnnotator) Console.TaskQueue_0).Enqueue(new Func<Task>(((ColorManager) class17).method_0)).Wait();
  }

  private static void smethod_1(StyledString frequency, params string colors)
  {
    ConsoleColor foregroundColor = System.Console.ForegroundColor;
    int length1 = frequency.CharacterGeometry.GetLength(0);
    int length2 = frequency.CharacterGeometry.GetLength(1);
    for (int index1 = 0; index1 < length1; ++index1)
    {
      for (int index2 = 0; index2 < length2; ++index2)
      {
        System.Console.ForegroundColor = ((ColorManagerFactory) Console.colorManager_0).GetConsoleColor(frequency.ColorGeometry[index1, index2]);
        if (index1 == length1 - 1 && index2 == length2 - 1)
          System.Console.Write(frequency.CharacterGeometry[index1, index2].ToString() + colors);
        else if (index2 == length2 - 1)
          System.Console.Write(frequency.CharacterGeometry[index1, index2].ToString() + "\r\n");
        else
          System.Console.Write(frequency.CharacterGeometry[index1, index2]);
      }
    }
    System.Console.ForegroundColor = foregroundColor;
  }

  private static void smethod_2<T>(Action<T> color, T string_2, [System.Runtime.InteropServices.In] Color obj2)
  {
    int foregroundColor = (int) System.Console.ForegroundColor;
    System.Console.ForegroundColor = ((ColorManagerFactory) Console.colorManager_0).GetConsoleColor(obj2);
    color(string_2);
    System.Console.ForegroundColor = (ConsoleColor) foregroundColor;
  }

  private static void smethod_3(
    Action<string> styledString_0,
    char[] string_2,
    int color_16,
    [System.Runtime.InteropServices.In] int obj3,
    [System.Runtime.InteropServices.In] Color obj4)
  {
    // ISSUE: reference to a compiler-generated method
    string string_2_1 = string_2.smethod_2<char[]>().Substring(color_16, obj3);
    Console.smethod_2<string>(styledString_0, string_2_1, obj4);
  }

  private static void smethod_4<T>(Action<T> action_0, T char_0, ColorAlternator int_2)
  {
    // ISSUE: reference to a compiler-generated method
    Color nextColor = ((ColorExtensions) int_2).GetNextColor(char_0.smethod_2<T>());
    int foregroundColor = (int) System.Console.ForegroundColor;
    System.Console.ForegroundColor = ((ColorManagerFactory) Console.colorManager_0).GetConsoleColor(nextColor);
    action_0(char_0);
    System.Console.ForegroundColor = (ConsoleColor) foregroundColor;
  }

  private static void smethod_5(
    [System.Runtime.InteropServices.In] Action<string> obj0,
    [System.Runtime.InteropServices.In] char[] obj1,
    [System.Runtime.InteropServices.In] int obj2,
    int int_3,
    ColorAlternator color_16)
  {
    // ISSUE: reference to a compiler-generated method
    string char_0 = obj1.smethod_2<char[]>().Substring(obj2, int_3);
    Console.smethod_4<string>(obj0, char_0, color_16);
  }

  private static void smethod_6<T>(string action_0, T char_0, StyleSheet int_2)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: reference to a compiler-generated method
    // ISSUE: reference to a compiler-generated method
    Console.smethod_0((IEnumerable<KeyValuePair<string, Color>>) new TextAnnotator.Class32(int_2).GetAnnotationMap(char_0.smethod_2<T>()), action_0);
  }

  private static void smethod_7([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] StyledString obj1, [System.Runtime.InteropServices.In] StyleSheet obj2)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: reference to a compiler-generated method
    Console.smethod_8((IEnumerable<KeyValuePair<string, Color>>) new TextAnnotator.Class32(obj2).GetAnnotationMap(obj1.AbstractValue), obj1);
    Console.smethod_1(obj1, obj0);
  }

  private static void smethod_8(
    [System.Runtime.InteropServices.In] IEnumerable<KeyValuePair<string, Color>> obj0,
    StyledString gparam_0)
  {
    int num = 0;
    foreach (KeyValuePair<string, Color> keyValuePair in obj0)
    {
      for (int index1 = 0; index1 < keyValuePair.Key.Length; ++index1)
      {
        int length1 = gparam_0.CharacterIndexGeometry.GetLength(0);
        int length2 = gparam_0.CharacterIndexGeometry.GetLength(1);
        for (int index2 = 0; index2 < length1; ++index2)
        {
          for (int index3 = 0; index3 < length2; ++index3)
          {
            if (gparam_0.CharacterIndexGeometry[index2, index3] == num)
              gparam_0.ColorGeometry[index2, index3] = keyValuePair.Value;
          }
        }
        ++num;
      }
    }
  }

  private static void smethod_9(
    string string_2,
    char[] styledString_0,
    int styleSheet_0,
    [System.Runtime.InteropServices.In] int obj3,
    [System.Runtime.InteropServices.In] StyleSheet obj4)
  {
    // ISSUE: reference to a compiler-generated method
    string char_0 = styledString_0.smethod_2<char[]>().Substring(styleSheet_0, obj3);
    Console.smethod_6<string>(string_2, char_0, obj4);
  }

  private static void smethod_10<T, U>(Action<T, U> string_2, T char_0, U int_2, Color int_3)
  {
    int foregroundColor = (int) System.Console.ForegroundColor;
    System.Console.ForegroundColor = ((ColorManagerFactory) Console.colorManager_0).GetConsoleColor(int_3);
    string_2(char_0, int_2);
    System.Console.ForegroundColor = (ConsoleColor) foregroundColor;
  }

  private static void smethod_11<T, U>([System.Runtime.InteropServices.In] Action<T, U> obj0, [System.Runtime.InteropServices.In] T obj1, [System.Runtime.InteropServices.In] U obj2, [System.Runtime.InteropServices.In] ColorAlternator obj3)
  {
    // ISSUE: reference to a compiler-generated field
    if (Console.Class18<T, U>.callSite_1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      Console.Class18<T, U>.callSite_1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (Console)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, string> target = Console.Class18<T, U>.callSite_1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, string>> callSite1 = Console.Class18<T, U>.callSite_1;
    // ISSUE: reference to a compiler-generated field
    if (Console.Class18<T, U>.callSite_0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      Console.Class18<T, U>.callSite_0 = CallSite<Func<CallSite, Type, string, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Format", (IEnumerable<Type>) null, typeof (Console), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    object obj = Console.Class18<T, U>.callSite_0.Target((CallSite) Console.Class18<T, U>.callSite_0, typeof (string), obj1.ToString(), obj2.smethod_3<U>());
    string string_0 = target((CallSite) callSite1, obj);
    Color nextColor = ((ColorExtensions) obj3).GetNextColor(string_0);
    int foregroundColor = (int) System.Console.ForegroundColor;
    System.Console.ForegroundColor = ((ColorManagerFactory) Console.colorManager_0).GetConsoleColor(nextColor);
    obj0(obj1, obj2);
    System.Console.ForegroundColor = (ConsoleColor) foregroundColor;
  }

  private static void smethod_12<T, U>([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] T obj1, [System.Runtime.InteropServices.In] U obj2, StyleSheet color_16)
  {
    // ISSUE: object of a compiler-generated type is created
    TextAnnotator textAnnotator = (TextAnnotator) new TextAnnotator.Class32(color_16);
    // ISSUE: reference to a compiler-generated field
    if (Console.Class19<T, U>.callSite_1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      Console.Class19<T, U>.callSite_1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (Console)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, string> target = Console.Class19<T, U>.callSite_1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, string>> callSite1 = Console.Class19<T, U>.callSite_1;
    // ISSUE: reference to a compiler-generated field
    if (Console.Class19<T, U>.callSite_0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      Console.Class19<T, U>.callSite_0 = CallSite<Func<CallSite, Type, string, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Format", (IEnumerable<Type>) null, typeof (Console), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    object obj = Console.Class19<T, U>.callSite_0.Target((CallSite) Console.Class19<T, U>.callSite_0, typeof (string), obj1.ToString(), obj2.smethod_3<U>());
    string taskGenerator = target((CallSite) callSite1, obj);
    // ISSUE: reference to a compiler-generated method
    Console.smethod_0((IEnumerable<KeyValuePair<string, Color>>) ((TextAnnotator.Class32) textAnnotator).GetAnnotationMap(taskGenerator), obj0);
  }

  private static void smethod_13<T, U>(
    [System.Runtime.InteropServices.In] string obj0,
    [System.Runtime.InteropServices.In] T obj1,
    [System.Runtime.InteropServices.In] U obj2,
    Color colorAlternator_0,
    [System.Runtime.InteropServices.In] Color obj4)
  {
    TextFormatter textFormatter = (TextFormatter) new TextPattern(obj4);
    // ISSUE: reference to a compiler-generated field
    if (Console.Class20<T, U>.callSite_1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      Console.Class20<T, U>.callSite_1 = CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (List<KeyValuePair<string, Color>>), typeof (Console)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, List<KeyValuePair<string, Color>>> target = Console.Class20<T, U>.callSite_1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>> callSite1 = Console.Class20<T, U>.callSite_1;
    // ISSUE: reference to a compiler-generated field
    if (Console.Class20<T, U>.callSite_0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      Console.Class20<T, U>.callSite_0 = CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetFormatMap", (IEnumerable<Type>) null, typeof (Console), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    object obj = Console.Class20<T, U>.callSite_0.Target((CallSite) Console.Class20<T, U>.callSite_0, textFormatter, obj1.ToString(), obj2.smethod_3<U>(), new Color[1]
    {
      colorAlternator_0
    });
    Console.smethod_0((IEnumerable<KeyValuePair<string, Color>>) target((CallSite) callSite1, obj), obj0);
  }

  private static void smethod_14<T>(
    string string_2,
    T gparam_0,
    Formatter gparam_1,
    Color color_16)
  {
    // ISSUE: reference to a compiler-generated method
    Console.smethod_0((IEnumerable<KeyValuePair<string, Color>>) ((TextPattern.Class33) new TextPattern(color_16)).GetFormatMap(gparam_0.ToString(), new object[1]
    {
      ((FrequencyBasedColorAlternator) gparam_1).get_Target()
    }, new Color[1]
    {
      ((FrequencyBasedColorAlternator) gparam_1).get_Color()
    }), string_2);
  }

  private static void smethod_15<T, U>(
    [System.Runtime.InteropServices.In] Action<T, U, U> obj0,
    [System.Runtime.InteropServices.In] T obj1,
    [System.Runtime.InteropServices.In] U obj2,
    [System.Runtime.InteropServices.In] U obj3,
    Color color_17)
  {
    int foregroundColor = (int) System.Console.ForegroundColor;
    System.Console.ForegroundColor = ((ColorManagerFactory) Console.colorManager_0).GetConsoleColor(color_17);
    obj0(obj1, obj2, obj3);
    System.Console.ForegroundColor = (ConsoleColor) foregroundColor;
  }

  private static void smethod_16<T, U>(
    Action<T, U, U> action_0,
    T gparam_0,
    U gparam_1,
    U gparam_2,
    ColorAlternator color_16)
  {
    string string_0 = string.Format(gparam_0.ToString(), (object) gparam_1, (object) gparam_2);
    Color nextColor = ((ColorExtensions) color_16).GetNextColor(string_0);
    int foregroundColor = (int) System.Console.ForegroundColor;
    System.Console.ForegroundColor = ((ColorManagerFactory) Console.colorManager_0).GetConsoleColor(nextColor);
    action_0(gparam_0, gparam_1, gparam_2);
    System.Console.ForegroundColor = (ConsoleColor) foregroundColor;
  }

  private static void smethod_17<T, U>(
    string action_0,
    T gparam_0,
    U gparam_1,
    U gparam_2,
    StyleSheet colorAlternator_0)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: reference to a compiler-generated method
    Console.smethod_0((IEnumerable<KeyValuePair<string, Color>>) new TextAnnotator.Class32(colorAlternator_0).GetAnnotationMap(string.Format(gparam_0.ToString(), (object) gparam_1, (object) gparam_2)), action_0);
  }

  private static void smethod_18<T, U>(
    string string_2,
    T gparam_0,
    U gparam_1,
    U gparam_2,
    Color styleSheet_0,
    [System.Runtime.InteropServices.In] Color obj5)
  {
    TextFormatter textFormatter = (TextFormatter) new TextPattern(obj5);
    // ISSUE: reference to a compiler-generated field
    if (Console.Class21<T, U>.callSite_1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      Console.Class21<T, U>.callSite_1 = CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (List<KeyValuePair<string, Color>>), typeof (Console)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, List<KeyValuePair<string, Color>>> target = Console.Class21<T, U>.callSite_1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>> callSite1 = Console.Class21<T, U>.callSite_1;
    // ISSUE: reference to a compiler-generated field
    if (Console.Class21<T, U>.callSite_0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      Console.Class21<T, U>.callSite_0 = CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetFormatMap", (IEnumerable<Type>) null, typeof (Console), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    object obj = Console.Class21<T, U>.callSite_0.Target((CallSite) Console.Class21<T, U>.callSite_0, textFormatter, gparam_0.ToString(), new U[2]
    {
      gparam_1,
      gparam_2
    }.smethod_3<U[]>(), new Color[1]{ styleSheet_0 });
    Console.smethod_0((IEnumerable<KeyValuePair<string, Color>>) target((CallSite) callSite1, obj), string_2);
  }

  private static void smethod_19<T>(
    [System.Runtime.InteropServices.In] string obj0,
    T gparam_0,
    Formatter gparam_1,
    Formatter gparam_2,
    Color color_16)
  {
    // ISSUE: reference to a compiler-generated method
    Console.smethod_0((IEnumerable<KeyValuePair<string, Color>>) ((TextPattern.Class33) new TextPattern(color_16)).GetFormatMap(gparam_0.ToString(), new object[2]
    {
      ((FrequencyBasedColorAlternator) gparam_1).get_Target(),
      ((FrequencyBasedColorAlternator) gparam_2).get_Target()
    }, new Color[2]
    {
      ((FrequencyBasedColorAlternator) gparam_1).get_Color(),
      ((FrequencyBasedColorAlternator) gparam_2).get_Color()
    }), obj0);
  }

  private static void smethod_20<T, U>(
    Action<T, U, U, U> string_2,
    T gparam_0,
    U formatter_0,
    U formatter_1,
    U color_16,
    [System.Runtime.InteropServices.In] Color obj5)
  {
    int foregroundColor = (int) System.Console.ForegroundColor;
    System.Console.ForegroundColor = ((ColorManagerFactory) Console.colorManager_0).GetConsoleColor(obj5);
    string_2(gparam_0, formatter_0, formatter_1, color_16);
    System.Console.ForegroundColor = (ConsoleColor) foregroundColor;
  }

  private static void smethod_21<T, U>(
    [System.Runtime.InteropServices.In] Action<T, U, U, U> obj0,
    T gparam_0,
    U gparam_1,
    U gparam_2,
    U gparam_3,
    ColorAlternator color_16)
  {
    string string_0 = string.Format(gparam_0.ToString(), (object) gparam_1, (object) gparam_2, (object) gparam_3);
    Color nextColor = ((ColorExtensions) color_16).GetNextColor(string_0);
    int foregroundColor = (int) System.Console.ForegroundColor;
    System.Console.ForegroundColor = ((ColorManagerFactory) Console.colorManager_0).GetConsoleColor(nextColor);
    obj0(gparam_0, gparam_1, gparam_2, gparam_3);
    System.Console.ForegroundColor = (ConsoleColor) foregroundColor;
  }

  private static void smethod_22<T, U>(
    [System.Runtime.InteropServices.In] string obj0,
    T gparam_0,
    U gparam_1,
    U gparam_2,
    U gparam_3,
    StyleSheet colorAlternator_0)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: reference to a compiler-generated method
    Console.smethod_0((IEnumerable<KeyValuePair<string, Color>>) new TextAnnotator.Class32(colorAlternator_0).GetAnnotationMap(string.Format(gparam_0.ToString(), (object) gparam_1, (object) gparam_2, (object) gparam_3)), obj0);
  }

  private static void smethod_23<T, U>(
    [System.Runtime.InteropServices.In] string obj0,
    T gparam_0,
    U gparam_1,
    U gparam_2,
    U gparam_3,
    Color styleSheet_0,
    [System.Runtime.InteropServices.In] Color obj6)
  {
    TextFormatter textFormatter = (TextFormatter) new TextPattern(obj6);
    // ISSUE: reference to a compiler-generated field
    if (Console.Class22<T, U>.callSite_1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      Console.Class22<T, U>.callSite_1 = CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (List<KeyValuePair<string, Color>>), typeof (Console)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, List<KeyValuePair<string, Color>>> target = Console.Class22<T, U>.callSite_1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>> callSite1 = Console.Class22<T, U>.callSite_1;
    // ISSUE: reference to a compiler-generated field
    if (Console.Class22<T, U>.callSite_0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      Console.Class22<T, U>.callSite_0 = CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetFormatMap", (IEnumerable<Type>) null, typeof (Console), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    object obj = Console.Class22<T, U>.callSite_0.Target((CallSite) Console.Class22<T, U>.callSite_0, textFormatter, gparam_0.ToString(), new U[3]
    {
      gparam_1,
      gparam_2,
      gparam_3
    }.smethod_3<U[]>(), new Color[1]{ styleSheet_0 });
    Console.smethod_0((IEnumerable<KeyValuePair<string, Color>>) target((CallSite) callSite1, obj), obj0);
  }

  private static void smethod_24<T>(
    [System.Runtime.InteropServices.In] string obj0,
    [System.Runtime.InteropServices.In] T obj1,
    Formatter gparam_1,
    Formatter gparam_2,
    Formatter gparam_3,
    Color color_16)
  {
    // ISSUE: reference to a compiler-generated method
    Console.smethod_0((IEnumerable<KeyValuePair<string, Color>>) ((TextPattern.Class33) new TextPattern(color_16)).GetFormatMap(obj1.ToString(), new object[3]
    {
      ((FrequencyBasedColorAlternator) gparam_1).get_Target(),
      ((FrequencyBasedColorAlternator) gparam_2).get_Target(),
      ((FrequencyBasedColorAlternator) gparam_3).get_Target()
    }, new Color[3]
    {
      ((FrequencyBasedColorAlternator) gparam_1).get_Color(),
      ((FrequencyBasedColorAlternator) gparam_2).get_Color(),
      ((FrequencyBasedColorAlternator) gparam_3).get_Color()
    }), obj0);
  }

  private static void smethod_25<T>(
    [System.Runtime.InteropServices.In] string obj0,
    T gparam_0,
    Formatter[] formatter_0,
    Color formatter_1)
  {
    // ISSUE: reference to a compiler-generated method
    Console.smethod_0((IEnumerable<KeyValuePair<string, Color>>) ((TextPattern.Class33) new TextPattern(formatter_1)).GetFormatMap(gparam_0.ToString(), ((IEnumerable<Formatter>) formatter_0).Select<Formatter, object>((Func<Formatter, object>) delegate))); // Unable to render the statement
  }

  private static void smethod_26<T>(
    [System.Runtime.InteropServices.In] Action<object, Color> obj0,
    [System.Runtime.InteropServices.In] IEnumerable<T> obj1,
    [System.Runtime.InteropServices.In] Color obj2,
    [System.Runtime.InteropServices.In] Color obj3,
    [System.Runtime.InteropServices.In] int obj4)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: reference to a compiler-generated method
    foreach (StyleClass<T> styleClass in new GradientGenerator.Class28<>().GenerateGradient<T>(obj1, obj2, obj3, obj4))
      obj0((object) styleClass.Target, styleClass.Color);
  }

  private static Figlet smethod_27(FigletFont action_0)
  {
    return action_0 == null ? new Figlet() : (Figlet) new FigletFont(action_0);
  }

  private static ColorStore smethod_28()
  {
    ConcurrentDictionary<Color, ConsoleColor> uint_1 = new ConcurrentDictionary<Color, ConsoleColor>();
    ConcurrentDictionary<ConsoleColor, Color> concurrentDictionary = new ConcurrentDictionary<ConsoleColor, Color>();
    concurrentDictionary.TryAdd(ConsoleColor.Black, Console.color_0);
    concurrentDictionary.TryAdd(ConsoleColor.Blue, Console.color_1);
    concurrentDictionary.TryAdd(ConsoleColor.Cyan, Console.color_2);
    concurrentDictionary.TryAdd(ConsoleColor.DarkBlue, Console.color_3);
    concurrentDictionary.TryAdd(ConsoleColor.DarkCyan, Console.color_4);
    concurrentDictionary.TryAdd(ConsoleColor.DarkGray, Console.color_5);
    concurrentDictionary.TryAdd(ConsoleColor.DarkGreen, Console.color_6);
    concurrentDictionary.TryAdd(ConsoleColor.DarkMagenta, Console.color_7);
    concurrentDictionary.TryAdd(ConsoleColor.DarkRed, Console.color_8);
    concurrentDictionary.TryAdd(ConsoleColor.DarkYellow, Console.color_9);
    concurrentDictionary.TryAdd(ConsoleColor.Gray, Console.color_10);
    concurrentDictionary.TryAdd(ConsoleColor.Green, Console.color_11);
    concurrentDictionary.TryAdd(ConsoleColor.Magenta, Console.color_12);
    concurrentDictionary.TryAdd(ConsoleColor.Red, Console.color_13);
    concurrentDictionary.TryAdd(ConsoleColor.White, Console.color_14);
    concurrentDictionary.TryAdd(ConsoleColor.Yellow, Console.color_15);
    ConcurrentDictionary<ConsoleColor, Color> uint_2 = concurrentDictionary;
    return (ColorStore) new DefaultFonts(uint_1, uint_2);
  }

  private static void smethod_29([System.Runtime.InteropServices.In] bool obj0)
  {
    Console.colorStore_0 = Console.smethod_28();
    Console.colorManagerFactory_0 = (ColorManagerFactory) new ColorMapper();
    Console.colorManager_0 = ((ColorMapper) Console.colorManagerFactory_0).GetManager(Console.colorStore_0, 16 /*0x10*/, 1, obj0);
    if (Console.colorManager_0.IsInCompatibilityMode)
      return;
    ((ColorMapper) new COLORREF()).SetBatchBufferColors(Console.dictionary_0);
  }

  public static Color BackgroundColor
  {
    get => ((ColorManagerFactory) Console.colorManager_0).GetColor(System.Console.BackgroundColor);
    [param: System.Runtime.InteropServices.In] set
    {
      System.Console.BackgroundColor = ((ColorManagerFactory) Console.colorManager_0).GetConsoleColor(value);
    }
  }

  public static int BufferHeight
  {
    get => System.Console.BufferHeight;
    [param: System.Runtime.InteropServices.In] set => System.Console.BufferHeight = value;
  }

  public static int BufferWidth
  {
    get => System.Console.BufferWidth;
    [param: System.Runtime.InteropServices.In] set => System.Console.BufferWidth = value;
  }

  public static bool CapsLock => System.Console.CapsLock;

  public static int CursorLeft
  {
    get => System.Console.CursorLeft;
    set => System.Console.CursorLeft = value;
  }

  public static int CursorSize
  {
    get => System.Console.CursorSize;
    set => System.Console.CursorSize = value;
  }

  public static int CursorTop
  {
    get => System.Console.CursorTop;
    set => System.Console.CursorTop = value;
  }

  public static bool CursorVisible
  {
    get => System.Console.CursorVisible;
    set => System.Console.CursorVisible = value;
  }

  public static TextWriter Error => System.Console.Error;

  public static Color ForegroundColor
  {
    get => ((ColorManagerFactory) Console.colorManager_0).GetColor(System.Console.ForegroundColor);
    set
    {
      System.Console.ForegroundColor = ((ColorManagerFactory) Console.colorManager_0).GetConsoleColor(value);
    }
  }

  public static TextReader In => System.Console.In;

  public static Encoding InputEncoding
  {
    get => System.Console.InputEncoding;
    set => System.Console.InputEncoding = value;
  }

  public static bool IsErrorRedirected => System.Console.IsErrorRedirected;

  public static bool IsInputRedirected => System.Console.IsInputRedirected;

  public static bool IsOutputRedirected => System.Console.IsOutputRedirected;

  public static bool KeyAvailable => System.Console.KeyAvailable;

  public static int LargestWindowHeight => System.Console.LargestWindowHeight;

  public static int LargestWindowWidth => System.Console.LargestWindowWidth;

  public static bool NumberLock => System.Console.NumberLock;

  public static TextWriter Out => System.Console.Out;

  public static Encoding OutputEncoding
  {
    get => System.Console.OutputEncoding;
    set => System.Console.OutputEncoding = value;
  }

  public static string Title
  {
    get => System.Console.Title;
    set => System.Console.Title = value;
  }

  public static bool TreatControlCAsInput
  {
    get => System.Console.TreatControlCAsInput;
    set => System.Console.TreatControlCAsInput = value;
  }

  public static int WindowHeight
  {
    get => System.Console.WindowHeight;
    set => System.Console.WindowHeight = value;
  }

  public static int WindowLeft
  {
    get => System.Console.WindowLeft;
    set => System.Console.WindowLeft = value;
  }

  public static int WindowTop
  {
    get => System.Console.WindowTop;
    set => System.Console.WindowTop = value;
  }

  public static int WindowWidth
  {
    get => System.Console.WindowWidth;
    set => System.Console.WindowWidth = value;
  }

  public static event ConsoleCancelEventHandler CancelKeyPress = new ConsoleCancelEventHandler(((Console.Class17) Console.Class15.\u003C\u003E9).method_0);

  static Console()
  {
    bool flag = false;
    try
    {
      Console.dictionary_0 = ((ColorMapper) new COLORREF()).GetBufferColors();
    }
    catch
    {
      flag = true;
    }
    Console.smethod_29(flag);
    // ISSUE: reference to a compiler-generated field
    System.Console.CancelKeyPress += (ConsoleCancelEventHandler) (([System.Runtime.InteropServices.In] obj0, [System.Runtime.InteropServices.In] obj1) => Console.consoleCancelEventHandler_0(obj0, obj1));
  }

  public static void Write(bool value) => System.Console.Write(value);

  public static void Write(bool value, [System.Runtime.InteropServices.In] Color obj1)
  {
    Console.smethod_2<bool>(new Action<bool>(System.Console.Write), value, obj1);
  }

  public static void WriteAlternating(bool value, [System.Runtime.InteropServices.In] ColorAlternator obj1)
  {
    Console.smethod_4<bool>(new Action<bool>(System.Console.Write), value, obj1);
  }

  public static void WriteStyled(bool value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<bool>(Console.string_1, value, obj1);
  }

  public static void Write([System.Runtime.InteropServices.In] char obj0) => System.Console.Write(obj0);

  public static void Write(char value, Color alternator)
  {
    Console.smethod_2<char>(new Action<char>(System.Console.Write), value, alternator);
  }

  public static void WriteAlternating(char value, ColorAlternator styleSheet)
  {
    Console.smethod_4<char>(new Action<char>(System.Console.Write), value, styleSheet);
  }

  public static void WriteStyled(char value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<char>(Console.string_1, value, obj1);
  }

  public static void Write([System.Runtime.InteropServices.In] char[] obj0) => System.Console.Write(obj0);

  public static void Write(char[] value, Color alternator)
  {
    Console.smethod_2<char[]>(new Action<char[]>(System.Console.Write), value, alternator);
  }

  public static void WriteAlternating(char[] value, ColorAlternator styleSheet)
  {
    Console.smethod_4<char[]>(new Action<char[]>(System.Console.Write), value, styleSheet);
  }

  public static void WriteStyled(char[] value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<char[]>(Console.string_1, value, obj1);
  }

  public static void Write([System.Runtime.InteropServices.In] Decimal obj0) => System.Console.Write(obj0);

  public static void Write(Decimal value, Color alternator)
  {
    Console.smethod_2<Decimal>(new Action<Decimal>(System.Console.Write), value, alternator);
  }

  public static void WriteAlternating(Decimal value, ColorAlternator styleSheet)
  {
    Console.smethod_4<Decimal>(new Action<Decimal>(System.Console.Write), value, styleSheet);
  }

  public static void WriteStyled(Decimal value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<Decimal>(Console.string_1, value, obj1);
  }

  public static void Write([System.Runtime.InteropServices.In] double obj0) => System.Console.Write(obj0);

  public static void Write(double value, Color alternator)
  {
    Console.smethod_2<double>(new Action<double>(System.Console.Write), value, alternator);
  }

  public static void WriteAlternating(double value, ColorAlternator styleSheet)
  {
    Console.smethod_4<double>(new Action<double>(System.Console.Write), value, styleSheet);
  }

  public static void WriteStyled(double value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<double>(Console.string_1, value, obj1);
  }

  public static void Write([System.Runtime.InteropServices.In] float obj0) => System.Console.Write(obj0);

  public static void Write(float value, Color alternator)
  {
    Console.smethod_2<float>(new Action<float>(System.Console.Write), value, alternator);
  }

  public static void WriteAlternating(float value, ColorAlternator styleSheet)
  {
    Console.smethod_4<float>(new Action<float>(System.Console.Write), value, styleSheet);
  }

  public static void WriteStyled(float value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<float>(Console.string_1, value, obj1);
  }

  public static void Write([System.Runtime.InteropServices.In] int obj0) => System.Console.Write(obj0);

  public static void Write(int value, Color alternator)
  {
    Console.smethod_2<int>(new Action<int>(System.Console.Write), value, alternator);
  }

  public static void WriteAlternating(int value, ColorAlternator styleSheet)
  {
    Console.smethod_4<int>(new Action<int>(System.Console.Write), value, styleSheet);
  }

  public static void WriteStyled(int value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<int>(Console.string_1, value, obj1);
  }

  public static void Write([System.Runtime.InteropServices.In] long obj0) => System.Console.Write(obj0);

  public static void Write(long value, Color alternator)
  {
    Console.smethod_2<long>(new Action<long>(System.Console.Write), value, alternator);
  }

  public static void WriteAlternating(long value, ColorAlternator styleSheet)
  {
    Console.smethod_4<long>(new Action<long>(System.Console.Write), value, styleSheet);
  }

  public static void WriteStyled(long value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<long>(Console.string_1, value, obj1);
  }

  public static void Write([System.Runtime.InteropServices.In] object obj0) => System.Console.Write(obj0);

  public static void Write(object value, Color alternator)
  {
    Console.smethod_2<object>(new Action<object>(System.Console.Write), value, alternator);
  }

  public static void WriteAlternating(object value, ColorAlternator styleSheet)
  {
    Console.smethod_4<object>(new Action<object>(System.Console.Write), value, styleSheet);
  }

  public static void WriteStyled(object value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<object>(Console.string_1, value, obj1);
  }

  public static void Write([System.Runtime.InteropServices.In] string obj0) => System.Console.Write(obj0);

  public static void Write(string value, Color alternator)
  {
    Console.smethod_2<string>(new Action<string>(System.Console.Write), value, alternator);
  }

  public static void WriteAlternating(string value, ColorAlternator styleSheet)
  {
    Console.smethod_4<string>(new Action<string>(System.Console.Write), value, styleSheet);
  }

  public static void WriteStyled(string value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<string>(Console.string_1, value, obj1);
  }

  public static void Write([System.Runtime.InteropServices.In] uint obj0) => System.Console.Write(obj0);

  public static void Write(uint value, Color alternator)
  {
    Console.smethod_2<uint>(new Action<uint>(System.Console.Write), value, alternator);
  }

  public static void WriteAlternating(uint value, ColorAlternator styleSheet)
  {
    Console.smethod_4<uint>(new Action<uint>(System.Console.Write), value, styleSheet);
  }

  public static void WriteStyled(uint value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<uint>(Console.string_1, value, obj1);
  }

  public static void Write([System.Runtime.InteropServices.In] ulong obj0) => System.Console.Write(obj0);

  public static void Write(ulong value, Color alternator)
  {
    Console.smethod_2<ulong>(new Action<ulong>(System.Console.Write), value, alternator);
  }

  public static void WriteAlternating(ulong value, ColorAlternator styleSheet)
  {
    Console.smethod_4<ulong>(new Action<ulong>(System.Console.Write), value, styleSheet);
  }

  public static void WriteStyled(ulong value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<ulong>(Console.string_1, value, obj1);
  }

  public static void Write([System.Runtime.InteropServices.In] string obj0, object color)
  {
    System.Console.Write(obj0, color);
  }

  public static void Write([System.Runtime.InteropServices.In] string obj0, object alternator, [System.Runtime.InteropServices.In] Color obj2)
  {
    Console.smethod_10<string, object>(new Action<string, object>(System.Console.Write), obj0, alternator, obj2);
  }

  public static void WriteAlternating(string format, object arg0, [System.Runtime.InteropServices.In] ColorAlternator obj2)
  {
    Console.smethod_11<string, object>(new Action<string, object>(System.Console.Write), format, arg0, obj2);
  }

  public static void WriteStyled([System.Runtime.InteropServices.In] string obj0, object arg0, StyleSheet color)
  {
    Console.smethod_12<string, object>(Console.string_1, obj0, arg0, color);
  }

  public static void WriteFormatted([System.Runtime.InteropServices.In] string obj0, object arg0, Color alternator, [System.Runtime.InteropServices.In] Color obj3)
  {
    Console.smethod_13<string, object>(Console.string_1, obj0, arg0, alternator, obj3);
  }

  public static void WriteFormatted([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] Formatter obj1, Color styleSheet)
  {
    Console.smethod_14<string>(Console.string_1, obj0, obj1, styleSheet);
  }

  public static void Write([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] object[] obj1)
  {
    System.Console.Write(obj0, obj1);
  }

  public static void Write(string format, Color arg0, object[] defaultColor)
  {
    Console.smethod_10<string, object[]>(new Action<string, object[]>(System.Console.Write), format, defaultColor, arg0);
  }

  public static void WriteAlternating(string format, params ColorAlternator args, [System.Runtime.InteropServices.In] object[] obj2)
  {
    Console.smethod_11<string, object[]>(new Action<string, object[]>(System.Console.Write), format, obj2, args);
  }

  public static void WriteStyled([System.Runtime.InteropServices.In] StyleSheet obj0, string color, params object[] args)
  {
    Console.smethod_12<string, object[]>(Console.string_1, color, args, obj0);
  }

  public static void WriteFormatted(
    [System.Runtime.InteropServices.In] string obj0,
    Color alternator,
    params Color args,
    [System.Runtime.InteropServices.In] object[] obj3)
  {
    Console.smethod_13<string, object[]>(Console.string_1, obj0, obj3, alternator, args);
  }

  public static void WriteFormatted([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] Color obj1, params Formatter[] args)
  {
    Console.smethod_25<string>(Console.string_1, obj0, args, obj1);
  }

  public static void Write([System.Runtime.InteropServices.In] char[] obj0, [System.Runtime.InteropServices.In] int obj1, int defaultColor)
  {
    System.Console.Write(obj0, obj1, defaultColor);
  }

  public static void Write([System.Runtime.InteropServices.In] char[] obj0, int defaultColor, params int args, [System.Runtime.InteropServices.In] Color obj3)
  {
    Console.smethod_3(new Action<string>(System.Console.Write), obj0, defaultColor, args, obj3);
  }

  public static void WriteAlternating([System.Runtime.InteropServices.In] char[] obj0, [System.Runtime.InteropServices.In] int obj1, int count, [System.Runtime.InteropServices.In] ColorAlternator obj3)
  {
    Console.smethod_5(new Action<string>(System.Console.Write), obj0, obj1, count, obj3);
  }

  public static void WriteStyled([System.Runtime.InteropServices.In] char[] obj0, [System.Runtime.InteropServices.In] int obj1, [System.Runtime.InteropServices.In] int obj2, StyleSheet color)
  {
    Console.smethod_9(Console.string_1, obj0, obj1, obj2, color);
  }

  public static void Write([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] object obj1, [System.Runtime.InteropServices.In] object obj2)
  {
    System.Console.Write(obj0, obj1, obj2);
  }

  public static void Write([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] object obj1, object count, Color styleSheet)
  {
    Console.smethod_15<string, object>(new Action<string, object, object>(System.Console.Write), obj0, obj1, count, styleSheet);
  }

  public static void WriteAlternating([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] object obj1, object arg1, [System.Runtime.InteropServices.In] ColorAlternator obj3)
  {
    Console.smethod_16<string, object>(new Action<string, object, object>(System.Console.Write), obj0, obj1, arg1, obj3);
  }

  public static void WriteStyled([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] object obj1, [System.Runtime.InteropServices.In] object obj2, StyleSheet color)
  {
    Console.smethod_17<string, object>(Console.string_1, obj0, obj1, obj2, color);
  }

  public static void WriteFormatted(
    [System.Runtime.InteropServices.In] string obj0,
    [System.Runtime.InteropServices.In] object obj1,
    [System.Runtime.InteropServices.In] object obj2,
    Color alternator,
    [System.Runtime.InteropServices.In] Color obj4)
  {
    Console.smethod_18<string, object>(Console.string_1, obj0, obj1, obj2, alternator, obj4);
  }

  public static void WriteFormatted(
    string format,
    Formatter arg0,
    Formatter arg1,
    Color styledColor)
  {
    Console.smethod_19<string>(Console.string_1, format, arg0, arg1, styledColor);
  }

  public static void Write([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] object obj1, [System.Runtime.InteropServices.In] object obj2, [System.Runtime.InteropServices.In] object obj3)
  {
    System.Console.Write(obj0, obj1, obj2, obj3);
  }

  public static void Write(
    [System.Runtime.InteropServices.In] string obj0,
    [System.Runtime.InteropServices.In] object obj1,
    [System.Runtime.InteropServices.In] object obj2,
    object defaultColor,
    [System.Runtime.InteropServices.In] Color obj4)
  {
    Console.smethod_20<string, object>(new Action<string, object, object, object>(System.Console.Write), obj0, obj1, obj2, defaultColor, obj4);
  }

  public static void WriteAlternating(
    string format,
    object arg0,
    object arg1,
    object arg2,
    ColorAlternator color)
  {
    Console.smethod_21<string, object>(new Action<string, object, object, object>(System.Console.Write), format, arg0, arg1, arg2, color);
  }

  public static void WriteStyled(
    string format,
    object arg0,
    object arg1,
    object arg2,
    StyleSheet alternator)
  {
    Console.smethod_22<string, object>(Console.string_1, format, arg0, arg1, arg2, alternator);
  }

  public static void WriteFormatted(
    string format,
    object arg0,
    object arg1,
    object arg2,
    Color styleSheet,
    [System.Runtime.InteropServices.In] Color obj5)
  {
    Console.smethod_23<string, object>(Console.string_1, format, arg0, arg1, arg2, styleSheet, obj5);
  }

  public static void WriteFormatted(
    [System.Runtime.InteropServices.In] string obj0,
    Formatter arg0,
    Formatter arg1,
    Formatter arg2,
    Color styledColor)
  {
    Console.smethod_24<string>(Console.string_1, obj0, arg0, arg1, arg2, styledColor);
  }

  public static void Write(
    string format,
    object arg0,
    object arg1,
    object arg2,
    object defaultColor)
  {
    System.Console.Write(format, new object[4]
    {
      arg0,
      arg1,
      arg2,
      defaultColor
    });
  }

  public static void Write(
    string format,
    object arg0,
    object arg1,
    object arg2,
    object arg3,
    [System.Runtime.InteropServices.In] Color obj5)
  {
    Console.smethod_10<string, object[]>(new Action<string, object[]>(System.Console.Write), format, new object[4]
    {
      arg0,
      arg1,
      arg2,
      arg3
    }, obj5);
  }

  public static void WriteAlternating(
    [System.Runtime.InteropServices.In] string obj0,
    object arg0,
    object arg1,
    object arg2,
    object arg3,
    ColorAlternator color)
  {
    Console.smethod_11<string, object[]>(new Action<string, object[]>(System.Console.Write), obj0, new object[4]
    {
      arg0,
      arg1,
      arg2,
      arg3
    }, color);
  }

  public static void WriteStyled(
    [System.Runtime.InteropServices.In] string obj0,
    object arg0,
    object arg1,
    object arg2,
    object arg3,
    StyleSheet alternator)
  {
    Console.smethod_12<string, object[]>(Console.string_1, obj0, new object[4]
    {
      arg0,
      arg1,
      arg2,
      arg3
    }, alternator);
  }

  public static void WriteFormatted(
    [System.Runtime.InteropServices.In] string obj0,
    object arg0,
    object arg1,
    object arg2,
    object arg3,
    Color styleSheet,
    [System.Runtime.InteropServices.In] Color obj6)
  {
    Console.smethod_13<string, object[]>(Console.string_1, obj0, new object[4]
    {
      arg0,
      arg1,
      arg2,
      arg3
    }, styleSheet, obj6);
  }

  public static void WriteFormatted(
    [System.Runtime.InteropServices.In] string obj0,
    [System.Runtime.InteropServices.In] Formatter obj1,
    Formatter arg1,
    Formatter arg2,
    Formatter arg3,
    Color styledColor)
  {
    Console.smethod_25<string>(Console.string_1, obj0, new Formatter[4]
    {
      obj1,
      arg1,
      arg2,
      arg3
    }, styledColor);
  }

  public static void WriteLine() => System.Console.WriteLine();

  public static void WriteLineAlternating([System.Runtime.InteropServices.In] ColorAlternator obj0)
  {
    Console.smethod_4<string>(new Action<string>(System.Console.Write), Console.string_0, obj0);
  }

  public static void WriteLineStyled([System.Runtime.InteropServices.In] StyleSheet obj0)
  {
    Console.smethod_6<string>(Console.string_1, Console.string_0, obj0);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] bool obj0)
  {
    System.Console.WriteLine(obj0);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] bool obj0, [System.Runtime.InteropServices.In] Color obj1)
  {
    Console.smethod_2<bool>(new Action<bool>(System.Console.WriteLine), obj0, obj1);
  }

  public static void WriteLineAlternating(bool alternator, [System.Runtime.InteropServices.In] ColorAlternator obj1)
  {
    Console.smethod_4<bool>(new Action<bool>(System.Console.WriteLine), alternator, obj1);
  }

  public static void WriteLineStyled(bool value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<bool>(Console.string_0, value, obj1);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] char obj0)
  {
    System.Console.WriteLine(obj0);
  }

  public static void WriteLine(char value, Color alternator)
  {
    Console.smethod_2<char>(new Action<char>(System.Console.WriteLine), value, alternator);
  }

  public static void WriteLineAlternating(char value, ColorAlternator styleSheet)
  {
    Console.smethod_4<char>(new Action<char>(System.Console.WriteLine), value, styleSheet);
  }

  public static void WriteLineStyled(char value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<char>(Console.string_0, value, obj1);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] char[] obj0)
  {
    System.Console.WriteLine(obj0);
  }

  public static void WriteLine(char[] value, Color alternator)
  {
    Console.smethod_2<char[]>(new Action<char[]>(System.Console.WriteLine), value, alternator);
  }

  public static void WriteLineAlternating(char[] value, ColorAlternator styleSheet)
  {
    Console.smethod_4<char[]>(new Action<char[]>(System.Console.WriteLine), value, styleSheet);
  }

  public static void WriteLineStyled(char[] value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<char[]>(Console.string_0, value, obj1);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] Decimal obj0)
  {
    System.Console.WriteLine(obj0);
  }

  public static void WriteLine(Decimal value, Color alternator)
  {
    Console.smethod_2<Decimal>(new Action<Decimal>(System.Console.WriteLine), value, alternator);
  }

  public static void WriteLineAlternating(Decimal value, ColorAlternator styleSheet)
  {
    Console.smethod_4<Decimal>(new Action<Decimal>(System.Console.WriteLine), value, styleSheet);
  }

  public static void WriteLineStyled(Decimal value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<Decimal>(Console.string_0, value, obj1);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] double obj0)
  {
    System.Console.WriteLine(obj0);
  }

  public static void WriteLine(double value, Color alternator)
  {
    Console.smethod_2<double>(new Action<double>(System.Console.WriteLine), value, alternator);
  }

  public static void WriteLineAlternating(double value, ColorAlternator styleSheet)
  {
    Console.smethod_4<double>(new Action<double>(System.Console.WriteLine), value, styleSheet);
  }

  public static void WriteLineStyled(double value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<double>(Console.string_0, value, obj1);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] float obj0)
  {
    System.Console.WriteLine(obj0);
  }

  public static void WriteLine(float value, Color alternator)
  {
    Console.smethod_2<float>(new Action<float>(System.Console.WriteLine), value, alternator);
  }

  public static void WriteLineAlternating(float value, ColorAlternator styleSheet)
  {
    Console.smethod_4<float>(new Action<float>(System.Console.WriteLine), value, styleSheet);
  }

  public static void WriteLineStyled(float value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<float>(Console.string_0, value, obj1);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] int obj0)
  {
    System.Console.WriteLine(obj0);
  }

  public static void WriteLine(int value, Color alternator)
  {
    Console.smethod_2<int>(new Action<int>(System.Console.WriteLine), value, alternator);
  }

  public static void WriteLineAlternating(int value, ColorAlternator styleSheet)
  {
    Console.smethod_4<int>(new Action<int>(System.Console.WriteLine), value, styleSheet);
  }

  public static void WriteLineStyled(int value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<int>(Console.string_0, value, obj1);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] long obj0)
  {
    System.Console.WriteLine(obj0);
  }

  public static void WriteLine(long value, Color alternator)
  {
    Console.smethod_2<long>(new Action<long>(System.Console.WriteLine), value, alternator);
  }

  public static void WriteLineAlternating(long value, ColorAlternator styleSheet)
  {
    Console.smethod_4<long>(new Action<long>(System.Console.WriteLine), value, styleSheet);
  }

  public static void WriteLineStyled(long value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<long>(Console.string_0, value, obj1);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] object obj0)
  {
    System.Console.WriteLine(obj0);
  }

  public static void WriteLine(object value, Color alternator)
  {
    Console.smethod_2<object>(new Action<object>(System.Console.WriteLine), value, alternator);
  }

  public static void WriteLineAlternating(object value, ColorAlternator styleSheet)
  {
    Console.smethod_4<object>(new Action<object>(System.Console.WriteLine), value, styleSheet);
  }

  public static void WriteLineStyled(StyledString value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_7(Console.string_0, value, obj1);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] string obj0)
  {
    System.Console.WriteLine(obj0);
  }

  public static void WriteLine(string value, Color alternator)
  {
    Console.smethod_2<string>(new Action<string>(System.Console.WriteLine), value, alternator);
  }

  public static void WriteLineAlternating(string value, ColorAlternator styleSheet)
  {
    Console.smethod_4<string>(new Action<string>(System.Console.WriteLine), value, styleSheet);
  }

  public static void WriteLineStyled(string value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<string>(Console.string_0, value, obj1);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] uint obj0)
  {
    System.Console.WriteLine(obj0);
  }

  public static void WriteLine(uint value, Color alternator)
  {
    Console.smethod_2<uint>(new Action<uint>(System.Console.WriteLine), value, alternator);
  }

  public static void WriteLineAlternating(uint value, ColorAlternator styleSheet)
  {
    Console.smethod_4<uint>(new Action<uint>(System.Console.WriteLine), value, styleSheet);
  }

  public static void WriteLineStyled(uint value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<uint>(Console.string_0, value, obj1);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] ulong obj0)
  {
    System.Console.WriteLine(obj0);
  }

  public static void WriteLine(ulong value, Color alternator)
  {
    Console.smethod_2<ulong>(new Action<ulong>(System.Console.WriteLine), value, alternator);
  }

  public static void WriteLineAlternating(ulong value, ColorAlternator styleSheet)
  {
    Console.smethod_4<ulong>(new Action<ulong>(System.Console.WriteLine), value, styleSheet);
  }

  public static void WriteLineStyled(ulong value, [System.Runtime.InteropServices.In] StyleSheet obj1)
  {
    Console.smethod_6<ulong>(Console.string_0, value, obj1);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] string obj0, object color)
  {
    System.Console.WriteLine(obj0, color);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] string obj0, object alternator, [System.Runtime.InteropServices.In] Color obj2)
  {
    Console.smethod_10<string, object>(new Action<string, object>(System.Console.WriteLine), obj0, alternator, obj2);
  }

  public static void WriteLineAlternating(string format, object arg0, [System.Runtime.InteropServices.In] ColorAlternator obj2)
  {
    Console.smethod_11<string, object>(new Action<string, object>(System.Console.WriteLine), format, arg0, obj2);
  }

  public static void WriteLineStyled([System.Runtime.InteropServices.In] string obj0, object arg0, StyleSheet color)
  {
    Console.smethod_12<string, object>(Console.string_0, obj0, arg0, color);
  }

  public static void WriteLineFormatted([System.Runtime.InteropServices.In] string obj0, object arg0, Color alternator, [System.Runtime.InteropServices.In] Color obj3)
  {
    Console.smethod_13<string, object>(Console.string_0, obj0, arg0, alternator, obj3);
  }

  public static void WriteLineFormatted([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] Formatter obj1, Color styleSheet)
  {
    Console.smethod_14<string>(Console.string_0, obj0, obj1, styleSheet);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] object[] obj1)
  {
    System.Console.WriteLine(obj0, obj1);
  }

  public static void WriteLine(string format, Color arg0, object[] defaultColor)
  {
    Console.smethod_10<string, object[]>(new Action<string, object[]>(System.Console.WriteLine), format, defaultColor, arg0);
  }

  public static void WriteLineAlternating(
    string format,
    params ColorAlternator args,
    [System.Runtime.InteropServices.In] object[] obj2)
  {
    Console.smethod_11<string, object[]>(new Action<string, object[]>(System.Console.WriteLine), format, obj2, args);
  }

  public static void WriteLineStyled([System.Runtime.InteropServices.In] StyleSheet obj0, string color, params object[] args)
  {
    Console.smethod_12<string, object[]>(Console.string_0, color, args, obj0);
  }

  public static void WriteLineFormatted(
    [System.Runtime.InteropServices.In] string obj0,
    Color alternator,
    params Color args,
    [System.Runtime.InteropServices.In] object[] obj3)
  {
    Console.smethod_13<string, object[]>(Console.string_0, obj0, obj3, alternator, args);
  }

  public static void WriteLineFormatted([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] Color obj1, params Formatter[] args)
  {
    Console.smethod_25<string>(Console.string_0, obj0, args, obj1);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] char[] obj0, [System.Runtime.InteropServices.In] int obj1, int defaultColor)
  {
    System.Console.WriteLine(obj0, obj1, defaultColor);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] char[] obj0, int defaultColor, params int args, [System.Runtime.InteropServices.In] Color obj3)
  {
    Console.smethod_3(new Action<string>(System.Console.WriteLine), obj0, defaultColor, args, obj3);
  }

  public static void WriteLineAlternating([System.Runtime.InteropServices.In] char[] obj0, [System.Runtime.InteropServices.In] int obj1, int count, [System.Runtime.InteropServices.In] ColorAlternator obj3)
  {
    Console.smethod_5(new Action<string>(System.Console.WriteLine), obj0, obj1, count, obj3);
  }

  public static void WriteLineStyled([System.Runtime.InteropServices.In] char[] obj0, [System.Runtime.InteropServices.In] int obj1, [System.Runtime.InteropServices.In] int obj2, StyleSheet color)
  {
    Console.smethod_9(Console.string_0, obj0, obj1, obj2, color);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] object obj1, [System.Runtime.InteropServices.In] object obj2)
  {
    System.Console.WriteLine(obj0, obj1, obj2);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] object obj1, object count, Color styleSheet)
  {
    Console.smethod_15<string, object>(new Action<string, object, object>(System.Console.WriteLine), obj0, obj1, count, styleSheet);
  }

  public static void WriteLineAlternating(
    [System.Runtime.InteropServices.In] string obj0,
    [System.Runtime.InteropServices.In] object obj1,
    object arg1,
    [System.Runtime.InteropServices.In] ColorAlternator obj3)
  {
    Console.smethod_16<string, object>(new Action<string, object, object>(System.Console.WriteLine), obj0, obj1, arg1, obj3);
  }

  public static void WriteLineStyled([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] object obj1, [System.Runtime.InteropServices.In] object obj2, StyleSheet color)
  {
    Console.smethod_17<string, object>(Console.string_0, obj0, obj1, obj2, color);
  }

  public static void WriteLineFormatted(
    [System.Runtime.InteropServices.In] string obj0,
    [System.Runtime.InteropServices.In] object obj1,
    [System.Runtime.InteropServices.In] object obj2,
    Color alternator,
    [System.Runtime.InteropServices.In] Color obj4)
  {
    Console.smethod_18<string, object>(Console.string_0, obj0, obj1, obj2, alternator, obj4);
  }

  public static void WriteLineFormatted(
    string format,
    Formatter arg0,
    Formatter arg1,
    Color styledColor)
  {
    Console.smethod_19<string>(Console.string_0, format, arg0, arg1, styledColor);
  }

  public static void WriteLine([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] object obj1, [System.Runtime.InteropServices.In] object obj2, [System.Runtime.InteropServices.In] object obj3)
  {
    System.Console.WriteLine(obj0, obj1, obj2, obj3);
  }

  public static void WriteLine(
    [System.Runtime.InteropServices.In] string obj0,
    [System.Runtime.InteropServices.In] object obj1,
    [System.Runtime.InteropServices.In] object obj2,
    object defaultColor,
    [System.Runtime.InteropServices.In] Color obj4)
  {
    Console.smethod_20<string, object>(new Action<string, object, object, object>(System.Console.WriteLine), obj0, obj1, obj2, defaultColor, obj4);
  }

  public static void WriteLineAlternating(
    string format,
    object arg0,
    object arg1,
    object arg2,
    ColorAlternator color)
  {
    Console.smethod_21<string, object>(new Action<string, object, object, object>(System.Console.WriteLine), format, arg0, arg1, arg2, color);
  }

  public static void WriteLineStyled(
    string format,
    object arg0,
    object arg1,
    object arg2,
    StyleSheet alternator)
  {
    Console.smethod_22<string, object>(Console.string_0, format, arg0, arg1, arg2, alternator);
  }

  public static void WriteLineFormatted(
    string format,
    object arg0,
    object arg1,
    object arg2,
    Color styleSheet,
    [System.Runtime.InteropServices.In] Color obj5)
  {
    Console.smethod_23<string, object>(Console.string_0, format, arg0, arg1, arg2, styleSheet, obj5);
  }

  public static void WriteLineFormatted(
    [System.Runtime.InteropServices.In] string obj0,
    Formatter arg0,
    Formatter arg1,
    Formatter arg2,
    Color styledColor)
  {
    Console.smethod_24<string>(Console.string_0, obj0, arg0, arg1, arg2, styledColor);
  }

  public static void WriteLine(
    string format,
    object arg0,
    object arg1,
    object arg2,
    object defaultColor)
  {
    System.Console.WriteLine(format, new object[4]
    {
      arg0,
      arg1,
      arg2,
      defaultColor
    });
  }

  public static void WriteLine(
    string format,
    object arg0,
    object arg1,
    object arg2,
    object arg3,
    [System.Runtime.InteropServices.In] Color obj5)
  {
    Console.smethod_10<string, object[]>(new Action<string, object[]>(System.Console.WriteLine), format, new object[4]
    {
      arg0,
      arg1,
      arg2,
      arg3
    }, obj5);
  }

  public static void WriteLineAlternating(
    [System.Runtime.InteropServices.In] string obj0,
    object arg0,
    object arg1,
    object arg2,
    object arg3,
    ColorAlternator color)
  {
    Console.smethod_11<string, object[]>(new Action<string, object[]>(System.Console.WriteLine), obj0, new object[4]
    {
      arg0,
      arg1,
      arg2,
      arg3
    }, color);
  }

  public static void WriteLineStyled(
    [System.Runtime.InteropServices.In] string obj0,
    object arg0,
    object arg1,
    object arg2,
    object arg3,
    StyleSheet alternator)
  {
    Console.smethod_12<string, object[]>(Console.string_0, obj0, new object[4]
    {
      arg0,
      arg1,
      arg2,
      arg3
    }, alternator);
  }

  public static void WriteLineFormatted(
    [System.Runtime.InteropServices.In] string obj0,
    object arg0,
    object arg1,
    object arg2,
    object arg3,
    Color styleSheet,
    [System.Runtime.InteropServices.In] Color obj6)
  {
    Console.smethod_13<string, object[]>(Console.string_0, obj0, new object[4]
    {
      arg0,
      arg1,
      arg2,
      arg3
    }, styleSheet, obj6);
  }

  public static void WriteLineFormatted(
    [System.Runtime.InteropServices.In] string obj0,
    [System.Runtime.InteropServices.In] Formatter obj1,
    Formatter arg1,
    Formatter arg2,
    Formatter arg3,
    Color styledColor)
  {
    Console.smethod_25<string>(Console.string_0, obj0, new Formatter[4]
    {
      obj1,
      arg1,
      arg2,
      arg3
    }, styledColor);
  }

  public static void WriteAscii([System.Runtime.InteropServices.In] string obj0)
  {
    Console.WriteAscii(obj0, (FigletFont) null);
  }

  public static void WriteAscii([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] FigletFont obj1)
  {
    Console.WriteLine(((FigletFont) Console.smethod_27(obj1)).ToAscii(obj0).ConcreteValue);
  }

  public static void WriteAscii([System.Runtime.InteropServices.In] string obj0, [System.Runtime.InteropServices.In] Color obj1)
  {
    Console.WriteAscii(obj0, (FigletFont) null, obj1);
  }

  public static void WriteAscii(string value, FigletFont font, [System.Runtime.InteropServices.In] Color obj2)
  {
    Console.WriteLine(((FigletFont) Console.smethod_27(font)).ToAscii(value).ConcreteValue, obj2);
  }

  public static void WriteAsciiAlternating(string value, ColorAlternator color)
  {
    Console.WriteAsciiAlternating(value, (FigletFont) null, color);
  }

  public static void WriteAsciiAlternating(string value, FigletFont font, ColorAlternator color)
  {
    string concreteValue = ((FigletFont) Console.smethod_27(font)).ToAscii(value).ConcreteValue;
    char[] chArray = new char[1]{ '\n' };
    foreach (string str in concreteValue.Split(chArray))
      Console.WriteLineAlternating(str, color);
  }

  public static void WriteAsciiStyled(string value, StyleSheet alternator)
  {
    Console.WriteAsciiStyled(value, (FigletFont) null, alternator);
  }

  public static void WriteAsciiStyled(string value, FigletFont font, StyleSheet alternator)
  {
    Console.WriteLineStyled(((FigletFont) Console.smethod_27(font)).ToAscii(value), alternator);
  }

  public static void WriteWithGradient<T>(
    IEnumerable<T> value,
    Color styleSheet,
    [System.Runtime.InteropServices.In] Color obj2,
    [System.Runtime.InteropServices.In] int obj3)
  {
    Console.smethod_26<T>(new Action<object, Color>(Console.Write), value, styleSheet, obj2, obj3);
  }

  public static void WriteLineWithGradient<T>(
    [System.Runtime.InteropServices.In] IEnumerable<T> obj0,
    [System.Runtime.InteropServices.In] Color obj1,
    Color styleSheet,
    [System.Runtime.InteropServices.In] int obj3)
  {
    Console.smethod_26<T>(new Action<object, Color>(Console.WriteLine), obj0, obj1, styleSheet, obj3);
  }

  public static int Read() => System.Console.Read();

  public static ConsoleKeyInfo ReadKey() => System.Console.ReadKey();

  public static ConsoleKeyInfo ReadKey([System.Runtime.InteropServices.In] bool obj0)
  {
    return System.Console.ReadKey(obj0);
  }

  public static string ReadLine() => System.Console.ReadLine();

  public static void ResetColor() => System.Console.ResetColor();

  public static void SetBufferSize(int input, int startColor)
  {
    System.Console.SetBufferSize(input, startColor);
  }

  public static void SetCursorPosition([System.Runtime.InteropServices.In] int obj0, [System.Runtime.InteropServices.In] int obj1)
  {
    System.Console.SetCursorPosition(obj0, obj1);
  }

  public static void SetError(TextWriter intercept) => System.Console.SetError(intercept);

  public static void SetIn(TextReader width) => System.Console.SetIn(width);

  public static void SetOut([System.Runtime.InteropServices.In] TextWriter obj0)
  {
    System.Console.SetOut(obj0);
  }

  public static void SetWindowPosition(int left, int top) => System.Console.SetWindowPosition(left, top);

  public static void SetWindowSize(int newError, [System.Runtime.InteropServices.In] int obj1)
  {
    System.Console.SetWindowSize(newError, obj1);
  }

  public static Stream OpenStandardError() => System.Console.OpenStandardError();

  public static Stream OpenStandardError(int newOut) => System.Console.OpenStandardError(newOut);

  public static Stream OpenStandardInput() => System.Console.OpenStandardInput();

  public static Stream OpenStandardInput(int left) => System.Console.OpenStandardInput(left);

  public static Stream OpenStandardOutput() => System.Console.OpenStandardOutput();

  public static Stream OpenStandardOutput([System.Runtime.InteropServices.In] int obj0)
  {
    return System.Console.OpenStandardOutput(obj0);
  }

  public static void MoveBufferArea(
    int width,
    int height,
    [System.Runtime.InteropServices.In] int obj2,
    [System.Runtime.InteropServices.In] int obj3,
    [System.Runtime.InteropServices.In] int obj4,
    [System.Runtime.InteropServices.In] int obj5)
  {
    System.Console.MoveBufferArea(width, height, obj2, obj3, obj4, obj5);
  }

  public static void MoveBufferArea(
    [System.Runtime.InteropServices.In] int obj0,
    int sourceTop,
    int sourceWidth,
    int sourceHeight,
    int targetLeft,
    int targetTop,
    [System.Runtime.InteropServices.In] char obj6,
    [System.Runtime.InteropServices.In] ConsoleColor obj7,
    [System.Runtime.InteropServices.In] ConsoleColor obj8)
  {
    System.Console.MoveBufferArea(obj0, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, obj6, obj7, obj8);
  }
}
