using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x0200035D RID: 861
	[ComVisible(true)]
	public class TrustManagerContext
	{
		// Token: 0x06002A80 RID: 10880 RVA: 0x0009CFEA File Offset: 0x0009B1EA
		public TrustManagerContext()
			: this(TrustManagerUIContext.Run)
		{
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x0009CFF3 File Offset: 0x0009B1F3
		public TrustManagerContext(TrustManagerUIContext uiContext)
		{
			this.m_ignorePersistedDecision = false;
			this.m_uiContext = uiContext;
			this.m_keepAlive = false;
			this.m_persist = true;
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x0009D017 File Offset: 0x0009B217
		// (set) Token: 0x06002A83 RID: 10883 RVA: 0x0009D01F File Offset: 0x0009B21F
		public virtual TrustManagerUIContext UIContext
		{
			get
			{
				return this.m_uiContext;
			}
			set
			{
				this.m_uiContext = value;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x0009D028 File Offset: 0x0009B228
		// (set) Token: 0x06002A85 RID: 10885 RVA: 0x0009D030 File Offset: 0x0009B230
		public virtual bool NoPrompt
		{
			get
			{
				return this.m_noPrompt;
			}
			set
			{
				this.m_noPrompt = value;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x0009D039 File Offset: 0x0009B239
		// (set) Token: 0x06002A87 RID: 10887 RVA: 0x0009D041 File Offset: 0x0009B241
		public virtual bool IgnorePersistedDecision
		{
			get
			{
				return this.m_ignorePersistedDecision;
			}
			set
			{
				this.m_ignorePersistedDecision = value;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x0009D04A File Offset: 0x0009B24A
		// (set) Token: 0x06002A89 RID: 10889 RVA: 0x0009D052 File Offset: 0x0009B252
		public virtual bool KeepAlive
		{
			get
			{
				return this.m_keepAlive;
			}
			set
			{
				this.m_keepAlive = value;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x0009D05B File Offset: 0x0009B25B
		// (set) Token: 0x06002A8B RID: 10891 RVA: 0x0009D063 File Offset: 0x0009B263
		public virtual bool Persist
		{
			get
			{
				return this.m_persist;
			}
			set
			{
				this.m_persist = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x0009D06C File Offset: 0x0009B26C
		// (set) Token: 0x06002A8D RID: 10893 RVA: 0x0009D074 File Offset: 0x0009B274
		public virtual ApplicationIdentity PreviousApplicationIdentity
		{
			get
			{
				return this.m_appId;
			}
			set
			{
				this.m_appId = value;
			}
		}

		// Token: 0x04001148 RID: 4424
		private bool m_ignorePersistedDecision;

		// Token: 0x04001149 RID: 4425
		private TrustManagerUIContext m_uiContext;

		// Token: 0x0400114A RID: 4426
		private bool m_noPrompt;

		// Token: 0x0400114B RID: 4427
		private bool m_keepAlive;

		// Token: 0x0400114C RID: 4428
		private bool m_persist;

		// Token: 0x0400114D RID: 4429
		private ApplicationIdentity m_appId;
	}
}
