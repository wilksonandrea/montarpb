using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007DD RID: 2013
	[ComVisible(true)]
	public class SoapAttribute : Attribute
	{
		// Token: 0x06005718 RID: 22296 RVA: 0x00134B03 File Offset: 0x00132D03
		internal void SetReflectInfo(object info)
		{
			this.ReflectInfo = info;
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06005719 RID: 22297 RVA: 0x00134B0C File Offset: 0x00132D0C
		// (set) Token: 0x0600571A RID: 22298 RVA: 0x00134B14 File Offset: 0x00132D14
		public virtual string XmlNamespace
		{
			get
			{
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x0600571B RID: 22299 RVA: 0x00134B1D File Offset: 0x00132D1D
		// (set) Token: 0x0600571C RID: 22300 RVA: 0x00134B25 File Offset: 0x00132D25
		public virtual bool UseAttribute
		{
			get
			{
				return this._bUseAttribute;
			}
			set
			{
				this._bUseAttribute = value;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x0600571D RID: 22301 RVA: 0x00134B2E File Offset: 0x00132D2E
		// (set) Token: 0x0600571E RID: 22302 RVA: 0x00134B36 File Offset: 0x00132D36
		public virtual bool Embedded
		{
			get
			{
				return this._bEmbedded;
			}
			set
			{
				this._bEmbedded = value;
			}
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x00134B3F File Offset: 0x00132D3F
		public SoapAttribute()
		{
		}

		// Token: 0x040027EB RID: 10219
		protected string ProtXmlNamespace;

		// Token: 0x040027EC RID: 10220
		private bool _bUseAttribute;

		// Token: 0x040027ED RID: 10221
		private bool _bEmbedded;

		// Token: 0x040027EE RID: 10222
		protected object ReflectInfo;
	}
}
