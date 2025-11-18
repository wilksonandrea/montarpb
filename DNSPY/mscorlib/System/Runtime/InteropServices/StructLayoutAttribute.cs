using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000937 RID: 2359
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class StructLayoutAttribute : Attribute
	{
		// Token: 0x06006045 RID: 24645 RVA: 0x0014BB44 File Offset: 0x00149D44
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeType type)
		{
			if (!StructLayoutAttribute.IsDefined(type))
			{
				return null;
			}
			int num = 0;
			int num2 = 0;
			LayoutKind layoutKind = LayoutKind.Auto;
			TypeAttributes typeAttributes = type.Attributes & TypeAttributes.LayoutMask;
			if (typeAttributes != TypeAttributes.NotPublic)
			{
				if (typeAttributes != TypeAttributes.SequentialLayout)
				{
					if (typeAttributes == TypeAttributes.ExplicitLayout)
					{
						layoutKind = LayoutKind.Explicit;
					}
				}
				else
				{
					layoutKind = LayoutKind.Sequential;
				}
			}
			else
			{
				layoutKind = LayoutKind.Auto;
			}
			CharSet charSet = CharSet.None;
			TypeAttributes typeAttributes2 = type.Attributes & TypeAttributes.StringFormatMask;
			if (typeAttributes2 != TypeAttributes.NotPublic)
			{
				if (typeAttributes2 != TypeAttributes.UnicodeClass)
				{
					if (typeAttributes2 == TypeAttributes.AutoClass)
					{
						charSet = CharSet.Auto;
					}
				}
				else
				{
					charSet = CharSet.Unicode;
				}
			}
			else
			{
				charSet = CharSet.Ansi;
			}
			type.GetRuntimeModule().MetadataImport.GetClassLayout(type.MetadataToken, out num, out num2);
			if (num == 0)
			{
				num = 8;
			}
			return new StructLayoutAttribute(layoutKind, num, num2, charSet);
		}

		// Token: 0x06006046 RID: 24646 RVA: 0x0014BBE3 File Offset: 0x00149DE3
		internal static bool IsDefined(RuntimeType type)
		{
			return !type.IsInterface && !type.HasElementType && !type.IsGenericParameter;
		}

		// Token: 0x06006047 RID: 24647 RVA: 0x0014BC00 File Offset: 0x00149E00
		internal StructLayoutAttribute(LayoutKind layoutKind, int pack, int size, CharSet charSet)
		{
			this._val = layoutKind;
			this.Pack = pack;
			this.Size = size;
			this.CharSet = charSet;
		}

		// Token: 0x06006048 RID: 24648 RVA: 0x0014BC25 File Offset: 0x00149E25
		[__DynamicallyInvokable]
		public StructLayoutAttribute(LayoutKind layoutKind)
		{
			this._val = layoutKind;
		}

		// Token: 0x06006049 RID: 24649 RVA: 0x0014BC34 File Offset: 0x00149E34
		public StructLayoutAttribute(short layoutKind)
		{
			this._val = (LayoutKind)layoutKind;
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x0600604A RID: 24650 RVA: 0x0014BC43 File Offset: 0x00149E43
		[__DynamicallyInvokable]
		public LayoutKind Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002B23 RID: 11043
		private const int DEFAULT_PACKING_SIZE = 8;

		// Token: 0x04002B24 RID: 11044
		internal LayoutKind _val;

		// Token: 0x04002B25 RID: 11045
		[__DynamicallyInvokable]
		public int Pack;

		// Token: 0x04002B26 RID: 11046
		[__DynamicallyInvokable]
		public int Size;

		// Token: 0x04002B27 RID: 11047
		[__DynamicallyInvokable]
		public CharSet CharSet;
	}
}
