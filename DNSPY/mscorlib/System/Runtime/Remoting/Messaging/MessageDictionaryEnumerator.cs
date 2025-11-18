using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000868 RID: 2152
	internal class MessageDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06005B2C RID: 23340 RVA: 0x0013F9E7 File Offset: 0x0013DBE7
		public MessageDictionaryEnumerator(MessageDictionary md, IDictionary hashtable)
		{
			this._md = md;
			if (hashtable != null)
			{
				this._enumHash = hashtable.GetEnumerator();
				return;
			}
			this._enumHash = null;
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06005B2D RID: 23341 RVA: 0x0013FA14 File Offset: 0x0013DC14
		public object Key
		{
			get
			{
				if (this.i < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
				}
				if (this.i < this._md._keys.Length)
				{
					return this._md._keys[this.i];
				}
				return this._enumHash.Key;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06005B2E RID: 23342 RVA: 0x0013FA70 File Offset: 0x0013DC70
		public object Value
		{
			get
			{
				if (this.i < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
				}
				if (this.i < this._md._keys.Length)
				{
					return this._md.GetMessageValue(this.i);
				}
				return this._enumHash.Value;
			}
		}

		// Token: 0x06005B2F RID: 23343 RVA: 0x0013FAC8 File Offset: 0x0013DCC8
		public bool MoveNext()
		{
			if (this.i == -2)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
			this.i++;
			if (this.i < this._md._keys.Length)
			{
				return true;
			}
			if (this._enumHash != null && this._enumHash.MoveNext())
			{
				return true;
			}
			this.i = -2;
			return false;
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06005B30 RID: 23344 RVA: 0x0013FB34 File Offset: 0x0013DD34
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06005B31 RID: 23345 RVA: 0x0013FB41 File Offset: 0x0013DD41
		public DictionaryEntry Entry
		{
			get
			{
				return new DictionaryEntry(this.Key, this.Value);
			}
		}

		// Token: 0x06005B32 RID: 23346 RVA: 0x0013FB54 File Offset: 0x0013DD54
		public void Reset()
		{
			this.i = -1;
			if (this._enumHash != null)
			{
				this._enumHash.Reset();
			}
		}

		// Token: 0x0400294E RID: 10574
		private int i = -1;

		// Token: 0x0400294F RID: 10575
		private IDictionaryEnumerator _enumHash;

		// Token: 0x04002950 RID: 10576
		private MessageDictionary _md;
	}
}
