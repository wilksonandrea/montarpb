using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000854 RID: 2132
	internal class DictionaryEnumeratorByKeys : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06005A54 RID: 23124 RVA: 0x0013D805 File Offset: 0x0013BA05
		public DictionaryEnumeratorByKeys(IDictionary properties)
		{
			this._properties = properties;
			this._keyEnum = properties.Keys.GetEnumerator();
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x0013D825 File Offset: 0x0013BA25
		public bool MoveNext()
		{
			return this._keyEnum.MoveNext();
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x0013D832 File Offset: 0x0013BA32
		public void Reset()
		{
			this._keyEnum.Reset();
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06005A57 RID: 23127 RVA: 0x0013D83F File Offset: 0x0013BA3F
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06005A58 RID: 23128 RVA: 0x0013D84C File Offset: 0x0013BA4C
		public DictionaryEntry Entry
		{
			get
			{
				return new DictionaryEntry(this.Key, this.Value);
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06005A59 RID: 23129 RVA: 0x0013D85F File Offset: 0x0013BA5F
		public object Key
		{
			get
			{
				return this._keyEnum.Current;
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06005A5A RID: 23130 RVA: 0x0013D86C File Offset: 0x0013BA6C
		public object Value
		{
			get
			{
				return this._properties[this.Key];
			}
		}

		// Token: 0x0400290A RID: 10506
		private IDictionary _properties;

		// Token: 0x0400290B RID: 10507
		private IEnumerator _keyEnum;
	}
}
