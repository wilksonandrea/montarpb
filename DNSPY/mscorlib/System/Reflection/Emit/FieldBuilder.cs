using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x0200063D RID: 1597
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_FieldBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class FieldBuilder : FieldInfo, _FieldBuilder
	{
		// Token: 0x06004A7F RID: 19071 RVA: 0x0010D36C File Offset: 0x0010B56C
		[SecurityCritical]
		internal FieldBuilder(TypeBuilder typeBuilder, string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			if (fieldName == null)
			{
				throw new ArgumentNullException("fieldName");
			}
			if (fieldName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "fieldName");
			}
			if (fieldName[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "fieldName");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type == typeof(void))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadFieldType"));
			}
			this.m_fieldName = fieldName;
			this.m_typeBuilder = typeBuilder;
			this.m_fieldType = type;
			this.m_Attributes = attributes & ~FieldAttributes.ReservedMask;
			SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper(this.m_typeBuilder.Module);
			fieldSigHelper.AddArgument(type, requiredCustomModifiers, optionalCustomModifiers);
			int num;
			byte[] array = fieldSigHelper.InternalGetSignature(out num);
			this.m_fieldTok = TypeBuilder.DefineField(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), typeBuilder.TypeToken.Token, fieldName, array, num, this.m_Attributes);
			this.m_tkField = new FieldToken(this.m_fieldTok, type);
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x0010D48A File Offset: 0x0010B68A
		[SecurityCritical]
		internal void SetData(byte[] data, int size)
		{
			ModuleBuilder.SetFieldRVAContent(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), this.m_tkField.Token, data, size);
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x0010D4AE File Offset: 0x0010B6AE
		internal TypeBuilder GetTypeBuilder()
		{
			return this.m_typeBuilder;
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06004A82 RID: 19074 RVA: 0x0010D4B6 File Offset: 0x0010B6B6
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_fieldTok;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06004A83 RID: 19075 RVA: 0x0010D4BE File Offset: 0x0010B6BE
		public override Module Module
		{
			get
			{
				return this.m_typeBuilder.Module;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06004A84 RID: 19076 RVA: 0x0010D4CB File Offset: 0x0010B6CB
		public override string Name
		{
			get
			{
				return this.m_fieldName;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x0010D4D3 File Offset: 0x0010B6D3
		public override Type DeclaringType
		{
			get
			{
				if (this.m_typeBuilder.m_isHiddenGlobalType)
				{
					return null;
				}
				return this.m_typeBuilder;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06004A86 RID: 19078 RVA: 0x0010D4EA File Offset: 0x0010B6EA
		public override Type ReflectedType
		{
			get
			{
				if (this.m_typeBuilder.m_isHiddenGlobalType)
				{
					return null;
				}
				return this.m_typeBuilder;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06004A87 RID: 19079 RVA: 0x0010D501 File Offset: 0x0010B701
		public override Type FieldType
		{
			get
			{
				return this.m_fieldType;
			}
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x0010D509 File Offset: 0x0010B709
		public override object GetValue(object obj)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x0010D51A File Offset: 0x0010B71A
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06004A8A RID: 19082 RVA: 0x0010D52B File Offset: 0x0010B72B
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06004A8B RID: 19083 RVA: 0x0010D53C File Offset: 0x0010B73C
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_Attributes;
			}
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x0010D544 File Offset: 0x0010B744
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x0010D555 File Offset: 0x0010B755
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x0010D566 File Offset: 0x0010B766
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x0010D577 File Offset: 0x0010B777
		public FieldToken GetToken()
		{
			return this.m_tkField;
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x0010D580 File Offset: 0x0010B780
		[SecuritySafeCritical]
		public void SetOffset(int iOffset)
		{
			this.m_typeBuilder.ThrowIfCreated();
			TypeBuilder.SetFieldLayoutOffset(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), this.GetToken().Token, iOffset);
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x0010D5BC File Offset: 0x0010B7BC
		[SecuritySafeCritical]
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			if (unmanagedMarshal == null)
			{
				throw new ArgumentNullException("unmanagedMarshal");
			}
			this.m_typeBuilder.ThrowIfCreated();
			byte[] array = unmanagedMarshal.InternalGetBytes();
			TypeBuilder.SetFieldMarshal(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), this.GetToken().Token, array, array.Length);
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x0010D610 File Offset: 0x0010B810
		[SecuritySafeCritical]
		public void SetConstant(object defaultValue)
		{
			this.m_typeBuilder.ThrowIfCreated();
			TypeBuilder.SetConstantValue(this.m_typeBuilder.GetModuleBuilder(), this.GetToken().Token, this.m_fieldType, defaultValue);
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x0010D650 File Offset: 0x0010B850
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
			ModuleBuilder moduleBuilder = this.m_typeBuilder.Module as ModuleBuilder;
			this.m_typeBuilder.ThrowIfCreated();
			TypeBuilder.DefineCustomAttribute(moduleBuilder, this.m_tkField.Token, moduleBuilder.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x0010D6C0 File Offset: 0x0010B8C0
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.m_typeBuilder.ThrowIfCreated();
			ModuleBuilder moduleBuilder = this.m_typeBuilder.Module as ModuleBuilder;
			customBuilder.CreateCustomAttribute(moduleBuilder, this.m_tkField.Token);
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x0010D709 File Offset: 0x0010B909
		void _FieldBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x0010D710 File Offset: 0x0010B910
		void _FieldBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x0010D717 File Offset: 0x0010B917
		void _FieldBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x0010D71E File Offset: 0x0010B91E
		void _FieldBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001EBD RID: 7869
		private int m_fieldTok;

		// Token: 0x04001EBE RID: 7870
		private FieldToken m_tkField;

		// Token: 0x04001EBF RID: 7871
		private TypeBuilder m_typeBuilder;

		// Token: 0x04001EC0 RID: 7872
		private string m_fieldName;

		// Token: 0x04001EC1 RID: 7873
		private FieldAttributes m_Attributes;

		// Token: 0x04001EC2 RID: 7874
		private Type m_fieldType;
	}
}
