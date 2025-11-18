// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Utility.ObjectCopier
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Utility;

public static class ObjectCopier
{
  private static readonly MethodInfo methodInfo_0;

  public ObjectCopier()
  {
  }

  public ObjectCopier()
  {
    ((DBQuery) this).list_0 = new List<string>();
    ((DBQuery) this).list_1 = new List<object>();
  }

  public void AddQuery([In] string obj0, [In] object obj1)
  {
    ((DBQuery) this).list_0.Add(obj0);
    ((DBQuery) this).list_1.Add(obj1);
  }

  public string[] GetTables() => ((DBQuery) this).list_0.ToArray();

  public object[] GetValues() => ((DBQuery) this).list_1.ToArray();

  public static bool IsPrimitive(this Type itemsModel_0)
  {
    return itemsModel_0 == typeof (string) || itemsModel_0.IsValueType & itemsModel_0.IsPrimitive;
  }

  public static object Copy(this object Format)
  {
    return ObjectCopier.smethod_0(Format, (IDictionary<object, object>) new Dictionary<object, object>((IEqualityComparer<object>) new SafeList<>()));
  }

  private static object smethod_0(object Now, [In] IDictionary<object, object> obj1)
  {
    // ISSUE: variable of a compiler-generated type
    ObjectCopier.Class7 class7 = (ObjectCopier.Class7) new ArrayEXT();
    // ISSUE: reference to a compiler-generated field
    class7.idictionary_0 = obj1;
    if (Now == null)
      return (object) null;
    Type type = Now.GetType();
    if (type.IsPrimitive())
      return Now;
    // ISSUE: reference to a compiler-generated field
    if (class7.idictionary_0.ContainsKey(Now))
    {
      // ISSUE: reference to a compiler-generated field
      return class7.idictionary_0[Now];
    }
    if (typeof (Delegate).IsAssignableFrom(type))
      return (object) null;
    object object_1 = ObjectCopier.methodInfo_0.Invoke(Now, (object[]) null);
    if (type.IsArray && !type.GetElementType().IsPrimitive())
    {
      // ISSUE: reference to a compiler-generated field
      class7.array_0 = (Array) object_1;
      // ISSUE: reference to a compiler-generated field
      class7.array_0.ForEach(new Action<Array, int[]>(((Class8) class7).method_0));
    }
    // ISSUE: reference to a compiler-generated field
    class7.idictionary_0.Add(Now, object_1);
    // ISSUE: reference to a compiler-generated field
    ObjectCopier.smethod_2(Now, class7.idictionary_0, object_1, type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, (Func<FieldInfo, bool>) null);
    // ISSUE: reference to a compiler-generated field
    ObjectCopier.smethod_1(Now, class7.idictionary_0, object_1, type);
    return object_1;
  }

  private static void smethod_1(
    [In] object obj0,
    IDictionary<object, object> value,
    [In] object obj2,
    [In] Type obj3)
  {
    if (!(obj3.BaseType != (Type) null))
      return;
    ObjectCopier.smethod_1(obj0, value, obj2, obj3.BaseType);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ObjectCopier.smethod_2(obj0, value, obj2, obj3.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, ObjectCopier.Class6.\u003C\u003E9__4_0 ?? (ObjectCopier.Class6.\u003C\u003E9__4_0 = new Func<FieldInfo, bool>(((REComparer) ObjectCopier.Class6.\u003C\u003E9).method_0)));
  }

  private static void smethod_2(
    [In] object obj0,
    IDictionary<object, object> idictionary_0,
    object object_1,
    Type type_0,
    [In] BindingFlags obj4,
    [In] Func<FieldInfo, bool> obj5)
  {
    foreach (FieldInfo field in type_0.GetFields(obj4))
    {
      if ((obj5 == null || obj5(field)) && !field.FieldType.IsPrimitive())
      {
        object obj = ObjectCopier.smethod_0(field.GetValue(obj0), idictionary_0);
        field.SetValue(object_1, obj);
      }
    }
  }
}
