using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Reflection.Emit
{
	// Token: 0x02000650 RID: 1616
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_MethodRental))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class MethodRental : _MethodRental
	{
		// Token: 0x06004C00 RID: 19456 RVA: 0x00112E24 File Offset: 0x00111024
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void SwapMethodBody(Type cls, int methodtoken, IntPtr rgIL, int methodSize, int flags)
		{
			if (methodSize <= 0 || methodSize >= 4128768)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadSizeForData"), "methodSize");
			}
			if (cls == null)
			{
				throw new ArgumentNullException("cls");
			}
			Module module = cls.Module;
			ModuleBuilder moduleBuilder = module as ModuleBuilder;
			InternalModuleBuilder internalModuleBuilder;
			if (moduleBuilder != null)
			{
				internalModuleBuilder = moduleBuilder.InternalModule;
			}
			else
			{
				internalModuleBuilder = module as InternalModuleBuilder;
			}
			if (internalModuleBuilder == null)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_NotDynamicModule"));
			}
			RuntimeType runtimeType;
			if (cls is TypeBuilder)
			{
				TypeBuilder typeBuilder = (TypeBuilder)cls;
				if (!typeBuilder.IsCreated())
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_NotAllTypesAreBaked", new object[] { typeBuilder.Name }));
				}
				runtimeType = typeBuilder.BakedRuntimeType;
			}
			else
			{
				runtimeType = cls as RuntimeType;
			}
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "cls");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			RuntimeAssembly runtimeAssembly = internalModuleBuilder.GetRuntimeAssembly();
			object syncRoot = runtimeAssembly.SyncRoot;
			lock (syncRoot)
			{
				MethodRental.SwapMethodBody(runtimeType.GetTypeHandleInternal(), methodtoken, rgIL, methodSize, flags, JitHelpers.GetStackCrawlMarkHandle(ref stackCrawlMark));
			}
		}

		// Token: 0x06004C01 RID: 19457
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void SwapMethodBody(RuntimeTypeHandle cls, int methodtoken, IntPtr rgIL, int methodSize, int flags, StackCrawlMarkHandle stackMark);

		// Token: 0x06004C02 RID: 19458 RVA: 0x00112F64 File Offset: 0x00111164
		private MethodRental()
		{
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x00112F6C File Offset: 0x0011116C
		void _MethodRental.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x00112F73 File Offset: 0x00111173
		void _MethodRental.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x00112F7A File Offset: 0x0011117A
		void _MethodRental.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x00112F81 File Offset: 0x00111181
		void _MethodRental.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001F59 RID: 8025
		public const int JitOnDemand = 0;

		// Token: 0x04001F5A RID: 8026
		public const int JitImmediate = 1;
	}
}
