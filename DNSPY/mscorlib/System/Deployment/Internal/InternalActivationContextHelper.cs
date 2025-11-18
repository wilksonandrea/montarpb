using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal
{
	// Token: 0x0200066F RID: 1647
	[ComVisible(false)]
	public static class InternalActivationContextHelper
	{
		// Token: 0x06004F1C RID: 20252 RVA: 0x0011C13C File Offset: 0x0011A33C
		[SecuritySafeCritical]
		public static object GetActivationContextData(ActivationContext appInfo)
		{
			return appInfo.ActivationContextData;
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x0011C144 File Offset: 0x0011A344
		[SecuritySafeCritical]
		public static object GetApplicationComponentManifest(ActivationContext appInfo)
		{
			return appInfo.ApplicationComponentManifest;
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x0011C14C File Offset: 0x0011A34C
		[SecuritySafeCritical]
		public static object GetDeploymentComponentManifest(ActivationContext appInfo)
		{
			return appInfo.DeploymentComponentManifest;
		}

		// Token: 0x06004F1F RID: 20255 RVA: 0x0011C154 File Offset: 0x0011A354
		public static void PrepareForExecution(ActivationContext appInfo)
		{
			appInfo.PrepareForExecution();
		}

		// Token: 0x06004F20 RID: 20256 RVA: 0x0011C15C File Offset: 0x0011A35C
		public static bool IsFirstRun(ActivationContext appInfo)
		{
			return appInfo.LastApplicationStateResult == ActivationContext.ApplicationStateDisposition.RunningFirstTime;
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x0011C16B File Offset: 0x0011A36B
		public static byte[] GetApplicationManifestBytes(ActivationContext appInfo)
		{
			if (appInfo == null)
			{
				throw new ArgumentNullException("appInfo");
			}
			return appInfo.GetApplicationManifestBytes();
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x0011C181 File Offset: 0x0011A381
		public static byte[] GetDeploymentManifestBytes(ActivationContext appInfo)
		{
			if (appInfo == null)
			{
				throw new ArgumentNullException("appInfo");
			}
			return appInfo.GetDeploymentManifestBytes();
		}
	}
}
