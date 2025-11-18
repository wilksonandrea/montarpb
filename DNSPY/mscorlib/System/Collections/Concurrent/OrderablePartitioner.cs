using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Collections.Concurrent
{
	// Token: 0x020004B2 RID: 1202
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public abstract class OrderablePartitioner<TSource> : Partitioner<TSource>
	{
		// Token: 0x06003996 RID: 14742 RVA: 0x000DC606 File Offset: 0x000DA806
		[__DynamicallyInvokable]
		protected OrderablePartitioner(bool keysOrderedInEachPartition, bool keysOrderedAcrossPartitions, bool keysNormalized)
		{
			this.KeysOrderedInEachPartition = keysOrderedInEachPartition;
			this.KeysOrderedAcrossPartitions = keysOrderedAcrossPartitions;
			this.KeysNormalized = keysNormalized;
		}

		// Token: 0x06003997 RID: 14743
		[__DynamicallyInvokable]
		public abstract IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount);

		// Token: 0x06003998 RID: 14744 RVA: 0x000DC623 File Offset: 0x000DA823
		[__DynamicallyInvokable]
		public virtual IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
		{
			throw new NotSupportedException(Environment.GetResourceString("Partitioner_DynamicPartitionsNotSupported"));
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x000DC634 File Offset: 0x000DA834
		// (set) Token: 0x0600399A RID: 14746 RVA: 0x000DC63C File Offset: 0x000DA83C
		[__DynamicallyInvokable]
		public bool KeysOrderedInEachPartition
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<KeysOrderedInEachPartition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<KeysOrderedInEachPartition>k__BackingField = value;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600399B RID: 14747 RVA: 0x000DC645 File Offset: 0x000DA845
		// (set) Token: 0x0600399C RID: 14748 RVA: 0x000DC64D File Offset: 0x000DA84D
		[__DynamicallyInvokable]
		public bool KeysOrderedAcrossPartitions
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<KeysOrderedAcrossPartitions>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<KeysOrderedAcrossPartitions>k__BackingField = value;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x000DC656 File Offset: 0x000DA856
		// (set) Token: 0x0600399E RID: 14750 RVA: 0x000DC65E File Offset: 0x000DA85E
		[__DynamicallyInvokable]
		public bool KeysNormalized
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<KeysNormalized>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<KeysNormalized>k__BackingField = value;
			}
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x000DC668 File Offset: 0x000DA868
		[__DynamicallyInvokable]
		public override IList<IEnumerator<TSource>> GetPartitions(int partitionCount)
		{
			IList<IEnumerator<KeyValuePair<long, TSource>>> orderablePartitions = this.GetOrderablePartitions(partitionCount);
			if (orderablePartitions.Count != partitionCount)
			{
				throw new InvalidOperationException("OrderablePartitioner_GetPartitions_WrongNumberOfPartitions");
			}
			IEnumerator<TSource>[] array = new IEnumerator<TSource>[partitionCount];
			for (int i = 0; i < partitionCount; i++)
			{
				array[i] = new OrderablePartitioner<TSource>.EnumeratorDropIndices(orderablePartitions[i]);
			}
			return array;
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000DC6B4 File Offset: 0x000DA8B4
		[__DynamicallyInvokable]
		public override IEnumerable<TSource> GetDynamicPartitions()
		{
			IEnumerable<KeyValuePair<long, TSource>> orderableDynamicPartitions = this.GetOrderableDynamicPartitions();
			return new OrderablePartitioner<TSource>.EnumerableDropIndices(orderableDynamicPartitions);
		}

		// Token: 0x0400192D RID: 6445
		[CompilerGenerated]
		private bool <KeysOrderedInEachPartition>k__BackingField;

		// Token: 0x0400192E RID: 6446
		[CompilerGenerated]
		private bool <KeysOrderedAcrossPartitions>k__BackingField;

		// Token: 0x0400192F RID: 6447
		[CompilerGenerated]
		private bool <KeysNormalized>k__BackingField;

		// Token: 0x02000BCC RID: 3020
		private class EnumerableDropIndices : IEnumerable<TSource>, IEnumerable, IDisposable
		{
			// Token: 0x06006E9A RID: 28314 RVA: 0x0017D853 File Offset: 0x0017BA53
			public EnumerableDropIndices(IEnumerable<KeyValuePair<long, TSource>> source)
			{
				this.m_source = source;
			}

			// Token: 0x06006E9B RID: 28315 RVA: 0x0017D862 File Offset: 0x0017BA62
			public IEnumerator<TSource> GetEnumerator()
			{
				return new OrderablePartitioner<TSource>.EnumeratorDropIndices(this.m_source.GetEnumerator());
			}

			// Token: 0x06006E9C RID: 28316 RVA: 0x0017D874 File Offset: 0x0017BA74
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06006E9D RID: 28317 RVA: 0x0017D87C File Offset: 0x0017BA7C
			public void Dispose()
			{
				IDisposable disposable = this.m_source as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x040035C0 RID: 13760
			private readonly IEnumerable<KeyValuePair<long, TSource>> m_source;
		}

		// Token: 0x02000BCD RID: 3021
		private class EnumeratorDropIndices : IEnumerator<TSource>, IDisposable, IEnumerator
		{
			// Token: 0x06006E9E RID: 28318 RVA: 0x0017D89E File Offset: 0x0017BA9E
			public EnumeratorDropIndices(IEnumerator<KeyValuePair<long, TSource>> source)
			{
				this.m_source = source;
			}

			// Token: 0x06006E9F RID: 28319 RVA: 0x0017D8AD File Offset: 0x0017BAAD
			public bool MoveNext()
			{
				return this.m_source.MoveNext();
			}

			// Token: 0x170012E9 RID: 4841
			// (get) Token: 0x06006EA0 RID: 28320 RVA: 0x0017D8BC File Offset: 0x0017BABC
			public TSource Current
			{
				get
				{
					KeyValuePair<long, TSource> keyValuePair = this.m_source.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x170012EA RID: 4842
			// (get) Token: 0x06006EA1 RID: 28321 RVA: 0x0017D8DC File Offset: 0x0017BADC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06006EA2 RID: 28322 RVA: 0x0017D8E9 File Offset: 0x0017BAE9
			public void Dispose()
			{
				this.m_source.Dispose();
			}

			// Token: 0x06006EA3 RID: 28323 RVA: 0x0017D8F6 File Offset: 0x0017BAF6
			public void Reset()
			{
				this.m_source.Reset();
			}

			// Token: 0x040035C1 RID: 13761
			private readonly IEnumerator<KeyValuePair<long, TSource>> m_source;
		}
	}
}
