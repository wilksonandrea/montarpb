using System;
using System.Collections.Generic;

namespace System.Reflection
{
	// Token: 0x020005EF RID: 1519
	[__DynamicallyInvokable]
	public static class RuntimeReflectionExtensions
	{
		// Token: 0x0600465A RID: 18010 RVA: 0x0010234B File Offset: 0x0010054B
		private static void CheckAndThrow(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(t is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x00102379 File Offset: 0x00100579
		private static void CheckAndThrow(MethodInfo m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("method");
			}
			if (!(m is RuntimeMethodInfo))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
			}
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x001023A7 File Offset: 0x001005A7
		[__DynamicallyInvokable]
		public static IEnumerable<PropertyInfo> GetRuntimeProperties(this Type type)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x001023B7 File Offset: 0x001005B7
		[__DynamicallyInvokable]
		public static IEnumerable<EventInfo> GetRuntimeEvents(this Type type)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x001023C7 File Offset: 0x001005C7
		[__DynamicallyInvokable]
		public static IEnumerable<MethodInfo> GetRuntimeMethods(this Type type)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x001023D7 File Offset: 0x001005D7
		[__DynamicallyInvokable]
		public static IEnumerable<FieldInfo> GetRuntimeFields(this Type type)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x001023E7 File Offset: 0x001005E7
		[__DynamicallyInvokable]
		public static PropertyInfo GetRuntimeProperty(this Type type, string name)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetProperty(name);
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x001023F6 File Offset: 0x001005F6
		[__DynamicallyInvokable]
		public static EventInfo GetRuntimeEvent(this Type type, string name)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetEvent(name);
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x00102405 File Offset: 0x00100605
		[__DynamicallyInvokable]
		public static MethodInfo GetRuntimeMethod(this Type type, string name, Type[] parameters)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetMethod(name, parameters);
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x00102415 File Offset: 0x00100615
		[__DynamicallyInvokable]
		public static FieldInfo GetRuntimeField(this Type type, string name)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetField(name);
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x00102424 File Offset: 0x00100624
		[__DynamicallyInvokable]
		public static MethodInfo GetRuntimeBaseDefinition(this MethodInfo method)
		{
			RuntimeReflectionExtensions.CheckAndThrow(method);
			return method.GetBaseDefinition();
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x00102432 File Offset: 0x00100632
		[__DynamicallyInvokable]
		public static InterfaceMapping GetRuntimeInterfaceMap(this TypeInfo typeInfo, Type interfaceType)
		{
			if (typeInfo == null)
			{
				throw new ArgumentNullException("typeInfo");
			}
			if (!(typeInfo is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			return typeInfo.GetInterfaceMap(interfaceType);
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x00102467 File Offset: 0x00100667
		[__DynamicallyInvokable]
		public static MethodInfo GetMethodInfo(this Delegate del)
		{
			if (del == null)
			{
				throw new ArgumentNullException("del");
			}
			return del.Method;
		}

		// Token: 0x04001CD6 RID: 7382
		private const BindingFlags everything = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
