using System;

namespace System.Globalization
{
	// Token: 0x020003E0 RID: 992
	[Serializable]
	public sealed class SortVersion : IEquatable<SortVersion>
	{
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060032E8 RID: 13032 RVA: 0x000C46D6 File Offset: 0x000C28D6
		public int FullVersion
		{
			get
			{
				return this.m_NlsVersion;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060032E9 RID: 13033 RVA: 0x000C46DE File Offset: 0x000C28DE
		public Guid SortId
		{
			get
			{
				return this.m_SortId;
			}
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000C46E6 File Offset: 0x000C28E6
		public SortVersion(int fullVersion, Guid sortId)
		{
			this.m_SortId = sortId;
			this.m_NlsVersion = fullVersion;
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000C46FC File Offset: 0x000C28FC
		internal SortVersion(int nlsVersion, int effectiveId, Guid customVersion)
		{
			this.m_NlsVersion = nlsVersion;
			if (customVersion == Guid.Empty)
			{
				byte[] bytes = BitConverter.GetBytes(effectiveId);
				byte b = (byte)((uint)effectiveId >> 24);
				byte b2 = (byte)((effectiveId & 16711680) >> 16);
				byte b3 = (byte)((effectiveId & 65280) >> 8);
				byte b4 = (byte)(effectiveId & 255);
				customVersion = new Guid(0, 0, 0, 0, 0, 0, 0, b, b2, b3, b4);
			}
			this.m_SortId = customVersion;
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000C476C File Offset: 0x000C296C
		public override bool Equals(object obj)
		{
			SortVersion sortVersion = obj as SortVersion;
			return sortVersion != null && this.Equals(sortVersion);
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000C4792 File Offset: 0x000C2992
		public bool Equals(SortVersion other)
		{
			return !(other == null) && this.m_NlsVersion == other.m_NlsVersion && this.m_SortId == other.m_SortId;
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x000C47C0 File Offset: 0x000C29C0
		public override int GetHashCode()
		{
			return (this.m_NlsVersion * 7) | this.m_SortId.GetHashCode();
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000C47DC File Offset: 0x000C29DC
		public static bool operator ==(SortVersion left, SortVersion right)
		{
			if (left != null)
			{
				return left.Equals(right);
			}
			return right == null || right.Equals(left);
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000C47F5 File Offset: 0x000C29F5
		public static bool operator !=(SortVersion left, SortVersion right)
		{
			return !(left == right);
		}

		// Token: 0x04001697 RID: 5783
		private int m_NlsVersion;

		// Token: 0x04001698 RID: 5784
		private Guid m_SortId;
	}
}
