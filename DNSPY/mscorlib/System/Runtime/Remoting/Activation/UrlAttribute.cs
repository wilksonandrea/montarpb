using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020008A1 RID: 2209
	[SecurityCritical]
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlAttribute : ContextAttribute
	{
		// Token: 0x06005D6C RID: 23916 RVA: 0x00148D9C File Offset: 0x00146F9C
		[SecurityCritical]
		public UrlAttribute(string callsiteURL)
			: base(UrlAttribute.propertyName)
		{
			if (callsiteURL == null)
			{
				throw new ArgumentNullException("callsiteURL");
			}
			this.url = callsiteURL;
		}

		// Token: 0x06005D6D RID: 23917 RVA: 0x00148DBE File Offset: 0x00146FBE
		[SecuritySafeCritical]
		public override bool Equals(object o)
		{
			return o is IContextProperty && o is UrlAttribute && ((UrlAttribute)o).UrlValue.Equals(this.url);
		}

		// Token: 0x06005D6E RID: 23918 RVA: 0x00148DE8 File Offset: 0x00146FE8
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			return this.url.GetHashCode();
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x00148DF5 File Offset: 0x00146FF5
		[SecurityCritical]
		[ComVisible(true)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return false;
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x00148DF8 File Offset: 0x00146FF8
		[SecurityCritical]
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06005D71 RID: 23921 RVA: 0x00148DFA File Offset: 0x00146FFA
		public string UrlValue
		{
			[SecurityCritical]
			get
			{
				return this.url;
			}
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x00148E02 File Offset: 0x00147002
		// Note: this type is marked as 'beforefieldinit'.
		static UrlAttribute()
		{
		}

		// Token: 0x04002A12 RID: 10770
		private string url;

		// Token: 0x04002A13 RID: 10771
		private static string propertyName = "UrlAttribute";
	}
}
