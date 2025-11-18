using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200041D RID: 1053
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct EventDescriptor
	{
		// Token: 0x06003447 RID: 13383 RVA: 0x000C6F9C File Offset: 0x000C519C
		public EventDescriptor(int traceloggingId, byte level, byte opcode, long keywords)
		{
			this.m_id = 0;
			this.m_version = 0;
			this.m_channel = 0;
			this.m_traceloggingId = traceloggingId;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_task = 0;
			this.m_keywords = keywords;
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000C6FD8 File Offset: 0x000C51D8
		public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
		{
			if (id < 0)
			{
				throw new ArgumentOutOfRangeException("id", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (id > 65535)
			{
				throw new ArgumentOutOfRangeException("id", Environment.GetResourceString("ArgumentOutOfRange_NeedValidId", new object[] { 1, ushort.MaxValue }));
			}
			this.m_traceloggingId = 0;
			this.m_id = (ushort)id;
			this.m_version = version;
			this.m_channel = channel;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_keywords = keywords;
			if (task < 0)
			{
				throw new ArgumentOutOfRangeException("task", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (task > 65535)
			{
				throw new ArgumentOutOfRangeException("task", Environment.GetResourceString("ArgumentOutOfRange_NeedValidId", new object[] { 1, ushort.MaxValue }));
			}
			this.m_task = (ushort)task;
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000C70C9 File Offset: 0x000C52C9
		public int EventId
		{
			get
			{
				return (int)this.m_id;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x000C70D1 File Offset: 0x000C52D1
		public byte Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x0600344B RID: 13387 RVA: 0x000C70D9 File Offset: 0x000C52D9
		public byte Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x000C70E1 File Offset: 0x000C52E1
		public byte Level
		{
			get
			{
				return this.m_level;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x0600344D RID: 13389 RVA: 0x000C70E9 File Offset: 0x000C52E9
		public byte Opcode
		{
			get
			{
				return this.m_opcode;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x0600344E RID: 13390 RVA: 0x000C70F1 File Offset: 0x000C52F1
		public int Task
		{
			get
			{
				return (int)this.m_task;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x0600344F RID: 13391 RVA: 0x000C70F9 File Offset: 0x000C52F9
		public long Keywords
		{
			get
			{
				return this.m_keywords;
			}
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000C7101 File Offset: 0x000C5301
		public override bool Equals(object obj)
		{
			return obj is EventDescriptor && this.Equals((EventDescriptor)obj);
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000C7119 File Offset: 0x000C5319
		public override int GetHashCode()
		{
			return (int)(this.m_id ^ (ushort)this.m_version ^ (ushort)this.m_channel ^ (ushort)this.m_level ^ (ushort)this.m_opcode ^ this.m_task) ^ (int)this.m_keywords;
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000C714C File Offset: 0x000C534C
		public bool Equals(EventDescriptor other)
		{
			return this.m_id == other.m_id && this.m_version == other.m_version && this.m_channel == other.m_channel && this.m_level == other.m_level && this.m_opcode == other.m_opcode && this.m_task == other.m_task && this.m_keywords == other.m_keywords;
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000C71BE File Offset: 0x000C53BE
		public static bool operator ==(EventDescriptor event1, EventDescriptor event2)
		{
			return event1.Equals(event2);
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000C71C8 File Offset: 0x000C53C8
		public static bool operator !=(EventDescriptor event1, EventDescriptor event2)
		{
			return !event1.Equals(event2);
		}

		// Token: 0x04001732 RID: 5938
		[FieldOffset(0)]
		private int m_traceloggingId;

		// Token: 0x04001733 RID: 5939
		[FieldOffset(0)]
		private ushort m_id;

		// Token: 0x04001734 RID: 5940
		[FieldOffset(2)]
		private byte m_version;

		// Token: 0x04001735 RID: 5941
		[FieldOffset(3)]
		private byte m_channel;

		// Token: 0x04001736 RID: 5942
		[FieldOffset(4)]
		private byte m_level;

		// Token: 0x04001737 RID: 5943
		[FieldOffset(5)]
		private byte m_opcode;

		// Token: 0x04001738 RID: 5944
		[FieldOffset(6)]
		private ushort m_task;

		// Token: 0x04001739 RID: 5945
		[FieldOffset(8)]
		private long m_keywords;
	}
}
