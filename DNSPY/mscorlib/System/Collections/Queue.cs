using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	// Token: 0x0200048F RID: 1167
	[DebuggerTypeProxy(typeof(Queue.QueueDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	[Serializable]
	public class Queue : ICollection, IEnumerable, ICloneable
	{
		// Token: 0x060037BE RID: 14270 RVA: 0x000D5AC9 File Offset: 0x000D3CC9
		public Queue()
			: this(32, 2f)
		{
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000D5AD8 File Offset: 0x000D3CD8
		public Queue(int capacity)
			: this(capacity, 2f)
		{
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000D5AE8 File Offset: 0x000D3CE8
		public Queue(int capacity, float growFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if ((double)growFactor < 1.0 || (double)growFactor > 10.0)
			{
				throw new ArgumentOutOfRangeException("growFactor", Environment.GetResourceString("ArgumentOutOfRange_QueueGrowFactor", new object[] { 1, 10 }));
			}
			this._array = new object[capacity];
			this._head = 0;
			this._tail = 0;
			this._size = 0;
			this._growFactor = (int)(growFactor * 100f);
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000D5B8C File Offset: 0x000D3D8C
		public Queue(ICollection col)
			: this((col == null) ? 32 : col.Count)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Enqueue(obj);
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060037C2 RID: 14274 RVA: 0x000D5BD7 File Offset: 0x000D3DD7
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000D5BE0 File Offset: 0x000D3DE0
		public virtual object Clone()
		{
			Queue queue = new Queue(this._size);
			queue._size = this._size;
			int num = this._size;
			int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
			Array.Copy(this._array, this._head, queue._array, 0, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, queue._array, this._array.Length - this._head, num);
			}
			queue._version = this._version;
			return queue;
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060037C4 RID: 14276 RVA: 0x000D5C81 File Offset: 0x000D3E81
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060037C5 RID: 14277 RVA: 0x000D5C84 File Offset: 0x000D3E84
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x000D5CA8 File Offset: 0x000D3EA8
		public virtual void Clear()
		{
			if (this._head < this._tail)
			{
				Array.Clear(this._array, this._head, this._size);
			}
			else
			{
				Array.Clear(this._array, this._head, this._array.Length - this._head);
				Array.Clear(this._array, 0, this._tail);
			}
			this._head = 0;
			this._tail = 0;
			this._size = 0;
			this._version++;
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000D5D34 File Offset: 0x000D3F34
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			int length = array.Length;
			if (length - index < this._size)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int num = this._size;
			if (num == 0)
			{
				return;
			}
			int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
			Array.Copy(this._array, this._head, array, index, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, array, index + this._array.Length - this._head, num);
			}
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000D5E10 File Offset: 0x000D4010
		public virtual void Enqueue(object obj)
		{
			if (this._size == this._array.Length)
			{
				int num = (int)((long)this._array.Length * (long)this._growFactor / 100L);
				if (num < this._array.Length + 4)
				{
					num = this._array.Length + 4;
				}
				this.SetCapacity(num);
			}
			this._array[this._tail] = obj;
			this._tail = (this._tail + 1) % this._array.Length;
			this._size++;
			this._version++;
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000D5EA4 File Offset: 0x000D40A4
		public virtual IEnumerator GetEnumerator()
		{
			return new Queue.QueueEnumerator(this);
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000D5EAC File Offset: 0x000D40AC
		public virtual object Dequeue()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyQueue"));
			}
			object obj = this._array[this._head];
			this._array[this._head] = null;
			this._head = (this._head + 1) % this._array.Length;
			this._size--;
			this._version++;
			return obj;
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000D5F21 File Offset: 0x000D4121
		public virtual object Peek()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyQueue"));
			}
			return this._array[this._head];
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000D5F48 File Offset: 0x000D4148
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Queue Synchronized(Queue queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			return new Queue.SynchronizedQueue(queue);
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000D5F60 File Offset: 0x000D4160
		public virtual bool Contains(object obj)
		{
			int num = this._head;
			int size = this._size;
			while (size-- > 0)
			{
				if (obj == null)
				{
					if (this._array[num] == null)
					{
						return true;
					}
				}
				else if (this._array[num] != null && this._array[num].Equals(obj))
				{
					return true;
				}
				num = (num + 1) % this._array.Length;
			}
			return false;
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000D5FBE File Offset: 0x000D41BE
		internal object GetElement(int i)
		{
			return this._array[(this._head + i) % this._array.Length];
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000D5FD8 File Offset: 0x000D41D8
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			if (this._size == 0)
			{
				return array;
			}
			if (this._head < this._tail)
			{
				Array.Copy(this._array, this._head, array, 0, this._size);
			}
			else
			{
				Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
				Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
			}
			return array;
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000D606C File Offset: 0x000D426C
		private void SetCapacity(int capacity)
		{
			object[] array = new object[capacity];
			if (this._size > 0)
			{
				if (this._head < this._tail)
				{
					Array.Copy(this._array, this._head, array, 0, this._size);
				}
				else
				{
					Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
					Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
				}
			}
			this._array = array;
			this._head = 0;
			this._tail = ((this._size == capacity) ? 0 : this._size);
			this._version++;
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000D612A File Offset: 0x000D432A
		public virtual void TrimToSize()
		{
			this.SetCapacity(this._size);
		}

		// Token: 0x040018BC RID: 6332
		private object[] _array;

		// Token: 0x040018BD RID: 6333
		private int _head;

		// Token: 0x040018BE RID: 6334
		private int _tail;

		// Token: 0x040018BF RID: 6335
		private int _size;

		// Token: 0x040018C0 RID: 6336
		private int _growFactor;

		// Token: 0x040018C1 RID: 6337
		private int _version;

		// Token: 0x040018C2 RID: 6338
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040018C3 RID: 6339
		private const int _MinimumGrow = 4;

		// Token: 0x040018C4 RID: 6340
		private const int _ShrinkThreshold = 32;

		// Token: 0x02000BA3 RID: 2979
		[Serializable]
		private class SynchronizedQueue : Queue
		{
			// Token: 0x06006CB4 RID: 27828 RVA: 0x00177EEF File Offset: 0x001760EF
			internal SynchronizedQueue(Queue q)
			{
				this._q = q;
				this.root = this._q.SyncRoot;
			}

			// Token: 0x17001262 RID: 4706
			// (get) Token: 0x06006CB5 RID: 27829 RVA: 0x00177F0F File Offset: 0x0017610F
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001263 RID: 4707
			// (get) Token: 0x06006CB6 RID: 27830 RVA: 0x00177F12 File Offset: 0x00176112
			public override object SyncRoot
			{
				get
				{
					return this.root;
				}
			}

			// Token: 0x17001264 RID: 4708
			// (get) Token: 0x06006CB7 RID: 27831 RVA: 0x00177F1C File Offset: 0x0017611C
			public override int Count
			{
				get
				{
					object obj = this.root;
					int count;
					lock (obj)
					{
						count = this._q.Count;
					}
					return count;
				}
			}

			// Token: 0x06006CB8 RID: 27832 RVA: 0x00177F64 File Offset: 0x00176164
			public override void Clear()
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.Clear();
				}
			}

			// Token: 0x06006CB9 RID: 27833 RVA: 0x00177FAC File Offset: 0x001761AC
			public override object Clone()
			{
				object obj = this.root;
				object obj2;
				lock (obj)
				{
					obj2 = new Queue.SynchronizedQueue((Queue)this._q.Clone());
				}
				return obj2;
			}

			// Token: 0x06006CBA RID: 27834 RVA: 0x00178000 File Offset: 0x00176200
			public override bool Contains(object obj)
			{
				object obj2 = this.root;
				bool flag2;
				lock (obj2)
				{
					flag2 = this._q.Contains(obj);
				}
				return flag2;
			}

			// Token: 0x06006CBB RID: 27835 RVA: 0x00178048 File Offset: 0x00176248
			public override void CopyTo(Array array, int arrayIndex)
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06006CBC RID: 27836 RVA: 0x00178090 File Offset: 0x00176290
			public override void Enqueue(object value)
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.Enqueue(value);
				}
			}

			// Token: 0x06006CBD RID: 27837 RVA: 0x001780D8 File Offset: 0x001762D8
			public override object Dequeue()
			{
				object obj = this.root;
				object obj2;
				lock (obj)
				{
					obj2 = this._q.Dequeue();
				}
				return obj2;
			}

			// Token: 0x06006CBE RID: 27838 RVA: 0x00178120 File Offset: 0x00176320
			public override IEnumerator GetEnumerator()
			{
				object obj = this.root;
				IEnumerator enumerator;
				lock (obj)
				{
					enumerator = this._q.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006CBF RID: 27839 RVA: 0x00178168 File Offset: 0x00176368
			public override object Peek()
			{
				object obj = this.root;
				object obj2;
				lock (obj)
				{
					obj2 = this._q.Peek();
				}
				return obj2;
			}

			// Token: 0x06006CC0 RID: 27840 RVA: 0x001781B0 File Offset: 0x001763B0
			public override object[] ToArray()
			{
				object obj = this.root;
				object[] array;
				lock (obj)
				{
					array = this._q.ToArray();
				}
				return array;
			}

			// Token: 0x06006CC1 RID: 27841 RVA: 0x001781F8 File Offset: 0x001763F8
			public override void TrimToSize()
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.TrimToSize();
				}
			}

			// Token: 0x04003543 RID: 13635
			private Queue _q;

			// Token: 0x04003544 RID: 13636
			private object root;
		}

		// Token: 0x02000BA4 RID: 2980
		[Serializable]
		private class QueueEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06006CC2 RID: 27842 RVA: 0x00178240 File Offset: 0x00176440
			internal QueueEnumerator(Queue q)
			{
				this._q = q;
				this._version = this._q._version;
				this._index = 0;
				this.currentElement = this._q._array;
				if (this._q._size == 0)
				{
					this._index = -1;
				}
			}

			// Token: 0x06006CC3 RID: 27843 RVA: 0x00178297 File Offset: 0x00176497
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006CC4 RID: 27844 RVA: 0x001782A0 File Offset: 0x001764A0
			public virtual bool MoveNext()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this._index < 0)
				{
					this.currentElement = this._q._array;
					return false;
				}
				this.currentElement = this._q.GetElement(this._index);
				this._index++;
				if (this._index == this._q._size)
				{
					this._index = -1;
				}
				return true;
			}

			// Token: 0x17001265 RID: 4709
			// (get) Token: 0x06006CC5 RID: 27845 RVA: 0x0017832C File Offset: 0x0017652C
			public virtual object Current
			{
				get
				{
					if (this.currentElement != this._q._array)
					{
						return this.currentElement;
					}
					if (this._index == 0)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
				}
			}

			// Token: 0x06006CC6 RID: 27846 RVA: 0x0017837C File Offset: 0x0017657C
			public virtual void Reset()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this._q._size == 0)
				{
					this._index = -1;
				}
				else
				{
					this._index = 0;
				}
				this.currentElement = this._q._array;
			}

			// Token: 0x04003545 RID: 13637
			private Queue _q;

			// Token: 0x04003546 RID: 13638
			private int _index;

			// Token: 0x04003547 RID: 13639
			private int _version;

			// Token: 0x04003548 RID: 13640
			private object currentElement;
		}

		// Token: 0x02000BA5 RID: 2981
		internal class QueueDebugView
		{
			// Token: 0x06006CC7 RID: 27847 RVA: 0x001783DA File Offset: 0x001765DA
			public QueueDebugView(Queue queue)
			{
				if (queue == null)
				{
					throw new ArgumentNullException("queue");
				}
				this.queue = queue;
			}

			// Token: 0x17001266 RID: 4710
			// (get) Token: 0x06006CC8 RID: 27848 RVA: 0x001783F7 File Offset: 0x001765F7
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this.queue.ToArray();
				}
			}

			// Token: 0x04003549 RID: 13641
			private Queue queue;
		}
	}
}
