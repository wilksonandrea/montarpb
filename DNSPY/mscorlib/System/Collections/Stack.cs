using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000492 RID: 1170
	[DebuggerTypeProxy(typeof(Stack.StackDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	[Serializable]
	public class Stack : ICollection, IEnumerable, ICloneable
	{
		// Token: 0x06003825 RID: 14373 RVA: 0x000D7651 File Offset: 0x000D5851
		public Stack()
		{
			this._array = new object[10];
			this._size = 0;
			this._version = 0;
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000D7674 File Offset: 0x000D5874
		public Stack(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (initialCapacity < 10)
			{
				initialCapacity = 10;
			}
			this._array = new object[initialCapacity];
			this._size = 0;
			this._version = 0;
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000D76C4 File Offset: 0x000D58C4
		public Stack(ICollection col)
			: this((col == null) ? 32 : col.Count)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Push(obj);
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06003828 RID: 14376 RVA: 0x000D770F File Offset: 0x000D590F
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06003829 RID: 14377 RVA: 0x000D7717 File Offset: 0x000D5917
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x0600382A RID: 14378 RVA: 0x000D771A File Offset: 0x000D591A
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000D773C File Offset: 0x000D593C
		public virtual void Clear()
		{
			Array.Clear(this._array, 0, this._size);
			this._size = 0;
			this._version++;
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000D7768 File Offset: 0x000D5968
		public virtual object Clone()
		{
			Stack stack = new Stack(this._size);
			stack._size = this._size;
			Array.Copy(this._array, 0, stack._array, 0, this._size);
			stack._version = this._version;
			return stack;
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000D77B4 File Offset: 0x000D59B4
		public virtual bool Contains(object obj)
		{
			int size = this._size;
			while (size-- > 0)
			{
				if (obj == null)
				{
					if (this._array[size] == null)
					{
						return true;
					}
				}
				else if (this._array[size] != null && this._array[size].Equals(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x000D7800 File Offset: 0x000D5A00
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
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < this._size)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int i = 0;
			if (array is object[])
			{
				object[] array2 = (object[])array;
				while (i < this._size)
				{
					array2[i + index] = this._array[this._size - i - 1];
					i++;
				}
				return;
			}
			while (i < this._size)
			{
				array.SetValue(this._array[this._size - i - 1], i + index);
				i++;
			}
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000D78CB File Offset: 0x000D5ACB
		public virtual IEnumerator GetEnumerator()
		{
			return new Stack.StackEnumerator(this);
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000D78D3 File Offset: 0x000D5AD3
		public virtual object Peek()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
			}
			return this._array[this._size - 1];
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000D78FC File Offset: 0x000D5AFC
		public virtual object Pop()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
			}
			this._version++;
			object[] array = this._array;
			int num = this._size - 1;
			this._size = num;
			object obj = array[num];
			this._array[this._size] = null;
			return obj;
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000D7958 File Offset: 0x000D5B58
		public virtual void Push(object obj)
		{
			if (this._size == this._array.Length)
			{
				object[] array = new object[2 * this._array.Length];
				Array.Copy(this._array, 0, array, 0, this._size);
				this._array = array;
			}
			object[] array2 = this._array;
			int size = this._size;
			this._size = size + 1;
			array2[size] = obj;
			this._version++;
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000D79C7 File Offset: 0x000D5BC7
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Stack Synchronized(Stack stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}
			return new Stack.SyncStack(stack);
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x000D79E0 File Offset: 0x000D5BE0
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			for (int i = 0; i < this._size; i++)
			{
				array[i] = this._array[this._size - i - 1];
			}
			return array;
		}

		// Token: 0x040018D3 RID: 6355
		private object[] _array;

		// Token: 0x040018D4 RID: 6356
		private int _size;

		// Token: 0x040018D5 RID: 6357
		private int _version;

		// Token: 0x040018D6 RID: 6358
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040018D7 RID: 6359
		private const int _defaultCapacity = 10;

		// Token: 0x02000BB2 RID: 2994
		[Serializable]
		private class SyncStack : Stack
		{
			// Token: 0x06006DD2 RID: 28114 RVA: 0x0017B2E8 File Offset: 0x001794E8
			internal SyncStack(Stack stack)
			{
				this._s = stack;
				this._root = stack.SyncRoot;
			}

			// Token: 0x170012A0 RID: 4768
			// (get) Token: 0x06006DD3 RID: 28115 RVA: 0x0017B303 File Offset: 0x00179503
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012A1 RID: 4769
			// (get) Token: 0x06006DD4 RID: 28116 RVA: 0x0017B306 File Offset: 0x00179506
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170012A2 RID: 4770
			// (get) Token: 0x06006DD5 RID: 28117 RVA: 0x0017B310 File Offset: 0x00179510
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._s.Count;
					}
					return count;
				}
			}

			// Token: 0x06006DD6 RID: 28118 RVA: 0x0017B358 File Offset: 0x00179558
			public override bool Contains(object obj)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._s.Contains(obj);
				}
				return flag2;
			}

			// Token: 0x06006DD7 RID: 28119 RVA: 0x0017B3A0 File Offset: 0x001795A0
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = new Stack.SyncStack((Stack)this._s.Clone());
				}
				return obj;
			}

			// Token: 0x06006DD8 RID: 28120 RVA: 0x0017B3F4 File Offset: 0x001795F4
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._s.Clear();
				}
			}

			// Token: 0x06006DD9 RID: 28121 RVA: 0x0017B43C File Offset: 0x0017963C
			public override void CopyTo(Array array, int arrayIndex)
			{
				object root = this._root;
				lock (root)
				{
					this._s.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06006DDA RID: 28122 RVA: 0x0017B484 File Offset: 0x00179684
			public override void Push(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._s.Push(value);
				}
			}

			// Token: 0x06006DDB RID: 28123 RVA: 0x0017B4CC File Offset: 0x001796CC
			public override object Pop()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._s.Pop();
				}
				return obj;
			}

			// Token: 0x06006DDC RID: 28124 RVA: 0x0017B514 File Offset: 0x00179714
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._s.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006DDD RID: 28125 RVA: 0x0017B55C File Offset: 0x0017975C
			public override object Peek()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._s.Peek();
				}
				return obj;
			}

			// Token: 0x06006DDE RID: 28126 RVA: 0x0017B5A4 File Offset: 0x001797A4
			public override object[] ToArray()
			{
				object root = this._root;
				object[] array;
				lock (root)
				{
					array = this._s.ToArray();
				}
				return array;
			}

			// Token: 0x04003568 RID: 13672
			private Stack _s;

			// Token: 0x04003569 RID: 13673
			private object _root;
		}

		// Token: 0x02000BB3 RID: 2995
		[Serializable]
		private class StackEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06006DDF RID: 28127 RVA: 0x0017B5EC File Offset: 0x001797EC
			internal StackEnumerator(Stack stack)
			{
				this._stack = stack;
				this._version = this._stack._version;
				this._index = -2;
				this.currentElement = null;
			}

			// Token: 0x06006DE0 RID: 28128 RVA: 0x0017B61B File Offset: 0x0017981B
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006DE1 RID: 28129 RVA: 0x0017B624 File Offset: 0x00179824
			public virtual bool MoveNext()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				bool flag;
				if (this._index == -2)
				{
					this._index = this._stack._size - 1;
					flag = this._index >= 0;
					if (flag)
					{
						this.currentElement = this._stack._array[this._index];
					}
					return flag;
				}
				if (this._index == -1)
				{
					return false;
				}
				int num = this._index - 1;
				this._index = num;
				flag = num >= 0;
				if (flag)
				{
					this.currentElement = this._stack._array[this._index];
				}
				else
				{
					this.currentElement = null;
				}
				return flag;
			}

			// Token: 0x170012A3 RID: 4771
			// (get) Token: 0x06006DE2 RID: 28130 RVA: 0x0017B6E3 File Offset: 0x001798E3
			public virtual object Current
			{
				get
				{
					if (this._index == -2)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._index == -1)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this.currentElement;
				}
			}

			// Token: 0x06006DE3 RID: 28131 RVA: 0x0017B71E File Offset: 0x0017991E
			public virtual void Reset()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this._index = -2;
				this.currentElement = null;
			}

			// Token: 0x0400356A RID: 13674
			private Stack _stack;

			// Token: 0x0400356B RID: 13675
			private int _index;

			// Token: 0x0400356C RID: 13676
			private int _version;

			// Token: 0x0400356D RID: 13677
			private object currentElement;
		}

		// Token: 0x02000BB4 RID: 2996
		internal class StackDebugView
		{
			// Token: 0x06006DE4 RID: 28132 RVA: 0x0017B752 File Offset: 0x00179952
			public StackDebugView(Stack stack)
			{
				if (stack == null)
				{
					throw new ArgumentNullException("stack");
				}
				this.stack = stack;
			}

			// Token: 0x170012A4 RID: 4772
			// (get) Token: 0x06006DE5 RID: 28133 RVA: 0x0017B76F File Offset: 0x0017996F
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this.stack.ToArray();
				}
			}

			// Token: 0x0400356E RID: 13678
			private Stack stack;
		}
	}
}
