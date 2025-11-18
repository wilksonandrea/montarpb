using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection.Emit
{
	// Token: 0x02000658 RID: 1624
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public struct OpCode
	{
		// Token: 0x06004CAC RID: 19628 RVA: 0x00116954 File Offset: 0x00114B54
		internal OpCode(OpCodeValues value, int flags)
		{
			this.m_stringname = null;
			this.m_pop = (StackBehaviour)((flags >> 12) & 31);
			this.m_push = (StackBehaviour)((flags >> 17) & 31);
			this.m_operand = (OperandType)(flags & 31);
			this.m_type = (OpCodeType)((flags >> 9) & 7);
			this.m_size = (flags >> 22) & 3;
			this.m_s1 = (byte)(value >> 8);
			this.m_s2 = (byte)value;
			this.m_ctrl = (FlowControl)((flags >> 5) & 15);
			this.m_endsUncondJmpBlk = (flags & 16777216) != 0;
			this.m_stackChange = flags >> 28;
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x001169DC File Offset: 0x00114BDC
		internal bool EndsUncondJmpBlk()
		{
			return this.m_endsUncondJmpBlk;
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x001169E4 File Offset: 0x00114BE4
		internal int StackChange()
		{
			return this.m_stackChange;
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06004CAF RID: 19631 RVA: 0x001169EC File Offset: 0x00114BEC
		[__DynamicallyInvokable]
		public OperandType OperandType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_operand;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06004CB0 RID: 19632 RVA: 0x001169F4 File Offset: 0x00114BF4
		[__DynamicallyInvokable]
		public FlowControl FlowControl
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_ctrl;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06004CB1 RID: 19633 RVA: 0x001169FC File Offset: 0x00114BFC
		[__DynamicallyInvokable]
		public OpCodeType OpCodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06004CB2 RID: 19634 RVA: 0x00116A04 File Offset: 0x00114C04
		[__DynamicallyInvokable]
		public StackBehaviour StackBehaviourPop
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_pop;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06004CB3 RID: 19635 RVA: 0x00116A0C File Offset: 0x00114C0C
		[__DynamicallyInvokable]
		public StackBehaviour StackBehaviourPush
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_push;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06004CB4 RID: 19636 RVA: 0x00116A14 File Offset: 0x00114C14
		[__DynamicallyInvokable]
		public int Size
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06004CB5 RID: 19637 RVA: 0x00116A1C File Offset: 0x00114C1C
		[__DynamicallyInvokable]
		public short Value
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_size == 2)
				{
					return (short)(((int)this.m_s1 << 8) | (int)this.m_s2);
				}
				return (short)this.m_s2;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06004CB6 RID: 19638 RVA: 0x00116A40 File Offset: 0x00114C40
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.Size == 0)
				{
					return null;
				}
				string[] array = OpCode.g_nameCache;
				if (array == null)
				{
					array = new string[287];
					OpCode.g_nameCache = array;
				}
				OpCodeValues opCodeValues = (OpCodeValues)((ushort)this.Value);
				int num = (int)opCodeValues;
				if (num > 255)
				{
					if (num < 65024 || num > 65054)
					{
						return null;
					}
					num = 256 + (num - 65024);
				}
				string text = Volatile.Read<string>(ref array[num]);
				if (text != null)
				{
					return text;
				}
				text = Enum.GetName(typeof(OpCodeValues), opCodeValues).ToLowerInvariant().Replace("_", ".");
				Volatile.Write<string>(ref array[num], text);
				return text;
			}
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x00116AF3 File Offset: 0x00114CF3
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is OpCode && this.Equals((OpCode)obj);
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x00116B0B File Offset: 0x00114D0B
		[__DynamicallyInvokable]
		public bool Equals(OpCode obj)
		{
			return obj.Value == this.Value;
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x00116B1C File Offset: 0x00114D1C
		[__DynamicallyInvokable]
		public static bool operator ==(OpCode a, OpCode b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x00116B26 File Offset: 0x00114D26
		[__DynamicallyInvokable]
		public static bool operator !=(OpCode a, OpCode b)
		{
			return !(a == b);
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x00116B32 File Offset: 0x00114D32
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this.Value;
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x00116B3A File Offset: 0x00114D3A
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04002138 RID: 8504
		internal const int OperandTypeMask = 31;

		// Token: 0x04002139 RID: 8505
		internal const int FlowControlShift = 5;

		// Token: 0x0400213A RID: 8506
		internal const int FlowControlMask = 15;

		// Token: 0x0400213B RID: 8507
		internal const int OpCodeTypeShift = 9;

		// Token: 0x0400213C RID: 8508
		internal const int OpCodeTypeMask = 7;

		// Token: 0x0400213D RID: 8509
		internal const int StackBehaviourPopShift = 12;

		// Token: 0x0400213E RID: 8510
		internal const int StackBehaviourPushShift = 17;

		// Token: 0x0400213F RID: 8511
		internal const int StackBehaviourMask = 31;

		// Token: 0x04002140 RID: 8512
		internal const int SizeShift = 22;

		// Token: 0x04002141 RID: 8513
		internal const int SizeMask = 3;

		// Token: 0x04002142 RID: 8514
		internal const int EndsUncondJmpBlkFlag = 16777216;

		// Token: 0x04002143 RID: 8515
		internal const int StackChangeShift = 28;

		// Token: 0x04002144 RID: 8516
		private string m_stringname;

		// Token: 0x04002145 RID: 8517
		private StackBehaviour m_pop;

		// Token: 0x04002146 RID: 8518
		private StackBehaviour m_push;

		// Token: 0x04002147 RID: 8519
		private OperandType m_operand;

		// Token: 0x04002148 RID: 8520
		private OpCodeType m_type;

		// Token: 0x04002149 RID: 8521
		private int m_size;

		// Token: 0x0400214A RID: 8522
		private byte m_s1;

		// Token: 0x0400214B RID: 8523
		private byte m_s2;

		// Token: 0x0400214C RID: 8524
		private FlowControl m_ctrl;

		// Token: 0x0400214D RID: 8525
		private bool m_endsUncondJmpBlk;

		// Token: 0x0400214E RID: 8526
		private int m_stackChange;

		// Token: 0x0400214F RID: 8527
		private static volatile string[] g_nameCache;
	}
}
