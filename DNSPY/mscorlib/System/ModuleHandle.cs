using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	// Token: 0x02000139 RID: 313
	[ComVisible(true)]
	public struct ModuleHandle
	{
		// Token: 0x060012A9 RID: 4777 RVA: 0x00037B18 File Offset: 0x00035D18
		private static ModuleHandle GetEmptyMH()
		{
			return default(ModuleHandle);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00037B2E File Offset: 0x00035D2E
		internal ModuleHandle(RuntimeModule module)
		{
			this.m_ptr = module;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00037B37 File Offset: 0x00035D37
		internal RuntimeModule GetRuntimeModule()
		{
			return this.m_ptr;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00037B3F File Offset: 0x00035D3F
		internal bool IsNullHandle()
		{
			return this.m_ptr == null;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00037B4D File Offset: 0x00035D4D
		public override int GetHashCode()
		{
			if (!(this.m_ptr != null))
			{
				return 0;
			}
			return this.m_ptr.GetHashCode();
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00037B6C File Offset: 0x00035D6C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			if (!(obj is ModuleHandle))
			{
				return false;
			}
			ModuleHandle moduleHandle = (ModuleHandle)obj;
			return moduleHandle.m_ptr == this.m_ptr;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00037B9B File Offset: 0x00035D9B
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(ModuleHandle handle)
		{
			return handle.m_ptr == this.m_ptr;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00037BAE File Offset: 0x00035DAE
		public static bool operator ==(ModuleHandle left, ModuleHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00037BB8 File Offset: 0x00035DB8
		public static bool operator !=(ModuleHandle left, ModuleHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060012B2 RID: 4786
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IRuntimeMethodInfo GetDynamicMethod(DynamicMethod method, RuntimeModule module, string name, byte[] sig, Resolver resolver);

		// Token: 0x060012B3 RID: 4787
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetToken(RuntimeModule module);

		// Token: 0x060012B4 RID: 4788 RVA: 0x00037BC5 File Offset: 0x00035DC5
		private static void ValidateModulePointer(RuntimeModule module)
		{
			if (module == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullModuleHandle"));
			}
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00037BE0 File Offset: 0x00035DE0
		public RuntimeTypeHandle GetRuntimeTypeHandleFromMetadataToken(int typeToken)
		{
			return this.ResolveTypeHandle(typeToken);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00037BE9 File Offset: 0x00035DE9
		public RuntimeTypeHandle ResolveTypeHandle(int typeToken)
		{
			return new RuntimeTypeHandle(ModuleHandle.ResolveTypeHandleInternal(this.GetRuntimeModule(), typeToken, null, null));
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00037BFE File Offset: 0x00035DFE
		public RuntimeTypeHandle ResolveTypeHandle(int typeToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			return new RuntimeTypeHandle(ModuleHandle.ResolveTypeHandleInternal(this.GetRuntimeModule(), typeToken, typeInstantiationContext, methodInstantiationContext));
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00037C14 File Offset: 0x00035E14
		[SecuritySafeCritical]
		internal unsafe static RuntimeType ResolveTypeHandleInternal(RuntimeModule module, int typeToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			ModuleHandle.ValidateModulePointer(module);
			if (!ModuleHandle.GetMetadataImport(module).IsValidToken(typeToken))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[]
				{
					typeToken,
					new ModuleHandle(module)
				}));
			}
			int num;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeInstantiationContext, out num);
			int num2;
			IntPtr[] array2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodInstantiationContext, out num2);
			IntPtr[] array3;
			IntPtr* ptr;
			if ((array3 = array) == null || array3.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array3[0];
			}
			IntPtr[] array4;
			IntPtr* ptr2;
			if ((array4 = array2) == null || array4.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array4[0];
			}
			RuntimeType runtimeType = null;
			ModuleHandle.ResolveType(module, typeToken, ptr, num, ptr2, num2, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			GC.KeepAlive(typeInstantiationContext);
			GC.KeepAlive(methodInstantiationContext);
			return runtimeType;
		}

		// Token: 0x060012B9 RID: 4793
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void ResolveType(RuntimeModule module, int typeToken, IntPtr* typeInstArgs, int typeInstCount, IntPtr* methodInstArgs, int methodInstCount, ObjectHandleOnStack type);

		// Token: 0x060012BA RID: 4794 RVA: 0x00037CDC File Offset: 0x00035EDC
		public RuntimeMethodHandle GetRuntimeMethodHandleFromMetadataToken(int methodToken)
		{
			return this.ResolveMethodHandle(methodToken);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00037CE5 File Offset: 0x00035EE5
		public RuntimeMethodHandle ResolveMethodHandle(int methodToken)
		{
			return this.ResolveMethodHandle(methodToken, null, null);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00037CF0 File Offset: 0x00035EF0
		internal static IRuntimeMethodInfo ResolveMethodHandleInternal(RuntimeModule module, int methodToken)
		{
			return ModuleHandle.ResolveMethodHandleInternal(module, methodToken, null, null);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00037CFB File Offset: 0x00035EFB
		public RuntimeMethodHandle ResolveMethodHandle(int methodToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			return new RuntimeMethodHandle(ModuleHandle.ResolveMethodHandleInternal(this.GetRuntimeModule(), methodToken, typeInstantiationContext, methodInstantiationContext));
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00037D10 File Offset: 0x00035F10
		[SecuritySafeCritical]
		internal static IRuntimeMethodInfo ResolveMethodHandleInternal(RuntimeModule module, int methodToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			int num;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeInstantiationContext, out num);
			int num2;
			IntPtr[] array2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodInstantiationContext, out num2);
			RuntimeMethodHandleInternal runtimeMethodHandleInternal = ModuleHandle.ResolveMethodHandleInternalCore(module, methodToken, array, num, array2, num2);
			IRuntimeMethodInfo runtimeMethodInfo = new RuntimeMethodInfoStub(runtimeMethodHandleInternal, RuntimeMethodHandle.GetLoaderAllocator(runtimeMethodHandleInternal));
			GC.KeepAlive(typeInstantiationContext);
			GC.KeepAlive(methodInstantiationContext);
			return runtimeMethodInfo;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00037D5C File Offset: 0x00035F5C
		[SecurityCritical]
		internal unsafe static RuntimeMethodHandleInternal ResolveMethodHandleInternalCore(RuntimeModule module, int methodToken, IntPtr[] typeInstantiationContext, int typeInstCount, IntPtr[] methodInstantiationContext, int methodInstCount)
		{
			ModuleHandle.ValidateModulePointer(module);
			if (!ModuleHandle.GetMetadataImport(module.GetNativeHandle()).IsValidToken(methodToken))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[]
				{
					methodToken,
					new ModuleHandle(module)
				}));
			}
			IntPtr* ptr;
			if (typeInstantiationContext == null || typeInstantiationContext.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &typeInstantiationContext[0];
			}
			IntPtr* ptr2;
			if (methodInstantiationContext == null || methodInstantiationContext.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &methodInstantiationContext[0];
			}
			return ModuleHandle.ResolveMethod(module.GetNativeHandle(), methodToken, ptr, typeInstCount, ptr2, methodInstCount);
		}

		// Token: 0x060012C0 RID: 4800
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern RuntimeMethodHandleInternal ResolveMethod(RuntimeModule module, int methodToken, IntPtr* typeInstArgs, int typeInstCount, IntPtr* methodInstArgs, int methodInstCount);

		// Token: 0x060012C1 RID: 4801 RVA: 0x00037DFC File Offset: 0x00035FFC
		public RuntimeFieldHandle GetRuntimeFieldHandleFromMetadataToken(int fieldToken)
		{
			return this.ResolveFieldHandle(fieldToken);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00037E05 File Offset: 0x00036005
		public RuntimeFieldHandle ResolveFieldHandle(int fieldToken)
		{
			return new RuntimeFieldHandle(ModuleHandle.ResolveFieldHandleInternal(this.GetRuntimeModule(), fieldToken, null, null));
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00037E1A File Offset: 0x0003601A
		public RuntimeFieldHandle ResolveFieldHandle(int fieldToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			return new RuntimeFieldHandle(ModuleHandle.ResolveFieldHandleInternal(this.GetRuntimeModule(), fieldToken, typeInstantiationContext, methodInstantiationContext));
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00037E30 File Offset: 0x00036030
		[SecuritySafeCritical]
		internal unsafe static IRuntimeFieldInfo ResolveFieldHandleInternal(RuntimeModule module, int fieldToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			ModuleHandle.ValidateModulePointer(module);
			if (!ModuleHandle.GetMetadataImport(module.GetNativeHandle()).IsValidToken(fieldToken))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[]
				{
					fieldToken,
					new ModuleHandle(module)
				}));
			}
			int num;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeInstantiationContext, out num);
			int num2;
			IntPtr[] array2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodInstantiationContext, out num2);
			IntPtr[] array3;
			IntPtr* ptr;
			if ((array3 = array) == null || array3.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array3[0];
			}
			IntPtr[] array4;
			IntPtr* ptr2;
			if ((array4 = array2) == null || array4.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array4[0];
			}
			IRuntimeFieldInfo runtimeFieldInfo = null;
			ModuleHandle.ResolveField(module.GetNativeHandle(), fieldToken, ptr, num, ptr2, num2, JitHelpers.GetObjectHandleOnStack<IRuntimeFieldInfo>(ref runtimeFieldInfo));
			GC.KeepAlive(typeInstantiationContext);
			GC.KeepAlive(methodInstantiationContext);
			return runtimeFieldInfo;
		}

		// Token: 0x060012C5 RID: 4805
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void ResolveField(RuntimeModule module, int fieldToken, IntPtr* typeInstArgs, int typeInstCount, IntPtr* methodInstArgs, int methodInstCount, ObjectHandleOnStack retField);

		// Token: 0x060012C6 RID: 4806
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool _ContainsPropertyMatchingHash(RuntimeModule module, int propertyToken, uint hash);

		// Token: 0x060012C7 RID: 4807 RVA: 0x00037F02 File Offset: 0x00036102
		[SecurityCritical]
		internal static bool ContainsPropertyMatchingHash(RuntimeModule module, int propertyToken, uint hash)
		{
			return ModuleHandle._ContainsPropertyMatchingHash(module.GetNativeHandle(), propertyToken, hash);
		}

		// Token: 0x060012C8 RID: 4808
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetAssembly(RuntimeModule handle, ObjectHandleOnStack retAssembly);

		// Token: 0x060012C9 RID: 4809 RVA: 0x00037F14 File Offset: 0x00036114
		[SecuritySafeCritical]
		internal static RuntimeAssembly GetAssembly(RuntimeModule module)
		{
			RuntimeAssembly runtimeAssembly = null;
			ModuleHandle.GetAssembly(module.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeAssembly>(ref runtimeAssembly));
			return runtimeAssembly;
		}

		// Token: 0x060012CA RID: 4810
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void GetModuleType(RuntimeModule handle, ObjectHandleOnStack type);

		// Token: 0x060012CB RID: 4811 RVA: 0x00037F38 File Offset: 0x00036138
		[SecuritySafeCritical]
		internal static RuntimeType GetModuleType(RuntimeModule module)
		{
			RuntimeType runtimeType = null;
			ModuleHandle.GetModuleType(module.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x060012CC RID: 4812
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetPEKind(RuntimeModule handle, out int peKind, out int machine);

		// Token: 0x060012CD RID: 4813 RVA: 0x00037F5C File Offset: 0x0003615C
		[SecuritySafeCritical]
		internal static void GetPEKind(RuntimeModule module, out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			int num;
			int num2;
			ModuleHandle.GetPEKind(module.GetNativeHandle(), out num, out num2);
			peKind = (PortableExecutableKinds)num;
			machine = (ImageFileMachine)num2;
		}

		// Token: 0x060012CE RID: 4814
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetMDStreamVersion(RuntimeModule module);

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x00037F7E File Offset: 0x0003617E
		public int MDStreamVersion
		{
			[SecuritySafeCritical]
			get
			{
				return ModuleHandle.GetMDStreamVersion(this.GetRuntimeModule().GetNativeHandle());
			}
		}

		// Token: 0x060012D0 RID: 4816
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr _GetMetadataImport(RuntimeModule module);

		// Token: 0x060012D1 RID: 4817 RVA: 0x00037F90 File Offset: 0x00036190
		[SecurityCritical]
		internal static MetadataImport GetMetadataImport(RuntimeModule module)
		{
			return new MetadataImport(ModuleHandle._GetMetadataImport(module.GetNativeHandle()), module);
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00037FA3 File Offset: 0x000361A3
		// Note: this type is marked as 'beforefieldinit'.
		static ModuleHandle()
		{
		}

		// Token: 0x04000675 RID: 1653
		public static readonly ModuleHandle EmptyHandle = ModuleHandle.GetEmptyMH();

		// Token: 0x04000676 RID: 1654
		private RuntimeModule m_ptr;
	}
}
