using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x0200061D RID: 1565
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_PropertyInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class PropertyInfo : MemberInfo, _PropertyInfo
	{
		// Token: 0x0600485A RID: 18522 RVA: 0x00106BBA File Offset: 0x00104DBA
		protected PropertyInfo()
		{
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x00106BC2 File Offset: 0x00104DC2
		[__DynamicallyInvokable]
		public static bool operator ==(PropertyInfo left, PropertyInfo right)
		{
			return left == right || (left != null && right != null && !(left is RuntimePropertyInfo) && !(right is RuntimePropertyInfo) && left.Equals(right));
		}

		// Token: 0x0600485C RID: 18524 RVA: 0x00106BE9 File Offset: 0x00104DE9
		[__DynamicallyInvokable]
		public static bool operator !=(PropertyInfo left, PropertyInfo right)
		{
			return !(left == right);
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x00106BF5 File Offset: 0x00104DF5
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x00106BFE File Offset: 0x00104DFE
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600485F RID: 18527 RVA: 0x00106C06 File Offset: 0x00104E06
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Property;
			}
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x00106C0A File Offset: 0x00104E0A
		[__DynamicallyInvokable]
		public virtual object GetConstantValue()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x00106C11 File Offset: 0x00104E11
		public virtual object GetRawConstantValue()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06004862 RID: 18530
		[__DynamicallyInvokable]
		public abstract Type PropertyType
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x06004863 RID: 18531
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x06004864 RID: 18532
		[__DynamicallyInvokable]
		public abstract MethodInfo[] GetAccessors(bool nonPublic);

		// Token: 0x06004865 RID: 18533
		[__DynamicallyInvokable]
		public abstract MethodInfo GetGetMethod(bool nonPublic);

		// Token: 0x06004866 RID: 18534
		[__DynamicallyInvokable]
		public abstract MethodInfo GetSetMethod(bool nonPublic);

		// Token: 0x06004867 RID: 18535
		[__DynamicallyInvokable]
		public abstract ParameterInfo[] GetIndexParameters();

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06004868 RID: 18536
		[__DynamicallyInvokable]
		public abstract PropertyAttributes Attributes
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06004869 RID: 18537
		[__DynamicallyInvokable]
		public abstract bool CanRead
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600486A RID: 18538
		[__DynamicallyInvokable]
		public abstract bool CanWrite
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x0600486B RID: 18539 RVA: 0x00106C18 File Offset: 0x00104E18
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public object GetValue(object obj)
		{
			return this.GetValue(obj, null);
		}

		// Token: 0x0600486C RID: 18540 RVA: 0x00106C22 File Offset: 0x00104E22
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public virtual object GetValue(object obj, object[] index)
		{
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		// Token: 0x0600486D RID: 18541
		public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x0600486E RID: 18542 RVA: 0x00106C2F File Offset: 0x00104E2F
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, null);
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x00106C3A File Offset: 0x00104E3A
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public virtual void SetValue(object obj, object value, object[] index)
		{
			this.SetValue(obj, value, BindingFlags.Default, null, index, null);
		}

		// Token: 0x06004870 RID: 18544 RVA: 0x00106C48 File Offset: 0x00104E48
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x00106C4F File Offset: 0x00104E4F
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x00106C56 File Offset: 0x00104E56
		[__DynamicallyInvokable]
		public MethodInfo[] GetAccessors()
		{
			return this.GetAccessors(false);
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06004873 RID: 18547 RVA: 0x00106C5F File Offset: 0x00104E5F
		[__DynamicallyInvokable]
		public virtual MethodInfo GetMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetGetMethod(true);
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06004874 RID: 18548 RVA: 0x00106C68 File Offset: 0x00104E68
		[__DynamicallyInvokable]
		public virtual MethodInfo SetMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetSetMethod(true);
			}
		}

		// Token: 0x06004875 RID: 18549 RVA: 0x00106C71 File Offset: 0x00104E71
		[__DynamicallyInvokable]
		public MethodInfo GetGetMethod()
		{
			return this.GetGetMethod(false);
		}

		// Token: 0x06004876 RID: 18550 RVA: 0x00106C7A File Offset: 0x00104E7A
		[__DynamicallyInvokable]
		public MethodInfo GetSetMethod()
		{
			return this.GetSetMethod(false);
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06004877 RID: 18551 RVA: 0x00106C83 File Offset: 0x00104E83
		[__DynamicallyInvokable]
		public bool IsSpecialName
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & PropertyAttributes.SpecialName) > PropertyAttributes.None;
			}
		}

		// Token: 0x06004878 RID: 18552 RVA: 0x00106C94 File Offset: 0x00104E94
		Type _PropertyInfo.GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004879 RID: 18553 RVA: 0x00106C9C File Offset: 0x00104E9C
		void _PropertyInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600487A RID: 18554 RVA: 0x00106CA3 File Offset: 0x00104EA3
		void _PropertyInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600487B RID: 18555 RVA: 0x00106CAA File Offset: 0x00104EAA
		void _PropertyInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600487C RID: 18556 RVA: 0x00106CB1 File Offset: 0x00104EB1
		void _PropertyInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
