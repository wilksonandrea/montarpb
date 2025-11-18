using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x02000635 RID: 1589
	[ComVisible(true)]
	public class DynamicILInfo
	{
		// Token: 0x06004A11 RID: 18961 RVA: 0x0010BFAC File Offset: 0x0010A1AC
		internal DynamicILInfo(DynamicScope scope, DynamicMethod method, byte[] methodSignature)
		{
			this.m_method = method;
			this.m_scope = scope;
			this.m_methodSignature = this.m_scope.GetTokenFor(methodSignature);
			this.m_exceptions = EmptyArray<byte>.Value;
			this.m_code = EmptyArray<byte>.Value;
			this.m_localSignature = EmptyArray<byte>.Value;
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x0010C000 File Offset: 0x0010A200
		[SecurityCritical]
		internal void GetCallableMethod(RuntimeModule module, DynamicMethod dm)
		{
			dm.m_methodHandle = ModuleHandle.GetDynamicMethod(dm, module, this.m_method.Name, (byte[])this.m_scope[this.m_methodSignature], new DynamicResolver(this));
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06004A13 RID: 18963 RVA: 0x0010C036 File Offset: 0x0010A236
		internal byte[] LocalSignature
		{
			get
			{
				if (this.m_localSignature == null)
				{
					this.m_localSignature = SignatureHelper.GetLocalVarSigHelper().InternalGetSignatureArray();
				}
				return this.m_localSignature;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06004A14 RID: 18964 RVA: 0x0010C056 File Offset: 0x0010A256
		internal byte[] Exceptions
		{
			get
			{
				return this.m_exceptions;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06004A15 RID: 18965 RVA: 0x0010C05E File Offset: 0x0010A25E
		internal byte[] Code
		{
			get
			{
				return this.m_code;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06004A16 RID: 18966 RVA: 0x0010C066 File Offset: 0x0010A266
		internal int MaxStackSize
		{
			get
			{
				return this.m_maxStackSize;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06004A17 RID: 18967 RVA: 0x0010C06E File Offset: 0x0010A26E
		public DynamicMethod DynamicMethod
		{
			get
			{
				return this.m_method;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06004A18 RID: 18968 RVA: 0x0010C076 File Offset: 0x0010A276
		internal DynamicScope DynamicScope
		{
			get
			{
				return this.m_scope;
			}
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x0010C07E File Offset: 0x0010A27E
		public void SetCode(byte[] code, int maxStackSize)
		{
			this.m_code = ((code != null) ? ((byte[])code.Clone()) : EmptyArray<byte>.Value);
			this.m_maxStackSize = maxStackSize;
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x0010C0A4 File Offset: 0x0010A2A4
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe void SetCode(byte* code, int codeSize, int maxStackSize)
		{
			if (codeSize < 0)
			{
				throw new ArgumentOutOfRangeException("codeSize", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (codeSize > 0 && code == null)
			{
				throw new ArgumentNullException("code");
			}
			this.m_code = new byte[codeSize];
			for (int i = 0; i < codeSize; i++)
			{
				this.m_code[i] = *code;
				code++;
			}
			this.m_maxStackSize = maxStackSize;
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x0010C10C File Offset: 0x0010A30C
		public void SetExceptions(byte[] exceptions)
		{
			this.m_exceptions = ((exceptions != null) ? ((byte[])exceptions.Clone()) : EmptyArray<byte>.Value);
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x0010C12C File Offset: 0x0010A32C
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe void SetExceptions(byte* exceptions, int exceptionsSize)
		{
			if (exceptionsSize < 0)
			{
				throw new ArgumentOutOfRangeException("exceptionsSize", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (exceptionsSize > 0 && exceptions == null)
			{
				throw new ArgumentNullException("exceptions");
			}
			this.m_exceptions = new byte[exceptionsSize];
			for (int i = 0; i < exceptionsSize; i++)
			{
				this.m_exceptions[i] = *exceptions;
				exceptions++;
			}
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x0010C18D File Offset: 0x0010A38D
		public void SetLocalSignature(byte[] localSignature)
		{
			this.m_localSignature = ((localSignature != null) ? ((byte[])localSignature.Clone()) : EmptyArray<byte>.Value);
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x0010C1AC File Offset: 0x0010A3AC
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe void SetLocalSignature(byte* localSignature, int signatureSize)
		{
			if (signatureSize < 0)
			{
				throw new ArgumentOutOfRangeException("signatureSize", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (signatureSize > 0 && localSignature == null)
			{
				throw new ArgumentNullException("localSignature");
			}
			this.m_localSignature = new byte[signatureSize];
			for (int i = 0; i < signatureSize; i++)
			{
				this.m_localSignature[i] = *localSignature;
				localSignature++;
			}
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x0010C20D File Offset: 0x0010A40D
		[SecuritySafeCritical]
		public int GetTokenFor(RuntimeMethodHandle method)
		{
			return this.DynamicScope.GetTokenFor(method);
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x0010C21B File Offset: 0x0010A41B
		public int GetTokenFor(DynamicMethod method)
		{
			return this.DynamicScope.GetTokenFor(method);
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x0010C229 File Offset: 0x0010A429
		public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle contextType)
		{
			return this.DynamicScope.GetTokenFor(method, contextType);
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x0010C238 File Offset: 0x0010A438
		public int GetTokenFor(RuntimeFieldHandle field)
		{
			return this.DynamicScope.GetTokenFor(field);
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x0010C246 File Offset: 0x0010A446
		public int GetTokenFor(RuntimeFieldHandle field, RuntimeTypeHandle contextType)
		{
			return this.DynamicScope.GetTokenFor(field, contextType);
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x0010C255 File Offset: 0x0010A455
		public int GetTokenFor(RuntimeTypeHandle type)
		{
			return this.DynamicScope.GetTokenFor(type);
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x0010C263 File Offset: 0x0010A463
		public int GetTokenFor(string literal)
		{
			return this.DynamicScope.GetTokenFor(literal);
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x0010C271 File Offset: 0x0010A471
		public int GetTokenFor(byte[] signature)
		{
			return this.DynamicScope.GetTokenFor(signature);
		}

		// Token: 0x04001E96 RID: 7830
		private DynamicMethod m_method;

		// Token: 0x04001E97 RID: 7831
		private DynamicScope m_scope;

		// Token: 0x04001E98 RID: 7832
		private byte[] m_exceptions;

		// Token: 0x04001E99 RID: 7833
		private byte[] m_code;

		// Token: 0x04001E9A RID: 7834
		private byte[] m_localSignature;

		// Token: 0x04001E9B RID: 7835
		private int m_maxStackSize;

		// Token: 0x04001E9C RID: 7836
		private int m_methodSignature;
	}
}
