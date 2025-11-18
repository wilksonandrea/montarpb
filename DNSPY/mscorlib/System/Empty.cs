using System;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020000D9 RID: 217
	[Serializable]
	internal sealed class Empty : ISerializable
	{
		// Token: 0x06000DE7 RID: 3559 RVA: 0x0002A9D7 File Offset: 0x00028BD7
		private Empty()
		{
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0002A9DF File Offset: 0x00028BDF
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0002A9E6 File Offset: 0x00028BE6
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 1, null, null);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0002A9FF File Offset: 0x00028BFF
		// Note: this type is marked as 'beforefieldinit'.
		static Empty()
		{
		}

		// Token: 0x04000568 RID: 1384
		public static readonly Empty Value = new Empty();
	}
}
