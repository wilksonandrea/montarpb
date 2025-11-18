using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x020004AF RID: 1199
	[ComVisible(false)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<>))]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class ConcurrentQueue<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x0600397B RID: 14715 RVA: 0x000DC180 File Offset: 0x000DA380
		[__DynamicallyInvokable]
		public ConcurrentQueue()
		{
			this.m_head = (this.m_tail = new ConcurrentQueue<T>.Segment(0L, this));
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000DC1B0 File Offset: 0x000DA3B0
		private void InitializeFromCollection(IEnumerable<T> collection)
		{
			ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(0L, this);
			this.m_head = segment;
			int num = 0;
			foreach (T t in collection)
			{
				segment.UnsafeAdd(t);
				num++;
				if (num >= 32)
				{
					segment = segment.UnsafeGrow();
					num = 0;
				}
			}
			this.m_tail = segment;
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x000DC228 File Offset: 0x000DA428
		[__DynamicallyInvokable]
		public ConcurrentQueue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x000DC245 File Offset: 0x000DA445
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.m_serializationArray = this.ToArray();
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x000DC253 File Offset: 0x000DA453
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.InitializeFromCollection(this.m_serializationArray);
			this.m_serializationArray = null;
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x000DC268 File Offset: 0x000DA468
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			((ICollection)this.ToList()).CopyTo(array, index);
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06003981 RID: 14721 RVA: 0x000DC285 File Offset: 0x000DA485
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06003982 RID: 14722 RVA: 0x000DC288 File Offset: 0x000DA488
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported"));
			}
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000DC299 File Offset: 0x000DA499
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000DC2A1 File Offset: 0x000DA4A1
		[__DynamicallyInvokable]
		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Enqueue(item);
			return true;
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000DC2AB File Offset: 0x000DA4AB
		[__DynamicallyInvokable]
		bool IProducerConsumerCollection<T>.TryTake(out T item)
		{
			return this.TryDequeue(out item);
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x000DC2B4 File Offset: 0x000DA4B4
		[__DynamicallyInvokable]
		public bool IsEmpty
		{
			[__DynamicallyInvokable]
			get
			{
				ConcurrentQueue<T>.Segment segment = this.m_head;
				if (!segment.IsEmpty)
				{
					return false;
				}
				if (segment.Next == null)
				{
					return true;
				}
				SpinWait spinWait = default(SpinWait);
				while (segment.IsEmpty)
				{
					if (segment.Next == null)
					{
						return true;
					}
					spinWait.SpinOnce();
					segment = this.m_head;
				}
				return false;
			}
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000DC30B File Offset: 0x000DA50B
		[__DynamicallyInvokable]
		public T[] ToArray()
		{
			return this.ToList().ToArray();
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000DC318 File Offset: 0x000DA518
		private List<T> ToList()
		{
			Interlocked.Increment(ref this.m_numSnapshotTakers);
			List<T> list = new List<T>();
			try
			{
				ConcurrentQueue<T>.Segment segment;
				ConcurrentQueue<T>.Segment segment2;
				int num;
				int num2;
				this.GetHeadTailPositions(out segment, out segment2, out num, out num2);
				if (segment == segment2)
				{
					segment.AddToList(list, num, num2);
				}
				else
				{
					segment.AddToList(list, num, 31);
					for (ConcurrentQueue<T>.Segment segment3 = segment.Next; segment3 != segment2; segment3 = segment3.Next)
					{
						segment3.AddToList(list, 0, 31);
					}
					segment2.AddToList(list, 0, num2);
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.m_numSnapshotTakers);
			}
			return list;
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000DC3AC File Offset: 0x000DA5AC
		private void GetHeadTailPositions(out ConcurrentQueue<T>.Segment head, out ConcurrentQueue<T>.Segment tail, out int headLow, out int tailHigh)
		{
			head = this.m_head;
			tail = this.m_tail;
			headLow = head.Low;
			tailHigh = tail.High;
			SpinWait spinWait = default(SpinWait);
			while (head != this.m_head || tail != this.m_tail || headLow != head.Low || tailHigh != tail.High || head.m_index > tail.m_index)
			{
				spinWait.SpinOnce();
				head = this.m_head;
				tail = this.m_tail;
				headLow = head.Low;
				tailHigh = tail.High;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600398A RID: 14730 RVA: 0x000DC458 File Offset: 0x000DA658
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				ConcurrentQueue<T>.Segment segment;
				ConcurrentQueue<T>.Segment segment2;
				int num;
				int num2;
				this.GetHeadTailPositions(out segment, out segment2, out num, out num2);
				if (segment == segment2)
				{
					return num2 - num + 1;
				}
				int num3 = 32 - num;
				num3 += 32 * (int)(segment2.m_index - segment.m_index - 1L);
				return num3 + (num2 + 1);
			}
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000DC4A6 File Offset: 0x000DA6A6
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this.ToList().CopyTo(array, index);
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000DC4C4 File Offset: 0x000DA6C4
		[__DynamicallyInvokable]
		public IEnumerator<T> GetEnumerator()
		{
			Interlocked.Increment(ref this.m_numSnapshotTakers);
			ConcurrentQueue<T>.Segment segment;
			ConcurrentQueue<T>.Segment segment2;
			int num;
			int num2;
			this.GetHeadTailPositions(out segment, out segment2, out num, out num2);
			return this.GetEnumerator(segment, segment2, num, num2);
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x000DC4F5 File Offset: 0x000DA6F5
		private IEnumerator<T> GetEnumerator(ConcurrentQueue<T>.Segment head, ConcurrentQueue<T>.Segment tail, int headLow, int tailHigh)
		{
			try
			{
				SpinWait spin = default(SpinWait);
				if (head == tail)
				{
					int num;
					for (int i = headLow; i <= tailHigh; i = num + 1)
					{
						spin.Reset();
						while (!head.m_state[i].m_value)
						{
							spin.SpinOnce();
						}
						yield return head.m_array[i];
						num = i;
					}
				}
				else
				{
					int num;
					for (int i = headLow; i < 32; i = num + 1)
					{
						spin.Reset();
						while (!head.m_state[i].m_value)
						{
							spin.SpinOnce();
						}
						yield return head.m_array[i];
						num = i;
					}
					ConcurrentQueue<T>.Segment curr;
					for (curr = head.Next; curr != tail; curr = curr.Next)
					{
						for (int i = 0; i < 32; i = num + 1)
						{
							spin.Reset();
							while (!curr.m_state[i].m_value)
							{
								spin.SpinOnce();
							}
							yield return curr.m_array[i];
							num = i;
						}
					}
					for (int i = 0; i <= tailHigh; i = num + 1)
					{
						spin.Reset();
						while (!tail.m_state[i].m_value)
						{
							spin.SpinOnce();
						}
						yield return tail.m_array[i];
						num = i;
					}
					curr = null;
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.m_numSnapshotTakers);
			}
			yield break;
			yield break;
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x000DC524 File Offset: 0x000DA724
		[__DynamicallyInvokable]
		public void Enqueue(T item)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				ConcurrentQueue<T>.Segment tail = this.m_tail;
				if (tail.TryAppend(item))
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x000DC554 File Offset: 0x000DA754
		[__DynamicallyInvokable]
		public bool TryDequeue(out T result)
		{
			while (!this.IsEmpty)
			{
				ConcurrentQueue<T>.Segment head = this.m_head;
				if (head.TryRemove(out result))
				{
					return true;
				}
			}
			result = default(T);
			return false;
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x000DC588 File Offset: 0x000DA788
		[__DynamicallyInvokable]
		public bool TryPeek(out T result)
		{
			Interlocked.Increment(ref this.m_numSnapshotTakers);
			while (!this.IsEmpty)
			{
				ConcurrentQueue<T>.Segment head = this.m_head;
				if (head.TryPeek(out result))
				{
					Interlocked.Decrement(ref this.m_numSnapshotTakers);
					return true;
				}
			}
			result = default(T);
			Interlocked.Decrement(ref this.m_numSnapshotTakers);
			return false;
		}

		// Token: 0x04001927 RID: 6439
		[NonSerialized]
		private volatile ConcurrentQueue<T>.Segment m_head;

		// Token: 0x04001928 RID: 6440
		[NonSerialized]
		private volatile ConcurrentQueue<T>.Segment m_tail;

		// Token: 0x04001929 RID: 6441
		private T[] m_serializationArray;

		// Token: 0x0400192A RID: 6442
		private const int SEGMENT_SIZE = 32;

		// Token: 0x0400192B RID: 6443
		[NonSerialized]
		internal volatile int m_numSnapshotTakers;

		// Token: 0x02000BCA RID: 3018
		private class Segment
		{
			// Token: 0x06006E87 RID: 28295 RVA: 0x0017D117 File Offset: 0x0017B317
			internal Segment(long index, ConcurrentQueue<T> source)
			{
				this.m_array = new T[32];
				this.m_state = new VolatileBool[32];
				this.m_high = -1;
				this.m_index = index;
				this.m_source = source;
			}

			// Token: 0x170012E3 RID: 4835
			// (get) Token: 0x06006E88 RID: 28296 RVA: 0x0017D156 File Offset: 0x0017B356
			internal ConcurrentQueue<T>.Segment Next
			{
				get
				{
					return this.m_next;
				}
			}

			// Token: 0x170012E4 RID: 4836
			// (get) Token: 0x06006E89 RID: 28297 RVA: 0x0017D160 File Offset: 0x0017B360
			internal bool IsEmpty
			{
				get
				{
					return this.Low > this.High;
				}
			}

			// Token: 0x06006E8A RID: 28298 RVA: 0x0017D170 File Offset: 0x0017B370
			internal void UnsafeAdd(T value)
			{
				this.m_high++;
				this.m_array[this.m_high] = value;
				this.m_state[this.m_high].m_value = true;
			}

			// Token: 0x06006E8B RID: 28299 RVA: 0x0017D1C4 File Offset: 0x0017B3C4
			internal ConcurrentQueue<T>.Segment UnsafeGrow()
			{
				ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(this.m_index + 1L, this.m_source);
				this.m_next = segment;
				return segment;
			}

			// Token: 0x06006E8C RID: 28300 RVA: 0x0017D1F4 File Offset: 0x0017B3F4
			internal void Grow()
			{
				ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(this.m_index + 1L, this.m_source);
				this.m_next = segment;
				this.m_source.m_tail = this.m_next;
			}

			// Token: 0x06006E8D RID: 28301 RVA: 0x0017D238 File Offset: 0x0017B438
			internal bool TryAppend(T value)
			{
				if (this.m_high >= 31)
				{
					return false;
				}
				int num = 32;
				try
				{
				}
				finally
				{
					num = Interlocked.Increment(ref this.m_high);
					if (num <= 31)
					{
						this.m_array[num] = value;
						this.m_state[num].m_value = true;
					}
					if (num == 31)
					{
						this.Grow();
					}
				}
				return num <= 31;
			}

			// Token: 0x06006E8E RID: 28302 RVA: 0x0017D2B4 File Offset: 0x0017B4B4
			internal bool TryRemove(out T result)
			{
				SpinWait spinWait = default(SpinWait);
				int i = this.Low;
				int num = this.High;
				while (i <= num)
				{
					if (Interlocked.CompareExchange(ref this.m_low, i + 1, i) == i)
					{
						SpinWait spinWait2 = default(SpinWait);
						while (!this.m_state[i].m_value)
						{
							spinWait2.SpinOnce();
						}
						result = this.m_array[i];
						if (this.m_source.m_numSnapshotTakers <= 0)
						{
							this.m_array[i] = default(T);
						}
						if (i + 1 >= 32)
						{
							spinWait2 = default(SpinWait);
							while (this.m_next == null)
							{
								spinWait2.SpinOnce();
							}
							this.m_source.m_head = this.m_next;
						}
						return true;
					}
					spinWait.SpinOnce();
					i = this.Low;
					num = this.High;
				}
				result = default(T);
				return false;
			}

			// Token: 0x06006E8F RID: 28303 RVA: 0x0017D3B8 File Offset: 0x0017B5B8
			internal bool TryPeek(out T result)
			{
				result = default(T);
				int low = this.Low;
				if (low > this.High)
				{
					return false;
				}
				SpinWait spinWait = default(SpinWait);
				while (!this.m_state[low].m_value)
				{
					spinWait.SpinOnce();
				}
				result = this.m_array[low];
				return true;
			}

			// Token: 0x06006E90 RID: 28304 RVA: 0x0017D41C File Offset: 0x0017B61C
			internal void AddToList(List<T> list, int start, int end)
			{
				for (int i = start; i <= end; i++)
				{
					SpinWait spinWait = default(SpinWait);
					while (!this.m_state[i].m_value)
					{
						spinWait.SpinOnce();
					}
					list.Add(this.m_array[i]);
				}
			}

			// Token: 0x170012E5 RID: 4837
			// (get) Token: 0x06006E91 RID: 28305 RVA: 0x0017D471 File Offset: 0x0017B671
			internal int Low
			{
				get
				{
					return Math.Min(this.m_low, 32);
				}
			}

			// Token: 0x170012E6 RID: 4838
			// (get) Token: 0x06006E92 RID: 28306 RVA: 0x0017D482 File Offset: 0x0017B682
			internal int High
			{
				get
				{
					return Math.Min(this.m_high, 31);
				}
			}

			// Token: 0x040035AF RID: 13743
			internal volatile T[] m_array;

			// Token: 0x040035B0 RID: 13744
			internal volatile VolatileBool[] m_state;

			// Token: 0x040035B1 RID: 13745
			private volatile ConcurrentQueue<T>.Segment m_next;

			// Token: 0x040035B2 RID: 13746
			internal readonly long m_index;

			// Token: 0x040035B3 RID: 13747
			private volatile int m_low;

			// Token: 0x040035B4 RID: 13748
			private volatile int m_high;

			// Token: 0x040035B5 RID: 13749
			private volatile ConcurrentQueue<T> m_source;
		}

		// Token: 0x02000BCB RID: 3019
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__27 : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06006E93 RID: 28307 RVA: 0x0017D493 File Offset: 0x0017B693
			[DebuggerHidden]
			public <GetEnumerator>d__27(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06006E94 RID: 28308 RVA: 0x0017D4A4 File Offset: 0x0017B6A4
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num - 1 <= 3)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06006E95 RID: 28309 RVA: 0x0017D4E0 File Offset: 0x0017B6E0
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						this.<>1__state = -3;
						spin = default(SpinWait);
						if (head != tail)
						{
							i = headLow;
							goto IL_180;
						}
						i = headLow;
						break;
					case 1:
					{
						this.<>1__state = -3;
						int num = i;
						i = num + 1;
						break;
					}
					case 2:
					{
						this.<>1__state = -3;
						int num = i;
						i = num + 1;
						goto IL_180;
					}
					case 3:
					{
						this.<>1__state = -3;
						int num = i;
						i = num + 1;
						goto IL_229;
					}
					case 4:
					{
						this.<>1__state = -3;
						int num = i;
						i = num + 1;
						goto IL_2DB;
					}
					default:
						return false;
					}
					if (i > tailHigh)
					{
						goto IL_2F3;
					}
					spin.Reset();
					while (!head.m_state[i].m_value)
					{
						spin.SpinOnce();
					}
					this.<>2__current = head.m_array[i];
					this.<>1__state = 1;
					return true;
					IL_180:
					if (i >= 32)
					{
						curr = head.Next;
						goto IL_247;
					}
					spin.Reset();
					while (!head.m_state[i].m_value)
					{
						spin.SpinOnce();
					}
					this.<>2__current = head.m_array[i];
					this.<>1__state = 2;
					return true;
					IL_229:
					if (i < 32)
					{
						spin.Reset();
						while (!curr.m_state[i].m_value)
						{
							spin.SpinOnce();
						}
						this.<>2__current = curr.m_array[i];
						this.<>1__state = 3;
						return true;
					}
					curr = curr.Next;
					IL_247:
					if (curr != tail)
					{
						i = 0;
						goto IL_229;
					}
					i = 0;
					IL_2DB:
					if (i <= tailHigh)
					{
						spin.Reset();
						while (!tail.m_state[i].m_value)
						{
							spin.SpinOnce();
						}
						this.<>2__current = tail.m_array[i];
						this.<>1__state = 4;
						return true;
					}
					curr = null;
					IL_2F3:
					this.<>m__Finally1();
					flag = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06006E96 RID: 28310 RVA: 0x0017D810 File Offset: 0x0017BA10
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				ConcurrentQueue<T> concurrentQueue = this;
				Interlocked.Decrement(ref concurrentQueue.m_numSnapshotTakers);
			}

			// Token: 0x170012E7 RID: 4839
			// (get) Token: 0x06006E97 RID: 28311 RVA: 0x0017D837 File Offset: 0x0017BA37
			T IEnumerator<T>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006E98 RID: 28312 RVA: 0x0017D83F File Offset: 0x0017BA3F
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170012E8 RID: 4840
			// (get) Token: 0x06006E99 RID: 28313 RVA: 0x0017D846 File Offset: 0x0017BA46
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040035B6 RID: 13750
			private int <>1__state;

			// Token: 0x040035B7 RID: 13751
			private T <>2__current;

			// Token: 0x040035B8 RID: 13752
			public ConcurrentQueue<T>.Segment head;

			// Token: 0x040035B9 RID: 13753
			public ConcurrentQueue<T>.Segment tail;

			// Token: 0x040035BA RID: 13754
			public int headLow;

			// Token: 0x040035BB RID: 13755
			public int tailHigh;

			// Token: 0x040035BC RID: 13756
			public ConcurrentQueue<T> <>4__this;

			// Token: 0x040035BD RID: 13757
			private SpinWait <spin>5__2;

			// Token: 0x040035BE RID: 13758
			private int <i>5__3;

			// Token: 0x040035BF RID: 13759
			private ConcurrentQueue<T>.Segment <curr>5__4;
		}
	}
}
