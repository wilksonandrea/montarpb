using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020004FE RID: 1278
	[ComVisible(true)]
	public struct LockCookie
	{
		// Token: 0x06003C5A RID: 15450 RVA: 0x000E401E File Offset: 0x000E221E
		public override int GetHashCode()
		{
			return this._dwFlags + this._dwWriterSeqNum + this._wReaderAndWriterLevel + this._dwThreadID;
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x000E403B File Offset: 0x000E223B
		public override bool Equals(object obj)
		{
			return obj is LockCookie && this.Equals((LockCookie)obj);
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x000E4053 File Offset: 0x000E2253
		public bool Equals(LockCookie obj)
		{
			return obj._dwFlags == this._dwFlags && obj._dwWriterSeqNum == this._dwWriterSeqNum && obj._wReaderAndWriterLevel == this._wReaderAndWriterLevel && obj._dwThreadID == this._dwThreadID;
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x000E408F File Offset: 0x000E228F
		public static bool operator ==(LockCookie a, LockCookie b)
		{
			return a.Equals(b);
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x000E4099 File Offset: 0x000E2299
		public static bool operator !=(LockCookie a, LockCookie b)
		{
			return !(a == b);
		}

		// Token: 0x040019A0 RID: 6560
		private int _dwFlags;

		// Token: 0x040019A1 RID: 6561
		private int _dwWriterSeqNum;

		// Token: 0x040019A2 RID: 6562
		private int _wReaderAndWriterLevel;

		// Token: 0x040019A3 RID: 6563
		private int _dwThreadID;
	}
}
