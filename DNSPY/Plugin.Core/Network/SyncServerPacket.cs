using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Plugin.Core.SharpDX;

namespace Plugin.Core.Network
{
	// Token: 0x02000047 RID: 71
	public class SyncServerPacket : IDisposable
	{
		// Token: 0x06000287 RID: 647 RVA: 0x000034CE File Offset: 0x000016CE
		public SyncServerPacket()
		{
			this.MStream = new MemoryStream();
			this.BWriter = new BinaryWriter(this.MStream);
			this.Handle = new SafeFileHandle(IntPtr.Zero, true);
			this.Disposed = false;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0001A98C File Offset: 0x00018B8C
		public SyncServerPacket(long long_0)
		{
			this.MStream = new MemoryStream();
			this.MStream.SetLength(long_0);
			this.BWriter = new BinaryWriter(this.MStream);
			this.Handle = new SafeFileHandle(IntPtr.Zero, true);
			this.Disposed = false;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000350A File Offset: 0x0000170A
		public byte[] ToArray()
		{
			return this.MStream.ToArray();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00003517 File Offset: 0x00001717
		public void SetMStream(MemoryStream MStream)
		{
			this.MStream = MStream;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00003520 File Offset: 0x00001720
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000352F File Offset: 0x0000172F
		protected virtual void Dispose(bool Disposing)
		{
			if (this.Disposed)
			{
				return;
			}
			this.MStream.Dispose();
			this.BWriter.Dispose();
			if (Disposing)
			{
				this.Handle.Dispose();
			}
			this.Disposed = true;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00003565 File Offset: 0x00001765
		public void WriteB(byte[] Value, int Offset, int Length)
		{
			this.BWriter.Write(Value, Offset, Length);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00003575 File Offset: 0x00001775
		public void WriteB(byte[] Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00003583 File Offset: 0x00001783
		public void WriteC(byte Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00003591 File Offset: 0x00001791
		public void WriteH(ushort Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000359F File Offset: 0x0000179F
		public void WriteH(short Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000035AD File Offset: 0x000017AD
		public void WriteD(uint Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000035BB File Offset: 0x000017BB
		public void WriteD(int Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000035C9 File Offset: 0x000017C9
		public void WriteT(float Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000035D7 File Offset: 0x000017D7
		public void WriteF(double Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000035E5 File Offset: 0x000017E5
		public void WriteQ(ulong Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000035F3 File Offset: 0x000017F3
		public void WriteQ(long Value)
		{
			this.BWriter.Write(Value);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00003601 File Offset: 0x00001801
		public void WriteN(string Name, int Count, string CodePage)
		{
			if (Name == null)
			{
				return;
			}
			this.WriteB(Encoding.GetEncoding(CodePage).GetBytes(Name));
			this.WriteB(new byte[Count - Name.Length]);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000362C File Offset: 0x0000182C
		public void WriteS(string Text, int Count)
		{
			if (Text == null)
			{
				return;
			}
			this.WriteB(Encoding.UTF8.GetBytes(Text));
			this.WriteB(new byte[Count - Text.Length]);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00003656 File Offset: 0x00001856
		public void WriteU(string Text, int Count)
		{
			if (Text == null)
			{
				return;
			}
			this.WriteB(Encoding.Unicode.GetBytes(Text));
			this.WriteB(new byte[Count - Text.Length * 2]);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00003682 File Offset: 0x00001882
		public void GoBack(int Value)
		{
			this.BWriter.BaseStream.Position -= (long)Value;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000369D File Offset: 0x0000189D
		public void WriteHV(Half3 Half)
		{
			this.WriteH(Half.X.RawValue);
			this.WriteH(Half.Y.RawValue);
			this.WriteH(Half.Z.RawValue);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000036D5 File Offset: 0x000018D5
		public void WriteTV(Half3 Half)
		{
			this.WriteT(Half.X);
			this.WriteT(Half.Y);
			this.WriteT(Half.Z);
		}

		// Token: 0x040000E8 RID: 232
		protected MemoryStream MStream;

		// Token: 0x040000E9 RID: 233
		protected BinaryWriter BWriter;

		// Token: 0x040000EA RID: 234
		protected SafeHandle Handle;

		// Token: 0x040000EB RID: 235
		protected bool Disposed;
	}
}
