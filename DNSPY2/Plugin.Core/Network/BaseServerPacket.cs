using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Plugin.Core.Enums;

namespace Plugin.Core.Network
{
	// Token: 0x02000048 RID: 72
	public abstract class BaseServerPacket
	{
		// Token: 0x0600029E RID: 670 RVA: 0x00002116 File Offset: 0x00000316
		public BaseServerPacket()
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000370A File Offset: 0x0000190A
		protected internal void WriteB(byte[] Value, int Offset, int Length)
		{
			this.BWriter.Write(Value, Offset, Length);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000371A File Offset: 0x0000191A
		protected internal void WriteB(byte[] Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00003728 File Offset: 0x00001928
		protected internal void WriteC(byte Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00003736 File Offset: 0x00001936
		protected internal void WriteH(ushort Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00003744 File Offset: 0x00001944
		protected internal void WriteH(short Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00003752 File Offset: 0x00001952
		protected internal void WriteD(uint Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00003760 File Offset: 0x00001960
		protected internal void WriteD(int Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000376E File Offset: 0x0000196E
		protected internal void WriteT(float Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000377C File Offset: 0x0000197C
		protected internal void WriteF(double Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000378A File Offset: 0x0000198A
		protected internal void WriteQ(ulong Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00003798 File Offset: 0x00001998
		protected internal void WriteQ(long Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000037A6 File Offset: 0x000019A6
		protected internal void WriteN(string Text, int Count, string CodePage)
		{
			if (Text == null)
			{
				return;
			}
			this.WriteB(Encoding.GetEncoding(CodePage).GetBytes(Text));
			this.WriteB(new byte[Count - Text.Length]);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000037D1 File Offset: 0x000019D1
		protected internal void WriteS(string Text, int Count)
		{
			if (Text == null)
			{
				return;
			}
			this.WriteB(Encoding.UTF8.GetBytes(Text));
			this.WriteB(new byte[Count - Text.Length]);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000037FB File Offset: 0x000019FB
		protected internal void WriteU(string Text, int Count)
		{
			if (Text == null)
			{
				return;
			}
			this.WriteB(Encoding.Unicode.GetBytes(Text));
			this.WriteB(new byte[Count - Text.Length * 2]);
		}

		// Token: 0x040000EC RID: 236
		protected MemoryStream MStream;

		// Token: 0x040000ED RID: 237
		protected BinaryWriter BWriter;

		// Token: 0x040000EE RID: 238
		protected SafeHandle Handle;

		// Token: 0x040000EF RID: 239
		protected bool Disposed;

		// Token: 0x040000F0 RID: 240
		protected int SECURITY_KEY;

		// Token: 0x040000F1 RID: 241
		protected int HASH_CODE;

		// Token: 0x040000F2 RID: 242
		protected int SEED_LENGTH;

		// Token: 0x040000F3 RID: 243
		protected NationsEnum NATIONS;
	}
}
