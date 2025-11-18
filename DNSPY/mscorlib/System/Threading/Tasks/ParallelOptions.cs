using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000550 RID: 1360
	[__DynamicallyInvokable]
	public class ParallelOptions
	{
		// Token: 0x06004010 RID: 16400 RVA: 0x000EE059 File Offset: 0x000EC259
		[__DynamicallyInvokable]
		public ParallelOptions()
		{
			this.m_scheduler = TaskScheduler.Default;
			this.m_maxDegreeOfParallelism = -1;
			this.m_cancellationToken = CancellationToken.None;
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06004011 RID: 16401 RVA: 0x000EE07E File Offset: 0x000EC27E
		// (set) Token: 0x06004012 RID: 16402 RVA: 0x000EE086 File Offset: 0x000EC286
		[__DynamicallyInvokable]
		public TaskScheduler TaskScheduler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_scheduler;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_scheduler = value;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06004013 RID: 16403 RVA: 0x000EE08F File Offset: 0x000EC28F
		internal TaskScheduler EffectiveTaskScheduler
		{
			get
			{
				if (this.m_scheduler == null)
				{
					return TaskScheduler.Current;
				}
				return this.m_scheduler;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x000EE0A5 File Offset: 0x000EC2A5
		// (set) Token: 0x06004015 RID: 16405 RVA: 0x000EE0AD File Offset: 0x000EC2AD
		[__DynamicallyInvokable]
		public int MaxDegreeOfParallelism
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_maxDegreeOfParallelism;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == 0 || value < -1)
				{
					throw new ArgumentOutOfRangeException("MaxDegreeOfParallelism");
				}
				this.m_maxDegreeOfParallelism = value;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x000EE0C8 File Offset: 0x000EC2C8
		// (set) Token: 0x06004017 RID: 16407 RVA: 0x000EE0D0 File Offset: 0x000EC2D0
		[__DynamicallyInvokable]
		public CancellationToken CancellationToken
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_cancellationToken;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_cancellationToken = value;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x000EE0DC File Offset: 0x000EC2DC
		internal int EffectiveMaxConcurrencyLevel
		{
			get
			{
				int num = this.MaxDegreeOfParallelism;
				int maximumConcurrencyLevel = this.EffectiveTaskScheduler.MaximumConcurrencyLevel;
				if (maximumConcurrencyLevel > 0 && maximumConcurrencyLevel != 2147483647)
				{
					num = ((num == -1) ? maximumConcurrencyLevel : Math.Min(maximumConcurrencyLevel, num));
				}
				return num;
			}
		}

		// Token: 0x04001ACB RID: 6859
		private TaskScheduler m_scheduler;

		// Token: 0x04001ACC RID: 6860
		private int m_maxDegreeOfParallelism;

		// Token: 0x04001ACD RID: 6861
		private CancellationToken m_cancellationToken;
	}
}
