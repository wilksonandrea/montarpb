using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Plugin.Core.Utility;

public static class ObjectCopier
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class6
	{
		public static readonly Class6 _003C_003E9 = new Class6();

		public static Func<FieldInfo, bool> _003C_003E9__4_0;

		internal bool method_0(FieldInfo fieldInfo_0)
		{
			return fieldInfo_0.IsPrivate;
		}
	}

	[CompilerGenerated]
	private sealed class Class7
	{
		public IDictionary<object, object> idictionary_0;

		public Array array_0;

		internal void method_0(Array array_1, int[] int_0)
		{
			array_1.SetValue(smethod_0(array_0.GetValue(int_0), idictionary_0), int_0);
		}
	}

	private static readonly MethodInfo methodInfo_0 = typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);

	public static bool IsPrimitive(this Type type)
	{
		if (type == typeof(string))
		{
			return true;
		}
		return type.IsValueType & type.IsPrimitive;
	}

	public static object Copy(this object originalObject)
	{
		return smethod_0(originalObject, new Dictionary<object, object>(new REComparer()));
	}

	private static object smethod_0(object object_0, IDictionary<object, object> idictionary_0)
	{
		if (object_0 == null)
		{
			return null;
		}
		Type type = object_0.GetType();
		if (type.IsPrimitive())
		{
			return object_0;
		}
		if (idictionary_0.ContainsKey(object_0))
		{
			return idictionary_0[object_0];
		}
		if (typeof(Delegate).IsAssignableFrom(type))
		{
			return null;
		}
		object obj = methodInfo_0.Invoke(object_0, null);
		if (type.IsArray && !type.GetElementType().IsPrimitive())
		{
			Array array_2 = (Array)obj;
			array_2.ForEach(delegate(Array array_1, int[] int_0)
			{
				array_1.SetValue(smethod_0(array_2.GetValue(int_0), idictionary_0), int_0);
			});
		}
		idictionary_0.Add(object_0, obj);
		smethod_2(object_0, idictionary_0, obj, type);
		smethod_1(object_0, idictionary_0, obj, type);
		return obj;
	}

	private static void smethod_1(object object_0, IDictionary<object, object> idictionary_0, object object_1, Type type_0)
	{
		if (type_0.BaseType != null)
		{
			smethod_1(object_0, idictionary_0, object_1, type_0.BaseType);
			smethod_2(object_0, idictionary_0, object_1, type_0.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, (FieldInfo fieldInfo_0) => fieldInfo_0.IsPrivate);
		}
	}

	private static void smethod_2(object object_0, IDictionary<object, object> idictionary_0, object object_1, Type type_0, BindingFlags bindingFlags_0 = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> func_0 = null)
	{
		FieldInfo[] fields = type_0.GetFields(bindingFlags_0);
		foreach (FieldInfo fieldInfo in fields)
		{
			if ((func_0 == null || func_0(fieldInfo)) && !fieldInfo.FieldType.IsPrimitive())
			{
				object value = smethod_0(fieldInfo.GetValue(object_0), idictionary_0);
				fieldInfo.SetValue(object_1, value);
			}
		}
	}

	public static T Copy<T>(this T original)
	{
		return (T)((object)original).Copy();
	}

	public static T Clone<T>(T Source)
	{
		if (!typeof(T).IsSerializable)
		{
			throw new ArgumentException("The type must be serializable.", "source");
		}
		if (Source == null)
		{
			return default(T);
		}
		IFormatter formatter = new BinaryFormatter();
		Stream stream = new MemoryStream();
		using (stream)
		{
			formatter.Serialize(stream, Source);
			stream.Seek(0L, SeekOrigin.Begin);
			return (T)formatter.Deserialize(stream);
		}
	}

	private static object smethod_3(object object_0)
	{
		if (object_0 == null)
		{
			return null;
		}
		Type type = object_0.GetType();
		if (!type.IsValueType && !(type == typeof(string)))
		{
			if (type.IsArray)
			{
				Type type2 = Type.GetType(type.FullName.Replace("[]", string.Empty));
				Array array = object_0 as Array;
				Array array2 = Array.CreateInstance(type2, array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					array2.SetValue(smethod_3(array.GetValue(i)), i);
				}
				return Convert.ChangeType(array2, object_0.GetType());
			}
			if (type.IsClass)
			{
				object obj = Activator.CreateInstance(object_0.GetType());
				FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (FieldInfo fieldInfo in fields)
				{
					object value = fieldInfo.GetValue(object_0);
					if (value != null)
					{
						fieldInfo.SetValue(obj, smethod_3(value));
					}
				}
				return obj;
			}
			throw new ArgumentException("Unknown type");
		}
		return object_0;
	}

	public static T DeepCopy<T>(T Source)
	{
		if (Source == null)
		{
			throw new ArgumentNullException("Object cannot be null");
		}
		return (T)smethod_3(Source);
	}
}
