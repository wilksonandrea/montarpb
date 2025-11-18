using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.DesignerServices
{
	// Token: 0x0200071D RID: 1821
	public sealed class WindowsRuntimeDesignerContext
	{
		// Token: 0x0600513F RID: 20799
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern IntPtr CreateDesignerContext([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] string[] paths, int count, bool shared);

		// Token: 0x06005140 RID: 20800 RVA: 0x0011E494 File Offset: 0x0011C694
		[SecurityCritical]
		internal static IntPtr CreateDesignerContext(IEnumerable<string> paths, [MarshalAs(UnmanagedType.Bool)] bool shared)
		{
			List<string> list = new List<string>(paths);
			string[] array = list.ToArray();
			foreach (string text in array)
			{
				if (text == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_Path"));
				}
				if (Path.IsRelative(text))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
				}
			}
			return WindowsRuntimeDesignerContext.CreateDesignerContext(array, array.Length, shared);
		}

		// Token: 0x06005141 RID: 20801
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetCurrentContext([MarshalAs(UnmanagedType.Bool)] bool isDesignerContext, IntPtr context);

		// Token: 0x06005142 RID: 20802 RVA: 0x0011E4FC File Offset: 0x0011C6FC
		[SecurityCritical]
		private WindowsRuntimeDesignerContext(IEnumerable<string> paths, string name, bool designModeRequired)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				throw new NotSupportedException();
			}
			if (!AppDomain.IsAppXModel())
			{
				throw new NotSupportedException();
			}
			if (designModeRequired && !AppDomain.IsAppXDesignMode())
			{
				throw new NotSupportedException();
			}
			this.m_name = name;
			object obj = WindowsRuntimeDesignerContext.s_lock;
			lock (obj)
			{
				if (WindowsRuntimeDesignerContext.s_sharedContext == IntPtr.Zero)
				{
					WindowsRuntimeDesignerContext.InitializeSharedContext(new string[0]);
				}
			}
			this.m_contextObject = WindowsRuntimeDesignerContext.CreateDesignerContext(paths, false);
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x0011E5B8 File Offset: 0x0011C7B8
		[SecurityCritical]
		public WindowsRuntimeDesignerContext(IEnumerable<string> paths, string name)
			: this(paths, name, true)
		{
		}

		// Token: 0x06005144 RID: 20804 RVA: 0x0011E5C4 File Offset: 0x0011C7C4
		[SecurityCritical]
		public static void InitializeSharedContext(IEnumerable<string> paths)
		{
			if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				throw new NotSupportedException();
			}
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			object obj = WindowsRuntimeDesignerContext.s_lock;
			lock (obj)
			{
				if (WindowsRuntimeDesignerContext.s_sharedContext != IntPtr.Zero)
				{
					throw new NotSupportedException();
				}
				IntPtr intPtr = WindowsRuntimeDesignerContext.CreateDesignerContext(paths, true);
				WindowsRuntimeDesignerContext.SetCurrentContext(false, intPtr);
				WindowsRuntimeDesignerContext.s_sharedContext = intPtr;
			}
		}

		// Token: 0x06005145 RID: 20805 RVA: 0x0011E64C File Offset: 0x0011C84C
		[SecurityCritical]
		public static void SetIterationContext(WindowsRuntimeDesignerContext context)
		{
			if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				throw new NotSupportedException();
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			object obj = WindowsRuntimeDesignerContext.s_lock;
			lock (obj)
			{
				WindowsRuntimeDesignerContext.SetCurrentContext(true, context.m_contextObject);
			}
		}

		// Token: 0x06005146 RID: 20806 RVA: 0x0011E6B4 File Offset: 0x0011C8B4
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Assembly GetAssembly(string assemblyName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoad(assemblyName, null, ref stackCrawlMark, this.m_contextObject, false);
		}

		// Token: 0x06005147 RID: 20807 RVA: 0x0011E6D4 File Offset: 0x0011C8D4
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Type GetType(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeTypeHandle.GetTypeByName(typeName, false, false, false, ref stackCrawlMark, this.m_contextObject, false);
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x0011E703 File Offset: 0x0011C903
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x06005149 RID: 20809 RVA: 0x0011E70B File Offset: 0x0011C90B
		// Note: this type is marked as 'beforefieldinit'.
		static WindowsRuntimeDesignerContext()
		{
		}

		// Token: 0x04002405 RID: 9221
		private static object s_lock = new object();

		// Token: 0x04002406 RID: 9222
		private static IntPtr s_sharedContext;

		// Token: 0x04002407 RID: 9223
		private IntPtr m_contextObject;

		// Token: 0x04002408 RID: 9224
		private string m_name;
	}
}
