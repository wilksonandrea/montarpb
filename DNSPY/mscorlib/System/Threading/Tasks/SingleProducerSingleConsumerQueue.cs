using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000584 RID: 1412
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SingleProducerSingleConsumerQueue<>.SingleProducerSingleConsumerQueue_DebugView))]
	internal sealed class SingleProducerSingleConsumerQueue<T> : IProducerConsumerQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06004270 RID: 17008 RVA: 0x000F70B0 File Offset: 0x000F52B0
		internal SingleProducerSingleConsumerQueue()
		{
			this.m_head = (this.m_tail = new SingleProducerSingleConsumerQueue<T>.Segment(32));
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x000F70E0 File Offset: 0x000F52E0
		public void Enqueue(T item)
		{
			SingleProducerSingleConsumerQueue<T>.Segment tail = this.m_tail;
			T[] array = tail.m_array;
			int last = tail.m_state.m_last;
			int num = (last + 1) & (array.Length - 1);
			if (num != tail.m_state.m_firstCopy)
			{
				array[last] = item;
				tail.m_state.m_last = num;
				return;
			}
			this.EnqueueSlow(item, ref tail);
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x000F7144 File Offset: 0x000F5344
		private void EnqueueSlow(T item, ref SingleProducerSingleConsumerQueue<T>.Segment segment)
		{
			if (segment.m_state.m_firstCopy != segment.m_state.m_first)
			{
				segment.m_state.m_firstCopy = segment.m_state.m_first;
				this.Enqueue(item);
				return;
			}
			int num = this.m_tail.m_array.Length << 1;
			if (num > 16777216)
			{
				num = 16777216;
			}
			SingleProducerSingleConsumerQueue<T>.Segment segment2 = new SingleProducerSingleConsumerQueue<T>.Segment(num);
			segment2.m_array[0] = item;
			segment2.m_state.m_last = 1;
			segment2.m_state.m_lastCopy = 1;
			try
			{
			}
			finally
			{
				Volatile.Write<SingleProducerSingleConsumerQueue<T>.Segment>(ref this.m_tail.m_next, segment2);
				this.m_tail = segment2;
			}
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x000F720C File Offset: 0x000F540C
		public bool TryDequeue(out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first != head.m_state.m_lastCopy)
			{
				result = array[first];
				array[first] = default(T);
				head.m_state.m_first = (first + 1) & (array.Length - 1);
				return true;
			}
			return this.TryDequeueSlow(ref head, ref array, out result);
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x000F7288 File Offset: 0x000F5488
		private bool TryDequeueSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryDequeue(out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			array[first] = default(T);
			segment.m_state.m_first = (first + 1) & (segment.m_array.Length - 1);
			segment.m_state.m_lastCopy = segment.m_state.m_last;
			return true;
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x000F7398 File Offset: 0x000F5598
		public bool TryPeek(out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first != head.m_state.m_lastCopy)
			{
				result = array[first];
				return true;
			}
			return this.TryPeekSlow(ref head, ref array, out result);
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x000F73EC File Offset: 0x000F55EC
		private bool TryPeekSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryPeek(out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			return true;
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x000F74B4 File Offset: 0x000F56B4
		public bool TryDequeueIf(Predicate<T> predicate, out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first == head.m_state.m_lastCopy)
			{
				return this.TryDequeueIfSlow(predicate, ref head, ref array, out result);
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				head.m_state.m_first = (first + 1) & (array.Length - 1);
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x000F7548 File Offset: 0x000F5748
		private bool TryDequeueIfSlow(Predicate<T> predicate, ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryDequeueIf(predicate, out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				segment.m_state.m_first = (first + 1) & (segment.m_array.Length - 1);
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x000F7678 File Offset: 0x000F5878
		public void Clear()
		{
			T t;
			while (this.TryDequeue(out t))
			{
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x000F7690 File Offset: 0x000F5890
		public bool IsEmpty
		{
			get
			{
				SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
				return head.m_state.m_first == head.m_state.m_lastCopy && head.m_state.m_first == head.m_state.m_last && head.m_next == null;
			}
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x000F76E9 File Offset: 0x000F58E9
		public IEnumerator<T> GetEnumerator()
		{
			SingleProducerSingleConsumerQueue<T>.Segment segment;
			for (segment = this.m_head; segment != null; segment = segment.m_next)
			{
				for (int pt = segment.m_state.m_first; pt != segment.m_state.m_last; pt = (pt + 1) & (segment.m_array.Length - 1))
				{
					yield return segment.m_array[pt];
				}
			}
			segment = null;
			yield break;
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x000F76F8 File Offset: 0x000F58F8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x0600427D RID: 17021 RVA: 0x000F7700 File Offset: 0x000F5900
		public int Count
		{
			get
			{
				int num = 0;
				for (SingleProducerSingleConsumerQueue<T>.Segment segment = this.m_head; segment != null; segment = segment.m_next)
				{
					int num2 = segment.m_array.Length;
					int first;
					int last;
					do
					{
						first = segment.m_state.m_first;
						last = segment.m_state.m_last;
					}
					while (first != segment.m_state.m_first);
					num += (last - first) & (num2 - 1);
				}
				return num;
			}
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x000F7768 File Offset: 0x000F5968
		int IProducerConsumerQueue<T>.GetCountSafe(object syncObj)
		{
			int count;
			lock (syncObj)
			{
				count = this.Count;
			}
			return count;
		}

		// Token: 0x04001B9C RID: 7068
		private const int INIT_SEGMENT_SIZE = 32;

		// Token: 0x04001B9D RID: 7069
		private const int MAX_SEGMENT_SIZE = 16777216;

		// Token: 0x04001B9E RID: 7070
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_head;

		// Token: 0x04001B9F RID: 7071
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_tail;

		// Token: 0x02000C2D RID: 3117
		[StructLayout(LayoutKind.Sequential)]
		private sealed class Segment
		{
			// Token: 0x0600702B RID: 28715 RVA: 0x001828EA File Offset: 0x00180AEA
			internal Segment(int size)
			{
				this.m_array = new T[size];
			}

			// Token: 0x040036FD RID: 14077
			internal SingleProducerSingleConsumerQueue<T>.Segment m_next;

			// Token: 0x040036FE RID: 14078
			internal readonly T[] m_array;

			// Token: 0x040036FF RID: 14079
			internal SingleProducerSingleConsumerQueue<T>.SegmentState m_state;
		}

		// Token: 0x02000C2E RID: 3118
		private struct SegmentState
		{
			// Token: 0x04003700 RID: 14080
			internal PaddingFor32 m_pad0;

			// Token: 0x04003701 RID: 14081
			internal volatile int m_first;

			// Token: 0x04003702 RID: 14082
			internal int m_lastCopy;

			// Token: 0x04003703 RID: 14083
			internal PaddingFor32 m_pad1;

			// Token: 0x04003704 RID: 14084
			internal int m_firstCopy;

			// Token: 0x04003705 RID: 14085
			internal volatile int m_last;

			// Token: 0x04003706 RID: 14086
			internal PaddingFor32 m_pad2;
		}

		// Token: 0x02000C2F RID: 3119
		private sealed class SingleProducerSingleConsumerQueue_DebugView
		{
			// Token: 0x0600702C RID: 28716 RVA: 0x001828FE File Offset: 0x00180AFE
			public SingleProducerSingleConsumerQueue_DebugView(SingleProducerSingleConsumerQueue<T> queue)
			{
				this.m_queue = queue;
			}

			// Token: 0x17001337 RID: 4919
			// (get) Token: 0x0600702D RID: 28717 RVA: 0x00182910 File Offset: 0x00180B10
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public T[] Items
			{
				get
				{
					List<T> list = new List<T>();
					foreach (T t in this.m_queue)
					{
						list.Add(t);
					}
					return list.ToArray();
				}
			}

			// Token: 0x04003707 RID: 14087
			private readonly SingleProducerSingleConsumerQueue<T> m_queue;
		}

		// Token: 0x02000C30 RID: 3120
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__16 : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x0600702E RID: 28718 RVA: 0x0018296C File Offset: 0x00180B6C
			[DebuggerHidden]
			public <GetEnumerator>d__16(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600702F RID: 28719 RVA: 0x0018297B File Offset: 0x00180B7B
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06007030 RID: 28720 RVA: 0x00182980 File Offset: 0x00180B80
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				SingleProducerSingleConsumerQueue<T> singleProducerSingleConsumerQueue = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					segment = singleProducerSingleConsumerQueue.m_head;
					goto IL_C0;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				pt = (pt + 1) & (segment.m_array.Length - 1);
				IL_95:
				if (pt != segment.m_state.m_last)
				{
					this.<>2__current = segment.m_array[pt];
					this.<>1__state = 1;
					return true;
				}
				segment = segment.m_next;
				IL_C0:
				if (segment == null)
				{
					segment = null;
					return false;
				}
				pt = segment.m_state.m_first;
				goto IL_95;
			}

			// Token: 0x17001338 RID: 4920
			// (get) Token: 0x06007031 RID: 28721 RVA: 0x00182A60 File Offset: 0x00180C60
			T IEnumerator<T>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06007032 RID: 28722 RVA: 0x00182A68 File Offset: 0x00180C68
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001339 RID: 4921
			// (get) Token: 0x06007033 RID: 28723 RVA: 0x00182A6F File Offset: 0x00180C6F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04003708 RID: 14088
			private int <>1__state;

			// Token: 0x04003709 RID: 14089
			private T <>2__current;

			// Token: 0x0400370A RID: 14090
			public SingleProducerSingleConsumerQueue<T> <>4__this;

			// Token: 0x0400370B RID: 14091
			private SingleProducerSingleConsumerQueue<T>.Segment <segment>5__2;

			// Token: 0x0400370C RID: 14092
			private int <pt>5__3;
		}
	}
}
