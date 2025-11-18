using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000853 RID: 2131
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public abstract class BaseChannelObjectWithProperties : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06005A42 RID: 23106 RVA: 0x0013D691 File Offset: 0x0013B891
		protected BaseChannelObjectWithProperties()
		{
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06005A43 RID: 23107 RVA: 0x0013D699 File Offset: 0x0013B899
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				return this;
			}
		}

		// Token: 0x17000F06 RID: 3846
		public virtual object this[object key]
		{
			[SecuritySafeCritical]
			get
			{
				return null;
			}
			[SecuritySafeCritical]
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06005A46 RID: 23110 RVA: 0x0013D6A6 File Offset: 0x0013B8A6
		public virtual ICollection Keys
		{
			[SecuritySafeCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06005A47 RID: 23111 RVA: 0x0013D6AC File Offset: 0x0013B8AC
		public virtual ICollection Values
		{
			[SecuritySafeCritical]
			get
			{
				ICollection keys = this.Keys;
				if (keys == null)
				{
					return null;
				}
				ArrayList arrayList = new ArrayList();
				foreach (object obj in keys)
				{
					arrayList.Add(this[obj]);
				}
				return arrayList;
			}
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x0013D718 File Offset: 0x0013B918
		[SecuritySafeCritical]
		public virtual bool Contains(object key)
		{
			if (key == null)
			{
				return false;
			}
			ICollection keys = this.Keys;
			if (keys == null)
			{
				return false;
			}
			string text = key as string;
			foreach (object obj in keys)
			{
				if (text != null)
				{
					string text2 = obj as string;
					if (text2 != null)
					{
						if (string.Compare(text, text2, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return true;
						}
						continue;
					}
				}
				if (key.Equals(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06005A49 RID: 23113 RVA: 0x0013D7AC File Offset: 0x0013B9AC
		public virtual bool IsReadOnly
		{
			[SecuritySafeCritical]
			get
			{
				return false;
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06005A4A RID: 23114 RVA: 0x0013D7AF File Offset: 0x0013B9AF
		public virtual bool IsFixedSize
		{
			[SecuritySafeCritical]
			get
			{
				return true;
			}
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x0013D7B2 File Offset: 0x0013B9B2
		[SecuritySafeCritical]
		public virtual void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x0013D7B9 File Offset: 0x0013B9B9
		[SecuritySafeCritical]
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005A4D RID: 23117 RVA: 0x0013D7C0 File Offset: 0x0013B9C0
		[SecuritySafeCritical]
		public virtual void Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005A4E RID: 23118 RVA: 0x0013D7C7 File Offset: 0x0013B9C7
		[SecuritySafeCritical]
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}

		// Token: 0x06005A4F RID: 23119 RVA: 0x0013D7CF File Offset: 0x0013B9CF
		[SecuritySafeCritical]
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06005A50 RID: 23120 RVA: 0x0013D7D8 File Offset: 0x0013B9D8
		public virtual int Count
		{
			[SecuritySafeCritical]
			get
			{
				ICollection keys = this.Keys;
				if (keys == null)
				{
					return 0;
				}
				return keys.Count;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06005A51 RID: 23121 RVA: 0x0013D7F7 File Offset: 0x0013B9F7
		public virtual object SyncRoot
		{
			[SecuritySafeCritical]
			get
			{
				return this;
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06005A52 RID: 23122 RVA: 0x0013D7FA File Offset: 0x0013B9FA
		public virtual bool IsSynchronized
		{
			[SecuritySafeCritical]
			get
			{
				return false;
			}
		}

		// Token: 0x06005A53 RID: 23123 RVA: 0x0013D7FD File Offset: 0x0013B9FD
		[SecuritySafeCritical]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}
	}
}
