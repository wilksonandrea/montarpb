using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Plugin.Core.Utility
{
	public static class ObjectCopier
	{
		private readonly static MethodInfo methodInfo_0;

		static ObjectCopier()
		{
			ObjectCopier.methodInfo_0 = typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		public static T Clone<T>(T Source)
		{
			T t;
			if (!typeof(T).IsSerializable)
			{
				throw new ArgumentException("The type must be serializable.", "source");
			}
			if (Source == null)
			{
				t = default(T);
				return t;
			}
			IFormatter binaryFormatter = new BinaryFormatter();
			Stream memoryStream = new MemoryStream();
			using (memoryStream)
			{
				binaryFormatter.Serialize(memoryStream, Source);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				t = (T)binaryFormatter.Deserialize(memoryStream);
			}
			return t;
		}

		public static object Copy(this object originalObject)
		{
			return ObjectCopier.smethod_0(originalObject, new Dictionary<object, object>(new REComparer()));
		}

		public static T Copy<T>(this T original)
		{
			return (T)original.Copy();
		}

		public static T DeepCopy<T>(T Source)
		{
			if (Source == null)
			{
				throw new ArgumentNullException("Object cannot be null");
			}
			return (T)ObjectCopier.smethod_3(Source);
		}

		public static bool IsPrimitive(this Type type)
		{
			if (type == typeof(string))
			{
				return true;
			}
			return type.IsValueType & type.IsPrimitive;
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
			object obj = ObjectCopier.methodInfo_0.Invoke(object_0, null);
			if (type.IsArray && !type.GetElementType().IsPrimitive())
			{
				Array arrays = (Array)obj;
				arrays.ForEach((Array array_1, int[] int_0) => array_1.SetValue(ObjectCopier.smethod_0(arrays.GetValue(int_0), idictionary_0), int_0));
			}
			idictionary_0.Add(object_0, obj);
			ObjectCopier.smethod_2(object_0, idictionary_0, obj, type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, null);
			ObjectCopier.smethod_1(object_0, idictionary_0, obj, type);
			return obj;
		}

		private static void smethod_1(object object_0, IDictionary<object, object> idictionary_0, object object_1, Type type_0)
		{
			if (type_0.BaseType != null)
			{
				ObjectCopier.smethod_1(object_0, idictionary_0, object_1, type_0.BaseType);
				ObjectCopier.smethod_2(object_0, idictionary_0, object_1, type_0.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, (FieldInfo fieldInfo_0) => fieldInfo_0.IsPrivate);
			}
		}

		private static void smethod_2(object object_0, IDictionary<object, object> idictionary_0, object object_1, Type type_0, BindingFlags bindingFlags_0 = 116, Func<FieldInfo, bool> func_0 = null)
		{
			FieldInfo[] fields = type_0.GetFields(bindingFlags_0);
			for (int i = 0; i < (int)fields.Length; i++)
			{
				FieldInfo fieldInfo = fields[i];
				if ((func_0 == null || func_0(fieldInfo)) && !fieldInfo.FieldType.IsPrimitive())
				{
					object obj = ObjectCopier.smethod_0(fieldInfo.GetValue(object_0), idictionary_0);
					fieldInfo.SetValue(object_1, obj);
				}
			}
		}

		private static object smethod_3(object object_0)
		{
			if (object_0 == null)
			{
				return null;
			}
			Type type = object_0.GetType();
			if (type.IsValueType || type == typeof(string))
			{
				return object_0;
			}
			if (type.IsArray)
			{
				Array object0 = object_0 as Array;
				Array arrays = Array.CreateInstance(Type.GetType(type.FullName.Replace("[]", string.Empty)), object0.Length);
				for (int i = 0; i < object0.Length; i++)
				{
					arrays.SetValue(ObjectCopier.smethod_3(object0.GetValue(i)), i);
				}
				return Convert.ChangeType(arrays, object_0.GetType());
			}
			if (!type.IsClass)
			{
				throw new ArgumentException("Unknown type");
			}
			object obj = Activator.CreateInstance(object_0.GetType());
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			for (int j = 0; j < (int)fields.Length; j++)
			{
				FieldInfo fieldInfo = fields[j];
				object value = fieldInfo.GetValue(object_0);
				if (value != null)
				{
					fieldInfo.SetValue(obj, ObjectCopier.smethod_3(value));
				}
			}
			return obj;
		}
	}
}