using System;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200095B RID: 2395
	internal sealed class SafeHeapHandleCache : IDisposable
	{
		// Token: 0x060061F0 RID: 25072 RVA: 0x0014EC77 File Offset: 0x0014CE77
		[SecuritySafeCritical]
		public SafeHeapHandleCache(ulong minSize = 64UL, ulong maxSize = 2048UL, int maxHandles = 0)
		{
			this._minSize = minSize;
			this._maxSize = maxSize;
			this._handleCache = new SafeHeapHandle[(maxHandles > 0) ? maxHandles : (Environment.ProcessorCount * 4)];
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x0014ECA8 File Offset: 0x0014CEA8
		[SecurityCritical]
		public SafeHeapHandle Acquire(ulong minSize = 0UL)
		{
			if (minSize < this._minSize)
			{
				minSize = this._minSize;
			}
			SafeHeapHandle safeHeapHandle = null;
			for (int i = 0; i < this._handleCache.Length; i++)
			{
				safeHeapHandle = Interlocked.Exchange<SafeHeapHandle>(ref this._handleCache[i], null);
				if (safeHeapHandle != null)
				{
					break;
				}
			}
			if (safeHeapHandle != null)
			{
				if (safeHeapHandle.ByteLength < minSize)
				{
					safeHeapHandle.Resize(minSize);
				}
			}
			else
			{
				safeHeapHandle = new SafeHeapHandle(minSize);
			}
			return safeHeapHandle;
		}

		// Token: 0x060061F2 RID: 25074 RVA: 0x0014ED10 File Offset: 0x0014CF10
		[SecurityCritical]
		public void Release(SafeHeapHandle handle)
		{
			if (handle.ByteLength <= this._maxSize)
			{
				for (int i = 0; i < this._handleCache.Length; i++)
				{
					handle = Interlocked.Exchange<SafeHeapHandle>(ref this._handleCache[i], handle);
					if (handle == null)
					{
						return;
					}
				}
			}
			handle.Dispose();
		}

		// Token: 0x060061F3 RID: 25075 RVA: 0x0014ED5C File Offset: 0x0014CF5C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060061F4 RID: 25076 RVA: 0x0014ED6C File Offset: 0x0014CF6C
		[SecuritySafeCritical]
		private void Dispose(bool disposing)
		{
			if (this._handleCache != null)
			{
				for (int i = 0; i < this._handleCache.Length; i++)
				{
					SafeHeapHandle safeHeapHandle = this._handleCache[i];
					this._handleCache[i] = null;
					if (safeHeapHandle != null && disposing)
					{
						safeHeapHandle.Dispose();
					}
				}
			}
		}

		// Token: 0x060061F5 RID: 25077 RVA: 0x0014EDB4 File Offset: 0x0014CFB4
		~SafeHeapHandleCache()
		{
			this.Dispose(false);
		}

		// Token: 0x04002B8B RID: 11147
		private readonly ulong _minSize;

		// Token: 0x04002B8C RID: 11148
		private readonly ulong _maxSize;

		// Token: 0x04002B8D RID: 11149
		[SecurityCritical]
		internal readonly SafeHeapHandle[] _handleCache;
	}
}
