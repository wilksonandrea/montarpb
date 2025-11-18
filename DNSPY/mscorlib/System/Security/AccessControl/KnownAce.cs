using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000201 RID: 513
	public abstract class KnownAce : GenericAce
	{
		// Token: 0x06001E57 RID: 7767 RVA: 0x00069E70 File Offset: 0x00068070
		internal KnownAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier securityIdentifier)
			: base(type, flags)
		{
			if (securityIdentifier == null)
			{
				throw new ArgumentNullException("securityIdentifier");
			}
			this.AccessMask = accessMask;
			this.SecurityIdentifier = securityIdentifier;
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x00069E9E File Offset: 0x0006809E
		// (set) Token: 0x06001E59 RID: 7769 RVA: 0x00069EA6 File Offset: 0x000680A6
		public int AccessMask
		{
			get
			{
				return this._accessMask;
			}
			set
			{
				this._accessMask = value;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x00069EAF File Offset: 0x000680AF
		// (set) Token: 0x06001E5B RID: 7771 RVA: 0x00069EB7 File Offset: 0x000680B7
		public SecurityIdentifier SecurityIdentifier
		{
			get
			{
				return this._sid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._sid = value;
			}
		}

		// Token: 0x04000AE9 RID: 2793
		private int _accessMask;

		// Token: 0x04000AEA RID: 2794
		private SecurityIdentifier _sid;

		// Token: 0x04000AEB RID: 2795
		internal const int AccessMaskLength = 4;
	}
}
