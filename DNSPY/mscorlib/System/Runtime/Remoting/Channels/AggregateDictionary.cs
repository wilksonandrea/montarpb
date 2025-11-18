using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000855 RID: 2133
	internal class AggregateDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06005A5B RID: 23131 RVA: 0x0013D87F File Offset: 0x0013BA7F
		public AggregateDictionary(ICollection dictionaries)
		{
			this._dictionaries = dictionaries;
		}

		// Token: 0x17000F12 RID: 3858
		public virtual object this[object key]
		{
			get
			{
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					if (dictionary.Contains(key))
					{
						return dictionary[key];
					}
				}
				return null;
			}
			set
			{
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					if (dictionary.Contains(key))
					{
						dictionary[key] = value;
					}
				}
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06005A5E RID: 23134 RVA: 0x0013D95C File Offset: 0x0013BB5C
		public virtual ICollection Keys
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					ICollection keys = dictionary.Keys;
					if (keys != null)
					{
						foreach (object obj2 in keys)
						{
							arrayList.Add(obj2);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06005A5F RID: 23135 RVA: 0x0013DA0C File Offset: 0x0013BC0C
		public virtual ICollection Values
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					ICollection values = dictionary.Values;
					if (values != null)
					{
						foreach (object obj2 in values)
						{
							arrayList.Add(obj2);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x06005A60 RID: 23136 RVA: 0x0013DABC File Offset: 0x0013BCBC
		public virtual bool Contains(object key)
		{
			foreach (object obj in this._dictionaries)
			{
				IDictionary dictionary = (IDictionary)obj;
				if (dictionary.Contains(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06005A61 RID: 23137 RVA: 0x0013DB20 File Offset: 0x0013BD20
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06005A62 RID: 23138 RVA: 0x0013DB23 File Offset: 0x0013BD23
		public virtual bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005A63 RID: 23139 RVA: 0x0013DB26 File Offset: 0x0013BD26
		public virtual void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005A64 RID: 23140 RVA: 0x0013DB2D File Offset: 0x0013BD2D
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005A65 RID: 23141 RVA: 0x0013DB34 File Offset: 0x0013BD34
		public virtual void Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005A66 RID: 23142 RVA: 0x0013DB3B File Offset: 0x0013BD3B
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x0013DB43 File Offset: 0x0013BD43
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06005A68 RID: 23144 RVA: 0x0013DB4C File Offset: 0x0013BD4C
		public virtual int Count
		{
			get
			{
				int num = 0;
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					num += dictionary.Count;
				}
				return num;
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06005A69 RID: 23145 RVA: 0x0013DBAC File Offset: 0x0013BDAC
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06005A6A RID: 23146 RVA: 0x0013DBAF File Offset: 0x0013BDAF
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005A6B RID: 23147 RVA: 0x0013DBB2 File Offset: 0x0013BDB2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}

		// Token: 0x0400290C RID: 10508
		private ICollection _dictionaries;
	}
}
