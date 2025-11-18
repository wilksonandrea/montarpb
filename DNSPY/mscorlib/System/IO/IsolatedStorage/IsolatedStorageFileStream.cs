using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.IsolatedStorage
{
	// Token: 0x020001B0 RID: 432
	[ComVisible(true)]
	public class IsolatedStorageFileStream : FileStream
	{
		// Token: 0x06001B2B RID: 6955 RVA: 0x0005BDDC File Offset: 0x00059FDC
		private IsolatedStorageFileStream()
		{
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0005BDE4 File Offset: 0x00059FE4
		public IsolatedStorageFileStream(string path, FileMode mode)
			: this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None, null)
		{
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0005BDF8 File Offset: 0x00059FF8
		public IsolatedStorageFileStream(string path, FileMode mode, IsolatedStorageFile isf)
			: this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None, isf)
		{
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0005BE0C File Offset: 0x0005A00C
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access)
			: this(path, mode, access, (access == FileAccess.Read) ? FileShare.Read : FileShare.None, 4096, null)
		{
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0005BE25 File Offset: 0x0005A025
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, IsolatedStorageFile isf)
			: this(path, mode, access, (access == FileAccess.Read) ? FileShare.Read : FileShare.None, 4096, isf)
		{
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0005BE3F File Offset: 0x0005A03F
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share)
			: this(path, mode, access, share, 4096, null)
		{
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0005BE52 File Offset: 0x0005A052
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, IsolatedStorageFile isf)
			: this(path, mode, access, share, 4096, isf)
		{
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0005BE66 File Offset: 0x0005A066
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
			: this(path, mode, access, share, bufferSize, null)
		{
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0005BE78 File Offset: 0x0005A078
		[SecuritySafeCritical]
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, IsolatedStorageFile isf)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0 || path.Equals("\\"))
			{
				throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_Path"));
			}
			if (isf == null)
			{
				this.m_OwnedStore = true;
				isf = IsolatedStorageFile.GetUserStoreForDomain();
			}
			if (isf.Disposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (mode - FileMode.CreateNew > 5)
			{
				throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_FileOpenMode"));
			}
			this.m_isf = isf;
			FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, this.m_isf.RootDirectory);
			fileIOPermission.Assert();
			fileIOPermission.PermitOnly();
			this.m_GivenPath = path;
			this.m_FullPath = this.m_isf.GetFullPath(this.m_GivenPath);
			ulong num = 0UL;
			bool flag = false;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				switch (mode)
				{
				case FileMode.CreateNew:
					flag = true;
					break;
				case FileMode.Create:
				case FileMode.OpenOrCreate:
				case FileMode.Truncate:
				case FileMode.Append:
					this.m_isf.Lock(ref flag2);
					try
					{
						num = IsolatedStorageFile.RoundToBlockSize((ulong)LongPathFile.GetLength(this.m_FullPath));
					}
					catch (FileNotFoundException)
					{
						flag = true;
					}
					catch
					{
					}
					break;
				}
				if (flag)
				{
					this.m_isf.ReserveOneBlock();
				}
				try
				{
					this.m_fs = new FileStream(this.m_FullPath, mode, access, share, bufferSize, FileOptions.None, this.m_GivenPath, true, true);
				}
				catch
				{
					if (flag)
					{
						this.m_isf.UnreserveOneBlock();
					}
					throw;
				}
				if (!flag && (mode == FileMode.Truncate || mode == FileMode.Create))
				{
					ulong num2 = IsolatedStorageFile.RoundToBlockSize((ulong)this.m_fs.Length);
					if (num > num2)
					{
						this.m_isf.Unreserve(num - num2);
					}
					else if (num2 > num)
					{
						this.m_isf.Reserve(num2 - num);
					}
				}
			}
			finally
			{
				if (flag2)
				{
					this.m_isf.Unlock();
				}
			}
			CodeAccessPermission.RevertAll();
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0005C070 File Offset: 0x0005A270
		public override bool CanRead
		{
			get
			{
				return this.m_fs.CanRead;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0005C07D File Offset: 0x0005A27D
		public override bool CanWrite
		{
			get
			{
				return this.m_fs.CanWrite;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0005C08A File Offset: 0x0005A28A
		public override bool CanSeek
		{
			get
			{
				return this.m_fs.CanSeek;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x0005C097 File Offset: 0x0005A297
		public override bool IsAsync
		{
			get
			{
				return this.m_fs.IsAsync;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x0005C0A4 File Offset: 0x0005A2A4
		public override long Length
		{
			get
			{
				return this.m_fs.Length;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x0005C0B1 File Offset: 0x0005A2B1
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x0005C0BE File Offset: 0x0005A2BE
		public override long Position
		{
			get
			{
				return this.m_fs.Position;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0005C0E4 File Offset: 0x0005A2E4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					try
					{
						if (this.m_fs != null)
						{
							this.m_fs.Close();
						}
					}
					finally
					{
						if (this.m_OwnedStore && this.m_isf != null)
						{
							this.m_isf.Close();
						}
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0005C14C File Offset: 0x0005A34C
		public override void Flush()
		{
			this.m_fs.Flush();
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0005C159 File Offset: 0x0005A359
		public override void Flush(bool flushToDisk)
		{
			this.m_fs.Flush(flushToDisk);
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x0005C167 File Offset: 0x0005A367
		[Obsolete("This property has been deprecated.  Please use IsolatedStorageFileStream's SafeFileHandle property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public override IntPtr Handle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				this.NotPermittedError();
				return Win32Native.INVALID_HANDLE_VALUE;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x0005C174 File Offset: 0x0005A374
		public override SafeFileHandle SafeFileHandle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				this.NotPermittedError();
				return null;
			}
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x0005C180 File Offset: 0x0005A380
		[SecuritySafeCritical]
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				this.m_isf.Reserve(length, (ulong)value);
				try
				{
					this.ZeroInit(length, (ulong)value);
					this.m_fs.SetLength(value);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, (ulong)value);
					throw;
				}
				if (length > (ulong)value)
				{
					this.m_isf.UndoReserveOperation((ulong)value, length);
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0005C23C File Offset: 0x0005A43C
		public override void Lock(long position, long length)
		{
			if (position < 0L || length < 0L)
			{
				throw new ArgumentOutOfRangeException((position < 0L) ? "position" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_fs.Lock(position, length);
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0005C276 File Offset: 0x0005A476
		public override void Unlock(long position, long length)
		{
			if (position < 0L || length < 0L)
			{
				throw new ArgumentOutOfRangeException((position < 0L) ? "position" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_fs.Unlock(position, length);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0005C2B0 File Offset: 0x0005A4B0
		private void ZeroInit(ulong oldLen, ulong newLen)
		{
			if (oldLen >= newLen)
			{
				return;
			}
			ulong num = newLen - oldLen;
			byte[] array = new byte[1024];
			long position = this.m_fs.Position;
			this.m_fs.Seek((long)oldLen, SeekOrigin.Begin);
			if (num <= 1024UL)
			{
				this.m_fs.Write(array, 0, (int)num);
				this.m_fs.Position = position;
				return;
			}
			int num2 = 1024 - (int)(oldLen & 1023UL);
			this.m_fs.Write(array, 0, num2);
			num -= (ulong)((long)num2);
			int num3 = (int)(num / 1024UL);
			for (int i = 0; i < num3; i++)
			{
				this.m_fs.Write(array, 0, 1024);
			}
			this.m_fs.Write(array, 0, (int)(num & 1023UL));
			this.m_fs.Position = position;
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0005C383 File Offset: 0x0005A583
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_fs.Read(buffer, offset, count);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0005C393 File Offset: 0x0005A593
		public override int ReadByte()
		{
			return this.m_fs.ReadByte();
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0005C3A0 File Offset: 0x0005A5A0
		[SecuritySafeCritical]
		public override long Seek(long offset, SeekOrigin origin)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			long num2;
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				ulong num;
				switch (origin)
				{
				case SeekOrigin.Begin:
					num = (ulong)((offset < 0L) ? 0L : offset);
					break;
				case SeekOrigin.Current:
					num = (ulong)((this.m_fs.Position + offset < 0L) ? 0L : (this.m_fs.Position + offset));
					break;
				case SeekOrigin.End:
					num = (ulong)((this.m_fs.Length + offset < 0L) ? 0L : (this.m_fs.Length + offset));
					break;
				default:
					throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_SeekOrigin"));
				}
				this.m_isf.Reserve(length, num);
				try
				{
					this.ZeroInit(length, num);
					num2 = this.m_fs.Seek(offset, origin);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, num);
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
			return num2;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0005C4AC File Offset: 0x0005A6AC
		[SecuritySafeCritical]
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				ulong num = (ulong)(this.m_fs.Position + (long)count);
				this.m_isf.Reserve(length, num);
				try
				{
					this.m_fs.Write(buffer, offset, count);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, num);
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0005C540 File Offset: 0x0005A740
		[SecuritySafeCritical]
		public override void WriteByte(byte value)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				ulong num = (ulong)(this.m_fs.Position + 1L);
				this.m_isf.Reserve(length, num);
				try
				{
					this.m_fs.WriteByte(value);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, num);
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0005C5D4 File Offset: 0x0005A7D4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			return this.m_fs.BeginRead(buffer, offset, numBytes, userCallback, stateObject);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0005C5E8 File Offset: 0x0005A7E8
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return this.m_fs.EndRead(asyncResult);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0005C604 File Offset: 0x0005A804
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			IAsyncResult asyncResult;
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				ulong num = (ulong)(this.m_fs.Position + (long)numBytes);
				this.m_isf.Reserve(length, num);
				try
				{
					asyncResult = this.m_fs.BeginWrite(buffer, offset, numBytes, userCallback, stateObject);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, num);
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
			return asyncResult;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0005C6A0 File Offset: 0x0005A8A0
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			this.m_fs.EndWrite(asyncResult);
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0005C6BC File Offset: 0x0005A8BC
		internal void NotPermittedError(string str)
		{
			throw new IsolatedStorageException(str);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0005C6C4 File Offset: 0x0005A8C4
		internal void NotPermittedError()
		{
			this.NotPermittedError(Environment.GetResourceString("IsolatedStorage_Operation_ISFS"));
		}

		// Token: 0x0400096E RID: 2414
		private const int s_BlockSize = 1024;

		// Token: 0x0400096F RID: 2415
		private const string s_BackSlash = "\\";

		// Token: 0x04000970 RID: 2416
		private FileStream m_fs;

		// Token: 0x04000971 RID: 2417
		private IsolatedStorageFile m_isf;

		// Token: 0x04000972 RID: 2418
		private string m_GivenPath;

		// Token: 0x04000973 RID: 2419
		private string m_FullPath;

		// Token: 0x04000974 RID: 2420
		private bool m_OwnedStore;
	}
}
