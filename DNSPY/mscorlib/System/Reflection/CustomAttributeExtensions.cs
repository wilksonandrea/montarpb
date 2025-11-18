using System;
using System.Collections.Generic;

namespace System.Reflection
{
	// Token: 0x020005CF RID: 1487
	[__DynamicallyInvokable]
	public static class CustomAttributeExtensions
	{
		// Token: 0x060044BE RID: 17598 RVA: 0x000FCEBA File Offset: 0x000FB0BA
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(this Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x000FCEC3 File Offset: 0x000FB0C3
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(this Module element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x000FCECC File Offset: 0x000FB0CC
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x000FCED5 File Offset: 0x000FB0D5
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x000FCEDE File Offset: 0x000FB0DE
		[__DynamicallyInvokable]
		public static T GetCustomAttribute<T>(this Assembly element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x000FCEF5 File Offset: 0x000FB0F5
		[__DynamicallyInvokable]
		public static T GetCustomAttribute<T>(this Module element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x000FCF0C File Offset: 0x000FB10C
		[__DynamicallyInvokable]
		public static T GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x000FCF23 File Offset: 0x000FB123
		[__DynamicallyInvokable]
		public static T GetCustomAttribute<T>(this ParameterInfo element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x000FCF3A File Offset: 0x000FB13A
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttribute(element, attributeType, inherit);
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x000FCF44 File Offset: 0x000FB144
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttribute(element, attributeType, inherit);
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x000FCF4E File Offset: 0x000FB14E
		[__DynamicallyInvokable]
		public static T GetCustomAttribute<T>(this MemberInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T), inherit));
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x000FCF66 File Offset: 0x000FB166
		[__DynamicallyInvokable]
		public static T GetCustomAttribute<T>(this ParameterInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T), inherit));
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x000FCF7E File Offset: 0x000FB17E
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x000FCF86 File Offset: 0x000FB186
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this Module element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x000FCF8E File Offset: 0x000FB18E
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x000FCF96 File Offset: 0x000FB196
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x000FCF9E File Offset: 0x000FB19E
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, inherit);
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x000FCFA7 File Offset: 0x000FB1A7
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, inherit);
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x000FCFB0 File Offset: 0x000FB1B0
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x000FCFB9 File Offset: 0x000FB1B9
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this Module element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x000FCFC2 File Offset: 0x000FB1C2
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x000FCFCB File Offset: 0x000FB1CB
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x000FCFD4 File Offset: 0x000FB1D4
		[__DynamicallyInvokable]
		public static IEnumerable<T> GetCustomAttributes<T>(this Assembly element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x000FCFEB File Offset: 0x000FB1EB
		[__DynamicallyInvokable]
		public static IEnumerable<T> GetCustomAttributes<T>(this Module element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x000FD002 File Offset: 0x000FB202
		[__DynamicallyInvokable]
		public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x000FD019 File Offset: 0x000FB219
		[__DynamicallyInvokable]
		public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x000FD030 File Offset: 0x000FB230
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, attributeType, inherit);
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x000FD03A File Offset: 0x000FB23A
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, attributeType, inherit);
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x000FD044 File Offset: 0x000FB244
		[__DynamicallyInvokable]
		public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element, bool inherit) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T), inherit);
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x000FD05C File Offset: 0x000FB25C
		[__DynamicallyInvokable]
		public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element, bool inherit) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T), inherit);
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x000FD074 File Offset: 0x000FB274
		[__DynamicallyInvokable]
		public static bool IsDefined(this Assembly element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x000FD07D File Offset: 0x000FB27D
		[__DynamicallyInvokable]
		public static bool IsDefined(this Module element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x000FD086 File Offset: 0x000FB286
		[__DynamicallyInvokable]
		public static bool IsDefined(this MemberInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x000FD08F File Offset: 0x000FB28F
		[__DynamicallyInvokable]
		public static bool IsDefined(this ParameterInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x000FD098 File Offset: 0x000FB298
		[__DynamicallyInvokable]
		public static bool IsDefined(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.IsDefined(element, attributeType, inherit);
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x000FD0A2 File Offset: 0x000FB2A2
		[__DynamicallyInvokable]
		public static bool IsDefined(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.IsDefined(element, attributeType, inherit);
		}
	}
}
