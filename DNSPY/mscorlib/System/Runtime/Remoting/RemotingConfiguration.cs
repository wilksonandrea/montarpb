using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting
{
	// Token: 0x020007C1 RID: 1985
	[ComVisible(true)]
	public static class RemotingConfiguration
	{
		// Token: 0x060055DA RID: 21978 RVA: 0x00130BB2 File Offset: 0x0012EDB2
		[SecuritySafeCritical]
		[Obsolete("Use System.Runtime.Remoting.RemotingConfiguration.Configure(string fileName, bool ensureSecurity) instead.", false)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void Configure(string filename)
		{
			RemotingConfiguration.Configure(filename, false);
		}

		// Token: 0x060055DB RID: 21979 RVA: 0x00130BBB File Offset: 0x0012EDBB
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void Configure(string filename, bool ensureSecurity)
		{
			RemotingConfigHandler.DoConfiguration(filename, ensureSecurity);
			RemotingServices.InternalSetRemoteActivationConfigured();
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x060055DC RID: 21980 RVA: 0x00130BC9 File Offset: 0x0012EDC9
		// (set) Token: 0x060055DD RID: 21981 RVA: 0x00130BD9 File Offset: 0x0012EDD9
		public static string ApplicationName
		{
			get
			{
				if (!RemotingConfigHandler.HasApplicationNameBeenSet())
				{
					return null;
				}
				return RemotingConfigHandler.ApplicationName;
			}
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				RemotingConfigHandler.ApplicationName = value;
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x060055DE RID: 21982 RVA: 0x00130BE1 File Offset: 0x0012EDE1
		public static string ApplicationId
		{
			[SecurityCritical]
			get
			{
				return Identity.AppDomainUniqueId;
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x060055DF RID: 21983 RVA: 0x00130BE8 File Offset: 0x0012EDE8
		public static string ProcessId
		{
			[SecurityCritical]
			get
			{
				return Identity.ProcessGuid;
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x060055E0 RID: 21984 RVA: 0x00130BEF File Offset: 0x0012EDEF
		// (set) Token: 0x060055E1 RID: 21985 RVA: 0x00130BF6 File Offset: 0x0012EDF6
		public static CustomErrorsModes CustomErrorsMode
		{
			get
			{
				return RemotingConfigHandler.CustomErrorsMode;
			}
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				RemotingConfigHandler.CustomErrorsMode = value;
			}
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x00130C00 File Offset: 0x0012EE00
		public static bool CustomErrorsEnabled(bool isLocalRequest)
		{
			switch (RemotingConfiguration.CustomErrorsMode)
			{
			case CustomErrorsModes.On:
				return true;
			case CustomErrorsModes.Off:
				return false;
			case CustomErrorsModes.RemoteOnly:
				return !isLocalRequest;
			default:
				return true;
			}
		}

		// Token: 0x060055E3 RID: 21987 RVA: 0x00130C34 File Offset: 0x0012EE34
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterActivatedServiceType(Type type)
		{
			ActivatedServiceTypeEntry activatedServiceTypeEntry = new ActivatedServiceTypeEntry(type);
			RemotingConfiguration.RegisterActivatedServiceType(activatedServiceTypeEntry);
		}

		// Token: 0x060055E4 RID: 21988 RVA: 0x00130C4E File Offset: 0x0012EE4E
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
		{
			RemotingConfigHandler.RegisterActivatedServiceType(entry);
			if (!RemotingConfiguration.s_ListeningForActivationRequests)
			{
				RemotingConfiguration.s_ListeningForActivationRequests = true;
				ActivationServices.StartListeningForRemoteRequests();
			}
		}

		// Token: 0x060055E5 RID: 21989 RVA: 0x00130C6C File Offset: 0x0012EE6C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterWellKnownServiceType(Type type, string objectUri, WellKnownObjectMode mode)
		{
			WellKnownServiceTypeEntry wellKnownServiceTypeEntry = new WellKnownServiceTypeEntry(type, objectUri, mode);
			RemotingConfiguration.RegisterWellKnownServiceType(wellKnownServiceTypeEntry);
		}

		// Token: 0x060055E6 RID: 21990 RVA: 0x00130C88 File Offset: 0x0012EE88
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
		{
			RemotingConfigHandler.RegisterWellKnownServiceType(entry);
		}

		// Token: 0x060055E7 RID: 21991 RVA: 0x00130C90 File Offset: 0x0012EE90
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterActivatedClientType(Type type, string appUrl)
		{
			ActivatedClientTypeEntry activatedClientTypeEntry = new ActivatedClientTypeEntry(type, appUrl);
			RemotingConfiguration.RegisterActivatedClientType(activatedClientTypeEntry);
		}

		// Token: 0x060055E8 RID: 21992 RVA: 0x00130CAB File Offset: 0x0012EEAB
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
		{
			RemotingConfigHandler.RegisterActivatedClientType(entry);
			RemotingServices.InternalSetRemoteActivationConfigured();
		}

		// Token: 0x060055E9 RID: 21993 RVA: 0x00130CB8 File Offset: 0x0012EEB8
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterWellKnownClientType(Type type, string objectUrl)
		{
			WellKnownClientTypeEntry wellKnownClientTypeEntry = new WellKnownClientTypeEntry(type, objectUrl);
			RemotingConfiguration.RegisterWellKnownClientType(wellKnownClientTypeEntry);
		}

		// Token: 0x060055EA RID: 21994 RVA: 0x00130CD3 File Offset: 0x0012EED3
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
		{
			RemotingConfigHandler.RegisterWellKnownClientType(entry);
			RemotingServices.InternalSetRemoteActivationConfigured();
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x00130CE0 File Offset: 0x0012EEE0
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
		{
			return RemotingConfigHandler.GetRegisteredActivatedServiceTypes();
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x00130CE7 File Offset: 0x0012EEE7
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
		{
			return RemotingConfigHandler.GetRegisteredWellKnownServiceTypes();
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x00130CEE File Offset: 0x0012EEEE
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
		{
			return RemotingConfigHandler.GetRegisteredActivatedClientTypes();
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x00130CF5 File Offset: 0x0012EEF5
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
		{
			return RemotingConfigHandler.GetRegisteredWellKnownClientTypes();
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x00130CFC File Offset: 0x0012EEFC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(Type svrType)
		{
			if (svrType == null)
			{
				throw new ArgumentNullException("svrType");
			}
			RuntimeType runtimeType = svrType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			return RemotingConfigHandler.IsRemotelyActivatedClientType(runtimeType);
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x00130D43 File Offset: 0x0012EF43
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(string typeName, string assemblyName)
		{
			return RemotingConfigHandler.IsRemotelyActivatedClientType(typeName, assemblyName);
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x00130D4C File Offset: 0x0012EF4C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static WellKnownClientTypeEntry IsWellKnownClientType(Type svrType)
		{
			if (svrType == null)
			{
				throw new ArgumentNullException("svrType");
			}
			RuntimeType runtimeType = svrType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			return RemotingConfigHandler.IsWellKnownClientType(runtimeType);
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x00130D93 File Offset: 0x0012EF93
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static WellKnownClientTypeEntry IsWellKnownClientType(string typeName, string assemblyName)
		{
			return RemotingConfigHandler.IsWellKnownClientType(typeName, assemblyName);
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x00130D9C File Offset: 0x0012EF9C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static bool IsActivationAllowed(Type svrType)
		{
			RuntimeType runtimeType = svrType as RuntimeType;
			if (svrType != null && runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			return RemotingConfigHandler.IsActivationAllowed(runtimeType);
		}

		// Token: 0x04002784 RID: 10116
		private static volatile bool s_ListeningForActivationRequests;
	}
}
