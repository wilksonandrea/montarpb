using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x02000601 RID: 1537
	internal class MetadataException : Exception
	{
		// Token: 0x060046D7 RID: 18135 RVA: 0x00102D7E File Offset: 0x00100F7E
		internal MetadataException(int hr)
		{
			this.m_hr = hr;
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x00102D8D File Offset: 0x00100F8D
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "MetadataException HResult = {0:x}.", this.m_hr);
		}

		// Token: 0x04001D5E RID: 7518
		private int m_hr;
	}
}
