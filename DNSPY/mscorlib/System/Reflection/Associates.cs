using System;
using System.Collections.Generic;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005CE RID: 1486
	internal static class Associates
	{
		// Token: 0x060044BB RID: 17595 RVA: 0x000FCC0A File Offset: 0x000FAE0A
		internal static bool IncludeAccessor(MethodInfo associate, bool nonPublic)
		{
			return associate != null && (nonPublic || associate.IsPublic);
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x000FCC24 File Offset: 0x000FAE24
		[SecurityCritical]
		private static RuntimeMethodInfo AssignAssociates(int tkMethod, RuntimeType declaredType, RuntimeType reflectedType)
		{
			if (MetadataToken.IsNullToken(tkMethod))
			{
				return null;
			}
			bool flag = declaredType != reflectedType;
			IntPtr[] array = null;
			int num = 0;
			RuntimeType[] instantiationInternal = declaredType.GetTypeHandleInternal().GetInstantiationInternal();
			if (instantiationInternal != null)
			{
				num = instantiationInternal.Length;
				array = new IntPtr[instantiationInternal.Length];
				for (int i = 0; i < instantiationInternal.Length; i++)
				{
					array[i] = instantiationInternal[i].GetTypeHandleInternal().Value;
				}
			}
			RuntimeMethodHandleInternal runtimeMethodHandleInternal = ModuleHandle.ResolveMethodHandleInternalCore(RuntimeTypeHandle.GetModule(declaredType), tkMethod, array, num, null, 0);
			if (flag)
			{
				MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(runtimeMethodHandleInternal);
				if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && (attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private)
				{
					return null;
				}
				if ((attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)
				{
					bool flag2 = (RuntimeTypeHandle.GetAttributes(declaredType) & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic;
					if (flag2)
					{
						int slot = RuntimeMethodHandle.GetSlot(runtimeMethodHandleInternal);
						runtimeMethodHandleInternal = RuntimeTypeHandle.GetMethodAt(reflectedType, slot);
					}
				}
			}
			RuntimeMethodInfo runtimeMethodInfo = RuntimeType.GetMethodBase(reflectedType, runtimeMethodHandleInternal) as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				runtimeMethodInfo = reflectedType.Module.ResolveMethod(tkMethod, null, null) as RuntimeMethodInfo;
			}
			return runtimeMethodInfo;
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x000FCD1C File Offset: 0x000FAF1C
		[SecurityCritical]
		internal static void AssignAssociates(MetadataImport scope, int mdPropEvent, RuntimeType declaringType, RuntimeType reflectedType, out RuntimeMethodInfo addOn, out RuntimeMethodInfo removeOn, out RuntimeMethodInfo fireOn, out RuntimeMethodInfo getter, out RuntimeMethodInfo setter, out MethodInfo[] other, out bool composedOfAllPrivateMethods, out BindingFlags bindingFlags)
		{
			RuntimeMethodInfo runtimeMethodInfo;
			setter = (runtimeMethodInfo = null);
			getter = (runtimeMethodInfo = runtimeMethodInfo);
			fireOn = (runtimeMethodInfo = runtimeMethodInfo);
			removeOn = (runtimeMethodInfo = runtimeMethodInfo);
			addOn = runtimeMethodInfo;
			Associates.Attributes attributes = Associates.Attributes.ComposedOfAllVirtualMethods | Associates.Attributes.ComposedOfAllPrivateMethods | Associates.Attributes.ComposedOfNoPublicMembers | Associates.Attributes.ComposedOfNoStaticMembers;
			while (RuntimeTypeHandle.IsGenericVariable(reflectedType))
			{
				reflectedType = (RuntimeType)reflectedType.BaseType;
			}
			bool flag = declaringType != reflectedType;
			List<MethodInfo> list = null;
			MetadataEnumResult metadataEnumResult;
			scope.Enum(MetadataTokenType.MethodDef, mdPropEvent, out metadataEnumResult);
			int num = metadataEnumResult.Length / 2;
			for (int i = 0; i < num; i++)
			{
				int num2 = metadataEnumResult[i * 2];
				MethodSemanticsAttributes methodSemanticsAttributes = (MethodSemanticsAttributes)metadataEnumResult[i * 2 + 1];
				RuntimeMethodInfo runtimeMethodInfo2 = Associates.AssignAssociates(num2, declaringType, reflectedType);
				if (!(runtimeMethodInfo2 == null))
				{
					MethodAttributes attributes2 = runtimeMethodInfo2.Attributes;
					bool flag2 = (attributes2 & MethodAttributes.MemberAccessMask) == MethodAttributes.Private;
					bool flag3 = (attributes2 & MethodAttributes.Virtual) > MethodAttributes.PrivateScope;
					MethodAttributes methodAttributes = attributes2 & MethodAttributes.MemberAccessMask;
					bool flag4 = methodAttributes == MethodAttributes.Public;
					bool flag5 = (attributes2 & MethodAttributes.Static) > MethodAttributes.PrivateScope;
					if (flag4)
					{
						attributes &= ~Associates.Attributes.ComposedOfNoPublicMembers;
						attributes &= ~Associates.Attributes.ComposedOfAllPrivateMethods;
					}
					else if (!flag2)
					{
						attributes &= ~Associates.Attributes.ComposedOfAllPrivateMethods;
					}
					if (flag5)
					{
						attributes &= ~Associates.Attributes.ComposedOfNoStaticMembers;
					}
					if (!flag3)
					{
						attributes &= ~Associates.Attributes.ComposedOfAllVirtualMethods;
					}
					if (methodSemanticsAttributes == MethodSemanticsAttributes.Setter)
					{
						setter = runtimeMethodInfo2;
					}
					else if (methodSemanticsAttributes == MethodSemanticsAttributes.Getter)
					{
						getter = runtimeMethodInfo2;
					}
					else if (methodSemanticsAttributes == MethodSemanticsAttributes.Fire)
					{
						fireOn = runtimeMethodInfo2;
					}
					else if (methodSemanticsAttributes == MethodSemanticsAttributes.AddOn)
					{
						addOn = runtimeMethodInfo2;
					}
					else if (methodSemanticsAttributes == MethodSemanticsAttributes.RemoveOn)
					{
						removeOn = runtimeMethodInfo2;
					}
					else
					{
						if (list == null)
						{
							list = new List<MethodInfo>(num);
						}
						list.Add(runtimeMethodInfo2);
					}
				}
			}
			bool flag6 = (attributes & Associates.Attributes.ComposedOfNoPublicMembers) == (Associates.Attributes)0;
			bool flag7 = (attributes & Associates.Attributes.ComposedOfNoStaticMembers) == (Associates.Attributes)0;
			bindingFlags = RuntimeType.FilterPreCalculate(flag6, flag, flag7);
			composedOfAllPrivateMethods = (attributes & Associates.Attributes.ComposedOfAllPrivateMethods) > (Associates.Attributes)0;
			other = ((list != null) ? list.ToArray() : null);
		}

		// Token: 0x02000C3B RID: 3131
		[Flags]
		internal enum Attributes
		{
			// Token: 0x0400373E RID: 14142
			ComposedOfAllVirtualMethods = 1,
			// Token: 0x0400373F RID: 14143
			ComposedOfAllPrivateMethods = 2,
			// Token: 0x04003740 RID: 14144
			ComposedOfNoPublicMembers = 4,
			// Token: 0x04003741 RID: 14145
			ComposedOfNoStaticMembers = 8
		}
	}
}
