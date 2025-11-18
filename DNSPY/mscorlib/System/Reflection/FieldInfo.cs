using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020005E7 RID: 1511
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_FieldInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class FieldInfo : MemberInfo, _FieldInfo
	{
		// Token: 0x060045EE RID: 17902 RVA: 0x0010159C File Offset: 0x000FF79C
		[__DynamicallyInvokable]
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
			}
			FieldInfo fieldInfo = RuntimeType.GetFieldInfo(handle.GetRuntimeFieldInfo());
			Type declaringType = fieldInfo.DeclaringType;
			if (declaringType != null && declaringType.IsGenericType)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_FieldDeclaringTypeGeneric"), fieldInfo.Name, declaringType.GetGenericTypeDefinition()));
			}
			return fieldInfo;
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x0010160E File Offset: 0x000FF80E
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
			}
			return RuntimeType.GetFieldInfo(declaringType.GetRuntimeType(), handle.GetRuntimeFieldInfo());
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x0010163C File Offset: 0x000FF83C
		protected FieldInfo()
		{
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x00101644 File Offset: 0x000FF844
		[__DynamicallyInvokable]
		public static bool operator ==(FieldInfo left, FieldInfo right)
		{
			return left == right || (left != null && right != null && !(left is RuntimeFieldInfo) && !(right is RuntimeFieldInfo) && left.Equals(right));
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x0010166B File Offset: 0x000FF86B
		[__DynamicallyInvokable]
		public static bool operator !=(FieldInfo left, FieldInfo right)
		{
			return !(left == right);
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x00101677 File Offset: 0x000FF877
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x00101680 File Offset: 0x000FF880
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x00101688 File Offset: 0x000FF888
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x0010168B File Offset: 0x000FF88B
		public virtual Type[] GetRequiredCustomModifiers()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x00101692 File Offset: 0x000FF892
		public virtual Type[] GetOptionalCustomModifiers()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x00101699 File Offset: 0x000FF899
		[CLSCompliant(false)]
		public virtual void SetValueDirect(TypedReference obj, object value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_AbstractNonCLS"));
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x001016AA File Offset: 0x000FF8AA
		[CLSCompliant(false)]
		public virtual object GetValueDirect(TypedReference obj)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_AbstractNonCLS"));
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x060045FA RID: 17914
		[__DynamicallyInvokable]
		public abstract RuntimeFieldHandle FieldHandle
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060045FB RID: 17915
		[__DynamicallyInvokable]
		public abstract Type FieldType
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x060045FC RID: 17916
		[__DynamicallyInvokable]
		public abstract object GetValue(object obj);

		// Token: 0x060045FD RID: 17917 RVA: 0x001016BB File Offset: 0x000FF8BB
		public virtual object GetRawConstantValue()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_AbstractNonCLS"));
		}

		// Token: 0x060045FE RID: 17918
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x060045FF RID: 17919
		[__DynamicallyInvokable]
		public abstract FieldAttributes Attributes
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x001016CC File Offset: 0x000FF8CC
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, BindingFlags.Default, Type.DefaultBinder, null);
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06004601 RID: 17921 RVA: 0x001016DD File Offset: 0x000FF8DD
		[__DynamicallyInvokable]
		public bool IsPublic
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06004602 RID: 17922 RVA: 0x001016EA File Offset: 0x000FF8EA
		[__DynamicallyInvokable]
		public bool IsPrivate
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06004603 RID: 17923 RVA: 0x001016F7 File Offset: 0x000FF8F7
		[__DynamicallyInvokable]
		public bool IsFamily
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Family;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06004604 RID: 17924 RVA: 0x00101704 File Offset: 0x000FF904
		[__DynamicallyInvokable]
		public bool IsAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Assembly;
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06004605 RID: 17925 RVA: 0x00101711 File Offset: 0x000FF911
		[__DynamicallyInvokable]
		public bool IsFamilyAndAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamANDAssem;
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06004606 RID: 17926 RVA: 0x0010171E File Offset: 0x000FF91E
		[__DynamicallyInvokable]
		public bool IsFamilyOrAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamORAssem;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06004607 RID: 17927 RVA: 0x0010172B File Offset: 0x000FF92B
		[__DynamicallyInvokable]
		public bool IsStatic
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.Static) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x00101739 File Offset: 0x000FF939
		[__DynamicallyInvokable]
		public bool IsInitOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.InitOnly) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x00101747 File Offset: 0x000FF947
		[__DynamicallyInvokable]
		public bool IsLiteral
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.Literal) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x0600460A RID: 17930 RVA: 0x00101755 File Offset: 0x000FF955
		public bool IsNotSerialized
		{
			get
			{
				return (this.Attributes & FieldAttributes.NotSerialized) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x0600460B RID: 17931 RVA: 0x00101766 File Offset: 0x000FF966
		[__DynamicallyInvokable]
		public bool IsSpecialName
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.SpecialName) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x0600460C RID: 17932 RVA: 0x00101777 File Offset: 0x000FF977
		public bool IsPinvokeImpl
		{
			get
			{
				return (this.Attributes & FieldAttributes.PinvokeImpl) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x0600460D RID: 17933 RVA: 0x00101788 File Offset: 0x000FF988
		public virtual bool IsSecurityCritical
		{
			get
			{
				return this.FieldHandle.IsSecurityCritical();
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600460E RID: 17934 RVA: 0x001017A4 File Offset: 0x000FF9A4
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				return this.FieldHandle.IsSecuritySafeCritical();
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600460F RID: 17935 RVA: 0x001017C0 File Offset: 0x000FF9C0
		public virtual bool IsSecurityTransparent
		{
			get
			{
				return this.FieldHandle.IsSecurityTransparent();
			}
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x001017DB File Offset: 0x000FF9DB
		Type _FieldInfo.GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x001017E3 File Offset: 0x000FF9E3
		void _FieldInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x001017EA File Offset: 0x000FF9EA
		void _FieldInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x001017F1 File Offset: 0x000FF9F1
		void _FieldInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x001017F8 File Offset: 0x000FF9F8
		void _FieldInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
