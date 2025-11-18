using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x0200065D RID: 1629
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ParameterBuilder))]
	[ComVisible(true)]
	public class ParameterBuilder : _ParameterBuilder
	{
		// Token: 0x06004CBD RID: 19645 RVA: 0x00116B44 File Offset: 0x00114D44
		[SecuritySafeCritical]
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			if (unmanagedMarshal == null)
			{
				throw new ArgumentNullException("unmanagedMarshal");
			}
			byte[] array = unmanagedMarshal.InternalGetBytes();
			TypeBuilder.SetFieldMarshal(this.m_methodBuilder.GetModuleBuilder().GetNativeHandle(), this.m_pdToken.Token, array, array.Length);
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x00116B8C File Offset: 0x00114D8C
		[SecuritySafeCritical]
		public virtual void SetConstant(object defaultValue)
		{
			TypeBuilder.SetConstantValue(this.m_methodBuilder.GetModuleBuilder(), this.m_pdToken.Token, (this.m_iPosition == 0) ? this.m_methodBuilder.ReturnType : this.m_methodBuilder.m_parameterTypes[this.m_iPosition - 1], defaultValue);
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x00116BE0 File Offset: 0x00114DE0
		[SecuritySafeCritical]
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			TypeBuilder.DefineCustomAttribute(this.m_methodBuilder.GetModuleBuilder(), this.m_pdToken.Token, ((ModuleBuilder)this.m_methodBuilder.GetModule()).GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x00116C4B File Offset: 0x00114E4B
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			customBuilder.CreateCustomAttribute((ModuleBuilder)this.m_methodBuilder.GetModule(), this.m_pdToken.Token);
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x00116C7C File Offset: 0x00114E7C
		private ParameterBuilder()
		{
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x00116C84 File Offset: 0x00114E84
		[SecurityCritical]
		internal ParameterBuilder(MethodBuilder methodBuilder, int sequence, ParameterAttributes attributes, string strParamName)
		{
			this.m_iPosition = sequence;
			this.m_strParamName = strParamName;
			this.m_methodBuilder = methodBuilder;
			this.m_strParamName = strParamName;
			this.m_attributes = attributes;
			this.m_pdToken = new ParameterToken(TypeBuilder.SetParamInfo(this.m_methodBuilder.GetModuleBuilder().GetNativeHandle(), this.m_methodBuilder.GetToken().Token, sequence, attributes, strParamName));
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x00116CF3 File Offset: 0x00114EF3
		public virtual ParameterToken GetToken()
		{
			return this.m_pdToken;
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x00116CFB File Offset: 0x00114EFB
		void _ParameterBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x00116D02 File Offset: 0x00114F02
		void _ParameterBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x00116D09 File Offset: 0x00114F09
		void _ParameterBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x00116D10 File Offset: 0x00114F10
		void _ParameterBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06004CC8 RID: 19656 RVA: 0x00116D17 File Offset: 0x00114F17
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_pdToken.Token;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06004CC9 RID: 19657 RVA: 0x00116D24 File Offset: 0x00114F24
		public virtual string Name
		{
			get
			{
				return this.m_strParamName;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06004CCA RID: 19658 RVA: 0x00116D2C File Offset: 0x00114F2C
		public virtual int Position
		{
			get
			{
				return this.m_iPosition;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06004CCB RID: 19659 RVA: 0x00116D34 File Offset: 0x00114F34
		public virtual int Attributes
		{
			get
			{
				return (int)this.m_attributes;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06004CCC RID: 19660 RVA: 0x00116D3C File Offset: 0x00114F3C
		public bool IsIn
		{
			get
			{
				return (this.m_attributes & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06004CCD RID: 19661 RVA: 0x00116D49 File Offset: 0x00114F49
		public bool IsOut
		{
			get
			{
				return (this.m_attributes & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06004CCE RID: 19662 RVA: 0x00116D56 File Offset: 0x00114F56
		public bool IsOptional
		{
			get
			{
				return (this.m_attributes & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		// Token: 0x04002192 RID: 8594
		private string m_strParamName;

		// Token: 0x04002193 RID: 8595
		private int m_iPosition;

		// Token: 0x04002194 RID: 8596
		private ParameterAttributes m_attributes;

		// Token: 0x04002195 RID: 8597
		private MethodBuilder m_methodBuilder;

		// Token: 0x04002196 RID: 8598
		private ParameterToken m_pdToken;
	}
}
