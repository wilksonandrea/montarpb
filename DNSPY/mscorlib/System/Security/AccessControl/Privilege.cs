using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x0200022A RID: 554
	internal sealed class Privilege
	{
		// Token: 0x0600200C RID: 8204 RVA: 0x00070770 File Offset: 0x0006E970
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private static Win32Native.LUID LuidFromPrivilege(string privilege)
		{
			Win32Native.LUID luid;
			luid.LowPart = 0U;
			luid.HighPart = 0U;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Privilege.privilegeLock.AcquireReaderLock(-1);
				if (Privilege.luids.Contains(privilege))
				{
					luid = (Win32Native.LUID)Privilege.luids[privilege];
					Privilege.privilegeLock.ReleaseReaderLock();
				}
				else
				{
					Privilege.privilegeLock.ReleaseReaderLock();
					if (!Win32Native.LookupPrivilegeValue(null, privilege, ref luid))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error == 8)
						{
							throw new OutOfMemoryException();
						}
						if (lastWin32Error == 5)
						{
							throw new UnauthorizedAccessException();
						}
						if (lastWin32Error == 1313)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPrivilegeName", new object[] { privilege }));
						}
						throw new InvalidOperationException();
					}
					else
					{
						Privilege.privilegeLock.AcquireWriterLock(-1);
					}
				}
			}
			finally
			{
				if (Privilege.privilegeLock.IsReaderLockHeld)
				{
					Privilege.privilegeLock.ReleaseReaderLock();
				}
				if (Privilege.privilegeLock.IsWriterLockHeld)
				{
					if (!Privilege.luids.Contains(privilege))
					{
						Privilege.luids[privilege] = luid;
						Privilege.privileges[luid] = privilege;
					}
					Privilege.privilegeLock.ReleaseWriterLock();
				}
			}
			return luid;
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x0007089C File Offset: 0x0006EA9C
		[SecurityCritical]
		public Privilege(string privilegeName)
		{
			if (privilegeName == null)
			{
				throw new ArgumentNullException("privilegeName");
			}
			this.luid = Privilege.LuidFromPrivilege(privilegeName);
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000708CC File Offset: 0x0006EACC
		[SecuritySafeCritical]
		~Privilege()
		{
			if (this.needToRevert)
			{
				this.Revert();
			}
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00070900 File Offset: 0x0006EB00
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void Enable()
		{
			this.ToggleState(true);
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x00070909 File Offset: 0x0006EB09
		public bool NeedToRevert
		{
			get
			{
				return this.needToRevert;
			}
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00070914 File Offset: 0x0006EB14
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private void ToggleState(bool enable)
		{
			int num = 0;
			if (!this.currentThread.Equals(Thread.CurrentThread))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustBeSameThread"));
			}
			if (this.needToRevert)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustRevertPrivilege"));
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				try
				{
					this.tlsContents = Thread.GetData(Privilege.tlsSlot) as Privilege.TlsContents;
					if (this.tlsContents == null)
					{
						this.tlsContents = new Privilege.TlsContents();
						Thread.SetData(Privilege.tlsSlot, this.tlsContents);
					}
					else
					{
						this.tlsContents.IncrementReferenceCount();
					}
					Win32Native.TOKEN_PRIVILEGE token_PRIVILEGE = default(Win32Native.TOKEN_PRIVILEGE);
					token_PRIVILEGE.PrivilegeCount = 1U;
					token_PRIVILEGE.Privilege.Luid = this.luid;
					token_PRIVILEGE.Privilege.Attributes = (enable ? 2U : 0U);
					Win32Native.TOKEN_PRIVILEGE token_PRIVILEGE2 = default(Win32Native.TOKEN_PRIVILEGE);
					uint num2 = 0U;
					if (!Win32Native.AdjustTokenPrivileges(this.tlsContents.ThreadHandle, false, ref token_PRIVILEGE, (uint)Marshal.SizeOf<Win32Native.TOKEN_PRIVILEGE>(token_PRIVILEGE2), ref token_PRIVILEGE2, ref num2))
					{
						num = Marshal.GetLastWin32Error();
					}
					else if (1300 == Marshal.GetLastWin32Error())
					{
						num = 1300;
					}
					else
					{
						this.initialState = (token_PRIVILEGE2.Privilege.Attributes & 2U) > 0U;
						this.stateWasChanged = this.initialState != enable;
						this.needToRevert = this.tlsContents.IsImpersonating || this.stateWasChanged;
					}
				}
				finally
				{
					if (!this.needToRevert)
					{
						this.Reset();
					}
				}
			}
			if (num == 1300)
			{
				throw new PrivilegeNotHeldException(Privilege.privileges[this.luid] as string);
			}
			if (num == 8)
			{
				throw new OutOfMemoryException();
			}
			if (num == 5 || num == 1347)
			{
				throw new UnauthorizedAccessException();
			}
			if (num != 0)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x00070B00 File Offset: 0x0006ED00
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void Revert()
		{
			int num = 0;
			if (!this.currentThread.Equals(Thread.CurrentThread))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustBeSameThread"));
			}
			if (!this.NeedToRevert)
			{
				return;
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				bool flag = true;
				try
				{
					if (this.stateWasChanged && (this.tlsContents.ReferenceCountValue > 1 || !this.tlsContents.IsImpersonating))
					{
						Win32Native.TOKEN_PRIVILEGE token_PRIVILEGE = default(Win32Native.TOKEN_PRIVILEGE);
						token_PRIVILEGE.PrivilegeCount = 1U;
						token_PRIVILEGE.Privilege.Luid = this.luid;
						token_PRIVILEGE.Privilege.Attributes = (this.initialState ? 2U : 0U);
						Win32Native.TOKEN_PRIVILEGE token_PRIVILEGE2 = default(Win32Native.TOKEN_PRIVILEGE);
						uint num2 = 0U;
						if (!Win32Native.AdjustTokenPrivileges(this.tlsContents.ThreadHandle, false, ref token_PRIVILEGE, (uint)Marshal.SizeOf<Win32Native.TOKEN_PRIVILEGE>(token_PRIVILEGE2), ref token_PRIVILEGE2, ref num2))
						{
							num = Marshal.GetLastWin32Error();
							flag = false;
						}
					}
				}
				finally
				{
					if (flag)
					{
						this.Reset();
					}
				}
			}
			if (num == 8)
			{
				throw new OutOfMemoryException();
			}
			if (num == 5)
			{
				throw new UnauthorizedAccessException();
			}
			if (num != 0)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00070C20 File Offset: 0x0006EE20
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void Reset()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this.stateWasChanged = false;
				this.initialState = false;
				this.needToRevert = false;
				if (this.tlsContents != null && this.tlsContents.DecrementReferenceCount() == 0)
				{
					this.tlsContents = null;
					Thread.SetData(Privilege.tlsSlot, null);
				}
			}
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x00070C84 File Offset: 0x0006EE84
		// Note: this type is marked as 'beforefieldinit'.
		static Privilege()
		{
		}

		// Token: 0x04000B66 RID: 2918
		private static LocalDataStoreSlot tlsSlot = Thread.AllocateDataSlot();

		// Token: 0x04000B67 RID: 2919
		private static Hashtable privileges = new Hashtable();

		// Token: 0x04000B68 RID: 2920
		private static Hashtable luids = new Hashtable();

		// Token: 0x04000B69 RID: 2921
		private static ReaderWriterLock privilegeLock = new ReaderWriterLock();

		// Token: 0x04000B6A RID: 2922
		private bool needToRevert;

		// Token: 0x04000B6B RID: 2923
		private bool initialState;

		// Token: 0x04000B6C RID: 2924
		private bool stateWasChanged;

		// Token: 0x04000B6D RID: 2925
		[SecurityCritical]
		private Win32Native.LUID luid;

		// Token: 0x04000B6E RID: 2926
		private readonly Thread currentThread = Thread.CurrentThread;

		// Token: 0x04000B6F RID: 2927
		private Privilege.TlsContents tlsContents;

		// Token: 0x04000B70 RID: 2928
		public const string CreateToken = "SeCreateTokenPrivilege";

		// Token: 0x04000B71 RID: 2929
		public const string AssignPrimaryToken = "SeAssignPrimaryTokenPrivilege";

		// Token: 0x04000B72 RID: 2930
		public const string LockMemory = "SeLockMemoryPrivilege";

		// Token: 0x04000B73 RID: 2931
		public const string IncreaseQuota = "SeIncreaseQuotaPrivilege";

		// Token: 0x04000B74 RID: 2932
		public const string UnsolicitedInput = "SeUnsolicitedInputPrivilege";

		// Token: 0x04000B75 RID: 2933
		public const string MachineAccount = "SeMachineAccountPrivilege";

		// Token: 0x04000B76 RID: 2934
		public const string TrustedComputingBase = "SeTcbPrivilege";

		// Token: 0x04000B77 RID: 2935
		public const string Security = "SeSecurityPrivilege";

		// Token: 0x04000B78 RID: 2936
		public const string TakeOwnership = "SeTakeOwnershipPrivilege";

		// Token: 0x04000B79 RID: 2937
		public const string LoadDriver = "SeLoadDriverPrivilege";

		// Token: 0x04000B7A RID: 2938
		public const string SystemProfile = "SeSystemProfilePrivilege";

		// Token: 0x04000B7B RID: 2939
		public const string SystemTime = "SeSystemtimePrivilege";

		// Token: 0x04000B7C RID: 2940
		public const string ProfileSingleProcess = "SeProfileSingleProcessPrivilege";

		// Token: 0x04000B7D RID: 2941
		public const string IncreaseBasePriority = "SeIncreaseBasePriorityPrivilege";

		// Token: 0x04000B7E RID: 2942
		public const string CreatePageFile = "SeCreatePagefilePrivilege";

		// Token: 0x04000B7F RID: 2943
		public const string CreatePermanent = "SeCreatePermanentPrivilege";

		// Token: 0x04000B80 RID: 2944
		public const string Backup = "SeBackupPrivilege";

		// Token: 0x04000B81 RID: 2945
		public const string Restore = "SeRestorePrivilege";

		// Token: 0x04000B82 RID: 2946
		public const string Shutdown = "SeShutdownPrivilege";

		// Token: 0x04000B83 RID: 2947
		public const string Debug = "SeDebugPrivilege";

		// Token: 0x04000B84 RID: 2948
		public const string Audit = "SeAuditPrivilege";

		// Token: 0x04000B85 RID: 2949
		public const string SystemEnvironment = "SeSystemEnvironmentPrivilege";

		// Token: 0x04000B86 RID: 2950
		public const string ChangeNotify = "SeChangeNotifyPrivilege";

		// Token: 0x04000B87 RID: 2951
		public const string RemoteShutdown = "SeRemoteShutdownPrivilege";

		// Token: 0x04000B88 RID: 2952
		public const string Undock = "SeUndockPrivilege";

		// Token: 0x04000B89 RID: 2953
		public const string SyncAgent = "SeSyncAgentPrivilege";

		// Token: 0x04000B8A RID: 2954
		public const string EnableDelegation = "SeEnableDelegationPrivilege";

		// Token: 0x04000B8B RID: 2955
		public const string ManageVolume = "SeManageVolumePrivilege";

		// Token: 0x04000B8C RID: 2956
		public const string Impersonate = "SeImpersonatePrivilege";

		// Token: 0x04000B8D RID: 2957
		public const string CreateGlobal = "SeCreateGlobalPrivilege";

		// Token: 0x04000B8E RID: 2958
		public const string TrustedCredentialManagerAccess = "SeTrustedCredManAccessPrivilege";

		// Token: 0x04000B8F RID: 2959
		public const string ReserveProcessor = "SeReserveProcessorPrivilege";

		// Token: 0x02000B35 RID: 2869
		private sealed class TlsContents : IDisposable
		{
			// Token: 0x06006B74 RID: 27508 RVA: 0x0017359E File Offset: 0x0017179E
			[SecuritySafeCritical]
			static TlsContents()
			{
			}

			// Token: 0x06006B75 RID: 27509 RVA: 0x001735BC File Offset: 0x001717BC
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			public TlsContents()
			{
				int num = 0;
				int num2 = 0;
				bool flag = true;
				if (Privilege.TlsContents.processHandle.IsInvalid)
				{
					object obj = Privilege.TlsContents.syncRoot;
					lock (obj)
					{
						if (Privilege.TlsContents.processHandle.IsInvalid)
						{
							SafeAccessTokenHandle safeAccessTokenHandle;
							if (!Win32Native.OpenProcessToken(Win32Native.GetCurrentProcess(), TokenAccessLevels.Duplicate, out safeAccessTokenHandle))
							{
								num2 = Marshal.GetLastWin32Error();
								flag = false;
							}
							Privilege.TlsContents.processHandle = safeAccessTokenHandle;
						}
					}
				}
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					try
					{
						SafeAccessTokenHandle safeAccessTokenHandle2 = this.threadHandle;
						num = Win32.OpenThreadToken(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges, WinSecurityContext.Process, out this.threadHandle);
						num &= 2147024895;
						if (num != 0)
						{
							if (flag)
							{
								this.threadHandle = safeAccessTokenHandle2;
								if (num != 1008)
								{
									flag = false;
								}
								if (flag)
								{
									num = 0;
									if (!Win32Native.DuplicateTokenEx(Privilege.TlsContents.processHandle, TokenAccessLevels.Impersonate | TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges, IntPtr.Zero, Win32Native.SECURITY_IMPERSONATION_LEVEL.Impersonation, System.Security.Principal.TokenType.TokenImpersonation, ref this.threadHandle))
									{
										num = Marshal.GetLastWin32Error();
										flag = false;
									}
								}
								if (flag)
								{
									num = Win32.SetThreadToken(this.threadHandle);
									num &= 2147024895;
									if (num != 0)
									{
										flag = false;
									}
								}
								if (flag)
								{
									this.isImpersonating = true;
								}
							}
							else
							{
								num = num2;
							}
						}
						else
						{
							flag = true;
						}
					}
					finally
					{
						if (!flag)
						{
							this.Dispose();
						}
					}
				}
				if (num == 8)
				{
					throw new OutOfMemoryException();
				}
				if (num == 5 || num == 1347)
				{
					throw new UnauthorizedAccessException();
				}
				if (num != 0)
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06006B76 RID: 27510 RVA: 0x0017373C File Offset: 0x0017193C
			[SecuritySafeCritical]
			~TlsContents()
			{
				if (!this.disposed)
				{
					this.Dispose(false);
				}
			}

			// Token: 0x06006B77 RID: 27511 RVA: 0x00173774 File Offset: 0x00171974
			[SecuritySafeCritical]
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06006B78 RID: 27512 RVA: 0x00173783 File Offset: 0x00171983
			[SecurityCritical]
			private void Dispose(bool disposing)
			{
				if (this.disposed)
				{
					return;
				}
				if (disposing && this.threadHandle != null)
				{
					this.threadHandle.Dispose();
					this.threadHandle = null;
				}
				if (this.isImpersonating)
				{
					Win32.RevertToSelf();
				}
				this.disposed = true;
			}

			// Token: 0x06006B79 RID: 27513 RVA: 0x001737C0 File Offset: 0x001719C0
			public void IncrementReferenceCount()
			{
				this.referenceCount++;
			}

			// Token: 0x06006B7A RID: 27514 RVA: 0x001737D0 File Offset: 0x001719D0
			[SecurityCritical]
			public int DecrementReferenceCount()
			{
				int num = this.referenceCount - 1;
				this.referenceCount = num;
				int num2 = num;
				if (num2 == 0)
				{
					this.Dispose();
				}
				return num2;
			}

			// Token: 0x17001221 RID: 4641
			// (get) Token: 0x06006B7B RID: 27515 RVA: 0x001737F9 File Offset: 0x001719F9
			public int ReferenceCountValue
			{
				get
				{
					return this.referenceCount;
				}
			}

			// Token: 0x17001222 RID: 4642
			// (get) Token: 0x06006B7C RID: 27516 RVA: 0x00173801 File Offset: 0x00171A01
			public SafeAccessTokenHandle ThreadHandle
			{
				[SecurityCritical]
				get
				{
					return this.threadHandle;
				}
			}

			// Token: 0x17001223 RID: 4643
			// (get) Token: 0x06006B7D RID: 27517 RVA: 0x00173809 File Offset: 0x00171A09
			public bool IsImpersonating
			{
				get
				{
					return this.isImpersonating;
				}
			}

			// Token: 0x0400335D RID: 13149
			private bool disposed;

			// Token: 0x0400335E RID: 13150
			private int referenceCount = 1;

			// Token: 0x0400335F RID: 13151
			[SecurityCritical]
			private SafeAccessTokenHandle threadHandle = new SafeAccessTokenHandle(IntPtr.Zero);

			// Token: 0x04003360 RID: 13152
			private bool isImpersonating;

			// Token: 0x04003361 RID: 13153
			[SecurityCritical]
			private static volatile SafeAccessTokenHandle processHandle = new SafeAccessTokenHandle(IntPtr.Zero);

			// Token: 0x04003362 RID: 13154
			private static readonly object syncRoot = new object();
		}
	}
}
