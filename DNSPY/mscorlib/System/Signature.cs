using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x0200013A RID: 314
	internal class Signature
	{
		// Token: 0x060012D3 RID: 4819
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void GetSignature(void* pCorSig, int cCorSig, RuntimeFieldHandleInternal fieldHandle, IRuntimeMethodInfo methodHandle, RuntimeType declaringType);

		// Token: 0x060012D4 RID: 4820 RVA: 0x00037FB0 File Offset: 0x000361B0
		[SecuritySafeCritical]
		public Signature(IRuntimeMethodInfo method, RuntimeType[] arguments, RuntimeType returnType, CallingConventions callingConvention)
		{
			this.m_pMethod = method.Value;
			this.m_arguments = arguments;
			this.m_returnTypeORfieldType = returnType;
			this.m_managedCallingConventionAndArgIteratorFlags = (int)((byte)callingConvention);
			this.GetSignature(null, 0, default(RuntimeFieldHandleInternal), method, null);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00037FFC File Offset: 0x000361FC
		[SecuritySafeCritical]
		public Signature(IRuntimeMethodInfo methodHandle, RuntimeType declaringType)
		{
			this.GetSignature(null, 0, default(RuntimeFieldHandleInternal), methodHandle, declaringType);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00038023 File Offset: 0x00036223
		[SecurityCritical]
		public Signature(IRuntimeFieldInfo fieldHandle, RuntimeType declaringType)
		{
			this.GetSignature(null, 0, fieldHandle.Value, null, declaringType);
			GC.KeepAlive(fieldHandle);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00038044 File Offset: 0x00036244
		[SecurityCritical]
		public unsafe Signature(void* pCorSig, int cCorSig, RuntimeType declaringType)
		{
			this.GetSignature(pCorSig, cCorSig, default(RuntimeFieldHandleInternal), null, declaringType);
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x0003806A File Offset: 0x0003626A
		internal CallingConventions CallingConvention
		{
			get
			{
				return (CallingConventions)((byte)this.m_managedCallingConventionAndArgIteratorFlags);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00038073 File Offset: 0x00036273
		internal RuntimeType[] Arguments
		{
			get
			{
				return this.m_arguments;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x0003807B File Offset: 0x0003627B
		internal RuntimeType ReturnType
		{
			get
			{
				return this.m_returnTypeORfieldType;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x00038083 File Offset: 0x00036283
		internal RuntimeType FieldType
		{
			get
			{
				return this.m_returnTypeORfieldType;
			}
		}

		// Token: 0x060012DC RID: 4828
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CompareSig(Signature sig1, Signature sig2);

		// Token: 0x060012DD RID: 4829
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type[] GetCustomModifiers(int position, bool required);

		// Token: 0x04000677 RID: 1655
		internal RuntimeType[] m_arguments;

		// Token: 0x04000678 RID: 1656
		internal RuntimeType m_declaringType;

		// Token: 0x04000679 RID: 1657
		internal RuntimeType m_returnTypeORfieldType;

		// Token: 0x0400067A RID: 1658
		internal object m_keepalive;

		// Token: 0x0400067B RID: 1659
		[SecurityCritical]
		internal unsafe void* m_sig;

		// Token: 0x0400067C RID: 1660
		internal int m_managedCallingConventionAndArgIteratorFlags;

		// Token: 0x0400067D RID: 1661
		internal int m_nSizeOfArgStack;

		// Token: 0x0400067E RID: 1662
		internal int m_csig;

		// Token: 0x0400067F RID: 1663
		internal RuntimeMethodHandleInternal m_pMethod;

		// Token: 0x02000AFD RID: 2813
		internal enum MdSigCallingConvention : byte
		{
			// Token: 0x040031F2 RID: 12786
			Generics = 16,
			// Token: 0x040031F3 RID: 12787
			HasThis = 32,
			// Token: 0x040031F4 RID: 12788
			ExplicitThis = 64,
			// Token: 0x040031F5 RID: 12789
			CallConvMask = 15,
			// Token: 0x040031F6 RID: 12790
			Default = 0,
			// Token: 0x040031F7 RID: 12791
			C,
			// Token: 0x040031F8 RID: 12792
			StdCall,
			// Token: 0x040031F9 RID: 12793
			ThisCall,
			// Token: 0x040031FA RID: 12794
			FastCall,
			// Token: 0x040031FB RID: 12795
			Vararg,
			// Token: 0x040031FC RID: 12796
			Field,
			// Token: 0x040031FD RID: 12797
			LocalSig,
			// Token: 0x040031FE RID: 12798
			Property,
			// Token: 0x040031FF RID: 12799
			Unmgd,
			// Token: 0x04003200 RID: 12800
			GenericInst,
			// Token: 0x04003201 RID: 12801
			Max
		}
	}
}
