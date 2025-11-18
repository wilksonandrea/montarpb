using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	// Token: 0x020000AD RID: 173
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Attribute))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Attribute : _Attribute
	{
		// Token: 0x060009CC RID: 2508 RVA: 0x0001F850 File Offset: 0x0001DA50
		private static Attribute[] InternalGetCustomAttributes(PropertyInfo element, Type type, bool inherit)
		{
			Attribute[] array = (Attribute[])element.GetCustomAttributes(type, inherit);
			if (!inherit)
			{
				return array;
			}
			Dictionary<Type, AttributeUsageAttribute> dictionary = new Dictionary<Type, AttributeUsageAttribute>(11);
			List<Attribute> list = new List<Attribute>();
			Attribute.CopyToArrayList(list, array, dictionary);
			Type[] indexParameterTypes = Attribute.GetIndexParameterTypes(element);
			PropertyInfo propertyInfo = Attribute.GetParentDefinition(element, indexParameterTypes);
			while (propertyInfo != null)
			{
				array = Attribute.GetCustomAttributes(propertyInfo, type, false);
				Attribute.AddAttributesToList(list, array, dictionary);
				propertyInfo = Attribute.GetParentDefinition(propertyInfo, indexParameterTypes);
			}
			Array array2 = Attribute.CreateAttributeArrayHelper(type, list.Count);
			Array.Copy(list.ToArray(), 0, array2, 0, list.Count);
			return (Attribute[])array2;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001F8E8 File Offset: 0x0001DAE8
		private static bool InternalIsDefined(PropertyInfo element, Type attributeType, bool inherit)
		{
			if (element.IsDefined(attributeType, inherit))
			{
				return true;
			}
			if (inherit)
			{
				AttributeUsageAttribute attributeUsageAttribute = Attribute.InternalGetAttributeUsage(attributeType);
				if (!attributeUsageAttribute.Inherited)
				{
					return false;
				}
				Type[] indexParameterTypes = Attribute.GetIndexParameterTypes(element);
				PropertyInfo propertyInfo = Attribute.GetParentDefinition(element, indexParameterTypes);
				while (propertyInfo != null)
				{
					if (propertyInfo.IsDefined(attributeType, false))
					{
						return true;
					}
					propertyInfo = Attribute.GetParentDefinition(propertyInfo, indexParameterTypes);
				}
			}
			return false;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0001F944 File Offset: 0x0001DB44
		private static PropertyInfo GetParentDefinition(PropertyInfo property, Type[] propertyParameters)
		{
			MethodInfo methodInfo = property.GetGetMethod(true);
			if (methodInfo == null)
			{
				methodInfo = property.GetSetMethod(true);
			}
			RuntimeMethodInfo runtimeMethodInfo = methodInfo as RuntimeMethodInfo;
			if (runtimeMethodInfo != null)
			{
				runtimeMethodInfo = runtimeMethodInfo.GetParentDefinition();
				if (runtimeMethodInfo != null)
				{
					return runtimeMethodInfo.DeclaringType.GetProperty(property.Name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, property.PropertyType, propertyParameters, null);
				}
			}
			return null;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0001F9A8 File Offset: 0x0001DBA8
		private static Attribute[] InternalGetCustomAttributes(EventInfo element, Type type, bool inherit)
		{
			Attribute[] array = (Attribute[])element.GetCustomAttributes(type, inherit);
			if (inherit)
			{
				Dictionary<Type, AttributeUsageAttribute> dictionary = new Dictionary<Type, AttributeUsageAttribute>(11);
				List<Attribute> list = new List<Attribute>();
				Attribute.CopyToArrayList(list, array, dictionary);
				EventInfo eventInfo = Attribute.GetParentDefinition(element);
				while (eventInfo != null)
				{
					array = Attribute.GetCustomAttributes(eventInfo, type, false);
					Attribute.AddAttributesToList(list, array, dictionary);
					eventInfo = Attribute.GetParentDefinition(eventInfo);
				}
				Array array2 = Attribute.CreateAttributeArrayHelper(type, list.Count);
				Array.Copy(list.ToArray(), 0, array2, 0, list.Count);
				return (Attribute[])array2;
			}
			return array;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001FA34 File Offset: 0x0001DC34
		private static EventInfo GetParentDefinition(EventInfo ev)
		{
			MethodInfo addMethod = ev.GetAddMethod(true);
			RuntimeMethodInfo runtimeMethodInfo = addMethod as RuntimeMethodInfo;
			if (runtimeMethodInfo != null)
			{
				runtimeMethodInfo = runtimeMethodInfo.GetParentDefinition();
				if (runtimeMethodInfo != null)
				{
					return runtimeMethodInfo.DeclaringType.GetEvent(ev.Name);
				}
			}
			return null;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0001FA7C File Offset: 0x0001DC7C
		private static bool InternalIsDefined(EventInfo element, Type attributeType, bool inherit)
		{
			if (element.IsDefined(attributeType, inherit))
			{
				return true;
			}
			if (inherit)
			{
				AttributeUsageAttribute attributeUsageAttribute = Attribute.InternalGetAttributeUsage(attributeType);
				if (!attributeUsageAttribute.Inherited)
				{
					return false;
				}
				EventInfo eventInfo = Attribute.GetParentDefinition(element);
				while (eventInfo != null)
				{
					if (eventInfo.IsDefined(attributeType, false))
					{
						return true;
					}
					eventInfo = Attribute.GetParentDefinition(eventInfo);
				}
			}
			return false;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0001FAD0 File Offset: 0x0001DCD0
		private static ParameterInfo GetParentDefinition(ParameterInfo param)
		{
			RuntimeMethodInfo runtimeMethodInfo = param.Member as RuntimeMethodInfo;
			if (runtimeMethodInfo != null)
			{
				runtimeMethodInfo = runtimeMethodInfo.GetParentDefinition();
				if (runtimeMethodInfo != null)
				{
					ParameterInfo[] parameters = runtimeMethodInfo.GetParameters();
					return parameters[param.Position];
				}
			}
			return null;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0001FB14 File Offset: 0x0001DD14
		private static Attribute[] InternalParamGetCustomAttributes(ParameterInfo param, Type type, bool inherit)
		{
			List<Type> list = new List<Type>();
			if (type == null)
			{
				type = typeof(Attribute);
			}
			object[] array = param.GetCustomAttributes(type, false);
			for (int i = 0; i < array.Length; i++)
			{
				Type type2 = array[i].GetType();
				AttributeUsageAttribute attributeUsageAttribute = Attribute.InternalGetAttributeUsage(type2);
				if (!attributeUsageAttribute.AllowMultiple)
				{
					list.Add(type2);
				}
			}
			Attribute[] array2;
			if (array.Length == 0)
			{
				array2 = Attribute.CreateAttributeArrayHelper(type, 0);
			}
			else
			{
				array2 = (Attribute[])array;
			}
			if (param.Member.DeclaringType == null)
			{
				return array2;
			}
			if (!inherit)
			{
				return array2;
			}
			for (ParameterInfo parameterInfo = Attribute.GetParentDefinition(param); parameterInfo != null; parameterInfo = Attribute.GetParentDefinition(parameterInfo))
			{
				array = parameterInfo.GetCustomAttributes(type, false);
				int num = 0;
				for (int j = 0; j < array.Length; j++)
				{
					Type type3 = array[j].GetType();
					AttributeUsageAttribute attributeUsageAttribute2 = Attribute.InternalGetAttributeUsage(type3);
					if (attributeUsageAttribute2.Inherited && !list.Contains(type3))
					{
						if (!attributeUsageAttribute2.AllowMultiple)
						{
							list.Add(type3);
						}
						num++;
					}
					else
					{
						array[j] = null;
					}
				}
				Attribute[] array3 = Attribute.CreateAttributeArrayHelper(type, num);
				num = 0;
				for (int k = 0; k < array.Length; k++)
				{
					if (array[k] != null)
					{
						array3[num] = (Attribute)array[k];
						num++;
					}
				}
				Attribute[] array4 = array2;
				array2 = Attribute.CreateAttributeArrayHelper(type, array4.Length + num);
				Array.Copy(array4, array2, array4.Length);
				int num2 = array4.Length;
				for (int l = 0; l < array3.Length; l++)
				{
					array2[num2 + l] = array3[l];
				}
			}
			return array2;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0001FCA8 File Offset: 0x0001DEA8
		private static bool InternalParamIsDefined(ParameterInfo param, Type type, bool inherit)
		{
			if (param.IsDefined(type, false))
			{
				return true;
			}
			if (param.Member.DeclaringType == null || !inherit)
			{
				return false;
			}
			for (ParameterInfo parameterInfo = Attribute.GetParentDefinition(param); parameterInfo != null; parameterInfo = Attribute.GetParentDefinition(parameterInfo))
			{
				object[] customAttributes = parameterInfo.GetCustomAttributes(type, false);
				for (int i = 0; i < customAttributes.Length; i++)
				{
					Type type2 = customAttributes[i].GetType();
					AttributeUsageAttribute attributeUsageAttribute = Attribute.InternalGetAttributeUsage(type2);
					if (customAttributes[i] is Attribute && attributeUsageAttribute.Inherited)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0001FD2C File Offset: 0x0001DF2C
		private static void CopyToArrayList(List<Attribute> attributeList, Attribute[] attributes, Dictionary<Type, AttributeUsageAttribute> types)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				attributeList.Add(attributes[i]);
				Type type = attributes[i].GetType();
				if (!types.ContainsKey(type))
				{
					types[type] = Attribute.InternalGetAttributeUsage(type);
				}
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0001FD70 File Offset: 0x0001DF70
		private static Type[] GetIndexParameterTypes(PropertyInfo element)
		{
			ParameterInfo[] indexParameters = element.GetIndexParameters();
			if (indexParameters.Length != 0)
			{
				Type[] array = new Type[indexParameters.Length];
				for (int i = 0; i < indexParameters.Length; i++)
				{
					array[i] = indexParameters[i].ParameterType;
				}
				return array;
			}
			return Array.Empty<Type>();
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0001FDB4 File Offset: 0x0001DFB4
		private static void AddAttributesToList(List<Attribute> attributeList, Attribute[] attributes, Dictionary<Type, AttributeUsageAttribute> types)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				Type type = attributes[i].GetType();
				AttributeUsageAttribute attributeUsageAttribute = null;
				types.TryGetValue(type, out attributeUsageAttribute);
				if (attributeUsageAttribute == null)
				{
					attributeUsageAttribute = Attribute.InternalGetAttributeUsage(type);
					types[type] = attributeUsageAttribute;
					if (attributeUsageAttribute.Inherited)
					{
						attributeList.Add(attributes[i]);
					}
				}
				else if (attributeUsageAttribute.Inherited && attributeUsageAttribute.AllowMultiple)
				{
					attributeList.Add(attributes[i]);
				}
			}
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001FE24 File Offset: 0x0001E024
		private static AttributeUsageAttribute InternalGetAttributeUsage(Type type)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(AttributeUsageAttribute), false);
			if (customAttributes.Length == 1)
			{
				return (AttributeUsageAttribute)customAttributes[0];
			}
			if (customAttributes.Length == 0)
			{
				return AttributeUsageAttribute.Default;
			}
			throw new FormatException(Environment.GetResourceString("Format_AttributeUsage", new object[] { type }));
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001FE75 File Offset: 0x0001E075
		[SecuritySafeCritical]
		private static Attribute[] CreateAttributeArrayHelper(Type elementType, int elementCount)
		{
			return (Attribute[])Array.UnsafeCreateInstance(elementType, elementCount);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001FE83 File Offset: 0x0001E083
		[__DynamicallyInvokable]
		public static Attribute[] GetCustomAttributes(MemberInfo element, Type type)
		{
			return Attribute.GetCustomAttributes(element, type, true);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001FE90 File Offset: 0x0001E090
		[__DynamicallyInvokable]
		public static Attribute[] GetCustomAttributes(MemberInfo element, Type type, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsSubclassOf(typeof(Attribute)) && type != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
			}
			MemberTypes memberType = element.MemberType;
			if (memberType == MemberTypes.Event)
			{
				return Attribute.InternalGetCustomAttributes((EventInfo)element, type, inherit);
			}
			if (memberType == MemberTypes.Property)
			{
				return Attribute.InternalGetCustomAttributes((PropertyInfo)element, type, inherit);
			}
			return element.GetCustomAttributes(type, inherit) as Attribute[];
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001FF32 File Offset: 0x0001E132
		[__DynamicallyInvokable]
		public static Attribute[] GetCustomAttributes(MemberInfo element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001FF3C File Offset: 0x0001E13C
		[__DynamicallyInvokable]
		public static Attribute[] GetCustomAttributes(MemberInfo element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			MemberTypes memberType = element.MemberType;
			if (memberType == MemberTypes.Event)
			{
				return Attribute.InternalGetCustomAttributes((EventInfo)element, typeof(Attribute), inherit);
			}
			if (memberType == MemberTypes.Property)
			{
				return Attribute.InternalGetCustomAttributes((PropertyInfo)element, typeof(Attribute), inherit);
			}
			return element.GetCustomAttributes(typeof(Attribute), inherit) as Attribute[];
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001FFB1 File Offset: 0x0001E1B1
		[__DynamicallyInvokable]
		public static bool IsDefined(MemberInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, true);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001FFBC File Offset: 0x0001E1BC
		[__DynamicallyInvokable]
		public static bool IsDefined(MemberInfo element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
			}
			MemberTypes memberType = element.MemberType;
			if (memberType == MemberTypes.Event)
			{
				return Attribute.InternalIsDefined((EventInfo)element, attributeType, inherit);
			}
			if (memberType == MemberTypes.Property)
			{
				return Attribute.InternalIsDefined((PropertyInfo)element, attributeType, inherit);
			}
			return element.IsDefined(attributeType, inherit);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00020059 File Offset: 0x0001E259
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00020064 File Offset: 0x0001E264
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(Environment.GetResourceString("RFLCT.AmbigCust"));
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002009C File Offset: 0x0001E29C
		[__DynamicallyInvokable]
		public static Attribute[] GetCustomAttributes(ParameterInfo element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x000200A5 File Offset: 0x0001E2A5
		[__DynamicallyInvokable]
		public static Attribute[] GetCustomAttributes(ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType, true);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000200B0 File Offset: 0x0001E2B0
		[__DynamicallyInvokable]
		public static Attribute[] GetCustomAttributes(ParameterInfo element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
			}
			if (element.Member == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidParameterInfo"), "element");
			}
			MemberInfo member = element.Member;
			if (member.MemberType == MemberTypes.Method && inherit)
			{
				return Attribute.InternalParamGetCustomAttributes(element, attributeType, inherit);
			}
			return element.GetCustomAttributes(attributeType, inherit) as Attribute[];
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00020160 File Offset: 0x0001E360
		[__DynamicallyInvokable]
		public static Attribute[] GetCustomAttributes(ParameterInfo element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (element.Member == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidParameterInfo"), "element");
			}
			MemberInfo member = element.Member;
			if (member.MemberType == MemberTypes.Method && inherit)
			{
				return Attribute.InternalParamGetCustomAttributes(element, null, inherit);
			}
			return element.GetCustomAttributes(typeof(Attribute), inherit) as Attribute[];
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000201D1 File Offset: 0x0001E3D1
		[__DynamicallyInvokable]
		public static bool IsDefined(ParameterInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, true);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000201DC File Offset: 0x0001E3DC
		[__DynamicallyInvokable]
		public static bool IsDefined(ParameterInfo element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
			}
			MemberInfo member = element.Member;
			MemberTypes memberType = member.MemberType;
			if (memberType == MemberTypes.Constructor)
			{
				return element.IsDefined(attributeType, false);
			}
			if (memberType == MemberTypes.Method)
			{
				return Attribute.InternalParamIsDefined(element, attributeType, inherit);
			}
			if (memberType != MemberTypes.Property)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidParamInfo"));
			}
			return element.IsDefined(attributeType, false);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00020286 File Offset: 0x0001E486
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00020290 File Offset: 0x0001E490
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(ParameterInfo element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(Environment.GetResourceString("RFLCT.AmbigCust"));
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000202CE File Offset: 0x0001E4CE
		public static Attribute[] GetCustomAttributes(Module element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType, true);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000202D8 File Offset: 0x0001E4D8
		public static Attribute[] GetCustomAttributes(Module element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000202E1 File Offset: 0x0001E4E1
		public static Attribute[] GetCustomAttributes(Module element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (Attribute[])element.GetCustomAttributes(typeof(Attribute), inherit);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00020310 File Offset: 0x0001E510
		public static Attribute[] GetCustomAttributes(Module element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
			}
			return (Attribute[])element.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00020386 File Offset: 0x0001E586
		public static bool IsDefined(Module element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, false);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00020390 File Offset: 0x0001E590
		public static bool IsDefined(Module element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
			}
			return element.IsDefined(attributeType, false);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00020401 File Offset: 0x0001E601
		public static Attribute GetCustomAttribute(Module element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0002040C File Offset: 0x0001E60C
		public static Attribute GetCustomAttribute(Module element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(Environment.GetResourceString("RFLCT.AmbigCust"));
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00020444 File Offset: 0x0001E644
		[__DynamicallyInvokable]
		public static Attribute[] GetCustomAttributes(Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType, true);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00020450 File Offset: 0x0001E650
		public static Attribute[] GetCustomAttributes(Assembly element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
			}
			return (Attribute[])element.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000204C6 File Offset: 0x0001E6C6
		[__DynamicallyInvokable]
		public static Attribute[] GetCustomAttributes(Assembly element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x000204CF File Offset: 0x0001E6CF
		public static Attribute[] GetCustomAttributes(Assembly element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (Attribute[])element.GetCustomAttributes(typeof(Attribute), inherit);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000204FB File Offset: 0x0001E6FB
		[__DynamicallyInvokable]
		public static bool IsDefined(Assembly element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, true);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00020508 File Offset: 0x0001E708
		public static bool IsDefined(Assembly element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
			}
			return element.IsDefined(attributeType, false);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00020579 File Offset: 0x0001E779
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00020584 File Offset: 0x0001E784
		public static Attribute GetCustomAttribute(Assembly element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(Environment.GetResourceString("RFLCT.AmbigCust"));
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000205BC File Offset: 0x0001E7BC
		[__DynamicallyInvokable]
		protected Attribute()
		{
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000205C4 File Offset: 0x0001E7C4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			RuntimeType runtimeType = (RuntimeType)base.GetType();
			RuntimeType runtimeType2 = (RuntimeType)obj.GetType();
			if (runtimeType2 != runtimeType)
			{
				return false;
			}
			FieldInfo[] fields = runtimeType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < fields.Length; i++)
			{
				object obj2 = ((RtFieldInfo)fields[i]).UnsafeGetValue(this);
				object obj3 = ((RtFieldInfo)fields[i]).UnsafeGetValue(obj);
				if (!Attribute.AreFieldValuesEqual(obj2, obj3))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00020648 File Offset: 0x0001E848
		private static bool AreFieldValuesEqual(object thisValue, object thatValue)
		{
			if (thisValue == null && thatValue == null)
			{
				return true;
			}
			if (thisValue == null || thatValue == null)
			{
				return false;
			}
			if (thisValue.GetType().IsArray)
			{
				if (!thisValue.GetType().Equals(thatValue.GetType()))
				{
					return false;
				}
				Array array = thisValue as Array;
				Array array2 = thatValue as Array;
				if (array.Length != array2.Length)
				{
					return false;
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (!Attribute.AreFieldValuesEqual(array.GetValue(i), array2.GetValue(i)))
					{
						return false;
					}
				}
			}
			else if (!thisValue.Equals(thatValue))
			{
				return false;
			}
			return true;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000206DC File Offset: 0x0001E8DC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			Type type = base.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			object obj = null;
			for (int i = 0; i < fields.Length; i++)
			{
				object obj2 = ((RtFieldInfo)fields[i]).UnsafeGetValue(this);
				if (obj2 != null && !obj2.GetType().IsArray)
				{
					obj = obj2;
				}
				if (obj != null)
				{
					break;
				}
			}
			if (obj != null)
			{
				return obj.GetHashCode();
			}
			return type.GetHashCode();
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00020741 File Offset: 0x0001E941
		public virtual object TypeId
		{
			get
			{
				return base.GetType();
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00020749 File Offset: 0x0001E949
		public virtual bool Match(object obj)
		{
			return this.Equals(obj);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00020752 File Offset: 0x0001E952
		public virtual bool IsDefaultAttribute()
		{
			return false;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00020755 File Offset: 0x0001E955
		void _Attribute.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002075C File Offset: 0x0001E95C
		void _Attribute.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00020763 File Offset: 0x0001E963
		void _Attribute.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002076A File Offset: 0x0001E96A
		void _Attribute.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
