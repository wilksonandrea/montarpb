using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005EA RID: 1514
	[Serializable]
	internal sealed class MdFieldInfo : RuntimeFieldInfo, ISerializable
	{
		// Token: 0x06004642 RID: 17986 RVA: 0x00102104 File Offset: 0x00100304
		internal MdFieldInfo(int tkField, FieldAttributes fieldAttributes, RuntimeTypeHandle declaringTypeHandle, RuntimeType.RuntimeTypeCache reflectedTypeCache, BindingFlags bindingFlags)
			: base(reflectedTypeCache, declaringTypeHandle.GetRuntimeType(), bindingFlags)
		{
			this.m_tkField = tkField;
			this.m_name = null;
			this.m_fieldAttributes = fieldAttributes;
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x0010212C File Offset: 0x0010032C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			MdFieldInfo mdFieldInfo = o as MdFieldInfo;
			return mdFieldInfo != null && mdFieldInfo.m_tkField == this.m_tkField && this.m_declaringType.GetTypeHandleInternal().GetModuleHandle().Equals(mdFieldInfo.m_declaringType.GetTypeHandleInternal().GetModuleHandle());
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x00102184 File Offset: 0x00100384
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_name == null)
				{
					this.m_name = this.GetRuntimeModule().MetadataImport.GetName(this.m_tkField).ToString();
				}
				return this.m_name;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06004645 RID: 17989 RVA: 0x001021CC File Offset: 0x001003CC
		public override int MetadataToken
		{
			get
			{
				return this.m_tkField;
			}
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x001021D4 File Offset: 0x001003D4
		internal override RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06004647 RID: 17991 RVA: 0x001021E1 File Offset: 0x001003E1
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06004648 RID: 17992 RVA: 0x001021E8 File Offset: 0x001003E8
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_fieldAttributes;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06004649 RID: 17993 RVA: 0x001021F0 File Offset: 0x001003F0
		public override bool IsSecurityCritical
		{
			get
			{
				return this.DeclaringType.IsSecurityCritical;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x0600464A RID: 17994 RVA: 0x001021FD File Offset: 0x001003FD
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.DeclaringType.IsSecuritySafeCritical;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x0600464B RID: 17995 RVA: 0x0010220A File Offset: 0x0010040A
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.DeclaringType.IsSecurityTransparent;
			}
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x00102217 File Offset: 0x00100417
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object GetValueDirect(TypedReference obj)
		{
			return this.GetValue(null);
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x00102220 File Offset: 0x00100420
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValueDirect(TypedReference obj, object value)
		{
			throw new FieldAccessException(Environment.GetResourceString("Acc_ReadOnly"));
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x00102231 File Offset: 0x00100431
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object GetValue(object obj)
		{
			return this.GetValue(false);
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x0010223A File Offset: 0x0010043A
		public override object GetRawConstantValue()
		{
			return this.GetValue(true);
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x00102244 File Offset: 0x00100444
		[SecuritySafeCritical]
		private object GetValue(bool raw)
		{
			object value = MdConstant.GetValue(this.GetRuntimeModule().MetadataImport, this.m_tkField, this.FieldType.GetTypeHandleInternal(), raw);
			if (value == DBNull.Value)
			{
				throw new NotSupportedException(Environment.GetResourceString("Arg_EnumLitValueNotFound"));
			}
			return value;
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x0010228D File Offset: 0x0010048D
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new FieldAccessException(Environment.GetResourceString("Acc_ReadOnly"));
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06004652 RID: 18002 RVA: 0x001022A0 File Offset: 0x001004A0
		public override Type FieldType
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_fieldType == null)
				{
					ConstArray sigOfFieldDef = this.GetRuntimeModule().MetadataImport.GetSigOfFieldDef(this.m_tkField);
					this.m_fieldType = new Signature(sigOfFieldDef.Signature.ToPointer(), sigOfFieldDef.Length, this.m_declaringType).FieldType;
				}
				return this.m_fieldType;
			}
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x00102307 File Offset: 0x00100507
		public override Type[] GetRequiredCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x0010230E File Offset: 0x0010050E
		public override Type[] GetOptionalCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x04001CC9 RID: 7369
		private int m_tkField;

		// Token: 0x04001CCA RID: 7370
		private string m_name;

		// Token: 0x04001CCB RID: 7371
		private RuntimeType m_fieldType;

		// Token: 0x04001CCC RID: 7372
		private FieldAttributes m_fieldAttributes;
	}
}
