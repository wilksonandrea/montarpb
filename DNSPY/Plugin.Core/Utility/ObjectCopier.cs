using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Plugin.Core.Utility
{
	// Token: 0x02000031 RID: 49
	public static class ObjectCopier
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00002B77 File Offset: 0x00000D77
		public static bool IsPrimitive(this Type type)
		{
			return type == typeof(string) || (type.IsValueType & type.IsPrimitive);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00002B9A File Offset: 0x00000D9A
		public static object Copy(this object originalObject)
		{
			return ObjectCopier.smethod_0(originalObject, new Dictionary<object, object>(new REComparer()));
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000187D8 File Offset: 0x000169D8
		private static object smethod_0(object object_0, IDictionary<object, object> idictionary_0)
		{
			ObjectCopier.Class7 @class = new ObjectCopier.Class7();
			@class.idictionary_0 = idictionary_0;
			if (object_0 == null)
			{
				return null;
			}
			Type type = object_0.GetType();
			if (type.IsPrimitive())
			{
				return object_0;
			}
			if (@class.idictionary_0.ContainsKey(object_0))
			{
				return @class.idictionary_0[object_0];
			}
			if (typeof(Delegate).IsAssignableFrom(type))
			{
				return null;
			}
			object obj = ObjectCopier.methodInfo_0.Invoke(object_0, null);
			if (type.IsArray && !type.GetElementType().IsPrimitive())
			{
				@class.array_0 = (Array)obj;
				@class.array_0.ForEach(new Action<Array, int[]>(@class.method_0));
			}
			@class.idictionary_0.Add(object_0, obj);
			ObjectCopier.smethod_2(object_0, @class.idictionary_0, obj, type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, null);
			ObjectCopier.smethod_1(object_0, @class.idictionary_0, obj, type);
			return obj;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000188AC File Offset: 0x00016AAC
		private static void smethod_1(object object_0, IDictionary<object, object> idictionary_0, object object_1, Type type_0)
		{
			if (type_0.BaseType != null)
			{
				ObjectCopier.smethod_1(object_0, idictionary_0, object_1, type_0.BaseType);
				ObjectCopier.smethod_2(object_0, idictionary_0, object_1, type_0.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, new Func<FieldInfo, bool>(ObjectCopier.Class6.<>9.method_0));
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00018904 File Offset: 0x00016B04
		private static void smethod_2(object object_0, IDictionary<object, object> idictionary_0, object object_1, Type type_0, BindingFlags bindingFlags_0 = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> func_0 = null)
		{
			foreach (FieldInfo fieldInfo in type_0.GetFields(bindingFlags_0))
			{
				if ((func_0 == null || func_0(fieldInfo)) && !fieldInfo.FieldType.IsPrimitive())
				{
					object obj = ObjectCopier.smethod_0(fieldInfo.GetValue(object_0), idictionary_0);
					fieldInfo.SetValue(object_1, obj);
				}
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00002BAC File Offset: 0x00000DAC
		public static T Copy<T>(this T original)
		{
			return (T)((object)original.Copy());
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00018960 File Offset: 0x00016B60
		public static T Clone<T>(T Source)
		{
			if (!typeof(T).IsSerializable)
			{
				throw new ArgumentException("The type must be serializable.", "source");
			}
			T t;
			if (Source == null)
			{
				t = default(T);
				return t;
			}
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new MemoryStream();
			using (stream)
			{
				formatter.Serialize(stream, Source);
				stream.Seek(0L, SeekOrigin.Begin);
				t = (T)((object)formatter.Deserialize(stream));
			}
			return t;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000189F8 File Offset: 0x00016BF8
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
				Type type2 = Type.GetType(type.FullName.Replace("[]", string.Empty));
				Array array = object_0 as Array;
				Array array2 = Array.CreateInstance(type2, array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					array2.SetValue(ObjectCopier.smethod_3(array.GetValue(i)), i);
				}
				return Convert.ChangeType(array2, object_0.GetType());
			}
			if (type.IsClass)
			{
				object obj = Activator.CreateInstance(object_0.GetType());
				foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					object value = fieldInfo.GetValue(object_0);
					if (value != null)
					{
						fieldInfo.SetValue(obj, ObjectCopier.smethod_3(value));
					}
				}
				return obj;
			}
			throw new ArgumentException("Unknown type");
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00002BBE File Offset: 0x00000DBE
		public static T DeepCopy<T>(T Source)
		{
			if (Source == null)
			{
				throw new ArgumentNullException("Object cannot be null");
			}
			return (T)((object)ObjectCopier.smethod_3(Source));
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00002BE3 File Offset: 0x00000DE3
		// Note: this type is marked as 'beforefieldinit'.
		static ObjectCopier()
		{
		}

		// Token: 0x04000093 RID: 147
		private static readonly MethodInfo methodInfo_0 = typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x02000032 RID: 50
		[CompilerGenerated]
		[Serializable]
		private sealed class Class6
		{
			// Token: 0x060001DA RID: 474 RVA: 0x00002C00 File Offset: 0x00000E00
			// Note: this type is marked as 'beforefieldinit'.
			static Class6()
			{
			}

			// Token: 0x060001DB RID: 475 RVA: 0x00002116 File Offset: 0x00000316
			public Class6()
			{
			}

			// Token: 0x060001DC RID: 476 RVA: 0x00002C0C File Offset: 0x00000E0C
			internal bool method_0(FieldInfo fieldInfo_0)
			{
				return fieldInfo_0.IsPrivate;
			}

			// Token: 0x04000094 RID: 148
			public static readonly ObjectCopier.Class6 <>9 = new ObjectCopier.Class6();

			// Token: 0x04000095 RID: 149
			public static Func<FieldInfo, bool> <>9__4_0;
		}

		// Token: 0x02000033 RID: 51
		[CompilerGenerated]
		private sealed class Class7
		{
			// Token: 0x060001DD RID: 477 RVA: 0x00002116 File Offset: 0x00000316
			public Class7()
			{
			}

			// Token: 0x060001DE RID: 478 RVA: 0x00002C14 File Offset: 0x00000E14
			internal void method_0(Array array_1, int[] int_0)
			{
				array_1.SetValue(ObjectCopier.smethod_0(this.array_0.GetValue(int_0), this.idictionary_0), int_0);
			}

			// Token: 0x04000096 RID: 150
			public IDictionary<object, object> idictionary_0;

			// Token: 0x04000097 RID: 151
			public Array array_0;
		}
	}
}
