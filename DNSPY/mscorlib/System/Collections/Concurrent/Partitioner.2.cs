using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x020004B4 RID: 1204
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class Partitioner
	{
		// Token: 0x060039A1 RID: 14753 RVA: 0x000DC6CE File Offset: 0x000DA8CE
		[__DynamicallyInvokable]
		public static OrderablePartitioner<TSource> Create<TSource>(IList<TSource> list, bool loadBalance)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (loadBalance)
			{
				return new Partitioner.DynamicPartitionerForIList<TSource>(list);
			}
			return new Partitioner.StaticIndexRangePartitionerForIList<TSource>(list);
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x000DC6EE File Offset: 0x000DA8EE
		[__DynamicallyInvokable]
		public static OrderablePartitioner<TSource> Create<TSource>(TSource[] array, bool loadBalance)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (loadBalance)
			{
				return new Partitioner.DynamicPartitionerForArray<TSource>(array);
			}
			return new Partitioner.StaticIndexRangePartitionerForArray<TSource>(array);
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x000DC70E File Offset: 0x000DA90E
		[__DynamicallyInvokable]
		public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source)
		{
			return Partitioner.Create<TSource>(source, EnumerablePartitionerOptions.None);
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x000DC717 File Offset: 0x000DA917
		[__DynamicallyInvokable]
		public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((partitionerOptions & ~EnumerablePartitionerOptions.NoBuffering) != EnumerablePartitionerOptions.None)
			{
				throw new ArgumentOutOfRangeException("partitionerOptions");
			}
			return new Partitioner.DynamicPartitionerForIEnumerable<TSource>(source, partitionerOptions);
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x000DC740 File Offset: 0x000DA940
		[__DynamicallyInvokable]
		public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive)
		{
			int num = 3;
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			long num2 = (toExclusive - fromInclusive) / (long)(PlatformHelper.ProcessorCount * num);
			if (num2 == 0L)
			{
				num2 = 1L;
			}
			return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, num2), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x000DC77F File Offset: 0x000DA97F
		[__DynamicallyInvokable]
		public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive, long rangeSize)
		{
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			if (rangeSize <= 0L)
			{
				throw new ArgumentOutOfRangeException("rangeSize");
			}
			return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000DC7AE File Offset: 0x000DA9AE
		private static IEnumerable<Tuple<long, long>> CreateRanges(long fromInclusive, long toExclusive, long rangeSize)
		{
			bool shouldQuit = false;
			long i = fromInclusive;
			while (i < toExclusive && !shouldQuit)
			{
				long num = i;
				long num2;
				try
				{
					num2 = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num2 = toExclusive;
					shouldQuit = true;
				}
				if (num2 > toExclusive)
				{
					num2 = toExclusive;
				}
				yield return new Tuple<long, long>(num, num2);
				i += rangeSize;
			}
			yield break;
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x000DC7CC File Offset: 0x000DA9CC
		[__DynamicallyInvokable]
		public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive)
		{
			int num = 3;
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			int num2 = (toExclusive - fromInclusive) / (PlatformHelper.ProcessorCount * num);
			if (num2 == 0)
			{
				num2 = 1;
			}
			return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, num2), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000DC809 File Offset: 0x000DAA09
		[__DynamicallyInvokable]
		public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive, int rangeSize)
		{
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			if (rangeSize <= 0)
			{
				throw new ArgumentOutOfRangeException("rangeSize");
			}
			return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x000DC837 File Offset: 0x000DAA37
		private static IEnumerable<Tuple<int, int>> CreateRanges(int fromInclusive, int toExclusive, int rangeSize)
		{
			bool shouldQuit = false;
			int i = fromInclusive;
			while (i < toExclusive && !shouldQuit)
			{
				int num = i;
				int num2;
				try
				{
					num2 = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num2 = toExclusive;
					shouldQuit = true;
				}
				if (num2 > toExclusive)
				{
					num2 = toExclusive;
				}
				yield return new Tuple<int, int>(num, num2);
				i += rangeSize;
			}
			yield break;
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x000DC858 File Offset: 0x000DAA58
		private static int GetDefaultChunkSize<TSource>()
		{
			int num;
			if (typeof(TSource).IsValueType)
			{
				if (typeof(TSource).StructLayoutAttribute.Value == LayoutKind.Explicit)
				{
					num = Math.Max(1, 512 / Marshal.SizeOf(typeof(TSource)));
				}
				else
				{
					num = 128;
				}
			}
			else
			{
				num = 512 / IntPtr.Size;
			}
			return num;
		}

		// Token: 0x04001933 RID: 6451
		private const int DEFAULT_BYTES_PER_CHUNK = 512;

		// Token: 0x02000BCE RID: 3022
		private abstract class DynamicPartitionEnumerator_Abstract<TSource, TSourceReader> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
		{
			// Token: 0x06006EA4 RID: 28324 RVA: 0x0017D903 File Offset: 0x0017BB03
			protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex)
				: this(sharedReader, sharedIndex, false)
			{
			}

			// Token: 0x06006EA5 RID: 28325 RVA: 0x0017D90E File Offset: 0x0017BB0E
			protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex, bool useSingleChunking)
			{
				this.m_sharedReader = sharedReader;
				this.m_sharedIndex = sharedIndex;
				this.m_maxChunkSize = (useSingleChunking ? 1 : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>.s_defaultMaxChunkSize);
			}

			// Token: 0x06006EA6 RID: 28326
			protected abstract bool GrabNextChunk(int requestedChunkSize);

			// Token: 0x170012EB RID: 4843
			// (get) Token: 0x06006EA7 RID: 28327
			// (set) Token: 0x06006EA8 RID: 28328
			protected abstract bool HasNoElementsLeft { get; set; }

			// Token: 0x170012EC RID: 4844
			// (get) Token: 0x06006EA9 RID: 28329
			public abstract KeyValuePair<long, TSource> Current { get; }

			// Token: 0x06006EAA RID: 28330
			public abstract void Dispose();

			// Token: 0x06006EAB RID: 28331 RVA: 0x0017D935 File Offset: 0x0017BB35
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170012ED RID: 4845
			// (get) Token: 0x06006EAC RID: 28332 RVA: 0x0017D93C File Offset: 0x0017BB3C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06006EAD RID: 28333 RVA: 0x0017D94C File Offset: 0x0017BB4C
			public bool MoveNext()
			{
				if (this.m_localOffset == null)
				{
					this.m_localOffset = new Partitioner.SharedInt(-1);
					this.m_currentChunkSize = new Partitioner.SharedInt(0);
					this.m_doublingCountdown = 3;
				}
				if (this.m_localOffset.Value < this.m_currentChunkSize.Value - 1)
				{
					this.m_localOffset.Value++;
					return true;
				}
				int num;
				if (this.m_currentChunkSize.Value == 0)
				{
					num = 1;
				}
				else if (this.m_doublingCountdown > 0)
				{
					num = this.m_currentChunkSize.Value;
				}
				else
				{
					num = Math.Min(this.m_currentChunkSize.Value * 2, this.m_maxChunkSize);
					this.m_doublingCountdown = 3;
				}
				this.m_doublingCountdown--;
				if (this.GrabNextChunk(num))
				{
					this.m_localOffset.Value = 0;
					return true;
				}
				return false;
			}

			// Token: 0x06006EAE RID: 28334 RVA: 0x0017DA2D File Offset: 0x0017BC2D
			// Note: this type is marked as 'beforefieldinit'.
			static DynamicPartitionEnumerator_Abstract()
			{
			}

			// Token: 0x040035C2 RID: 13762
			protected readonly TSourceReader m_sharedReader;

			// Token: 0x040035C3 RID: 13763
			protected static int s_defaultMaxChunkSize = Partitioner.GetDefaultChunkSize<TSource>();

			// Token: 0x040035C4 RID: 13764
			protected Partitioner.SharedInt m_currentChunkSize;

			// Token: 0x040035C5 RID: 13765
			protected Partitioner.SharedInt m_localOffset;

			// Token: 0x040035C6 RID: 13766
			private const int CHUNK_DOUBLING_RATE = 3;

			// Token: 0x040035C7 RID: 13767
			private int m_doublingCountdown;

			// Token: 0x040035C8 RID: 13768
			protected readonly int m_maxChunkSize;

			// Token: 0x040035C9 RID: 13769
			protected readonly Partitioner.SharedLong m_sharedIndex;
		}

		// Token: 0x02000BCF RID: 3023
		private class DynamicPartitionerForIEnumerable<TSource> : OrderablePartitioner<TSource>
		{
			// Token: 0x06006EAF RID: 28335 RVA: 0x0017DA39 File Offset: 0x0017BC39
			internal DynamicPartitionerForIEnumerable(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions)
				: base(true, false, true)
			{
				this.m_source = source;
				this.m_useSingleChunking = (partitionerOptions & EnumerablePartitionerOptions.NoBuffering) > EnumerablePartitionerOptions.None;
			}

			// Token: 0x06006EB0 RID: 28336 RVA: 0x0017DA58 File Offset: 0x0017BC58
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				IEnumerable<KeyValuePair<long, TSource>> enumerable = new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this.m_source.GetEnumerator(), this.m_useSingleChunking, true);
				for (int i = 0; i < partitionCount; i++)
				{
					array[i] = enumerable.GetEnumerator();
				}
				return array;
			}

			// Token: 0x06006EB1 RID: 28337 RVA: 0x0017DAA9 File Offset: 0x0017BCA9
			public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
			{
				return new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this.m_source.GetEnumerator(), this.m_useSingleChunking, false);
			}

			// Token: 0x170012EE RID: 4846
			// (get) Token: 0x06006EB2 RID: 28338 RVA: 0x0017DAC2 File Offset: 0x0017BCC2
			public override bool SupportsDynamicPartitions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x040035CA RID: 13770
			private IEnumerable<TSource> m_source;

			// Token: 0x040035CB RID: 13771
			private readonly bool m_useSingleChunking;

			// Token: 0x02000D09 RID: 3337
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable, IDisposable
			{
				// Token: 0x06007204 RID: 29188 RVA: 0x00188B1C File Offset: 0x00186D1C
				internal InternalPartitionEnumerable(IEnumerator<TSource> sharedReader, bool useSingleChunking, bool isStaticPartitioning)
				{
					this.m_sharedReader = sharedReader;
					this.m_sharedIndex = new Partitioner.SharedLong(-1L);
					this.m_hasNoElementsLeft = new Partitioner.SharedBool(false);
					this.m_sourceDepleted = new Partitioner.SharedBool(false);
					this.m_sharedLock = new object();
					this.m_useSingleChunking = useSingleChunking;
					if (!this.m_useSingleChunking)
					{
						int num = ((PlatformHelper.ProcessorCount > 4) ? 4 : 1);
						this.m_FillBuffer = new KeyValuePair<long, TSource>[num * Partitioner.GetDefaultChunkSize<TSource>()];
					}
					if (isStaticPartitioning)
					{
						this.m_activePartitionCount = new Partitioner.SharedInt(0);
						return;
					}
					this.m_activePartitionCount = null;
				}

				// Token: 0x06007205 RID: 29189 RVA: 0x00188BB0 File Offset: 0x00186DB0
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					if (this.m_disposed)
					{
						throw new ObjectDisposedException(Environment.GetResourceString("PartitionerStatic_CanNotCallGetEnumeratorAfterSourceHasBeenDisposed"));
					}
					return new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex, this.m_hasNoElementsLeft, this.m_sharedLock, this.m_activePartitionCount, this, this.m_useSingleChunking);
				}

				// Token: 0x06007206 RID: 29190 RVA: 0x00188BFF File Offset: 0x00186DFF
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06007207 RID: 29191 RVA: 0x00188C08 File Offset: 0x00186E08
				private void TryCopyFromFillBuffer(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					actualNumElementsGrabbed = 0;
					KeyValuePair<long, TSource>[] fillBuffer = this.m_FillBuffer;
					if (fillBuffer == null)
					{
						return;
					}
					if (this.m_FillBufferCurrentPosition >= this.m_FillBufferSize)
					{
						return;
					}
					Interlocked.Increment(ref this.m_activeCopiers);
					int num = Interlocked.Add(ref this.m_FillBufferCurrentPosition, requestedChunkSize);
					int num2 = num - requestedChunkSize;
					if (num2 < this.m_FillBufferSize)
					{
						actualNumElementsGrabbed = ((num < this.m_FillBufferSize) ? num : (this.m_FillBufferSize - num2));
						Array.Copy(fillBuffer, num2, destArray, 0, actualNumElementsGrabbed);
					}
					Interlocked.Decrement(ref this.m_activeCopiers);
				}

				// Token: 0x06007208 RID: 29192 RVA: 0x00188C91 File Offset: 0x00186E91
				internal bool GrabChunk(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					actualNumElementsGrabbed = 0;
					if (this.m_hasNoElementsLeft.Value)
					{
						return false;
					}
					if (this.m_useSingleChunking)
					{
						return this.GrabChunk_Single(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
					}
					return this.GrabChunk_Buffered(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
				}

				// Token: 0x06007209 RID: 29193 RVA: 0x00188CC4 File Offset: 0x00186EC4
				internal bool GrabChunk_Single(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					object sharedLock = this.m_sharedLock;
					bool flag2;
					lock (sharedLock)
					{
						if (this.m_hasNoElementsLeft.Value)
						{
							flag2 = false;
						}
						else
						{
							try
							{
								if (this.m_sharedReader.MoveNext())
								{
									this.m_sharedIndex.Value = checked(this.m_sharedIndex.Value + 1L);
									destArray[0] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
									actualNumElementsGrabbed = 1;
									flag2 = true;
								}
								else
								{
									this.m_sourceDepleted.Value = true;
									this.m_hasNoElementsLeft.Value = true;
									flag2 = false;
								}
							}
							catch
							{
								this.m_sourceDepleted.Value = true;
								this.m_hasNoElementsLeft.Value = true;
								throw;
							}
						}
					}
					return flag2;
				}

				// Token: 0x0600720A RID: 29194 RVA: 0x00188DB0 File Offset: 0x00186FB0
				internal bool GrabChunk_Buffered(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					this.TryCopyFromFillBuffer(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
					if (actualNumElementsGrabbed == requestedChunkSize)
					{
						return true;
					}
					if (this.m_sourceDepleted.Value)
					{
						this.m_hasNoElementsLeft.Value = true;
						this.m_FillBuffer = null;
						return actualNumElementsGrabbed > 0;
					}
					object sharedLock = this.m_sharedLock;
					lock (sharedLock)
					{
						if (this.m_sourceDepleted.Value)
						{
							return actualNumElementsGrabbed > 0;
						}
						try
						{
							if (this.m_activeCopiers > 0)
							{
								SpinWait spinWait = default(SpinWait);
								while (this.m_activeCopiers > 0)
								{
									spinWait.SpinOnce();
								}
							}
							while (actualNumElementsGrabbed < requestedChunkSize)
							{
								if (!this.m_sharedReader.MoveNext())
								{
									this.m_sourceDepleted.Value = true;
									break;
								}
								this.m_sharedIndex.Value = checked(this.m_sharedIndex.Value + 1L);
								destArray[actualNumElementsGrabbed] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
								actualNumElementsGrabbed++;
							}
							KeyValuePair<long, TSource>[] fillBuffer = this.m_FillBuffer;
							if (!this.m_sourceDepleted.Value && fillBuffer != null && this.m_FillBufferCurrentPosition >= fillBuffer.Length)
							{
								for (int i = 0; i < fillBuffer.Length; i++)
								{
									if (!this.m_sharedReader.MoveNext())
									{
										this.m_sourceDepleted.Value = true;
										this.m_FillBufferSize = i;
										break;
									}
									this.m_sharedIndex.Value = checked(this.m_sharedIndex.Value + 1L);
									fillBuffer[i] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
								}
								this.m_FillBufferCurrentPosition = 0;
							}
						}
						catch
						{
							this.m_sourceDepleted.Value = true;
							this.m_hasNoElementsLeft.Value = true;
							throw;
						}
					}
					return actualNumElementsGrabbed > 0;
				}

				// Token: 0x0600720B RID: 29195 RVA: 0x00188FCC File Offset: 0x001871CC
				public void Dispose()
				{
					if (!this.m_disposed)
					{
						this.m_disposed = true;
						this.m_sharedReader.Dispose();
					}
				}

				// Token: 0x0400394A RID: 14666
				private readonly IEnumerator<TSource> m_sharedReader;

				// Token: 0x0400394B RID: 14667
				private Partitioner.SharedLong m_sharedIndex;

				// Token: 0x0400394C RID: 14668
				private volatile KeyValuePair<long, TSource>[] m_FillBuffer;

				// Token: 0x0400394D RID: 14669
				private volatile int m_FillBufferSize;

				// Token: 0x0400394E RID: 14670
				private volatile int m_FillBufferCurrentPosition;

				// Token: 0x0400394F RID: 14671
				private volatile int m_activeCopiers;

				// Token: 0x04003950 RID: 14672
				private Partitioner.SharedBool m_hasNoElementsLeft;

				// Token: 0x04003951 RID: 14673
				private Partitioner.SharedBool m_sourceDepleted;

				// Token: 0x04003952 RID: 14674
				private object m_sharedLock;

				// Token: 0x04003953 RID: 14675
				private bool m_disposed;

				// Token: 0x04003954 RID: 14676
				private Partitioner.SharedInt m_activePartitionCount;

				// Token: 0x04003955 RID: 14677
				private readonly bool m_useSingleChunking;
			}

			// Token: 0x02000D0A RID: 3338
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, IEnumerator<TSource>>
			{
				// Token: 0x0600720C RID: 29196 RVA: 0x00188FE8 File Offset: 0x001871E8
				internal InternalPartitionEnumerator(IEnumerator<TSource> sharedReader, Partitioner.SharedLong sharedIndex, Partitioner.SharedBool hasNoElementsLeft, object sharedLock, Partitioner.SharedInt activePartitionCount, Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable enumerable, bool useSingleChunking)
					: base(sharedReader, sharedIndex, useSingleChunking)
				{
					this.m_hasNoElementsLeft = hasNoElementsLeft;
					this.m_sharedLock = sharedLock;
					this.m_enumerable = enumerable;
					this.m_activePartitionCount = activePartitionCount;
					if (this.m_activePartitionCount != null)
					{
						Interlocked.Increment(ref this.m_activePartitionCount.Value);
					}
				}

				// Token: 0x0600720D RID: 29197 RVA: 0x00189038 File Offset: 0x00187238
				protected override bool GrabNextChunk(int requestedChunkSize)
				{
					if (this.HasNoElementsLeft)
					{
						return false;
					}
					if (this.m_localList == null)
					{
						this.m_localList = new KeyValuePair<long, TSource>[this.m_maxChunkSize];
					}
					return this.m_enumerable.GrabChunk(this.m_localList, requestedChunkSize, ref this.m_currentChunkSize.Value);
				}

				// Token: 0x17001386 RID: 4998
				// (get) Token: 0x0600720E RID: 29198 RVA: 0x00189085 File Offset: 0x00187285
				// (set) Token: 0x0600720F RID: 29199 RVA: 0x00189094 File Offset: 0x00187294
				protected override bool HasNoElementsLeft
				{
					get
					{
						return this.m_hasNoElementsLeft.Value;
					}
					set
					{
						this.m_hasNoElementsLeft.Value = true;
					}
				}

				// Token: 0x17001387 RID: 4999
				// (get) Token: 0x06007210 RID: 29200 RVA: 0x001890A4 File Offset: 0x001872A4
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this.m_currentChunkSize == null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
						}
						return this.m_localList[this.m_localOffset.Value];
					}
				}

				// Token: 0x06007211 RID: 29201 RVA: 0x001890D6 File Offset: 0x001872D6
				public override void Dispose()
				{
					if (this.m_activePartitionCount != null && Interlocked.Decrement(ref this.m_activePartitionCount.Value) == 0)
					{
						this.m_enumerable.Dispose();
					}
				}

				// Token: 0x04003956 RID: 14678
				private KeyValuePair<long, TSource>[] m_localList;

				// Token: 0x04003957 RID: 14679
				private readonly Partitioner.SharedBool m_hasNoElementsLeft;

				// Token: 0x04003958 RID: 14680
				private readonly object m_sharedLock;

				// Token: 0x04003959 RID: 14681
				private readonly Partitioner.SharedInt m_activePartitionCount;

				// Token: 0x0400395A RID: 14682
				private Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable m_enumerable;
			}
		}

		// Token: 0x02000BD0 RID: 3024
		private abstract class DynamicPartitionerForIndexRange_Abstract<TSource, TCollection> : OrderablePartitioner<TSource>
		{
			// Token: 0x06006EB3 RID: 28339 RVA: 0x0017DAC5 File Offset: 0x0017BCC5
			protected DynamicPartitionerForIndexRange_Abstract(TCollection data)
				: base(true, false, true)
			{
				this.m_data = data;
			}

			// Token: 0x06006EB4 RID: 28340
			protected abstract IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TCollection data);

			// Token: 0x06006EB5 RID: 28341 RVA: 0x0017DAD8 File Offset: 0x0017BCD8
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				IEnumerable<KeyValuePair<long, TSource>> orderableDynamicPartitions_Factory = this.GetOrderableDynamicPartitions_Factory(this.m_data);
				for (int i = 0; i < partitionCount; i++)
				{
					array[i] = orderableDynamicPartitions_Factory.GetEnumerator();
				}
				return array;
			}

			// Token: 0x06006EB6 RID: 28342 RVA: 0x0017DB1E File Offset: 0x0017BD1E
			public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
			{
				return this.GetOrderableDynamicPartitions_Factory(this.m_data);
			}

			// Token: 0x170012EF RID: 4847
			// (get) Token: 0x06006EB7 RID: 28343 RVA: 0x0017DB2C File Offset: 0x0017BD2C
			public override bool SupportsDynamicPartitions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x040035CC RID: 13772
			private TCollection m_data;
		}

		// Token: 0x02000BD1 RID: 3025
		private abstract class DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSourceReader> : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>
		{
			// Token: 0x06006EB8 RID: 28344 RVA: 0x0017DB2F File Offset: 0x0017BD2F
			protected DynamicPartitionEnumeratorForIndexRange_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex)
				: base(sharedReader, sharedIndex)
			{
			}

			// Token: 0x170012F0 RID: 4848
			// (get) Token: 0x06006EB9 RID: 28345
			protected abstract int SourceCount { get; }

			// Token: 0x06006EBA RID: 28346 RVA: 0x0017DB3C File Offset: 0x0017BD3C
			protected override bool GrabNextChunk(int requestedChunkSize)
			{
				while (!this.HasNoElementsLeft)
				{
					long num = Volatile.Read(ref this.m_sharedIndex.Value);
					if (this.HasNoElementsLeft)
					{
						return false;
					}
					long num2 = Math.Min((long)(this.SourceCount - 1), num + (long)requestedChunkSize);
					if (Interlocked.CompareExchange(ref this.m_sharedIndex.Value, num2, num) == num)
					{
						this.m_currentChunkSize.Value = (int)(num2 - num);
						this.m_localOffset.Value = -1;
						this.m_startIndex = (int)(num + 1L);
						return true;
					}
				}
				return false;
			}

			// Token: 0x170012F1 RID: 4849
			// (get) Token: 0x06006EBB RID: 28347 RVA: 0x0017DBC3 File Offset: 0x0017BDC3
			// (set) Token: 0x06006EBC RID: 28348 RVA: 0x0017DBE3 File Offset: 0x0017BDE3
			protected override bool HasNoElementsLeft
			{
				get
				{
					return Volatile.Read(ref this.m_sharedIndex.Value) >= (long)(this.SourceCount - 1);
				}
				set
				{
				}
			}

			// Token: 0x06006EBD RID: 28349 RVA: 0x0017DBE5 File Offset: 0x0017BDE5
			public override void Dispose()
			{
			}

			// Token: 0x040035CD RID: 13773
			protected int m_startIndex;
		}

		// Token: 0x02000BD2 RID: 3026
		private class DynamicPartitionerForIList<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, IList<TSource>>
		{
			// Token: 0x06006EBE RID: 28350 RVA: 0x0017DBE7 File Offset: 0x0017BDE7
			internal DynamicPartitionerForIList(IList<TSource> source)
				: base(source)
			{
			}

			// Token: 0x06006EBF RID: 28351 RVA: 0x0017DBF0 File Offset: 0x0017BDF0
			protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(IList<TSource> m_data)
			{
				return new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerable(m_data);
			}

			// Token: 0x02000D0B RID: 3339
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
			{
				// Token: 0x06007212 RID: 29202 RVA: 0x001890FD File Offset: 0x001872FD
				internal InternalPartitionEnumerable(IList<TSource> sharedReader)
				{
					this.m_sharedReader = sharedReader;
					this.m_sharedIndex = new Partitioner.SharedLong(-1L);
				}

				// Token: 0x06007213 RID: 29203 RVA: 0x00189119 File Offset: 0x00187319
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					return new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex);
				}

				// Token: 0x06007214 RID: 29204 RVA: 0x0018912C File Offset: 0x0018732C
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x0400395B RID: 14683
				private readonly IList<TSource> m_sharedReader;

				// Token: 0x0400395C RID: 14684
				private Partitioner.SharedLong m_sharedIndex;
			}

			// Token: 0x02000D0C RID: 3340
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, IList<TSource>>
			{
				// Token: 0x06007215 RID: 29205 RVA: 0x00189134 File Offset: 0x00187334
				internal InternalPartitionEnumerator(IList<TSource> sharedReader, Partitioner.SharedLong sharedIndex)
					: base(sharedReader, sharedIndex)
				{
				}

				// Token: 0x17001388 RID: 5000
				// (get) Token: 0x06007216 RID: 29206 RVA: 0x0018913E File Offset: 0x0018733E
				protected override int SourceCount
				{
					get
					{
						return this.m_sharedReader.Count;
					}
				}

				// Token: 0x17001389 RID: 5001
				// (get) Token: 0x06007217 RID: 29207 RVA: 0x0018914C File Offset: 0x0018734C
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this.m_currentChunkSize == null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
						}
						return new KeyValuePair<long, TSource>((long)(this.m_startIndex + this.m_localOffset.Value), this.m_sharedReader[this.m_startIndex + this.m_localOffset.Value]);
					}
				}
			}
		}

		// Token: 0x02000BD3 RID: 3027
		private class DynamicPartitionerForArray<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, TSource[]>
		{
			// Token: 0x06006EC0 RID: 28352 RVA: 0x0017DBF8 File Offset: 0x0017BDF8
			internal DynamicPartitionerForArray(TSource[] source)
				: base(source)
			{
			}

			// Token: 0x06006EC1 RID: 28353 RVA: 0x0017DC01 File Offset: 0x0017BE01
			protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TSource[] m_data)
			{
				return new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerable(m_data);
			}

			// Token: 0x02000D0D RID: 3341
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
			{
				// Token: 0x06007218 RID: 29208 RVA: 0x001891AA File Offset: 0x001873AA
				internal InternalPartitionEnumerable(TSource[] sharedReader)
				{
					this.m_sharedReader = sharedReader;
					this.m_sharedIndex = new Partitioner.SharedLong(-1L);
				}

				// Token: 0x06007219 RID: 29209 RVA: 0x001891C6 File Offset: 0x001873C6
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x0600721A RID: 29210 RVA: 0x001891CE File Offset: 0x001873CE
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					return new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex);
				}

				// Token: 0x0400395D RID: 14685
				private readonly TSource[] m_sharedReader;

				// Token: 0x0400395E RID: 14686
				private Partitioner.SharedLong m_sharedIndex;
			}

			// Token: 0x02000D0E RID: 3342
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSource[]>
			{
				// Token: 0x0600721B RID: 29211 RVA: 0x001891E1 File Offset: 0x001873E1
				internal InternalPartitionEnumerator(TSource[] sharedReader, Partitioner.SharedLong sharedIndex)
					: base(sharedReader, sharedIndex)
				{
				}

				// Token: 0x1700138A RID: 5002
				// (get) Token: 0x0600721C RID: 29212 RVA: 0x001891EB File Offset: 0x001873EB
				protected override int SourceCount
				{
					get
					{
						return this.m_sharedReader.Length;
					}
				}

				// Token: 0x1700138B RID: 5003
				// (get) Token: 0x0600721D RID: 29213 RVA: 0x001891F8 File Offset: 0x001873F8
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this.m_currentChunkSize == null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
						}
						return new KeyValuePair<long, TSource>((long)(this.m_startIndex + this.m_localOffset.Value), this.m_sharedReader[this.m_startIndex + this.m_localOffset.Value]);
					}
				}
			}
		}

		// Token: 0x02000BD4 RID: 3028
		private abstract class StaticIndexRangePartitioner<TSource, TCollection> : OrderablePartitioner<TSource>
		{
			// Token: 0x06006EC2 RID: 28354 RVA: 0x0017DC09 File Offset: 0x0017BE09
			protected StaticIndexRangePartitioner()
				: base(true, true, true)
			{
			}

			// Token: 0x170012F2 RID: 4850
			// (get) Token: 0x06006EC3 RID: 28355
			protected abstract int SourceCount { get; }

			// Token: 0x06006EC4 RID: 28356
			protected abstract IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex);

			// Token: 0x06006EC5 RID: 28357 RVA: 0x0017DC14 File Offset: 0x0017BE14
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				int num2;
				int num = Math.DivRem(this.SourceCount, partitionCount, out num2);
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				int num3 = -1;
				for (int i = 0; i < partitionCount; i++)
				{
					int num4 = num3 + 1;
					if (i < num2)
					{
						num3 = num4 + num;
					}
					else
					{
						num3 = num4 + num - 1;
					}
					array[i] = this.CreatePartition(num4, num3);
				}
				return array;
			}
		}

		// Token: 0x02000BD5 RID: 3029
		private abstract class StaticIndexRangePartition<TSource> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
		{
			// Token: 0x06006EC6 RID: 28358 RVA: 0x0017DC7E File Offset: 0x0017BE7E
			protected StaticIndexRangePartition(int startIndex, int endIndex)
			{
				this.m_startIndex = startIndex;
				this.m_endIndex = endIndex;
				this.m_offset = startIndex - 1;
			}

			// Token: 0x170012F3 RID: 4851
			// (get) Token: 0x06006EC7 RID: 28359
			public abstract KeyValuePair<long, TSource> Current { get; }

			// Token: 0x06006EC8 RID: 28360 RVA: 0x0017DC9F File Offset: 0x0017BE9F
			public void Dispose()
			{
			}

			// Token: 0x06006EC9 RID: 28361 RVA: 0x0017DCA1 File Offset: 0x0017BEA1
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06006ECA RID: 28362 RVA: 0x0017DCA8 File Offset: 0x0017BEA8
			public bool MoveNext()
			{
				if (this.m_offset < this.m_endIndex)
				{
					this.m_offset++;
					return true;
				}
				this.m_offset = this.m_endIndex + 1;
				return false;
			}

			// Token: 0x170012F4 RID: 4852
			// (get) Token: 0x06006ECB RID: 28363 RVA: 0x0017DCDF File Offset: 0x0017BEDF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040035CE RID: 13774
			protected readonly int m_startIndex;

			// Token: 0x040035CF RID: 13775
			protected readonly int m_endIndex;

			// Token: 0x040035D0 RID: 13776
			protected volatile int m_offset;
		}

		// Token: 0x02000BD6 RID: 3030
		private class StaticIndexRangePartitionerForIList<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, IList<TSource>>
		{
			// Token: 0x06006ECC RID: 28364 RVA: 0x0017DCEC File Offset: 0x0017BEEC
			internal StaticIndexRangePartitionerForIList(IList<TSource> list)
			{
				this.m_list = list;
			}

			// Token: 0x170012F5 RID: 4853
			// (get) Token: 0x06006ECD RID: 28365 RVA: 0x0017DCFB File Offset: 0x0017BEFB
			protected override int SourceCount
			{
				get
				{
					return this.m_list.Count;
				}
			}

			// Token: 0x06006ECE RID: 28366 RVA: 0x0017DD08 File Offset: 0x0017BF08
			protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
			{
				return new Partitioner.StaticIndexRangePartitionForIList<TSource>(this.m_list, startIndex, endIndex);
			}

			// Token: 0x040035D1 RID: 13777
			private IList<TSource> m_list;
		}

		// Token: 0x02000BD7 RID: 3031
		private class StaticIndexRangePartitionForIList<TSource> : Partitioner.StaticIndexRangePartition<TSource>
		{
			// Token: 0x06006ECF RID: 28367 RVA: 0x0017DD17 File Offset: 0x0017BF17
			internal StaticIndexRangePartitionForIList(IList<TSource> list, int startIndex, int endIndex)
				: base(startIndex, endIndex)
			{
				this.m_list = list;
			}

			// Token: 0x170012F6 RID: 4854
			// (get) Token: 0x06006ED0 RID: 28368 RVA: 0x0017DD2C File Offset: 0x0017BF2C
			public override KeyValuePair<long, TSource> Current
			{
				get
				{
					if (this.m_offset < this.m_startIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
					}
					return new KeyValuePair<long, TSource>((long)this.m_offset, this.m_list[this.m_offset]);
				}
			}

			// Token: 0x040035D2 RID: 13778
			private volatile IList<TSource> m_list;
		}

		// Token: 0x02000BD8 RID: 3032
		private class StaticIndexRangePartitionerForArray<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, TSource[]>
		{
			// Token: 0x06006ED1 RID: 28369 RVA: 0x0017DD7C File Offset: 0x0017BF7C
			internal StaticIndexRangePartitionerForArray(TSource[] array)
			{
				this.m_array = array;
			}

			// Token: 0x170012F7 RID: 4855
			// (get) Token: 0x06006ED2 RID: 28370 RVA: 0x0017DD8B File Offset: 0x0017BF8B
			protected override int SourceCount
			{
				get
				{
					return this.m_array.Length;
				}
			}

			// Token: 0x06006ED3 RID: 28371 RVA: 0x0017DD95 File Offset: 0x0017BF95
			protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
			{
				return new Partitioner.StaticIndexRangePartitionForArray<TSource>(this.m_array, startIndex, endIndex);
			}

			// Token: 0x040035D3 RID: 13779
			private TSource[] m_array;
		}

		// Token: 0x02000BD9 RID: 3033
		private class StaticIndexRangePartitionForArray<TSource> : Partitioner.StaticIndexRangePartition<TSource>
		{
			// Token: 0x06006ED4 RID: 28372 RVA: 0x0017DDA4 File Offset: 0x0017BFA4
			internal StaticIndexRangePartitionForArray(TSource[] array, int startIndex, int endIndex)
				: base(startIndex, endIndex)
			{
				this.m_array = array;
			}

			// Token: 0x170012F8 RID: 4856
			// (get) Token: 0x06006ED5 RID: 28373 RVA: 0x0017DDB8 File Offset: 0x0017BFB8
			public override KeyValuePair<long, TSource> Current
			{
				get
				{
					if (this.m_offset < this.m_startIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
					}
					return new KeyValuePair<long, TSource>((long)this.m_offset, this.m_array[this.m_offset]);
				}
			}

			// Token: 0x040035D4 RID: 13780
			private volatile TSource[] m_array;
		}

		// Token: 0x02000BDA RID: 3034
		private class SharedInt
		{
			// Token: 0x06006ED6 RID: 28374 RVA: 0x0017DE08 File Offset: 0x0017C008
			internal SharedInt(int value)
			{
				this.Value = value;
			}

			// Token: 0x040035D5 RID: 13781
			internal volatile int Value;
		}

		// Token: 0x02000BDB RID: 3035
		private class SharedBool
		{
			// Token: 0x06006ED7 RID: 28375 RVA: 0x0017DE19 File Offset: 0x0017C019
			internal SharedBool(bool value)
			{
				this.Value = value;
			}

			// Token: 0x040035D6 RID: 13782
			internal volatile bool Value;
		}

		// Token: 0x02000BDC RID: 3036
		private class SharedLong
		{
			// Token: 0x06006ED8 RID: 28376 RVA: 0x0017DE2A File Offset: 0x0017C02A
			internal SharedLong(long value)
			{
				this.Value = value;
			}

			// Token: 0x040035D7 RID: 13783
			internal long Value;
		}

		// Token: 0x02000BDD RID: 3037
		[CompilerGenerated]
		private sealed class <CreateRanges>d__6 : IEnumerable<Tuple<long, long>>, IEnumerable, IEnumerator<Tuple<long, long>>, IDisposable, IEnumerator
		{
			// Token: 0x06006ED9 RID: 28377 RVA: 0x0017DE39 File Offset: 0x0017C039
			[DebuggerHidden]
			public <CreateRanges>d__6(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06006EDA RID: 28378 RVA: 0x0017DE53 File Offset: 0x0017C053
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06006EDB RID: 28379 RVA: 0x0017DE58 File Offset: 0x0017C058
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
					i += rangeSize;
				}
				else
				{
					this.<>1__state = -1;
					shouldQuit = false;
					i = fromInclusive;
				}
				if (i >= toExclusive || shouldQuit)
				{
					return false;
				}
				long num2 = i;
				long num3;
				try
				{
					num3 = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num3 = toExclusive;
					shouldQuit = true;
				}
				if (num3 > toExclusive)
				{
					num3 = toExclusive;
				}
				this.<>2__current = new Tuple<long, long>(num2, num3);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170012F9 RID: 4857
			// (get) Token: 0x06006EDC RID: 28380 RVA: 0x0017DF20 File Offset: 0x0017C120
			Tuple<long, long> IEnumerator<Tuple<long, long>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006EDD RID: 28381 RVA: 0x0017DF28 File Offset: 0x0017C128
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170012FA RID: 4858
			// (get) Token: 0x06006EDE RID: 28382 RVA: 0x0017DF2F File Offset: 0x0017C12F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006EDF RID: 28383 RVA: 0x0017DF38 File Offset: 0x0017C138
			[DebuggerHidden]
			IEnumerator<Tuple<long, long>> IEnumerable<Tuple<long, long>>.GetEnumerator()
			{
				Partitioner.<CreateRanges>d__6 <CreateRanges>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<CreateRanges>d__ = this;
				}
				else
				{
					<CreateRanges>d__ = new Partitioner.<CreateRanges>d__6(0);
				}
				<CreateRanges>d__.fromInclusive = fromInclusive;
				<CreateRanges>d__.toExclusive = toExclusive;
				<CreateRanges>d__.rangeSize = rangeSize;
				return <CreateRanges>d__;
			}

			// Token: 0x06006EE0 RID: 28384 RVA: 0x0017DF93 File Offset: 0x0017C193
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Tuple<System.Int64,System.Int64>>.GetEnumerator();
			}

			// Token: 0x040035D8 RID: 13784
			private int <>1__state;

			// Token: 0x040035D9 RID: 13785
			private Tuple<long, long> <>2__current;

			// Token: 0x040035DA RID: 13786
			private int <>l__initialThreadId;

			// Token: 0x040035DB RID: 13787
			private long fromInclusive;

			// Token: 0x040035DC RID: 13788
			public long <>3__fromInclusive;

			// Token: 0x040035DD RID: 13789
			private long rangeSize;

			// Token: 0x040035DE RID: 13790
			public long <>3__rangeSize;

			// Token: 0x040035DF RID: 13791
			private long toExclusive;

			// Token: 0x040035E0 RID: 13792
			public long <>3__toExclusive;

			// Token: 0x040035E1 RID: 13793
			private bool <shouldQuit>5__2;

			// Token: 0x040035E2 RID: 13794
			private long <i>5__3;
		}

		// Token: 0x02000BDE RID: 3038
		[CompilerGenerated]
		private sealed class <CreateRanges>d__9 : IEnumerable<Tuple<int, int>>, IEnumerable, IEnumerator<Tuple<int, int>>, IDisposable, IEnumerator
		{
			// Token: 0x06006EE1 RID: 28385 RVA: 0x0017DF9B File Offset: 0x0017C19B
			[DebuggerHidden]
			public <CreateRanges>d__9(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06006EE2 RID: 28386 RVA: 0x0017DFB5 File Offset: 0x0017C1B5
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06006EE3 RID: 28387 RVA: 0x0017DFB8 File Offset: 0x0017C1B8
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
					i += rangeSize;
				}
				else
				{
					this.<>1__state = -1;
					shouldQuit = false;
					i = fromInclusive;
				}
				if (i >= toExclusive || shouldQuit)
				{
					return false;
				}
				int num2 = i;
				int num3;
				try
				{
					num3 = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num3 = toExclusive;
					shouldQuit = true;
				}
				if (num3 > toExclusive)
				{
					num3 = toExclusive;
				}
				this.<>2__current = new Tuple<int, int>(num2, num3);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170012FB RID: 4859
			// (get) Token: 0x06006EE4 RID: 28388 RVA: 0x0017E080 File Offset: 0x0017C280
			Tuple<int, int> IEnumerator<Tuple<int, int>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006EE5 RID: 28389 RVA: 0x0017E088 File Offset: 0x0017C288
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170012FC RID: 4860
			// (get) Token: 0x06006EE6 RID: 28390 RVA: 0x0017E08F File Offset: 0x0017C28F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006EE7 RID: 28391 RVA: 0x0017E098 File Offset: 0x0017C298
			[DebuggerHidden]
			IEnumerator<Tuple<int, int>> IEnumerable<Tuple<int, int>>.GetEnumerator()
			{
				Partitioner.<CreateRanges>d__9 <CreateRanges>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<CreateRanges>d__ = this;
				}
				else
				{
					<CreateRanges>d__ = new Partitioner.<CreateRanges>d__9(0);
				}
				<CreateRanges>d__.fromInclusive = fromInclusive;
				<CreateRanges>d__.toExclusive = toExclusive;
				<CreateRanges>d__.rangeSize = rangeSize;
				return <CreateRanges>d__;
			}

			// Token: 0x06006EE8 RID: 28392 RVA: 0x0017E0F3 File Offset: 0x0017C2F3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Tuple<System.Int32,System.Int32>>.GetEnumerator();
			}

			// Token: 0x040035E3 RID: 13795
			private int <>1__state;

			// Token: 0x040035E4 RID: 13796
			private Tuple<int, int> <>2__current;

			// Token: 0x040035E5 RID: 13797
			private int <>l__initialThreadId;

			// Token: 0x040035E6 RID: 13798
			private int fromInclusive;

			// Token: 0x040035E7 RID: 13799
			public int <>3__fromInclusive;

			// Token: 0x040035E8 RID: 13800
			private int rangeSize;

			// Token: 0x040035E9 RID: 13801
			public int <>3__rangeSize;

			// Token: 0x040035EA RID: 13802
			private int toExclusive;

			// Token: 0x040035EB RID: 13803
			public int <>3__toExclusive;

			// Token: 0x040035EC RID: 13804
			private bool <shouldQuit>5__2;

			// Token: 0x040035ED RID: 13805
			private int <i>5__3;
		}
	}
}
