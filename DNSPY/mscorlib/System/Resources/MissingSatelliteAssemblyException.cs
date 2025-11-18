using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Resources
{
	// Token: 0x02000392 RID: 914
	[ComVisible(true)]
	[Serializable]
	public class MissingSatelliteAssemblyException : SystemException
	{
		// Token: 0x06002D0B RID: 11531 RVA: 0x000A9FEB File Offset: 0x000A81EB
		public MissingSatelliteAssemblyException()
			: base(Environment.GetResourceString("MissingSatelliteAssembly_Default"))
		{
			base.SetErrorCode(-2146233034);
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000AA008 File Offset: 0x000A8208
		public MissingSatelliteAssemblyException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233034);
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000AA01C File Offset: 0x000A821C
		public MissingSatelliteAssemblyException(string message, string cultureName)
			: base(message)
		{
			base.SetErrorCode(-2146233034);
			this._cultureName = cultureName;
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000AA037 File Offset: 0x000A8237
		public MissingSatelliteAssemblyException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233034);
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000AA04C File Offset: 0x000A824C
		protected MissingSatelliteAssemblyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002D10 RID: 11536 RVA: 0x000AA056 File Offset: 0x000A8256
		public string CultureName
		{
			get
			{
				return this._cultureName;
			}
		}

		// Token: 0x0400122E RID: 4654
		private string _cultureName;
	}
}
