using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000485 RID: 1157
	internal class TraceLoggingMetadataCollector
	{
		// Token: 0x06003749 RID: 14153 RVA: 0x000D4AD7 File Offset: 0x000D2CD7
		internal TraceLoggingMetadataCollector()
		{
			this.impl = new TraceLoggingMetadataCollector.Impl();
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x000D4AF5 File Offset: 0x000D2CF5
		private TraceLoggingMetadataCollector(TraceLoggingMetadataCollector other, FieldMetadata group)
		{
			this.impl = other.impl;
			this.currentGroup = group;
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x0600374B RID: 14155 RVA: 0x000D4B1B File Offset: 0x000D2D1B
		// (set) Token: 0x0600374C RID: 14156 RVA: 0x000D4B23 File Offset: 0x000D2D23
		internal EventFieldTags Tags
		{
			[CompilerGenerated]
			get
			{
				return this.<Tags>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Tags>k__BackingField = value;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x0600374D RID: 14157 RVA: 0x000D4B2C File Offset: 0x000D2D2C
		internal int ScratchSize
		{
			get
			{
				return (int)this.impl.scratchSize;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x0600374E RID: 14158 RVA: 0x000D4B39 File Offset: 0x000D2D39
		internal int DataCount
		{
			get
			{
				return (int)this.impl.dataCount;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x0600374F RID: 14159 RVA: 0x000D4B46 File Offset: 0x000D2D46
		internal int PinCount
		{
			get
			{
				return (int)this.impl.pinCount;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06003750 RID: 14160 RVA: 0x000D4B53 File Offset: 0x000D2D53
		private bool BeginningBufferedArray
		{
			get
			{
				return this.bufferedArrayFieldCount == 0;
			}
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000D4B60 File Offset: 0x000D2D60
		public TraceLoggingMetadataCollector AddGroup(string name)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = this;
			if (name != null || this.BeginningBufferedArray)
			{
				FieldMetadata fieldMetadata = new FieldMetadata(name, TraceLoggingDataType.Struct, this.Tags, this.BeginningBufferedArray);
				this.AddField(fieldMetadata);
				traceLoggingMetadataCollector = new TraceLoggingMetadataCollector(this, fieldMetadata);
			}
			return traceLoggingMetadataCollector;
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000D4BA0 File Offset: 0x000D2DA0
		public void AddScalar(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			int num;
			switch (traceLoggingDataType)
			{
			case TraceLoggingDataType.Int8:
			case TraceLoggingDataType.UInt8:
				break;
			case TraceLoggingDataType.Int16:
			case TraceLoggingDataType.UInt16:
				goto IL_6F;
			case TraceLoggingDataType.Int32:
			case TraceLoggingDataType.UInt32:
			case TraceLoggingDataType.Float:
			case TraceLoggingDataType.Boolean32:
			case TraceLoggingDataType.HexInt32:
				num = 4;
				goto IL_8B;
			case TraceLoggingDataType.Int64:
			case TraceLoggingDataType.UInt64:
			case TraceLoggingDataType.Double:
			case TraceLoggingDataType.FileTime:
			case TraceLoggingDataType.HexInt64:
				num = 8;
				goto IL_8B;
			case TraceLoggingDataType.Binary:
			case (TraceLoggingDataType)16:
			case (TraceLoggingDataType)19:
				goto IL_80;
			case TraceLoggingDataType.Guid:
			case TraceLoggingDataType.SystemTime:
				num = 16;
				goto IL_8B;
			default:
				if (traceLoggingDataType != TraceLoggingDataType.Char8)
				{
					if (traceLoggingDataType != TraceLoggingDataType.Char16)
					{
						goto IL_80;
					}
					goto IL_6F;
				}
				break;
			}
			num = 1;
			goto IL_8B;
			IL_6F:
			num = 2;
			goto IL_8B;
			IL_80:
			throw new ArgumentOutOfRangeException("type");
			IL_8B:
			this.impl.AddScalar(num);
			this.AddField(new FieldMetadata(name, type, this.Tags, this.BeginningBufferedArray));
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000D4C60 File Offset: 0x000D2E60
		public void AddBinary(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			if (traceLoggingDataType != TraceLoggingDataType.Binary && traceLoggingDataType - TraceLoggingDataType.CountedUtf16String > 1)
			{
				throw new ArgumentOutOfRangeException("type");
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, this.BeginningBufferedArray));
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000D4CBC File Offset: 0x000D2EBC
		public void AddArray(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			switch (traceLoggingDataType)
			{
			case TraceLoggingDataType.Utf16String:
			case TraceLoggingDataType.MbcsString:
			case TraceLoggingDataType.Int8:
			case TraceLoggingDataType.UInt8:
			case TraceLoggingDataType.Int16:
			case TraceLoggingDataType.UInt16:
			case TraceLoggingDataType.Int32:
			case TraceLoggingDataType.UInt32:
			case TraceLoggingDataType.Int64:
			case TraceLoggingDataType.UInt64:
			case TraceLoggingDataType.Float:
			case TraceLoggingDataType.Double:
			case TraceLoggingDataType.Boolean32:
			case TraceLoggingDataType.Guid:
			case TraceLoggingDataType.FileTime:
			case TraceLoggingDataType.HexInt32:
			case TraceLoggingDataType.HexInt64:
				goto IL_7C;
			case TraceLoggingDataType.Binary:
			case (TraceLoggingDataType)16:
			case TraceLoggingDataType.SystemTime:
			case (TraceLoggingDataType)19:
				break;
			default:
				if (traceLoggingDataType == TraceLoggingDataType.Char8 || traceLoggingDataType == TraceLoggingDataType.Char16)
				{
					goto IL_7C;
				}
				break;
			}
			throw new ArgumentOutOfRangeException("type");
			IL_7C:
			if (this.BeginningBufferedArray)
			{
				throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedNestedArraysEnums"));
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, true));
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000D4D88 File Offset: 0x000D2F88
		public void BeginBufferedArray()
		{
			if (this.bufferedArrayFieldCount >= 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedNestedArraysEnums"));
			}
			this.bufferedArrayFieldCount = 0;
			this.impl.BeginBuffered();
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000D4DB5 File Offset: 0x000D2FB5
		public void EndBufferedArray()
		{
			if (this.bufferedArrayFieldCount != 1)
			{
				throw new InvalidOperationException(Environment.GetResourceString("EventSource_IncorrentlyAuthoredTypeInfo"));
			}
			this.bufferedArrayFieldCount = int.MinValue;
			this.impl.EndBuffered();
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000D4DE8 File Offset: 0x000D2FE8
		public void AddCustom(string name, TraceLoggingDataType type, byte[] metadata)
		{
			if (this.BeginningBufferedArray)
			{
				throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedCustomSerializedData"));
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, metadata));
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000D4E38 File Offset: 0x000D3038
		internal byte[] GetMetadata()
		{
			int num = this.impl.Encode(null);
			byte[] array = new byte[num];
			this.impl.Encode(array);
			return array;
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000D4E67 File Offset: 0x000D3067
		private void AddField(FieldMetadata fieldMetadata)
		{
			this.Tags = EventFieldTags.None;
			this.bufferedArrayFieldCount++;
			this.impl.fields.Add(fieldMetadata);
			if (this.currentGroup != null)
			{
				this.currentGroup.IncrementStructFieldCount();
			}
		}

		// Token: 0x040018A2 RID: 6306
		private readonly TraceLoggingMetadataCollector.Impl impl;

		// Token: 0x040018A3 RID: 6307
		private readonly FieldMetadata currentGroup;

		// Token: 0x040018A4 RID: 6308
		private int bufferedArrayFieldCount = int.MinValue;

		// Token: 0x040018A5 RID: 6309
		[CompilerGenerated]
		private EventFieldTags <Tags>k__BackingField;

		// Token: 0x02000BA2 RID: 2978
		private class Impl
		{
			// Token: 0x06006CAE RID: 27822 RVA: 0x00177DED File Offset: 0x00175FED
			public void AddScalar(int size)
			{
				checked
				{
					if (this.bufferNesting == 0)
					{
						if (!this.scalar)
						{
							this.dataCount += 1;
						}
						this.scalar = true;
						this.scratchSize = (short)((int)this.scratchSize + size);
					}
				}
			}

			// Token: 0x06006CAF RID: 27823 RVA: 0x00177E24 File Offset: 0x00176024
			public void AddNonscalar()
			{
				checked
				{
					if (this.bufferNesting == 0)
					{
						this.scalar = false;
						this.pinCount += 1;
						this.dataCount += 1;
					}
				}
			}

			// Token: 0x06006CB0 RID: 27824 RVA: 0x00177E53 File Offset: 0x00176053
			public void BeginBuffered()
			{
				if (this.bufferNesting == 0)
				{
					this.AddNonscalar();
				}
				this.bufferNesting++;
			}

			// Token: 0x06006CB1 RID: 27825 RVA: 0x00177E71 File Offset: 0x00176071
			public void EndBuffered()
			{
				this.bufferNesting--;
			}

			// Token: 0x06006CB2 RID: 27826 RVA: 0x00177E84 File Offset: 0x00176084
			public int Encode(byte[] metadata)
			{
				int num = 0;
				foreach (FieldMetadata fieldMetadata in this.fields)
				{
					fieldMetadata.Encode(ref num, metadata);
				}
				return num;
			}

			// Token: 0x06006CB3 RID: 27827 RVA: 0x00177EDC File Offset: 0x001760DC
			public Impl()
			{
			}

			// Token: 0x0400353D RID: 13629
			internal readonly List<FieldMetadata> fields = new List<FieldMetadata>();

			// Token: 0x0400353E RID: 13630
			internal short scratchSize;

			// Token: 0x0400353F RID: 13631
			internal sbyte dataCount;

			// Token: 0x04003540 RID: 13632
			internal sbyte pinCount;

			// Token: 0x04003541 RID: 13633
			private int bufferNesting;

			// Token: 0x04003542 RID: 13634
			private bool scalar;
		}
	}
}
