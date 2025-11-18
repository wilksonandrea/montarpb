using System;
using System.Configuration.Assemblies;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Security.Policy;
using System.Threading;

namespace System
{
	// Token: 0x0200008E RID: 142
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Activator))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class Activator : _Activator
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x000199B0 File Offset: 0x00017BB0
		private Activator()
		{
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000199B8 File Offset: 0x00017BB8
		public static object CreateInstance(Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture)
		{
			return Activator.CreateInstance(type, bindingAttr, binder, args, culture, null);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000199C8 File Offset: 0x00017BC8
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static object CreateInstance(Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type is TypeBuilder)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_CreateInstanceWithTypeBuilder"));
			}
			if ((bindingAttr & (BindingFlags)255) == BindingFlags.Default)
			{
				bindingAttr |= BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
			}
			if (activationAttributes != null && activationAttributes.Length != 0)
			{
				if (!type.IsMarshalByRef)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_ActivAttrOnNonMBR"));
				}
				if (!type.IsContextful && (activationAttributes.Length > 1 || !(activationAttributes[0] is UrlAttribute)))
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonUrlAttrOnMBR"));
				}
			}
			RuntimeType runtimeType = type.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "type");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return runtimeType.CreateInstanceImpl(bindingAttr, binder, args, culture, activationAttributes, ref stackCrawlMark);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00019A93 File Offset: 0x00017C93
		[__DynamicallyInvokable]
		public static object CreateInstance(Type type, params object[] args)
		{
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, args, null, null);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00019AA4 File Offset: 0x00017CA4
		public static object CreateInstance(Type type, object[] args, object[] activationAttributes)
		{
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, args, null, activationAttributes);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00019AB5 File Offset: 0x00017CB5
		[__DynamicallyInvokable]
		public static object CreateInstance(Type type)
		{
			return Activator.CreateInstance(type, false);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00019AC0 File Offset: 0x00017CC0
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstance(assemblyName, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, null, null, ref stackCrawlMark);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00019AE4 File Offset: 0x00017CE4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstance(assemblyName, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, activationAttributes, null, ref stackCrawlMark);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00019B08 File Offset: 0x00017D08
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static object CreateInstance(Type type, bool nonPublic)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "type");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return runtimeType.CreateInstanceDefaultCtor(!nonPublic, false, true, ref stackCrawlMark);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00019B60 File Offset: 0x00017D60
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static T CreateInstance<T>()
		{
			RuntimeType runtimeType = typeof(T) as RuntimeType;
			if (runtimeType.HasElementType)
			{
				throw new MissingMethodException(Environment.GetResourceString("Arg_NoDefCTor"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return (T)((object)runtimeType.CreateInstanceDefaultCtor(true, true, true, ref stackCrawlMark));
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00019BA7 File Offset: 0x00017DA7
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
		{
			return Activator.CreateInstanceFrom(assemblyFile, typeName, null);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00019BB1 File Offset: 0x00017DB1
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes)
		{
			return Activator.CreateInstanceFrom(assemblyFile, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, activationAttributes);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00019BC4 File Offset: 0x00017DC4
		[SecuritySafeCritical]
		[Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityInfo, ref stackCrawlMark);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00019BE8 File Offset: 0x00017DE8
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null, ref stackCrawlMark);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00019C0C File Offset: 0x00017E0C
		[SecurityCritical]
		internal static ObjectHandle CreateInstance(string assemblyString, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo, ref StackCrawlMark stackMark)
		{
			if (securityInfo != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
			}
			Type type = null;
			Assembly assembly = null;
			if (assemblyString == null)
			{
				assembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
			}
			else
			{
				RuntimeAssembly runtimeAssembly;
				AssemblyName assemblyName = RuntimeAssembly.CreateAssemblyName(assemblyString, false, out runtimeAssembly);
				if (runtimeAssembly != null)
				{
					assembly = runtimeAssembly;
				}
				else if (assemblyName.ContentType == AssemblyContentType.WindowsRuntime)
				{
					type = Type.GetType(typeName + ", " + assemblyString, true, ignoreCase);
				}
				else
				{
					assembly = RuntimeAssembly.InternalLoadAssemblyName(assemblyName, securityInfo, null, ref stackMark, true, false, false);
				}
			}
			if (type == null)
			{
				if (assembly == null)
				{
					return null;
				}
				type = assembly.GetType(typeName, true, ignoreCase);
			}
			object obj = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			if (obj == null)
			{
				return null;
			}
			return new ObjectHandle(obj);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00019CD0 File Offset: 0x00017ED0
		[Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
		{
			if (securityInfo != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
			}
			return Activator.CreateInstanceFromInternal(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityInfo);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00019D10 File Offset: 0x00017F10
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			return Activator.CreateInstanceFromInternal(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00019D30 File Offset: 0x00017F30
		private static ObjectHandle CreateInstanceFromInternal(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, securityInfo);
			Type type = assembly.GetType(typeName, true, ignoreCase);
			object obj = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			if (obj == null)
			{
				return null;
			}
			return new ObjectHandle(obj);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00019D6D File Offset: 0x00017F6D
		[SecurityCritical]
		public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00019D88 File Offset: 0x00017F88
		[SecurityCritical]
		[Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			if (securityAttributes != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
			}
			return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00019DD8 File Offset: 0x00017FD8
		[SecurityCritical]
		public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00019E07 File Offset: 0x00018007
		[SecurityCritical]
		public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return domain.InternalCreateInstanceFromWithNoSecurity(assemblyFile, typeName);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00019E20 File Offset: 0x00018020
		[SecurityCritical]
		[Obsolete("Methods which use Evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			if (securityAttributes != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
			}
			return domain.InternalCreateInstanceFromWithNoSecurity(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00019E70 File Offset: 0x00018070
		[SecurityCritical]
		public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return domain.InternalCreateInstanceFromWithNoSecurity(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00019EA0 File Offset: 0x000180A0
		[SecuritySafeCritical]
		public static ObjectHandle CreateInstance(ActivationContext activationContext)
		{
			AppDomainManager appDomainManager = AppDomain.CurrentDomain.DomainManager;
			if (appDomainManager == null)
			{
				appDomainManager = new AppDomainManager();
			}
			return appDomainManager.ApplicationActivator.CreateInstance(activationContext);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00019ED0 File Offset: 0x000180D0
		[SecuritySafeCritical]
		public static ObjectHandle CreateInstance(ActivationContext activationContext, string[] activationCustomData)
		{
			AppDomainManager appDomainManager = AppDomain.CurrentDomain.DomainManager;
			if (appDomainManager == null)
			{
				appDomainManager = new AppDomainManager();
			}
			return appDomainManager.ApplicationActivator.CreateInstance(activationContext, activationCustomData);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00019EFE File Offset: 0x000180FE
		public static ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
		{
			return Activator.CreateComInstanceFrom(assemblyName, typeName, null, AssemblyHashAlgorithm.None);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00019F0C File Offset: 0x0001810C
		public static ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyName, hashValue, hashAlgorithm);
			Type type = assembly.GetType(typeName, true, false);
			object[] customAttributes = type.GetCustomAttributes(typeof(ComVisibleAttribute), false);
			if (customAttributes.Length != 0 && !((ComVisibleAttribute)customAttributes[0]).Value)
			{
				throw new TypeLoadException(Environment.GetResourceString("Argument_TypeMustBeVisibleFromCom"));
			}
			if (assembly == null)
			{
				return null;
			}
			object obj = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, null);
			if (obj == null)
			{
				return null;
			}
			return new ObjectHandle(obj);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00019F8B File Offset: 0x0001818B
		[SecurityCritical]
		public static object GetObject(Type type, string url)
		{
			return Activator.GetObject(type, url, null);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00019F95 File Offset: 0x00018195
		[SecurityCritical]
		public static object GetObject(Type type, string url, object state)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return RemotingServices.Connect(type, url, state);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00019FB3 File Offset: 0x000181B3
		[Conditional("_DEBUG")]
		private static void Log(bool test, string title, string success, string failure)
		{
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00019FB7 File Offset: 0x000181B7
		void _Activator.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00019FBE File Offset: 0x000181BE
		void _Activator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00019FC5 File Offset: 0x000181C5
		void _Activator.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00019FCC File Offset: 0x000181CC
		void _Activator.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000374 RID: 884
		internal const int LookupMask = 255;

		// Token: 0x04000375 RID: 885
		internal const BindingFlags ConLookup = BindingFlags.Instance | BindingFlags.Public;

		// Token: 0x04000376 RID: 886
		internal const BindingFlags ConstructorDefault = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
	}
}
