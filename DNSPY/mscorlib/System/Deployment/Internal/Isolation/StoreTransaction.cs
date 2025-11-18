using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006B4 RID: 1716
	internal class StoreTransaction : IDisposable
	{
		// Token: 0x06005013 RID: 20499 RVA: 0x0011CF9A File Offset: 0x0011B19A
		public void Add(StoreOperationInstallDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x0011CFAE File Offset: 0x0011B1AE
		public void Add(StoreOperationPinDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x0011CFC2 File Offset: 0x0011B1C2
		public void Add(StoreOperationSetCanonicalizationContext o)
		{
			this._list.Add(o);
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x0011CFD6 File Offset: 0x0011B1D6
		public void Add(StoreOperationSetDeploymentMetadata o)
		{
			this._list.Add(o);
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x0011CFEA File Offset: 0x0011B1EA
		public void Add(StoreOperationStageComponent o)
		{
			this._list.Add(o);
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x0011CFFE File Offset: 0x0011B1FE
		public void Add(StoreOperationStageComponentFile o)
		{
			this._list.Add(o);
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x0011D012 File Offset: 0x0011B212
		public void Add(StoreOperationUninstallDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x0011D026 File Offset: 0x0011B226
		public void Add(StoreOperationUnpinDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x0011D03A File Offset: 0x0011B23A
		public void Add(StoreOperationScavenge o)
		{
			this._list.Add(o);
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x0011D050 File Offset: 0x0011B250
		~StoreTransaction()
		{
			this.Dispose(false);
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x0011D080 File Offset: 0x0011B280
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x0011D08C File Offset: 0x0011B28C
		[SecuritySafeCritical]
		private void Dispose(bool fDisposing)
		{
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
			StoreTransactionOperation[] storeOps = this._storeOps;
			this._storeOps = null;
			if (storeOps != null)
			{
				for (int num = 0; num != storeOps.Length; num++)
				{
					StoreTransactionOperation storeTransactionOperation = storeOps[num];
					if (storeTransactionOperation.Data.DataPtr != IntPtr.Zero)
					{
						switch (storeTransactionOperation.Operation)
						{
						case StoreTransactionOperationType.SetCanonicalizationContext:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationSetCanonicalizationContext));
							break;
						case StoreTransactionOperationType.StageComponent:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationStageComponent));
							break;
						case StoreTransactionOperationType.PinDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationPinDeployment));
							break;
						case StoreTransactionOperationType.UnpinDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationUnpinDeployment));
							break;
						case StoreTransactionOperationType.StageComponentFile:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationStageComponentFile));
							break;
						case StoreTransactionOperationType.InstallDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationInstallDeployment));
							break;
						case StoreTransactionOperationType.UninstallDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationUninstallDeployment));
							break;
						case StoreTransactionOperationType.SetDeploymentMetadata:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationSetDeploymentMetadata));
							break;
						case StoreTransactionOperationType.Scavenge:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationScavenge));
							break;
						}
						Marshal.FreeCoTaskMem(storeTransactionOperation.Data.DataPtr);
					}
				}
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x0600501F RID: 20511 RVA: 0x0011D24F File Offset: 0x0011B44F
		public StoreTransactionOperation[] Operations
		{
			get
			{
				if (this._storeOps == null)
				{
					this._storeOps = this.GenerateStoreOpsList();
				}
				return this._storeOps;
			}
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x0011D26C File Offset: 0x0011B46C
		[SecuritySafeCritical]
		private StoreTransactionOperation[] GenerateStoreOpsList()
		{
			StoreTransactionOperation[] array = new StoreTransactionOperation[this._list.Count];
			for (int num = 0; num != this._list.Count; num++)
			{
				object obj = this._list[num];
				Type type = obj.GetType();
				array[num].Data.DataPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(obj));
				Marshal.StructureToPtr(obj, array[num].Data.DataPtr, false);
				if (type == typeof(StoreOperationSetCanonicalizationContext))
				{
					array[num].Operation = StoreTransactionOperationType.SetCanonicalizationContext;
				}
				else if (type == typeof(StoreOperationStageComponent))
				{
					array[num].Operation = StoreTransactionOperationType.StageComponent;
				}
				else if (type == typeof(StoreOperationPinDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.PinDeployment;
				}
				else if (type == typeof(StoreOperationUnpinDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.UnpinDeployment;
				}
				else if (type == typeof(StoreOperationStageComponentFile))
				{
					array[num].Operation = StoreTransactionOperationType.StageComponentFile;
				}
				else if (type == typeof(StoreOperationInstallDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.InstallDeployment;
				}
				else if (type == typeof(StoreOperationUninstallDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.UninstallDeployment;
				}
				else if (type == typeof(StoreOperationSetDeploymentMetadata))
				{
					array[num].Operation = StoreTransactionOperationType.SetDeploymentMetadata;
				}
				else
				{
					if (!(type == typeof(StoreOperationScavenge)))
					{
						throw new Exception("How did you get here?");
					}
					array[num].Operation = StoreTransactionOperationType.Scavenge;
				}
			}
			return array;
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x0011D43B File Offset: 0x0011B63B
		public StoreTransaction()
		{
		}

		// Token: 0x04002279 RID: 8825
		private ArrayList _list = new ArrayList();

		// Token: 0x0400227A RID: 8826
		private StoreTransactionOperation[] _storeOps;
	}
}
