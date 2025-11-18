using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020005D4 RID: 1492
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ConstructorInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class ConstructorInfo : MethodBase, _ConstructorInfo
	{
		// Token: 0x060044ED RID: 17645 RVA: 0x000FD104 File Offset: 0x000FB304
		protected ConstructorInfo()
		{
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x000FD10C File Offset: 0x000FB30C
		[__DynamicallyInvokable]
		public static bool operator ==(ConstructorInfo left, ConstructorInfo right)
		{
			return left == right || (left != null && right != null && !(left is RuntimeConstructorInfo) && !(right is RuntimeConstructorInfo) && left.Equals(right));
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x000FD133 File Offset: 0x000FB333
		[__DynamicallyInvokable]
		public static bool operator !=(ConstructorInfo left, ConstructorInfo right)
		{
			return !(left == right);
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x000FD13F File Offset: 0x000FB33F
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x000FD148 File Offset: 0x000FB348
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x000FD150 File Offset: 0x000FB350
		internal virtual Type GetReturnType()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060044F3 RID: 17651 RVA: 0x000FD157 File Offset: 0x000FB357
		[ComVisible(true)]
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Constructor;
			}
		}

		// Token: 0x060044F4 RID: 17652
		public abstract object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x060044F5 RID: 17653 RVA: 0x000FD15A File Offset: 0x000FB35A
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public object Invoke(object[] parameters)
		{
			return this.Invoke(BindingFlags.Default, null, parameters, null);
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x000FD166 File Offset: 0x000FB366
		Type _ConstructorInfo.GetType()
		{
			return base.GetType();
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x000FD16E File Offset: 0x000FB36E
		object _ConstructorInfo.Invoke_2(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.Invoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x000FD17D File Offset: 0x000FB37D
		object _ConstructorInfo.Invoke_3(object obj, object[] parameters)
		{
			return base.Invoke(obj, parameters);
		}

		// Token: 0x060044F9 RID: 17657 RVA: 0x000FD187 File Offset: 0x000FB387
		object _ConstructorInfo.Invoke_4(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.Invoke(invokeAttr, binder, parameters, culture);
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x000FD194 File Offset: 0x000FB394
		object _ConstructorInfo.Invoke_5(object[] parameters)
		{
			return this.Invoke(parameters);
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x000FD19D File Offset: 0x000FB39D
		void _ConstructorInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x000FD1A4 File Offset: 0x000FB3A4
		void _ConstructorInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x000FD1AB File Offset: 0x000FB3AB
		void _ConstructorInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x000FD1B2 File Offset: 0x000FB3B2
		void _ConstructorInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x000FD1B9 File Offset: 0x000FB3B9
		// Note: this type is marked as 'beforefieldinit'.
		static ConstructorInfo()
		{
		}

		// Token: 0x04001C52 RID: 7250
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static readonly string ConstructorName = ".ctor";

		// Token: 0x04001C53 RID: 7251
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static readonly string TypeConstructorName = ".cctor";
	}
}
