using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005B3 RID: 1459
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Assembly))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[Serializable]
	public abstract class Assembly : _Assembly, IEvidenceFactory, ICustomAttributeProvider, ISerializable
	{
		// Token: 0x06004379 RID: 17273 RVA: 0x000FA5D5 File Offset: 0x000F87D5
		protected Assembly()
		{
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x000FA5DD File Offset: 0x000F87DD
		public static string CreateQualifiedName(string assemblyName, string typeName)
		{
			return typeName + ", " + assemblyName;
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x000FA5EC File Offset: 0x000F87EC
		public static Assembly GetAssembly(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Module module = type.Module;
			if (module == null)
			{
				return null;
			}
			return module.Assembly;
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x000FA625 File Offset: 0x000F8825
		[__DynamicallyInvokable]
		public static bool operator ==(Assembly left, Assembly right)
		{
			return left == right || (left != null && right != null && !(left is RuntimeAssembly) && !(right is RuntimeAssembly) && left.Equals(right));
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x000FA64C File Offset: 0x000F884C
		[__DynamicallyInvokable]
		public static bool operator !=(Assembly left, Assembly right)
		{
			return !(left == right);
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x000FA658 File Offset: 0x000F8858
		[__DynamicallyInvokable]
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x000FA661 File Offset: 0x000F8861
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x000FA66C File Offset: 0x000F886C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadFrom(assemblyFile, null, null, AssemblyHashAlgorithm.None, false, false, ref stackCrawlMark);
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x000FA688 File Offset: 0x000F8888
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly ReflectionOnlyLoadFrom(string assemblyFile)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadFrom(assemblyFile, null, null, AssemblyHashAlgorithm.None, true, false, ref stackCrawlMark);
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x000FA6A4 File Offset: 0x000F88A4
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of LoadFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadFrom(assemblyFile, securityEvidence, null, AssemblyHashAlgorithm.None, false, false, ref stackCrawlMark);
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x000FA6C0 File Offset: 0x000F88C0
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of LoadFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadFrom(assemblyFile, securityEvidence, hashValue, hashAlgorithm, false, false, ref stackCrawlMark);
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x000FA6DC File Offset: 0x000F88DC
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadFrom(assemblyFile, null, hashValue, hashAlgorithm, false, false, ref stackCrawlMark);
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x000FA6F8 File Offset: 0x000F88F8
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly UnsafeLoadFrom(string assemblyFile)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadFrom(assemblyFile, null, null, AssemblyHashAlgorithm.None, false, true, ref stackCrawlMark);
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x000FA714 File Offset: 0x000F8914
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(string assemblyString)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoad(assemblyString, null, ref stackCrawlMark, false);
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x000FA730 File Offset: 0x000F8930
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static Type GetType_Compat(string assemblyString, string typeName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			RuntimeAssembly runtimeAssembly;
			AssemblyName assemblyName = RuntimeAssembly.CreateAssemblyName(assemblyString, false, out runtimeAssembly);
			if (runtimeAssembly == null)
			{
				if (assemblyName.ContentType == AssemblyContentType.WindowsRuntime)
				{
					return Type.GetType(typeName + ", " + assemblyString, true, false);
				}
				runtimeAssembly = RuntimeAssembly.InternalLoadAssemblyName(assemblyName, null, null, ref stackCrawlMark, true, false, false);
			}
			return runtimeAssembly.GetType(typeName, true, false);
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x000FA788 File Offset: 0x000F8988
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly ReflectionOnlyLoad(string assemblyString)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoad(assemblyString, null, ref stackCrawlMark, true);
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x000FA7A4 File Offset: 0x000F89A4
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(string assemblyString, Evidence assemblySecurity)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoad(assemblyString, assemblySecurity, ref stackCrawlMark, false);
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x000FA7C0 File Offset: 0x000F89C0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(AssemblyName assemblyRef)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, null, null, ref stackCrawlMark, true, false, false);
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x000FA7DC File Offset: 0x000F89DC
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, assemblySecurity, null, ref stackCrawlMark, true, false, false);
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x000FA7F8 File Offset: 0x000F89F8
		[SecuritySafeCritical]
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadWithPartialName(string partialName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.LoadWithPartialNameInternal(partialName, null, ref stackCrawlMark);
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x000FA810 File Offset: 0x000F8A10
		[SecuritySafeCritical]
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.LoadWithPartialNameInternal(partialName, securityEvidence, ref stackCrawlMark);
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x000FA828 File Offset: 0x000F8A28
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(byte[] rawAssembly)
		{
			AppDomain.CheckLoadByteArraySupported();
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.nLoadImage(rawAssembly, null, null, ref stackCrawlMark, false, false, SecurityContextSource.CurrentAssembly);
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x000FA84C File Offset: 0x000F8A4C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly ReflectionOnlyLoad(byte[] rawAssembly)
		{
			AppDomain.CheckReflectionOnlyLoadSupported();
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.nLoadImage(rawAssembly, null, null, ref stackCrawlMark, true, false, SecurityContextSource.CurrentAssembly);
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x000FA870 File Offset: 0x000F8A70
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
		{
			AppDomain.CheckLoadByteArraySupported();
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, null, ref stackCrawlMark, false, false, SecurityContextSource.CurrentAssembly);
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x000FA894 File Offset: 0x000F8A94
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, SecurityContextSource securityContextSource)
		{
			AppDomain.CheckLoadByteArraySupported();
			if (securityContextSource < SecurityContextSource.CurrentAppDomain || securityContextSource > SecurityContextSource.CurrentAssembly)
			{
				throw new ArgumentOutOfRangeException("securityContextSource");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, null, ref stackCrawlMark, false, false, securityContextSource);
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x000FA8C8 File Offset: 0x000F8AC8
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static Assembly LoadImageSkipIntegrityCheck(byte[] rawAssembly, byte[] rawSymbolStore, SecurityContextSource securityContextSource)
		{
			AppDomain.CheckLoadByteArraySupported();
			if (securityContextSource < SecurityContextSource.CurrentAppDomain || securityContextSource > SecurityContextSource.CurrentAssembly)
			{
				throw new ArgumentOutOfRangeException("securityContextSource");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, null, ref stackCrawlMark, false, true, securityContextSource);
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x000FA8FC File Offset: 0x000F8AFC
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			AppDomain.CheckLoadByteArraySupported();
			if (securityEvidence != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				Zone hostEvidence = securityEvidence.GetHostEvidence<Zone>();
				if (hostEvidence == null || hostEvidence.SecurityZone != SecurityZone.MyComputer)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
				}
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, securityEvidence, ref stackCrawlMark, false, false, SecurityContextSource.CurrentAssembly);
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x000FA94E File Offset: 0x000F8B4E
		[SecuritySafeCritical]
		public static Assembly LoadFile(string path)
		{
			AppDomain.CheckLoadFileSupported();
			new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, path).Demand();
			return RuntimeAssembly.nLoadFile(path, null);
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x000FA969 File Offset: 0x000F8B69
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of LoadFile which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence)]
		public static Assembly LoadFile(string path, Evidence securityEvidence)
		{
			AppDomain.CheckLoadFileSupported();
			if (securityEvidence != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
			}
			new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, path).Demand();
			return RuntimeAssembly.nLoadFile(path, securityEvidence);
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x000FA9A4 File Offset: 0x000F8BA4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly GetExecutingAssembly()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.GetExecutingAssembly(ref stackCrawlMark);
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x000FA9BC File Offset: 0x000F8BBC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly GetCallingAssembly()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCallersCaller;
			return RuntimeAssembly.GetExecutingAssembly(ref stackCrawlMark);
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x000FA9D4 File Offset: 0x000F8BD4
		[SecuritySafeCritical]
		public static Assembly GetEntryAssembly()
		{
			AppDomainManager appDomainManager = AppDomain.CurrentDomain.DomainManager;
			if (appDomainManager == null)
			{
				appDomainManager = new AppDomainManager();
			}
			return appDomainManager.EntryAssembly;
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06004399 RID: 17305 RVA: 0x000FA9FB File Offset: 0x000F8BFB
		// (remove) Token: 0x0600439A RID: 17306 RVA: 0x000FAA02 File Offset: 0x000F8C02
		public virtual event ModuleResolveEventHandler ModuleResolve
		{
			[SecurityCritical]
			add
			{
				throw new NotImplementedException();
			}
			[SecurityCritical]
			remove
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x0600439B RID: 17307 RVA: 0x000FAA09 File Offset: 0x000F8C09
		public virtual string CodeBase
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x0600439C RID: 17308 RVA: 0x000FAA10 File Offset: 0x000F8C10
		public virtual string EscapedCodeBase
		{
			[SecuritySafeCritical]
			get
			{
				return AssemblyName.EscapeCodeBase(this.CodeBase);
			}
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x000FAA1D File Offset: 0x000F8C1D
		[__DynamicallyInvokable]
		public virtual AssemblyName GetName()
		{
			return this.GetName(false);
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x000FAA26 File Offset: 0x000F8C26
		public virtual AssemblyName GetName(bool copiedName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x0600439F RID: 17311 RVA: 0x000FAA2D File Offset: 0x000F8C2D
		[__DynamicallyInvokable]
		public virtual string FullName
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060043A0 RID: 17312 RVA: 0x000FAA34 File Offset: 0x000F8C34
		[__DynamicallyInvokable]
		public virtual MethodInfo EntryPoint
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x000FAA3B File Offset: 0x000F8C3B
		Type _Assembly.GetType()
		{
			return base.GetType();
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x000FAA43 File Offset: 0x000F8C43
		[__DynamicallyInvokable]
		public virtual Type GetType(string name)
		{
			return this.GetType(name, false, false);
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x000FAA4E File Offset: 0x000F8C4E
		[__DynamicallyInvokable]
		public virtual Type GetType(string name, bool throwOnError)
		{
			return this.GetType(name, throwOnError, false);
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x000FAA59 File Offset: 0x000F8C59
		[__DynamicallyInvokable]
		public virtual Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060043A5 RID: 17317 RVA: 0x000FAA60 File Offset: 0x000F8C60
		[__DynamicallyInvokable]
		public virtual IEnumerable<Type> ExportedTypes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetExportedTypes();
			}
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x000FAA68 File Offset: 0x000F8C68
		[__DynamicallyInvokable]
		public virtual Type[] GetExportedTypes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060043A7 RID: 17319 RVA: 0x000FAA70 File Offset: 0x000F8C70
		[__DynamicallyInvokable]
		public virtual IEnumerable<TypeInfo> DefinedTypes
		{
			[__DynamicallyInvokable]
			get
			{
				Type[] types = this.GetTypes();
				TypeInfo[] array = new TypeInfo[types.Length];
				for (int i = 0; i < types.Length; i++)
				{
					TypeInfo typeInfo = types[i].GetTypeInfo();
					if (typeInfo == null)
					{
						throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoTypeInfo", new object[] { types[i].FullName }));
					}
					array[i] = typeInfo;
				}
				return array;
			}
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x000FAAD4 File Offset: 0x000F8CD4
		[__DynamicallyInvokable]
		public virtual Type[] GetTypes()
		{
			Module[] modules = this.GetModules(false);
			int num = modules.Length;
			int num2 = 0;
			Type[][] array = new Type[num][];
			for (int i = 0; i < num; i++)
			{
				array[i] = modules[i].GetTypes();
				num2 += array[i].Length;
			}
			int num3 = 0;
			Type[] array2 = new Type[num2];
			for (int j = 0; j < num; j++)
			{
				int num4 = array[j].Length;
				Array.Copy(array[j], 0, array2, num3, num4);
				num3 += num4;
			}
			return array2;
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x000FAB58 File Offset: 0x000F8D58
		[__DynamicallyInvokable]
		public virtual Stream GetManifestResourceStream(Type type, string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x000FAB5F File Offset: 0x000F8D5F
		[__DynamicallyInvokable]
		public virtual Stream GetManifestResourceStream(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x000FAB66 File Offset: 0x000F8D66
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x000FAB6D File Offset: 0x000F8D6D
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x000FAB74 File Offset: 0x000F8D74
		public virtual Evidence Evidence
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060043AE RID: 17326 RVA: 0x000FAB7B File Offset: 0x000F8D7B
		public virtual PermissionSet PermissionSet
		{
			[SecurityCritical]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060043AF RID: 17327 RVA: 0x000FAB82 File Offset: 0x000F8D82
		public bool IsFullyTrusted
		{
			[SecuritySafeCritical]
			get
			{
				return this.PermissionSet.IsUnrestricted();
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060043B0 RID: 17328 RVA: 0x000FAB8F File Offset: 0x000F8D8F
		public virtual SecurityRuleSet SecurityRuleSet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x000FAB96 File Offset: 0x000F8D96
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060043B2 RID: 17330 RVA: 0x000FABA0 File Offset: 0x000F8DA0
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual Module ManifestModule
		{
			[__DynamicallyInvokable]
			get
			{
				RuntimeAssembly runtimeAssembly = this as RuntimeAssembly;
				if (runtimeAssembly != null)
				{
					return runtimeAssembly.ManifestModule;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060043B3 RID: 17331 RVA: 0x000FABC9 File Offset: 0x000F8DC9
		[__DynamicallyInvokable]
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x000FABD1 File Offset: 0x000F8DD1
		[__DynamicallyInvokable]
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x000FABD8 File Offset: 0x000F8DD8
		[__DynamicallyInvokable]
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x000FABDF File Offset: 0x000F8DDF
		[__DynamicallyInvokable]
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x000FABE6 File Offset: 0x000F8DE6
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060043B8 RID: 17336 RVA: 0x000FABED File Offset: 0x000F8DED
		[ComVisible(false)]
		public virtual bool ReflectionOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x000FABF4 File Offset: 0x000F8DF4
		public Module LoadModule(string moduleName, byte[] rawModule)
		{
			return this.LoadModule(moduleName, rawModule, null);
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x000FABFF File Offset: 0x000F8DFF
		public virtual Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x000FAC06 File Offset: 0x000F8E06
		public object CreateInstance(string typeName)
		{
			return this.CreateInstance(typeName, false, BindingFlags.Instance | BindingFlags.Public, null, null, null, null);
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x000FAC16 File Offset: 0x000F8E16
		public object CreateInstance(string typeName, bool ignoreCase)
		{
			return this.CreateInstance(typeName, ignoreCase, BindingFlags.Instance | BindingFlags.Public, null, null, null, null);
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x000FAC28 File Offset: 0x000F8E28
		public virtual object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			return Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060043BE RID: 17342 RVA: 0x000FAC59 File Offset: 0x000F8E59
		[__DynamicallyInvokable]
		public virtual IEnumerable<Module> Modules
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetLoadedModules(true);
			}
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x000FAC62 File Offset: 0x000F8E62
		public Module[] GetLoadedModules()
		{
			return this.GetLoadedModules(false);
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x000FAC6B File Offset: 0x000F8E6B
		public virtual Module[] GetLoadedModules(bool getResourceModules)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043C1 RID: 17345 RVA: 0x000FAC72 File Offset: 0x000F8E72
		[__DynamicallyInvokable]
		public Module[] GetModules()
		{
			return this.GetModules(false);
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x000FAC7B File Offset: 0x000F8E7B
		public virtual Module[] GetModules(bool getResourceModules)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x000FAC82 File Offset: 0x000F8E82
		public virtual Module GetModule(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x000FAC89 File Offset: 0x000F8E89
		public virtual FileStream GetFile(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x000FAC90 File Offset: 0x000F8E90
		public virtual FileStream[] GetFiles()
		{
			return this.GetFiles(false);
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x000FAC99 File Offset: 0x000F8E99
		public virtual FileStream[] GetFiles(bool getResourceModules)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x000FACA0 File Offset: 0x000F8EA0
		[__DynamicallyInvokable]
		public virtual string[] GetManifestResourceNames()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x000FACA7 File Offset: 0x000F8EA7
		public virtual AssemblyName[] GetReferencedAssemblies()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x000FACAE File Offset: 0x000F8EAE
		[__DynamicallyInvokable]
		public virtual ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x000FACB8 File Offset: 0x000F8EB8
		[__DynamicallyInvokable]
		public override string ToString()
		{
			string fullName = this.FullName;
			if (fullName == null)
			{
				return base.ToString();
			}
			return fullName;
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060043CB RID: 17355 RVA: 0x000FACD7 File Offset: 0x000F8ED7
		public virtual string Location
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060043CC RID: 17356 RVA: 0x000FACDE File Offset: 0x000F8EDE
		[ComVisible(false)]
		public virtual string ImageRuntimeVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060043CD RID: 17357 RVA: 0x000FACE5 File Offset: 0x000F8EE5
		public virtual bool GlobalAssemblyCache
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060043CE RID: 17358 RVA: 0x000FACEC File Offset: 0x000F8EEC
		[ComVisible(false)]
		public virtual long HostContext
		{
			get
			{
				RuntimeAssembly runtimeAssembly = this as RuntimeAssembly;
				if (runtimeAssembly != null)
				{
					return runtimeAssembly.HostContext;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060043CF RID: 17359 RVA: 0x000FAD15 File Offset: 0x000F8F15
		[__DynamicallyInvokable]
		public virtual bool IsDynamic
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}
	}
}
