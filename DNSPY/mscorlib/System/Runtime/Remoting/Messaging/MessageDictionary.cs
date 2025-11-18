using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000867 RID: 2151
	internal abstract class MessageDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06005B16 RID: 23318 RVA: 0x0013F687 File Offset: 0x0013D887
		internal MessageDictionary(string[] keys, IDictionary idict)
		{
			this._keys = keys;
			this._dict = idict;
		}

		// Token: 0x06005B17 RID: 23319 RVA: 0x0013F69D File Offset: 0x0013D89D
		internal bool HasUserData()
		{
			return this._dict != null && this._dict.Count > 0;
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06005B18 RID: 23320 RVA: 0x0013F6B8 File Offset: 0x0013D8B8
		internal IDictionary InternalDictionary
		{
			get
			{
				return this._dict;
			}
		}

		// Token: 0x06005B19 RID: 23321
		internal abstract object GetMessageValue(int i);

		// Token: 0x06005B1A RID: 23322
		[SecurityCritical]
		internal abstract void SetSpecialKey(int keyNum, object value);

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06005B1B RID: 23323 RVA: 0x0013F6C0 File Offset: 0x0013D8C0
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06005B1C RID: 23324 RVA: 0x0013F6C3 File Offset: 0x0013D8C3
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06005B1D RID: 23325 RVA: 0x0013F6C6 File Offset: 0x0013D8C6
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06005B1E RID: 23326 RVA: 0x0013F6C9 File Offset: 0x0013D8C9
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06005B1F RID: 23327 RVA: 0x0013F6CC File Offset: 0x0013D8CC
		public virtual bool Contains(object key)
		{
			return this.ContainsSpecialKey(key) || (this._dict != null && this._dict.Contains(key));
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x0013F6F0 File Offset: 0x0013D8F0
		protected virtual bool ContainsSpecialKey(object key)
		{
			if (!(key is string))
			{
				return false;
			}
			string text = (string)key;
			for (int i = 0; i < this._keys.Length; i++)
			{
				if (text.Equals(this._keys[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005B21 RID: 23329 RVA: 0x0013F734 File Offset: 0x0013D934
		public virtual void CopyTo(Array array, int index)
		{
			for (int i = 0; i < this._keys.Length; i++)
			{
				array.SetValue(this.GetMessageValue(i), index + i);
			}
			if (this._dict != null)
			{
				this._dict.CopyTo(array, index + this._keys.Length);
			}
		}

		// Token: 0x17000F5C RID: 3932
		public virtual object this[object key]
		{
			get
			{
				string text = key as string;
				if (text != null)
				{
					for (int i = 0; i < this._keys.Length; i++)
					{
						if (text.Equals(this._keys[i]))
						{
							return this.GetMessageValue(i);
						}
					}
					if (this._dict != null)
					{
						return this._dict[key];
					}
				}
				return null;
			}
			[SecuritySafeCritical]
			set
			{
				if (!this.ContainsSpecialKey(key))
				{
					if (this._dict == null)
					{
						this._dict = new Hashtable();
					}
					this._dict[key] = value;
					return;
				}
				if (key.Equals(Message.UriKey))
				{
					this.SetSpecialKey(0, value);
					return;
				}
				if (key.Equals(Message.CallContextKey))
				{
					this.SetSpecialKey(1, value);
					return;
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKey"));
			}
		}

		// Token: 0x06005B24 RID: 23332 RVA: 0x0013F84E File Offset: 0x0013DA4E
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new MessageDictionaryEnumerator(this, this._dict);
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x0013F85C File Offset: 0x0013DA5C
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005B26 RID: 23334 RVA: 0x0013F863 File Offset: 0x0013DA63
		public virtual void Add(object key, object value)
		{
			if (this.ContainsSpecialKey(key))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKey"));
			}
			if (this._dict == null)
			{
				this._dict = new Hashtable();
			}
			this._dict.Add(key, value);
		}

		// Token: 0x06005B27 RID: 23335 RVA: 0x0013F89E File Offset: 0x0013DA9E
		public virtual void Clear()
		{
			if (this._dict != null)
			{
				this._dict.Clear();
			}
		}

		// Token: 0x06005B28 RID: 23336 RVA: 0x0013F8B3 File Offset: 0x0013DAB3
		public virtual void Remove(object key)
		{
			if (this.ContainsSpecialKey(key) || this._dict == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKey"));
			}
			this._dict.Remove(key);
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06005B29 RID: 23337 RVA: 0x0013F8E4 File Offset: 0x0013DAE4
		public virtual ICollection Keys
		{
			get
			{
				int num = this._keys.Length;
				ICollection collection = ((this._dict != null) ? this._dict.Keys : null);
				if (collection != null)
				{
					num += collection.Count;
				}
				ArrayList arrayList = new ArrayList(num);
				for (int i = 0; i < this._keys.Length; i++)
				{
					arrayList.Add(this._keys[i]);
				}
				if (collection != null)
				{
					arrayList.AddRange(collection);
				}
				return arrayList;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06005B2A RID: 23338 RVA: 0x0013F954 File Offset: 0x0013DB54
		public virtual ICollection Values
		{
			get
			{
				int num = this._keys.Length;
				ICollection collection = ((this._dict != null) ? this._dict.Keys : null);
				if (collection != null)
				{
					num += collection.Count;
				}
				ArrayList arrayList = new ArrayList(num);
				for (int i = 0; i < this._keys.Length; i++)
				{
					arrayList.Add(this.GetMessageValue(i));
				}
				if (collection != null)
				{
					arrayList.AddRange(collection);
				}
				return arrayList;
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06005B2B RID: 23339 RVA: 0x0013F9C0 File Offset: 0x0013DBC0
		public virtual int Count
		{
			get
			{
				if (this._dict != null)
				{
					return this._dict.Count + this._keys.Length;
				}
				return this._keys.Length;
			}
		}

		// Token: 0x0400294C RID: 10572
		internal string[] _keys;

		// Token: 0x0400294D RID: 10573
		internal IDictionary _dict;
	}
}
