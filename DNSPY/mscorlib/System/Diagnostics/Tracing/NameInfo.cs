using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044B RID: 1099
	internal sealed class NameInfo : ConcurrentSetItem<KeyValuePair<string, EventTags>, NameInfo>
	{
		// Token: 0x0600364E RID: 13902 RVA: 0x000D2C5C File Offset: 0x000D0E5C
		internal static void ReserveEventIDsBelow(int eventId)
		{
			int num;
			int num2;
			do
			{
				num = NameInfo.lastIdentity;
				num2 = (NameInfo.lastIdentity & -16777216) + eventId;
				num2 = Math.Max(num2, num);
			}
			while (Interlocked.CompareExchange(ref NameInfo.lastIdentity, num2, num) != num);
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000D2C94 File Offset: 0x000D0E94
		public NameInfo(string name, EventTags tags, int typeMetadataSize)
		{
			this.name = name;
			this.tags = tags & (EventTags)268435455;
			this.identity = Interlocked.Increment(ref NameInfo.lastIdentity);
			int num = 0;
			Statics.EncodeTags((int)this.tags, ref num, null);
			this.nameMetadata = Statics.MetadataForString(name, num, 0, typeMetadataSize);
			num = 2;
			Statics.EncodeTags((int)this.tags, ref num, this.nameMetadata);
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000D2CFF File Offset: 0x000D0EFF
		public override int Compare(NameInfo other)
		{
			return this.Compare(other.name, other.tags);
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x000D2D13 File Offset: 0x000D0F13
		public override int Compare(KeyValuePair<string, EventTags> key)
		{
			return this.Compare(key.Key, key.Value & (EventTags)268435455);
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000D2D30 File Offset: 0x000D0F30
		private int Compare(string otherName, EventTags otherTags)
		{
			int num = StringComparer.Ordinal.Compare(this.name, otherName);
			if (num == 0 && this.tags != otherTags)
			{
				num = ((this.tags < otherTags) ? (-1) : 1);
			}
			return num;
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000D2D6A File Offset: 0x000D0F6A
		// Note: this type is marked as 'beforefieldinit'.
		static NameInfo()
		{
		}

		// Token: 0x0400184C RID: 6220
		private static int lastIdentity = 184549376;

		// Token: 0x0400184D RID: 6221
		internal readonly string name;

		// Token: 0x0400184E RID: 6222
		internal readonly EventTags tags;

		// Token: 0x0400184F RID: 6223
		internal readonly int identity;

		// Token: 0x04001850 RID: 6224
		internal readonly byte[] nameMetadata;
	}
}
