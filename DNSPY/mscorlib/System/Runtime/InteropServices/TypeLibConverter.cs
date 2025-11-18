using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.TCEAdapterGen;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200097B RID: 2427
	[Guid("F1C3BF79-C3E4-11d3-88E7-00902754C43A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public sealed class TypeLibConverter : ITypeLibConverter
	{
		// Token: 0x0600626D RID: 25197 RVA: 0x00150E60 File Offset: 0x0014F060
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, int flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, bool unsafeInterfaces)
		{
			return this.ConvertTypeLibToAssembly(typeLib, asmFileName, unsafeInterfaces ? TypeLibImporterFlags.UnsafeInterfaces : TypeLibImporterFlags.None, notifySink, publicKey, keyPair, null, null);
		}

		// Token: 0x0600626E RID: 25198 RVA: 0x00150E88 File Offset: 0x0014F088
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, string asmNamespace, Version asmVersion)
		{
			if (typeLib == null)
			{
				throw new ArgumentNullException("typeLib");
			}
			if (asmFileName == null)
			{
				throw new ArgumentNullException("asmFileName");
			}
			if (notifySink == null)
			{
				throw new ArgumentNullException("notifySink");
			}
			if (string.Empty.Equals(asmFileName))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileName"), "asmFileName");
			}
			if (asmFileName.Length > 260)
			{
				throw new ArgumentException(Environment.GetResourceString("IO.PathTooLong"), asmFileName);
			}
			if ((flags & TypeLibImporterFlags.PrimaryInteropAssembly) != TypeLibImporterFlags.None && publicKey == null && keyPair == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_PIAMustBeStrongNamed"));
			}
			ArrayList arrayList = null;
			AssemblyNameFlags assemblyNameFlags = AssemblyNameFlags.None;
			AssemblyName assemblyNameFromTypelib = TypeLibConverter.GetAssemblyNameFromTypelib(typeLib, asmFileName, publicKey, keyPair, asmVersion, assemblyNameFlags);
			AssemblyBuilder assemblyBuilder = TypeLibConverter.CreateAssemblyForTypeLib(typeLib, asmFileName, assemblyNameFromTypelib, (flags & TypeLibImporterFlags.PrimaryInteropAssembly) > TypeLibImporterFlags.None, (flags & TypeLibImporterFlags.ReflectionOnlyLoading) > TypeLibImporterFlags.None, (flags & TypeLibImporterFlags.NoDefineVersionResource) > TypeLibImporterFlags.None);
			string fileName = Path.GetFileName(asmFileName);
			ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(fileName, fileName);
			if (asmNamespace == null)
			{
				asmNamespace = assemblyNameFromTypelib.Name;
			}
			TypeLibConverter.TypeResolveHandler typeResolveHandler = new TypeLibConverter.TypeResolveHandler(moduleBuilder, notifySink);
			AppDomain domain = Thread.GetDomain();
			ResolveEventHandler resolveEventHandler = new ResolveEventHandler(typeResolveHandler.ResolveEvent);
			ResolveEventHandler resolveEventHandler2 = new ResolveEventHandler(typeResolveHandler.ResolveAsmEvent);
			ResolveEventHandler resolveEventHandler3 = new ResolveEventHandler(typeResolveHandler.ResolveROAsmEvent);
			domain.TypeResolve += resolveEventHandler;
			domain.AssemblyResolve += resolveEventHandler2;
			domain.ReflectionOnlyAssemblyResolve += resolveEventHandler3;
			TypeLibConverter.nConvertTypeLibToMetadata(typeLib, assemblyBuilder.InternalAssembly, moduleBuilder.InternalModule, asmNamespace, flags, typeResolveHandler, out arrayList);
			TypeLibConverter.UpdateComTypesInAssembly(assemblyBuilder, moduleBuilder);
			if (arrayList.Count > 0)
			{
				new TCEAdapterGenerator().Process(moduleBuilder, arrayList);
			}
			domain.TypeResolve -= resolveEventHandler;
			domain.AssemblyResolve -= resolveEventHandler2;
			domain.ReflectionOnlyAssemblyResolve -= resolveEventHandler3;
			return assemblyBuilder;
		}

		// Token: 0x0600626F RID: 25199 RVA: 0x00151024 File Offset: 0x0014F224
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public object ConvertAssemblyToTypeLib(Assembly assembly, string strTypeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink)
		{
			AssemblyBuilder assemblyBuilder = assembly as AssemblyBuilder;
			RuntimeAssembly runtimeAssembly;
			if (assemblyBuilder != null)
			{
				runtimeAssembly = assemblyBuilder.InternalAssembly;
			}
			else
			{
				runtimeAssembly = assembly as RuntimeAssembly;
			}
			return TypeLibConverter.nConvertAssemblyToTypeLib(runtimeAssembly, strTypeLibName, flags, notifySink);
		}

		// Token: 0x06006270 RID: 25200 RVA: 0x0015105C File Offset: 0x0014F25C
		public bool GetPrimaryInteropAssembly(Guid g, int major, int minor, int lcid, out string asmName, out string asmCodeBase)
		{
			string text = "{" + g.ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
			string text2 = major.ToString("x", CultureInfo.InvariantCulture) + "." + minor.ToString("x", CultureInfo.InvariantCulture);
			asmName = null;
			asmCodeBase = null;
			using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("TypeLib", false))
			{
				if (registryKey != null)
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(text))
					{
						if (registryKey2 != null)
						{
							using (RegistryKey registryKey3 = registryKey2.OpenSubKey(text2, false))
							{
								if (registryKey3 != null)
								{
									asmName = (string)registryKey3.GetValue("PrimaryInteropAssemblyName");
									asmCodeBase = (string)registryKey3.GetValue("PrimaryInteropAssemblyCodeBase");
								}
							}
						}
					}
				}
			}
			return asmName != null;
		}

		// Token: 0x06006271 RID: 25201 RVA: 0x00151170 File Offset: 0x0014F370
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static AssemblyBuilder CreateAssemblyForTypeLib(object typeLib, string asmFileName, AssemblyName asmName, bool bPrimaryInteropAssembly, bool bReflectionOnly, bool bNoDefineVersionResource)
		{
			AppDomain domain = Thread.GetDomain();
			string text = null;
			if (asmFileName != null)
			{
				text = Path.GetDirectoryName(asmFileName);
				if (string.IsNullOrEmpty(text))
				{
					text = null;
				}
			}
			AssemblyBuilderAccess assemblyBuilderAccess;
			if (bReflectionOnly)
			{
				assemblyBuilderAccess = AssemblyBuilderAccess.ReflectionOnly;
			}
			else
			{
				assemblyBuilderAccess = AssemblyBuilderAccess.RunAndSave;
			}
			List<CustomAttributeBuilder> list = new List<CustomAttributeBuilder>();
			ConstructorInfo constructor = typeof(SecurityRulesAttribute).GetConstructor(new Type[] { typeof(SecurityRuleSet) });
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(constructor, new object[] { SecurityRuleSet.Level2 });
			list.Add(customAttributeBuilder);
			AssemblyBuilder assemblyBuilder = domain.DefineDynamicAssembly(asmName, assemblyBuilderAccess, text, false, list);
			TypeLibConverter.SetGuidAttributeOnAssembly(assemblyBuilder, typeLib);
			TypeLibConverter.SetImportedFromTypeLibAttrOnAssembly(assemblyBuilder, typeLib);
			if (bNoDefineVersionResource)
			{
				TypeLibConverter.SetTypeLibVersionAttribute(assemblyBuilder, typeLib);
			}
			else
			{
				TypeLibConverter.SetVersionInformation(assemblyBuilder, typeLib, asmName);
			}
			if (bPrimaryInteropAssembly)
			{
				TypeLibConverter.SetPIAAttributeOnAssembly(assemblyBuilder, typeLib);
			}
			return assemblyBuilder;
		}

		// Token: 0x06006272 RID: 25202 RVA: 0x0015122C File Offset: 0x0014F42C
		[SecurityCritical]
		internal static AssemblyName GetAssemblyNameFromTypelib(object typeLib, string asmFileName, byte[] publicKey, StrongNameKeyPair keyPair, Version asmVersion, AssemblyNameFlags asmNameFlags)
		{
			string text = null;
			string text2 = null;
			int num = 0;
			string text3 = null;
			ITypeLib typeLib2 = (ITypeLib)typeLib;
			typeLib2.GetDocumentation(-1, out text, out text2, out num, out text3);
			if (asmFileName == null)
			{
				asmFileName = text;
			}
			else
			{
				string fileName = Path.GetFileName(asmFileName);
				string extension = Path.GetExtension(asmFileName);
				if (!".dll".Equals(extension, StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileExtension"));
				}
				asmFileName = fileName.Substring(0, fileName.Length - ".dll".Length);
			}
			if (asmVersion == null)
			{
				int num2;
				int num3;
				Marshal.GetTypeLibVersion(typeLib2, out num2, out num3);
				asmVersion = new Version(num2, num3, 0, 0);
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Init(asmFileName, publicKey, null, asmVersion, null, AssemblyHashAlgorithm.None, AssemblyVersionCompatibility.SameMachine, null, asmNameFlags, keyPair);
			return assemblyName;
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x001512F0 File Offset: 0x0014F4F0
		private static void UpdateComTypesInAssembly(AssemblyBuilder asmBldr, ModuleBuilder modBldr)
		{
			AssemblyBuilderData assemblyData = asmBldr.m_assemblyData;
			Type[] types = modBldr.GetTypes();
			int num = types.Length;
			for (int i = 0; i < num; i++)
			{
				assemblyData.AddPublicComType(types[i]);
			}
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x00151324 File Offset: 0x0014F524
		[SecurityCritical]
		private static void SetGuidAttributeOnAssembly(AssemblyBuilder asmBldr, object typeLib)
		{
			Type[] array = new Type[] { typeof(string) };
			ConstructorInfo constructor = typeof(GuidAttribute).GetConstructor(array);
			object[] array2 = new object[] { Marshal.GetTypeLibGuid((ITypeLib)typeLib).ToString() };
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(constructor, array2);
			asmBldr.SetCustomAttribute(customAttributeBuilder);
		}

		// Token: 0x06006275 RID: 25205 RVA: 0x0015138C File Offset: 0x0014F58C
		[SecurityCritical]
		private static void SetImportedFromTypeLibAttrOnAssembly(AssemblyBuilder asmBldr, object typeLib)
		{
			Type[] array = new Type[] { typeof(string) };
			ConstructorInfo constructor = typeof(ImportedFromTypeLibAttribute).GetConstructor(array);
			string typeLibName = Marshal.GetTypeLibName((ITypeLib)typeLib);
			object[] array2 = new object[] { typeLibName };
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(constructor, array2);
			asmBldr.SetCustomAttribute(customAttributeBuilder);
		}

		// Token: 0x06006276 RID: 25206 RVA: 0x001513E8 File Offset: 0x0014F5E8
		[SecurityCritical]
		private static void SetTypeLibVersionAttribute(AssemblyBuilder asmBldr, object typeLib)
		{
			Type[] array = new Type[]
			{
				typeof(int),
				typeof(int)
			};
			ConstructorInfo constructor = typeof(TypeLibVersionAttribute).GetConstructor(array);
			int num;
			int num2;
			Marshal.GetTypeLibVersion((ITypeLib)typeLib, out num, out num2);
			object[] array2 = new object[] { num, num2 };
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(constructor, array2);
			asmBldr.SetCustomAttribute(customAttributeBuilder);
		}

		// Token: 0x06006277 RID: 25207 RVA: 0x00151464 File Offset: 0x0014F664
		[SecurityCritical]
		private static void SetVersionInformation(AssemblyBuilder asmBldr, object typeLib, AssemblyName asmName)
		{
			string text = null;
			string text2 = null;
			int num = 0;
			string text3 = null;
			ITypeLib typeLib2 = (ITypeLib)typeLib;
			typeLib2.GetDocumentation(-1, out text, out text2, out num, out text3);
			string text4 = string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("TypeLibConverter_ImportedTypeLibProductName"), text);
			asmBldr.DefineVersionInfoResource(text4, asmName.Version.ToString(), null, null, null);
			TypeLibConverter.SetTypeLibVersionAttribute(asmBldr, typeLib);
		}

		// Token: 0x06006278 RID: 25208 RVA: 0x001514C8 File Offset: 0x0014F6C8
		[SecurityCritical]
		private static void SetPIAAttributeOnAssembly(AssemblyBuilder asmBldr, object typeLib)
		{
			IntPtr zero = IntPtr.Zero;
			ITypeLib typeLib2 = (ITypeLib)typeLib;
			int num = 0;
			int num2 = 0;
			Type[] array = new Type[]
			{
				typeof(int),
				typeof(int)
			};
			ConstructorInfo constructor = typeof(PrimaryInteropAssemblyAttribute).GetConstructor(array);
			try
			{
				typeLib2.GetLibAttr(out zero);
				TYPELIBATTR typelibattr = (TYPELIBATTR)Marshal.PtrToStructure(zero, typeof(TYPELIBATTR));
				num = (int)typelibattr.wMajorVerNum;
				num2 = (int)typelibattr.wMinorVerNum;
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					typeLib2.ReleaseTLibAttr(zero);
				}
			}
			object[] array2 = new object[] { num, num2 };
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(constructor, array2);
			asmBldr.SetCustomAttribute(customAttributeBuilder);
		}

		// Token: 0x06006279 RID: 25209
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nConvertTypeLibToMetadata(object typeLib, RuntimeAssembly asmBldr, RuntimeModule modBldr, string nameSpace, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, out ArrayList eventItfInfoList);

		// Token: 0x0600627A RID: 25210
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object nConvertAssemblyToTypeLib(RuntimeAssembly assembly, string strTypeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink);

		// Token: 0x0600627B RID: 25211
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void LoadInMemoryTypeByName(RuntimeModule module, string className);

		// Token: 0x0600627C RID: 25212 RVA: 0x001515A0 File Offset: 0x0014F7A0
		public TypeLibConverter()
		{
		}

		// Token: 0x04002BE6 RID: 11238
		private const string s_strTypeLibAssemblyTitlePrefix = "TypeLib ";

		// Token: 0x04002BE7 RID: 11239
		private const string s_strTypeLibAssemblyDescPrefix = "Assembly generated from typelib ";

		// Token: 0x04002BE8 RID: 11240
		private const int MAX_NAMESPACE_LENGTH = 1024;

		// Token: 0x02000C9B RID: 3227
		private class TypeResolveHandler : ITypeLibImporterNotifySink
		{
			// Token: 0x06007120 RID: 28960 RVA: 0x001851A2 File Offset: 0x001833A2
			public TypeResolveHandler(ModuleBuilder mod, ITypeLibImporterNotifySink userSink)
			{
				this.m_Module = mod;
				this.m_UserSink = userSink;
			}

			// Token: 0x06007121 RID: 28961 RVA: 0x001851C3 File Offset: 0x001833C3
			public void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg)
			{
				this.m_UserSink.ReportEvent(eventKind, eventCode, eventMsg);
			}

			// Token: 0x06007122 RID: 28962 RVA: 0x001851D4 File Offset: 0x001833D4
			public Assembly ResolveRef(object typeLib)
			{
				Assembly assembly = this.m_UserSink.ResolveRef(typeLib);
				if (assembly == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
				if (runtimeAssembly == null)
				{
					AssemblyBuilder assemblyBuilder = assembly as AssemblyBuilder;
					if (assemblyBuilder != null)
					{
						runtimeAssembly = assemblyBuilder.InternalAssembly;
					}
				}
				if (runtimeAssembly == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
				}
				this.m_AsmList.Add(runtimeAssembly);
				return runtimeAssembly;
			}

			// Token: 0x06007123 RID: 28963 RVA: 0x0018524C File Offset: 0x0018344C
			[SecurityCritical]
			public Assembly ResolveEvent(object sender, ResolveEventArgs args)
			{
				try
				{
					TypeLibConverter.LoadInMemoryTypeByName(this.m_Module.GetNativeHandle(), args.Name);
					return this.m_Module.Assembly;
				}
				catch (TypeLoadException ex)
				{
					if (ex.ResourceId != -2146233054)
					{
						throw;
					}
				}
				foreach (RuntimeAssembly runtimeAssembly in this.m_AsmList)
				{
					try
					{
						runtimeAssembly.GetType(args.Name, true, false);
						return runtimeAssembly;
					}
					catch (TypeLoadException ex2)
					{
						if (ex2._HResult != -2146233054)
						{
							throw;
						}
					}
				}
				return null;
			}

			// Token: 0x06007124 RID: 28964 RVA: 0x00185310 File Offset: 0x00183510
			public Assembly ResolveAsmEvent(object sender, ResolveEventArgs args)
			{
				foreach (RuntimeAssembly runtimeAssembly in this.m_AsmList)
				{
					if (string.Compare(runtimeAssembly.FullName, args.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return runtimeAssembly;
					}
				}
				return null;
			}

			// Token: 0x06007125 RID: 28965 RVA: 0x00185378 File Offset: 0x00183578
			public Assembly ResolveROAsmEvent(object sender, ResolveEventArgs args)
			{
				foreach (RuntimeAssembly runtimeAssembly in this.m_AsmList)
				{
					if (string.Compare(runtimeAssembly.FullName, args.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return runtimeAssembly;
					}
				}
				string text = AppDomain.CurrentDomain.ApplyPolicy(args.Name);
				return Assembly.ReflectionOnlyLoad(text);
			}

			// Token: 0x0400385E RID: 14430
			private ModuleBuilder m_Module;

			// Token: 0x0400385F RID: 14431
			private ITypeLibImporterNotifySink m_UserSink;

			// Token: 0x04003860 RID: 14432
			private List<RuntimeAssembly> m_AsmList = new List<RuntimeAssembly>();
		}
	}
}
