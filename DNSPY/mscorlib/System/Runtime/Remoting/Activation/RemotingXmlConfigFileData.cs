using System;
using System.Collections;
using System.Reflection;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200089F RID: 2207
	internal class RemotingXmlConfigFileData
	{
		// Token: 0x06005D39 RID: 23865 RVA: 0x00146968 File Offset: 0x00144B68
		internal void AddInteropXmlElementEntry(string xmlElementName, string xmlElementNamespace, string urtTypeName, string urtAssemblyName)
		{
			this.TryToLoadTypeIfApplicable(urtTypeName, urtAssemblyName);
			RemotingXmlConfigFileData.InteropXmlElementEntry interopXmlElementEntry = new RemotingXmlConfigFileData.InteropXmlElementEntry(xmlElementName, xmlElementNamespace, urtTypeName, urtAssemblyName);
			this.InteropXmlElementEntries.Add(interopXmlElementEntry);
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x00146998 File Offset: 0x00144B98
		internal void AddInteropXmlTypeEntry(string xmlTypeName, string xmlTypeNamespace, string urtTypeName, string urtAssemblyName)
		{
			this.TryToLoadTypeIfApplicable(urtTypeName, urtAssemblyName);
			RemotingXmlConfigFileData.InteropXmlTypeEntry interopXmlTypeEntry = new RemotingXmlConfigFileData.InteropXmlTypeEntry(xmlTypeName, xmlTypeNamespace, urtTypeName, urtAssemblyName);
			this.InteropXmlTypeEntries.Add(interopXmlTypeEntry);
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x001469C8 File Offset: 0x00144BC8
		internal void AddPreLoadEntry(string typeName, string assemblyName)
		{
			this.TryToLoadTypeIfApplicable(typeName, assemblyName);
			RemotingXmlConfigFileData.PreLoadEntry preLoadEntry = new RemotingXmlConfigFileData.PreLoadEntry(typeName, assemblyName);
			this.PreLoadEntries.Add(preLoadEntry);
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x001469F4 File Offset: 0x00144BF4
		internal RemotingXmlConfigFileData.RemoteAppEntry AddRemoteAppEntry(string appUri)
		{
			RemotingXmlConfigFileData.RemoteAppEntry remoteAppEntry = new RemotingXmlConfigFileData.RemoteAppEntry(appUri);
			this.RemoteAppEntries.Add(remoteAppEntry);
			return remoteAppEntry;
		}

		// Token: 0x06005D3D RID: 23869 RVA: 0x00146A18 File Offset: 0x00144C18
		internal void AddServerActivatedEntry(string typeName, string assemName, ArrayList contextAttributes)
		{
			this.TryToLoadTypeIfApplicable(typeName, assemName);
			RemotingXmlConfigFileData.TypeEntry typeEntry = new RemotingXmlConfigFileData.TypeEntry(typeName, assemName, contextAttributes);
			this.ServerActivatedEntries.Add(typeEntry);
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x00146A44 File Offset: 0x00144C44
		internal RemotingXmlConfigFileData.ServerWellKnownEntry AddServerWellKnownEntry(string typeName, string assemName, ArrayList contextAttributes, string objURI, WellKnownObjectMode objMode)
		{
			this.TryToLoadTypeIfApplicable(typeName, assemName);
			RemotingXmlConfigFileData.ServerWellKnownEntry serverWellKnownEntry = new RemotingXmlConfigFileData.ServerWellKnownEntry(typeName, assemName, contextAttributes, objURI, objMode);
			this.ServerWellKnownEntries.Add(serverWellKnownEntry);
			return serverWellKnownEntry;
		}

		// Token: 0x06005D3F RID: 23871 RVA: 0x00146A74 File Offset: 0x00144C74
		private void TryToLoadTypeIfApplicable(string typeName, string assemblyName)
		{
			if (!RemotingXmlConfigFileData.LoadTypes)
			{
				return;
			}
			Assembly assembly = Assembly.Load(assemblyName);
			if (assembly == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_AssemblyLoadFailed", new object[] { assemblyName }));
			}
			Type type = assembly.GetType(typeName, false, false);
			if (type == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_BadType", new object[] { typeName }));
			}
		}

		// Token: 0x06005D40 RID: 23872 RVA: 0x00146AE4 File Offset: 0x00144CE4
		public RemotingXmlConfigFileData()
		{
		}

		// Token: 0x04002A03 RID: 10755
		internal static volatile bool LoadTypes;

		// Token: 0x04002A04 RID: 10756
		internal string ApplicationName;

		// Token: 0x04002A05 RID: 10757
		internal RemotingXmlConfigFileData.LifetimeEntry Lifetime;

		// Token: 0x04002A06 RID: 10758
		internal bool UrlObjRefMode = RemotingConfigHandler.UrlObjRefMode;

		// Token: 0x04002A07 RID: 10759
		internal RemotingXmlConfigFileData.CustomErrorsEntry CustomErrors;

		// Token: 0x04002A08 RID: 10760
		internal ArrayList ChannelEntries = new ArrayList();

		// Token: 0x04002A09 RID: 10761
		internal ArrayList InteropXmlElementEntries = new ArrayList();

		// Token: 0x04002A0A RID: 10762
		internal ArrayList InteropXmlTypeEntries = new ArrayList();

		// Token: 0x04002A0B RID: 10763
		internal ArrayList PreLoadEntries = new ArrayList();

		// Token: 0x04002A0C RID: 10764
		internal ArrayList RemoteAppEntries = new ArrayList();

		// Token: 0x04002A0D RID: 10765
		internal ArrayList ServerActivatedEntries = new ArrayList();

		// Token: 0x04002A0E RID: 10766
		internal ArrayList ServerWellKnownEntries = new ArrayList();

		// Token: 0x02000C80 RID: 3200
		internal class ChannelEntry
		{
			// Token: 0x060070D3 RID: 28883 RVA: 0x00184ADA File Offset: 0x00182CDA
			internal ChannelEntry(string typeName, string assemblyName, Hashtable properties)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemblyName;
				this.Properties = properties;
			}

			// Token: 0x04003817 RID: 14359
			internal string TypeName;

			// Token: 0x04003818 RID: 14360
			internal string AssemblyName;

			// Token: 0x04003819 RID: 14361
			internal Hashtable Properties;

			// Token: 0x0400381A RID: 14362
			internal bool DelayLoad;

			// Token: 0x0400381B RID: 14363
			internal ArrayList ClientSinkProviders = new ArrayList();

			// Token: 0x0400381C RID: 14364
			internal ArrayList ServerSinkProviders = new ArrayList();
		}

		// Token: 0x02000C81 RID: 3201
		internal class ClientWellKnownEntry
		{
			// Token: 0x060070D4 RID: 28884 RVA: 0x00184B0D File Offset: 0x00182D0D
			internal ClientWellKnownEntry(string typeName, string assemName, string url)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemName;
				this.Url = url;
			}

			// Token: 0x0400381D RID: 14365
			internal string TypeName;

			// Token: 0x0400381E RID: 14366
			internal string AssemblyName;

			// Token: 0x0400381F RID: 14367
			internal string Url;
		}

		// Token: 0x02000C82 RID: 3202
		internal class ContextAttributeEntry
		{
			// Token: 0x060070D5 RID: 28885 RVA: 0x00184B2A File Offset: 0x00182D2A
			internal ContextAttributeEntry(string typeName, string assemName, Hashtable properties)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemName;
				this.Properties = properties;
			}

			// Token: 0x04003820 RID: 14368
			internal string TypeName;

			// Token: 0x04003821 RID: 14369
			internal string AssemblyName;

			// Token: 0x04003822 RID: 14370
			internal Hashtable Properties;
		}

		// Token: 0x02000C83 RID: 3203
		internal class InteropXmlElementEntry
		{
			// Token: 0x060070D6 RID: 28886 RVA: 0x00184B47 File Offset: 0x00182D47
			internal InteropXmlElementEntry(string xmlElementName, string xmlElementNamespace, string urtTypeName, string urtAssemblyName)
			{
				this.XmlElementName = xmlElementName;
				this.XmlElementNamespace = xmlElementNamespace;
				this.UrtTypeName = urtTypeName;
				this.UrtAssemblyName = urtAssemblyName;
			}

			// Token: 0x04003823 RID: 14371
			internal string XmlElementName;

			// Token: 0x04003824 RID: 14372
			internal string XmlElementNamespace;

			// Token: 0x04003825 RID: 14373
			internal string UrtTypeName;

			// Token: 0x04003826 RID: 14374
			internal string UrtAssemblyName;
		}

		// Token: 0x02000C84 RID: 3204
		internal class CustomErrorsEntry
		{
			// Token: 0x060070D7 RID: 28887 RVA: 0x00184B6C File Offset: 0x00182D6C
			internal CustomErrorsEntry(CustomErrorsModes mode)
			{
				this.Mode = mode;
			}

			// Token: 0x04003827 RID: 14375
			internal CustomErrorsModes Mode;
		}

		// Token: 0x02000C85 RID: 3205
		internal class InteropXmlTypeEntry
		{
			// Token: 0x060070D8 RID: 28888 RVA: 0x00184B7B File Offset: 0x00182D7B
			internal InteropXmlTypeEntry(string xmlTypeName, string xmlTypeNamespace, string urtTypeName, string urtAssemblyName)
			{
				this.XmlTypeName = xmlTypeName;
				this.XmlTypeNamespace = xmlTypeNamespace;
				this.UrtTypeName = urtTypeName;
				this.UrtAssemblyName = urtAssemblyName;
			}

			// Token: 0x04003828 RID: 14376
			internal string XmlTypeName;

			// Token: 0x04003829 RID: 14377
			internal string XmlTypeNamespace;

			// Token: 0x0400382A RID: 14378
			internal string UrtTypeName;

			// Token: 0x0400382B RID: 14379
			internal string UrtAssemblyName;
		}

		// Token: 0x02000C86 RID: 3206
		internal class LifetimeEntry
		{
			// Token: 0x1700135C RID: 4956
			// (get) Token: 0x060070D9 RID: 28889 RVA: 0x00184BA0 File Offset: 0x00182DA0
			// (set) Token: 0x060070DA RID: 28890 RVA: 0x00184BA8 File Offset: 0x00182DA8
			internal TimeSpan LeaseTime
			{
				get
				{
					return this._leaseTime;
				}
				set
				{
					this._leaseTime = value;
					this.IsLeaseTimeSet = true;
				}
			}

			// Token: 0x1700135D RID: 4957
			// (get) Token: 0x060070DB RID: 28891 RVA: 0x00184BB8 File Offset: 0x00182DB8
			// (set) Token: 0x060070DC RID: 28892 RVA: 0x00184BC0 File Offset: 0x00182DC0
			internal TimeSpan RenewOnCallTime
			{
				get
				{
					return this._renewOnCallTime;
				}
				set
				{
					this._renewOnCallTime = value;
					this.IsRenewOnCallTimeSet = true;
				}
			}

			// Token: 0x1700135E RID: 4958
			// (get) Token: 0x060070DD RID: 28893 RVA: 0x00184BD0 File Offset: 0x00182DD0
			// (set) Token: 0x060070DE RID: 28894 RVA: 0x00184BD8 File Offset: 0x00182DD8
			internal TimeSpan SponsorshipTimeout
			{
				get
				{
					return this._sponsorshipTimeout;
				}
				set
				{
					this._sponsorshipTimeout = value;
					this.IsSponsorshipTimeoutSet = true;
				}
			}

			// Token: 0x1700135F RID: 4959
			// (get) Token: 0x060070DF RID: 28895 RVA: 0x00184BE8 File Offset: 0x00182DE8
			// (set) Token: 0x060070E0 RID: 28896 RVA: 0x00184BF0 File Offset: 0x00182DF0
			internal TimeSpan LeaseManagerPollTime
			{
				get
				{
					return this._leaseManagerPollTime;
				}
				set
				{
					this._leaseManagerPollTime = value;
					this.IsLeaseManagerPollTimeSet = true;
				}
			}

			// Token: 0x060070E1 RID: 28897 RVA: 0x00184C00 File Offset: 0x00182E00
			public LifetimeEntry()
			{
			}

			// Token: 0x0400382C RID: 14380
			internal bool IsLeaseTimeSet;

			// Token: 0x0400382D RID: 14381
			internal bool IsRenewOnCallTimeSet;

			// Token: 0x0400382E RID: 14382
			internal bool IsSponsorshipTimeoutSet;

			// Token: 0x0400382F RID: 14383
			internal bool IsLeaseManagerPollTimeSet;

			// Token: 0x04003830 RID: 14384
			private TimeSpan _leaseTime;

			// Token: 0x04003831 RID: 14385
			private TimeSpan _renewOnCallTime;

			// Token: 0x04003832 RID: 14386
			private TimeSpan _sponsorshipTimeout;

			// Token: 0x04003833 RID: 14387
			private TimeSpan _leaseManagerPollTime;
		}

		// Token: 0x02000C87 RID: 3207
		internal class PreLoadEntry
		{
			// Token: 0x060070E2 RID: 28898 RVA: 0x00184C08 File Offset: 0x00182E08
			public PreLoadEntry(string typeName, string assemblyName)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemblyName;
			}

			// Token: 0x04003834 RID: 14388
			internal string TypeName;

			// Token: 0x04003835 RID: 14389
			internal string AssemblyName;
		}

		// Token: 0x02000C88 RID: 3208
		internal class RemoteAppEntry
		{
			// Token: 0x060070E3 RID: 28899 RVA: 0x00184C1E File Offset: 0x00182E1E
			internal RemoteAppEntry(string appUri)
			{
				this.AppUri = appUri;
			}

			// Token: 0x060070E4 RID: 28900 RVA: 0x00184C44 File Offset: 0x00182E44
			internal void AddWellKnownEntry(string typeName, string assemName, string url)
			{
				RemotingXmlConfigFileData.ClientWellKnownEntry clientWellKnownEntry = new RemotingXmlConfigFileData.ClientWellKnownEntry(typeName, assemName, url);
				this.WellKnownObjects.Add(clientWellKnownEntry);
			}

			// Token: 0x060070E5 RID: 28901 RVA: 0x00184C68 File Offset: 0x00182E68
			internal void AddActivatedEntry(string typeName, string assemName, ArrayList contextAttributes)
			{
				RemotingXmlConfigFileData.TypeEntry typeEntry = new RemotingXmlConfigFileData.TypeEntry(typeName, assemName, contextAttributes);
				this.ActivatedObjects.Add(typeEntry);
			}

			// Token: 0x04003836 RID: 14390
			internal string AppUri;

			// Token: 0x04003837 RID: 14391
			internal ArrayList WellKnownObjects = new ArrayList();

			// Token: 0x04003838 RID: 14392
			internal ArrayList ActivatedObjects = new ArrayList();
		}

		// Token: 0x02000C89 RID: 3209
		internal class ServerWellKnownEntry : RemotingXmlConfigFileData.TypeEntry
		{
			// Token: 0x060070E6 RID: 28902 RVA: 0x00184C8B File Offset: 0x00182E8B
			internal ServerWellKnownEntry(string typeName, string assemName, ArrayList contextAttributes, string objURI, WellKnownObjectMode objMode)
				: base(typeName, assemName, contextAttributes)
			{
				this.ObjectURI = objURI;
				this.ObjectMode = objMode;
			}

			// Token: 0x04003839 RID: 14393
			internal string ObjectURI;

			// Token: 0x0400383A RID: 14394
			internal WellKnownObjectMode ObjectMode;
		}

		// Token: 0x02000C8A RID: 3210
		internal class SinkProviderEntry
		{
			// Token: 0x060070E7 RID: 28903 RVA: 0x00184CA6 File Offset: 0x00182EA6
			internal SinkProviderEntry(string typeName, string assemName, Hashtable properties, bool isFormatter)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemName;
				this.Properties = properties;
				this.IsFormatter = isFormatter;
			}

			// Token: 0x0400383B RID: 14395
			internal string TypeName;

			// Token: 0x0400383C RID: 14396
			internal string AssemblyName;

			// Token: 0x0400383D RID: 14397
			internal Hashtable Properties;

			// Token: 0x0400383E RID: 14398
			internal ArrayList ProviderData = new ArrayList();

			// Token: 0x0400383F RID: 14399
			internal bool IsFormatter;
		}

		// Token: 0x02000C8B RID: 3211
		internal class TypeEntry
		{
			// Token: 0x060070E8 RID: 28904 RVA: 0x00184CD6 File Offset: 0x00182ED6
			internal TypeEntry(string typeName, string assemName, ArrayList contextAttributes)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemName;
				this.ContextAttributes = contextAttributes;
			}

			// Token: 0x04003840 RID: 14400
			internal string TypeName;

			// Token: 0x04003841 RID: 14401
			internal string AssemblyName;

			// Token: 0x04003842 RID: 14402
			internal ArrayList ContextAttributes;
		}
	}
}
