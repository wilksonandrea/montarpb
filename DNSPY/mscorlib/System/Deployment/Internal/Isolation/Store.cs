using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006B0 RID: 1712
	internal class Store
	{
		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06004FE8 RID: 20456 RVA: 0x0011CB48 File Offset: 0x0011AD48
		public IStore InternalStore
		{
			get
			{
				return this._pStore;
			}
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x0011CB50 File Offset: 0x0011AD50
		public Store(IStore pStore)
		{
			if (pStore == null)
			{
				throw new ArgumentNullException("pStore");
			}
			this._pStore = pStore;
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x0011CB70 File Offset: 0x0011AD70
		[SecuritySafeCritical]
		public uint[] Transact(StoreTransactionOperation[] operations)
		{
			if (operations == null || operations.Length == 0)
			{
				throw new ArgumentException("operations");
			}
			uint[] array = new uint[operations.Length];
			int[] array2 = new int[operations.Length];
			this._pStore.Transact(new IntPtr(operations.Length), operations, array, array2);
			return array;
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x0011CBB8 File Offset: 0x0011ADB8
		[SecuritySafeCritical]
		public IDefinitionIdentity BindReferenceToAssemblyIdentity(uint Flags, IReferenceIdentity ReferenceIdentity, uint cDeploymentsToIgnore, IDefinitionIdentity[] DefinitionIdentity_DeploymentsToIgnore)
		{
			Guid iid_IDefinitionIdentity = IsolationInterop.IID_IDefinitionIdentity;
			object obj = this._pStore.BindReferenceToAssembly(Flags, ReferenceIdentity, cDeploymentsToIgnore, DefinitionIdentity_DeploymentsToIgnore, ref iid_IDefinitionIdentity);
			return (IDefinitionIdentity)obj;
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x0011CBE4 File Offset: 0x0011ADE4
		[SecuritySafeCritical]
		public void CalculateDelimiterOfDeploymentsBasedOnQuota(uint dwFlags, uint cDeployments, IDefinitionAppId[] rgpIDefinitionAppId_Deployments, ref StoreApplicationReference InstallerReference, ulong ulonglongQuota, ref uint Delimiter, ref ulong SizeSharedWithExternalDeployment, ref ulong SizeConsumedByInputDeploymentArray)
		{
			IntPtr zero = IntPtr.Zero;
			this._pStore.CalculateDelimiterOfDeploymentsBasedOnQuota(dwFlags, new IntPtr((long)((ulong)cDeployments)), rgpIDefinitionAppId_Deployments, ref InstallerReference, ulonglongQuota, ref zero, ref SizeSharedWithExternalDeployment, ref SizeConsumedByInputDeploymentArray);
			Delimiter = (uint)zero.ToInt64();
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x0011CC20 File Offset: 0x0011AE20
		[SecuritySafeCritical]
		public ICMS BindReferenceToAssemblyManifest(uint Flags, IReferenceIdentity ReferenceIdentity, uint cDeploymentsToIgnore, IDefinitionIdentity[] DefinitionIdentity_DeploymentsToIgnore)
		{
			Guid iid_ICMS = IsolationInterop.IID_ICMS;
			object obj = this._pStore.BindReferenceToAssembly(Flags, ReferenceIdentity, cDeploymentsToIgnore, DefinitionIdentity_DeploymentsToIgnore, ref iid_ICMS);
			return (ICMS)obj;
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x0011CC4C File Offset: 0x0011AE4C
		[SecuritySafeCritical]
		public ICMS GetAssemblyManifest(uint Flags, IDefinitionIdentity DefinitionIdentity)
		{
			Guid iid_ICMS = IsolationInterop.IID_ICMS;
			object assemblyInformation = this._pStore.GetAssemblyInformation(Flags, DefinitionIdentity, ref iid_ICMS);
			return (ICMS)assemblyInformation;
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x0011CC78 File Offset: 0x0011AE78
		[SecuritySafeCritical]
		public IDefinitionIdentity GetAssemblyIdentity(uint Flags, IDefinitionIdentity DefinitionIdentity)
		{
			Guid iid_IDefinitionIdentity = IsolationInterop.IID_IDefinitionIdentity;
			object assemblyInformation = this._pStore.GetAssemblyInformation(Flags, DefinitionIdentity, ref iid_IDefinitionIdentity);
			return (IDefinitionIdentity)assemblyInformation;
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x0011CCA1 File Offset: 0x0011AEA1
		public StoreAssemblyEnumeration EnumAssemblies(Store.EnumAssembliesFlags Flags)
		{
			return this.EnumAssemblies(Flags, null);
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x0011CCAC File Offset: 0x0011AEAC
		[SecuritySafeCritical]
		public StoreAssemblyEnumeration EnumAssemblies(Store.EnumAssembliesFlags Flags, IReferenceIdentity refToMatch)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY));
			object obj = this._pStore.EnumAssemblies((uint)Flags, refToMatch, ref guidOfType);
			return new StoreAssemblyEnumeration((IEnumSTORE_ASSEMBLY)obj);
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x0011CCE4 File Offset: 0x0011AEE4
		[SecuritySafeCritical]
		public StoreAssemblyFileEnumeration EnumFiles(Store.EnumAssemblyFilesFlags Flags, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));
			object obj = this._pStore.EnumFiles((uint)Flags, Assembly, ref guidOfType);
			return new StoreAssemblyFileEnumeration((IEnumSTORE_ASSEMBLY_FILE)obj);
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x0011CD1C File Offset: 0x0011AF1C
		[SecuritySafeCritical]
		public StoreAssemblyFileEnumeration EnumPrivateFiles(Store.EnumApplicationPrivateFiles Flags, IDefinitionAppId Application, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));
			object obj = this._pStore.EnumPrivateFiles((uint)Flags, Application, Assembly, ref guidOfType);
			return new StoreAssemblyFileEnumeration((IEnumSTORE_ASSEMBLY_FILE)obj);
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x0011CD58 File Offset: 0x0011AF58
		[SecuritySafeCritical]
		public IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE EnumInstallationReferences(Store.EnumAssemblyInstallReferenceFlags Flags, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE));
			object obj = this._pStore.EnumInstallationReferences((uint)Flags, Assembly, ref guidOfType);
			return (IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE)obj;
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x0011CD8C File Offset: 0x0011AF8C
		[SecuritySafeCritical]
		public Store.IPathLock LockAssemblyPath(IDefinitionIdentity asm)
		{
			IntPtr intPtr;
			string text = this._pStore.LockAssemblyPath(0U, asm, out intPtr);
			return new Store.AssemblyPathLock(this._pStore, intPtr, text);
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x0011CDB8 File Offset: 0x0011AFB8
		[SecuritySafeCritical]
		public Store.IPathLock LockApplicationPath(IDefinitionAppId app)
		{
			IntPtr intPtr;
			string text = this._pStore.LockApplicationPath(0U, app, out intPtr);
			return new Store.ApplicationPathLock(this._pStore, intPtr, text);
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x0011CDE4 File Offset: 0x0011AFE4
		[SecuritySafeCritical]
		public ulong QueryChangeID(IDefinitionIdentity asm)
		{
			return this._pStore.QueryChangeID(asm);
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x0011CE00 File Offset: 0x0011B000
		[SecuritySafeCritical]
		public StoreCategoryEnumeration EnumCategories(Store.EnumCategoriesFlags Flags, IReferenceIdentity CategoryMatch)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY));
			object obj = this._pStore.EnumCategories((uint)Flags, CategoryMatch, ref guidOfType);
			return new StoreCategoryEnumeration((IEnumSTORE_CATEGORY)obj);
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x0011CE38 File Offset: 0x0011B038
		public StoreSubcategoryEnumeration EnumSubcategories(Store.EnumSubcategoriesFlags Flags, IDefinitionIdentity CategoryMatch)
		{
			return this.EnumSubcategories(Flags, CategoryMatch, null);
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x0011CE44 File Offset: 0x0011B044
		[SecuritySafeCritical]
		public StoreSubcategoryEnumeration EnumSubcategories(Store.EnumSubcategoriesFlags Flags, IDefinitionIdentity Category, string SearchPattern)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_SUBCATEGORY));
			object obj = this._pStore.EnumSubcategories((uint)Flags, Category, SearchPattern, ref guidOfType);
			return new StoreSubcategoryEnumeration((IEnumSTORE_CATEGORY_SUBCATEGORY)obj);
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x0011CE80 File Offset: 0x0011B080
		[SecuritySafeCritical]
		public StoreCategoryInstanceEnumeration EnumCategoryInstances(Store.EnumCategoryInstancesFlags Flags, IDefinitionIdentity Category, string SubCat)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_INSTANCE));
			object obj = this._pStore.EnumCategoryInstances((uint)Flags, Category, SubCat, ref guidOfType);
			return new StoreCategoryInstanceEnumeration((IEnumSTORE_CATEGORY_INSTANCE)obj);
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x0011CEBC File Offset: 0x0011B0BC
		[SecurityCritical]
		public byte[] GetDeploymentProperty(Store.GetPackagePropertyFlags Flags, IDefinitionAppId Deployment, StoreApplicationReference Reference, Guid PropertySet, string PropertyName)
		{
			BLOB blob = default(BLOB);
			byte[] array = null;
			try
			{
				this._pStore.GetDeploymentProperty((uint)Flags, Deployment, ref Reference, ref PropertySet, PropertyName, out blob);
				array = new byte[blob.Size];
				Marshal.Copy(blob.BlobData, array, 0, (int)blob.Size);
			}
			finally
			{
				blob.Dispose();
			}
			return array;
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x0011CF24 File Offset: 0x0011B124
		[SecuritySafeCritical]
		public StoreDeploymentMetadataEnumeration EnumInstallerDeployments(Guid InstallerId, string InstallerName, string InstallerMetadata, IReferenceAppId DeploymentFilter)
		{
			StoreApplicationReference storeApplicationReference = new StoreApplicationReference(InstallerId, InstallerName, InstallerMetadata);
			object obj = this._pStore.EnumInstallerDeploymentMetadata(0U, ref storeApplicationReference, DeploymentFilter, ref IsolationInterop.IID_IEnumSTORE_DEPLOYMENT_METADATA);
			return new StoreDeploymentMetadataEnumeration((IEnumSTORE_DEPLOYMENT_METADATA)obj);
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x0011CF60 File Offset: 0x0011B160
		[SecuritySafeCritical]
		public StoreDeploymentMetadataPropertyEnumeration EnumInstallerDeploymentProperties(Guid InstallerId, string InstallerName, string InstallerMetadata, IDefinitionAppId Deployment)
		{
			StoreApplicationReference storeApplicationReference = new StoreApplicationReference(InstallerId, InstallerName, InstallerMetadata);
			object obj = this._pStore.EnumInstallerDeploymentMetadataProperties(0U, ref storeApplicationReference, Deployment, ref IsolationInterop.IID_IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY);
			return new StoreDeploymentMetadataPropertyEnumeration((IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY)obj);
		}

		// Token: 0x0400226F RID: 8815
		private IStore _pStore;

		// Token: 0x02000C57 RID: 3159
		[Flags]
		public enum EnumAssembliesFlags
		{
			// Token: 0x0400379C RID: 14236
			Nothing = 0,
			// Token: 0x0400379D RID: 14237
			VisibleOnly = 1,
			// Token: 0x0400379E RID: 14238
			MatchServicing = 2,
			// Token: 0x0400379F RID: 14239
			ForceLibrarySemantics = 4
		}

		// Token: 0x02000C58 RID: 3160
		[Flags]
		public enum EnumAssemblyFilesFlags
		{
			// Token: 0x040037A1 RID: 14241
			Nothing = 0,
			// Token: 0x040037A2 RID: 14242
			IncludeInstalled = 1,
			// Token: 0x040037A3 RID: 14243
			IncludeMissing = 2
		}

		// Token: 0x02000C59 RID: 3161
		[Flags]
		public enum EnumApplicationPrivateFiles
		{
			// Token: 0x040037A5 RID: 14245
			Nothing = 0,
			// Token: 0x040037A6 RID: 14246
			IncludeInstalled = 1,
			// Token: 0x040037A7 RID: 14247
			IncludeMissing = 2
		}

		// Token: 0x02000C5A RID: 3162
		[Flags]
		public enum EnumAssemblyInstallReferenceFlags
		{
			// Token: 0x040037A9 RID: 14249
			Nothing = 0
		}

		// Token: 0x02000C5B RID: 3163
		public interface IPathLock : IDisposable
		{
			// Token: 0x1700134F RID: 4943
			// (get) Token: 0x06007073 RID: 28787
			string Path { get; }
		}

		// Token: 0x02000C5C RID: 3164
		private class AssemblyPathLock : Store.IPathLock, IDisposable
		{
			// Token: 0x06007074 RID: 28788 RVA: 0x00183146 File Offset: 0x00181346
			public AssemblyPathLock(IStore s, IntPtr c, string path)
			{
				this._pSourceStore = s;
				this._pLockCookie = c;
				this._path = path;
			}

			// Token: 0x06007075 RID: 28789 RVA: 0x0018316E File Offset: 0x0018136E
			[SecuritySafeCritical]
			private void Dispose(bool fDisposing)
			{
				if (fDisposing)
				{
					GC.SuppressFinalize(this);
				}
				if (this._pLockCookie != IntPtr.Zero)
				{
					this._pSourceStore.ReleaseAssemblyPath(this._pLockCookie);
					this._pLockCookie = IntPtr.Zero;
				}
			}

			// Token: 0x06007076 RID: 28790 RVA: 0x001831A8 File Offset: 0x001813A8
			~AssemblyPathLock()
			{
				this.Dispose(false);
			}

			// Token: 0x06007077 RID: 28791 RVA: 0x001831D8 File Offset: 0x001813D8
			void IDisposable.Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x17001350 RID: 4944
			// (get) Token: 0x06007078 RID: 28792 RVA: 0x001831E1 File Offset: 0x001813E1
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x040037AA RID: 14250
			private IStore _pSourceStore;

			// Token: 0x040037AB RID: 14251
			private IntPtr _pLockCookie = IntPtr.Zero;

			// Token: 0x040037AC RID: 14252
			private string _path;
		}

		// Token: 0x02000C5D RID: 3165
		private class ApplicationPathLock : Store.IPathLock, IDisposable
		{
			// Token: 0x06007079 RID: 28793 RVA: 0x001831E9 File Offset: 0x001813E9
			public ApplicationPathLock(IStore s, IntPtr c, string path)
			{
				this._pSourceStore = s;
				this._pLockCookie = c;
				this._path = path;
			}

			// Token: 0x0600707A RID: 28794 RVA: 0x00183211 File Offset: 0x00181411
			[SecuritySafeCritical]
			private void Dispose(bool fDisposing)
			{
				if (fDisposing)
				{
					GC.SuppressFinalize(this);
				}
				if (this._pLockCookie != IntPtr.Zero)
				{
					this._pSourceStore.ReleaseApplicationPath(this._pLockCookie);
					this._pLockCookie = IntPtr.Zero;
				}
			}

			// Token: 0x0600707B RID: 28795 RVA: 0x0018324C File Offset: 0x0018144C
			~ApplicationPathLock()
			{
				this.Dispose(false);
			}

			// Token: 0x0600707C RID: 28796 RVA: 0x0018327C File Offset: 0x0018147C
			void IDisposable.Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x17001351 RID: 4945
			// (get) Token: 0x0600707D RID: 28797 RVA: 0x00183285 File Offset: 0x00181485
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x040037AD RID: 14253
			private IStore _pSourceStore;

			// Token: 0x040037AE RID: 14254
			private IntPtr _pLockCookie = IntPtr.Zero;

			// Token: 0x040037AF RID: 14255
			private string _path;
		}

		// Token: 0x02000C5E RID: 3166
		[Flags]
		public enum EnumCategoriesFlags
		{
			// Token: 0x040037B1 RID: 14257
			Nothing = 0
		}

		// Token: 0x02000C5F RID: 3167
		[Flags]
		public enum EnumSubcategoriesFlags
		{
			// Token: 0x040037B3 RID: 14259
			Nothing = 0
		}

		// Token: 0x02000C60 RID: 3168
		[Flags]
		public enum EnumCategoryInstancesFlags
		{
			// Token: 0x040037B5 RID: 14261
			Nothing = 0
		}

		// Token: 0x02000C61 RID: 3169
		[Flags]
		public enum GetPackagePropertyFlags
		{
			// Token: 0x040037B7 RID: 14263
			Nothing = 0
		}
	}
}
