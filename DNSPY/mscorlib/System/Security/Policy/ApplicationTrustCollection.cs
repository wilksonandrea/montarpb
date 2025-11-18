using System;
using System.Collections;
using System.Deployment.Internal.Isolation;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Security.Policy
{
	// Token: 0x02000347 RID: 839
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class ApplicationTrustCollection : ICollection, IEnumerable
	{
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x00099CCB File Offset: 0x00097ECB
		private static StoreApplicationReference InstallReference
		{
			get
			{
				if (ApplicationTrustCollection.s_installReference == null)
				{
					Interlocked.CompareExchange(ref ApplicationTrustCollection.s_installReference, new StoreApplicationReference(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{60051b8f-4f12-400a-8e50-dd05ebd438d1}", null), null);
				}
				return (StoreApplicationReference)ApplicationTrustCollection.s_installReference;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x00099D00 File Offset: 0x00097F00
		private ArrayList AppTrusts
		{
			[SecurityCritical]
			get
			{
				if (this.m_appTrusts == null)
				{
					ArrayList arrayList = new ArrayList();
					if (this.m_storeBounded)
					{
						this.RefreshStorePointer();
						StoreDeploymentMetadataEnumeration storeDeploymentMetadataEnumeration = this.m_pStore.EnumInstallerDeployments(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{60051b8f-4f12-400a-8e50-dd05ebd438d1}", "ApplicationTrust", null);
						foreach (object obj in storeDeploymentMetadataEnumeration)
						{
							IDefinitionAppId definitionAppId = (IDefinitionAppId)obj;
							StoreDeploymentMetadataPropertyEnumeration storeDeploymentMetadataPropertyEnumeration = this.m_pStore.EnumInstallerDeploymentProperties(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{60051b8f-4f12-400a-8e50-dd05ebd438d1}", "ApplicationTrust", definitionAppId);
							foreach (object obj2 in storeDeploymentMetadataPropertyEnumeration)
							{
								StoreOperationMetadataProperty storeOperationMetadataProperty = (StoreOperationMetadataProperty)obj2;
								string value = storeOperationMetadataProperty.Value;
								if (value != null && value.Length > 0)
								{
									SecurityElement securityElement = SecurityElement.FromString(value);
									ApplicationTrust applicationTrust = new ApplicationTrust();
									applicationTrust.FromXml(securityElement);
									arrayList.Add(applicationTrust);
								}
							}
						}
					}
					Interlocked.CompareExchange(ref this.m_appTrusts, arrayList, null);
				}
				return this.m_appTrusts as ArrayList;
			}
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x00099E48 File Offset: 0x00098048
		[SecurityCritical]
		internal ApplicationTrustCollection()
			: this(false)
		{
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x00099E51 File Offset: 0x00098051
		internal ApplicationTrustCollection(bool storeBounded)
		{
			this.m_storeBounded = storeBounded;
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x00099E60 File Offset: 0x00098060
		[SecurityCritical]
		private void RefreshStorePointer()
		{
			if (this.m_pStore != null)
			{
				Marshal.ReleaseComObject(this.m_pStore.InternalStore);
			}
			this.m_pStore = IsolationInterop.GetUserStore();
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x00099E86 File Offset: 0x00098086
		public int Count
		{
			[SecuritySafeCritical]
			get
			{
				return this.AppTrusts.Count;
			}
		}

		// Token: 0x17000578 RID: 1400
		public ApplicationTrust this[int index]
		{
			[SecurityCritical]
			get
			{
				return this.AppTrusts[index] as ApplicationTrust;
			}
		}

		// Token: 0x17000579 RID: 1401
		public ApplicationTrust this[string appFullName]
		{
			[SecurityCritical]
			get
			{
				ApplicationIdentity applicationIdentity = new ApplicationIdentity(appFullName);
				ApplicationTrustCollection applicationTrustCollection = this.Find(applicationIdentity, ApplicationVersionMatch.MatchExactVersion);
				if (applicationTrustCollection.Count > 0)
				{
					return applicationTrustCollection[0];
				}
				return null;
			}
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x00099ED8 File Offset: 0x000980D8
		[SecurityCritical]
		private void CommitApplicationTrust(ApplicationIdentity applicationIdentity, string trustXml)
		{
			StoreOperationMetadataProperty[] array = new StoreOperationMetadataProperty[]
			{
				new StoreOperationMetadataProperty(ApplicationTrustCollection.ClrPropertySet, "ApplicationTrust", trustXml)
			};
			IEnumDefinitionIdentity enumDefinitionIdentity = applicationIdentity.Identity.EnumAppPath();
			IDefinitionIdentity[] array2 = new IDefinitionIdentity[1];
			IDefinitionIdentity definitionIdentity = null;
			if (enumDefinitionIdentity.Next(1U, array2) == 1U)
			{
				definitionIdentity = array2[0];
			}
			IDefinitionAppId definitionAppId = IsolationInterop.AppIdAuthority.CreateDefinition();
			definitionAppId.SetAppPath(1U, new IDefinitionIdentity[] { definitionIdentity });
			definitionAppId.put_Codebase(applicationIdentity.CodeBase);
			using (StoreTransaction storeTransaction = new StoreTransaction())
			{
				storeTransaction.Add(new StoreOperationSetDeploymentMetadata(definitionAppId, ApplicationTrustCollection.InstallReference, array));
				this.RefreshStorePointer();
				this.m_pStore.Transact(storeTransaction.Operations);
			}
			this.m_appTrusts = null;
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x00099FAC File Offset: 0x000981AC
		[SecurityCritical]
		public int Add(ApplicationTrust trust)
		{
			if (trust == null)
			{
				throw new ArgumentNullException("trust");
			}
			if (trust.ApplicationIdentity == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ApplicationTrustShouldHaveIdentity"));
			}
			if (this.m_storeBounded)
			{
				this.CommitApplicationTrust(trust.ApplicationIdentity, trust.ToXml().ToString());
				return -1;
			}
			return this.AppTrusts.Add(trust);
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x0009A00C File Offset: 0x0009820C
		[SecurityCritical]
		public void AddRange(ApplicationTrust[] trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			int i = 0;
			try
			{
				while (i < trusts.Length)
				{
					this.Add(trusts[i]);
					i++;
				}
			}
			catch
			{
				for (int j = 0; j < i; j++)
				{
					this.Remove(trusts[j]);
				}
				throw;
			}
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x0009A06C File Offset: 0x0009826C
		[SecurityCritical]
		public void AddRange(ApplicationTrustCollection trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			int num = 0;
			try
			{
				foreach (ApplicationTrust applicationTrust in trusts)
				{
					this.Add(applicationTrust);
					num++;
				}
			}
			catch
			{
				for (int i = 0; i < num; i++)
				{
					this.Remove(trusts[i]);
				}
				throw;
			}
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x0009A0DC File Offset: 0x000982DC
		[SecurityCritical]
		public ApplicationTrustCollection Find(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
		{
			ApplicationTrustCollection applicationTrustCollection = new ApplicationTrustCollection(false);
			foreach (ApplicationTrust applicationTrust in this)
			{
				if (CmsUtils.CompareIdentities(applicationTrust.ApplicationIdentity, applicationIdentity, versionMatch))
				{
					applicationTrustCollection.Add(applicationTrust);
				}
			}
			return applicationTrustCollection;
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x0009A120 File Offset: 0x00098320
		[SecurityCritical]
		public void Remove(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
		{
			ApplicationTrustCollection applicationTrustCollection = this.Find(applicationIdentity, versionMatch);
			this.RemoveRange(applicationTrustCollection);
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x0009A140 File Offset: 0x00098340
		[SecurityCritical]
		public void Remove(ApplicationTrust trust)
		{
			if (trust == null)
			{
				throw new ArgumentNullException("trust");
			}
			if (trust.ApplicationIdentity == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ApplicationTrustShouldHaveIdentity"));
			}
			if (this.m_storeBounded)
			{
				this.CommitApplicationTrust(trust.ApplicationIdentity, null);
				return;
			}
			this.AppTrusts.Remove(trust);
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x0009A198 File Offset: 0x00098398
		[SecurityCritical]
		public void RemoveRange(ApplicationTrust[] trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			int i = 0;
			try
			{
				while (i < trusts.Length)
				{
					this.Remove(trusts[i]);
					i++;
				}
			}
			catch
			{
				for (int j = 0; j < i; j++)
				{
					this.Add(trusts[j]);
				}
				throw;
			}
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x0009A1F8 File Offset: 0x000983F8
		[SecurityCritical]
		public void RemoveRange(ApplicationTrustCollection trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			int num = 0;
			try
			{
				foreach (ApplicationTrust applicationTrust in trusts)
				{
					this.Remove(applicationTrust);
					num++;
				}
			}
			catch
			{
				for (int i = 0; i < num; i++)
				{
					this.Add(trusts[i]);
				}
				throw;
			}
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x0009A268 File Offset: 0x00098468
		[SecurityCritical]
		public void Clear()
		{
			ArrayList appTrusts = this.AppTrusts;
			if (this.m_storeBounded)
			{
				foreach (object obj in appTrusts)
				{
					ApplicationTrust applicationTrust = (ApplicationTrust)obj;
					if (applicationTrust.ApplicationIdentity == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_ApplicationTrustShouldHaveIdentity"));
					}
					this.CommitApplicationTrust(applicationTrust.ApplicationIdentity, null);
				}
			}
			appTrusts.Clear();
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x0009A2F0 File Offset: 0x000984F0
		public ApplicationTrustEnumerator GetEnumerator()
		{
			return new ApplicationTrustEnumerator(this);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x0009A2F8 File Offset: 0x000984F8
		[SecuritySafeCritical]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ApplicationTrustEnumerator(this);
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x0009A300 File Offset: 0x00098500
		[SecuritySafeCritical]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index++);
			}
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x0009A39A File Offset: 0x0009859A
		public void CopyTo(ApplicationTrust[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x0009A3A4 File Offset: 0x000985A4
		public bool IsSynchronized
		{
			[SecuritySafeCritical]
			get
			{
				return false;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x0009A3A7 File Offset: 0x000985A7
		public object SyncRoot
		{
			[SecuritySafeCritical]
			get
			{
				return this;
			}
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x0009A3AA File Offset: 0x000985AA
		// Note: this type is marked as 'beforefieldinit'.
		static ApplicationTrustCollection()
		{
		}

		// Token: 0x0400111D RID: 4381
		private const string ApplicationTrustProperty = "ApplicationTrust";

		// Token: 0x0400111E RID: 4382
		private const string InstallerIdentifier = "{60051b8f-4f12-400a-8e50-dd05ebd438d1}";

		// Token: 0x0400111F RID: 4383
		private static Guid ClrPropertySet = new Guid("c989bb7a-8385-4715-98cf-a741a8edb823");

		// Token: 0x04001120 RID: 4384
		private static object s_installReference = null;

		// Token: 0x04001121 RID: 4385
		private object m_appTrusts;

		// Token: 0x04001122 RID: 4386
		private bool m_storeBounded;

		// Token: 0x04001123 RID: 4387
		private Store m_pStore;
	}
}
