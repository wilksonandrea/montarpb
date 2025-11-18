using System;
using System.Reflection;
using System.Security;
using System.StubHelpers;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A0E RID: 2574
	internal static class ICustomPropertyProviderImpl
	{
		// Token: 0x0600658A RID: 25994 RVA: 0x001592FC File Offset: 0x001574FC
		internal static ICustomProperty CreateProperty(object target, string propertyName)
		{
			PropertyInfo property = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			if (property == null)
			{
				return null;
			}
			return new CustomPropertyImpl(property);
		}

		// Token: 0x0600658B RID: 25995 RVA: 0x0015932C File Offset: 0x0015752C
		[SecurityCritical]
		internal unsafe static ICustomProperty CreateIndexedProperty(object target, string propertyName, TypeNameNative* pIndexedParamType)
		{
			Type type = null;
			SystemTypeMarshaler.ConvertToManaged(pIndexedParamType, ref type);
			return ICustomPropertyProviderImpl.CreateIndexedProperty(target, propertyName, type);
		}

		// Token: 0x0600658C RID: 25996 RVA: 0x0015934C File Offset: 0x0015754C
		internal static ICustomProperty CreateIndexedProperty(object target, string propertyName, Type indexedParamType)
		{
			PropertyInfo property = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, new Type[] { indexedParamType }, null);
			if (property == null)
			{
				return null;
			}
			return new CustomPropertyImpl(property);
		}

		// Token: 0x0600658D RID: 25997 RVA: 0x00159386 File Offset: 0x00157586
		[SecurityCritical]
		internal unsafe static void GetType(object target, TypeNameNative* pIndexedParamType)
		{
			SystemTypeMarshaler.ConvertToNative(target.GetType(), pIndexedParamType);
		}
	}
}
