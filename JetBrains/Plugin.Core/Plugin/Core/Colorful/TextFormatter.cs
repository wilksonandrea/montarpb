// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.TextFormatter
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class TextFormatter
{
  private Color color_0;
  private TextPattern textPattern_0;
  private readonly string string_0;

  private List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> method_0(string taskGenerator)
  {
    List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> source = new List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>>();
    List<MatchLocation> matchLocationList = new List<MatchLocation>();
    foreach (StyleClass<TextPattern> style in ((TextAnnotator) this).styleSheet_0.Styles)
    {
      foreach (MatchLocation matchLocation in style.Target.GetMatchLocations(taskGenerator))
      {
        if (matchLocationList.Contains(matchLocation))
        {
          int index = matchLocationList.IndexOf(matchLocation);
          source.RemoveAt(index);
          matchLocationList.RemoveAt(index);
        }
        source.Add(new KeyValuePair<StyleClass<TextPattern>, MatchLocation>(style, matchLocation));
        matchLocationList.Add(matchLocation);
      }
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return source.OrderBy<KeyValuePair<StyleClass<TextPattern>, MatchLocation>, MatchLocation>(TextAnnotator.Class32.\u003C\u003E9__4_0 ?? (TextAnnotator.Class32.\u003C\u003E9__4_0 = new Func<KeyValuePair<StyleClass<TextPattern>, MatchLocation>, MatchLocation>(((TextPattern) TextAnnotator.Class32.\u003C\u003E9).method_0))).ToList<KeyValuePair<StyleClass<TextPattern>, MatchLocation>>();
  }

  private List<KeyValuePair<string, Color>> method_1(
    IEnumerable<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> stateMachine,
    [In] string obj1)
  {
    List<KeyValuePair<string, Color>> keyValuePairList = new List<KeyValuePair<string, Color>>();
    MatchLocation matchLocation1 = new MatchLocation(0, 0);
    int startIndex1 = 0;
    foreach (KeyValuePair<StyleClass<TextPattern>, MatchLocation> keyValuePair in stateMachine)
    {
      MatchLocation matchLocation2 = keyValuePair.Value;
      if (matchLocation1.End > matchLocation2.Beginning)
        matchLocation1 = new MatchLocation(0, 0);
      int end = matchLocation1.End;
      int beginning = matchLocation2.Beginning;
      int startIndex2 = beginning;
      startIndex1 = matchLocation2.End;
      string key1 = obj1.Substring(end, beginning - end);
      obj1.Substring(startIndex2, startIndex1 - startIndex2);
      string key2 = ((Styler.MatchFoundLite) ((TextAnnotator) this).dictionary_0[keyValuePair.Key])(obj1, keyValuePair.Value, obj1.Substring(startIndex2, startIndex1 - startIndex2));
      if (key1 != "")
        keyValuePairList.Add(new KeyValuePair<string, Color>(key1, ((TextAnnotator) this).styleSheet_0.UnstyledColor));
      if (key2 != "")
        keyValuePairList.Add(new KeyValuePair<string, Color>(key2, keyValuePair.Key.Color));
      matchLocation1 = matchLocation2.Prototype();
    }
    if (startIndex1 < obj1.Length)
    {
      string key = obj1.Substring(startIndex1, obj1.Length - startIndex1);
      keyValuePairList.Add(new KeyValuePair<string, Color>(key, ((TextAnnotator) this).styleSheet_0.UnstyledColor));
    }
    return keyValuePairList;
  }

  static TextFormatter()
  {
    // ISSUE: reference to a compiler-generated field
    TextAnnotator.Class32.\u003C\u003E9 = (TextAnnotator.Class32) new TextFormatter();
  }
}
