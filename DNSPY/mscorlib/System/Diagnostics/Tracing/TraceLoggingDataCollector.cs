using System;
using System.Security;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000481 RID: 1153
	[SecuritySafeCritical]
	internal class TraceLoggingDataCollector
	{
		// Token: 0x06003717 RID: 14103 RVA: 0x000D4510 File Offset: 0x000D2710
		private TraceLoggingDataCollector()
		{
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000D4518 File Offset: 0x000D2718
		public int BeginBufferedArray()
		{
			return DataCollector.ThreadInstance.BeginBufferedArray();
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000D4524 File Offset: 0x000D2724
		public void EndBufferedArray(int bookmark, int count)
		{
			DataCollector.ThreadInstance.EndBufferedArray(bookmark, count);
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000D4532 File Offset: 0x000D2732
		public TraceLoggingDataCollector AddGroup()
		{
			return this;
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000D4535 File Offset: 0x000D2735
		public unsafe void AddScalar(bool value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000D4545 File Offset: 0x000D2745
		public unsafe void AddScalar(sbyte value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000D4555 File Offset: 0x000D2755
		public unsafe void AddScalar(byte value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000D4565 File Offset: 0x000D2765
		public unsafe void AddScalar(short value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000D4575 File Offset: 0x000D2775
		public unsafe void AddScalar(ushort value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000D4585 File Offset: 0x000D2785
		public unsafe void AddScalar(int value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000D4595 File Offset: 0x000D2795
		public unsafe void AddScalar(uint value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000D45A5 File Offset: 0x000D27A5
		public unsafe void AddScalar(long value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000D45B5 File Offset: 0x000D27B5
		public unsafe void AddScalar(ulong value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000D45C5 File Offset: 0x000D27C5
		public unsafe void AddScalar(IntPtr value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), IntPtr.Size);
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000D45D9 File Offset: 0x000D27D9
		public unsafe void AddScalar(UIntPtr value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), UIntPtr.Size);
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000D45ED File Offset: 0x000D27ED
		public unsafe void AddScalar(float value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000D45FD File Offset: 0x000D27FD
		public unsafe void AddScalar(double value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000D460D File Offset: 0x000D280D
		public unsafe void AddScalar(char value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000D461D File Offset: 0x000D281D
		public unsafe void AddScalar(Guid value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 16);
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000D462E File Offset: 0x000D282E
		public void AddBinary(string value)
		{
			DataCollector.ThreadInstance.AddBinary(value, (value == null) ? 0 : (value.Length * 2));
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000D4649 File Offset: 0x000D2849
		public void AddBinary(byte[] value)
		{
			DataCollector.ThreadInstance.AddBinary(value, (value == null) ? 0 : value.Length);
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000D465F File Offset: 0x000D285F
		public void AddArray(bool[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000D4676 File Offset: 0x000D2876
		public void AddArray(sbyte[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000D468D File Offset: 0x000D288D
		public void AddArray(short[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000D46A4 File Offset: 0x000D28A4
		public void AddArray(ushort[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000D46BB File Offset: 0x000D28BB
		public void AddArray(int[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000D46D2 File Offset: 0x000D28D2
		public void AddArray(uint[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000D46E9 File Offset: 0x000D28E9
		public void AddArray(long[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000D4700 File Offset: 0x000D2900
		public void AddArray(ulong[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000D4717 File Offset: 0x000D2917
		public void AddArray(IntPtr[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, IntPtr.Size);
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000D4732 File Offset: 0x000D2932
		public void AddArray(UIntPtr[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, UIntPtr.Size);
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000D474D File Offset: 0x000D294D
		public void AddArray(float[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000D4764 File Offset: 0x000D2964
		public void AddArray(double[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000D477B File Offset: 0x000D297B
		public void AddArray(char[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000D4792 File Offset: 0x000D2992
		public void AddArray(Guid[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 16);
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000D47AA File Offset: 0x000D29AA
		public void AddCustom(byte[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000D47C1 File Offset: 0x000D29C1
		// Note: this type is marked as 'beforefieldinit'.
		static TraceLoggingDataCollector()
		{
		}

		// Token: 0x0400186E RID: 6254
		internal static readonly TraceLoggingDataCollector Instance = new TraceLoggingDataCollector();
	}
}
