using System;

namespace System.Resources
{
	// Token: 0x02000398 RID: 920
	internal struct ResourceLocator
	{
		// Token: 0x06002D47 RID: 11591 RVA: 0x000AB92F File Offset: 0x000A9B2F
		internal ResourceLocator(int dataPos, object value)
		{
			this._dataPos = dataPos;
			this._value = value;
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002D48 RID: 11592 RVA: 0x000AB93F File Offset: 0x000A9B3F
		internal int DataPosition
		{
			get
			{
				return this._dataPos;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x000AB947 File Offset: 0x000A9B47
		// (set) Token: 0x06002D4A RID: 11594 RVA: 0x000AB94F File Offset: 0x000A9B4F
		internal object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000AB958 File Offset: 0x000A9B58
		internal static bool CanCache(ResourceTypeCode value)
		{
			return value <= ResourceTypeCode.TimeSpan;
		}

		// Token: 0x04001258 RID: 4696
		internal object _value;

		// Token: 0x04001259 RID: 4697
		internal int _dataPos;
	}
}
