using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F9 RID: 2553
	public static class WindowsRuntimeMetadata
	{
		// Token: 0x060064E2 RID: 25826 RVA: 0x00157756 File Offset: 0x00155956
		[SecurityCritical]
		public static IEnumerable<string> ResolveNamespace(string namespaceName, IEnumerable<string> packageGraphFilePaths)
		{
			return WindowsRuntimeMetadata.ResolveNamespace(namespaceName, null, packageGraphFilePaths);
		}

		// Token: 0x060064E3 RID: 25827 RVA: 0x00157760 File Offset: 0x00155960
		[SecurityCritical]
		public static IEnumerable<string> ResolveNamespace(string namespaceName, string windowsSdkFilePath, IEnumerable<string> packageGraphFilePaths)
		{
			if (namespaceName == null)
			{
				throw new ArgumentNullException("namespaceName");
			}
			string[] array = null;
			if (packageGraphFilePaths != null)
			{
				List<string> list = new List<string>(packageGraphFilePaths);
				array = new string[list.Count];
				int num = 0;
				foreach (string text in list)
				{
					array[num] = text;
					num++;
				}
			}
			string[] array2 = null;
			WindowsRuntimeMetadata.nResolveNamespace(namespaceName, windowsSdkFilePath, array, (array == null) ? 0 : array.Length, JitHelpers.GetObjectHandleOnStack<string[]>(ref array2));
			return array2;
		}

		// Token: 0x060064E4 RID: 25828
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void nResolveNamespace(string namespaceName, string windowsSdkFilePath, string[] packageGraphFilePaths, int cPackageGraphFilePaths, ObjectHandleOnStack retFileNames);

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060064E5 RID: 25829 RVA: 0x001577F8 File Offset: 0x001559F8
		// (remove) Token: 0x060064E6 RID: 25830 RVA: 0x0015782C File Offset: 0x00155A2C
		public static event EventHandler<NamespaceResolveEventArgs> ReflectionOnlyNamespaceResolve
		{
			[CompilerGenerated]
			[SecurityCritical]
			add
			{
				EventHandler<NamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve;
				EventHandler<NamespaceResolveEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<NamespaceResolveEventArgs> eventHandler3 = (EventHandler<NamespaceResolveEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<NamespaceResolveEventArgs>>(ref WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			[SecurityCritical]
			remove
			{
				EventHandler<NamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve;
				EventHandler<NamespaceResolveEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<NamespaceResolveEventArgs> eventHandler3 = (EventHandler<NamespaceResolveEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<NamespaceResolveEventArgs>>(ref WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060064E7 RID: 25831 RVA: 0x00157860 File Offset: 0x00155A60
		internal static RuntimeAssembly[] OnReflectionOnlyNamespaceResolveEvent(AppDomain appDomain, RuntimeAssembly assembly, string namespaceName)
		{
			EventHandler<NamespaceResolveEventArgs> reflectionOnlyNamespaceResolve = WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve;
			if (reflectionOnlyNamespaceResolve != null)
			{
				Delegate[] invocationList = reflectionOnlyNamespaceResolve.GetInvocationList();
				int num = invocationList.Length;
				for (int i = 0; i < num; i++)
				{
					NamespaceResolveEventArgs namespaceResolveEventArgs = new NamespaceResolveEventArgs(namespaceName, assembly);
					((EventHandler<NamespaceResolveEventArgs>)invocationList[i])(appDomain, namespaceResolveEventArgs);
					Collection<Assembly> resolvedAssemblies = namespaceResolveEventArgs.ResolvedAssemblies;
					if (resolvedAssemblies.Count > 0)
					{
						RuntimeAssembly[] array = new RuntimeAssembly[resolvedAssemblies.Count];
						int num2 = 0;
						foreach (Assembly assembly2 in resolvedAssemblies)
						{
							array[num2] = AppDomain.GetRuntimeAssembly(assembly2);
							num2++;
						}
						return array;
					}
				}
			}
			return null;
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060064E8 RID: 25832 RVA: 0x00157924 File Offset: 0x00155B24
		// (remove) Token: 0x060064E9 RID: 25833 RVA: 0x00157958 File Offset: 0x00155B58
		public static event EventHandler<DesignerNamespaceResolveEventArgs> DesignerNamespaceResolve
		{
			[CompilerGenerated]
			[SecurityCritical]
			add
			{
				EventHandler<DesignerNamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.DesignerNamespaceResolve;
				EventHandler<DesignerNamespaceResolveEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<DesignerNamespaceResolveEventArgs> eventHandler3 = (EventHandler<DesignerNamespaceResolveEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<DesignerNamespaceResolveEventArgs>>(ref WindowsRuntimeMetadata.DesignerNamespaceResolve, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			[SecurityCritical]
			remove
			{
				EventHandler<DesignerNamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.DesignerNamespaceResolve;
				EventHandler<DesignerNamespaceResolveEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<DesignerNamespaceResolveEventArgs> eventHandler3 = (EventHandler<DesignerNamespaceResolveEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<DesignerNamespaceResolveEventArgs>>(ref WindowsRuntimeMetadata.DesignerNamespaceResolve, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060064EA RID: 25834 RVA: 0x0015798C File Offset: 0x00155B8C
		internal static string[] OnDesignerNamespaceResolveEvent(AppDomain appDomain, string namespaceName)
		{
			EventHandler<DesignerNamespaceResolveEventArgs> designerNamespaceResolve = WindowsRuntimeMetadata.DesignerNamespaceResolve;
			if (designerNamespaceResolve != null)
			{
				Delegate[] invocationList = designerNamespaceResolve.GetInvocationList();
				int num = invocationList.Length;
				for (int i = 0; i < num; i++)
				{
					DesignerNamespaceResolveEventArgs designerNamespaceResolveEventArgs = new DesignerNamespaceResolveEventArgs(namespaceName);
					((EventHandler<DesignerNamespaceResolveEventArgs>)invocationList[i])(appDomain, designerNamespaceResolveEventArgs);
					Collection<string> resolvedAssemblyFiles = designerNamespaceResolveEventArgs.ResolvedAssemblyFiles;
					if (resolvedAssemblyFiles.Count > 0)
					{
						string[] array = new string[resolvedAssemblyFiles.Count];
						int num2 = 0;
						foreach (string text in resolvedAssemblyFiles)
						{
							if (string.IsNullOrEmpty(text))
							{
								throw new ArgumentException(Environment.GetResourceString("Arg_EmptyOrNullString"), "DesignerNamespaceResolveEventArgs.ResolvedAssemblyFiles");
							}
							array[num2] = text;
							num2++;
						}
						return array;
					}
				}
			}
			return null;
		}

		// Token: 0x04002D32 RID: 11570
		[CompilerGenerated]
		private static EventHandler<NamespaceResolveEventArgs> ReflectionOnlyNamespaceResolve;

		// Token: 0x04002D33 RID: 11571
		[CompilerGenerated]
		private static EventHandler<DesignerNamespaceResolveEventArgs> DesignerNamespaceResolve;
	}
}
