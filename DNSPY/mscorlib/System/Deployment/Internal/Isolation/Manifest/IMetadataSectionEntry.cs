using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000714 RID: 1812
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("AB1ED79F-943E-407d-A80B-0744E3A95B28")]
	[ComImport]
	internal interface IMetadataSectionEntry
	{
		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06005104 RID: 20740
		MetadataSectionEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06005105 RID: 20741
		uint SchemaVersion
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06005106 RID: 20742
		uint ManifestFlags
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06005107 RID: 20743
		uint UsagePatterns
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06005108 RID: 20744
		IDefinitionIdentity CdfIdentity
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06005109 RID: 20745
		string LocalPath
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x0600510A RID: 20746
		uint HashAlgorithm
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x0600510B RID: 20747
		object ManifestHash
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x0600510C RID: 20748
		string ContentType
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x0600510D RID: 20749
		string RuntimeImageVersion
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x0600510E RID: 20750
		object MvidValue
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x0600510F RID: 20751
		IDescriptionMetadataEntry DescriptionData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06005110 RID: 20752
		IDeploymentMetadataEntry DeploymentData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06005111 RID: 20753
		IDependentOSMetadataEntry DependentOSData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06005112 RID: 20754
		string defaultPermissionSetID
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06005113 RID: 20755
		string RequestedExecutionLevel
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06005114 RID: 20756
		bool RequestedExecutionLevelUIAccess
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06005115 RID: 20757
		IReferenceIdentity ResourceTypeResourcesDependency
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06005116 RID: 20758
		IReferenceIdentity ResourceTypeManifestResourcesDependency
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06005117 RID: 20759
		string KeyInfoElement
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06005118 RID: 20760
		ICompatibleFrameworksMetadataEntry CompatibleFrameworksData
		{
			[SecurityCritical]
			get;
		}
	}
}
