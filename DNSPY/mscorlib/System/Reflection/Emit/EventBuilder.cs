using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x0200063B RID: 1595
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_EventBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventBuilder : _EventBuilder
	{
		// Token: 0x06004A69 RID: 19049 RVA: 0x0010D19F File Offset: 0x0010B39F
		private EventBuilder()
		{
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x0010D1A7 File Offset: 0x0010B3A7
		internal EventBuilder(ModuleBuilder mod, string name, EventAttributes attr, TypeBuilder type, EventToken evToken)
		{
			this.m_name = name;
			this.m_module = mod;
			this.m_attributes = attr;
			this.m_evToken = evToken;
			this.m_type = type;
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x0010D1D4 File Offset: 0x0010B3D4
		public EventToken GetEventToken()
		{
			return this.m_evToken;
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x0010D1DC File Offset: 0x0010B3DC
		[SecurityCritical]
		private void SetMethodSemantics(MethodBuilder mdBuilder, MethodSemanticsAttributes semantics)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_type.ThrowIfCreated();
			TypeBuilder.DefineMethodSemantics(this.m_module.GetNativeHandle(), this.m_evToken.Token, semantics, mdBuilder.GetToken().Token);
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x0010D232 File Offset: 0x0010B432
		[SecuritySafeCritical]
		public void SetAddOnMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.AddOn);
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x0010D23C File Offset: 0x0010B43C
		[SecuritySafeCritical]
		public void SetRemoveOnMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.RemoveOn);
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x0010D247 File Offset: 0x0010B447
		[SecuritySafeCritical]
		public void SetRaiseMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Fire);
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x0010D252 File Offset: 0x0010B452
		[SecuritySafeCritical]
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Other);
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x0010D25C File Offset: 0x0010B45C
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
			this.m_type.ThrowIfCreated();
			TypeBuilder.DefineCustomAttribute(this.m_module, this.m_evToken.Token, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x0010D2C3 File Offset: 0x0010B4C3
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.m_type.ThrowIfCreated();
			customBuilder.CreateCustomAttribute(this.m_module, this.m_evToken.Token);
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x0010D2F5 File Offset: 0x0010B4F5
		void _EventBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x0010D2FC File Offset: 0x0010B4FC
		void _EventBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x0010D303 File Offset: 0x0010B503
		void _EventBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x0010D30A File Offset: 0x0010B50A
		void _EventBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001EB6 RID: 7862
		private string m_name;

		// Token: 0x04001EB7 RID: 7863
		private EventToken m_evToken;

		// Token: 0x04001EB8 RID: 7864
		private ModuleBuilder m_module;

		// Token: 0x04001EB9 RID: 7865
		private EventAttributes m_attributes;

		// Token: 0x04001EBA RID: 7866
		private TypeBuilder m_type;
	}
}
