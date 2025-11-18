using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
	// Token: 0x02000A59 RID: 2649
	[ComVisible(true)]
	[Serializable]
	public sealed class ActivationArguments : EvidenceBase
	{
		// Token: 0x060066CE RID: 26318 RVA: 0x00159E53 File Offset: 0x00158053
		private ActivationArguments()
		{
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x060066CF RID: 26319 RVA: 0x00159E5B File Offset: 0x0015805B
		internal bool UseFusionActivationContext
		{
			get
			{
				return this.m_useFusionActivationContext;
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x060066D0 RID: 26320 RVA: 0x00159E63 File Offset: 0x00158063
		// (set) Token: 0x060066D1 RID: 26321 RVA: 0x00159E6B File Offset: 0x0015806B
		internal bool ActivateInstance
		{
			get
			{
				return this.m_activateInstance;
			}
			set
			{
				this.m_activateInstance = value;
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x060066D2 RID: 26322 RVA: 0x00159E74 File Offset: 0x00158074
		internal string ApplicationFullName
		{
			get
			{
				return this.m_appFullName;
			}
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x060066D3 RID: 26323 RVA: 0x00159E7C File Offset: 0x0015807C
		internal string[] ApplicationManifestPaths
		{
			get
			{
				return this.m_appManifestPaths;
			}
		}

		// Token: 0x060066D4 RID: 26324 RVA: 0x00159E84 File Offset: 0x00158084
		public ActivationArguments(ApplicationIdentity applicationIdentity)
			: this(applicationIdentity, null)
		{
		}

		// Token: 0x060066D5 RID: 26325 RVA: 0x00159E8E File Offset: 0x0015808E
		public ActivationArguments(ApplicationIdentity applicationIdentity, string[] activationData)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this.m_appFullName = applicationIdentity.FullName;
			this.m_activationData = activationData;
		}

		// Token: 0x060066D6 RID: 26326 RVA: 0x00159EB7 File Offset: 0x001580B7
		public ActivationArguments(ActivationContext activationData)
			: this(activationData, null)
		{
		}

		// Token: 0x060066D7 RID: 26327 RVA: 0x00159EC4 File Offset: 0x001580C4
		public ActivationArguments(ActivationContext activationContext, string[] activationData)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			this.m_appFullName = activationContext.Identity.FullName;
			this.m_appManifestPaths = activationContext.ManifestPaths;
			this.m_activationData = activationData;
			this.m_useFusionActivationContext = true;
		}

		// Token: 0x060066D8 RID: 26328 RVA: 0x00159F10 File Offset: 0x00158110
		internal ActivationArguments(string appFullName, string[] appManifestPaths, string[] activationData)
		{
			if (appFullName == null)
			{
				throw new ArgumentNullException("appFullName");
			}
			this.m_appFullName = appFullName;
			this.m_appManifestPaths = appManifestPaths;
			this.m_activationData = activationData;
			this.m_useFusionActivationContext = true;
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x060066D9 RID: 26329 RVA: 0x00159F42 File Offset: 0x00158142
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return new ApplicationIdentity(this.m_appFullName);
			}
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x060066DA RID: 26330 RVA: 0x00159F4F File Offset: 0x0015814F
		public ActivationContext ActivationContext
		{
			get
			{
				if (!this.UseFusionActivationContext)
				{
					return null;
				}
				if (this.m_appManifestPaths == null)
				{
					return new ActivationContext(new ApplicationIdentity(this.m_appFullName));
				}
				return new ActivationContext(new ApplicationIdentity(this.m_appFullName), this.m_appManifestPaths);
			}
		}

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x060066DB RID: 26331 RVA: 0x00159F8A File Offset: 0x0015818A
		public string[] ActivationData
		{
			get
			{
				return this.m_activationData;
			}
		}

		// Token: 0x060066DC RID: 26332 RVA: 0x00159F94 File Offset: 0x00158194
		public override EvidenceBase Clone()
		{
			ActivationArguments activationArguments = new ActivationArguments();
			activationArguments.m_useFusionActivationContext = this.m_useFusionActivationContext;
			activationArguments.m_activateInstance = this.m_activateInstance;
			activationArguments.m_appFullName = this.m_appFullName;
			if (this.m_appManifestPaths != null)
			{
				activationArguments.m_appManifestPaths = new string[this.m_appManifestPaths.Length];
				Array.Copy(this.m_appManifestPaths, activationArguments.m_appManifestPaths, activationArguments.m_appManifestPaths.Length);
			}
			if (this.m_activationData != null)
			{
				activationArguments.m_activationData = new string[this.m_activationData.Length];
				Array.Copy(this.m_activationData, activationArguments.m_activationData, activationArguments.m_activationData.Length);
			}
			activationArguments.m_activateInstance = this.m_activateInstance;
			activationArguments.m_appFullName = this.m_appFullName;
			activationArguments.m_useFusionActivationContext = this.m_useFusionActivationContext;
			return activationArguments;
		}

		// Token: 0x04002E1F RID: 11807
		private bool m_useFusionActivationContext;

		// Token: 0x04002E20 RID: 11808
		private bool m_activateInstance;

		// Token: 0x04002E21 RID: 11809
		private string m_appFullName;

		// Token: 0x04002E22 RID: 11810
		private string[] m_appManifestPaths;

		// Token: 0x04002E23 RID: 11811
		private string[] m_activationData;
	}
}
