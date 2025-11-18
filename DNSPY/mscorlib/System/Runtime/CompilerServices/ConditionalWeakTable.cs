using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008E7 RID: 2279
	[ComVisible(false)]
	[__DynamicallyInvokable]
	public sealed class ConditionalWeakTable<TKey, TValue> where TKey : class where TValue : class
	{
		// Token: 0x06005DEE RID: 24046 RVA: 0x0014990E File Offset: 0x00147B0E
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public ConditionalWeakTable()
		{
			this._buckets = new int[0];
			this._entries = new ConditionalWeakTable<TKey, TValue>.Entry[0];
			this._freeList = -1;
			this._lock = new object();
			this.Resize();
		}

		// Token: 0x06005DEF RID: 24047 RVA: 0x00149948 File Offset: 0x00147B48
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			object @lock = this._lock;
			bool flag2;
			lock (@lock)
			{
				this.VerifyIntegrity();
				flag2 = this.TryGetValueWorker(key, out value);
			}
			return flag2;
		}

		// Token: 0x06005DF0 RID: 24048 RVA: 0x001499A0 File Offset: 0x00147BA0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			object @lock = this._lock;
			lock (@lock)
			{
				this.VerifyIntegrity();
				this._invalid = true;
				int num = this.FindEntry(key);
				if (num != -1)
				{
					this._invalid = false;
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
				}
				this.CreateEntry(key, value);
				this._invalid = false;
			}
		}

		// Token: 0x06005DF1 RID: 24049 RVA: 0x00149A20 File Offset: 0x00147C20
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			object @lock = this._lock;
			bool flag2;
			lock (@lock)
			{
				this.VerifyIntegrity();
				this._invalid = true;
				int num = RuntimeHelpers.GetHashCode(key) & int.MaxValue;
				int num2 = num % this._buckets.Length;
				int num3 = -1;
				for (int num4 = this._buckets[num2]; num4 != -1; num4 = this._entries[num4].next)
				{
					if (this._entries[num4].hashCode == num && this._entries[num4].depHnd.GetPrimary() == key)
					{
						if (num3 == -1)
						{
							this._buckets[num2] = this._entries[num4].next;
						}
						else
						{
							this._entries[num3].next = this._entries[num4].next;
						}
						this._entries[num4].depHnd.Free();
						this._entries[num4].next = this._freeList;
						this._freeList = num4;
						this._invalid = false;
						return true;
					}
					num3 = num4;
				}
				this._invalid = false;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06005DF2 RID: 24050 RVA: 0x00149BA0 File Offset: 0x00147DA0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public TValue GetValue(TKey key, ConditionalWeakTable<TKey, TValue>.CreateValueCallback createValueCallback)
		{
			if (createValueCallback == null)
			{
				throw new ArgumentNullException("createValueCallback");
			}
			TValue tvalue;
			if (this.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			TValue tvalue2 = createValueCallback(key);
			object @lock = this._lock;
			TValue tvalue3;
			lock (@lock)
			{
				this.VerifyIntegrity();
				this._invalid = true;
				if (this.TryGetValueWorker(key, out tvalue))
				{
					this._invalid = false;
					tvalue3 = tvalue;
				}
				else
				{
					this.CreateEntry(key, tvalue2);
					this._invalid = false;
					tvalue3 = tvalue2;
				}
			}
			return tvalue3;
		}

		// Token: 0x06005DF3 RID: 24051 RVA: 0x00149C38 File Offset: 0x00147E38
		[__DynamicallyInvokable]
		public TValue GetOrCreateValue(TKey key)
		{
			return this.GetValue(key, (TKey k) => Activator.CreateInstance<TValue>());
		}

		// Token: 0x06005DF4 RID: 24052 RVA: 0x00149C60 File Offset: 0x00147E60
		[SecuritySafeCritical]
		[FriendAccessAllowed]
		internal TKey FindEquivalentKeyUnsafe(TKey key, out TValue value)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				for (int i = 0; i < this._buckets.Length; i++)
				{
					for (int num = this._buckets[i]; num != -1; num = this._entries[num].next)
					{
						object obj;
						object obj2;
						this._entries[num].depHnd.GetPrimaryAndSecondary(out obj, out obj2);
						if (object.Equals(obj, key))
						{
							value = (TValue)((object)obj2);
							return (TKey)((object)obj);
						}
					}
				}
			}
			value = default(TValue);
			return default(TKey);
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06005DF5 RID: 24053 RVA: 0x00149D24 File Offset: 0x00147F24
		internal ICollection<TKey> Keys
		{
			[SecuritySafeCritical]
			get
			{
				List<TKey> list = new List<TKey>();
				object @lock = this._lock;
				lock (@lock)
				{
					for (int i = 0; i < this._buckets.Length; i++)
					{
						for (int num = this._buckets[i]; num != -1; num = this._entries[num].next)
						{
							TKey tkey = (TKey)((object)this._entries[num].depHnd.GetPrimary());
							if (tkey != null)
							{
								list.Add(tkey);
							}
						}
					}
				}
				return list;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06005DF6 RID: 24054 RVA: 0x00149DCC File Offset: 0x00147FCC
		internal ICollection<TValue> Values
		{
			[SecuritySafeCritical]
			get
			{
				List<TValue> list = new List<TValue>();
				object @lock = this._lock;
				lock (@lock)
				{
					for (int i = 0; i < this._buckets.Length; i++)
					{
						for (int num = this._buckets[i]; num != -1; num = this._entries[num].next)
						{
							object obj = null;
							object obj2 = null;
							this._entries[num].depHnd.GetPrimaryAndSecondary(out obj, out obj2);
							if (obj != null)
							{
								list.Add((TValue)((object)obj2));
							}
						}
					}
				}
				return list;
			}
		}

		// Token: 0x06005DF7 RID: 24055 RVA: 0x00149E78 File Offset: 0x00148078
		[SecuritySafeCritical]
		internal void Clear()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				for (int i = 0; i < this._buckets.Length; i++)
				{
					this._buckets[i] = -1;
				}
				int j;
				for (j = 0; j < this._entries.Length; j++)
				{
					if (this._entries[j].depHnd.IsAllocated)
					{
						this._entries[j].depHnd.Free();
					}
					this._entries[j].next = j - 1;
				}
				this._freeList = j - 1;
			}
		}

		// Token: 0x06005DF8 RID: 24056 RVA: 0x00149F2C File Offset: 0x0014812C
		[SecurityCritical]
		private bool TryGetValueWorker(TKey key, out TValue value)
		{
			int num = this.FindEntry(key);
			if (num != -1)
			{
				object obj = null;
				object obj2 = null;
				this._entries[num].depHnd.GetPrimaryAndSecondary(out obj, out obj2);
				if (obj != null)
				{
					value = (TValue)((object)obj2);
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06005DF9 RID: 24057 RVA: 0x00149F7C File Offset: 0x0014817C
		[SecurityCritical]
		private void CreateEntry(TKey key, TValue value)
		{
			if (this._freeList == -1)
			{
				this.Resize();
			}
			int num = RuntimeHelpers.GetHashCode(key) & int.MaxValue;
			int num2 = num % this._buckets.Length;
			int freeList = this._freeList;
			this._freeList = this._entries[freeList].next;
			this._entries[freeList].hashCode = num;
			this._entries[freeList].depHnd = new DependentHandle(key, value);
			this._entries[freeList].next = this._buckets[num2];
			this._buckets[num2] = freeList;
		}

		// Token: 0x06005DFA RID: 24058 RVA: 0x0014A02C File Offset: 0x0014822C
		[SecurityCritical]
		private void Resize()
		{
			int num = this._buckets.Length;
			bool flag = false;
			int i;
			for (i = 0; i < this._entries.Length; i++)
			{
				if (this._entries[i].depHnd.IsAllocated && this._entries[i].depHnd.GetPrimary() == null)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				num = HashHelpers.GetPrime((this._buckets.Length == 0) ? 6 : (this._buckets.Length * 2));
			}
			int num2 = -1;
			int[] array = new int[num];
			for (int j = 0; j < num; j++)
			{
				array[j] = -1;
			}
			ConditionalWeakTable<TKey, TValue>.Entry[] array2 = new ConditionalWeakTable<TKey, TValue>.Entry[num];
			for (i = 0; i < this._entries.Length; i++)
			{
				DependentHandle depHnd = this._entries[i].depHnd;
				if (depHnd.IsAllocated && depHnd.GetPrimary() != null)
				{
					int num3 = this._entries[i].hashCode % num;
					array2[i].depHnd = depHnd;
					array2[i].hashCode = this._entries[i].hashCode;
					array2[i].next = array[num3];
					array[num3] = i;
				}
				else
				{
					this._entries[i].depHnd.Free();
					array2[i].depHnd = default(DependentHandle);
					array2[i].next = num2;
					num2 = i;
				}
			}
			while (i != array2.Length)
			{
				array2[i].depHnd = default(DependentHandle);
				array2[i].next = num2;
				num2 = i;
				i++;
			}
			this._buckets = array;
			this._entries = array2;
			this._freeList = num2;
		}

		// Token: 0x06005DFB RID: 24059 RVA: 0x0014A1EC File Offset: 0x001483EC
		[SecurityCritical]
		private int FindEntry(TKey key)
		{
			int num = RuntimeHelpers.GetHashCode(key) & int.MaxValue;
			for (int num2 = this._buckets[num % this._buckets.Length]; num2 != -1; num2 = this._entries[num2].next)
			{
				if (this._entries[num2].hashCode == num && this._entries[num2].depHnd.GetPrimary() == key)
				{
					return num2;
				}
			}
			return -1;
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x0014A26A File Offset: 0x0014846A
		private void VerifyIntegrity()
		{
			if (this._invalid)
			{
				throw new InvalidOperationException(Environment.GetResourceString("CollectionCorrupted"));
			}
		}

		// Token: 0x06005DFD RID: 24061 RVA: 0x0014A284 File Offset: 0x00148484
		[SecuritySafeCritical]
		protected override void Finalize()
		{
			try
			{
				if (!Environment.HasShutdownStarted)
				{
					if (this._lock != null)
					{
						object @lock = this._lock;
						lock (@lock)
						{
							if (!this._invalid)
							{
								ConditionalWeakTable<TKey, TValue>.Entry[] entries = this._entries;
								this._invalid = true;
								this._entries = null;
								this._buckets = null;
								for (int i = 0; i < entries.Length; i++)
								{
									entries[i].depHnd.Free();
								}
							}
						}
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x04002A46 RID: 10822
		private int[] _buckets;

		// Token: 0x04002A47 RID: 10823
		private ConditionalWeakTable<TKey, TValue>.Entry[] _entries;

		// Token: 0x04002A48 RID: 10824
		private int _freeList;

		// Token: 0x04002A49 RID: 10825
		private const int _initialCapacity = 5;

		// Token: 0x04002A4A RID: 10826
		private readonly object _lock;

		// Token: 0x04002A4B RID: 10827
		private bool _invalid;

		// Token: 0x02000C8E RID: 3214
		// (Invoke) Token: 0x060070F2 RID: 28914
		[__DynamicallyInvokable]
		public delegate TValue CreateValueCallback(TKey key);

		// Token: 0x02000C8F RID: 3215
		private struct Entry
		{
			// Token: 0x04003843 RID: 14403
			public DependentHandle depHnd;

			// Token: 0x04003844 RID: 14404
			public int hashCode;

			// Token: 0x04003845 RID: 14405
			public int next;
		}

		// Token: 0x02000C90 RID: 3216
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060070F5 RID: 28917 RVA: 0x00184CF3 File Offset: 0x00182EF3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060070F6 RID: 28918 RVA: 0x00184CFF File Offset: 0x00182EFF
			public <>c()
			{
			}

			// Token: 0x060070F7 RID: 28919 RVA: 0x00184D07 File Offset: 0x00182F07
			internal TValue <GetOrCreateValue>b__5_0(TKey k)
			{
				return Activator.CreateInstance<TValue>();
			}

			// Token: 0x04003846 RID: 14406
			public static readonly ConditionalWeakTable<TKey, TValue>.<>c <>9 = new ConditionalWeakTable<TKey, TValue>.<>c();

			// Token: 0x04003847 RID: 14407
			public static ConditionalWeakTable<TKey, TValue>.CreateValueCallback <>9__5_0;
		}
	}
}
