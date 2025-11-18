// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Utility.ColorUtil
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Models;
using Plugin.Core.XML;
using System.Drawing;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Utility;

public class ColorUtil
{
  public static Color White;
  public static Color Black;
  public static Color Red;
  public static Color Green;
  public static Color Blue;
  public static Color Yellow;
  public static Color Fuchsia;
  public static Color Cyan;
  public static Color Silver;
  public static Color LightGrey;

  public static void Get2Titles(
    int string_0,
    int int_0,
    [In] ref TitleModel obj2,
    [In] ref TitleModel obj3,
    [In] bool obj4)
  {
    if (!obj4)
    {
      obj2 = (TitleModel) new TRuleModel();
      obj3 = (TitleModel) new TRuleModel();
    }
    else
    {
      obj2 = (TitleModel) null;
      obj3 = (TitleModel) null;
    }
    if (string_0 == 0 && int_0 == 0)
      return;
    foreach (TitleModel titleModel in TitleSystemXML.list_0)
    {
      if (titleModel.Id == string_0)
        obj2 = titleModel;
      else if (titleModel.Id == int_0)
        obj3 = titleModel;
    }
  }

  public static void Get3Titles(
    int titleId1,
    int titleId2,
    [Out] int title1,
    out TitleModel title2,
    ref TitleModel ReturnNull = true,
    [In] ref TitleModel obj5,
    [In] bool obj6)
  {
    if (!obj6)
    {
      title2 = (TitleModel) new TRuleModel();
      ReturnNull = (TitleModel) new TRuleModel();
      obj5 = (TitleModel) new TRuleModel();
    }
    else
    {
      title2 = (TitleModel) null;
      ReturnNull = (TitleModel) null;
      obj5 = (TitleModel) null;
    }
    if (titleId1 == 0 && titleId2 == 0 && title1 == 0)
      return;
    foreach (TitleModel titleModel in TitleSystemXML.list_0)
    {
      if (titleModel.Id == titleId1)
        title2 = titleModel;
      else if (titleModel.Id == titleId2)
        ReturnNull = titleModel;
      else if (titleModel.Id == title1)
        obj5 = titleModel;
    }
  }
}
