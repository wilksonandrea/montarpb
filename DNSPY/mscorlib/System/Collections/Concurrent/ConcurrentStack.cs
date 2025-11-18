using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x020004AA RID: 1194
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<>))]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class ConcurrentStack<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x06003907 RID: 14599 RVA: 0x000DA39B File Offset: 0x000D859B
		[__DynamicallyInvokable]
		public ConcurrentStack()
		{
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000DA3A3 File Offset: 0x000D85A3
		[__DynamicallyInvokable]
		public ConcurrentStack(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x000DA3C0 File Offset: 0x000D85C0
		private void InitializeFromCollection(IEnumerable<T> collection)
		{
			ConcurrentStack<T>.Node node = null;
			foreach (T t in collection)
			{
				node = new ConcurrentStack<T>.Node(t)
				{
					m_next = node
				};
			}
			this.m_head = node;
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000DA41C File Offset: 0x000D861C
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.m_serializationArray = this.ToArray();
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000DA42C File Offset: 0x000D862C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			ConcurrentStack<T>.Node node = null;
			ConcurrentStack<T>.Node node2 = null;
			for (int i = 0; i < this.m_serializationArray.Length; i++)
			{
				ConcurrentStack<T>.Node node3 = new ConcurrentStack<T>.Node(this.m_serializationArray[i]);
				if (node == null)
				{
					node2 = node3;
				}
				else
				{
					node.m_next = node3;
				}
				node = node3;
			}
			this.m_head = node2;
			this.m_serializationArray = null;
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x0600390C RID: 14604 RVA: 0x000DA482 File Offset: 0x000D8682
		[__DynamicallyInvokable]
		public bool IsEmpty
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_head == null;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600390D RID: 14605 RVA: 0x000DA490 File Offset: 0x000D8690
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				int num = 0;
				for (ConcurrentStack<T>.Node node = this.m_head; node != null; node = node.m_next)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x0600390E RID: 14606 RVA: 0x000DA4B9 File Offset: 0x000D86B9
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x000DA4BC File Offset: 0x000D86BC
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported"));
			}
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000DA4CD File Offset: 0x000D86CD
		[__DynamicallyInvokable]
		public void Clear()
		{
			this.m_head = null;
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000DA4D8 File Offset: 0x000D86D8
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			((ICollection)this.ToList()).CopyTo(array, index);
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000DA4F5 File Offset: 0x000D86F5
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this.ToList().CopyTo(array, index);
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000DA514 File Offset: 0x000D8714
		[__DynamicallyInvokable]
		public void Push(T item)
		{
			ConcurrentStack<T>.Node node = new ConcurrentStack<T>.Node(item);
			node.m_next = this.m_head;
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, node, node.m_next) == node.m_next)
			{
				return;
			}
			this.PushCore(node, node);
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000DA559 File Offset: 0x000D8759
		[__DynamicallyInvokable]
		public void PushRange(T[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.PushRange(items, 0, items.Length);
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000DA574 File Offset: 0x000D8774
		[__DynamicallyInvokable]
		public void PushRange(T[] items, int startIndex, int count)
		{
			this.ValidatePushPopRangeInput(items, startIndex, count);
			if (count == 0)
			{
				return;
			}
			ConcurrentStack<T>.Node node2;
			ConcurrentStack<T>.Node node = (node2 = new ConcurrentStack<T>.Node(items[startIndex]));
			for (int i = startIndex + 1; i < startIndex + count; i++)
			{
				node2 = new ConcurrentStack<T>.Node(items[i])
				{
					m_next = node2
				};
			}
			node.m_next = this.m_head;
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, node2, node.m_next) == node.m_next)
			{
				return;
			}
			this.PushCore(node2, node);
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000DA5F4 File Offset: 0x000D87F4
		private void PushCore(ConcurrentStack<T>.Node head, ConcurrentStack<T>.Node tail)
		{
			SpinWait spinWait = default(SpinWait);
			do
			{
				spinWait.SpinOnce();
				tail.m_next = this.m_head;
			}
			while (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, head, tail.m_next) != tail.m_next);
			if (CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPushFailed(spinWait.Count);
			}
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000DA658 File Offset: 0x000D8858
		private void ValidatePushPopRangeInput(T[] items, int startIndex, int count)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ConcurrentStack_PushPopRange_CountOutOfRange"));
			}
			int num = items.Length;
			if (startIndex >= num || startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ConcurrentStack_PushPopRange_StartOutOfRange"));
			}
			if (num - count < startIndex)
			{
				throw new ArgumentException(Environment.GetResourceString("ConcurrentStack_PushPopRange_InvalidCount"));
			}
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000DA6C3 File Offset: 0x000D88C3
		[__DynamicallyInvokable]
		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Push(item);
			return true;
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000DA6D0 File Offset: 0x000D88D0
		[__DynamicallyInvokable]
		public bool TryPeek(out T result)
		{
			ConcurrentStack<T>.Node head = this.m_head;
			if (head == null)
			{
				result = default(T);
				return false;
			}
			result = head.m_value;
			return true;
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000DA700 File Offset: 0x000D8900
		[__DynamicallyInvokable]
		public bool TryPop(out T result)
		{
			ConcurrentStack<T>.Node head = this.m_head;
			if (head == null)
			{
				result = default(T);
				return false;
			}
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, head.m_next, head) == head)
			{
				result = head.m_value;
				return true;
			}
			return this.TryPopCore(out result);
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000DA74C File Offset: 0x000D894C
		[__DynamicallyInvokable]
		public int TryPopRange(T[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			return this.TryPopRange(items, 0, items.Length);
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000DA768 File Offset: 0x000D8968
		[__DynamicallyInvokable]
		public int TryPopRange(T[] items, int startIndex, int count)
		{
			this.ValidatePushPopRangeInput(items, startIndex, count);
			if (count == 0)
			{
				return 0;
			}
			ConcurrentStack<T>.Node node;
			int num = this.TryPopCore(count, out node);
			if (num > 0)
			{
				this.CopyRemovedItems(node, items, startIndex, num);
			}
			return num;
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000DA79C File Offset: 0x000D899C
		private bool TryPopCore(out T result)
		{
			ConcurrentStack<T>.Node node;
			if (this.TryPopCore(1, out node) == 1)
			{
				result = node.m_value;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000DA7CC File Offset: 0x000D89CC
		private int TryPopCore(int count, out ConcurrentStack<T>.Node poppedHead)
		{
			SpinWait spinWait = default(SpinWait);
			int num = 1;
			Random random = new Random(Environment.TickCount & int.MaxValue);
			ConcurrentStack<T>.Node head;
			int num2;
			for (;;)
			{
				head = this.m_head;
				if (head == null)
				{
					break;
				}
				ConcurrentStack<T>.Node node = head;
				num2 = 1;
				while (num2 < count && node.m_next != null)
				{
					node = node.m_next;
					num2++;
				}
				if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, node.m_next, head) == head)
				{
					goto Block_5;
				}
				for (int i = 0; i < num; i++)
				{
					spinWait.SpinOnce();
				}
				num = (spinWait.NextSpinWillYield ? random.Next(1, 8) : (num * 2));
			}
			if (count == 1 && CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPopFailed(spinWait.Count);
			}
			poppedHead = null;
			return 0;
			Block_5:
			if (count == 1 && CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPopFailed(spinWait.Count);
			}
			poppedHead = head;
			return num2;
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000DA8B8 File Offset: 0x000D8AB8
		private void CopyRemovedItems(ConcurrentStack<T>.Node head, T[] collection, int startIndex, int nodesCount)
		{
			ConcurrentStack<T>.Node node = head;
			for (int i = startIndex; i < startIndex + nodesCount; i++)
			{
				collection[i] = node.m_value;
				node = node.m_next;
			}
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000DA8EA File Offset: 0x000D8AEA
		[__DynamicallyInvokable]
		bool IProducerConsumerCollection<T>.TryTake(out T item)
		{
			return this.TryPop(out item);
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000DA8F3 File Offset: 0x000D8AF3
		[__DynamicallyInvokable]
		public T[] ToArray()
		{
			return this.ToList().ToArray();
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000DA900 File Offset: 0x000D8B00
		private List<T> ToList()
		{
			List<T> list = new List<T>();
			for (ConcurrentStack<T>.Node node = this.m_head; node != null; node = node.m_next)
			{
				list.Add(node.m_value);
			}
			return list;
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x000DA935 File Offset: 0x000D8B35
		[__DynamicallyInvokable]
		public IEnumerator<T> GetEnumerator()
		{
			return this.GetEnumerator(this.m_head);
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x000DA945 File Offset: 0x000D8B45
		private IEnumerator<T> GetEnumerator(ConcurrentStack<T>.Node head)
		{
			for (ConcurrentStack<T>.Node current = head; current != null; current = current.m_next)
			{
				yield return current.m_value;
			}
			yield break;
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x000DA954 File Offset: 0x000D8B54
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x04001911 RID: 6417
		[NonSerialized]
		private volatile ConcurrentStack<T>.Node m_head;

		// Token: 0x04001912 RID: 6418
		private T[] m_serializationArray;

		// Token: 0x04001913 RID: 6419
		private const int BACKOFF_MAX_YIELDS = 8;

		// Token: 0x02000BC4 RID: 3012
		private class Node
		{
			// Token: 0x06006E71 RID: 28273 RVA: 0x0017CE3C File Offset: 0x0017B03C
			internal Node(T value)
			{
				this.m_value = value;
				this.m_next = null;
			}

			// Token: 0x0400359A RID: 13722
			internal readonly T m_value;

			// Token: 0x0400359B RID: 13723
			internal ConcurrentStack<T>.Node m_next;
		}

		// Token: 0x02000BC5 RID: 3013
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__37 : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06006E72 RID: 28274 RVA: 0x0017CE52 File Offset: 0x0017B052
			[DebuggerHidden]
			public <GetEnumerator>d__37(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06006E73 RID: 28275 RVA: 0x0017CE61 File Offset: 0x0017B061
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06006E74 RID: 28276 RVA: 0x0017CE64 File Offset: 0x0017B064
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					current = current.m_next;
				}
				else
				{
					this.<>1__state = -1;
					current = head;
				}
				if (current == null)
				{
					return false;
				}
				this.<>2__current = current.m_value;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170012DB RID: 4827
			// (get) Token: 0x06006E75 RID: 28277 RVA: 0x0017CED1 File Offset: 0x0017B0D1
			T IEnumerator<T>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006E76 RID: 28278 RVA: 0x0017CED9 File Offset: 0x0017B0D9
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170012DC RID: 4828
			// (get) Token: 0x06006E77 RID: 28279 RVA: 0x0017CEE0 File Offset: 0x0017B0E0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400359C RID: 13724
			private int <>1__state;

			// Token: 0x0400359D RID: 13725
			private T <>2__current;

			// Token: 0x0400359E RID: 13726
			public ConcurrentStack<T>.Node head;

			// Token: 0x0400359F RID: 13727
			private ConcurrentStack<T>.Node <current>5__2;
		}
	}
}
