using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Policy
{
	// Token: 0x02000349 RID: 841
	internal sealed class AssemblyEvidenceFactory : IRuntimeEvidenceFactory
	{
		// Token: 0x060029CB RID: 10699 RVA: 0x0009A436 File Offset: 0x00098636
		private AssemblyEvidenceFactory(RuntimeAssembly targetAssembly, PEFileEvidenceFactory peFileFactory)
		{
			this.m_targetAssembly = targetAssembly;
			this.m_peFileFactory = peFileFactory;
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x0009A44C File Offset: 0x0009864C
		internal SafePEFileHandle PEFile
		{
			[SecurityCritical]
			get
			{
				return this.m_peFileFactory.PEFile;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x0009A459 File Offset: 0x00098659
		public IEvidenceFactory Target
		{
			get
			{
				return this.m_targetAssembly;
			}
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x0009A464 File Offset: 0x00098664
		public EvidenceBase GenerateEvidence(Type evidenceType)
		{
			EvidenceBase evidenceBase = this.m_peFileFactory.GenerateEvidence(evidenceType);
			if (evidenceBase != null)
			{
				return evidenceBase;
			}
			if (evidenceType == typeof(GacInstalled))
			{
				return this.GenerateGacEvidence();
			}
			if (evidenceType == typeof(Hash))
			{
				return this.GenerateHashEvidence();
			}
			if (evidenceType == typeof(PermissionRequestEvidence))
			{
				return this.GeneratePermissionRequestEvidence();
			}
			if (evidenceType == typeof(StrongName))
			{
				return this.GenerateStrongNameEvidence();
			}
			return null;
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x0009A4E8 File Offset: 0x000986E8
		private GacInstalled GenerateGacEvidence()
		{
			if (!this.m_targetAssembly.GlobalAssemblyCache)
			{
				return null;
			}
			this.m_peFileFactory.FireEvidenceGeneratedEvent(EvidenceTypeGenerated.Gac);
			return new GacInstalled();
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x0009A50A File Offset: 0x0009870A
		private Hash GenerateHashEvidence()
		{
			if (this.m_targetAssembly.IsDynamic)
			{
				return null;
			}
			this.m_peFileFactory.FireEvidenceGeneratedEvent(EvidenceTypeGenerated.Hash);
			return new Hash(this.m_targetAssembly);
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x0009A534 File Offset: 0x00098734
		[SecuritySafeCritical]
		private PermissionRequestEvidence GeneratePermissionRequestEvidence()
		{
			PermissionSet permissionSet = null;
			PermissionSet permissionSet2 = null;
			PermissionSet permissionSet3 = null;
			AssemblyEvidenceFactory.GetAssemblyPermissionRequests(this.m_targetAssembly.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref permissionSet), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref permissionSet2), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref permissionSet3));
			if (permissionSet != null || permissionSet2 != null || permissionSet3 != null)
			{
				return new PermissionRequestEvidence(permissionSet, permissionSet2, permissionSet3);
			}
			return null;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x0009A580 File Offset: 0x00098780
		[SecuritySafeCritical]
		private StrongName GenerateStrongNameEvidence()
		{
			byte[] array = null;
			string text = null;
			ushort num = 0;
			ushort num2 = 0;
			ushort num3 = 0;
			ushort num4 = 0;
			AssemblyEvidenceFactory.GetStrongNameInformation(this.m_targetAssembly.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<byte[]>(ref array), JitHelpers.GetStringHandleOnStack(ref text), out num, out num2, out num3, out num4);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return new StrongName(new StrongNamePublicKeyBlob(array), text, new Version((int)num, (int)num2, (int)num3, (int)num4), this.m_targetAssembly);
		}

		// Token: 0x060029D3 RID: 10707
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetAssemblyPermissionRequests(RuntimeAssembly assembly, ObjectHandleOnStack retMinimumPermissions, ObjectHandleOnStack retOptionalPermissions, ObjectHandleOnStack retRefusedPermissions);

		// Token: 0x060029D4 RID: 10708 RVA: 0x0009A5E7 File Offset: 0x000987E7
		public IEnumerable<EvidenceBase> GetFactorySuppliedEvidence()
		{
			return this.m_peFileFactory.GetFactorySuppliedEvidence();
		}

		// Token: 0x060029D5 RID: 10709
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetStrongNameInformation(RuntimeAssembly assembly, ObjectHandleOnStack retPublicKeyBlob, StringHandleOnStack retSimpleName, out ushort majorVersion, out ushort minorVersion, out ushort build, out ushort revision);

		// Token: 0x060029D6 RID: 10710 RVA: 0x0009A5F4 File Offset: 0x000987F4
		[SecurityCritical]
		private static Evidence UpgradeSecurityIdentity(Evidence peFileEvidence, RuntimeAssembly targetAssembly)
		{
			peFileEvidence.Target = new AssemblyEvidenceFactory(targetAssembly, peFileEvidence.Target as PEFileEvidenceFactory);
			HostSecurityManager hostSecurityManager = AppDomain.CurrentDomain.HostSecurityManager;
			if ((hostSecurityManager.Flags & HostSecurityManagerOptions.HostAssemblyEvidence) == HostSecurityManagerOptions.HostAssemblyEvidence)
			{
				peFileEvidence = hostSecurityManager.ProvideAssemblyEvidence(targetAssembly, peFileEvidence);
				if (peFileEvidence == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Policy_NullHostEvidence", new object[]
					{
						hostSecurityManager.GetType().FullName,
						targetAssembly.FullName
					}));
				}
			}
			return peFileEvidence;
		}

		// Token: 0x04001126 RID: 4390
		private PEFileEvidenceFactory m_peFileFactory;

		// Token: 0x04001127 RID: 4391
		private RuntimeAssembly m_targetAssembly;
	}
}
