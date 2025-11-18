using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006AA RID: 1706
	internal struct StoreOperationSetDeploymentMetadata
	{
		// Token: 0x06004FDE RID: 20446 RVA: 0x0011C857 File Offset: 0x0011AA57
		public StoreOperationSetDeploymentMetadata(IDefinitionAppId Deployment, StoreApplicationReference Reference, StoreOperationMetadataProperty[] SetProperties)
		{
			this = new StoreOperationSetDeploymentMetadata(Deployment, Reference, SetProperties, null);
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x0011C864 File Offset: 0x0011AA64
		[SecuritySafeCritical]
		public StoreOperationSetDeploymentMetadata(IDefinitionAppId Deployment, StoreApplicationReference Reference, StoreOperationMetadataProperty[] SetProperties, StoreOperationMetadataProperty[] TestProperties)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationSetDeploymentMetadata));
			this.Flags = StoreOperationSetDeploymentMetadata.OpFlags.Nothing;
			this.Deployment = Deployment;
			if (SetProperties != null)
			{
				this.PropertiesToSet = StoreOperationSetDeploymentMetadata.MarshalProperties(SetProperties);
				this.cPropertiesToSet = new IntPtr(SetProperties.Length);
			}
			else
			{
				this.PropertiesToSet = IntPtr.Zero;
				this.cPropertiesToSet = IntPtr.Zero;
			}
			if (TestProperties != null)
			{
				this.PropertiesToTest = StoreOperationSetDeploymentMetadata.MarshalProperties(TestProperties);
				this.cPropertiesToTest = new IntPtr(TestProperties.Length);
			}
			else
			{
				this.PropertiesToTest = IntPtr.Zero;
				this.cPropertiesToTest = IntPtr.Zero;
			}
			this.InstallerReference = Reference.ToIntPtr();
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x0011C910 File Offset: 0x0011AB10
		[SecurityCritical]
		public void Destroy()
		{
			if (this.PropertiesToSet != IntPtr.Zero)
			{
				StoreOperationSetDeploymentMetadata.DestroyProperties(this.PropertiesToSet, (ulong)this.cPropertiesToSet.ToInt64());
				this.PropertiesToSet = IntPtr.Zero;
				this.cPropertiesToSet = IntPtr.Zero;
			}
			if (this.PropertiesToTest != IntPtr.Zero)
			{
				StoreOperationSetDeploymentMetadata.DestroyProperties(this.PropertiesToTest, (ulong)this.cPropertiesToTest.ToInt64());
				this.PropertiesToTest = IntPtr.Zero;
				this.cPropertiesToTest = IntPtr.Zero;
			}
			if (this.InstallerReference != IntPtr.Zero)
			{
				StoreApplicationReference.Destroy(this.InstallerReference);
				this.InstallerReference = IntPtr.Zero;
			}
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x0011C9C4 File Offset: 0x0011ABC4
		[SecurityCritical]
		private static void DestroyProperties(IntPtr rgItems, ulong iItems)
		{
			if (rgItems != IntPtr.Zero)
			{
				ulong num = (ulong)((long)Marshal.SizeOf(typeof(StoreOperationMetadataProperty)));
				for (ulong num2 = 0UL; num2 < iItems; num2 += 1UL)
				{
					Marshal.DestroyStructure(new IntPtr((long)(num2 * num + (ulong)rgItems.ToInt64())), typeof(StoreOperationMetadataProperty));
				}
				Marshal.FreeCoTaskMem(rgItems);
			}
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x0011CA24 File Offset: 0x0011AC24
		[SecurityCritical]
		private static IntPtr MarshalProperties(StoreOperationMetadataProperty[] Props)
		{
			if (Props == null || Props.Length == 0)
			{
				return IntPtr.Zero;
			}
			int num = Marshal.SizeOf(typeof(StoreOperationMetadataProperty));
			IntPtr intPtr = Marshal.AllocCoTaskMem(num * Props.Length);
			for (int num2 = 0; num2 != Props.Length; num2++)
			{
				Marshal.StructureToPtr<StoreOperationMetadataProperty>(Props[num2], new IntPtr((long)(num2 * num) + intPtr.ToInt64()), false);
			}
			return intPtr;
		}

		// Token: 0x04002250 RID: 8784
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002251 RID: 8785
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationSetDeploymentMetadata.OpFlags Flags;

		// Token: 0x04002252 RID: 8786
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Deployment;

		// Token: 0x04002253 RID: 8787
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr InstallerReference;

		// Token: 0x04002254 RID: 8788
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr cPropertiesToTest;

		// Token: 0x04002255 RID: 8789
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr PropertiesToTest;

		// Token: 0x04002256 RID: 8790
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr cPropertiesToSet;

		// Token: 0x04002257 RID: 8791
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr PropertiesToSet;

		// Token: 0x02000C53 RID: 3155
		[Flags]
		public enum OpFlags
		{
			// Token: 0x0400378F RID: 14223
			Nothing = 0
		}

		// Token: 0x02000C54 RID: 3156
		public enum Disposition
		{
			// Token: 0x04003791 RID: 14225
			Failed,
			// Token: 0x04003792 RID: 14226
			Set = 2
		}
	}
}
