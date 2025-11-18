using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Plugin.Core.Enums;

namespace Plugin.Core.Network
{
	// Token: 0x02000045 RID: 69
	public abstract class BaseClientPacket
	{
		// Token: 0x06000262 RID: 610 RVA: 0x00002116 File Offset: 0x00000316
		public BaseClientPacket()
		{
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000333B File Offset: 0x0000153B
		protected internal byte[] ReadB(int Length)
		{
			return this.BReader.ReadBytes(Length);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00003349 File Offset: 0x00001549
		protected internal byte ReadC()
		{
			return this.BReader.ReadByte();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00003356 File Offset: 0x00001556
		protected internal short ReadH()
		{
			return this.BReader.ReadInt16();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00003363 File Offset: 0x00001563
		protected internal ushort ReadUH()
		{
			return this.BReader.ReadUInt16();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00003370 File Offset: 0x00001570
		protected internal int ReadD()
		{
			return this.BReader.ReadInt32();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000337D File Offset: 0x0000157D
		protected internal uint ReadUD()
		{
			return this.BReader.ReadUInt32();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000338A File Offset: 0x0000158A
		protected internal float ReadT()
		{
			return this.BReader.ReadSingle();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00003397 File Offset: 0x00001597
		protected internal long ReadQ()
		{
			return this.BReader.ReadInt64();
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000033A4 File Offset: 0x000015A4
		protected internal ulong ReadUQ()
		{
			return this.BReader.ReadUInt64();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000033B1 File Offset: 0x000015B1
		protected internal double ReadF()
		{
			return this.BReader.ReadDouble();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0001A694 File Offset: 0x00018894
		protected internal string ReadN(int Length, string CodePage)
		{
			string text = "";
			try
			{
				text = Encoding.GetEncoding(CodePage).GetString(this.ReadB(Length));
				int num = text.IndexOf('\0');
				if (num != -1)
				{
					text = text.Substring(0, num);
				}
			}
			catch
			{
			}
			return text;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0001A6E8 File Offset: 0x000188E8
		protected internal string ReadS(int Length)
		{
			string text = "";
			try
			{
				text = Encoding.UTF8.GetString(this.ReadB(Length));
				int num = text.IndexOf('\0');
				if (num != -1)
				{
					text = text.Substring(0, num);
				}
			}
			catch
			{
			}
			return text;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0001A738 File Offset: 0x00018938
		protected internal string ReadU(int Length)
		{
			string text = "";
			try
			{
				text = Encoding.Unicode.GetString(this.ReadB(Length));
				int num = text.IndexOf('\0');
				if (num != -1)
				{
					text = text.Substring(0, num);
				}
			}
			catch
			{
			}
			return text;
		}

		// Token: 0x040000DC RID: 220
		protected MemoryStream MStream;

		// Token: 0x040000DD RID: 221
		protected BinaryReader BReader;

		// Token: 0x040000DE RID: 222
		protected SafeHandle Handle;

		// Token: 0x040000DF RID: 223
		protected bool Disposed;

		// Token: 0x040000E0 RID: 224
		protected int SECURITY_KEY;

		// Token: 0x040000E1 RID: 225
		protected int HASH_CODE;

		// Token: 0x040000E2 RID: 226
		protected int SEED_LENGTH;

		// Token: 0x040000E3 RID: 227
		protected NationsEnum NATIONS;
	}
}
