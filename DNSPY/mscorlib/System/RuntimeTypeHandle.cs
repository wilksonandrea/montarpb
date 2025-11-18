using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x02000130 RID: 304
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct RuntimeTypeHandle : ISerializable
	{
		// Token: 0x060011BC RID: 4540 RVA: 0x00036C68 File Offset: 0x00034E68
		internal RuntimeTypeHandle GetNativeHandle()
		{
			RuntimeType type = this.m_type;
			if (type == null)
			{
				throw new ArgumentNullException(null, Environment.GetResourceString("Arg_InvalidHandle"));
			}
			return new RuntimeTypeHandle(type);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00036C9C File Offset: 0x00034E9C
		internal RuntimeType GetTypeChecked()
		{
			RuntimeType type = this.m_type;
			if (type == null)
			{
				throw new ArgumentNullException(null, Environment.GetResourceString("Arg_InvalidHandle"));
			}
			return type;
		}

		// Token: 0x060011BE RID: 4542
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInstanceOfType(RuntimeType type, object o);

		// Token: 0x060011BF RID: 4543 RVA: 0x00036CCC File Offset: 0x00034ECC
		[SecuritySafeCritical]
		internal unsafe static Type GetTypeHelper(Type typeStart, Type[] genericArgs, IntPtr pModifiers, int cModifiers)
		{
			Type type = typeStart;
			if (genericArgs != null)
			{
				type = type.MakeGenericType(genericArgs);
			}
			if (cModifiers > 0)
			{
				int* ptr = (int*)pModifiers.ToPointer();
				for (int i = 0; i < cModifiers; i++)
				{
					if ((byte)Marshal.ReadInt32((IntPtr)((void*)ptr), i * 4) == 15)
					{
						type = type.MakePointerType();
					}
					else if ((byte)Marshal.ReadInt32((IntPtr)((void*)ptr), i * 4) == 16)
					{
						type = type.MakeByRefType();
					}
					else if ((byte)Marshal.ReadInt32((IntPtr)((void*)ptr), i * 4) == 29)
					{
						type = type.MakeArrayType();
					}
					else
					{
						type = type.MakeArrayType(Marshal.ReadInt32((IntPtr)((void*)ptr), ++i * 4));
					}
				}
			}
			return type;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00036D6F File Offset: 0x00034F6F
		[__DynamicallyInvokable]
		public static bool operator ==(RuntimeTypeHandle left, object right)
		{
			return left.Equals(right);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00036D79 File Offset: 0x00034F79
		[__DynamicallyInvokable]
		public static bool operator ==(object left, RuntimeTypeHandle right)
		{
			return right.Equals(left);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00036D83 File Offset: 0x00034F83
		[__DynamicallyInvokable]
		public static bool operator !=(RuntimeTypeHandle left, object right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00036D90 File Offset: 0x00034F90
		[__DynamicallyInvokable]
		public static bool operator !=(object left, RuntimeTypeHandle right)
		{
			return !right.Equals(left);
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00036D9D File Offset: 0x00034F9D
		internal static RuntimeTypeHandle EmptyHandle
		{
			get
			{
				return new RuntimeTypeHandle(null);
			}
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00036DA5 File Offset: 0x00034FA5
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (!(this.m_type != null))
			{
				return 0;
			}
			return this.m_type.GetHashCode();
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00036DC4 File Offset: 0x00034FC4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is RuntimeTypeHandle && ((RuntimeTypeHandle)obj).m_type == this.m_type;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00036DF4 File Offset: 0x00034FF4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public bool Equals(RuntimeTypeHandle handle)
		{
			return handle.m_type == this.m_type;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x00036E08 File Offset: 0x00035008
		public IntPtr Value
		{
			[SecurityCritical]
			get
			{
				if (!(this.m_type != null))
				{
					return IntPtr.Zero;
				}
				return this.m_type.m_handle;
			}
		}

		// Token: 0x060011C9 RID: 4553
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetValueInternal(RuntimeTypeHandle handle);

		// Token: 0x060011CA RID: 4554 RVA: 0x00036E29 File Offset: 0x00035029
		internal RuntimeTypeHandle(RuntimeType type)
		{
			this.m_type = type;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00036E32 File Offset: 0x00035032
		internal bool IsNullHandle()
		{
			return this.m_type == null;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00036E40 File Offset: 0x00035040
		[SecuritySafeCritical]
		internal static bool IsPrimitive(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return (corElementType >= CorElementType.Boolean && corElementType <= CorElementType.R8) || corElementType == CorElementType.I || corElementType == CorElementType.U;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00036E6C File Offset: 0x0003506C
		[SecuritySafeCritical]
		internal static bool IsByRef(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.ByRef;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00036E88 File Offset: 0x00035088
		[SecuritySafeCritical]
		internal static bool IsPointer(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.Ptr;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00036EA4 File Offset: 0x000350A4
		[SecuritySafeCritical]
		internal static bool IsArray(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.Array || corElementType == CorElementType.SzArray;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00036EC4 File Offset: 0x000350C4
		[SecuritySafeCritical]
		internal static bool IsSzArray(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.SzArray;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00036EE0 File Offset: 0x000350E0
		[SecuritySafeCritical]
		internal static bool HasElementType(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.Array || corElementType == CorElementType.SzArray || corElementType == CorElementType.Ptr || corElementType == CorElementType.ByRef;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00036F0C File Offset: 0x0003510C
		[SecurityCritical]
		internal static IntPtr[] CopyRuntimeTypeHandles(RuntimeTypeHandle[] inHandles, out int length)
		{
			if (inHandles == null || inHandles.Length == 0)
			{
				length = 0;
				return null;
			}
			IntPtr[] array = new IntPtr[inHandles.Length];
			for (int i = 0; i < inHandles.Length; i++)
			{
				array[i] = inHandles[i].Value;
			}
			length = array.Length;
			return array;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00036F54 File Offset: 0x00035154
		[SecurityCritical]
		internal static IntPtr[] CopyRuntimeTypeHandles(Type[] inHandles, out int length)
		{
			if (inHandles == null || inHandles.Length == 0)
			{
				length = 0;
				return null;
			}
			IntPtr[] array = new IntPtr[inHandles.Length];
			for (int i = 0; i < inHandles.Length; i++)
			{
				array[i] = inHandles[i].GetTypeHandleInternal().Value;
			}
			length = array.Length;
			return array;
		}

		// Token: 0x060011D4 RID: 4564
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateInstance(RuntimeType type, bool publicOnly, bool noCheck, ref bool canBeCached, ref RuntimeMethodHandleInternal ctor, ref bool bNeedSecurityCheck);

		// Token: 0x060011D5 RID: 4565
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateCaInstance(RuntimeType type, IRuntimeMethodInfo ctor);

		// Token: 0x060011D6 RID: 4566
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object Allocate(RuntimeType type);

		// Token: 0x060011D7 RID: 4567
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateInstanceForAnotherGenericParameter(RuntimeType type, RuntimeType genericParameter);

		// Token: 0x060011D8 RID: 4568 RVA: 0x00036F9D File Offset: 0x0003519D
		internal RuntimeType GetRuntimeType()
		{
			return this.m_type;
		}

		// Token: 0x060011D9 RID: 4569
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern CorElementType GetCorElementType(RuntimeType type);

		// Token: 0x060011DA RID: 4570
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeAssembly GetAssembly(RuntimeType type);

		// Token: 0x060011DB RID: 4571
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeModule GetModule(RuntimeType type);

		// Token: 0x060011DC RID: 4572 RVA: 0x00036FA5 File Offset: 0x000351A5
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public ModuleHandle GetModuleHandle()
		{
			return new ModuleHandle(RuntimeTypeHandle.GetModule(this.m_type));
		}

		// Token: 0x060011DD RID: 4573
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetBaseType(RuntimeType type);

		// Token: 0x060011DE RID: 4574
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern TypeAttributes GetAttributes(RuntimeType type);

		// Token: 0x060011DF RID: 4575
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetElementType(RuntimeType type);

		// Token: 0x060011E0 RID: 4576
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CompareCanonicalHandles(RuntimeType left, RuntimeType right);

		// Token: 0x060011E1 RID: 4577
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetArrayRank(RuntimeType type);

		// Token: 0x060011E2 RID: 4578
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetToken(RuntimeType type);

		// Token: 0x060011E3 RID: 4579
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodHandleInternal GetMethodAt(RuntimeType type, int slot);

		// Token: 0x060011E4 RID: 4580 RVA: 0x00036FB7 File Offset: 0x000351B7
		internal static RuntimeTypeHandle.IntroducedMethodEnumerator GetIntroducedMethods(RuntimeType type)
		{
			return new RuntimeTypeHandle.IntroducedMethodEnumerator(type);
		}

		// Token: 0x060011E5 RID: 4581
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeMethodHandleInternal GetFirstIntroducedMethod(RuntimeType type);

		// Token: 0x060011E6 RID: 4582
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNextIntroducedMethod(ref RuntimeMethodHandleInternal method);

		// Token: 0x060011E7 RID: 4583
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetFields(RuntimeType type, IntPtr* result, int* count);

		// Token: 0x060011E8 RID: 4584
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type[] GetInterfaces(RuntimeType type);

		// Token: 0x060011E9 RID: 4585
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetConstraints(RuntimeTypeHandle handle, ObjectHandleOnStack types);

		// Token: 0x060011EA RID: 4586 RVA: 0x00036FC0 File Offset: 0x000351C0
		[SecuritySafeCritical]
		internal Type[] GetConstraints()
		{
			Type[] array = null;
			RuntimeTypeHandle.GetConstraints(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref array));
			return array;
		}

		// Token: 0x060011EB RID: 4587
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr GetGCHandle(RuntimeTypeHandle handle, GCHandleType type);

		// Token: 0x060011EC RID: 4588 RVA: 0x00036FE2 File Offset: 0x000351E2
		[SecurityCritical]
		internal IntPtr GetGCHandle(GCHandleType type)
		{
			return RuntimeTypeHandle.GetGCHandle(this.GetNativeHandle(), type);
		}

		// Token: 0x060011ED RID: 4589
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetNumVirtuals(RuntimeType type);

		// Token: 0x060011EE RID: 4590
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void VerifyInterfaceIsImplemented(RuntimeTypeHandle handle, RuntimeTypeHandle interfaceHandle);

		// Token: 0x060011EF RID: 4591 RVA: 0x00036FF0 File Offset: 0x000351F0
		[SecuritySafeCritical]
		internal void VerifyInterfaceIsImplemented(RuntimeTypeHandle interfaceHandle)
		{
			RuntimeTypeHandle.VerifyInterfaceIsImplemented(this.GetNativeHandle(), interfaceHandle.GetNativeHandle());
		}

		// Token: 0x060011F0 RID: 4592
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetInterfaceMethodImplementationSlot(RuntimeTypeHandle handle, RuntimeTypeHandle interfaceHandle, RuntimeMethodHandleInternal interfaceMethodHandle);

		// Token: 0x060011F1 RID: 4593 RVA: 0x00037004 File Offset: 0x00035204
		[SecuritySafeCritical]
		internal int GetInterfaceMethodImplementationSlot(RuntimeTypeHandle interfaceHandle, RuntimeMethodHandleInternal interfaceMethodHandle)
		{
			return RuntimeTypeHandle.GetInterfaceMethodImplementationSlot(this.GetNativeHandle(), interfaceHandle.GetNativeHandle(), interfaceMethodHandle);
		}

		// Token: 0x060011F2 RID: 4594
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsZapped(RuntimeType type);

		// Token: 0x060011F3 RID: 4595
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsDoNotForceOrderOfConstructorsSet();

		// Token: 0x060011F4 RID: 4596
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsComObject(RuntimeType type, bool isGenericCOM);

		// Token: 0x060011F5 RID: 4597
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsContextful(RuntimeType type);

		// Token: 0x060011F6 RID: 4598
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInterface(RuntimeType type);

		// Token: 0x060011F7 RID: 4599
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool _IsVisible(RuntimeTypeHandle typeHandle);

		// Token: 0x060011F8 RID: 4600 RVA: 0x00037019 File Offset: 0x00035219
		[SecuritySafeCritical]
		internal static bool IsVisible(RuntimeType type)
		{
			return RuntimeTypeHandle._IsVisible(new RuntimeTypeHandle(type));
		}

		// Token: 0x060011F9 RID: 4601
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsSecurityCritical(RuntimeTypeHandle typeHandle);

		// Token: 0x060011FA RID: 4602 RVA: 0x00037026 File Offset: 0x00035226
		[SecuritySafeCritical]
		internal bool IsSecurityCritical()
		{
			return RuntimeTypeHandle.IsSecurityCritical(this.GetNativeHandle());
		}

		// Token: 0x060011FB RID: 4603
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsSecuritySafeCritical(RuntimeTypeHandle typeHandle);

		// Token: 0x060011FC RID: 4604 RVA: 0x00037033 File Offset: 0x00035233
		[SecuritySafeCritical]
		internal bool IsSecuritySafeCritical()
		{
			return RuntimeTypeHandle.IsSecuritySafeCritical(this.GetNativeHandle());
		}

		// Token: 0x060011FD RID: 4605
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsSecurityTransparent(RuntimeTypeHandle typeHandle);

		// Token: 0x060011FE RID: 4606 RVA: 0x00037040 File Offset: 0x00035240
		[SecuritySafeCritical]
		internal bool IsSecurityTransparent()
		{
			return RuntimeTypeHandle.IsSecurityTransparent(this.GetNativeHandle());
		}

		// Token: 0x060011FF RID: 4607
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasProxyAttribute(RuntimeType type);

		// Token: 0x06001200 RID: 4608
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsValueType(RuntimeType type);

		// Token: 0x06001201 RID: 4609
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ConstructName(RuntimeTypeHandle handle, TypeNameFormatFlags formatFlags, StringHandleOnStack retString);

		// Token: 0x06001202 RID: 4610 RVA: 0x00037050 File Offset: 0x00035250
		[SecuritySafeCritical]
		internal string ConstructName(TypeNameFormatFlags formatFlags)
		{
			string text = null;
			RuntimeTypeHandle.ConstructName(this.GetNativeHandle(), formatFlags, JitHelpers.GetStringHandleOnStack(ref text));
			return text;
		}

		// Token: 0x06001203 RID: 4611
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* _GetUtf8Name(RuntimeType type);

		// Token: 0x06001204 RID: 4612 RVA: 0x00037073 File Offset: 0x00035273
		[SecuritySafeCritical]
		internal static Utf8String GetUtf8Name(RuntimeType type)
		{
			return new Utf8String(RuntimeTypeHandle._GetUtf8Name(type));
		}

		// Token: 0x06001205 RID: 4613
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CanCastTo(RuntimeType type, RuntimeType target);

		// Token: 0x06001206 RID: 4614
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetDeclaringType(RuntimeType type);

		// Token: 0x06001207 RID: 4615
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IRuntimeMethodInfo GetDeclaringMethod(RuntimeType type);

		// Token: 0x06001208 RID: 4616
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetDefaultConstructor(RuntimeTypeHandle handle, ObjectHandleOnStack method);

		// Token: 0x06001209 RID: 4617 RVA: 0x00037080 File Offset: 0x00035280
		[SecuritySafeCritical]
		internal IRuntimeMethodInfo GetDefaultConstructor()
		{
			IRuntimeMethodInfo runtimeMethodInfo = null;
			RuntimeTypeHandle.GetDefaultConstructor(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref runtimeMethodInfo));
			return runtimeMethodInfo;
		}

		// Token: 0x0600120A RID: 4618
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, StackCrawlMarkHandle stackMark, IntPtr pPrivHostBinder, bool loadTypeFromPartialName, ObjectHandleOnStack type);

		// Token: 0x0600120B RID: 4619 RVA: 0x000370A2 File Offset: 0x000352A2
		internal static RuntimeType GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark, bool loadTypeFromPartialName)
		{
			return RuntimeTypeHandle.GetTypeByName(name, throwOnError, ignoreCase, reflectionOnly, ref stackMark, IntPtr.Zero, loadTypeFromPartialName);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000370B8 File Offset: 0x000352B8
		[SecuritySafeCritical]
		internal static RuntimeType GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark, IntPtr pPrivHostBinder, bool loadTypeFromPartialName)
		{
			if (name != null && name.Length != 0)
			{
				RuntimeType runtimeType = null;
				RuntimeTypeHandle.GetTypeByName(name, throwOnError, ignoreCase, reflectionOnly, JitHelpers.GetStackCrawlMarkHandle(ref stackMark), pPrivHostBinder, loadTypeFromPartialName, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
				return runtimeType;
			}
			if (throwOnError)
			{
				throw new TypeLoadException(Environment.GetResourceString("Arg_TypeLoadNullStr"));
			}
			return null;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00037103 File Offset: 0x00035303
		internal static Type GetTypeByName(string name, ref StackCrawlMark stackMark)
		{
			return RuntimeTypeHandle.GetTypeByName(name, false, false, false, ref stackMark, false);
		}

		// Token: 0x0600120E RID: 4622
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetTypeByNameUsingCARules(string name, RuntimeModule scope, ObjectHandleOnStack type);

		// Token: 0x0600120F RID: 4623 RVA: 0x00037110 File Offset: 0x00035310
		[SecuritySafeCritical]
		internal static RuntimeType GetTypeByNameUsingCARules(string name, RuntimeModule scope)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			RuntimeType runtimeType = null;
			RuntimeTypeHandle.GetTypeByNameUsingCARules(name, scope.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x06001210 RID: 4624
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void GetInstantiation(RuntimeTypeHandle type, ObjectHandleOnStack types, bool fAsRuntimeTypeArray);

		// Token: 0x06001211 RID: 4625 RVA: 0x0003714C File Offset: 0x0003534C
		[SecuritySafeCritical]
		internal RuntimeType[] GetInstantiationInternal()
		{
			RuntimeType[] array = null;
			RuntimeTypeHandle.GetInstantiation(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref array), true);
			return array;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00037170 File Offset: 0x00035370
		[SecuritySafeCritical]
		internal Type[] GetInstantiationPublic()
		{
			Type[] array = null;
			RuntimeTypeHandle.GetInstantiation(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref array), false);
			return array;
		}

		// Token: 0x06001213 RID: 4627
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void Instantiate(RuntimeTypeHandle handle, IntPtr* pInst, int numGenericArgs, ObjectHandleOnStack type);

		// Token: 0x06001214 RID: 4628 RVA: 0x00037194 File Offset: 0x00035394
		[SecurityCritical]
		internal unsafe RuntimeType Instantiate(Type[] inst)
		{
			int num;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(inst, out num);
			IntPtr[] array2;
			IntPtr* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			RuntimeType runtimeType = null;
			RuntimeTypeHandle.Instantiate(this.GetNativeHandle(), ptr, num, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			GC.KeepAlive(inst);
			return runtimeType;
		}

		// Token: 0x06001215 RID: 4629
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakeArray(RuntimeTypeHandle handle, int rank, ObjectHandleOnStack type);

		// Token: 0x06001216 RID: 4630 RVA: 0x000371E4 File Offset: 0x000353E4
		[SecuritySafeCritical]
		internal RuntimeType MakeArray(int rank)
		{
			RuntimeType runtimeType = null;
			RuntimeTypeHandle.MakeArray(this.GetNativeHandle(), rank, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x06001217 RID: 4631
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakeSZArray(RuntimeTypeHandle handle, ObjectHandleOnStack type);

		// Token: 0x06001218 RID: 4632 RVA: 0x00037208 File Offset: 0x00035408
		[SecuritySafeCritical]
		internal RuntimeType MakeSZArray()
		{
			RuntimeType runtimeType = null;
			RuntimeTypeHandle.MakeSZArray(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x06001219 RID: 4633
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakeByRef(RuntimeTypeHandle handle, ObjectHandleOnStack type);

		// Token: 0x0600121A RID: 4634 RVA: 0x0003722C File Offset: 0x0003542C
		[SecuritySafeCritical]
		internal RuntimeType MakeByRef()
		{
			RuntimeType runtimeType = null;
			RuntimeTypeHandle.MakeByRef(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x0600121B RID: 4635
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakePointer(RuntimeTypeHandle handle, ObjectHandleOnStack type);

		// Token: 0x0600121C RID: 4636 RVA: 0x00037250 File Offset: 0x00035450
		[SecurityCritical]
		internal RuntimeType MakePointer()
		{
			RuntimeType runtimeType = null;
			RuntimeTypeHandle.MakePointer(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x0600121D RID: 4637
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool IsCollectible(RuntimeTypeHandle handle);

		// Token: 0x0600121E RID: 4638
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasInstantiation(RuntimeType type);

		// Token: 0x0600121F RID: 4639 RVA: 0x00037272 File Offset: 0x00035472
		internal bool HasInstantiation()
		{
			return RuntimeTypeHandle.HasInstantiation(this.GetTypeChecked());
		}

		// Token: 0x06001220 RID: 4640
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetGenericTypeDefinition(RuntimeTypeHandle type, ObjectHandleOnStack retType);

		// Token: 0x06001221 RID: 4641 RVA: 0x00037280 File Offset: 0x00035480
		[SecuritySafeCritical]
		internal static RuntimeType GetGenericTypeDefinition(RuntimeType type)
		{
			RuntimeType runtimeType = type;
			if (RuntimeTypeHandle.HasInstantiation(runtimeType) && !RuntimeTypeHandle.IsGenericTypeDefinition(runtimeType))
			{
				RuntimeTypeHandle.GetGenericTypeDefinition(runtimeType.GetTypeHandleInternal(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			}
			return runtimeType;
		}

		// Token: 0x06001222 RID: 4642
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericTypeDefinition(RuntimeType type);

		// Token: 0x06001223 RID: 4643
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericVariable(RuntimeType type);

		// Token: 0x06001224 RID: 4644 RVA: 0x000372B2 File Offset: 0x000354B2
		internal bool IsGenericVariable()
		{
			return RuntimeTypeHandle.IsGenericVariable(this.GetTypeChecked());
		}

		// Token: 0x06001225 RID: 4645
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGenericVariableIndex(RuntimeType type);

		// Token: 0x06001226 RID: 4646 RVA: 0x000372C0 File Offset: 0x000354C0
		[SecuritySafeCritical]
		internal int GetGenericVariableIndex()
		{
			RuntimeType typeChecked = this.GetTypeChecked();
			if (!RuntimeTypeHandle.IsGenericVariable(typeChecked))
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericParameter"));
			}
			return RuntimeTypeHandle.GetGenericVariableIndex(typeChecked);
		}

		// Token: 0x06001227 RID: 4647
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ContainsGenericVariables(RuntimeType handle);

		// Token: 0x06001228 RID: 4648 RVA: 0x000372F2 File Offset: 0x000354F2
		[SecuritySafeCritical]
		internal bool ContainsGenericVariables()
		{
			return RuntimeTypeHandle.ContainsGenericVariables(this.GetTypeChecked());
		}

		// Token: 0x06001229 RID: 4649
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool SatisfiesConstraints(RuntimeType paramType, IntPtr* pTypeContext, int typeContextLength, IntPtr* pMethodContext, int methodContextLength, RuntimeType toType);

		// Token: 0x0600122A RID: 4650 RVA: 0x00037300 File Offset: 0x00035500
		[SecurityCritical]
		internal unsafe static bool SatisfiesConstraints(RuntimeType paramType, RuntimeType[] typeContext, RuntimeType[] methodContext, RuntimeType toType)
		{
			int num;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeContext, out num);
			int num2;
			IntPtr[] array2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodContext, out num2);
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
			bool flag = RuntimeTypeHandle.SatisfiesConstraints(paramType, ptr, num, ptr2, num2, toType);
			GC.KeepAlive(typeContext);
			GC.KeepAlive(methodContext);
			return flag;
		}

		// Token: 0x0600122B RID: 4651
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr _GetMetadataImport(RuntimeType type);

		// Token: 0x0600122C RID: 4652 RVA: 0x0003737E File Offset: 0x0003557E
		[SecurityCritical]
		internal static MetadataImport GetMetadataImport(RuntimeType type)
		{
			return new MetadataImport(RuntimeTypeHandle._GetMetadataImport(type), type);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0003738C File Offset: 0x0003558C
		[SecurityCritical]
		private RuntimeTypeHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			RuntimeType runtimeType = (RuntimeType)info.GetValue("TypeObj", typeof(RuntimeType));
			this.m_type = runtimeType;
			if (this.m_type == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x000373E8 File Offset: 0x000355E8
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.m_type == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFieldState"));
			}
			info.AddValue("TypeObj", this.m_type, typeof(RuntimeType));
		}

		// Token: 0x0600122F RID: 4655
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsEquivalentTo(RuntimeType rtType1, RuntimeType rtType2);

		// Token: 0x06001230 RID: 4656
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsEquivalentType(RuntimeType type);

		// Token: 0x0400065F RID: 1631
		private RuntimeType m_type;

		// Token: 0x02000AFC RID: 2812
		internal struct IntroducedMethodEnumerator
		{
			// Token: 0x06006A49 RID: 27209 RVA: 0x0016DB7F File Offset: 0x0016BD7F
			[SecuritySafeCritical]
			internal IntroducedMethodEnumerator(RuntimeType type)
			{
				this._handle = RuntimeTypeHandle.GetFirstIntroducedMethod(type);
				this._firstCall = true;
			}

			// Token: 0x06006A4A RID: 27210 RVA: 0x0016DB94 File Offset: 0x0016BD94
			[SecuritySafeCritical]
			public bool MoveNext()
			{
				if (this._firstCall)
				{
					this._firstCall = false;
				}
				else if (this._handle.Value != IntPtr.Zero)
				{
					RuntimeTypeHandle.GetNextIntroducedMethod(ref this._handle);
				}
				return !(this._handle.Value == IntPtr.Zero);
			}

			// Token: 0x170011F7 RID: 4599
			// (get) Token: 0x06006A4B RID: 27211 RVA: 0x0016DBEC File Offset: 0x0016BDEC
			public RuntimeMethodHandleInternal Current
			{
				get
				{
					return this._handle;
				}
			}

			// Token: 0x06006A4C RID: 27212 RVA: 0x0016DBF4 File Offset: 0x0016BDF4
			public RuntimeTypeHandle.IntroducedMethodEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x040031EF RID: 12783
			private bool _firstCall;

			// Token: 0x040031F0 RID: 12784
			private RuntimeMethodHandleInternal _handle;
		}
	}
}
