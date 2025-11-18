using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000838 RID: 2104
	[Serializable]
	internal class CrossAppDomainChannel : IChannel, IChannelSender, IChannelReceiver
	{
		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x060059D8 RID: 23000 RVA: 0x0013CA09 File Offset: 0x0013AC09
		// (set) Token: 0x060059D9 RID: 23001 RVA: 0x0013CA1F File Offset: 0x0013AC1F
		private static CrossAppDomainChannel gAppDomainChannel
		{
			get
			{
				return Thread.GetDomain().RemotingData.ChannelServicesData.xadmessageSink;
			}
			set
			{
				Thread.GetDomain().RemotingData.ChannelServicesData.xadmessageSink = value;
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x060059DA RID: 23002 RVA: 0x0013CA38 File Offset: 0x0013AC38
		internal static CrossAppDomainChannel AppDomainChannel
		{
			get
			{
				if (CrossAppDomainChannel.gAppDomainChannel == null)
				{
					CrossAppDomainChannel crossAppDomainChannel = new CrossAppDomainChannel();
					object obj = CrossAppDomainChannel.staticSyncObject;
					lock (obj)
					{
						if (CrossAppDomainChannel.gAppDomainChannel == null)
						{
							CrossAppDomainChannel.gAppDomainChannel = crossAppDomainChannel;
						}
					}
				}
				return CrossAppDomainChannel.gAppDomainChannel;
			}
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x0013CA90 File Offset: 0x0013AC90
		[SecurityCritical]
		internal static void RegisterChannel()
		{
			CrossAppDomainChannel appDomainChannel = CrossAppDomainChannel.AppDomainChannel;
			ChannelServices.RegisterChannelInternal(appDomainChannel, false);
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x060059DC RID: 23004 RVA: 0x0013CAAA File Offset: 0x0013ACAA
		public virtual string ChannelName
		{
			[SecurityCritical]
			get
			{
				return "XAPPDMN";
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x060059DD RID: 23005 RVA: 0x0013CAB1 File Offset: 0x0013ACB1
		public virtual string ChannelURI
		{
			get
			{
				return "XAPPDMN_URI";
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x060059DE RID: 23006 RVA: 0x0013CAB8 File Offset: 0x0013ACB8
		public virtual int ChannelPriority
		{
			[SecurityCritical]
			get
			{
				return 100;
			}
		}

		// Token: 0x060059DF RID: 23007 RVA: 0x0013CABC File Offset: 0x0013ACBC
		[SecurityCritical]
		public string Parse(string url, out string objectURI)
		{
			objectURI = url;
			return null;
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x060059E0 RID: 23008 RVA: 0x0013CAC2 File Offset: 0x0013ACC2
		public virtual object ChannelData
		{
			[SecurityCritical]
			get
			{
				return new CrossAppDomainData(Context.DefaultContext.InternalContextID, Thread.GetDomain().GetId(), Identity.ProcessGuid);
			}
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x0013CAE4 File Offset: 0x0013ACE4
		[SecurityCritical]
		public virtual IMessageSink CreateMessageSink(string url, object data, out string objectURI)
		{
			objectURI = null;
			IMessageSink messageSink = null;
			if (url != null && data == null)
			{
				if (url.StartsWith("XAPPDMN", StringComparison.Ordinal))
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_AppDomains_NYI"));
				}
			}
			else
			{
				CrossAppDomainData crossAppDomainData = data as CrossAppDomainData;
				if (crossAppDomainData != null && crossAppDomainData.ProcessGuid.Equals(Identity.ProcessGuid))
				{
					messageSink = CrossAppDomainSink.FindOrCreateSink(crossAppDomainData);
				}
			}
			return messageSink;
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x0013CB3E File Offset: 0x0013AD3E
		[SecurityCritical]
		public virtual string[] GetUrlsForUri(string objectURI)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x060059E3 RID: 23011 RVA: 0x0013CB4F File Offset: 0x0013AD4F
		[SecurityCritical]
		public virtual void StartListening(object data)
		{
		}

		// Token: 0x060059E4 RID: 23012 RVA: 0x0013CB51 File Offset: 0x0013AD51
		[SecurityCritical]
		public virtual void StopListening(object data)
		{
		}

		// Token: 0x060059E5 RID: 23013 RVA: 0x0013CB53 File Offset: 0x0013AD53
		public CrossAppDomainChannel()
		{
		}

		// Token: 0x060059E6 RID: 23014 RVA: 0x0013CB5B File Offset: 0x0013AD5B
		// Note: this type is marked as 'beforefieldinit'.
		static CrossAppDomainChannel()
		{
		}

		// Token: 0x040028ED RID: 10477
		private const string _channelName = "XAPPDMN";

		// Token: 0x040028EE RID: 10478
		private const string _channelURI = "XAPPDMN_URI";

		// Token: 0x040028EF RID: 10479
		private static object staticSyncObject = new object();

		// Token: 0x040028F0 RID: 10480
		private static PermissionSet s_fullTrust = new PermissionSet(PermissionState.Unrestricted);
	}
}
