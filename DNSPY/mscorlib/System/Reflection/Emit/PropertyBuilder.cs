using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x0200065F RID: 1631
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_PropertyBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class PropertyBuilder : PropertyInfo, _PropertyBuilder
	{
		// Token: 0x06004CD7 RID: 19671 RVA: 0x00116DBD File Offset: 0x00114FBD
		private PropertyBuilder()
		{
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x00116DC8 File Offset: 0x00114FC8
		internal PropertyBuilder(ModuleBuilder mod, string name, SignatureHelper sig, PropertyAttributes attr, Type returnType, PropertyToken prToken, TypeBuilder containingType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "name");
			}
			this.m_name = name;
			this.m_moduleBuilder = mod;
			this.m_signature = sig;
			this.m_attributes = attr;
			this.m_returnType = returnType;
			this.m_prToken = prToken;
			this.m_tkProperty = prToken.Token;
			this.m_containingType = containingType;
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x00116E66 File Offset: 0x00115066
		[SecuritySafeCritical]
		public void SetConstant(object defaultValue)
		{
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.SetConstantValue(this.m_moduleBuilder, this.m_prToken.Token, this.m_returnType, defaultValue);
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06004CDA RID: 19674 RVA: 0x00116E90 File Offset: 0x00115090
		public PropertyToken PropertyToken
		{
			get
			{
				return this.m_prToken;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06004CDB RID: 19675 RVA: 0x00116E98 File Offset: 0x00115098
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_tkProperty;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06004CDC RID: 19676 RVA: 0x00116EA0 File Offset: 0x001150A0
		public override Module Module
		{
			get
			{
				return this.m_containingType.Module;
			}
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x00116EB0 File Offset: 0x001150B0
		[SecurityCritical]
		private void SetMethodSemantics(MethodBuilder mdBuilder, MethodSemanticsAttributes semantics)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.DefineMethodSemantics(this.m_moduleBuilder.GetNativeHandle(), this.m_prToken.Token, semantics, mdBuilder.GetToken().Token);
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x00116F06 File Offset: 0x00115106
		[SecuritySafeCritical]
		public void SetGetMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Getter);
			this.m_getMethod = mdBuilder;
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x00116F17 File Offset: 0x00115117
		[SecuritySafeCritical]
		public void SetSetMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Setter);
			this.m_setMethod = mdBuilder;
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x00116F28 File Offset: 0x00115128
		[SecuritySafeCritical]
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Other);
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x00116F34 File Offset: 0x00115134
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
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.DefineCustomAttribute(this.m_moduleBuilder, this.m_prToken.Token, this.m_moduleBuilder.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x00116F9B File Offset: 0x0011519B
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.m_containingType.ThrowIfCreated();
			customBuilder.CreateCustomAttribute(this.m_moduleBuilder, this.m_prToken.Token);
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x00116FCD File Offset: 0x001151CD
		public override object GetValue(object obj, object[] index)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x00116FDE File Offset: 0x001151DE
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x00116FEF File Offset: 0x001151EF
		public override void SetValue(object obj, object value, object[] index)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x00117000 File Offset: 0x00115200
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x00117011 File Offset: 0x00115211
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x00117022 File Offset: 0x00115222
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			if (nonPublic || this.m_getMethod == null)
			{
				return this.m_getMethod;
			}
			if ((this.m_getMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
			{
				return this.m_getMethod;
			}
			return null;
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x00117054 File Offset: 0x00115254
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			if (nonPublic || this.m_setMethod == null)
			{
				return this.m_setMethod;
			}
			if ((this.m_setMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
			{
				return this.m_setMethod;
			}
			return null;
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x00117086 File Offset: 0x00115286
		public override ParameterInfo[] GetIndexParameters()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06004CEB RID: 19691 RVA: 0x00117097 File Offset: 0x00115297
		public override Type PropertyType
		{
			get
			{
				return this.m_returnType;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06004CEC RID: 19692 RVA: 0x0011709F File Offset: 0x0011529F
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.m_attributes;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06004CED RID: 19693 RVA: 0x001170A7 File Offset: 0x001152A7
		public override bool CanRead
		{
			get
			{
				return this.m_getMethod != null;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06004CEE RID: 19694 RVA: 0x001170BA File Offset: 0x001152BA
		public override bool CanWrite
		{
			get
			{
				return this.m_setMethod != null;
			}
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x001170CD File Offset: 0x001152CD
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x001170DE File Offset: 0x001152DE
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x001170EF File Offset: 0x001152EF
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x00117100 File Offset: 0x00115300
		void _PropertyBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x00117107 File Offset: 0x00115307
		void _PropertyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x0011710E File Offset: 0x0011530E
		void _PropertyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x00117115 File Offset: 0x00115315
		void _PropertyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06004CF6 RID: 19702 RVA: 0x0011711C File Offset: 0x0011531C
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06004CF7 RID: 19703 RVA: 0x00117124 File Offset: 0x00115324
		public override Type DeclaringType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06004CF8 RID: 19704 RVA: 0x0011712C File Offset: 0x0011532C
		public override Type ReflectedType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x04002199 RID: 8601
		private string m_name;

		// Token: 0x0400219A RID: 8602
		private PropertyToken m_prToken;

		// Token: 0x0400219B RID: 8603
		private int m_tkProperty;

		// Token: 0x0400219C RID: 8604
		private ModuleBuilder m_moduleBuilder;

		// Token: 0x0400219D RID: 8605
		private SignatureHelper m_signature;

		// Token: 0x0400219E RID: 8606
		private PropertyAttributes m_attributes;

		// Token: 0x0400219F RID: 8607
		private Type m_returnType;

		// Token: 0x040021A0 RID: 8608
		private MethodInfo m_getMethod;

		// Token: 0x040021A1 RID: 8609
		private MethodInfo m_setMethod;

		// Token: 0x040021A2 RID: 8610
		private TypeBuilder m_containingType;
	}
}
