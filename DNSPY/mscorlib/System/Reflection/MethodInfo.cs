using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x0200060A RID: 1546
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_MethodInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class MethodInfo : MethodBase, _MethodInfo
	{
		// Token: 0x06004738 RID: 18232 RVA: 0x001039F8 File Offset: 0x00101BF8
		protected MethodInfo()
		{
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x00103A00 File Offset: 0x00101C00
		[__DynamicallyInvokable]
		public static bool operator ==(MethodInfo left, MethodInfo right)
		{
			return left == right || (left != null && right != null && !(left is RuntimeMethodInfo) && !(right is RuntimeMethodInfo) && left.Equals(right));
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x00103A27 File Offset: 0x00101C27
		[__DynamicallyInvokable]
		public static bool operator !=(MethodInfo left, MethodInfo right)
		{
			return !(left == right);
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x00103A33 File Offset: 0x00101C33
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x00103A3C File Offset: 0x00101C3C
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600473D RID: 18237 RVA: 0x00103A44 File Offset: 0x00101C44
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Method;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x0600473E RID: 18238 RVA: 0x00103A47 File Offset: 0x00101C47
		[__DynamicallyInvokable]
		public virtual Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x0600473F RID: 18239 RVA: 0x00103A4E File Offset: 0x00101C4E
		[__DynamicallyInvokable]
		public virtual ParameterInfo ReturnParameter
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06004740 RID: 18240
		public abstract ICustomAttributeProvider ReturnTypeCustomAttributes { get; }

		// Token: 0x06004741 RID: 18241
		[__DynamicallyInvokable]
		public abstract MethodInfo GetBaseDefinition();

		// Token: 0x06004742 RID: 18242 RVA: 0x00103A55 File Offset: 0x00101C55
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public override Type[] GetGenericArguments()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x00103A66 File Offset: 0x00101C66
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public virtual MethodInfo GetGenericMethodDefinition()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x00103A77 File Offset: 0x00101C77
		[__DynamicallyInvokable]
		public virtual MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x00103A88 File Offset: 0x00101C88
		[__DynamicallyInvokable]
		public virtual Delegate CreateDelegate(Type delegateType)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x00103A99 File Offset: 0x00101C99
		[__DynamicallyInvokable]
		public virtual Delegate CreateDelegate(Type delegateType, object target)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x00103AAA File Offset: 0x00101CAA
		Type _MethodInfo.GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x00103AB2 File Offset: 0x00101CB2
		void _MethodInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x00103AB9 File Offset: 0x00101CB9
		void _MethodInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x00103AC0 File Offset: 0x00101CC0
		void _MethodInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x00103AC7 File Offset: 0x00101CC7
		void _MethodInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
