using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x020007B0 RID: 1968
	internal class DomainSpecificRemotingData
	{
		// Token: 0x06005515 RID: 21781 RVA: 0x0012E154 File Offset: 0x0012C354
		internal DomainSpecificRemotingData()
		{
			this._flags = 0;
			this._ConfigLock = new object();
			this._ChannelServicesData = new ChannelServicesData();
			this._IDTableLock = new ReaderWriterLock();
			this._appDomainProperties = new IContextProperty[1];
			this._appDomainProperties[0] = new LeaseLifeTimeServiceProperty();
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06005516 RID: 21782 RVA: 0x0012E1A8 File Offset: 0x0012C3A8
		// (set) Token: 0x06005517 RID: 21783 RVA: 0x0012E1B0 File Offset: 0x0012C3B0
		internal LeaseManager LeaseManager
		{
			get
			{
				return this._LeaseManager;
			}
			set
			{
				this._LeaseManager = value;
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06005518 RID: 21784 RVA: 0x0012E1B9 File Offset: 0x0012C3B9
		internal object ConfigLock
		{
			get
			{
				return this._ConfigLock;
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06005519 RID: 21785 RVA: 0x0012E1C1 File Offset: 0x0012C3C1
		internal ReaderWriterLock IDTableLock
		{
			get
			{
				return this._IDTableLock;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x0600551A RID: 21786 RVA: 0x0012E1C9 File Offset: 0x0012C3C9
		// (set) Token: 0x0600551B RID: 21787 RVA: 0x0012E1D1 File Offset: 0x0012C3D1
		internal LocalActivator LocalActivator
		{
			[SecurityCritical]
			get
			{
				return this._LocalActivator;
			}
			[SecurityCritical]
			set
			{
				this._LocalActivator = value;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x0600551C RID: 21788 RVA: 0x0012E1DA File Offset: 0x0012C3DA
		// (set) Token: 0x0600551D RID: 21789 RVA: 0x0012E1E2 File Offset: 0x0012C3E2
		internal ActivationListener ActivationListener
		{
			get
			{
				return this._ActivationListener;
			}
			set
			{
				this._ActivationListener = value;
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x0600551E RID: 21790 RVA: 0x0012E1EB File Offset: 0x0012C3EB
		// (set) Token: 0x0600551F RID: 21791 RVA: 0x0012E1F8 File Offset: 0x0012C3F8
		internal bool InitializingActivation
		{
			get
			{
				return (this._flags & 1) == 1;
			}
			set
			{
				if (value)
				{
					this._flags |= 1;
					return;
				}
				this._flags &= -2;
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06005520 RID: 21792 RVA: 0x0012E21B File Offset: 0x0012C41B
		// (set) Token: 0x06005521 RID: 21793 RVA: 0x0012E228 File Offset: 0x0012C428
		internal bool ActivationInitialized
		{
			get
			{
				return (this._flags & 2) == 2;
			}
			set
			{
				if (value)
				{
					this._flags |= 2;
					return;
				}
				this._flags &= -3;
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06005522 RID: 21794 RVA: 0x0012E24B File Offset: 0x0012C44B
		// (set) Token: 0x06005523 RID: 21795 RVA: 0x0012E258 File Offset: 0x0012C458
		internal bool ActivatorListening
		{
			get
			{
				return (this._flags & 4) == 4;
			}
			set
			{
				if (value)
				{
					this._flags |= 4;
					return;
				}
				this._flags &= -5;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06005524 RID: 21796 RVA: 0x0012E27B File Offset: 0x0012C47B
		internal IContextProperty[] AppDomainContextProperties
		{
			get
			{
				return this._appDomainProperties;
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06005525 RID: 21797 RVA: 0x0012E283 File Offset: 0x0012C483
		internal ChannelServicesData ChannelServicesData
		{
			get
			{
				return this._ChannelServicesData;
			}
		}

		// Token: 0x04002733 RID: 10035
		private const int ACTIVATION_INITIALIZING = 1;

		// Token: 0x04002734 RID: 10036
		private const int ACTIVATION_INITIALIZED = 2;

		// Token: 0x04002735 RID: 10037
		private const int ACTIVATOR_LISTENING = 4;

		// Token: 0x04002736 RID: 10038
		[SecurityCritical]
		private LocalActivator _LocalActivator;

		// Token: 0x04002737 RID: 10039
		private ActivationListener _ActivationListener;

		// Token: 0x04002738 RID: 10040
		private IContextProperty[] _appDomainProperties;

		// Token: 0x04002739 RID: 10041
		private int _flags;

		// Token: 0x0400273A RID: 10042
		private object _ConfigLock;

		// Token: 0x0400273B RID: 10043
		private ChannelServicesData _ChannelServicesData;

		// Token: 0x0400273C RID: 10044
		private LeaseManager _LeaseManager;

		// Token: 0x0400273D RID: 10045
		private ReaderWriterLock _IDTableLock;
	}
}
