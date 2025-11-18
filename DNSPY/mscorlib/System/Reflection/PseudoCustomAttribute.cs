using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020005E1 RID: 1505
	internal static class PseudoCustomAttribute
	{
		// Token: 0x060045A2 RID: 17826
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSecurityAttributes(RuntimeModule module, int token, bool assembly, out object[] securityAttributes);

		// Token: 0x060045A3 RID: 17827 RVA: 0x00100420 File Offset: 0x000FE620
		[SecurityCritical]
		internal static void GetSecurityAttributes(RuntimeModule module, int token, bool assembly, out object[] securityAttributes)
		{
			PseudoCustomAttribute._GetSecurityAttributes(module.GetNativeHandle(), token, assembly, out securityAttributes);
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x00100430 File Offset: 0x000FE630
		[SecurityCritical]
		static PseudoCustomAttribute()
		{
			RuntimeType[] array = new RuntimeType[]
			{
				typeof(FieldOffsetAttribute) as RuntimeType,
				typeof(SerializableAttribute) as RuntimeType,
				typeof(MarshalAsAttribute) as RuntimeType,
				typeof(ComImportAttribute) as RuntimeType,
				typeof(NonSerializedAttribute) as RuntimeType,
				typeof(InAttribute) as RuntimeType,
				typeof(OutAttribute) as RuntimeType,
				typeof(OptionalAttribute) as RuntimeType,
				typeof(DllImportAttribute) as RuntimeType,
				typeof(PreserveSigAttribute) as RuntimeType,
				typeof(TypeForwardedToAttribute) as RuntimeType
			};
			PseudoCustomAttribute.s_pcasCount = array.Length;
			Dictionary<RuntimeType, RuntimeType> dictionary = new Dictionary<RuntimeType, RuntimeType>(PseudoCustomAttribute.s_pcasCount);
			for (int i = 0; i < PseudoCustomAttribute.s_pcasCount; i++)
			{
				dictionary[array[i]] = array[i];
			}
			PseudoCustomAttribute.s_pca = dictionary;
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x00100544 File Offset: 0x000FE744
		[SecurityCritical]
		[Conditional("_DEBUG")]
		private static void VerifyPseudoCustomAttribute(RuntimeType pca)
		{
			AttributeUsageAttribute attributeUsage = CustomAttribute.GetAttributeUsage(pca);
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x00100558 File Offset: 0x000FE758
		internal static bool IsSecurityAttribute(RuntimeType type)
		{
			return type == (RuntimeType)typeof(SecurityAttribute) || type.IsSubclassOf(typeof(SecurityAttribute));
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x00100584 File Offset: 0x000FE784
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeType type, RuntimeType caType, bool includeSecCa, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
			{
				return new Attribute[0];
			}
			List<Attribute> list = new List<Attribute>();
			if (flag || caType == (RuntimeType)typeof(SerializableAttribute))
			{
				Attribute attribute = SerializableAttribute.GetCustomAttribute(type);
				if (attribute != null)
				{
					list.Add(attribute);
				}
			}
			if (flag || caType == (RuntimeType)typeof(ComImportAttribute))
			{
				Attribute attribute = ComImportAttribute.GetCustomAttribute(type);
				if (attribute != null)
				{
					list.Add(attribute);
				}
			}
			if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && !type.IsGenericParameter && type.GetElementType() == null)
			{
				if (type.IsGenericType)
				{
					type = (RuntimeType)type.GetGenericTypeDefinition();
				}
				object[] array;
				PseudoCustomAttribute.GetSecurityAttributes(type.Module.ModuleHandle.GetRuntimeModule(), type.MetadataToken, false, out array);
				if (array != null)
				{
					foreach (object obj in array)
					{
						if (caType == obj.GetType() || obj.GetType().IsSubclassOf(caType))
						{
							list.Add((Attribute)obj);
						}
					}
				}
			}
			count = list.Count;
			return list.ToArray();
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x00100708 File Offset: 0x000FE908
		[SecurityCritical]
		internal static bool IsDefined(RuntimeType type, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			int num;
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null) || PseudoCustomAttribute.IsSecurityAttribute(caType)) && (((flag || caType == (RuntimeType)typeof(SerializableAttribute)) && SerializableAttribute.IsDefined(type)) || ((flag || caType == (RuntimeType)typeof(ComImportAttribute)) && ComImportAttribute.IsDefined(type)) || ((flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && PseudoCustomAttribute.GetCustomAttributes(type, caType, true, out num).Length != 0));
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x001007C8 File Offset: 0x000FE9C8
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeMethodInfo method, RuntimeType caType, bool includeSecCa, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
			{
				return new Attribute[0];
			}
			List<Attribute> list = new List<Attribute>();
			if (flag || caType == (RuntimeType)typeof(DllImportAttribute))
			{
				Attribute attribute = DllImportAttribute.GetCustomAttribute(method);
				if (attribute != null)
				{
					list.Add(attribute);
				}
			}
			if (flag || caType == (RuntimeType)typeof(PreserveSigAttribute))
			{
				Attribute attribute = PreserveSigAttribute.GetCustomAttribute(method);
				if (attribute != null)
				{
					list.Add(attribute);
				}
			}
			if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)))
			{
				object[] array;
				PseudoCustomAttribute.GetSecurityAttributes(method.Module.ModuleHandle.GetRuntimeModule(), method.MetadataToken, false, out array);
				if (array != null)
				{
					foreach (object obj in array)
					{
						if (caType == obj.GetType() || obj.GetType().IsSubclassOf(caType))
						{
							list.Add((Attribute)obj);
						}
					}
				}
			}
			count = list.Count;
			return list.ToArray();
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x00100914 File Offset: 0x000FEB14
		[SecurityCritical]
		internal static bool IsDefined(RuntimeMethodInfo method, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			int num;
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)) && (((flag || caType == (RuntimeType)typeof(DllImportAttribute)) && DllImportAttribute.IsDefined(method)) || ((flag || caType == (RuntimeType)typeof(PreserveSigAttribute)) && PreserveSigAttribute.IsDefined(method)) || ((flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && PseudoCustomAttribute.GetCustomAttributes(method, caType, true, out num).Length != 0));
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x001009CC File Offset: 0x000FEBCC
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeParameterInfo parameter, RuntimeType caType, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)
			{
				return null;
			}
			Attribute[] array = new Attribute[PseudoCustomAttribute.s_pcasCount];
			if (flag || caType == (RuntimeType)typeof(InAttribute))
			{
				Attribute attribute = InAttribute.GetCustomAttribute(parameter);
				if (attribute != null)
				{
					Attribute[] array2 = array;
					int num = count;
					count = num + 1;
					array2[num] = attribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(OutAttribute))
			{
				Attribute attribute = OutAttribute.GetCustomAttribute(parameter);
				if (attribute != null)
				{
					Attribute[] array3 = array;
					int num = count;
					count = num + 1;
					array3[num] = attribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(OptionalAttribute))
			{
				Attribute attribute = OptionalAttribute.GetCustomAttribute(parameter);
				if (attribute != null)
				{
					Attribute[] array4 = array;
					int num = count;
					count = num + 1;
					array4[num] = attribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(MarshalAsAttribute))
			{
				Attribute attribute = MarshalAsAttribute.GetCustomAttribute(parameter);
				if (attribute != null)
				{
					Attribute[] array5 = array;
					int num = count;
					count = num + 1;
					array5[num] = attribute;
				}
			}
			return array;
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x00100AF4 File Offset: 0x000FECF4
		[SecurityCritical]
		internal static bool IsDefined(RuntimeParameterInfo parameter, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)) && (((flag || caType == (RuntimeType)typeof(InAttribute)) && InAttribute.IsDefined(parameter)) || ((flag || caType == (RuntimeType)typeof(OutAttribute)) && OutAttribute.IsDefined(parameter)) || ((flag || caType == (RuntimeType)typeof(OptionalAttribute)) && OptionalAttribute.IsDefined(parameter)) || ((flag || caType == (RuntimeType)typeof(MarshalAsAttribute)) && MarshalAsAttribute.IsDefined(parameter)));
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x00100BDC File Offset: 0x000FEDDC
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeAssembly assembly, RuntimeType caType, bool includeSecCa, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
			{
				return new Attribute[0];
			}
			List<Attribute> list = new List<Attribute>();
			if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)))
			{
				object[] array;
				PseudoCustomAttribute.GetSecurityAttributes(assembly.ManifestModule.ModuleHandle.GetRuntimeModule(), RuntimeAssembly.GetToken(assembly.GetNativeHandle()), true, out array);
				if (array != null)
				{
					foreach (object obj in array)
					{
						if (caType == obj.GetType() || obj.GetType().IsSubclassOf(caType))
						{
							list.Add((Attribute)obj);
						}
					}
				}
			}
			count = list.Count;
			return list.ToArray();
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x00100CD4 File Offset: 0x000FEED4
		[SecurityCritical]
		internal static bool IsDefined(RuntimeAssembly assembly, RuntimeType caType)
		{
			int num;
			return PseudoCustomAttribute.GetCustomAttributes(assembly, caType, true, out num).Length != 0;
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x00100CEF File Offset: 0x000FEEEF
		internal static Attribute[] GetCustomAttributes(RuntimeModule module, RuntimeType caType, out int count)
		{
			count = 0;
			return null;
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x00100CF5 File Offset: 0x000FEEF5
		internal static bool IsDefined(RuntimeModule module, RuntimeType caType)
		{
			return false;
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x00100CF8 File Offset: 0x000FEEF8
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeFieldInfo field, RuntimeType caType, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)
			{
				return null;
			}
			Attribute[] array = new Attribute[PseudoCustomAttribute.s_pcasCount];
			if (flag || caType == (RuntimeType)typeof(MarshalAsAttribute))
			{
				Attribute attribute = MarshalAsAttribute.GetCustomAttribute(field);
				if (attribute != null)
				{
					Attribute[] array2 = array;
					int num = count;
					count = num + 1;
					array2[num] = attribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(FieldOffsetAttribute))
			{
				Attribute attribute = FieldOffsetAttribute.GetCustomAttribute(field);
				if (attribute != null)
				{
					Attribute[] array3 = array;
					int num = count;
					count = num + 1;
					array3[num] = attribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(NonSerializedAttribute))
			{
				Attribute attribute = NonSerializedAttribute.GetCustomAttribute(field);
				if (attribute != null)
				{
					Attribute[] array4 = array;
					int num = count;
					count = num + 1;
					array4[num] = attribute;
				}
			}
			return array;
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x00100DF0 File Offset: 0x000FEFF0
		[SecurityCritical]
		internal static bool IsDefined(RuntimeFieldInfo field, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)) && (((flag || caType == (RuntimeType)typeof(MarshalAsAttribute)) && MarshalAsAttribute.IsDefined(field)) || ((flag || caType == (RuntimeType)typeof(FieldOffsetAttribute)) && FieldOffsetAttribute.IsDefined(field)) || ((flag || caType == (RuntimeType)typeof(NonSerializedAttribute)) && NonSerializedAttribute.IsDefined(field)));
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x00100EB4 File Offset: 0x000FF0B4
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeConstructorInfo ctor, RuntimeType caType, bool includeSecCa, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
			{
				return new Attribute[0];
			}
			List<Attribute> list = new List<Attribute>();
			if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)))
			{
				object[] array;
				PseudoCustomAttribute.GetSecurityAttributes(ctor.Module.ModuleHandle.GetRuntimeModule(), ctor.MetadataToken, false, out array);
				if (array != null)
				{
					foreach (object obj in array)
					{
						if (caType == obj.GetType() || obj.GetType().IsSubclassOf(caType))
						{
							list.Add((Attribute)obj);
						}
					}
				}
			}
			count = list.Count;
			return list.ToArray();
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x00100FA8 File Offset: 0x000FF1A8
		[SecurityCritical]
		internal static bool IsDefined(RuntimeConstructorInfo ctor, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			int num;
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)) && ((flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && PseudoCustomAttribute.GetCustomAttributes(ctor, caType, true, out num).Length != 0);
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x00101018 File Offset: 0x000FF218
		internal static Attribute[] GetCustomAttributes(RuntimePropertyInfo property, RuntimeType caType, out int count)
		{
			count = 0;
			return null;
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x0010101E File Offset: 0x000FF21E
		internal static bool IsDefined(RuntimePropertyInfo property, RuntimeType caType)
		{
			return false;
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x00101021 File Offset: 0x000FF221
		internal static Attribute[] GetCustomAttributes(RuntimeEventInfo e, RuntimeType caType, out int count)
		{
			count = 0;
			return null;
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x00101027 File Offset: 0x000FF227
		internal static bool IsDefined(RuntimeEventInfo e, RuntimeType caType)
		{
			return false;
		}

		// Token: 0x04001C99 RID: 7321
		private static Dictionary<RuntimeType, RuntimeType> s_pca;

		// Token: 0x04001C9A RID: 7322
		private static int s_pcasCount;
	}
}
