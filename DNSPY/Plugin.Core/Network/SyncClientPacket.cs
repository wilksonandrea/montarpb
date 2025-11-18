using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;

namespace Plugin.Core.Network
{
	// Token: 0x02000046 RID: 70
	public class SyncClientPacket : IDisposable
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0001A788 File Offset: 0x00018988
		public SyncClientPacket(byte[] byte_0)
		{
			this.MStream = new MemoryStream(byte_0, 0, byte_0.Length);
			this.BReader = new BinaryReader(this.MStream);
			this.Handle = new SafeFileHandle(IntPtr.Zero, true);
			this.Disposed = false;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000033BE File Offset: 0x000015BE
		public byte[] ToArray()
		{
			return this.MStream.ToArray();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000033CB File Offset: 0x000015CB
		public void SetMStream(MemoryStream MStream)
		{
			this.MStream = MStream;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000033D4 File Offset: 0x000015D4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000033E3 File Offset: 0x000015E3
		protected virtual void Dispose(bool Disposing)
		{
			if (this.Disposed)
			{
				return;
			}
			this.MStream.Dispose();
			this.BReader.Dispose();
			if (Disposing)
			{
				this.Handle.Dispose();
			}
			this.Disposed = true;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00003419 File Offset: 0x00001619
		public byte[] ReadB(int Length)
		{
			return this.BReader.ReadBytes(Length);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00003427 File Offset: 0x00001627
		public byte ReadC()
		{
			return this.BReader.ReadByte();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00003434 File Offset: 0x00001634
		public short ReadH()
		{
			return this.BReader.ReadInt16();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00003441 File Offset: 0x00001641
		public ushort ReadUH()
		{
			return this.BReader.ReadUInt16();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000344E File Offset: 0x0000164E
		public int ReadD()
		{
			return this.BReader.ReadInt32();
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000345B File Offset: 0x0000165B
		public uint ReadUD()
		{
			return this.BReader.ReadUInt32();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00003468 File Offset: 0x00001668
		public float ReadT()
		{
			return this.BReader.ReadSingle();
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00003475 File Offset: 0x00001675
		public double ReadF()
		{
			return this.BReader.ReadDouble();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00003482 File Offset: 0x00001682
		public long ReadQ()
		{
			return this.BReader.ReadInt64();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000348F File Offset: 0x0000168F
		public ulong ReadUQ()
		{
			return this.BReader.ReadUInt64();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0001A7D4 File Offset: 0x000189D4
		public string ReadN(int Length, string CodePage)
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

		// Token: 0x06000280 RID: 640 RVA: 0x0001A828 File Offset: 0x00018A28
		public string ReadS(int Length)
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

		// Token: 0x06000281 RID: 641 RVA: 0x0001A878 File Offset: 0x00018A78
		public string ReadU(int Length)
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

		// Token: 0x06000282 RID: 642 RVA: 0x0001A8C8 File Offset: 0x00018AC8
		public byte ReadC(out bool Exception)
		{
			byte b2;
			try
			{
				byte b = this.BReader.ReadByte();
				Exception = false;
				b2 = b;
			}
			catch
			{
				Exception = true;
				b2 = 0;
			}
			return b2;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0001A900 File Offset: 0x00018B00
		public ushort ReadUH(out bool Exception)
		{
			ushort num2;
			try
			{
				ushort num = this.BReader.ReadUInt16();
				Exception = false;
				num2 = num;
			}
			catch
			{
				Exception = true;
				num2 = 0;
			}
			return num2;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0001A938 File Offset: 0x00018B38
		public void Advance(int Bytes)
		{
			if ((this.BReader.BaseStream.Position += (long)Bytes) > this.BReader.BaseStream.Length)
			{
				CLogger.Print("Advance crashed.", LoggerType.Warning, null);
				throw new Exception("Offset has exceeded the buffer value.");
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000349C File Offset: 0x0000169C
		public Half3 ReadUHV()
		{
			return new Half3(this.ReadUH(), this.ReadUH(), this.ReadUH());
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000034B5 File Offset: 0x000016B5
		public Half3 ReadTV()
		{
			return new Half3(this.ReadT(), this.ReadT(), this.ReadT());
		}

		// Token: 0x040000E4 RID: 228
		protected MemoryStream MStream;

		// Token: 0x040000E5 RID: 229
		protected BinaryReader BReader;

		// Token: 0x040000E6 RID: 230
		protected SafeHandle Handle;

		// Token: 0x040000E7 RID: 231
		protected bool Disposed;
	}
}
