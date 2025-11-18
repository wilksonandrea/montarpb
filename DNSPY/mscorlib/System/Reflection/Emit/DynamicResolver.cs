using System;
using System.Security;
using System.Threading;

namespace System.Reflection.Emit
{
	// Token: 0x02000634 RID: 1588
	internal class DynamicResolver : Resolver
	{
		// Token: 0x06004A03 RID: 18947 RVA: 0x0010BA64 File Offset: 0x00109C64
		internal DynamicResolver(DynamicILGenerator ilGenerator)
		{
			this.m_stackSize = ilGenerator.GetMaxStackSize();
			this.m_exceptions = ilGenerator.GetExceptions();
			this.m_code = ilGenerator.BakeByteArray();
			this.m_localSignature = ilGenerator.m_localSignature.InternalGetSignatureArray();
			this.m_scope = ilGenerator.m_scope;
			this.m_method = (DynamicMethod)ilGenerator.m_methodBuilder;
			this.m_method.m_resolver = this;
		}

		// Token: 0x06004A04 RID: 18948 RVA: 0x0010BAD8 File Offset: 0x00109CD8
		internal DynamicResolver(DynamicILInfo dynamicILInfo)
		{
			this.m_stackSize = dynamicILInfo.MaxStackSize;
			this.m_code = dynamicILInfo.Code;
			this.m_localSignature = dynamicILInfo.LocalSignature;
			this.m_exceptionHeader = dynamicILInfo.Exceptions;
			this.m_scope = dynamicILInfo.DynamicScope;
			this.m_method = dynamicILInfo.DynamicMethod;
			this.m_method.m_resolver = this;
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x0010BB40 File Offset: 0x00109D40
		protected override void Finalize()
		{
			try
			{
				DynamicMethod method = this.m_method;
				if (!(method == null))
				{
					if (method.m_methodHandle != null)
					{
						DynamicResolver.DestroyScout destroyScout = null;
						try
						{
							destroyScout = new DynamicResolver.DestroyScout();
						}
						catch
						{
							if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
							{
								GC.ReRegisterForFinalize(this);
							}
							return;
						}
						destroyScout.m_methodHandle = method.m_methodHandle.Value;
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06004A06 RID: 18950 RVA: 0x0010BBC8 File Offset: 0x00109DC8
		internal override RuntimeType GetJitContext(ref int securityControlFlags)
		{
			DynamicResolver.SecurityControlFlags securityControlFlags2 = DynamicResolver.SecurityControlFlags.Default;
			if (this.m_method.m_restrictedSkipVisibility)
			{
				securityControlFlags2 |= DynamicResolver.SecurityControlFlags.RestrictedSkipVisibilityChecks;
			}
			else if (this.m_method.m_skipVisibility)
			{
				securityControlFlags2 |= DynamicResolver.SecurityControlFlags.SkipVisibilityChecks;
			}
			RuntimeType typeOwner = this.m_method.m_typeOwner;
			if (this.m_method.m_creationContext != null)
			{
				securityControlFlags2 |= DynamicResolver.SecurityControlFlags.HasCreationContext;
				if (this.m_method.m_creationContext.CanSkipEvaluation)
				{
					securityControlFlags2 |= DynamicResolver.SecurityControlFlags.CanSkipCSEvaluation;
				}
			}
			securityControlFlags = (int)securityControlFlags2;
			return typeOwner;
		}

		// Token: 0x06004A07 RID: 18951 RVA: 0x0010BC34 File Offset: 0x00109E34
		private static int CalculateNumberOfExceptions(__ExceptionInfo[] excp)
		{
			int num = 0;
			if (excp == null)
			{
				return 0;
			}
			for (int i = 0; i < excp.Length; i++)
			{
				num += excp[i].GetNumberOfCatches();
			}
			return num;
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x0010BC64 File Offset: 0x00109E64
		internal override byte[] GetCodeInfo(ref int stackSize, ref int initLocals, ref int EHCount)
		{
			stackSize = this.m_stackSize;
			if (this.m_exceptionHeader != null && this.m_exceptionHeader.Length != 0)
			{
				if (this.m_exceptionHeader.Length < 4)
				{
					throw new FormatException();
				}
				byte b = this.m_exceptionHeader[0];
				if ((b & 64) != 0)
				{
					byte[] array = new byte[4];
					for (int i = 0; i < 3; i++)
					{
						array[i] = this.m_exceptionHeader[i + 1];
					}
					EHCount = (BitConverter.ToInt32(array, 0) - 4) / 24;
				}
				else
				{
					EHCount = (int)((this.m_exceptionHeader[1] - 2) / 12);
				}
			}
			else
			{
				EHCount = DynamicResolver.CalculateNumberOfExceptions(this.m_exceptions);
			}
			initLocals = (this.m_method.InitLocals ? 1 : 0);
			return this.m_code;
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x0010BD11 File Offset: 0x00109F11
		internal override byte[] GetLocalsSignature()
		{
			return this.m_localSignature;
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x0010BD19 File Offset: 0x00109F19
		internal override byte[] GetRawEHInfo()
		{
			return this.m_exceptionHeader;
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x0010BD24 File Offset: 0x00109F24
		[SecurityCritical]
		internal unsafe override void GetEHInfo(int excNumber, void* exc)
		{
			for (int i = 0; i < this.m_exceptions.Length; i++)
			{
				int numberOfCatches = this.m_exceptions[i].GetNumberOfCatches();
				if (excNumber < numberOfCatches)
				{
					((Resolver.CORINFO_EH_CLAUSE*)exc)->Flags = this.m_exceptions[i].GetExceptionTypes()[excNumber];
					((Resolver.CORINFO_EH_CLAUSE*)exc)->TryOffset = this.m_exceptions[i].GetStartAddress();
					if ((((Resolver.CORINFO_EH_CLAUSE*)exc)->Flags & 2) != 2)
					{
						((Resolver.CORINFO_EH_CLAUSE*)exc)->TryLength = this.m_exceptions[i].GetEndAddress() - ((Resolver.CORINFO_EH_CLAUSE*)exc)->TryOffset;
					}
					else
					{
						((Resolver.CORINFO_EH_CLAUSE*)exc)->TryLength = this.m_exceptions[i].GetFinallyEndAddress() - ((Resolver.CORINFO_EH_CLAUSE*)exc)->TryOffset;
					}
					((Resolver.CORINFO_EH_CLAUSE*)exc)->HandlerOffset = this.m_exceptions[i].GetCatchAddresses()[excNumber];
					((Resolver.CORINFO_EH_CLAUSE*)exc)->HandlerLength = this.m_exceptions[i].GetCatchEndAddresses()[excNumber] - ((Resolver.CORINFO_EH_CLAUSE*)exc)->HandlerOffset;
					((Resolver.CORINFO_EH_CLAUSE*)exc)->ClassTokenOrFilterOffset = this.m_exceptions[i].GetFilterAddresses()[excNumber];
					return;
				}
				excNumber -= numberOfCatches;
			}
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x0010BE16 File Offset: 0x0010A016
		internal override string GetStringLiteral(int token)
		{
			return this.m_scope.GetString(token);
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x0010BE24 File Offset: 0x0010A024
		internal override CompressedStack GetSecurityContext()
		{
			return this.m_method.m_creationContext;
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x0010BE34 File Offset: 0x0010A034
		[SecurityCritical]
		internal override void ResolveToken(int token, out IntPtr typeHandle, out IntPtr methodHandle, out IntPtr fieldHandle)
		{
			typeHandle = 0;
			methodHandle = 0;
			fieldHandle = 0;
			object obj = this.m_scope[token];
			if (obj == null)
			{
				throw new InvalidProgramException();
			}
			if (obj is RuntimeTypeHandle)
			{
				typeHandle = ((RuntimeTypeHandle)obj).Value;
				return;
			}
			if (obj is RuntimeMethodHandle)
			{
				methodHandle = ((RuntimeMethodHandle)obj).Value;
				return;
			}
			if (obj is RuntimeFieldHandle)
			{
				fieldHandle = ((RuntimeFieldHandle)obj).Value;
				return;
			}
			DynamicMethod dynamicMethod = obj as DynamicMethod;
			if (dynamicMethod != null)
			{
				methodHandle = dynamicMethod.GetMethodDescriptor().Value;
				return;
			}
			GenericMethodInfo genericMethodInfo = obj as GenericMethodInfo;
			if (genericMethodInfo != null)
			{
				methodHandle = genericMethodInfo.m_methodHandle.Value;
				typeHandle = genericMethodInfo.m_context.Value;
				return;
			}
			GenericFieldInfo genericFieldInfo = obj as GenericFieldInfo;
			if (genericFieldInfo != null)
			{
				fieldHandle = genericFieldInfo.m_fieldHandle.Value;
				typeHandle = genericFieldInfo.m_context.Value;
				return;
			}
			VarArgMethod varArgMethod = obj as VarArgMethod;
			if (varArgMethod == null)
			{
				return;
			}
			if (varArgMethod.m_dynamicMethod == null)
			{
				methodHandle = varArgMethod.m_method.MethodHandle.Value;
				typeHandle = varArgMethod.m_method.GetDeclaringTypeInternal().GetTypeHandleInternal().Value;
				return;
			}
			methodHandle = varArgMethod.m_dynamicMethod.GetMethodDescriptor().Value;
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x0010BF90 File Offset: 0x0010A190
		internal override byte[] ResolveSignature(int token, int fromMethod)
		{
			return this.m_scope.ResolveSignature(token, fromMethod);
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x0010BF9F File Offset: 0x0010A19F
		internal override MethodInfo GetDynamicMethod()
		{
			return this.m_method.GetMethodInfo();
		}

		// Token: 0x04001E8F RID: 7823
		private __ExceptionInfo[] m_exceptions;

		// Token: 0x04001E90 RID: 7824
		private byte[] m_exceptionHeader;

		// Token: 0x04001E91 RID: 7825
		private DynamicMethod m_method;

		// Token: 0x04001E92 RID: 7826
		private byte[] m_code;

		// Token: 0x04001E93 RID: 7827
		private byte[] m_localSignature;

		// Token: 0x04001E94 RID: 7828
		private int m_stackSize;

		// Token: 0x04001E95 RID: 7829
		private DynamicScope m_scope;

		// Token: 0x02000C41 RID: 3137
		private class DestroyScout
		{
			// Token: 0x06007054 RID: 28756 RVA: 0x00182E24 File Offset: 0x00181024
			[SecuritySafeCritical]
			~DestroyScout()
			{
				if (!this.m_methodHandle.IsNullHandle())
				{
					if (RuntimeMethodHandle.GetResolver(this.m_methodHandle) != null)
					{
						if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
						{
							GC.ReRegisterForFinalize(this);
						}
					}
					else
					{
						RuntimeMethodHandle.Destroy(this.m_methodHandle);
					}
				}
			}

			// Token: 0x06007055 RID: 28757 RVA: 0x00182E8C File Offset: 0x0018108C
			public DestroyScout()
			{
			}

			// Token: 0x04003755 RID: 14165
			internal RuntimeMethodHandleInternal m_methodHandle;
		}

		// Token: 0x02000C42 RID: 3138
		[Flags]
		internal enum SecurityControlFlags
		{
			// Token: 0x04003757 RID: 14167
			Default = 0,
			// Token: 0x04003758 RID: 14168
			SkipVisibilityChecks = 1,
			// Token: 0x04003759 RID: 14169
			RestrictedSkipVisibilityChecks = 2,
			// Token: 0x0400375A RID: 14170
			HasCreationContext = 4,
			// Token: 0x0400375B RID: 14171
			CanSkipCSEvaluation = 8
		}
	}
}
