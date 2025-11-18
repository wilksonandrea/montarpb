using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006B6 RID: 1718
	internal static class IsolationInterop
	{
		// Token: 0x06005023 RID: 20515 RVA: 0x0011D44E File Offset: 0x0011B64E
		[SecuritySafeCritical]
		public static Store GetUserStore()
		{
			return new Store(IsolationInterop.GetUserStore(0U, IntPtr.Zero, ref IsolationInterop.IID_IStore) as IStore);
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06005024 RID: 20516 RVA: 0x0011D46C File Offset: 0x0011B66C
		public static IIdentityAuthority IdentityAuthority
		{
			[SecuritySafeCritical]
			get
			{
				if (IsolationInterop._idAuth == null)
				{
					object synchObject = IsolationInterop._synchObject;
					lock (synchObject)
					{
						if (IsolationInterop._idAuth == null)
						{
							IsolationInterop._idAuth = IsolationInterop.GetIdentityAuthority();
						}
					}
				}
				return IsolationInterop._idAuth;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06005025 RID: 20517 RVA: 0x0011D4CC File Offset: 0x0011B6CC
		public static IAppIdAuthority AppIdAuthority
		{
			[SecuritySafeCritical]
			get
			{
				if (IsolationInterop._appIdAuth == null)
				{
					object synchObject = IsolationInterop._synchObject;
					lock (synchObject)
					{
						if (IsolationInterop._appIdAuth == null)
						{
							IsolationInterop._appIdAuth = IsolationInterop.GetAppIdAuthority();
						}
					}
				}
				return IsolationInterop._appIdAuth;
			}
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x0011D52C File Offset: 0x0011B72C
		[SecuritySafeCritical]
		internal static IActContext CreateActContext(IDefinitionAppId AppId)
		{
			IsolationInterop.CreateActContextParameters createActContextParameters;
			createActContextParameters.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParameters));
			createActContextParameters.Flags = 16U;
			createActContextParameters.CustomStoreList = IntPtr.Zero;
			createActContextParameters.CultureFallbackList = IntPtr.Zero;
			createActContextParameters.ProcessorArchitectureList = IntPtr.Zero;
			createActContextParameters.Source = IntPtr.Zero;
			createActContextParameters.ProcArch = 0;
			IsolationInterop.CreateActContextParametersSource createActContextParametersSource;
			createActContextParametersSource.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParametersSource));
			createActContextParametersSource.Flags = 0U;
			createActContextParametersSource.SourceType = 1U;
			createActContextParametersSource.Data = IntPtr.Zero;
			IsolationInterop.CreateActContextParametersSourceDefinitionAppid createActContextParametersSourceDefinitionAppid;
			createActContextParametersSourceDefinitionAppid.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParametersSourceDefinitionAppid));
			createActContextParametersSourceDefinitionAppid.Flags = 0U;
			createActContextParametersSourceDefinitionAppid.AppId = AppId;
			IActContext actContext;
			try
			{
				createActContextParametersSource.Data = createActContextParametersSourceDefinitionAppid.ToIntPtr();
				createActContextParameters.Source = createActContextParametersSource.ToIntPtr();
				actContext = IsolationInterop.CreateActContext(ref createActContextParameters) as IActContext;
			}
			finally
			{
				if (createActContextParametersSource.Data != IntPtr.Zero)
				{
					IsolationInterop.CreateActContextParametersSourceDefinitionAppid.Destroy(createActContextParametersSource.Data);
					createActContextParametersSource.Data = IntPtr.Zero;
				}
				if (createActContextParameters.Source != IntPtr.Zero)
				{
					IsolationInterop.CreateActContextParametersSource.Destroy(createActContextParameters.Source);
					createActContextParameters.Source = IntPtr.Zero;
				}
			}
			return actContext;
		}

		// Token: 0x06005027 RID: 20519
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object CreateActContext(ref IsolationInterop.CreateActContextParameters Params);

		// Token: 0x06005028 RID: 20520
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object CreateCMSFromXml([In] byte[] buffer, [In] uint bufferSize, [In] IManifestParseErrorCallback Callback, [In] ref Guid riid);

		// Token: 0x06005029 RID: 20521
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object ParseManifest([MarshalAs(UnmanagedType.LPWStr)] [In] string pszManifestPath, [In] IManifestParseErrorCallback pIManifestParseErrorCallback, [In] ref Guid riid);

		// Token: 0x0600502A RID: 20522
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		private static extern object GetUserStore([In] uint Flags, [In] IntPtr hToken, [In] ref Guid riid);

		// Token: 0x0600502B RID: 20523
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern IIdentityAuthority GetIdentityAuthority();

		// Token: 0x0600502C RID: 20524
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern IAppIdAuthority GetAppIdAuthority();

		// Token: 0x0600502D RID: 20525 RVA: 0x0011D678 File Offset: 0x0011B878
		internal static Guid GetGuidOfType(Type type)
		{
			GuidAttribute guidAttribute = (GuidAttribute)Attribute.GetCustomAttribute(type, typeof(GuidAttribute), false);
			return new Guid(guidAttribute.Value);
		}

		// Token: 0x0600502E RID: 20526 RVA: 0x0011D6A8 File Offset: 0x0011B8A8
		// Note: this type is marked as 'beforefieldinit'.
		static IsolationInterop()
		{
		}

		// Token: 0x0400227B RID: 8827
		private static object _synchObject = new object();

		// Token: 0x0400227C RID: 8828
		private static volatile IIdentityAuthority _idAuth = null;

		// Token: 0x0400227D RID: 8829
		private static volatile IAppIdAuthority _appIdAuth = null;

		// Token: 0x0400227E RID: 8830
		public const string IsolationDllName = "clr.dll";

		// Token: 0x0400227F RID: 8831
		public static Guid IID_ICMS = IsolationInterop.GetGuidOfType(typeof(ICMS));

		// Token: 0x04002280 RID: 8832
		public static Guid IID_IDefinitionIdentity = IsolationInterop.GetGuidOfType(typeof(IDefinitionIdentity));

		// Token: 0x04002281 RID: 8833
		public static Guid IID_IManifestInformation = IsolationInterop.GetGuidOfType(typeof(IManifestInformation));

		// Token: 0x04002282 RID: 8834
		public static Guid IID_IEnumSTORE_ASSEMBLY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY));

		// Token: 0x04002283 RID: 8835
		public static Guid IID_IEnumSTORE_ASSEMBLY_FILE = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));

		// Token: 0x04002284 RID: 8836
		public static Guid IID_IEnumSTORE_CATEGORY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY));

		// Token: 0x04002285 RID: 8837
		public static Guid IID_IEnumSTORE_CATEGORY_INSTANCE = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_INSTANCE));

		// Token: 0x04002286 RID: 8838
		public static Guid IID_IEnumSTORE_DEPLOYMENT_METADATA = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_DEPLOYMENT_METADATA));

		// Token: 0x04002287 RID: 8839
		public static Guid IID_IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY));

		// Token: 0x04002288 RID: 8840
		public static Guid IID_IStore = IsolationInterop.GetGuidOfType(typeof(IStore));

		// Token: 0x04002289 RID: 8841
		public static Guid GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING = new Guid("2ec93463-b0c3-45e1-8364-327e96aea856");

		// Token: 0x0400228A RID: 8842
		public static Guid SXS_INSTALL_REFERENCE_SCHEME_SXS_STRONGNAME_SIGNED_PRIVATE_ASSEMBLY = new Guid("3ab20ac0-67e8-4512-8385-a487e35df3da");

		// Token: 0x02000C62 RID: 3170
		internal struct CreateActContextParameters
		{
			// Token: 0x040037B8 RID: 14264
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040037B9 RID: 14265
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040037BA RID: 14266
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr CustomStoreList;

			// Token: 0x040037BB RID: 14267
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr CultureFallbackList;

			// Token: 0x040037BC RID: 14268
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr ProcessorArchitectureList;

			// Token: 0x040037BD RID: 14269
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr Source;

			// Token: 0x040037BE RID: 14270
			[MarshalAs(UnmanagedType.U2)]
			public ushort ProcArch;

			// Token: 0x02000D13 RID: 3347
			[Flags]
			public enum CreateFlags
			{
				// Token: 0x04003969 RID: 14697
				Nothing = 0,
				// Token: 0x0400396A RID: 14698
				StoreListValid = 1,
				// Token: 0x0400396B RID: 14699
				CultureListValid = 2,
				// Token: 0x0400396C RID: 14700
				ProcessorFallbackListValid = 4,
				// Token: 0x0400396D RID: 14701
				ProcessorValid = 8,
				// Token: 0x0400396E RID: 14702
				SourceValid = 16,
				// Token: 0x0400396F RID: 14703
				IgnoreVisibility = 32
			}
		}

		// Token: 0x02000C63 RID: 3171
		internal struct CreateActContextParametersSource
		{
			// Token: 0x0600707E RID: 28798 RVA: 0x00183290 File Offset: 0x00181490
			[SecurityCritical]
			public IntPtr ToIntPtr()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf<IsolationInterop.CreateActContextParametersSource>(this));
				Marshal.StructureToPtr<IsolationInterop.CreateActContextParametersSource>(this, intPtr, false);
				return intPtr;
			}

			// Token: 0x0600707F RID: 28799 RVA: 0x001832BC File Offset: 0x001814BC
			[SecurityCritical]
			public static void Destroy(IntPtr p)
			{
				Marshal.DestroyStructure(p, typeof(IsolationInterop.CreateActContextParametersSource));
				Marshal.FreeCoTaskMem(p);
			}

			// Token: 0x040037BF RID: 14271
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040037C0 RID: 14272
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040037C1 RID: 14273
			[MarshalAs(UnmanagedType.U4)]
			public uint SourceType;

			// Token: 0x040037C2 RID: 14274
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr Data;

			// Token: 0x02000D14 RID: 3348
			[Flags]
			public enum SourceFlags
			{
				// Token: 0x04003971 RID: 14705
				Definition = 1,
				// Token: 0x04003972 RID: 14706
				Reference = 2
			}
		}

		// Token: 0x02000C64 RID: 3172
		internal struct CreateActContextParametersSourceDefinitionAppid
		{
			// Token: 0x06007080 RID: 28800 RVA: 0x001832D4 File Offset: 0x001814D4
			[SecurityCritical]
			public IntPtr ToIntPtr()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf<IsolationInterop.CreateActContextParametersSourceDefinitionAppid>(this));
				Marshal.StructureToPtr<IsolationInterop.CreateActContextParametersSourceDefinitionAppid>(this, intPtr, false);
				return intPtr;
			}

			// Token: 0x06007081 RID: 28801 RVA: 0x00183300 File Offset: 0x00181500
			[SecurityCritical]
			public static void Destroy(IntPtr p)
			{
				Marshal.DestroyStructure(p, typeof(IsolationInterop.CreateActContextParametersSourceDefinitionAppid));
				Marshal.FreeCoTaskMem(p);
			}

			// Token: 0x040037C3 RID: 14275
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040037C4 RID: 14276
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040037C5 RID: 14277
			public IDefinitionAppId AppId;
		}
	}
}
